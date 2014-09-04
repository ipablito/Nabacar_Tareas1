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

'<snippetOverrideGoalTotalCount>
Imports System.ServiceModel

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Query
Imports Microsoft.Xrm.Sdk.Client
Imports Microsoft.Xrm.Sdk.Messages

' This namespace is found in Microsoft.Crm.Sdk.Proxy.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Crm.Sdk.Messages

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' Demonstrates how to override the goal total count and close a goal.</summary>
	''' <remarks>
	''' At run-time, you will be given the option to delete all the
	''' database records created by this program.</remarks>
	Public Class OverrideGoalTotalCount
		#Region "Class Level Members"

		Private _metricId As Guid
		Private _actualId As Guid
		Private _inprogressId As Guid
		Private _goalId As Guid
		Private _accountId As Guid
		Private _salesManagerId As Guid
		Private _phoneCallId As Guid
		Private _phoneCall2Id As Guid
		Private _rollupQueryIds As New List(Of Guid)()

		Private _serviceProxy As OrganizationServiceProxy

		#End Region ' Class Level Members

		#Region "How To Sample Code"
		''' <summary>
		''' This method first connects to the Organization service. Afterwards,
		''' a goal is created specifying the target count. The goal is then
		''' rolled up, the actual and in-progress values are overridden and finally the 
		''' goal is closed.
		''' </summary>
		''' <param name="serverConfig">Contains server connection information.</param>
		''' <param name="promptforDelete">When True, the user will be prompted to delete all
		''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                       ByVal promptforDelete As Boolean)
            Try
                '<snippetOverrideGoalTotalCount1>
                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy will be properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    ' This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes()

                    CreateRequiredRecords()

                    ' Create the count metric, setting the Metric Type to 'Count' by
                    ' setting IsAmount to false.
                    Dim sampleMetric As New Metric() With
                        {
                            .Name = "Sample Count Metric",
                            .IsAmount = False
                        }
                    _metricId = _serviceProxy.Create(sampleMetric)
                    sampleMetric.Id = _metricId

                    Console.Write("Created phone call metric, ")

                    '					#Region "Create RollupFields"

                    ' Create RollupField which targets completed (received) phone calls.
                    Dim actual As New RollupField() With
                        {
                            .SourceEntity = PhoneCall.EntityLogicalName,
                            .GoalAttribute = "actualinteger",
                            .SourceState = 1,
                            .SourceStatus = 4,
                            .EntityForDateAttribute = PhoneCall.EntityLogicalName,
                            .DateAttribute = "actualend",
                            .MetricId = sampleMetric.ToEntityReference()
                        }
                    _actualId = _serviceProxy.Create(actual)

                    Console.Write("created actual revenue RollupField, ")

                    ' Create RollupField which targets open (in-progress) phone calls.
                    Dim inprogress As New RollupField() With
                        {
                            .SourceEntity = PhoneCall.EntityLogicalName,
                            .GoalAttribute = "inprogressinteger",
                            .SourceState = 0,
                            .EntityForDateAttribute = PhoneCall.EntityLogicalName,
                            .DateAttribute = "createdon",
                            .MetricId = sampleMetric.ToEntityReference()
                        }
                    _inprogressId = _serviceProxy.Create(inprogress)

                    Console.Write("created in-progress revenue RollupField, ")

                    '					#End Region

                    '					#Region "Create the goal rollup queries"

                    ' Note: Formatting the FetchXml onto multiple lines in the following 
                    ' rollup queries causes the length property to be greater than 1,000
                    ' chars and will cause an exception.

                    ' The following query locates closed incoming phone calls.
                    Dim goalRollupQuery As _
                        New GoalRollupQuery() With
                        {
                            .Name = "Example Goal Rollup Query - Actual",
                            .QueryEntityType = PhoneCall.EntityLogicalName,
                            .FetchXml = _
                            "<fetch version='1.0' output-format='xml-platform' " _
                            & "     mapping='logical' distinct='false'>" _
                            & " <entity name='phonecall'>" _
                            & "     <attribute name='subject'/>" _
                            & "     <attribute name='statecode'/>" _
                            & "     <attribute name='prioritycode'/>" _
                            & "     <attribute name='scheduledend'/>" _
                            & "     <attribute name='createdby'/>" _
                            & "     <attribute name='regardingobjectid'/>" _
                            & "     <attribute name='activityid'/>" _
                            & "     <order attribute='subject' descending='false'/>" _
                            & "     <filter type='and'>" _
                            & "         <condition attribute='directioncode' " _
                            & "                     operator='eq' value='0'/>" _
                            & "         <condition attribute='statecode' " _
                            & "                     operator='eq' value='1' />" _
                            & "     </filter>" _
                            & " </entity>" _
                            & "</fetch>"
                        }
                    _rollupQueryIds.Add(_serviceProxy.Create(goalRollupQuery))
                    goalRollupQuery.Id = _rollupQueryIds(0)

                    ' The following query locates open incoming phone calls.
                    Dim inProgressGoalRollupQuery As _
                        New GoalRollupQuery() With
                        {
                            .Name = "Example Goal Rollup Query - InProgress",
                            .QueryEntityType = PhoneCall.EntityLogicalName,
                            .FetchXml = _
                            "<fetch version='1.0' output-format='xml-platform' " _
                            & "     mapping='logical' distinct='false'>" _
                            & " <entity name='phonecall'>" _
                            & "     <attribute name='subject'/>" _
                            & "     <attribute name='statecode'/>" _
                            & "     <attribute name='prioritycode'/>" _
                            & "     <attribute name='scheduledend'/>" _
                            & "     <attribute name='createdby'/>" _
                            & "     <attribute name='regardingobjectid'/>" _
                            & "     <attribute name='activityid'/>" _
                            & "     <order attribute='subject' descending='false'/>" _
                            & "     <filter type='and'>" _
                            & "         <condition attribute='directioncode' " _
                            & "                     operator='eq' value='0'/>" _
                            & "         <condition attribute='statecode' " _
                            & "                     operator='eq' value='0' />" _
                            & "     </filter>" _
                            & " </entity>" _
                            & "</fetch>"
                        }
                    _rollupQueryIds.Add(_serviceProxy.Create(inProgressGoalRollupQuery))
                    inProgressGoalRollupQuery.Id = _rollupQueryIds(1)

                    Console.Write("created rollup queries for incoming phone calls." & vbLf)
                    Console.WriteLine()

                    '					#End Region

                    '					#Region "Create a goal to track the open incoming phone calls."

                    ' Create the goal.
                    Dim goal As New Goal() With
                        {
                            .Title = "Sample Goal",
                            .RollupOnlyFromChildGoals = False,
                            .ConsiderOnlyGoalOwnersRecords = False,
                            .TargetInteger = 5,
                            .RollupQueryActualIntegerId =
                                goalRollupQuery.ToEntityReference(),
                            .RollUpQueryInprogressIntegerId =
                                inProgressGoalRollupQuery.ToEntityReference(),
                            .IsFiscalPeriodGoal = False,
                            .MetricId = sampleMetric.ToEntityReference(),
                            .GoalOwnerId = New EntityReference With
                                           {
                                               .Id = _salesManagerId,
                    .LogicalName = SystemUser.EntityLogicalName
                                           },
                            .OwnerId = New EntityReference With
                                       {
                                           .Id = _salesManagerId,
                                           .LogicalName = SystemUser.EntityLogicalName
                                       },
                            .GoalStartDate = Date.Today.AddDays(-1),
                    .GoalEndDate = Date.Today.AddDays(30)
                        }
                    _goalId = _serviceProxy.Create(goal)
                    goal.Id = _goalId

                    Console.WriteLine("Created goal")
                    Console.WriteLine("-------------------")
                    Console.WriteLine("Target: {0}", goal.TargetInteger.Value)
                    Console.WriteLine("Goal owner: {0}", goal.GoalOwnerId.Id)
                    Console.WriteLine("Goal Start Date: {0}", goal.GoalStartDate)
                    Console.WriteLine("Goal End Date: {0}", goal.GoalEndDate)
                    Console.WriteLine("<End of Listing>")
                    Console.WriteLine()

                    '					#End Region

                    '					#Region "Calculate rollup and display result"

                    ' Calculate roll-up of the goal.
                    Dim recalculateRequest As New RecalculateRequest() With
                        {.Target = goal.ToEntityReference()}
                    _serviceProxy.Execute(recalculateRequest)

                    Console.WriteLine("Calculated roll-up of goal.")
                    Console.WriteLine()

                    ' Retrieve and report 3 different computed values for the goal
                    ' - Percentage
                    ' - Actual (Integer)
                    ' - In-Progress (Integer)
                    Dim retrieveValues As New QueryExpression() With
                        {
                            .EntityName = goal.EntityLogicalName,
                            .ColumnSet = New ColumnSet("title",
                                                       "percentage",
                                                       "actualinteger",
                                                       "inprogressinteger")
                        }
                    Dim ec As EntityCollection = _serviceProxy.RetrieveMultiple(
                        retrieveValues)

                    ' Compute and display the results.
                    For i As Integer = 0 To ec.Entities.Count - 1
                        Dim temp As Goal = CType(ec.Entities(i), Goal)
                        Console.WriteLine("Roll-up details for goal: {0}", temp.Title)
                        Console.WriteLine("---------------")
                        Console.WriteLine("Percentage Achieved: {0}", temp.Percentage)
                        Console.WriteLine("Actual (Integer): {0}",
                                          temp.ActualInteger.Value)
                        Console.WriteLine("In-Progress (Integer): {0}",
                                          temp.InProgressInteger.Value)
                        Console.WriteLine("<End of Listing>")
                    Next i

                    Console.WriteLine()

                    '					#End Region

                    '					#Region "Update goal to override the actual rollup value"

                    ' Override the actual and in-progress values of the goal.
                    ' To prevent rollup values to be overwritten during next Recalculate operation, 
                    ' set: goal.IsOverridden = true;

                    goal.IsOverride = True
                    goal.ActualInteger = 10
                    goal.InProgressInteger = 5

                    ' Update the goal.
                    Dim update As New UpdateRequest() With {.Target = goal}
                    _serviceProxy.Execute(update)

                    Console.WriteLine("Goal actual and in-progress values overridden.")
                    Console.WriteLine()

                    '					#End Region

                    '					#Region "Retrieve result of manual override"

                    ' Retrieve and report 3 different computed values for the goal
                    ' - Percentage
                    ' - Actual (Integer)
                    ' - In-Progress (Integer)
                    retrieveValues = New QueryExpression() With
                                     {
                                         .EntityName = goal.EntityLogicalName,
                                         .ColumnSet = New ColumnSet("title",
                                                                    "percentage",
                                                                    "actualinteger",
                                                                    "inprogressinteger")
                                     }
                    ec = _serviceProxy.RetrieveMultiple(retrieveValues)

                    ' Compute and display the results.
                    For i As Integer = 0 To ec.Entities.Count - 1
                        Dim temp As Goal = CType(ec.Entities(i), Goal)
                        Console.WriteLine("Roll-up details for goal: {0}", temp.Title)
                        Console.WriteLine("---------------")
                        Console.WriteLine("Percentage Achieved: {0}", temp.Percentage)
                        Console.WriteLine("Actual (Integer): {0}",
                                          temp.ActualInteger.Value)
                        Console.WriteLine("In-Progress (Integer): {0}",
                                          temp.InProgressInteger.Value)
                        Console.WriteLine("<End of Listing>")
                    Next i

                    Console.WriteLine()

                    '					#End Region

                    '					#Region "Close the goal"

                    ' Close the goal.
                    Dim closeGoal As New SetStateRequest() With
                        {
                            .EntityMoniker = goal.ToEntityReference(),
                            .State = New OptionSetValue(1),
                            .Status = New OptionSetValue(1)
                        }

                    Console.WriteLine("Goal closed.")

                    '					#End Region

                    DeleteRequiredRecords(promptforDelete)
                End Using
                '</snippetOverrideGoalTotalCount1>

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

'			#Region "Create or Retrieve the necessary system users"

			' Retrieve a sales manager.
			_salesManagerId = SystemUserProvider.RetrieveMarketingManager(_serviceProxy)

'			#End Region

'			#Region "Create PhoneCall record and supporting account"

            Dim newAccount As Account = New Account With
                                        {
                                            .Name = "Margie's Travel",
                                            .Address1_PostalCode = "99999"
                                        }
			_accountId = (_serviceProxy.Create(newAccount))
			newAccount.Id = _accountId

			' Create Guids for PhoneCalls
			_phoneCallId = Guid.NewGuid()
			_phoneCall2Id = Guid.NewGuid()

			' Create ActivityPartys for the phone calls' "From" field.
            Dim activityParty As New ActivityParty() With
                {
                    .PartyId = newAccount.ToEntityReference(),
                    .ActivityId = New EntityReference With
                                  {
                                      .Id = _phoneCallId,
                                      .LogicalName = PhoneCall.EntityLogicalName
                                  },
                    .ParticipationTypeMask = New OptionSetValue(9)
                }

            Dim activityPartyClosed As New ActivityParty() With
                {
                    .PartyId = newAccount.ToEntityReference(),
                    .ActivityId = New EntityReference With
                                  {
                                      .Id = _phoneCall2Id,
                                      .LogicalName = PhoneCall.EntityLogicalName
                                  },
                    .ParticipationTypeMask = New OptionSetValue(9)
                }

			' Create an open phone call.
            Dim phoneCall_renamed As New PhoneCall() With
                {
                    .Id = _phoneCallId,
                    .Subject = "Sample Phone Call",
                    .DirectionCode = False,
                    .To = New ActivityParty() {activityParty}
                }
            _serviceProxy.Create(phoneCall_renamed)

			' Create a second phone call to close
            phoneCall_renamed = New PhoneCall() With
                                {
                                    .Id = _phoneCall2Id,
                                    .Subject = "Sample Phone Call 2",
                                    .DirectionCode = False,
                                    .To = New ActivityParty() {activityParty},
                                    .ActualEnd = Date.Now
                                }
            _serviceProxy.Create(phoneCall_renamed)

			' Close the second phone call.
            Dim closePhoneCall As New SetStateRequest() With
                {
                    .EntityMoniker = phoneCall_renamed.ToEntityReference(),
                    .State = New OptionSetValue(1),
                    .Status = New OptionSetValue(4)
                }
			_serviceProxy.Execute(closePhoneCall)

'			#End Region
		End Sub

		''' <summary>
		''' Deletes any entity records that were created for this sample.
		''' <param name="prompt">Indicates whether to prompt the user 
		''' to delete the records created in this sample.</param>
		''' </summary>
		Public Sub DeleteRequiredRecords(ByVal prompt As Boolean)
			' The three system users that were created by this sample will continue to 
			' exist on your system because system users cannot be deleted in Microsoft
			' Dynamics CRM.  They can only be enabled or disabled.

			Dim toBeDeleted As Boolean = True

			If prompt Then
				' Ask the user if the created entities should be deleted.
				Console.Write(vbLf & "Do you want these entity records deleted? (y/n) [y]: ")
				Dim answer As String = Console.ReadLine()
                If answer.StartsWith("y") OrElse answer.StartsWith("Y") _
                    OrElse answer = String.Empty Then
                    toBeDeleted = True
                Else
                    toBeDeleted = False
                End If
			End If

			If toBeDeleted Then
				' Delete all records created in this sample.
				_serviceProxy.Delete("phonecall", _phoneCallId)
				_serviceProxy.Delete("phonecall", _phoneCall2Id)
				_serviceProxy.Delete("goal", _goalId)
				_serviceProxy.Delete("goalrollupquery", _rollupQueryIds(1))
				_serviceProxy.Delete("goalrollupquery", _rollupQueryIds(0))
				_serviceProxy.Delete("account", _accountId)
				_serviceProxy.Delete("rollupfield", _actualId)
				_serviceProxy.Delete("rollupfield", _inprogressId)
				_serviceProxy.Delete("metric", _metricId)

				Console.WriteLine("Entity record(s) have been deleted.")
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
                Dim config As ServerConnection.Configuration = _
                    serverConnect.GetServerConfiguration()

				Dim app As New OverrideGoalTotalCount()
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
'</snippetOverrideGoalTotalCount>