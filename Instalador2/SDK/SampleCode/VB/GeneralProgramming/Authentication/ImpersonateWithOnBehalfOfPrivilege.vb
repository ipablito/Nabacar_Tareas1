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

'<snippetImpersonateWithOnBehalfOfPrivilege>
Imports System.ServiceModel

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Query
Imports Microsoft.Xrm.Sdk.Client

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' Demonstrates how to impersonate another user with the on-behalf-of privilege and
	''' do basic entity operations like create, retrieve, update, and delete.</summary>
	''' <remarks>
	''' The system user account under which you run the sample must be part of the 
	''' Administrators group on your computer system so that this sample can create
	''' and use a second user account. You must also have the
	''' "Act on Behalf of Another User" privilege in Microsoft Dynamics CRM.
	''' 
	''' Note that the effective set of privileges for the operations performed will be the
	''' intersection of the privileges that the logged on (privileged) user possesses with
	''' that of the user that is being impersonated.
	'''
	''' At run-time, you will be given the option to delete all the
	''' database records created by this program.</remarks>
	Public Class ImpersonateWithOnBehalfOfPrivilege
		#Region "Class Level Members"

		Private _userId As Guid
		Private _accountId As Guid
		Private _serviceProxy As OrganizationServiceProxy

		#End Region ' Class Level Members

		#Region "How To Sample Code"
		''' <summary>
		''' This method connects to the Organization service using an impersonated user
		''' credential. Afterwards, basic create, retrieve, update, and delete entity
		''' operations are performed as the impersonated user.
		''' </summary>
		''' <param name="serverConfig">Contains server connection information.</param>
		''' <param name="promptforDelete">When True, the user will be prompted to delete
		''' all created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                       ByVal promptforDelete As Boolean)
            Try
                '<snippetImpersonateWithOnBehalfOfPrivilege1>
                '<snippetImpersonateWithOnBehalfOfPrivilege2>
                ' Connect to the Organization service. 
                ' The using statement ensures that the service proxy will be properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    ' This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes()

                    CreateRequiredRecords()

                    ' Retrieve the system user ID of the user to impersonate.
                    Dim orgContext As New OrganizationServiceContext(_serviceProxy)
                    _userId = ( _
                        From user In orgContext.CreateQuery(Of SystemUser)() _
                        Where user.FullName.Equals("Kevin Cook") _
                        Select user.SystemUserId.Value).FirstOrDefault()

                    ' To impersonate another user, set the OrganizationServiceProxy.CallerId
                    ' property to the ID of the other user.
                    _serviceProxy.CallerId = _userId

                    ' Instantiate an account object.
                    ' See the Entity Metadata topic in the SDK documentation to determine 
                    ' which attributes must be set for each entity.
                    Dim account As Account = New Account With {.Name = "Fourth Coffee"}

                    ' Create an account record named Fourth Coffee.
                    _accountId = _serviceProxy.Create(account)
                    Console.Write("{0} {1} created, ", account.LogicalName, account.Name)
                    '</snippetImpersonateWithOnBehalfOfPrivilege2>

                    ' Retrieve the account containing several of its attributes.
                    ' CreatedBy should reference the impersonated SystemUser.
                    ' CreatedOnBehalfBy should reference the running SystemUser.
                    Dim cols As New ColumnSet("name", "createdby", "createdonbehalfby",
                                              "address1_postalcode", "lastusedincampaign")

                    Dim retrievedAccount As Account = CType(_serviceProxy.Retrieve(
                            account.EntityLogicalName, _accountId, cols), Account)
                    Console.Write("retrieved, ")

                    ' Update the postal code attribute.
                    retrievedAccount.Address1_PostalCode = "98052"

                    ' The address 2 postal code was set accidentally, so set it to null.
                    retrievedAccount.Address2_PostalCode = Nothing

                    ' Shows use of a Money value.
                    retrievedAccount.Revenue = New Money(5000000)

                    ' Shows use of a boolean value.
                    retrievedAccount.CreditOnHold = False

                    ' Update the account record.
                    _serviceProxy.Update(retrievedAccount)
                    Console.Write("updated, ")

                    ' Delete the account record.
                    _serviceProxy.Delete(account.EntityLogicalName, _accountId)
                    Console.WriteLine("and deleted.")

                    DeleteRequiredRecords(promptforDelete)
                End Using
                '</snippetImpersonateWithOnBehalfOfPrivilege1>

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
			' Create a second user that we will impersonate in our sample code.
			SystemUserProvider.RetrieveSalesManager(_serviceProxy)
		End Sub

		''' <summary>
		''' Deletes any entity records that were created for this sample.
		''' <param name="prompt">Indicates whether to prompt the user 
		''' to delete the records created in this sample.</param>
		''' </summary>
		Public Sub DeleteRequiredRecords(ByVal prompt As Boolean)
			' For this sample, all created records are deleted in the Run() method.
			' The system user named "Kevin Cook" that was created by this sample will
			' continue to exist on your system because system users cannot be deleted
			' in Microsoft Dynamics CRM.  They can only be enabled or disabled.
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

				Dim app As New ImpersonateWithOnBehalfOfPrivilege()
				app.Run(config, True)
			Catch ex As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp)
				Console.WriteLine("Code: {0}", ex.Detail.ErrorCode)
				Console.WriteLine("Message: {0}", ex.Detail.Message)
				Console.WriteLine("Plugin Trace: {0}", ex.Detail.TraceText)
                Console.WriteLine("Inner Fault: {0}", If(Nothing Is ex.Detail.InnerFault, "No Inner Fault", "Has Inner Fault"))
			Catch ex As TimeoutException
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine("Message: {0}", ex.Message)
				Console.WriteLine("Stack Trace: {0}", ex.StackTrace)
                Console.WriteLine("Inner Fault: {0}", If(Nothing Is ex.InnerException.Message, "No Inner Fault", ex.InnerException.Message))
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
						Console.WriteLine("Plugin Trace: {0}", fe.Detail.TraceText)
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
'</snippetImpersonateWithOnBehalfOfPrivilege>
