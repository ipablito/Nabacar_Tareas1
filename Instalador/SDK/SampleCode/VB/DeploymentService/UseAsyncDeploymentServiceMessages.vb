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

'<snippetUseAsyncDeploymentServiceMessages>
Imports Microsoft.VisualBasic
Imports System
Imports System.ServiceModel
Imports System.Threading

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk.Client
Imports Microsoft.Xrm.Sdk.Deployment
Imports Microsoft.Xrm.Sdk.Deployment.Proxy
Imports System.Net

Namespace Microsoft.Crm.Sdk.Samples
    ''' <summary>
    ''' This sample demonstrates how to use the Deployment Web Service to create an 
    ''' organization and poll the status of the job.
    ''' </summary>
    ''' <remarks>
    ''' At run-time, you will be given the option to delete all the
    ''' database records created by this program.
    ''' </remarks>
    Public Class UseAsyncDeploymentServiceMessages
#Region "Class Level Members"

        Private _serviceProxy As OrganizationServiceProxy

        ' Friendly Name for the organization database
        Private _friendlyName As String = "Alpine1"

        ' Unique Name for the organization
        Private _uniqueName As String = "Alpine1"

        ' Name of the SQL server on which the organization database is installed
        Private _sqlServerName As String = "sqlServerName"

        ' URL of the Microsoft SQL Server on which the Microsoft Dynamics CRM Connector 
        ' for SQL Server Reporting Services is installed:
        ' Format: "http://reportServerName/reportserver"
        Private _srsUrl As String = "http://reportServerName/reportserver"

        ' Name of the system administrator for the new organization
        ' Format: "domain\\user"
        Private _sysAdminName As String = "domain\user"

        Private _organizationID As Guid
        Private client As DeploymentServiceClient

#End Region ' Class Level Members

#Region "How To Sample Code"
        ''' <summary>
        ''' Demonstrates how to use the Deployment Web Service to create an organization 
        ''' and poll the status of the job.
        ''' </summary>
        ''' <param name="serverConfig">Contains server connection information.</param>
        ''' <param name="promptforDelete">When True, the user will be prompted to delete 
        ''' all created entities.</param>

        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                       ByVal promptforDelete As Boolean)
            Try
                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy will be properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    ' This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes()

                    CreateRequiredRecords(serverConfig, promptforDelete)

                    '<snippetUseAsyncDeploymentServiceMessages1>
                    ' Instantiate DeploymentServiceClient for calling the service.
                    client = ProxyClientHelper.CreateClient(
                        New Uri(serverConfig.DiscoveryUri.ToString() _
                                .Replace("Services", "Deployment") _
                                .Replace("Discovery", "Deployment")))

                    ' Setting credentials from the current security context. 
                    If serverConfig.Credentials Is Nothing Then
                        client.ClientCredentials.Windows.ClientCredential =
                        CredentialCache.DefaultNetworkCredentials
                    Else
                        client.ClientCredentials.Windows.ClientCredential =
                            serverConfig.Credentials.Windows.ClientCredential
                    End If

                    Using client
                        ' Set properties for the new organization
                        Dim organization As Microsoft.Xrm.Sdk.Deployment.Organization =
                            New Microsoft.Xrm.Sdk.Deployment.Organization With
                            {
                                .BaseCurrencyCode = "USD",
                                .BaseCurrencyName = "US Dollar",
                                .BaseCurrencyPrecision = 2,
                                .BaseCurrencySymbol = "$",
                                .BaseLanguageCode = 1033,
                                .FriendlyName = _friendlyName,
                                .UniqueName = _uniqueName,
                                .SqlCollation = "Latin1_General_CI_AI",
                                .SqlServerName = _sqlServerName,
                                .SrsUrl = _srsUrl,
                                .SqmIsEnabled = False
                            }

                        ' Create a request for the deployment web service
                        ' CRM server app pool must have permissions on SQL server
                        Dim request As BeginCreateOrganizationRequest =
                            New BeginCreateOrganizationRequest With
                            {
                                .Organization = organization,
                                .SysAdminName = _sysAdminName
                            }

                        ' Execute the request
                        Dim response As BeginCreateOrganizationResponse =
                            CType(client.Execute(request), 
                                BeginCreateOrganizationResponse)

                        ' The operation is asynchronous, so the response object contains
                        ' a unique identifier for the operation
                        Dim operationId As Guid = response.OperationId

                        ' Retrieve the Operation using the OperationId
                        Dim retrieveOperationStatus As New RetrieveRequest()
                        retrieveOperationStatus.EntityType = DeploymentEntityType.DeferredOperationStatus
                        retrieveOperationStatus.InstanceTag = New EntityInstanceId With {.Id = operationId}

                        Dim retrieveResponse_Renamed As RetrieveResponse
                        Dim deferredOperationStatus_Renamed As DeferredOperationStatus

                        Console.WriteLine("Retrieving state of the job...")

                        ' Retrieve the Operation State until Organization is created
                        Do
                            ' Wait 3 secs to not overload server
                            Thread.Sleep(3000)

                            retrieveResponse_Renamed = CType(client.Execute(retrieveOperationStatus), 
                                RetrieveResponse)

                            deferredOperationStatus_Renamed = (CType(retrieveResponse_Renamed.Entity, 
                                                               DeferredOperationStatus))
                        Loop While deferredOperationStatus_Renamed.State <> DeferredOperationState.Processing AndAlso
                            deferredOperationStatus_Renamed.State <> DeferredOperationState.Completed

                        ' Poll OrganizationStatusRequest
                        Dim retrieveReqServer As New RetrieveRequest()
                        retrieveReqServer.EntityType = DeploymentEntityType.Organization
                        retrieveReqServer.InstanceTag = New EntityInstanceId()
                        retrieveReqServer.InstanceTag.Name = organization.UniqueName

                        Dim retrieveRespServer As RetrieveResponse
                        Dim orgState As OrganizationState

                        Console.WriteLine("Retrieving state of the organization...")

                        ' Retrieve and check the Organization State until is enabled
                        Do
                            retrieveRespServer = CType(client.Execute(retrieveReqServer), 
                                RetrieveResponse)
                            _organizationID = (CType(retrieveRespServer.Entity, 
                                               Microsoft.Xrm.Sdk.Deployment.Organization)).Id
                            orgState = (CType(retrieveRespServer.Entity, 
                                        Microsoft.Xrm.Sdk.Deployment.Organization)).State

                            ' Wait 5 secs to not overload server
                            Thread.Sleep(5000)
                        Loop While orgState <> OrganizationState.Enabled

                        Console.WriteLine("Organization has been created!")
                        '</snippetUseAsyncDeploymentServiceMessages1>

                        DeleteRequiredRecords(promptforDelete)

                    End Using
                End Using

                ' Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            Catch fe As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
                ' You can handle an exception here or pass it back to the calling method.
                Throw
            End Try
        End Sub

#Region "Public methods"

        ''' <summary>
        ''' Creates any entity records that this sample requires.
        ''' </summary>
        Public Sub CreateRequiredRecords(ByVal config As ServerConnection.Configuration,
                                         ByVal prompt As Boolean)
            ' For prompt and AD environment, pass current server details. 
            If (prompt) And config.EndpointType =
                AuthenticationProviderType.ActiveDirectory Then
                Me._sqlServerName = config.ServerAddress
                Me._sysAdminName = config.ServerAddress & "dom\administrator"
                Me._srsUrl = "http://" & config.ServerAddress & "/reportserver"
            End If
        End Sub

        ''' <summary>
        ''' Deletes any entity records that were created for this sample.
        ''' <param name="prompt">Indicates whether to prompt the user 
        ''' to delete the records created in this sample.</param>
        ''' </summary>
        Public Sub DeleteRequiredRecords(ByVal prompt As Boolean)
            Dim toBeDeleted As Boolean = True

            If prompt Then
                ' Ask the user if the created entities should be deleted.
                Console.Write(vbLf & "Do you want these entity records deleted? (y/n) [y]: ")
                Dim answer As String = Console.ReadLine()
                If answer.StartsWith("y") OrElse
                    answer.StartsWith("Y") OrElse
                    answer = String.Empty Then
                    toBeDeleted = True
                Else
                    toBeDeleted = False
                End If
            End If

            If toBeDeleted Then
                ' First disable the org
                Dim organizationCreated As New EntityInstanceId()
                organizationCreated.Id = _organizationID
                Dim organization As Microsoft.Xrm.Sdk.Deployment.Organization =
                    CType(client.Retrieve(DeploymentEntityType.Organization, organizationCreated), 
                        Microsoft.Xrm.Sdk.Deployment.Organization)

                ' Update status to disabled
                organization.State = OrganizationState.Disabled

                client.Update(organization)
                Console.WriteLine("Organization has been disabled.")

                ' Second delete it
                client.Delete(DeploymentEntityType.Organization, organizationCreated)
                Console.WriteLine("Organization has been deleted.")
            End If
        End Sub
#End Region ' Public Methods

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

                Dim app As New UseAsyncDeploymentServiceMessages()
                app.Run(config, True)
            Catch ex As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
                Console.WriteLine("The application terminated with an error.")
                Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp)
                Console.WriteLine("Code: {0}", ex.Detail.ErrorCode)
                Console.WriteLine("Message: {0}", ex.Detail.Message)
                Console.WriteLine("Plugin Trace: {0}", ex.Detail.TraceText)
                Console.WriteLine("Inner Fault: {0}",
                                  If(Nothing Is ex.Detail.InnerFault, "No Inner Fault", "Has Inner Fault"))
            Catch ex As System.TimeoutException
                Console.WriteLine("The application terminated with an error.")
                Console.WriteLine("Message: {0}", ex.Message)
                Console.WriteLine("Stack Trace: {0}", ex.StackTrace)
                Console.WriteLine("Inner Fault: {0}",
                                  If(Nothing Is ex.InnerException.Message, "No Inner Fault", ex.InnerException.Message))
            Catch ex As System.Exception
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
'</snippetUseAsyncDeploymentServiceMessages>