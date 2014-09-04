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

'<snippetRemoveRoleFromUser>
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
	''' Demonstrates how to do basic role association with the system user.
	''' </summary>
	''' <remarks>
	''' At run-time, you will be given the option to revert the role 
	''' association created by this program.</remarks>
	Public Class RemoveRoleFromUser
		#Region "Class Level Members"

		' Define the IDs needed for this sample.
		Private _userId As Guid
		Private _givenRole As String = "salesperson"
		Private _serviceProxy As OrganizationServiceProxy

		#End Region ' Class Level Members

		#Region "How To Sample Code"
		''' <summary>
        ''' This method first connects to the Organization service. Afterwards, it
        ''' creates/retrieves a system user, and
        ''' updates the system user to associate with the salesperson role. 
		''' Note: Creating a user is only supported
        ''' in an on-premises/active directory environment.
		''' </summary>
		''' <param name="serverConfig">Contains server connection information.</param>
        ''' <param name="promptforDelete">When True, the user is prompted to delete all
		''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                       ByVal promptforDelete As Boolean)
            Try
                '<snippetRemoveRoleFromUser1>
                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy is properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    _serviceProxy.EnableProxyTypes()

                    CreateRequiredRecords()

                    ' Retrieve a user.
                    Dim user As SystemUser =
                        _serviceProxy.Retrieve(
                            SystemUser.EntityLogicalName, _userId,
                            New ColumnSet(New String() {"systemuserid",
                                                        "firstname",
                                                        "lastname"})).ToEntity(Of SystemUser)()

                    If user IsNot Nothing Then
                        Console.WriteLine("{1} {0} user account is retrieved.",
                                          user.FirstName, user.LastName)
                        ' Find the role.
                        Dim query As QueryExpression =
                            New QueryExpression With
                            {
                                .EntityName = "role",
                                .ColumnSet = New ColumnSet("roleid")
                            }
                        query.Criteria = New FilterExpression()
                        query.Criteria.AddCondition(
                            New ConditionExpression("name", ConditionOperator.Equal, {_givenRole}))

                        ' Get the role.
                        Dim roles As EntityCollection = _serviceProxy.RetrieveMultiple(query)

                        ' Disassociate the role.
                        If roles.Entities.Count > 0 Then
                            Dim salesRole As Role = _serviceProxy.RetrieveMultiple(query) _
                                                    .Entities(0).ToEntity(Of Role)()

                            Console.WriteLine("Role {0} is retrieved.", _givenRole)

                            _serviceProxy.Disassociate("systemuser",
                                                       user.Id,
                                                       New Relationship("systemuserroles_association"),
                                                       New EntityReferenceCollection() From
                                                       {New EntityReference("role", salesRole.Id)})
                            Console.WriteLine("Role {0} is disassociated from user {1} {2}.",
                                              _givenRole, user.FirstName, user.LastName)
                        End If
                    End If

                End Using
                '</snippetRemoveRoleFromUser1>
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
			' Create/Retrieve a user and associate a role.

			_userId = SystemUserProvider.RetrieveAUserWithoutAnyRoleAssigned(_serviceProxy)
            ' Find the role.
            Dim query As QueryExpression =
                New QueryExpression With
                {
                    .EntityName = Role.EntityLogicalName,
                    .ColumnSet = New ColumnSet("roleid")
                }
            query.Criteria = New FilterExpression()
            query.Criteria.AddCondition(
                New ConditionExpression("name", ConditionOperator.Equal, {_givenRole}))

            ' Get the role.
			Dim roles As EntityCollection = _serviceProxy.RetrieveMultiple(query)
			If roles.Entities.Count > 0 Then
                Dim salesRole As Role = _serviceProxy.RetrieveMultiple(query) _
                                        .Entities(0).ToEntity(Of Role)()

				' Associate the user with the role for this sample.
				If salesRole IsNot Nothing AndAlso _userId <> Guid.Empty Then
                    _serviceProxy.Associate("systemuser",
                                            _userId,
                                            New Relationship("systemuserroles_association"),
                                            New EntityReferenceCollection() From
                                            {New EntityReference(Role.EntityLogicalName, salesRole.Id)}
                                            )
				End If
			End If
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

				Dim app As New RemoveRoleFromUser()
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
'</snippetRemoveRoleFromUser>
