' =====================================================================
'
'  This file is part of the Microsoft Dynamics CRM SDK code samples.
'
'  Copyright (C) Microsoft Corporation.  All rights reserved.
'
'  This source code is intended only as a supplement to Microsoft
'  Development Tools and/or on-line documentation.  See these other
'  materials for detailed information regarding Microsoft code samples.
'
'  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
'  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'  PARTICULAR PURPOSE.
'
' =====================================================================
Imports System
Imports System.Globalization
Imports System.Net
Imports System.Xml

Namespace Microsoft.Crm.Services.Utility
    ''' <summary>
    ''' Microsoft Online (Microsoft account as well as Microsoft Office 365 a.k.a. OSDP / OrgId)) Authentication Policy 
    ''' for CRM Web Services.
    ''' </summary>
    Public NotInheritable Class OnlineAuthenticationPolicy
        ''' <summary>
        ''' Construct an Instance of the OnlineAuthenticationPolicy class
        ''' </summary>
        ''' <param name="flatWsdlUrl">URL to the Flattened WSDL</param>
        Public Sub New(ByVal flatWsdlUrl As String)
            Me.Initialize(flatWsdlUrl)
        End Sub

        ''' <summary>
        ''' Construct an Instance of the OnlineAuthenticationPolicy class
        ''' </summary>
        ''' <param name="appliesTo">AppliesTo for the web service</param>
        ''' <param name="policy">Microsoft account Policy that should be used</param>
        ''' <param name="issuerUri">Issuer URI that should be used for authenticating tokens</param>
        Public Sub New(ByVal appliesTo As String, ByVal policy As String, ByVal issuerUri As Uri)
            Me.AppliesTo = appliesTo
            Me.Policy = policy
            Me.IssuerUri = issuerUri
        End Sub

#Region "Properties"
        ''' <summary>
        ''' AppliesTo value that should be set on the service
        ''' </summary>
        Private privateAppliesTo As String
        Public Property AppliesTo() As String
            Get
                Return privateAppliesTo
            End Get
            Private Set(ByVal value As String)
                privateAppliesTo = value
            End Set
        End Property

        ''' <summary>
        ''' Microsoft account / Org Id Policy
        ''' </summary>
        Private privatePolicy As String
        Public Property Policy() As String
            Get
                Return privatePolicy
            End Get
            Private Set(ByVal value As String)
                privatePolicy = value
            End Set
        End Property

        ''' <summary>
        ''' Microsoft account / Org Id Issuer that issues the tokens
        ''' </summary>
        Private privateIssuerUri As Uri
        Public Property IssuerUri() As Uri
            Get
                Return privateIssuerUri
            End Get
            Private Set(ByVal value As Uri)
                privateIssuerUri = value
            End Set
        End Property
#End Region

#Region "Methods"
        Private Sub Initialize(ByVal flatWsdlUrl As String)
            If String.IsNullOrWhiteSpace(flatWsdlUrl) Then
                Throw New ArgumentNullException("flatWsdlUrl")
            End If

            ' Parse the WSDL
            Dim wsdl As XmlDocument = DownloadWsdl(flatWsdlUrl)

            ' Setup the namespace manager required for executing queries
            Dim namespaceManager As New XmlNamespaceManager(wsdl.NameTable)
            namespaceManager.AddNamespace("wsdl", "http://schemas.xmlsoap.org/wsdl/")

            ' Fetching target wsdl uri from "wsdl:import" node.
            Dim importNode As XmlNode = SelectFirstNodeOrDefault(wsdl.DocumentElement, "wsdl:import", namespaceManager)
            Dim targetWsdlUri As String = ReadAttributeValue(importNode, "location")
            Dim targetWsdl As XmlDocument = DownloadWsdl(targetWsdlUri)

            ' Initialize with the downloaded WSDL
            Me.Initialize(flatWsdlUrl, targetWsdl)
        End Sub

        Private Sub Initialize(ByVal url As String, ByVal wsdl As XmlDocument)
            Const wsp As String = "wsp"
            Const MsXrm As String = "ms-xrm"
            Const MsXrm2012 As String = "ms-xrm2012"
            Const sp As String = "sp"
            Const ad As String = "ad"
            Const ExactlyOne As String = ":ExactlyOne/"
            Const All As String = ":All/"
            Const AuthenticationPolicy As String = ":AuthenticationPolicy/"
            Const SecureTokenService As String = ":SecureTokenService/"
            Const ReferenceParameters As String = ":ReferenceParameters/"
            Const PolicyNodePath As String = wsp & ":Policy"
            Const AllPathFormat As String = "{0}{1}{0}{2}{3}"
            Const TrustPathFormat As String = AllPathFormat & "{4}{3}{5}{3}"
            Const LiveTrustPathFormat As String = TrustPathFormat & ":LiveTrust"
            Const OrgTrustPathFormat As String = TrustPathFormat & ":OrgTrust"
            Const IssuerContainerPathFormat As String = AllPathFormat & ":SignedSupportingTokens/{0}:Policy/{3}:IssuedToken/{3}:Issuer"

            Const LiveAppliesToNodeName As String = MsXrm & ":AppliesTo"
            Const OrgAppliesToNodeName As String = MsXrm2012 & ":AppliesTo"
            Const LivePolicyNodeName As String = MsXrm & ":LivePolicy"
            Const OrgPolicyNodeName As String = MsXrm2012 & ":LivePolicy"

            Const LiveIdReferenceIssuerUriNodeName As String = ad & ReferenceParameters & MsXrm2012 & ":LiveIssuer"
            Const OrgIdReferenceIssuerUriNodeName As String = ad & ReferenceParameters & MsXrm2012 & ":OrgIdIssuer"
            Const AddressIssuerUriNodeName As String = ad & ":Address"

            ' Setup the namespace manager required for executing queries
            Dim namespaceManager As New XmlNamespaceManager(wsdl.NameTable)
            namespaceManager.AddNamespace(wsp, "http://schemas.xmlsoap.org/ws/2004/09/policy")
            namespaceManager.AddNamespace(MsXrm, "http://schemas.microsoft.com/xrm/2011/Contracts/Services")
            namespaceManager.AddNamespace(MsXrm2012, "http://schemas.microsoft.com/xrm/2012/Contracts/Services")
            namespaceManager.AddNamespace(sp, "http://docs.oasis-open.org/ws-sx/ws-securitypolicy/200702")
            namespaceManager.AddNamespace(ad, "http://www.w3.org/2005/08/addressing")

            ' Retrieve the root policy node to fetch properties AppliesTo, Policy and IssuerUri required for authentication. 
            Dim policyNode As XmlNode = SelectFirstNodeOrDefault(wsdl.DocumentElement, PolicyNodePath, namespaceManager)
            If Nothing IsNot policyNode Then
                ' Set authentication type to OnlineFederation.
                Dim authWithOrgId As Boolean = True

                ' Construct the org trust path.
                Dim orgIdentityProviderTrustPath As String = String.Format(CultureInfo.InvariantCulture, OrgTrustPathFormat, wsp,
                                                                           ExactlyOne, All, MsXrm2012, AuthenticationPolicy, SecureTokenService)

                ' Retrieve the OrgId authentication policy (that contains the AppliesTo and LivePolicy values).
                Dim IdentityProviderTrustConfiguration As XmlNode = SelectFirstNodeOrDefault(policyNode, orgIdentityProviderTrustPath, namespaceManager)

                ' If OrgId authentication policy node note found then try Microsoft account policy node.
                If Nothing Is IdentityProviderTrustConfiguration Then
                    ' Construct the Microsoft account trust path.
                    Dim liveIdentityProviderTrustPath As String = String.Format(CultureInfo.InvariantCulture, LiveTrustPathFormat,
                                                                                wsp, ExactlyOne, All, MsXrm, AuthenticationPolicy, SecureTokenService)

                    IdentityProviderTrustConfiguration = SelectFirstNodeOrDefault(policyNode, liveIdentityProviderTrustPath, namespaceManager)
                    ' Set OnlineFederation authentication flag to false.
                    authWithOrgId = False
                End If

                If Nothing IsNot IdentityProviderTrustConfiguration Then
                    ' Retrieve AppliesTo value based on IdentityProvider type.
                    Dim appliesTo As String = ReadNodeValue(IdentityProviderTrustConfiguration,
                                                            If(authWithOrgId, OrgAppliesToNodeName, LiveAppliesToNodeName),
                                                            namespaceManager)
                    ' Retrieve LivePolicy value based on IdentityProvider type.
                    Dim livePolicy As String = ReadNodeValue(IdentityProviderTrustConfiguration,
                                                             If(authWithOrgId, OrgPolicyNodeName, LivePolicyNodeName),
                                                             namespaceManager)

                    Dim issuerContainerPath As String = String.Format(CultureInfo.InvariantCulture, IssuerContainerPathFormat, wsp, ExactlyOne, All, sp)

                    ' The issuer container node contains the Issuer URI. Since the Discovery Service exposes both Office 365 
                    ' and Microsoft account authentication mechanisms, it lists multiple issuers. In that case, the issuer is 
                    ' listed under the reference parameters. In other scenarios, it is listed in the Address node instead.
                    Dim issuerContainerNode As XmlNode = SelectFirstNodeOrDefault(policyNode, issuerContainerPath, namespaceManager)
                    If Nothing IsNot issuerContainerNode Then
                        ' Read the value from the reference parameters. If it is not set, check the Address node.
                        Dim issuerUri As String = ReadNodeValue(issuerContainerNode,
                                                                If(authWithOrgId, OrgIdReferenceIssuerUriNodeName, LiveIdReferenceIssuerUriNodeName),
                                                                namespaceManager)

                        ' Try Address node to find issuer Uri.
                        If String.IsNullOrWhiteSpace(issuerUri) Then
                            issuerUri = ReadNodeValue(issuerContainerNode, AddressIssuerUriNodeName, namespaceManager)
                        End If
                        ' If the issuer was discovered, it means that all of the required information has been found.
                        If Not String.IsNullOrWhiteSpace(issuerUri) Then
                            Me.Initialize(appliesTo, livePolicy, New Uri(issuerUri))
                            Return
                        End If
                    End If
                End If
            End If

            ' Some piece of information could not be found.
            Throw New InvalidOperationException(String.Format(CultureInfo.InvariantCulture,
                                                              "Unable to parse the authentication policy from the WSDL ""{0}"".",
                                                              url))
        End Sub

        Private Sub Initialize(ByVal appliesTo As String, ByVal policy As String, ByVal issuerUri As Uri)
            If String.IsNullOrWhiteSpace(appliesTo) Then
                Throw New ArgumentNullException("appliesTo")
            ElseIf String.IsNullOrWhiteSpace(policy) Then
                Throw New ArgumentNullException("policy")
            ElseIf Nothing Is issuerUri Then
                Throw New ArgumentNullException("issuerUri")
            End If

            Me.AppliesTo = appliesTo
            Me.Policy = policy
            Me.IssuerUri = issuerUri
        End Sub

        Private Function DownloadWsdl(ByVal flatWsdlUrl As String) As XmlDocument
            ' Download the Flat WSDL to determine the authentication policy information
            Dim client As New WebClient()
            Dim wsdl As String
            Try
                wsdl = client.DownloadString(flatWsdlUrl)
            Catch ex As WebException
                Throw New InvalidOperationException(String.Format(CultureInfo.InvariantCulture,
                                                                  "Unable to download the authentication policy from WSDL ""{0}"".",
                                                                  flatWsdlUrl), ex)
            End Try

            ' Parse the XML into a document
            Dim wsdlDoc As New XmlDocument()
            Try
                wsdlDoc.LoadXml(wsdl)
            Catch ex As XmlException
                Throw New InvalidOperationException(String.Format(CultureInfo.InvariantCulture,
                                                                  "Unable to parse the WSDL ""{0}"".", flatWsdlUrl), ex)
            End Try

            Return wsdlDoc
        End Function

        Private Function SelectFirstNodeOrDefault(ByVal root As XmlNode, ByVal path As String, ByVal namespaceManager As XmlNamespaceManager) As XmlNode
            Dim nodes As XmlNodeList = root.SelectNodes(path, namespaceManager)
            If 0 = nodes.Count Then
                Return Nothing
            End If

            Return nodes(0)
        End Function

        Private Function ReadNodeValue(ByVal parent As XmlNode, ByVal nodeName As String, ByVal namespaceManager As XmlNamespaceManager) As String
            Dim node As XmlNode = SelectFirstNodeOrDefault(parent, nodeName, namespaceManager)
            If Nothing IsNot node Then
                Return node.InnerText
            End If

            Return Nothing
        End Function

        Private Function ReadAttributeValue(ByVal parent As XmlNode, ByVal attributeName As String) As String
            Dim attribute As XmlAttribute = parent.Attributes(attributeName)
            If Nothing IsNot attribute Then
                Return attribute.InnerText
            End If
            Return Nothing
        End Function
#End Region
    End Class
End Namespace