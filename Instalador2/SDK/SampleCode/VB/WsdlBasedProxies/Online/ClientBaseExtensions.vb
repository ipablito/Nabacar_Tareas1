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
Imports System.IdentityModel.Tokens
Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Security

Imports Microsoft.IdentityModel.Protocols.WSTrust


Public Module ClientBaseExtensions
	<System.Runtime.CompilerServices.Extension> _
	Public Function ConfigureCrmOnlineBinding(Of TChannel As Class)(ByVal client As ClientBase(Of TChannel), ByVal issuerUri As Uri) As Binding
		If Nothing Is client Then
			Throw New ArgumentNullException("client")
		End If

		'When this is represented in the configuration file, it attempts to show the CardSpace dialog.
		'As a workaround, the binding is being setup manually using code.
		Dim securityElement As New TransportSecurityBindingElement()
		securityElement.DefaultAlgorithmSuite = SecurityAlgorithmSuite.TripleDes
		securityElement.MessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10
		securityElement.EndpointSupportingTokenParameters.Signed.Add(New System.ServiceModel.Security.Tokens.IssuedSecurityTokenParameters() With {.RequireDerivedKeys = False, .KeySize = 192, .IssuerAddress = New EndpointAddress(issuerUri)})

		'Create a new list of the binding elements
		Dim elementList As New List(Of BindingElement)()
		elementList.Add(securityElement)
		elementList.AddRange(client.Endpoint.Binding.CreateBindingElements())

		Dim binding As Binding = New CustomBinding(elementList)
		client.ChannelFactory.Endpoint.Binding = binding

		'Configure the channel factory for use with federation
		client.ChannelFactory.ConfigureChannelFactory()
		Return client.Endpoint.Binding
	End Function

	<System.Runtime.CompilerServices.Extension> _
	Public Function CreateChannel(Of TChannel As Class)(ByVal client As ClientBase(Of TChannel), ByVal token As SecurityToken) As TChannel
		If Nothing Is client Then
			Throw New ArgumentNullException("client")
		End If

		If Nothing Is token Then
			Return client.ChannelFactory.CreateChannel()
		End If

		SyncLock client.ChannelFactory
			Return client.ChannelFactory.CreateChannelWithIssuedToken(token)
		End SyncLock
	End Function
End Module

#Region "Overrides for the clients"
Namespace CrmSdk.Discovery
    Partial Class DiscoveryServiceClient
#Region "Properties"
        Public Property Token() As SecurityToken
#End Region

        Protected Overrides Function CreateChannel() As IDiscoveryService
            Return Me.CreateChannel(Me.Token)
        End Function
    End Class
End Namespace

Namespace CrmSdk
    Partial Class OrganizationServiceClient
#Region "Properties"
        Public Property Token() As SecurityToken
#End Region

        Protected Overrides Function CreateChannel() As IOrganizationService
            Return Me.CreateChannel(Me.Token)
        End Function
    End Class
End Namespace
#End Region