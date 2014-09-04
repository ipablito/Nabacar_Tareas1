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

'<snippetRetrieveAUser>
Imports System.ServiceModel
Imports System.ServiceModel.Description

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Query
Imports Microsoft.Xrm.Sdk.Client
Imports Microsoft.Crm.Sdk.Messages

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' Demonstrates how to create basic query to find a user.
	''' </summary>
	''' <remarks>
	''' At run-time, you will be given the option to revert the role 
	''' association created by this program.</remarks>
	Public Class RetrieveAUser
		#Region "Class Level Members"

		' Define the IDs needed for this sample.
		Private _domain As String
		Private _userName As String
		Private _firstName As String
		Private _lastName As String
		Private _serviceProxy As OrganizationServiceProxy

		#End Region ' Class Level Members

		#Region "How To Sample Code"
		''' <summary>
        ''' This method first connects to the Organization service. Afterwards, it
        ''' creates/retrieves a system user,
        ''' retrieves user details, and 
        ''' creates a query to find the system user using domain\username or first and last name details. 
		''' Note: Creating a user is only supported
        ''' in an on-premises/active directory environment.
		''' </summary>
		''' <param name="serverConfig">Contains server connection information.</param>
		''' <param name="promptforDelete">When True, the user will be prompted to delete all
		''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                       ByVal promptforDelete As Boolean)
            Try
                '<snippetRetrieveAUser1>
                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy is properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    _serviceProxy.EnableProxyTypes()

                    CreateRequiredRecords()

                    ' Find a user using domain\username or first and last name details.
                    Dim userQuery As QueryExpression =
                        New QueryExpression With
                        {
                            .EntityName = "systemuser",
                            .ColumnSet = New ColumnSet(New String() {"systemuserid", "domainname", "fullname"})
                        }
                    userQuery.Criteria.FilterOperator = LogicalOperator.Or
                    userQuery.Criteria.Filters.Add(New FilterExpression(LogicalOperator.And))
                    userQuery.Criteria.Filters.Item(0).AddCondition(
                        New ConditionExpression("domainname", ConditionOperator.Equal, _domain & _userName))
                    userQuery.Criteria.Filters.Add(New FilterExpression(LogicalOperator.And))
                    userQuery.Criteria.Filters.Item(1).AddCondition(
                        New ConditionExpression("firstname", ConditionOperator.Equal, _firstName))
                    userQuery.Criteria.Filters.Item(1).AddCondition(
                         New ConditionExpression("lastname", ConditionOperator.Equal, _lastName))

                    ' Retrieve all columns.
                    Dim entities As EntityCollection = _serviceProxy.RetrieveMultiple(userQuery)

                    If entities.Entities.Count > 0 Then
                        Dim user As SystemUser = entities(0).ToEntity(Of SystemUser)()

                        ' Write out some key user properties.
                        Console.WriteLine("Id: {0}", user.Id)
                        Console.WriteLine("DomainName: {0}", user.DomainName)
                        Console.WriteLine("FullName: {0}", user.FullName)
                    End If
                End Using
                '</snippetRetrieveAUser1>
                ' Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            Catch fe As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
                ' You can handle an exception here or pass it back to the calling method.
                Throw
            End Try
        End Sub

		''' <summary>
		''' Creates any entity records that this sample requires.
		''' </summary>
		Public Sub CreateRequiredRecords()
			' For this sample, all required entities are created in the Run() method.
			' Create/Retrieve a user to get associated user details.
            Dim user As SystemUser =
                _serviceProxy.Retrieve(SystemUser.EntityLogicalName,
                                       SystemUserProvider.RetrieveAUserWithoutAnyRoleAssigned(_serviceProxy),
                                       New ColumnSet(New String() {"domainname",
                                                                   "firstname",
                                                                   "lastname"})).ToEntity(Of SystemUser)()


            ' Extract the domain, username, firstname and lastname from the user record.
			Dim userPath() As String = user.DomainName.Split(New Char() { "\"c })
			If userPath.Length > 1 Then
				_domain = userPath(0) & "\"
				_userName = userPath(1)
			Else
				_domain = String.Empty
				_userName = userPath(0)
			End If

			_firstName = user.FirstName
			_lastName = user.LastName
		End Sub

		#End Region ' How To Sample Code

		#Region "Main method"

		''' <summary>
		''' Standard Main() method used by most SDK samples.
		''' </summary>
		''' <param name="args"></param>
		Public Shared Sub Main(ByVal args() As String)
			Try
                ' Obtain the target organization's web address and client logon 
				' credentials from the user.
				Dim serverConnect As New ServerConnection()
                Dim config As ServerConnection.Configuration =
                    serverConnect.GetServerConfiguration()

				Dim app As New RetrieveAUser()
				app.Run(config, True)
			Catch ex As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp)
				Console.WriteLine("Code: {0}", ex.Detail.ErrorCode)
				Console.WriteLine("Message: {0}", ex.Detail.Message)
				Console.WriteLine("Trace: {0}", ex.Detail.TraceText)
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
						Console.WriteLine("Trace: {0}", fe.Detail.TraceText)
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
'</snippetRetrieveAUser>

