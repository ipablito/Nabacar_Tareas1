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

'<snippetUsingQueriesToTrackGoals>
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
	''' Demonstrates how to use queries to track goals.</summary>
	''' <remarks>
	''' At run-time, you will be given the option to delete all the
	''' database records created by this program.</remarks>
	Public Class UsingQueriesToTrackGoals
		#Region "Class Level Members"

		Private _salesManagerId As Guid
		Private _unitGroupId As Guid
		Private _defaultUnitId As Guid
		Private _productId As Guid
		Private _priceListId As Guid
		Private _priceListItemId As Guid
		Private _orderDetailId As Guid
		Private _metricId As Guid
		Private _actualId As Guid
		Private _parentGoalId As Guid
		Private _firstChildGoalId As Guid
		Private _accountId As Guid
		Private _rollupQueryId As Guid
		Private _salesRepresentativeId As Guid
		Private _orderId As Guid

		Private _serviceProxy As OrganizationServiceProxy

		#End Region ' Class Level Members

		#Region "How To Sample Code"
		''' <summary>
		''' This method first connects to the Organization service. Afterwards, a 
		''' rollup field and rollup query are created. The rollup query is only
		''' associated with the child goal. Then a parent goal and child goal
		''' are created. The goals are both rolled up and their results are displayed. 
		''' </summary>
		''' <param name="serverConfig">Contains server connection information.</param>
		''' <param name="promptforDelete">When True, the user will be prompted to delete all
		''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                       ByVal promptforDelete As Boolean)
            Try
                '<snippetUsingQueriesToTrackGoals1>
                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy will be properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    ' This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes()

                    CreateRequiredRecords()

                    ' Create the revenue metric, setting the Amount Data Type to 'Money'
                    ' and the Metric Type to 'Amount'.
                    Dim sampleMetric As New Metric() With
                        {
                            .Name = "Sample Revenue Metric",
                            .AmountDataType = New OptionSetValue(0),
                            .IsAmount = True
                        }
                    _metricId = _serviceProxy.Create(sampleMetric)
                    sampleMetric.Id = _metricId

                    Console.Write("Created revenue metric, ")

                    '					#Region "Create RollupFields"

                    ' Create RollupField which targets the actual totals.
                    Dim actual As New RollupField() With
                        {
                            .SourceEntity = SalesOrder.EntityLogicalName,
                            .SourceAttribute = "totalamount",
                            .GoalAttribute = "actualmoney",
                            .SourceState = 1,
                            .EntityForDateAttribute = SalesOrder.EntityLogicalName,
                            .DateAttribute = "datefulfilled",
                            .MetricId = sampleMetric.ToEntityReference()
                        }
                    _actualId = _serviceProxy.Create(actual)

                    Console.Write("created actual revenue RollupField, ")

                    '					#End Region

                    '					#Region "Create the goal rollup query"

                    ' The query locates sales orders in the first sales 
                    ' representative's area (zip code: 60661) and with a value
                    ' greater than $1,000.
                    Dim goalRollupQuery As New GoalRollupQuery() With
                        {
                            .Name = "First Example Goal Rollup Query",
                            .QueryEntityType = SalesOrder.EntityLogicalName,
                            .FetchXml = _
                            "<fetch mapping=""logical"" version=""1.0"">" _
                            & " <entity name=""salesorder"">" _
                            & "     <attribute name=""customerid"" />" _
                            & "     <attribute name=""name"" />" _
                            & "     <attribute name=""salesorderid"" />" _
                            & "     <attribute name=""statuscode"" />" _
                            & "     <attribute name=""totalamount"" />" _
                            & "     <order attribute=""name"" />" _
                            & "     <filter>" _
                            & "         <condition attribute=""totalamount"" " _
                            & "                      operator=""gt"" value=""1000"" />" _
                            & "         <condition attribute=""billto_postalcode"" " _
                            & "                      operator=""eq"" value=""60661"" />" _
                            & "     </filter>" _
                            & " </entity>" _
                            & "</fetch>"
                        }
                    _rollupQueryId = _serviceProxy.Create(goalRollupQuery)
                    goalRollupQuery.Id = _rollupQueryId

                    Console.Write("created rollup query.")
                    Console.WriteLine()

                    '					#End Region

                    '					#Region "Create two goals: one parent and one child goal"

                    ' Create the parent goal.
                    Dim parentGoal As New Goal() With
                        {
                            .Title = "Parent Goal Example",
                            .RollupOnlyFromChildGoals = True,
                            .TargetMoney = New Money(1000D),
                            .IsFiscalPeriodGoal = False,
                            .MetricId = sampleMetric.ToEntityReference(),
                            .GoalOwnerId =
                            New EntityReference With
                            {
                                .Id = _salesManagerId,
                                .LogicalName = SystemUser.EntityLogicalName
                            },
                            .OwnerId =
                            New EntityReference With
                            {
                                .Id = _salesManagerId,
                                .LogicalName = SystemUser.EntityLogicalName
                            },
                            .GoalStartDate = Date.Today.AddDays(-1),
                            .GoalEndDate = Date.Today.AddDays(30)
                        }
                    _parentGoalId = _serviceProxy.Create(parentGoal)
                    parentGoal.Id = _parentGoalId

                    Console.WriteLine("Created parent goal")
                    Console.WriteLine("-------------------")
                    Console.WriteLine("Target: {0}", parentGoal.TargetMoney.Value)
                    Console.WriteLine("Goal owner: {0}", parentGoal.GoalOwnerId.Id)
                    Console.WriteLine("Goal Start Date: {0}", parentGoal.GoalStartDate)
                    Console.WriteLine("Goal End Date: {0}", parentGoal.GoalEndDate)
                    Console.WriteLine("<End of Listing>")
                    Console.WriteLine()

                    ' Create the child goal.
                    Dim firstChildGoal As New Goal() With
                        {
                            .Title = "First Child Goal Example",
                            .ConsiderOnlyGoalOwnersRecords = True,
                            .TargetMoney = New Money(1000D),
                            .IsFiscalPeriodGoal = False,
                            .MetricId = sampleMetric.ToEntityReference(),
                            .ParentGoalId = parentGoal.ToEntityReference(),
                            .GoalOwnerId =
                            New EntityReference With
                            {
                                .Id = _salesRepresentativeId,
                                .LogicalName = SystemUser.EntityLogicalName
                            },
                            .OwnerId =
                            New EntityReference With
                            {
                                .Id = _salesManagerId,
                                .LogicalName = SystemUser.EntityLogicalName
                            },
                            .RollUpQueryActualMoneyId =
                            goalRollupQuery.ToEntityReference(),
                            .GoalStartDate = Date.Today.AddDays(-1),
                            .GoalEndDate = Date.Today.AddDays(30)
                        }
                    _firstChildGoalId = _serviceProxy.Create(firstChildGoal)

                    Console.WriteLine("First child goal")
                    Console.WriteLine("----------------")
                    Console.WriteLine("Target: {0}", firstChildGoal.TargetMoney.Value)
                    Console.WriteLine("Goal owner: {0}", firstChildGoal.GoalOwnerId.Id)
                    Console.WriteLine("Goal Start Date: {0}", firstChildGoal.GoalStartDate)
                    Console.WriteLine("Goal End Date: {0}", firstChildGoal.GoalEndDate)
                    Console.WriteLine("<End of Listing>")
                    Console.WriteLine()

                    '					#End Region

                    ' Calculate roll-up of goals.
                    ' Note: Recalculate can be run against any goal in the tree to cause
                    ' a rollup of the whole tree.
                    Dim recalculateRequest As New RecalculateRequest() With
                        {
                            .Target = parentGoal.ToEntityReference()
                        }
                    _serviceProxy.Execute(recalculateRequest)

                    Console.WriteLine("Calculated roll-up of goals.")
                    Console.WriteLine()

                    ' Retrieve and report 3 different computed values for the goals
                    ' - Percentage
                    ' - ComputedTargetAsOfTodayPercentageAchieved
                    ' - ComputedTargetAsOfTodayMoney
                    Dim retrieveValues As New QueryExpression() With
                        {
                            .EntityName = Goal.EntityLogicalName,
                            .ColumnSet =
                            New ColumnSet("title",
                                          "computedtargetasoftodaypercentageachieved",
                                          "computedtargetasoftodaymoney")
                        }
                    Dim ec As EntityCollection = _serviceProxy.RetrieveMultiple(
                        retrieveValues)

                    ' Compute and display the results
                    For i As Integer = 0 To ec.Entities.Count - 1
                        Dim temp As Goal = CType(ec.Entities(i), Goal)
                        Console.WriteLine("Roll-up details for goal: {0}", temp.Title)
                        Console.WriteLine("---------------")
                        Console.WriteLine("ComputedTargetAsOfTodayPercentageAchieved: {0}",
                                          temp.ComputedTargetAsOfTodayPercentageAchieved)
                        Console.WriteLine("ComputedTargetAsOfTodayMoney: {0}",
                                          temp.ComputedTargetAsOfTodayMoney.Value)
                        Console.WriteLine("<End of Listing>")
                    Next i

                    DeleteRequiredRecords(promptforDelete)
                End Using
                '</snippetUsingQueriesToTrackGoals1>

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
            _salesRepresentativeId = SystemUserProvider.RetrieveSalespersons(_serviceProxy,
                                                                             ldapPath)(0)

'			#End Region

'			#Region "Create records to support SalesOrder records"
			' Create a unit group
            Dim newUnitGroup As UoMSchedule = New UoMSchedule With
                                              {
                                                  .Name = "Example Unit Group",
                                                  .BaseUoMName = "Example Primary Unit"
                                              }
			_unitGroupId = _serviceProxy.Create(newUnitGroup)

			' Retrieve the default unit id that was automatically created
			' when we created the Unit Group
            Dim unitQuery As QueryExpression =
                New QueryExpression With
                {
                    .EntityName = UoM.EntityLogicalName,
                    .ColumnSet = New ColumnSet("uomid", "name"),
                    .PageInfo = New PagingInfo With
                                {
                                    .PageNumber = 1,
                                    .Count = 1
                                }
                }
            unitQuery.Criteria = New FilterExpression()
            unitQuery.Criteria.AddCondition("uomscheduleid",
                                            ConditionOperator.Equal,
                                            {_unitGroupId})

			' Retrieve the unit.
            Dim unit As UoM =
                CType(_serviceProxy.RetrieveMultiple(unitQuery).Entities(0), 
                    UoM)
			_defaultUnitId = unit.UoMId.Value

			' Create a few products
            Dim newProduct As Product =
                New Product With
                {
                    .ProductNumber = "1",
                    .Name = "Example Product",
                    .QuantityDecimal = 2,
                    .DefaultUoMScheduleId =
                    New EntityReference(UoMSchedule.EntityLogicalName,
                                        _unitGroupId),
                    .DefaultUoMId = New EntityReference(UoM.EntityLogicalName,
                                                        _defaultUnitId)
                }
			_productId = _serviceProxy.Create(newProduct)
			newProduct.Id = _productId

			' Create a price list
            Dim newPriceList As PriceLevel = New PriceLevel With
                                             {
                                                 .Name = "Example Price List"
                                             }
			_priceListId = _serviceProxy.Create(newPriceList)

			' Create a price list item for the first product and apply volume discount
            Dim newPriceListItem As ProductPriceLevel =
                New ProductPriceLevel With
                {
                    .PriceLevelId = New EntityReference(PriceLevel.EntityLogicalName,
                                                        _priceListId),
                    .ProductId = New EntityReference(Product.EntityLogicalName,
                                                     _productId),
                    .UoMId = New EntityReference(UoM.EntityLogicalName,
                                                 _defaultUnitId),
                    .Amount = New Money(20)
                }
			_priceListItemId = _serviceProxy.Create(newPriceListItem)

			' Create an account record for the sales order's potential customerid 
            Dim newAccount As Account = New Account With
                                        {
                                            .Name = "Litware, Inc.",
                                            .Address1_PostalCode = "60661"
                                        }
			_accountId = _serviceProxy.Create(newAccount)
			newAccount.Id = _accountId

'			#End Region ' Create records to support SalesOrder

'			#Region "Create SalesOrder record"

			' Create the sales order.
            Dim order As New SalesOrder() With
                {
                    .Name = "Faux Order",
                    .DateFulfilled = New Date(2010, 8, 1),
                    .PriceLevelId =
                    New EntityReference(PriceLevel.EntityLogicalName,
                                        _priceListId),
                    .CustomerId = New EntityReference(Account.EntityLogicalName,
                                                      _accountId),
                    .FreightAmount = New Money(20D)
                }
			_orderId = _serviceProxy.Create(order)
			order.Id = _orderId

			' Add the product to the order with the price overriden with a
			' negative value.
            Dim orderDetail As New SalesOrderDetail() With
                {
                    .ProductId = newProduct.ToEntityReference(),
                    .Quantity = 4,
                    .SalesOrderId = order.ToEntityReference(),
                    .IsPriceOverridden = True,
                    .PricePerUnit = New Money(1000D),
                    .UoMId = New EntityReference(UoM.EntityLogicalName,
                                                 _defaultUnitId)
                }
			_orderDetailId = _serviceProxy.Create(orderDetail)

'			#End Region ' Create SalesOrder record
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
				_serviceProxy.Delete("goal", _parentGoalId)
				_serviceProxy.Delete("goalrollupquery", _rollupQueryId)
				_serviceProxy.Delete("salesorderdetail", _orderDetailId)
				_serviceProxy.Delete("salesorder", _orderId)
				_serviceProxy.Delete("account", _accountId)
				_serviceProxy.Delete("productpricelevel", _priceListItemId)
				_serviceProxy.Delete("pricelevel", _priceListId)
				_serviceProxy.Delete("product", _productId)
				_serviceProxy.Delete("uomschedule", _unitGroupId)
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

				Dim app As New UsingQueriesToTrackGoals()
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
'</snippetUsingQueriesToTrackGoals>