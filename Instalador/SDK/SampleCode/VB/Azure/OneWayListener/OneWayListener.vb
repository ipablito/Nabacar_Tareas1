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

'<snippetOneWayListener>
Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Text

Imports Microsoft.Xrm.Sdk
Imports Microsoft.ServiceBus

Namespace Microsoft.Crm.Sdk.Samples
	<ServiceBehavior> _
	Friend Class RemoteService
		Implements IServiceEndpointPlugin
		#Region "IServiceEndpointPlugin Member"

        Public Sub Execute(ByVal context As RemoteExecutionContext) Implements IServiceEndpointPlugin.Execute
            Utility.Print(context)
        End Sub

#End Region
    End Class

	Friend Class Program
		Shared Sub Main(ByVal args() As String)
			ServiceBusEnvironment.SystemConnectivity.Mode = ConnectivityMode.Http

			Console.Write("Your Service Namespace: ")
			Dim serviceNamespace As String = Console.ReadLine()
			Console.Write("Your Issuer Name: ")
			Dim issuerName As String = Console.ReadLine()

			' The issuer secret is the Service Bus namespace management key.
			Console.Write("Your Issuer Secret: ")
			Dim issuerSecret As String = Console.ReadLine()

			' Create the service URI based on the service namespace.
			Dim address As Uri = ServiceBusEnvironment.CreateServiceUri(Uri.UriSchemeHttps, serviceNamespace, "RemoteService")
			Console.WriteLine("Service address: " & address.ToString())

			' Create the credentials object for the endpoint.
            Dim sharedSecretServiceBusCredential As New TransportClientEndpointBehavior() With
                {
                    .TokenProvider = TokenProvider.CreateSharedSecretTokenProvider(issuerName, issuerSecret)
                }

			' Create the binding object.
			Dim binding As New WS2007HttpRelayBinding()
			binding.Security.Mode = EndToEndSecurityMode.Transport

			' Create the service host reading the configuration.
			Dim host As New ServiceHost(GetType(RemoteService))
			host.AddServiceEndpoint(GetType(IServiceEndpointPlugin), binding, address)

			' Create the ServiceRegistrySettings behavior for the endpoint.
			Dim serviceRegistrySettings As IEndpointBehavior = New ServiceRegistrySettings(DiscoveryType.Public)

			' Add the Service Bus credentials to all endpoints specified in configuration.
			For Each endpoint As ServiceEndpoint In host.Description.Endpoints
				endpoint.Behaviors.Add(serviceRegistrySettings)
				endpoint.Behaviors.Add(sharedSecretServiceBusCredential)
			Next endpoint

			' Open the service.
			host.Open()

			Console.WriteLine("Press [Enter] to exit")
			Console.ReadLine()

			' Close the service.
			Console.Write("Closing the service host...")
			host.Close()
			Console.WriteLine(" done.")
		End Sub
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
'</snippetOneWayListener>
