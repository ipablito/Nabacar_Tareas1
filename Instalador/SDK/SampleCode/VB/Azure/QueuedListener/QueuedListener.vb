' ==============================================================================
'  This file is part of the Microsoft Dynamics CRM SDK Code Samples.
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
' ==============================================================================

'<snippetQueuedListener>
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.Text

Imports Microsoft.ServiceBus
Imports Microsoft.Xrm.Sdk

Namespace Microsoft.Crm.Sdk.Samples
	Friend Class Program
		Public Shared Sub Main(ByVal args() As String)
			Dim consumer_Renamed As New Consumer()

			consumer_Renamed.CreateMessageBuffer()
			consumer_Renamed.ProcessMessages()
			consumer_Renamed.DeleteMessageBuffer()
		End Sub
	End Class

	Friend Class Consumer
		Private client As MessageBufferClient
		Private policy As MessageBufferPolicy
		Private credential As TransportClientEndpointBehavior
		Private uri_Renamed As Uri

		Public Sub New()
			Console.Write("Your Service Namespace: ")
			Dim serviceNamespace As String = Console.ReadLine()
			Console.Write("Your Issuer Name: ")
			Dim issuerName As String = Console.ReadLine()
			Console.Write("Your Issuer Secret: ")
			Dim issuerSecret As String = Console.ReadLine()

			' Create the policy for the message buffer.
			Me.policy = New MessageBufferPolicy()
			Me.policy.Authorization = AuthorizationPolicy.Required
			Me.policy.MaxMessageCount = 10
			' Messages in the message buffer expire after 5 minutes.
			Me.policy.ExpiresAfter = TimeSpan.FromMinutes(5)
			Me.policy.OverflowPolicy = OverflowPolicy.RejectIncomingMessage
			Me.policy.TransportProtection = TransportProtectionPolicy.AllPaths

			' Create the credentials object for the endpoint.
			Me.credential = New TransportClientEndpointBehavior()
			Me.credential.CredentialType = TransportClientCredentialType.SharedSecret
			Me.credential.Credentials.SharedSecret.IssuerName = issuerName
			Me.credential.Credentials.SharedSecret.IssuerSecret = issuerSecret

			' Create the URI for the message buffer.
			Me.uri_Renamed = ServiceBusEnvironment.CreateServiceUri(Uri.UriSchemeHttps, serviceNamespace, "MessageBuffer")
			Console.WriteLine("Message buffer address '{0}'", Me.uri_Renamed.AbsoluteUri)
		End Sub

		Public Sub CreateMessageBuffer()
			Console.Write("Press [Enter] to create the message buffer: ")
			Console.ReadLine()

			' Create the client for the message buffer.
			Me.client = GetOrCreateQueue(Me.credential, Me.uri_Renamed, Me.policy)
		End Sub

		Public Sub ProcessMessages()
			Do
				Console.Write("Press [Enter] to retrieve a message from the message buffer (type quit to exit): ")
				Dim line As String = Console.ReadLine()

				If (Not String.IsNullOrEmpty(line)) AndAlso String.Equals(line, "quit", StringComparison.OrdinalIgnoreCase) Then
					Exit Do
				End If

				Try
					' Retrieve a message from the message buffer.
					Console.WriteLine("Waiting fom a message from the message buffer... ")
					Dim retrievedMessage As Message = Me.client.PeekLock()

					' Check if the message was sent from Microsoft Dynamics CRM.
					If retrievedMessage.Headers.Action = "http://schemas.microsoft.com/xrm/2011/Contracts/IServiceEndpointPlugin/Execute" Then
						Utility.Print(retrievedMessage.GetBody(Of RemoteExecutionContext)())
						Me.client.DeleteLockedMessage(retrievedMessage)
					Else
						' The message did not originate from Microsoft Dynamics CRM.
						Me.client.ReleaseLock(retrievedMessage)
					End If
				Catch e As TimeoutException
					Console.WriteLine(e.Message)
					Continue Do
				Catch e As System.ServiceModel.FaultException
					Console.WriteLine(e.Message)
					Continue Do
				End Try
			Loop
		End Sub

		Public Sub DeleteMessageBuffer()
			' Delete the message buffer.
			Console.Write("Deleting Message buffer at {0} ...", Me.uri_Renamed.AbsoluteUri)
			Me.client.DeleteMessageBuffer()
			Console.WriteLine(" done.")
		End Sub

		Private Function GetOrCreateQueue(ByVal sharedSecredServiceBusCredential As TransportClientEndpointBehavior, ByVal queueUri As Uri, ByRef queuePolicy As MessageBufferPolicy) As MessageBufferClient
			Dim client As MessageBufferClient

			Try
				client = MessageBufferClient.GetMessageBuffer(sharedSecredServiceBusCredential, queueUri)
				queuePolicy = client.GetPolicy()
				Console.WriteLine("Message buffer already exists at '{0}'.", client.MessageBufferUri)

				Return client
			Catch e As FaultException
				' Not found. Ignore and make a new queue below. 
				' Other exceptions get bubbled up.
			End Try

			client = MessageBufferClient.CreateMessageBuffer(sharedSecredServiceBusCredential, queueUri, queuePolicy)
			queuePolicy = client.GetPolicy()
			Console.WriteLine("Message buffer created at '{0}'.", client.MessageBufferUri)
			Return client
		End Function
	End Class

	Friend NotInheritable Class Utility
		Private Sub New()
		End Sub
		Public Shared Sub Print(ByVal context As RemoteExecutionContext)
			Console.WriteLine("----------")
			If context Is Nothing Then
				Console.WriteLine("Context is null.")
				Return
			End If

			Console.WriteLine("UserId: {0}", context.UserId)
			Console.WriteLine("OrganizationId: {0}", context.OrganizationId)
			Console.WriteLine("OrganizationName: {0}", context.OrganizationName)
			Console.WriteLine("MessageName: {0}", context.MessageName)
			Console.WriteLine("Stage: {0}", context.Stage)
			Console.WriteLine("Mode: {0}", context.Mode)
			Console.WriteLine("PrimaryEntityName: {0}", context.PrimaryEntityName)
			Console.WriteLine("SecondaryEntityName: {0}", context.SecondaryEntityName)

			Console.WriteLine("BusinessUnitId: {0}", context.BusinessUnitId)
			Console.WriteLine("CorrelationId: {0}", context.CorrelationId)
			Console.WriteLine("Depth: {0}", context.Depth)
			Console.WriteLine("InitiatingUserId: {0}", context.InitiatingUserId)
			Console.WriteLine("IsExecutingOffline: {0}", context.IsExecutingOffline)
			Console.WriteLine("IsInTransaction: {0}", context.IsInTransaction)
			Console.WriteLine("IsolationMode: {0}", context.IsolationMode)
			Console.WriteLine("Mode: {0}", context.Mode)
			Console.WriteLine("OperationCreatedOn: {0}", context.OperationCreatedOn.ToString())
			Console.WriteLine("OperationId: {0}", context.OperationId)
			Console.WriteLine("PrimaryEntityId: {0}", context.PrimaryEntityId)
			Console.WriteLine("OwningExtension LogicalName: {0}", context.OwningExtension.LogicalName)
			Console.WriteLine("OwningExtension Name: {0}", context.OwningExtension.Name)
			Console.WriteLine("OwningExtension Id: {0}", context.OwningExtension.Id)
			Console.WriteLine("SharedVariables: {0}", (If(context.SharedVariables Is Nothing, "NULL", SerializeParameterCollection(context.SharedVariables))))
			Console.WriteLine("InputParameters: {0}", (If(context.InputParameters Is Nothing, "NULL", SerializeParameterCollection(context.InputParameters))))
			Console.WriteLine("OutputParameters: {0}", (If(context.OutputParameters Is Nothing, "NULL", SerializeParameterCollection(context.OutputParameters))))
			Console.WriteLine("PreEntityImages: {0}", (If(context.PreEntityImages Is Nothing, "NULL", SerializeEntityImageCollection(context.PreEntityImages))))
			Console.WriteLine("PostEntityImages: {0}", (If(context.PostEntityImages Is Nothing, "NULL", SerializeEntityImageCollection(context.PostEntityImages))))
			Console.WriteLine("----------")
		End Sub

		#Region "Private methods."
		Private Shared Function SerializeEntity(ByVal e As Entity) As String
			Dim sb As New StringBuilder()
			sb.Append(Environment.NewLine)
			sb.Append(" LogicalName: " & e.LogicalName)
			sb.Append(Environment.NewLine)
            sb.Append(" EntityId: " & e.Id.ToString())
			sb.Append(Environment.NewLine)
			sb.Append(" Attributes: [")
			For Each parameter As KeyValuePair(Of String, Object) In e.Attributes
                sb.Append(parameter.Key & ": " & parameter.Value.ToString() & "; ")
			Next parameter
			sb.Append("]")
			Return sb.ToString()
		End Function

		Private Shared Function SerializeParameterCollection(ByVal parameterCollection_Renamed As ParameterCollection) As String
			Dim sb As New StringBuilder()
			For Each parameter As KeyValuePair(Of String, Object) In parameterCollection_Renamed
				If parameter.Value IsNot Nothing AndAlso parameter.Value.GetType() Is GetType(Entity) Then
					Dim e As Entity = CType(parameter.Value, Entity)
					sb.Append(parameter.Key & ": " & SerializeEntity(e))
				Else
                    sb.Append(parameter.Key & ": " & parameter.Value.ToString() & "; ")
				End If
			Next parameter
			Return sb.ToString()
		End Function
		Private Shared Function SerializeEntityImageCollection(ByVal entityImageCollection_Renamed As EntityImageCollection) As String
			Dim sb As New StringBuilder()
			For Each entityImage As KeyValuePair(Of String, Entity) In entityImageCollection_Renamed
				sb.Append(Environment.NewLine)
				sb.Append(entityImage.Key & ": " & SerializeEntity(entityImage.Value))
			Next entityImage
			Return sb.ToString()
		End Function
		#End Region
	End Class
End Namespace
'</snippetQueuedListener>

