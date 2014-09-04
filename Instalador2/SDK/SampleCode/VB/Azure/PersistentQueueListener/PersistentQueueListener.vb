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

'<snippetPersistentQueueListener>
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.Text

Imports Microsoft.ServiceBus
Imports Microsoft.ServiceBus.Messaging
Imports Microsoft.Xrm.Sdk

Namespace Microsoft.Crm.Sdk.Samples
	Friend Class Program
		Public Shared Sub Main(ByVal args() As String)
			Dim consumer_Renamed As New Consumer()

			consumer_Renamed.CreateQueueClient()
			consumer_Renamed.ProcessMessages()
			consumer_Renamed.DeleteQueue()
		End Sub
	End Class

	Friend Class Consumer
		Private Const MyQueuePath As String = "MyQueue"
		Private queueDescription_Renamed As QueueDescription
		Private queueClient_Renamed As QueueClient
		Private namespaceUri As Uri
		Private credential As TransportClientEndpointBehavior

		Public Sub New()
			Console.Write("Your Service Namespace: ")
			Dim serviceNamespace As String = Console.ReadLine()
			Console.Write("Your Issuer Name: ")
			Dim issuerName As String = Console.ReadLine()
			Console.Write("Your Issuer Secret: ")
			Dim issuerSecret As String = Console.ReadLine()

			' Configure queue settings.
			Me.queueDescription_Renamed = New QueueDescription(MyQueuePath)
			' Setting Max Size and TTL for demonstration purpose
			' but can be changed per user discretion to suite their system needs.
			' Refer service bus documentation to understand the limitations.
			' Setting Queue max size to 1GB where as default Max Size is 5GB.
			Me.queueDescription_Renamed.MaxSizeInMegabytes = 1024
			' Setting message TTL to 5 days where as default TTL is 14 days.
			Me.queueDescription_Renamed.DefaultMessageTimeToLive = TimeSpan.FromDays(5)

			' Create management credentials.
			Me.credential = New TransportClientEndpointBehavior() With {.TokenProvider = TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret)}

			' Create the URI for the queue.
			Me.namespaceUri = ServiceBusEnvironment.CreateServiceUri("sb", serviceNamespace, String.Empty)
			Console.WriteLine("Service Bus Namespace Uri address '{0}'", Me.namespaceUri.AbsoluteUri)
		End Sub

		Public Sub CreateQueueClient()
			' get the existing queue or create a new queue if it doesn't exist.
			Me.queueClient_Renamed = GetOrCreateQueue(namespaceUri, Me.credential.TokenProvider)
		End Sub

		Public Sub ProcessMessages()
			' Get receive mode (PeekLock or ReceiveAndDelete) from queueClient.
			Dim mode As ReceiveMode = Me.queueClient_Renamed.Mode
			Do
				Console.Write("Press [Enter] to retrieve a message from the queue (type quit to exit): ")
				Dim line As String = Console.ReadLine()

				If (Not String.IsNullOrEmpty(line)) AndAlso String.Equals(line, "quit", StringComparison.OrdinalIgnoreCase) Then
					Exit Do
				End If

				' Retrieve a message from the queue.
				Console.WriteLine("Waiting for a message from the queue... ")
				Dim message As BrokeredMessage
				Try
					message = Me.queueClient_Renamed.Receive()
					' Check if the message received.
					If message IsNot Nothing Then
						Try
							' Verify EntityLogicalName and RequestName message properties 
							' to only process specific message sent from Microsoft Dynamics CRM. 
							Dim keyRoot As String = "http://schemas.microsoft.com/xrm/2011/Claims/"
							Dim entityLogicalNameKey As String = "EntityLogicalName"
							Dim requestNameKey As String = "RequestName"
							Dim entityLogicalNameValue As Object
							Dim requestNameValue As Object
							message.Properties.TryGetValue(keyRoot & entityLogicalNameKey, entityLogicalNameValue)
							message.Properties.TryGetValue(keyRoot & requestNameKey, requestNameValue)

							' Filter message with specific message properties. i.e. EntityLogicalName=letter and RequestName=Create
							If entityLogicalNameValue IsNot Nothing AndAlso requestNameValue IsNot Nothing Then
								If entityLogicalNameValue.ToString() = "letter" AndAlso requestNameValue.ToString() = "Create" Then
									Console.WriteLine("--------------------------------")
									Console.WriteLine(String.Format("Message received: Id = {0}", message.MessageId))
									' Display message properties that are set on the brokered message.
									Utility.PrintMessageProperties(message.Properties)
									' Display body details.
									Utility.Print(message.GetBody(Of RemoteExecutionContext)())
									Console.WriteLine("--------------------------------")
								Else
									Continue Do
								End If
							Else
								Continue Do
							End If
							' If receive mode is PeekLock then set message complete to remove message from queue.
                            If mode = ReceiveMode.PeekLock Then
                                message.Complete()
                            End If
						Catch ex As Exception
							' Indicate a problem, unlock message in queue.
                            If mode = ReceiveMode.PeekLock Then
                                message.Abandon()
                            End If
							Console.WriteLine(ex.Message)
							Continue Do
						End Try
					Else
						Exit Do
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

		Public Sub DeleteQueue()
			DeleteQueueInternal(namespaceUri, MyQueuePath, credential.TokenProvider)
		End Sub

		Private Sub DeleteQueueInternal(ByVal address As Uri, ByVal path As String, ByVal tokenProvider_Renamed As TokenProvider)
			Dim settings = New NamespaceManagerSettings() With {.TokenProvider = tokenProvider_Renamed}
			Dim namespaceClient = New ServiceBus.NamespaceManager(address, settings)
			Try
				namespaceClient.DeleteQueue(path)
				Console.WriteLine("Queue deleted successfully.")
			Catch e As FaultException
				Console.WriteLine("Exception when deleting queue.. {0}", e)
			End Try
		End Sub

		Private Function GetOrCreateQueue(ByVal namespaceUri As Uri, ByVal tokenProvider_Renamed As TokenProvider) As QueueClient
			Dim namespaceClient As New NamespaceManager(namespaceUri, tokenProvider_Renamed)

			' Create queue if not already exist.
			If Not namespaceClient.QueueExists(MyQueuePath) Then
				namespaceClient.CreateQueue(Me.queueDescription_Renamed)
				Console.WriteLine("Queue created.")
			Else
				Console.WriteLine("Queue already exists.")
			End If

			Dim factory As MessagingFactory = MessagingFactory.Create(namespaceUri, tokenProvider_Renamed)
			Console.WriteLine("Creating queue client...")
			Return factory.CreateQueueClient(MyQueuePath, ReceiveMode.PeekLock)
		End Function
	End Class

	Friend NotInheritable Class Utility
		Private Sub New()
		End Sub
		Public Shared Sub Print(ByVal context As RemoteExecutionContext)
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

		Friend Shared Sub PrintMessageProperties(ByVal iDictionary_Renamed As IDictionary(Of String, Object))
			If iDictionary_Renamed.Count = 0 Then
				Console.WriteLine("No Message properties found.")
				Return
			End If
			For Each item In iDictionary_Renamed
				Dim key As String = If(item.Key IsNot Nothing, item.Key.ToString(), "")
				Dim value As String = If(item.Value IsNot Nothing, item.Value.ToString(), "")
				Console.WriteLine(key & " " & value)
			Next item
		End Sub
	End Class
End Namespace
'</snippetPersistentQueueListener>

