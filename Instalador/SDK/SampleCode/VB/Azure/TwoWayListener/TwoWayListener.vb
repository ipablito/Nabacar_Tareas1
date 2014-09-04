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

'<snippetTwoWayListener>
Imports System.ServiceModel
Imports System.Text

' This namespace is found in the Microsoft.Xrm.Sdk.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Xrm.Sdk

' This namespace is found in Microsoft.ServiceBus.dll assembly 
' found in the Windows Azure SDK
Imports Microsoft.ServiceBus

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' Creates a two-way endpoint listening for messages from the Windows Azure Service
	''' Bus.
	''' </summary>
	Public Class TwoWayListener
		#Region "How-To Sample Code"

		''' <summary>
		''' The Execute method is called when a message is posted to the Azure Service
		''' Bus.
		''' </summary>
		<ServiceBehavior> _
		Private Class TwoWayEndpoint
			Implements ITwoWayServiceEndpointPlugin
			#Region "ITwoWayServiceEndpointPlugin Member"

			''' <summary>
			''' This method is called when a message is posted to the Azure Service Bus.
			''' </summary>
			''' <param name="context">Data for the request.</param>
			''' <returns>A 'Success' string.</returns>
            Public Function Execute(ByVal context As RemoteExecutionContext) As String _
                Implements ITwoWayServiceEndpointPlugin.Execute
                Utility.Print(context)
                Return "Success"
            End Function

#End Region
        End Class

		''' <summary>
		''' Prompts for required information and hosts a service until the user ends the 
		''' session.
		''' </summary>
		Public Sub Run()
			'<snippetTwoWayListener1>
			ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http

			Console.Write("Enter your Azure service namespace: ")
			Dim serviceNamespace As String = Console.ReadLine()

			' The service namespace issuer name to use.  If one hasn't been setup
			' explicitly it will be the default issuer name listed on the service
			' namespace.
			Console.Write("Enter your service namespace issuer name: ")
			Dim issuerName As String = Console.ReadLine()

			' Issuer secret is the Windows Azure Service Bus namespace current management key.
			Console.Write("Enter your service namespace issuer key: ")
			Dim issuerKey As String = Console.ReadLine()

			' Input the same path that was specified in the Service Bus Configuration dialog
			' when registering the Azure-aware plug-in with the Plug-in Registration tool.
			Console.Write("Enter your endpoint path: ")
			Dim servicePath As String = Console.ReadLine()

			' Leverage the Azure API to create the correct URI.
			Dim address As Uri = ServiceBusEnvironment.CreateServiceUri(Uri.UriSchemeHttps, serviceNamespace, servicePath)

			Console.WriteLine("The service address is: " & address.ToString())

			' Create the shared secret credentials object for the endpoint matching the 
			' Azure access control services issuer 
            Dim sharedSecretServiceBusCredential = New TransportClientEndpointBehavior() With
                                                   {
                                                       .TokenProvider = TokenProvider.CreateSharedSecretTokenProvider(issuerName,
                                                                                                                      issuerKey)
                                                   }

			' Using an HTTP binding instead of a SOAP binding for this endpoint.
			Dim binding As New WS2007HttpRelayBinding()
			binding.Security.Mode = EndToEndSecurityMode.Transport

			' Create the service host for Azure to post messages to.
			Dim host As New ServiceHost(GetType(TwoWayEndpoint))
			host.AddServiceEndpoint(GetType(ITwoWayServiceEndpointPlugin), binding, address)

			' Create the ServiceRegistrySettings behavior for the endpoint.
			Dim serviceRegistrySettings_Renamed = New ServiceRegistrySettings(DiscoveryType.Public)

			' Add the service bus credentials to all endpoints specified in configuration.

			For Each endpoint In host.Description.Endpoints
				endpoint.Behaviors.Add(serviceRegistrySettings_Renamed)
				endpoint.Behaviors.Add(sharedSecretServiceBusCredential)
			Next endpoint

			' Begin listening for messages posted to Azure.
			host.Open()

            Console.WriteLine(Environment.NewLine _
                              & "Listening for messages from Azure" _
                              & Environment.NewLine & "Press [Enter] to exit")

			' Keep the listener open until Enter is pressed.
			Console.ReadLine()

			Console.Write("Closing the service host...")
			host.Close()
			Console.WriteLine(" done.")
			'</snippetTwoWayListener1>
		End Sub

		''' <summary>
		''' Containts methods to display the RemoteExecutionContext provided when a 
		''' message is posted to the Azure Service Bus.
		''' </summary>
		Private NotInheritable Class Utility
			''' <summary>
			''' Writes out the RemoteExecutionContext to the Console.
			''' </summary>
			''' <param name="context"></param>
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
                Console.WriteLine("SharedVariables: {0}",
                                  (If(context.SharedVariables Is Nothing,
                                      "NULL",
                                      SerializeParameterCollection(context.SharedVariables))))
                Console.WriteLine("InputParameters: {0}",
                                  (If(context.InputParameters Is Nothing,
                                      "NULL",
                                      SerializeParameterCollection(context.InputParameters))))
                Console.WriteLine("OutputParameters: {0}",
                                  (If(context.OutputParameters Is Nothing,
                                      "NULL",
                                      SerializeParameterCollection(context.OutputParameters))))
                Console.WriteLine("PreEntityImages: {0}",
                                  (If(context.PreEntityImages Is Nothing,
                                      "NULL",
                                      SerializeEntityImageCollection(context.PreEntityImages))))
                Console.WriteLine("PostEntityImages: {0}",
                                  (If(context.PostEntityImages Is Nothing,
                                      "NULL",
                                      SerializeEntityImageCollection(context.PostEntityImages))))
				Console.WriteLine("----------")
			End Sub

			''' <summary>
			''' Writes out the attributes of an entity.
			''' </summary>
			''' <param name="e">The entity to serialize.</param>
			''' <returns>A human readable representation of the entity.</returns>
			Private Shared Function SerializeEntity(ByVal e As Entity) As String
				Dim sb As New StringBuilder()
                sb.AppendFormat("{0} LogicalName: {1}{0} EntityId: {2}{0} Attributes: [",
                                Environment.NewLine, e.LogicalName, e.Id)
				For Each parameter As KeyValuePair(Of String, Object) In e.Attributes
					sb.AppendFormat("{0}: {1}; ", parameter.Key, parameter.Value)
				Next parameter
				sb.Append("]")
				Return sb.ToString()
			End Function

			''' <summary>
			''' Flattens a collection into a delimited string.
			''' </summary>
			''' <param name="parameterCollection_Renamed">The values must be of type Entity 
			''' to print the values.</param>
			''' <returns>A string representing the collection passed in.</returns>
			Private Shared Function SerializeParameterCollection(ByVal parameterCollection_Renamed As ParameterCollection) As String
				Dim sb As New StringBuilder()
				For Each parameter As KeyValuePair(Of String, Object) In parameterCollection_Renamed
					Dim e As Entity = TryCast(parameter.Value, Entity)
					If e IsNot Nothing Then
						sb.AppendFormat("{0}: {1}", parameter.Key, SerializeEntity(e))
					Else
						sb.AppendFormat("{0}: {1}; ", parameter.Key, parameter.Value)
					End If
				Next parameter
				Return sb.ToString()
			End Function

			''' <summary>
			''' Flattens a collection into a delimited string.
			''' </summary>
			''' <param name="entityImageCollection_Renamed">The collection to flatten.</param>
			''' <returns>A string representation of the collection.</returns>
			Private Shared Function SerializeEntityImageCollection(ByVal entityImageCollection_Renamed As EntityImageCollection) As String
				Dim sb As New StringBuilder()
				For Each entityImage As KeyValuePair(Of String, Entity) In entityImageCollection_Renamed
					sb.AppendFormat("{0}{1}: {2}", Environment.NewLine, entityImage.Key, SerializeEntity(entityImage.Value))
				Next entityImage
				Return sb.ToString()
			End Function
		End Class

		#End Region ' How-To Sample Code

		''' <summary>
		''' Standard Main() method used by most SDK samples.
		''' </summary>
		Public Shared Sub Main()
			Try
				Dim app As New TwoWayListener()
				app.Run()
			Catch ex As FaultException(Of ServiceEndpointFault)
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine("Message: {0}", ex.Detail.Message)
				Console.WriteLine("Inner Fault: {0}",If(Nothing Is ex.InnerException.Message, "No Inner Fault", "Has Inner Fault"))
			Catch ex As TimeoutException
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine("Message: {0}", ex.Message)
				Console.WriteLine("Stack Trace: {0}", ex.StackTrace)
				Console.WriteLine("Inner Fault: {0}",If(Nothing Is ex.InnerException.Message, "No Inner Fault", ex.InnerException.Message))
			Catch ex As Exception
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine(ex.Message)

				' Display the details of the inner exception.
				If ex.InnerException IsNot Nothing Then
					Console.WriteLine(ex.InnerException.Message)

					Dim fe As FaultException(Of ServiceEndpointFault) = TryCast(ex.InnerException, FaultException(Of ServiceEndpointFault))
					If fe IsNot Nothing Then
						Console.WriteLine("Message: {0}", fe.Detail.Message)
						Console.WriteLine("Inner Fault: {0}",If(Nothing Is ex.InnerException.Message, "No Inner Fault", "Has Inner Fault"))
					End If
				End If

			Finally
				Console.WriteLine("Press <Enter> to exit.")
				Console.ReadLine()
			End Try
		End Sub
	End Class
End Namespace
'</snippetTwoWayListener>