﻿' =====================================================================
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
'<snippetCRUDRecurringAppointment>
Imports System.ServiceModel
Imports System.ServiceModel.Description

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Client
Imports Microsoft.Xrm.Sdk.Query
Imports Microsoft.Xrm.Sdk.Discovery
Imports Microsoft.Xrm.Sdk.Messages

' This namespace is found in Microsoft.Crm.Sdk.Proxy.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Crm.Sdk.Messages

Namespace Microsoft.Crm.Sdk.Samples
    ''' <summary>
    ''' This sample shows how to create, retrieve, update, and delete a 
    ''' recurring appointment series.
    ''' </summary>
    Public Class CRUDRecurringAppointment
        #Region "Class Level Members"

        ''' <summary>
        ''' Stores the organization service proxy.
        ''' </summary>
        Private _serviceProxy As OrganizationServiceProxy

        ' Define the IDs as well as strings needed for this sample.
        Public _recurringAppointmentMasterId As Guid

        #End Region ' Class Level Members

        #Region "How To Sample Code"
        ''' <summary>
        ''' Create and configure the organization service proxy.
        ''' Create a recurring appointment.
        ''' Retrieve the recurring appointment.
        ''' Update the retrieved recurring appointment.
        ''' Optionally delete any entity records that were created for this sample.
        ''' </summary>
        ''' <param name="serverConfig">Contains server connection information.</param>
        ''' <param name="promptforDelete">When True, the user will be prompted to delete all
        ''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration, ByVal promptForDelete As Boolean)
            Try

                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy will be properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    ' This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes()


                    '<snippetCRUDRecurringAppointment1>
                    ' Define an anonymous type to define the possible recurrence pattern values.
                    Dim RecurrencePatternTypes = New With {Key .Daily = 0, Key .Weekly = 1, Key .Monthly = 2, Key .Yearly = 3}

                    ' Define an anonymous type to define the possible values for days 
                    ' of the week
                    Dim DayOfWeek = New With {Key .Sunday = &H1, Key .Monday = &H2, Key .Tuesday = &H4, Key .Wednesday = &H8, _
                                              Key .Thursday = &H10, Key .Friday = &H20, Key .Saturday = &H40}

                    ' Define an anonymous type to define the possible values  
                    ' for the recurrence rule pattern end type.
                    Dim RecurrenceRulePatternEndType = New With {Key .NoEndDate = 1, Key .Occurrences = 2, Key .PatternEndDate = 3}

                    ' Create a recurring appointment
                    Dim newRecurringAppointment As RecurringAppointmentMaster = New RecurringAppointmentMaster With { _
                        .Subject = "Sample Recurring Appointment", .StartTime = Date.Now.AddHours(1), .EndTime = Date.Now.AddHours(2), _
                        .RecurrencePatternType = New OptionSetValue(RecurrencePatternTypes.Weekly), .Interval = 1, _
                        .DaysOfWeekMask = DayOfWeek.Thursday, .PatternStartDate = Date.Today, .Occurrences = 10, _
                        .PatternEndType = New OptionSetValue(RecurrenceRulePatternEndType.Occurrences)}

                    _recurringAppointmentMasterId = _serviceProxy.Create(newRecurringAppointment)
                    Console.WriteLine("Created {0}.", newRecurringAppointment.Subject)

                    ' Retrieve the newly created recurring appointment
                    Dim recurringAppointmentQuery As QueryExpression = New QueryExpression With { _
                        .EntityName = RecurringAppointmentMaster.EntityLogicalName, .ColumnSet = New ColumnSet("subject"), _
                        .PageInfo = New PagingInfo With {.Count = 1, .PageNumber = 1}}
                    recurringAppointmentQuery.Criteria = New FilterExpression()
                    recurringAppointmentQuery.Criteria.AddCondition("subject", ConditionOperator.Equal, {"Sample Recurring Appointment"})
                    recurringAppointmentQuery.Criteria.AddCondition("interval", ConditionOperator.Equal, {1})

                    Dim retrievedRecurringAppointment As RecurringAppointmentMaster = _
                        _serviceProxy.RetrieveMultiple(recurringAppointmentQuery).Entities.Select(Function(x) _
                                                                                  CType(x, RecurringAppointmentMaster)).FirstOrDefault()

                    Console.WriteLine("Retrieved the recurring appointment.")

                    ' Update the recurring appointment.
                    ' Update the following for the retrieved recurring appointment series:
                    ' 1. Update the subject.
                    ' 2. Update the number of occurences to 5.
                    ' 3. Update the appointment interval to 2.

                    retrievedRecurringAppointment.Subject = "Updated Recurring Appointment"
                    retrievedRecurringAppointment.Occurrences = 5
                    retrievedRecurringAppointment.Interval = 2
                    _serviceProxy.Update(retrievedRecurringAppointment)

                    Console.WriteLine("Updated the subject, occurrences, and interval of the recurring appointment.")
                    '</snippetCRUDRecurringAppointment1>

                    DeleteRequiredRecords(promptForDelete)
                End Using
            ' Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            Catch fe As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
                ' You can handle an exception here or pass it back to the calling method.
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Deletes any entity records that were created for this sample.
        ''' <param name="prompt">Indicates whether to prompt the user to delete 
        ''' the records created in this sample.</param>
        ''' </summary>
        Public Sub DeleteRequiredRecords(ByVal prompt As Boolean)
            Dim deleteRecords As Boolean = True

            If prompt Then
                Console.WriteLine(vbLf & "Do you want these entity records to be deleted? (y/n)")
                Dim answer As String = Console.ReadLine()

                deleteRecords = (answer.StartsWith("y") OrElse answer.StartsWith("Y"))
            End If

            If deleteRecords Then
                _serviceProxy.Delete(RecurringAppointmentMaster.EntityLogicalName, _recurringAppointmentMasterId)

                Console.WriteLine("Entity records have been deleted.")
            End If
        End Sub

        #End Region ' How To Sample Code

        #Region "Main"
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

                Dim app As New CRUDRecurringAppointment()
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

                    Dim fe As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault) = _
                        TryCast(ex.InnerException, FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault))
                    If fe IsNot Nothing Then
                        Console.WriteLine("Timestamp: {0}", fe.Detail.Timestamp)
                        Console.WriteLine("Code: {0}", fe.Detail.ErrorCode)
                        Console.WriteLine("Message: {0}", fe.Detail.Message)
                        Console.WriteLine("Plugin Trace: {0}", fe.Detail.TraceText)
                        Console.WriteLine("Inner Fault: {0}", If(Nothing Is fe.Detail.InnerFault, "No Inner Fault", "Has Inner Fault"))
                    End If
                End If
            ' Additional exceptions to catch: SecurityTokenValidationException, ExpiredSecurityTokenException,
            ' SecurityAccessDeniedException, MessageSecurityException, and SecurityNegotiationException.

            Finally

                Console.WriteLine("Press <Enter> to exit.")
                Console.ReadLine()
            End Try

        End Sub
        #End Region ' Main
    End Class
End Namespace
'</snippetCRUDRecurringAppointment>