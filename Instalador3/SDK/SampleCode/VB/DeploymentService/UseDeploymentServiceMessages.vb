' =====================================================================
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
' =====================================================================

'<snippetUseDeploymentServiceMessages>
Imports System.Net
Imports System.ServiceModel

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk.Query
Imports Microsoft.Xrm.Sdk.Client

' These namespaces are found in the Microsoft.Xrm.Sdk.Deployment.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk.Deployment

Namespace Microsoft.Crm.Sdk.Samples
    ''' <summary>
    ''' Demonstrates how to retrieve deployment information programmatically.</summary>
    ''' <remarks>
    ''' NOTE: The deployment service only supports Active Directory, so the user running
    ''' this sample must have a valid Active Directory account.
    ''' </remarks>
    Public Class UseDeploymentServiceMessages
#Region "Class Level Members"

        Private _serviceProxy As OrganizationServiceProxy

#End Region ' Class Level Members

#Region "How To Sample Code"
        ''' <summary>
        ''' This method first connects to the Deployment service. Then,
        ''' a variety of messages are used to retrieve deployment information.
        ''' </summary>
        ''' <param name="serverConfig">Contains server connection information.</param>
        ''' <param name="promptforDelete">When True, the user will be prompted to delete
        ''' all created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                       ByVal promptforDelete As Boolean)
            Try
                '<snippetUseDeploymentServiceMessages1>
                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy will be properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    ' This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes()

                    ' Instantiate DeploymentServiceClient for calling the service.
                    Dim serviceClient As DeploymentServiceClient =
                        Microsoft.Xrm.Sdk.Deployment.Proxy.ProxyClientHelper.CreateClient(
                            New Uri(serverConfig.DiscoveryUri.ToString() _
                                    .Replace("Services", "Deployment") _
                                    .Replace("Discovery", "Deployment")))

                    ' Setting credentials from the current security context. 
                    If serverConfig.Credentials Is Nothing Then
                        serviceClient.ClientCredentials.Windows.ClientCredential =
                        CredentialCache.DefaultNetworkCredentials
                    Else
                        serviceClient.ClientCredentials.Windows.ClientCredential =
                            serverConfig.Credentials.Windows.ClientCredential
                    End If

                    ' Retrieve all deployed instances of Microsoft Dynamics CRM.
                    Dim organizations = serviceClient.RetrieveAll(
                        DeploymentEntityType.Organization)

                    ' Print list of all retrieved organizations.
                    Console.WriteLine("Organizations in your deployment")
                    Console.WriteLine("================================")
                    For Each organization In organizations
                        Console.WriteLine(organization.Name)
                    Next organization
                    Console.WriteLine("<End of Listing>")
                    Console.WriteLine()


                    ' Retrieve details of first organization from previous call.
                    Dim deployment As Microsoft.Xrm.Sdk.Deployment.Organization =
                        CType(serviceClient.Retrieve(DeploymentEntityType.Organization,
                                                     organizations(0)), 
                                                 Microsoft.Xrm.Sdk.Deployment.Organization)

                    ' Print out retrieved details about your organization.
                    Console.WriteLine(String.Format("Selected deployment details for {0}",
                                                    serverConfig.OrganizationName))
                    Console.WriteLine("=========================================")
                    Console.Write("Friendly Name: ")
                    Console.WriteLine(deployment.FriendlyName)
                    Console.Write("Unique Name: ")
                    Console.WriteLine(deployment.UniqueName)
                    Console.Write("Organization Version: ")
                    Console.WriteLine(deployment.Version)
                    Console.Write("SQL Server Name: ")
                    Console.WriteLine(deployment.SqlServerName)
                    Console.Write("SRS URL: ")
                    Console.WriteLine(deployment.SrsUrl)
                    Console.WriteLine("<End of Listing>")
                    Console.WriteLine()

                    ' Retrieve license and user information for your organization.
                    Dim licenseRequest As New TrackLicenseRequest()
                    Dim licenseResponse As TrackLicenseResponse =
                        CType(serviceClient.Execute(licenseRequest), 
                            TrackLicenseResponse)

                    ' Print out the number of servers and the user list.
                    Console.WriteLine(String.Format("License and user information for {0}",
                                                    serverConfig.OrganizationName))
                    Console.WriteLine("=========================================")
                    Console.Write("Number of servers: ")

                    Console.WriteLine(If(licenseResponse.Servers IsNot Nothing,
                                         licenseResponse.Servers.Count.ToString(),
                                         "null"))
                    Console.WriteLine("Users:")
                    For Each user As OrganizationUserInfo In licenseResponse.Users.ToList()
                        Console.WriteLine(user.FullName)
                    Next user
                    Console.WriteLine("<End of Listing>")
                    Console.WriteLine()

                    ' Retrieve advanced settings for your organization.
                    Dim request As RetrieveAdvancedSettingsRequest =
                        New RetrieveAdvancedSettingsRequest With
                        {
                            .ConfigurationEntityName = "Server",
                            .ColumnSet = New ColumnSet(New String() {"Id", "FullName",
                                                "Name", "Roles", "State", "Version"})
                        }
                    Dim configuration As ConfigurationEntity =
                        (CType(serviceClient.Execute(request), 
                         RetrieveAdvancedSettingsResponse)).Entity

                    ' Print out all advanced settings where IsWritable==true.
                    Console.WriteLine("Advanced deployment settings that can be updated")
                    Console.WriteLine("================================================")
                    For Each setting In configuration.Attributes
                        If setting.Key <> "Id" Then
                            Console.WriteLine(String.Format("{0}: {1}", setting.Key, setting.Value))
                        End If
                    Next setting
                    Console.WriteLine("<End of Listing>")
                    Console.WriteLine()

                End Using
                '</snippetUseDeploymentServiceMessages1>

                ' Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            Catch fe As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
                ' You can handle an exception here or pass it back to the calling method.
                Throw
            End Try
        End Sub

#End Region ' How To Sample Code

#Region "Main method"

        ''' <summary>
        ''' Standard Main() method used by most SDK samples.
        ''' </summary>
        ''' <param name="args"></param>
        Public Shared Sub Main(ByVal args() As String)
            Try
                ' Obtain the target organization's Web address and client logon 
                ' credentials from the user.
                Dim serverConnect As New ServerConnection()
                Dim config As ServerConnection.Configuration =
                    serverConnect.GetServerConfiguration()

                Dim app As New UseDeploymentServiceMessages()
                app.Run(config, True)
            Catch ex As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
                Console.WriteLine("The application terminated with an error.")
                Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp)
                Console.WriteLine("Code: {0}", ex.Detail.ErrorCode)
                Console.WriteLine("Message: {0}", ex.Detail.Message)
                Console.WriteLine("Plugin Trace: {0}", ex.Detail.TraceText)
                Console.WriteLine("Inner Fault: {0}",
                                  If(Nothing Is ex.Detail.InnerFault, "No Inner Fault", "Has Inner Fault"))
            Catch ex As TimeoutException
                Console.WriteLine("The application terminated with an error.")
                Console.WriteLine("Message: {0}", ex.Message)
                Console.WriteLine("Stack Trace: {0}", ex.StackTrace)
                Console.WriteLine("Inner Fault: {0}",
                                  If(Nothing Is ex.InnerException.Message, "No Inner Fault", ex.InnerException.Message))
            Catch ex As Exception
                Console.WriteLine("The application terminated with an error.")
                Console.WriteLine(ex.Message)

                ' Display the details of the inner exception.
                If ex.InnerException IsNot Nothing Then
                    Console.WriteLine(ex.InnerException.Message)

                    Dim fe As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault) =
                        TryCast(ex.InnerException, 
                            FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault))
                    If fe IsNot Nothing Then
                        Console.WriteLine("Timestamp: {0}", fe.Detail.Timestamp)
                        Console.WriteLine("Code: {0}", fe.Detail.ErrorCode)
                        Console.WriteLine("Message: {0}", fe.Detail.Message)
                        Console.WriteLine("Plugin Trace: {0}", fe.Detail.TraceText)
                        Console.WriteLine("Inner Fault: {0}",
                                          If(Nothing Is fe.Detail.InnerFault, "No Inner Fault", "Has Inner Fault"))
                    End If
                End If
                ' Additional exceptions to catch: SecurityTokenValidationException, ExpiredSecurityTokenException,
                ' SecurityAccessDeniedException, MessageSecurityException, and SecurityNegotiationException.

            Finally
                Console.WriteLine("Press <Enter> to exit.")
                Console.ReadLine()
            End Try
        End Sub
#End Region ' Main method      

    End Class
End Namespace
'</snippetUseDeploymentServiceMessages>