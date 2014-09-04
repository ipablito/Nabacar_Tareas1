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

'<snippetRollupAllGoalsForFiscalPeriodAndStretchedTargetRevenue>
Imports System.ServiceModel

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Query
Imports Microsoft.Xrm.Sdk.Client

' This namespace is found in Microsoft.Crm.Sdk.Proxy.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Crm.Sdk.Messages

Namespace Microsoft.Crm.Sdk.Samples
    ''' <summary>
    ''' Demonstrates how to work with fiscal period goals that utilize stretched
    ''' targets.
    ''' </summary>
    ''' <remarks>
    ''' At run-time, you will be given the option to delete all the
    ''' database records created by this program.</remarks>
    Public Class RollupAllGoalsForFiscalPeriodAndStretchedTargetRevenue
#Region "Class Level Members"

        Private _salesManagerId As Guid
        Private _accountId As Guid
        Private _phoneCallId As Guid
        Private _phoneCall2Id As Guid
        Private _metricId As Guid
        Private _actualId As Guid
        Private _parentGoalId As Guid
        Private _firstChildGoalId As Guid
        Private _secondChildGoalId As Guid
        Private _rollupQueryIds As New List(Of Guid)()
        Private _salesRepresentativeIds As New List(Of Guid)()

        Private _serviceProxy As OrganizationServiceProxy

#End Region ' Class Level Members

#Region "How To Sample Code"
        ''' <summary>
        ''' This method first connects to the Organization service. Afterwards, the
        ''' sample creates a goal and child goals for a particular fiscal period. 
        ''' Stretched targets are tracked as well.
        ''' </summary>
        ''' <param name="serverConfig">Contains server connection information.</param>
        ''' <param name="promptforDelete">When True, the user will be prompted to delete all
        ''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                       ByVal promptforDelete As Boolean)
            Try
                '<snippetRollupAllGoalsForFiscalPeriodAndStretchedTargetRevenue1>
                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy will be properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    ' This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes()

                    CreateRequiredRecords()

                    '					#Region "Create goal metric"

                    ' Create the metric, setting the Metric Type to 'Count' and enabling
                    ' stretch tracking.
                    Dim metric As New Metric() With
                        {
                            .Name = "Sample Count Metric",
                            .IsAmount = False,
                            .IsStretchTracked = True
                        }
                    _metricId = _serviceProxy.Create(metric)
                    metric.Id = _metricId

                    Console.Write("Created count metric, ")

                    '					#End Region

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
                            .MetricId = metric.ToEntityReference()
                        }
                    _actualId = _serviceProxy.Create(actual)

                    Console.Write("created completed phone call RollupField, ")

                    '					#End Region

                    '					#Region "Create the goal rollup queries"

                    ' Note: Formatting the FetchXml onto multiple lines in the following 
                    ' rollup queries causes the length property to be greater than 1,000
                    ' chars and will cause an exception.

                    ' The following query locates closed incoming phone calls.
                    Dim goalRollupQuery As New GoalRollupQuery() With
                        {
                            .Name = "Example Goal Rollup Query",
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
                            & "         operator='eq' value='0'/>" _
                            & "         <condition attribute='statecode' " _
                            & "         operator='eq' value='1' />" _
                            & "     </filter>" _
                            & " </entity>" _
                            & "</fetch>"
                        }
                    _rollupQueryIds.Add(_serviceProxy.Create(goalRollupQuery))
                    goalRollupQuery.Id = _rollupQueryIds(0)

                    ' The following query locates closed outgoing phone calls.
                    Dim goalRollupQuery2 As New GoalRollupQuery() With
                        {
                            .Name = "Example Goal Rollup Query",
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
                            & "                     operator='eq' value='1'/>" _
                            & "         <condition attribute='statecode' " _
                            & "                     operator='eq' value='1' />" _
                            & "     </filter>" _
                            & " </entity>" _
                            & "</fetch>"
                        }
                    _rollupQueryIds.Add(_serviceProxy.Create(goalRollupQuery2))
                    goalRollupQuery2.Id = _rollupQueryIds(1)

                    Console.Write("created rollup queries for phone calls." & vbLf)
                    Console.WriteLine()

                    '					#End Region

                    '					#Region "Create goals"

                    ' Determine current fiscal period and year.
                    ' Note: This sample assumes quarterly fiscal periods.
                    Dim [date] As Date = Date.Now
                    Dim quarterNumber As Integer = (Month([date]) - 1) \ 3 + 1
                    Dim yearNumber As Integer = [date].Year

                    ' Create three goals: one parent goal and two child goals.
                    Dim parentGoal As New Goal() With
                        {
                            .Title = "Parent Goal Example",
                            .RollupOnlyFromChildGoals = True,
                            .ConsiderOnlyGoalOwnersRecords = True,
                            .TargetInteger = 8,
                            .StretchTargetInteger = 10,
                            .IsFiscalPeriodGoal = True,
                            .FiscalPeriod = New OptionSetValue(quarterNumber),
                            .FiscalYear = New OptionSetValue(yearNumber),
                            .MetricId = metric.ToEntityReference(),
                            .GoalOwnerId = New EntityReference With
                                           {
                                               .Id = _salesManagerId,
                                               .LogicalName =
                                               SystemUser.EntityLogicalName
                                           },
                            .OwnerId = New EntityReference With
                                       {
                                           .Id = _salesManagerId,
                                           .LogicalName = SystemUser.EntityLogicalName
                                       }
                        }
                    _parentGoalId = _serviceProxy.Create(parentGoal)
                    parentGoal.Id = _parentGoalId

                    Console.WriteLine("Created parent goal")
                    Console.WriteLine("-------------------")
                    Console.WriteLine("Target: {0}", parentGoal.TargetInteger.Value)
                    Console.WriteLine("Stretch Target: {0}",
                                      parentGoal.StretchTargetInteger.Value)
                    Console.WriteLine("Goal owner: {0}", parentGoal.GoalOwnerId.Id)
                    Console.WriteLine("Goal Fiscal Period: {0}",
                                      parentGoal.FiscalPeriod.Value)
                    Console.WriteLine("Goal Fiscal Year: {0}",
                                      parentGoal.FiscalYear.Value)
                    Console.WriteLine("<End of Listing>")
                    Console.WriteLine()

                    Dim firstChildGoal As New Goal() With
                        {
                            .Title = "First Child Goal Example",
                            .ConsiderOnlyGoalOwnersRecords = True,
                            .TargetInteger = 5,
                            .StretchTargetInteger = 6,
                            .IsFiscalPeriodGoal = True,
                            .FiscalPeriod = New OptionSetValue(quarterNumber),
                            .FiscalYear = New OptionSetValue(yearNumber),
                            .MetricId = metric.ToEntityReference(),
                            .ParentGoalId = parentGoal.ToEntityReference(),
                            .GoalOwnerId = New EntityReference With
                                           {
                                               .Id = _salesRepresentativeIds(0),
                                               .LogicalName =
                                               SystemUser.EntityLogicalName
                                           },
                            .OwnerId = New EntityReference With
                                       {
                                           .Id = _salesManagerId,
                                           .LogicalName = SystemUser.EntityLogicalName
                                       },
                            .RollupQueryActualIntegerId =
                            goalRollupQuery.ToEntityReference()
                        }
                    _firstChildGoalId = _serviceProxy.Create(firstChildGoal)

                    Console.WriteLine("First child goal")
                    Console.WriteLine("----------------")
                    Console.WriteLine("Target: {0}", firstChildGoal.TargetInteger.Value)
                    Console.WriteLine("Stretch Target: {0}",
                                      firstChildGoal.StretchTargetInteger.Value)
                    Console.WriteLine("Goal owner: {0}", firstChildGoal.GoalOwnerId.Id)
                    Console.WriteLine("Goal Fiscal Period: {0}",
                                      firstChildGoal.FiscalPeriod.Value)
                    Console.WriteLine("Goal Fiscal Year: {0}",
                                      firstChildGoal.FiscalYear.Value)
                    Console.WriteLine()

                    Dim secondChildGoal As New Goal() With
                        {
                            .Title = "Second Child Goal Example",
                            .ConsiderOnlyGoalOwnersRecords = True,
                            .TargetInteger = 3,
                            .StretchTargetInteger = 4,
                            .IsFiscalPeriodGoal = True,
                            .FiscalPeriod = New OptionSetValue(quarterNumber),
                            .FiscalYear = New OptionSetValue(yearNumber),
                            .MetricId = metric.ToEntityReference(),
                            .ParentGoalId = parentGoal.ToEntityReference(),
                            .GoalOwnerId = New EntityReference With
                                           {
                                               .Id = _salesRepresentativeIds(1),
                                               .LogicalName = SystemUser.EntityLogicalName
                                           },
                            .OwnerId = New EntityReference With
                                       {
                                           .Id = _salesManagerId,
                                           .LogicalName = SystemUser.EntityLogicalName
                                       },
                            .RollupQueryActualIntegerId =
                            goalRollupQuery2.ToEntityReference()
                        }
                    _secondChildGoalId = _serviceProxy.Create(secondChildGoal)

                    Console.WriteLine("Second child goal")
                    Console.WriteLine("-----------------")
                    Console.WriteLine("Target: {0}", secondChildGoal.TargetInteger.Value)
                    Console.WriteLine("Stretch Target: {0}",
                                      secondChildGoal.StretchTargetInteger.Value)
                    Console.WriteLine("Goal owner: {0}", secondChildGoal.GoalOwnerId.Id)
                    Console.WriteLine("Goal Fiscal Period: {0}",
                                      secondChildGoal.FiscalPeriod.Value)
                    Console.WriteLine("Goal Fiscal Year: {0}",
                                      secondChildGoal.FiscalYear.Value)
                    Console.WriteLine()

                    '					#End Region

                    '					#Region "Calculate rollup and display result"

                    ' Calculate roll-up of goals.
                    Dim recalculateRequest As New RecalculateRequest() With
                        {
                            .Target = New EntityReference(Goal.EntityLogicalName,
                                                          _parentGoalId)
                        }
                    _serviceProxy.Execute(recalculateRequest)

                    Console.WriteLine("Calculated roll-up of goals.")

                    ' Retrieve and report 3 different computed values for the goals
                    ' - Percentage
                    ' - ComputedTargetAsOfTodayPercentageAchieved
                    ' - ComputedTargetAsOfTodayInteger
                    Dim retrieveValues As New QueryExpression() With
                        {
                            .EntityName = Goal.EntityLogicalName,
                            .ColumnSet = New ColumnSet(
                                "title",
                                "percentage",
                                "computedtargetasoftodaypercentageachieved",
                                "computedtargetasoftodayinteger")
                        }
                    Dim ec As EntityCollection = _serviceProxy.RetrieveMultiple(
                        retrieveValues)

                    ' Compute and display the results
                    For i As Integer = 0 To ec.Entities.Count - 1
                        Dim temp As Goal = CType(ec.Entities(i), Goal)
                        Console.WriteLine("Roll-up details for goal: {0}", temp.Title)
                        Console.WriteLine("---------------")
                        Console.WriteLine("Percentage: {0}", temp.Percentage)
                        Console.WriteLine("ComputedTargetAsOfTodayPercentageAchieved: {0}",
                                          temp.ComputedTargetAsOfTodayPercentageAchieved)
                        Console.WriteLine("ComputedTargetAsOfTodayInteger: {0}",
                                          temp.ComputedTargetAsOfTodayInteger.Value)
                        Console.WriteLine("<End of Listing>")
                    Next i

                    '					#End Region

                    DeleteRequiredRecords(promptforDelete)
                End Using
                '</snippetRollupAllGoalsForFiscalPeriodAndStretchedTargetRevenue1>

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

            ' Retrieve the ldapPath
            Dim ldapPath As String = String.Empty
            ' Retrieve the sales team - 1 sales manager and 2 sales representatives.
            _salesManagerId = SystemUserProvider.RetrieveSalesManager(_serviceProxy,
                                                                      ldapPath)
            _salesRepresentativeIds = SystemUserProvider.RetrieveSalespersons(
                _serviceProxy, ldapPath)

            '			#End Region

            '			#Region "Create PhoneCall record and supporting account"

            Dim account As Account = New Account With
                                     {
                                         .Name = "Margie's Travel",
                                         .Address1_PostalCode = "99999"
                                     }
            _accountId = (_serviceProxy.Create(account))
            account.Id = _accountId

            ' Create Guids for PhoneCalls
            _phoneCallId = Guid.NewGuid()
            _phoneCall2Id = Guid.NewGuid()

            ' Create ActivityPartys for the phone calls' "From" field.
            Dim activityParty As New ActivityParty() With
                {
                    .PartyId = account.ToEntityReference(),
                    .ActivityId = New EntityReference With
                                  {
                                      .Id = _phoneCallId,
                                      .LogicalName = PhoneCall.EntityLogicalName
                                  },
                    .ParticipationTypeMask = New OptionSetValue(9)
                }

            Dim activityPartyClosed As New ActivityParty() With
                {
                    .PartyId = account.ToEntityReference(),
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
                    .To = New ActivityParty() {activityParty},
                    .OwnerId = New EntityReference("systemuser",
                                                   _salesRepresentativeIds(0)),
                    .ActualEnd = Date.Now
                }
            _serviceProxy.Create(phoneCall_renamed)

            ' Close the first phone call.
            Dim closePhoneCall As New SetStateRequest() With
                {
                    .EntityMoniker = phoneCall_renamed.ToEntityReference(),
                    .State = New OptionSetValue(1),
                    .Status = New OptionSetValue(4)
                }
            _serviceProxy.Execute(closePhoneCall)

            ' Create a second phone call. 
            phoneCall_renamed = New PhoneCall() With
                                {
                                    .Id = _phoneCall2Id,
                                    .Subject = "Sample Phone Call 2",
                                    .DirectionCode = True,
                                    .To = New ActivityParty() {activityParty},
                                    .OwnerId =
                                    New EntityReference("systemuser",
                                                        _salesRepresentativeIds(1)),
                                    .ActualEnd = Date.Now}
            _serviceProxy.Create(phoneCall_renamed)

            ' Close the second phone call.
            closePhoneCall = New SetStateRequest() With
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
                _serviceProxy.Delete("goal", _firstChildGoalId)
                _serviceProxy.Delete("goal", _secondChildGoalId)
                _serviceProxy.Delete("goal", _parentGoalId)
                _serviceProxy.Delete("goalrollupquery", _rollupQueryIds(0))
                _serviceProxy.Delete("goalrollupquery", _rollupQueryIds(1))
                _serviceProxy.Delete("account", _accountId)
                _serviceProxy.Delete("phonecall", _phoneCallId)
                _serviceProxy.Delete("phonecall", _phoneCall2Id)
                _serviceProxy.Delete("rollupfield", _actualId)
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
                Dim config As ServerConnection.Configuration =
                    serverConnect.GetServerConfiguration()

                Dim app As New RollupAllGoalsForFiscalPeriodAndStretchedTargetRevenue()
                app.Run(config, True)
            Catch ex As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
                Console.WriteLine("The application terminated with an error.")
                Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp)
                Console.WriteLine("Code: {0}", ex.Detail.ErrorCode)
                Console.WriteLine("Message: {0}", ex.Detail.Message)
                Console.WriteLine("Plugin Trace: {0}", ex.Detail.TraceText)
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
'</snippetRollupAllGoalsForFiscalPeriodAndStretchedTargetRevenue>