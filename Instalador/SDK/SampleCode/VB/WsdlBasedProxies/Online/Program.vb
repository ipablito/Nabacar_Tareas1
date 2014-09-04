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
Imports System.Collections.Generic
Imports System.Globalization
Imports System.IdentityModel.Tokens
Imports System.ServiceModel
Imports System.ServiceModel.Description

Imports Microsoft.Crm.Services.Utility

Imports CrmSdk
Imports CrmSdk.Discovery

Namespace Microsoft.Crm.Sdk.Samples

    Friend NotInheritable Class Program

#Region "Constants"
        ''' <summary>
        ''' Microsoft account (e.g. youremail@live.com) or Microsoft Office 365 (Org ID e.g. youremail@yourorg.onmicrosoft.com) User Name.
        ''' </summary>
        Private Const UserName As String = "youremail@yourorg.onmicrosoft.com"

        ''' <summary>
        ''' Microsoft account or Microsoft Office 365 (Org ID) Password.
        ''' </summary>
        Private Const UserPassword As String = "password"

        ''' <summary>
        ''' Unique Name of the organization
        ''' </summary>
        Private Const OrganizationUniqueName As String = "orgname"

        ''' <summary>
        ''' URL for the Discovery Service
        ''' For North America
        '''     Microsoft account, discovery service url could be https://dev.crm.dynamics.com/XRMServices/2011/Discovery.svc
        '''     Microsoft Office 365, discovery service url could be https://disco.crm.dynamics.com/XRMServices/2011/Discovery.svc
        ''' To use appropriate discovery service url for other environments refer http://technet.microsoft.com/en-us/library/gg309401.aspx
        ''' </summary>
        Private Const DiscoveryServiceUrl As String = "https://disco.crm.dynamics.com/XRMServices/2011/Discovery.svc"

        ''' <summary>
        ''' Suffix for the Flat WSDL
        ''' </summary>
        Private Const WsdlSuffix As String = "?wsdl"
#End Region

        Private Sub New()
        End Sub
        Shared Sub Main(ByVal args() As String)
            ' Retrieve the authentication policy for the Discovery service.
            Dim discoveryPolicy As New OnlineAuthenticationPolicy(DiscoveryServiceUrl & WsdlSuffix)
            ' Authenticate the user using the authentication policy.
            Dim discoveryToken As SecurityToken = Authenticate(discoveryPolicy, UserName, UserPassword)

            ' Retrieve the organization service URL for the given organization
            Dim organizationServiceUrl As String = DiscoverOrganizationUrl(discoveryToken, OrganizationUniqueName,
                                                                           DiscoveryServiceUrl, discoveryPolicy.IssuerUri)

            ' The Discovery Service token cannot be reused against the Organization Service as the Issuer and AppliesTo may differ between
            ' the discovery and organization services.
            Dim organizationPolicy As New OnlineAuthenticationPolicy(organizationServiceUrl & WsdlSuffix)
            Dim organizationToken As SecurityToken = Authenticate(organizationPolicy, UserName, UserPassword)

            ' Execute the sample
            ExecuteWhoAmI(organizationToken, organizationServiceUrl, organizationPolicy.IssuerUri)
            Console.Write("Press [Enter] to exit.... ")
            Console.ReadLine()
        End Sub

        Private Shared Function Authenticate(ByVal policy As OnlineAuthenticationPolicy,
                                             ByVal userName As String,
                                             ByVal password As String) As SecurityToken
            Dim credentials As New ClientCredentials()
            credentials.UserName.UserName = userName
            credentials.UserName.Password = password

            Return WsdlTokenManager.Authenticate(credentials, policy.AppliesTo, policy.Policy, policy.IssuerUri)
        End Function

        Private Shared Function DiscoverOrganizationUrl(ByVal token As SecurityToken,
                                                        ByVal organizationName As String,
                                                        ByVal discoveryServiceUrl As String,
                                                        ByVal issuerUri As Uri) As String
            Using client As New DiscoveryServiceClient("CustomBinding_IDiscoveryService",
                                                       discoveryServiceUrl)
                client.ConfigureCrmOnlineBinding(issuerUri)
                client.Token = token

                Dim request As New RetrieveOrganizationRequest() With {.UniqueName = organizationName}
                Dim response As RetrieveOrganizationResponse
                Try
                    response = CType(client.Execute(request), CrmSdk.Discovery.RetrieveOrganizationResponse)
                Catch e1 As CommunicationException
                    Throw
                End Try

                For Each endpoint As KeyValuePair(Of EndpointType, String) In response.Detail.Endpoints
                    If EndpointType.OrganizationService.Equals(endpoint.Key) Then
                        Console.WriteLine("Organization Service URL: {0}", endpoint.Value)
                        Return endpoint.Value
                    End If
                Next endpoint

                Throw New InvalidOperationException(
                    String.Format(CultureInfo.InvariantCulture,
                                  "Organization {0} does not have an OrganizationService endpoint defined.",
                                  organizationName))
            End Using
        End Function

        Private Shared Sub ExecuteWhoAmI(ByVal token As SecurityToken,
                                         ByVal serviceUrl As String, ByVal issuerUri As Uri)
            Using client As New OrganizationServiceClient("CustomBinding_IOrganizationService",
                                                          New EndpointAddress(serviceUrl))
                client.ConfigureCrmOnlineBinding(issuerUri)
                client.Token = token

                Dim request As New OrganizationRequest()
                request.RequestName = "WhoAmI"

                Dim response As OrganizationResponse = CType(client.Execute(request), OrganizationResponse)

                For Each result As KeyValuePair(Of String, Object) In response.Results
                    If "UserId" = result.Key Then
                        Console.WriteLine("User ID: {0}", result.Value)
                        Exit For
                    End If
                Next result
            End Using
        End Sub
    End Class
End Namespace