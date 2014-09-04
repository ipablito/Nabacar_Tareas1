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
Imports System.Globalization
Imports System.ServiceModel
Imports System.ServiceModel.Description

Imports CrmSdk.Discovery
Imports CrmSdk


Namespace Microsoft.Crm.Sdk.Samples

	Friend NotInheritable Class Program
		#Region "Constants"
		''' <summary>
		''' User Name
		''' </summary>
		Private Const UserName As String = "user@domain"

		''' <summary>
		''' Password
		''' </summary>
		Private Const UserPassword As String = "password"

		''' <summary>
		''' Unique Name of the organization
		''' </summary>
		Private Const OrganizationUniqueName As String = "orgname"

		''' <summary>
		''' URL for the Discovery Service
		''' </summary>
		Private Const DiscoveryServiceUrl As String = "https://dev.contoso.com:555/XRMServices/2011/Discovery.svc"
		#End Region

		Private Sub New()
		End Sub
		Shared Sub Main(ByVal args() As String)
			'Generate the credentials
			Dim credentials As New ClientCredentials()
			credentials.UserName.UserName = UserName
			credentials.UserName.Password = UserPassword

			'Execute the sample
			Dim serviceUrl As String = DiscoverOrganizationUrl(credentials, OrganizationUniqueName, DiscoveryServiceUrl)
			ExecuteWhoAmI(credentials, serviceUrl)
		End Sub

		Private Shared Sub ApplyCredentials(Of TChannel As Class)(ByVal client As ClientBase(Of TChannel), ByVal credentials As ClientCredentials)
			client.ClientCredentials.UserName.UserName = credentials.UserName.UserName
			client.ClientCredentials.UserName.Password = credentials.UserName.Password
		End Sub

		Private Shared Function DiscoverOrganizationUrl(ByVal credentials As ClientCredentials, ByVal organizationName As String, ByVal discoveryServiceUrl As String) As String
			Using client As New DiscoveryServiceClient("CustomBinding_IDiscoveryService", discoveryServiceUrl)
				ApplyCredentials(client, credentials)

				Dim request As New RetrieveOrganizationRequest() With {.UniqueName = organizationName}
				Dim response As RetrieveOrganizationResponse = CType(client.Execute(request), RetrieveOrganizationResponse)
				For Each endpoint As KeyValuePair(Of EndpointType, String) In response.Detail.Endpoints
                    If EndpointType.OrganizationService.Equals(endpoint.Key) Then
                        Console.WriteLine("Organization Service URL: {0}", endpoint.Value)
                        Return endpoint.Value
                    End If
				Next endpoint

				Throw New InvalidOperationException(String.Format(CultureInfo.InvariantCulture, "Organization {0} does not have an OrganizationService endpoint defined.", organizationName))
			End Using
		End Function

		Private Shared Sub ExecuteWhoAmI(ByVal credentials As ClientCredentials, ByVal serviceUrl As String)
			Using client As New OrganizationServiceClient("CustomBinding_IOrganizationService", New EndpointAddress(serviceUrl))
				ApplyCredentials(client, credentials)

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