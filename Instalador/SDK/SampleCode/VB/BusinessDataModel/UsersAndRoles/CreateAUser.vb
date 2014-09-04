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

'<snippetCreateAUser>
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
	''' Demonstrates how to do basic entity operations like create 
	''' a system user account.</summary>
	''' <remarks>
	''' At run-time, you will be given the option to delete all the
	''' database records created by this program.</remarks>
	Public Class CreateAUser
		#Region "Class Level Members"

		' Define the IDs needed for this sample.
		Private _domain As String = String.Empty
		Private _userName As String = "dparker"
		Private _firstName As String = "Darren"
		Private _lastName As String = "Parker"
		Private _serviceProxy As OrganizationServiceProxy

		#End Region ' Class Level Members

		#Region "How To Sample Code"
		''' <summary>
		''' This method first connects to the Organization service. Afterwards,
        ''' it creates a system user account with a given active directory account.
        ''' Note: Creating a user is only supported in an on-premises/active directory environment.
		''' </summary>
		''' <param name="serverConfig">Contains server connection information.</param>
        ''' <param name="promptforDelete">When True, the user is prompted to delete all
		''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                       ByVal promptforDelete As Boolean)
            Try
                '<snippetCreateAUser1>
                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy is properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    _serviceProxy.EnableProxyTypes()

                    CreateRequiredRecords()

                    ' Retrieve the default business unit needed to create the user.
                    Dim businessUnitQuery As QueryExpression =
                        New QueryExpression With
                        {
                            .EntityName = BusinessUnit.EntityLogicalName,
                            .ColumnSet = New ColumnSet("businessunitid")
                        }
                    businessUnitQuery.Criteria = New FilterExpression()
                    businessUnitQuery.Criteria.AddCondition(
                        New ConditionExpression("parentbusinessunitid", ConditionOperator.Null))

                    Dim defaultBusinessUnit As BusinessUnit =
                        _serviceProxy.RetrieveMultiple(businessUnitQuery).Entities(0).ToEntity(Of BusinessUnit)()

                    'Create a new system user.
                    Dim user As SystemUser =
                        New SystemUser With
                        {
                            .DomainName = _domain & _userName,
                            .FirstName = _firstName,
                            .LastName = _lastName,
                            .BusinessUnitId =
                            New EntityReference With
                            {
                                .LogicalName = BusinessUnit.EntityLogicalName,
                                .Name = BusinessUnit.EntityLogicalName,
                                .Id = defaultBusinessUnit.Id
                            }
                        }

                    Dim userId As Guid = _serviceProxy.Create(user)

                    Console.WriteLine("Created a system user {0} for '{1}, {2}'", userId, _lastName, _firstName)
                End Using
                '</snippetCreateAUser1>
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
			' Obtain the current user's information.
			Dim who As New WhoAmIRequest()
			Dim whoResp As WhoAmIResponse = CType(_serviceProxy.Execute(who), WhoAmIResponse)
			Dim currentUserId As Guid = whoResp.UserId

            Dim currentUser As SystemUser =
                _serviceProxy.Retrieve(SystemUser.EntityLogicalName,
                                       currentUserId,
                                       New ColumnSet("domainname")).ToEntity(Of SystemUser)()

			' Extract the domain and create the LDAP object.
			Dim userPath() As String = currentUser.DomainName.Split(New Char() { "\"c })
			If userPath.Length > 1 Then
				_domain = userPath(0) & "\"
			Else
				_domain = String.Empty
			End If

            Dim existingUser As SystemUser =
                SystemUserProvider.GetUserIdIfExist(_serviceProxy, _domain, _userName, _firstName, _lastName)

			If existingUser IsNot Nothing Then
				Throw New Exception("User already exist!")
			End If

            ' Setup an Active Directory account in the current domain for this sample.
			Dim ldapPath As String = String.Empty
            Dim accountSetup As Boolean =
                SystemUserProvider.CreateADAccount(_userName, _firstName, _lastName, _serviceProxy, ldapPath)
			If accountSetup Then
				Console.WriteLine("An AD account created for '{0}, {1}'", _lastName, _firstName)
			Else
				Console.WriteLine("AD account already exist for '{0}, {1}'", _lastName, _firstName)
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
				' Obtain the target organization's Web address and client logon 
				' credentials from the user.
				Dim serverConnect As New ServerConnection()
                Dim config As ServerConnection.Configuration =
                    serverConnect.GetServerConfiguration()

				Dim app As New CreateAUser()
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
'</snippetCreateAUser>
