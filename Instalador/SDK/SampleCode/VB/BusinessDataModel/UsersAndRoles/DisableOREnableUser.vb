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

'<snippetDisableOrEnableUser>
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
	''' Demonstrates how to do disable/enable a system user account.
	''' </summary>
	''' <remarks>
	''' At run-time, you will be given the option to revert the role 
	''' association created by this program.</remarks>
	Public Class DisableOrEnableUser
		#Region "Class Level Members"

		' Define the IDs needed for this sample.
		Private _userId As Guid
		Private _serviceProxy As OrganizationServiceProxy

		#End Region ' Class Level Members

		#Region "How To Sample Code"
		''' <summary>
		''' This method first connects to the Organization service. Afterwards,
        ''' creates/retrieves a system user,
        ''' and updates the system user to disable/enable the user account.
		''' Note: Creating a user is only supported
        ''' in an on-premises/active directory environment.
		''' </summary>
		''' <param name="serverConfig">Contains server connection information.</param>
        ''' <param name="promptforDelete">When True, the user is prompted to delete all
		''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                       ByVal promptforDelete As Boolean)
            Try
                '<snippetDisableOrEnableUser1>
                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy is properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    _serviceProxy.EnableProxyTypes()

                    CreateRequiredRecords()

                    ' Retrieve a user.
                    Dim user As SystemUser =
                        _serviceProxy.Retrieve(SystemUser.EntityLogicalName,
                                               _userId,
                                               New ColumnSet(New String() {"systemuserid", "firstname", "lastname"})).ToEntity(Of SystemUser)()

                    If user IsNot Nothing Then
                        Console.WriteLine("{1} {0} user account is retrieved.", user.LastName, user.FirstName)
                        Dim request As New SetStateRequest() With
                            {
                                .EntityMoniker = user.ToEntityReference(),
                                .State = New OptionSetValue(1),
                                .Status = New OptionSetValue(-1)
                            }
                        ' Sets the user to disabled.
                        ' Required by request but always valued at -1 in this context.
                        '                            
                        '                            //Sets the user to enabled.
                        '                            State = new OptionSetValue(0),
                        '                            // Required by request but always valued at -1 in this context.
                        '                            Status = new OptionSetValue(-1)    
                        '                            

                        _serviceProxy.Execute(request)

                        Console.WriteLine("User account is disabled.")
                    End If
                    DeleteRequiredRecords(promptforDelete)
                End Using
                '</snippetDisableOrEnableUser1>
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
			' Retrieve a user.
			_userId = SystemUserProvider.RetrieveAUserWithoutAnyRoleAssigned(_serviceProxy)
		End Sub

		''' <summary>
        ''' Deletes/reverts any entity records that were created for this sample.
		''' <param name="prompt">Indicates whether to prompt the user 
		''' to delete/revert the records created in this sample.</param>
		''' </summary>
		Public Sub DeleteRequiredRecords(ByVal prompt As Boolean)
			Dim deleteRecords As Boolean = True

			If prompt Then
				Console.Write(vbLf & "Do you want these entity records deleted/reverted? (y/n) [y]: ")
				Dim answer As String = Console.ReadLine()

                deleteRecords = (answer.StartsWith("y") OrElse
                                 answer.StartsWith("Y") OrElse
                                 answer = String.Empty)
			End If

			If deleteRecords Then
                Dim request As New SetStateRequest() With
                    {
                        .EntityMoniker = New EntityReference(SystemUser.EntityLogicalName, _userId),
                        .State = New OptionSetValue(0),
                        .Status = New OptionSetValue(-1)
                    }
                'Sets the user to enabled.
                ' Required by request but always valued at -1 in this context.
				_serviceProxy.Execute(request)
				Console.WriteLine("User account is enabled back again.")
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

				Dim app As New DisableOrEnableUser()
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
'</snippetDisableOrEnableUser>
