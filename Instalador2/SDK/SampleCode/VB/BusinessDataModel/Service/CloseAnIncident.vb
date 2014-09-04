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


'<snippetCloseAnIncident>
Imports System
Imports System.ServiceModel
Imports Microsoft.Crm.Sdk.Messages
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Client

Namespace Microsoft.Crm.Sdk.Samples
	Public Class CloseAnIncident
		#Region "Class Level Members"
		Private _serviceProxy As OrganizationServiceProxy
		Private _incidentId As Guid
		Private _appointmentId As Guid
		Private _accountId As Guid
		#End Region

		#Region "How To Sample Code"

        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                       ByVal promptforDelete As Boolean)
            _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
            Using _serviceProxy
                ' This statement is required to enable early bound type support.
                _serviceProxy.EnableProxyTypes()
                CreateRequiredRecords()
                RunIncidentManipulation()
                DeleteRecords(promptforDelete)
            End Using
        End Sub

		Private Sub RunIncidentManipulation()
			Console.WriteLine("=== Creating and Closing an Incident (Case) ===")

			' Create an incident.
            Dim incident =
                New Incident With
                {
                    .CustomerId = New EntityReference(Account.EntityLogicalName,
                                                      _accountId),
                    .Title = "Sample Incident"
                }

			_incidentId = _serviceProxy.Create(incident)
			NotifyEntityCreated(Incident.EntityLogicalName, _incidentId)

            ' Create a 30-minute appointment regarding the incident.
            Dim appointment =
                New Appointment With
                {
                    .ScheduledStart = Date.Now,
                    .ScheduledEnd = Date.Now.Add(New TimeSpan(0, 30, 0)),
                    .Subject = "Sample 30-minute Appointment",
                    .RegardingObjectId = New EntityReference(incident.EntityLogicalName,
                                                             _incidentId)
                }

			_appointmentId = _serviceProxy.Create(appointment)
			NotifyEntityCreated(Appointment.EntityLogicalName, _appointmentId)

			' Show the time spent on the incident before closing the appointment.
			NotifyTimeSpentOnIncident()
            ' Check the validity of the state transition to closed on the incident.
			NotifyValidityOfIncidentSolvedStateChange()
			'<snippetCloseAnIncident1>
			' Close the appointment.
            Dim setAppointmentStateReq =
                New SetStateRequest With
                {
                    .EntityMoniker = New EntityReference(appointment.EntityLogicalName,
                                                         _appointmentId),
                    .State = New OptionSetValue(CInt(Fix(AppointmentState.Completed))),
                    .Status = New OptionSetValue(CInt(Fix(appointment_statuscode.Completed)))
                }

			_serviceProxy.Execute(setAppointmentStateReq)

			Console.WriteLine("  Appointment state set to completed.")
			'</snippetCloseAnIncident1>

			' Show the time spent on the incident after closing the appointment.
			NotifyTimeSpentOnIncident()
            ' Check the validity of the state transition to closed again.
			NotifyValidityOfIncidentSolvedStateChange()

			' Create the incident's resolution.
            Dim incidentResolution =
                New IncidentResolution With
                {
                    .Subject = "Resolved Sample Incident",
                    .IncidentId = New EntityReference(incident.EntityLogicalName,
                                                      _incidentId)
                }

			'<snippetCloseAnIncident2>
			' Close the incident with the resolution.
            Dim closeIncidentRequest =
                New CloseIncidentRequest With
                {
                    .IncidentResolution = incidentResolution,
                    .Status =
                    New OptionSetValue(CInt(Fix(incident_statuscode.ProblemSolved)))
                }

			_serviceProxy.Execute(closeIncidentRequest)

			Console.WriteLine("  Incident closed.")
			'</snippetCloseAnIncident2>
		End Sub

		Private Sub NotifyValidityOfIncidentSolvedStateChange()
			'<snippetCloseAnIncident3>
            ' Validate the state transition.  
            Dim isValidRequest =
                New IsValidStateTransitionRequest With
                {
                    .Entity = New EntityReference(Incident.EntityLogicalName,
                                                  _incidentId),
                    .NewState = IncidentState.Resolved.ToString(),
                    .NewStatus = CInt(Fix(incident_statuscode.ProblemSolved))
                }

            Dim response = CType(_serviceProxy.Execute(isValidRequest), 
                IsValidStateTransitionResponse)
			Dim isValidString = If(response.IsValid, "is valid", "is not valid")
            Console.WriteLine("  The transition to a completed status reason {0}.",
                              isValidString)
			'</snippetCloseAnIncident3>
		End Sub

		Private Sub NotifyTimeSpentOnIncident()
			'<snippetCloseAnIncident4>
			' Calculate the total number of minutes spent on an incident. 
            Dim calculateRequestTime =
                New CalculateTotalTimeIncidentRequest With
                {
                    .IncidentId = _incidentId
                }
            Dim response = CType(_serviceProxy.Execute(calculateRequestTime), 
                CalculateTotalTimeIncidentResponse)

            Console.WriteLine("  {0} minutes have been spent on the incident.",
                              response.TotalTime)
			'</snippetCloseAnIncident4>
		End Sub

		Private Sub NotifyEntityCreated(ByVal entityName As String, ByVal entityId As Guid)
			Console.WriteLine("  {0} created with GUID {{{1}}}", entityName, entityId)
		End Sub

		Private Sub CreateRequiredRecords()
			' Create an account to act as a customer for the incident.
            Dim account = New Account With
                          {
                              .Name = "Litware, Inc.",
                              .Address1_StateOrProvince = "Colorado"
                          }
			_accountId = (_serviceProxy.Create(account))
		End Sub

		Private Sub DeleteRecords(ByVal prompt As Boolean)
			Dim toBeDeleted As Boolean = True

			If prompt Then
				' Ask the user if the created entities should be deleted.
				Console.Write(vbLf & "Do you want these entity records deleted? (y/n) [y]: ")
				Dim answer As String = Console.ReadLine()
                If answer.StartsWith("y") OrElse answer.StartsWith("Y") OrElse answer =
                    String.Empty Then
                    toBeDeleted = True
                Else
                    toBeDeleted = False
                End If
			End If

			If toBeDeleted Then
				' The account is all that needs to be deleted.  Everything else will be
				' deleted with it.
				_serviceProxy.Delete(Account.EntityLogicalName, _accountId)
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

				Dim app = New CloseAnIncident()
				app.Run(config, True)
			Catch ex As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp)
				Console.WriteLine("Code: {0}", ex.Detail.ErrorCode)
				Console.WriteLine("Message: {0}", ex.Detail.Message)
				Console.WriteLine("Plugin Trace: {0}", ex.Detail.TraceText)
                Console.WriteLine("Inner Fault: {0}",
                                  If(Nothing Is ex.Detail.InnerFault, "No Inner Fault", "Has Inner Fault"))
			Catch ex As System.TimeoutException
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine("Message: {0}", ex.Message)
				Console.WriteLine("Stack Trace: {0}", ex.StackTrace)
                Console.WriteLine("Inner Fault: {0}",
                                  If(Nothing Is ex.InnerException.Message, "No Inner Fault", ex.InnerException.Message))
			Catch ex As System.Exception
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
'</snippetCloseAnIncident>