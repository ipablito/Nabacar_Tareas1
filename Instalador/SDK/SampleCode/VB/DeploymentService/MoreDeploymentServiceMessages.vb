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

'<snippetMoreDeploymentServiceMessages>
Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Net
Imports System.ServiceModel
Imports System.Linq

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk.Query
Imports Microsoft.Xrm.Sdk.Client

' These namespaces are found in the Microsoft.Xrm.Sdk.Deployment.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk.Deployment
Imports Microsoft.Xrm.Sdk.Deployment.Proxy

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' Demonstrates how to use the following DeploymentServiceMessages:
	''' RetrieveRequest, UpdateRequest and UpdateAdvancedSettingsRequest
	''' </summary>
	''' <remarks>
	''' NOTE: The deployment service only supports Active Directory, so the user running
	''' this sample must have a valid Active Directory account.
	''' This sample demonstrates its full functionality in a multi-server environment. 
	''' During execution it can disable one of the server for demonstration.
	''' </remarks>
	Public Class MoreDeploymentServiceMessages
		#Region "Class Level Members"

		Private _serviceProxy As OrganizationServiceProxy

		#End Region ' Class Level Members

		#Region "How To Sample Code"
		''' <summary>
		''' This method first connects to the Deployment service. Then,
		''' uses RetrieveRequest, UpdateRequest and UpdateAdvancedSettingsRequest.
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

                    ' Instantiate DeploymentServiceClient for calling the service.
                    Dim serviceClient As DeploymentServiceClient =
                        ProxyClientHelper.CreateClient(
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

                    '					#Region "RetrieveRequest"

                    ' Retrieve all servers available in the deployment
                    Console.WriteLine(vbLf & "Retrieving list of servers..." & vbLf)
                    Dim servers = serviceClient.RetrieveAll(DeploymentEntityType.Server)

                    ' Print list of all retrieved servers.
                    Console.WriteLine("Servers in your deployment")
                    Console.WriteLine("================================")
                    For Each server In servers
                        Console.WriteLine(server.Name)
                    Next server
                    Console.WriteLine("<End of Listing>")
                    Console.WriteLine()

                    ' Retrieve details of first (other than current server) or default server from previous call.
                    Dim serverId = servers.FirstOrDefault(
                        Function(x) x.Name.ToLowerInvariant() <> serverConfig.ServerAddress.ToLowerInvariant())
                    ' If no other server exists then default to existing one.
                    If serverId Is Nothing Then
                        serverId = servers.FirstOrDefault()
                    End If

                    Console.WriteLine(vbLf & "Retrieving details of one server..." & vbLf)
                    Dim retrieveReqServer As New RetrieveRequest()
                    retrieveReqServer.EntityType = DeploymentEntityType.Server
                    retrieveReqServer.InstanceTag = serverId
                    Dim retrieveRespServer As RetrieveResponse =
                        CType(serviceClient.Execute(retrieveReqServer), RetrieveResponse)
                    Dim serverToUpdate As Server = CType(retrieveRespServer.Entity, Server)

                    Console.WriteLine("================================")
                    Console.WriteLine("Name: " & serverToUpdate.Name)
                    Console.WriteLine("State: " & serverToUpdate.State)
                    Console.WriteLine()

                    '					#End Region ' RetrieveRequest

                    '					#Region "UpdateRequest"
                    ' Avoid updating current server as it would disrupt the further sample execution.
                    If servers.Count > 1 Then
                        ' Modified the property we want to update
                        serverToUpdate.State = ServerState.Disabled

                        ' Update the deployment record
                        Console.WriteLine(vbLf & "Updating server..." & vbLf)
                        Dim updateReq As New UpdateRequest()
                        updateReq.Entity = serverToUpdate
                        Dim uptRes As UpdateResponse =
                            CType(serviceClient.Execute(updateReq), UpdateResponse)

                        ' Retrieve server details again to check if it is updated
                        Dim retrieveRespServerUpdated As RetrieveResponse =
                            CType(serviceClient.Execute(retrieveReqServer), RetrieveResponse)
                        Dim serverUpdated As Server = CType(retrieveRespServerUpdated.Entity, Server)

                        Console.WriteLine("Server Updated")
                        Console.WriteLine("================================")
                        Console.WriteLine("Name: " & serverUpdated.Name)
                        Console.WriteLine("State: " & serverUpdated.State)
                        Console.WriteLine()

                        ' Revert change
                        serverUpdated.State = ServerState.Enabled

                        Console.WriteLine(vbLf & "Reverting change made in server..." & vbLf)
                        Dim updateReqRevert As New UpdateRequest()
                        updateReqRevert.Entity = serverUpdated
                        Dim uptResRev As UpdateResponse =
                            CType(serviceClient.Execute(updateReqRevert), UpdateResponse)

                        Dim retrieveRespServerReverted As RetrieveResponse =
                            CType(serviceClient.Execute(retrieveReqServer), RetrieveResponse)
                        Dim serverReverted As Server = CType(retrieveRespServerReverted.Entity, Server)

                        Console.WriteLine("Server Reverted")
                        Console.WriteLine("================================")
                        Console.WriteLine("Name: " & serverReverted.Name)
                        Console.WriteLine("State: " & serverReverted.State)
                        Console.WriteLine()
                    Else
                        Console.WriteLine(vbLf & "Multi-server environment missing." & vbLf _
                                          & "Skipping server update request to avoid disruption in the sample execution.")
                    End If
                    '					#End Region ' UpdateRequest

                    '					#Region "UpdateAdvanceRequest"

                    ' Retrieve Advanced Settings for your organization.
                    Console.WriteLine(vbLf & "Retrieving Advanced Settings..." & vbLf)
                    Dim requestAdvSettings As RetrieveAdvancedSettingsRequest =
                        New RetrieveAdvancedSettingsRequest With
                        {
                            .ConfigurationEntityName = "Deployment",
                            .ColumnSet = New ColumnSet("Id")
                        }
                    Dim configuration As ConfigurationEntity =
                        (CType(serviceClient.Execute(requestAdvSettings), RetrieveAdvancedSettingsResponse)).Entity

                    ' Print out all advanced settings where IsWritable==true.
                    Console.WriteLine("Advanced deployment settings that can be updated")
                    Console.WriteLine("================================================")
                    For Each setting In configuration.Attributes
                        If setting.Key <> "Id" Then
                            Console.WriteLine(String.Format("{0}: {1}", setting.Key, setting.Value))
                        End If
                    Next setting
                    Console.WriteLine()

                    ' Create the Configuration Entity with the values to update
                    Dim configEntity As New ConfigurationEntity()
                    configEntity.LogicalName = "Deployment"
                    configEntity.Attributes = New AttributeCollection()
                    configEntity.Attributes.Add(
                        New KeyValuePair(Of String, Object)("AutomaticallyInstallDatabaseUpdates", True))

                    ' Update Advanced Settings
                    Console.WriteLine(vbLf & "Updating Advanced Settings..." & vbLf)
                    Dim updateAdvanceReq As New UpdateAdvancedSettingsRequest()
                    updateAdvanceReq.Entity = configEntity
                    serviceClient.Execute(updateAdvanceReq)

                    ' Retrieve Advanced Settings to check if they have been updated
                    Dim configurationUpdated As ConfigurationEntity =
                        (CType(serviceClient.Execute(requestAdvSettings), RetrieveAdvancedSettingsResponse)).Entity

                    Console.WriteLine("Advanced deployment settings updated")
                    Console.WriteLine("================================================")
                    For Each setting In configurationUpdated.Attributes
                        If setting.Key <> "Id" Then
                            Console.WriteLine(String.Format("{0}: {1}", setting.Key, setting.Value))
                        End If
                    Next setting
                    Console.WriteLine()

                    ' Revert change
                    Dim entityRevert As New ConfigurationEntity()
                    entityRevert.LogicalName = "Deployment"
                    entityRevert.Attributes = New AttributeCollection()
                    entityRevert.Attributes.Add(
                        New KeyValuePair(Of String, Object)("AutomaticallyInstallDatabaseUpdates", False))

                    Console.WriteLine(vbLf & "Reverting Advanced Settings..." & vbLf)
                    Dim requestRevert As New UpdateAdvancedSettingsRequest()
                    requestRevert.Entity = entityRevert
                    serviceClient.Execute(requestRevert)

                    Dim configurationReverted As ConfigurationEntity =
                        (CType(serviceClient.Execute(requestAdvSettings), RetrieveAdvancedSettingsResponse)).Entity
                    Console.WriteLine("Advanced deployment settings reverted")
                    Console.WriteLine("================================================")
                    For Each setting In configurationReverted.Attributes
                        If setting.Key <> "Id" Then
                            Console.WriteLine(String.Format("{0}: {1}", setting.Key, setting.Value))
                        End If
                    Next setting
                    Console.WriteLine()
                    '					#End Region ' UpdateAdvanceRequest
                End Using
                ' Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            Catch fe As FaultException(Of DeploymentServiceFault)
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
				Dim config As ServerConnection.Configuration = serverConnect.GetServerConfiguration()

				Dim app As New MoreDeploymentServiceMessages()
				app.Run(config, True)
			Catch ex As FaultException(Of DeploymentServiceFault)
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp)
				Console.WriteLine("Code: {0}", ex.Detail.ErrorCode)
				Console.WriteLine("Message: {0}", ex.Detail.Message)
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

                    Dim fe As FaultException(Of DeploymentServiceFault) =
                        TryCast(ex.InnerException, 
                            FaultException(Of DeploymentServiceFault))
					If fe IsNot Nothing Then
						Console.WriteLine("Timestamp: {0}", fe.Detail.Timestamp)
						Console.WriteLine("Code: {0}", fe.Detail.ErrorCode)
						Console.WriteLine("Message: {0}", fe.Detail.Message)
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
'</snippetMoreDeploymentServiceMessages>