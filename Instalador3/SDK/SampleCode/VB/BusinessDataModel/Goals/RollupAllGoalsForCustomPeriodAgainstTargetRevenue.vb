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

'<snippetRollupAllGoalsForCustomPeriodAgainstTargetRevenue>
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
	''' Demonstrates how to work with Goals and related entities.</summary>
	''' <remarks>
	''' At run-time, you will be given the option to delete all the
	''' database records created by this program.</remarks>
	Public Class RollupAllGoalsForCustomPeriodAgainstTargetRevenue
		#Region "Class Level Members"

		Private _salesManagerId As Guid
		Private _unitGroupId As Guid
		Private _defaultUnitId As Guid
		Private _product1Id As Guid
		Private _product2Id As Guid
		Private _discountTypeId As Guid
		Private _discountId As Guid
		Private _priceListId As Guid
		Private _priceListItem1Id As Guid
		Private _priceListItem2Id As Guid
		Private _catalogProductId As Guid
		Private _catalogProductPriceOverrideId As Guid
		Private _writeInProductId As Guid
		Private _metricId As Guid
		Private _inProgressId As Guid
		Private _actualId As Guid
		Private _parentGoalId As Guid
		Private _firstChildGoalId As Guid
		Private _secondChildGoalId As Guid
		Private _accountIds As New List(Of Guid)()
		Private _rollupQueryIds As New List(Of Guid)()
		Private _salesRepresentativeIds As New List(Of Guid)()
		Private _opportunityIds As New List(Of Guid)()

		Private _serviceProxy As OrganizationServiceProxy

		#End Region ' Class Level Members

		#Region "How To Sample Code"
		''' <summary>
		''' This method first connects to the Organization service. Afterwards,
		''' several actions on Goal records are executed.
		''' </summary>
		''' <param name="serverConfig">Contains server connection information.</param>
		''' <param name="promptforDelete">When True, the user will be prompted to delete all
		''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                       ByVal promptforDelete As Boolean)
            Try
                '<snippetRollupAllGoalsForCustomPeriodAgainstTargetRevenue1>
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

                    Console.Write("Created revenue metric, ")

                    ' Create first RollupField which targets the estimated values.
                    Dim inProgress As New RollupField() With
                        {
                            .SourceEntity = Opportunity.EntityLogicalName,
                            .SourceAttribute = "estimatedvalue",
                            .GoalAttribute = "inprogressmoney",
                            .SourceState = 0,
                            .EntityForDateAttribute = Opportunity.EntityLogicalName,
                            .DateAttribute = "estimatedclosedate",
                            .MetricId = New EntityReference(Metric.EntityLogicalName,
                                                            _metricId)
                        }
                    _inProgressId = _serviceProxy.Create(inProgress)

                    Console.Write("created in-progress RollupField, ")

                    ' Create second RollupField which targets the actual values.
                    Dim actual As New RollupField() With
                        {
                            .SourceEntity = Opportunity.EntityLogicalName,
                            .SourceAttribute = "actualvalue",
                            .GoalAttribute = "actualmoney",
                            .SourceState = 1,
                            .EntityForDateAttribute = Opportunity.EntityLogicalName,
                            .DateAttribute = "actualclosedate",
                            .MetricId = New EntityReference(Metric.EntityLogicalName,
                                                            _metricId)
                        }
                    _actualId = _serviceProxy.Create(actual)

                    Console.Write("created actual revenue RollupField, ")

                    ' Create the goal rollup queries.
                    ' Note: Formatting the FetchXml onto multiple lines in the following 
                    ' rollup queries causes the lenth property to be greater than 1,000
                    ' chars and will cause an exception.

                    ' The first query locates opportunities in the first sales 
                    ' representative's area (zip code: 60661).
                    Dim goalRollupQuery As _
                        New GoalRollupQuery() With
                        {
                            .Name = "First Example Goal Rollup Query",
                            .QueryEntityType = Opportunity.EntityLogicalName,
                            .FetchXml = _
                            "<fetch version=""1.0"" output-format=""xml-platform"" " _
                            & "     mapping=""logical"" distinct=""false"">" _
                            & " <entity name=""opportunity"">" _
                            & "     <attribute name=""totalamount""/>" _
                            & "     <attribute name=""name""/>" _
                            & "     <attribute name=""customerid""/>" _
                            & "     <attribute name=""estimatedvalue""/>" _
                            & "     <attribute name=""statuscode""/>" _
                            & "     <attribute name=""opportunityid""/>" _
                            & "     <order attribute=""name"" descending=""false""/>" _
                            & "     <link-entity name=""account"" from=""accountid"" " _
                            & "                 to=""customerid"" alias=""aa"">" _
                            & "         <filter type=""and"">" _
                            & "             <condition " _
                            & "                     attribute=""address1_postalcode"" " _
                            & "                     operator=""eq"" value=""60661""/>" _
                            & "         </filter>" _
                            & "     </link-entity>" _
                            & " </entity>" _
                            & "</fetch>"
                        }
                    _rollupQueryIds.Add(_serviceProxy.Create(goalRollupQuery))

                    Console.Write("created first rollup query for zip code 60661, ")

                    ' The second query locates opportunities in the second sales 
                    ' representative's area (zip code: 99999).
                    goalRollupQuery =
                        New GoalRollupQuery() With
                        {
                            .Name = "Second Example Goal Rollup Query",
                            .QueryEntityType = Opportunity.EntityLogicalName,
                            .FetchXml = _
                            "<fetch version=""1.0"" output-format=""xml-platform"" " _
                            & "     mapping=""logical"" distinct=""false"">" _
                            & " <entity name=""opportunity"">" _
                            & "     <attribute name=""totalamount""/>" _
                            & "     <attribute name=""customerid""/>" _
                            & "     <attribute name=""estimatedvalue""/>" _
                            & "     <attribute name=""statuscode""/>" _
                            & "     <attribute name=""opportunityid""/>" _
                            & "     <order attribute=""name"" descending=""false""/>" _
                            & "     <link-entity name=""account"" from=""accountid"" " _
                            & "                  to=""customerid"" alias=""aa"">" _
                            & "         <filter type=""and"">" _
                            & "             <condition " _
                            & "                     attribute=""address1_postalcode"" " _
                            & "                     operator=""eq"" value=""99999""/>" _
                            & "         </filter>" _
                            & "     </link-entity>" _
                            & " </entity>" _
                            & "</fetch>"
                        }
                    _rollupQueryIds.Add(_serviceProxy.Create(goalRollupQuery))

                    Console.WriteLine("created second rollup query for zip code 99999.")
                    Console.WriteLine()

                    ' Create three goals: one parent goal and two child goals.
                    Dim parentGoal As New Goal() With
                        {
                            .Title = "Parent Goal Example",
                            .RollupOnlyFromChildGoals = True,
                            .ConsiderOnlyGoalOwnersRecords = True,
                            .TargetMoney = New Money(300D),
                            .IsFiscalPeriodGoal = False,
                            .MetricId = New EntityReference With
                                        {
                                            .Id = _metricId,
                                            .LogicalName = Metric.EntityLogicalName
                                        },
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

                    Console.WriteLine("Created parent goal")
                    Console.WriteLine("-------------------")
                    Console.WriteLine("Target: {0}", parentGoal.TargetMoney.Value)
                    Console.WriteLine("Goal owner: {0}", parentGoal.GoalOwnerId.Id)
                    Console.WriteLine("Goal Start Date: {0}", parentGoal.GoalStartDate)
                    Console.WriteLine("Goal End Date: {0}", parentGoal.GoalEndDate)
                    Console.WriteLine("<End of Listing>")
                    Console.WriteLine()

                    Dim firstChildGoal As New Goal() With
                        {
                            .Title = "First Child Goal Example",
                            .ConsiderOnlyGoalOwnersRecords = True,
                            .TargetMoney = New Money(100D),
                            .IsFiscalPeriodGoal = False,
                            .MetricId = New EntityReference With
                                        {
                                            .Id = _metricId,
                                            .LogicalName = Metric.EntityLogicalName
                                        },
                            .ParentGoalId = New EntityReference With
                                            {
                                                .Id = _parentGoalId,
                                                .LogicalName = Goal.EntityLogicalName
                                            },
                            .GoalOwnerId = New EntityReference With
                                           {
                                               .Id = _salesRepresentativeIds(0),
                                               .LogicalName = SystemUser.EntityLogicalName
                                           },
                            .OwnerId = New EntityReference With
                                       {
                                           .Id = _salesManagerId,
                                           .LogicalName = SystemUser.EntityLogicalName
                                       },
                            .RollUpQueryActualMoneyId =
                            New EntityReference With
                                {
                                    .Id = _rollupQueryIds(0),
                                    .LogicalName = goalRollupQuery.EntityLogicalName
                                },
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
                    Console.WriteLine()

                    Dim secondChildGoal As New Goal() With
                        {
                            .Title = "Second Child Goal Example",
                            .ConsiderOnlyGoalOwnersRecords = True,
                            .TargetMoney = New Money(200D),
                            .IsFiscalPeriodGoal = False,
                            .MetricId = New EntityReference With
                                        {
                                            .Id = _metricId,
                                            .LogicalName = Metric.EntityLogicalName
                                        },
                            .ParentGoalId = New EntityReference With
                                            {
                                                .Id = _parentGoalId,
                                                .LogicalName = Goal.EntityLogicalName
                                            },
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
                            .RollUpQueryActualMoneyId =
                            New EntityReference With
                            {
                                .Id = _rollupQueryIds(1),
                                .LogicalName = goalRollupQuery.EntityLogicalName
                            },
                            .GoalStartDate = Date.Today.AddDays(-1),
                            .GoalEndDate = Date.Today.AddDays(30)
                        }
                    _secondChildGoalId = _serviceProxy.Create(secondChildGoal)

                    Console.WriteLine("Second child goal")
                    Console.WriteLine("-----------------")
                    Console.WriteLine("Target: {0}", secondChildGoal.TargetMoney.Value)
                    Console.WriteLine("Goal owner: {0}", secondChildGoal.GoalOwnerId.Id)
                    Console.WriteLine("Goal Start Date: {0}", secondChildGoal.GoalStartDate)
                    Console.WriteLine("Goal End Date: {0}", secondChildGoal.GoalEndDate)
                    Console.WriteLine()

                    ' <snippetRecalculate1>
                    ' Calculate roll-up of goals.
                    Dim recalculateRequest As New RecalculateRequest() With
                        {
                            .Target = New EntityReference(Goal.EntityLogicalName,
                                                          _parentGoalId)
                        }
                    _serviceProxy.Execute(recalculateRequest)

                    '</snippetRecalculate1> 
                    Console.WriteLine("Calculated roll-up of goals.")

                    ' Retrieve and report 3 different computed values for the goals
                    ' - Percentage
                    ' - ComputedTargetAsOfTodayPercentageAchieved
                    ' - ComputedTargetAsOfTodayMoney
                    Dim retrieveValues As New QueryExpression() With
                        {
                            .EntityName = Goal.EntityLogicalName,
                            .ColumnSet =
                            New ColumnSet("title",
                                          "percentage",
                                          "computedtargetasoftodaypercentageachieved",
                                          "computedtargetasoftodaymoney")}
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
                        Console.WriteLine("ComputedTargetAsOfTodayMoney: {0}",
                                          temp.ComputedTargetAsOfTodayMoney.Value)
                        Console.WriteLine("<End of Listing>")
                    Next i

                    DeleteRequiredRecords(promptforDelete)
                End Using
                '</snippetRollupAllGoalsForCustomPeriodAgainstTargetRevenue1>

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
                _serviceProxy,
                ldapPath)

'			#End Region

'			#Region "Create records to support Opportunity records"
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
                        .ColumnSet = New ColumnSet("uomid",
                                                   "name"),
                        .PageInfo = New PagingInfo With
                                    {
                                        .PageNumber = 1,
                                        .Count = 1}
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
            Dim newProduct1 As Product =
                New Product With
                {
                    .ProductNumber = "1",
                    .Name = "Example Product 1",
                    .QuantityDecimal = 2,
                    .DefaultUoMScheduleId =
                    New EntityReference(UoMSchedule.EntityLogicalName,
                                        _unitGroupId),
                    .DefaultUoMId = New EntityReference(UoM.EntityLogicalName,
                                                        _defaultUnitId)
                }
			_product1Id = _serviceProxy.Create(newProduct1)

            Dim newProduct2 As Product =
                New Product With
                {
                    .ProductNumber = "2",
                    .Name = "Example Product 2",
                    .QuantityDecimal = 3,
                    .DefaultUoMScheduleId =
                    New EntityReference(UoMSchedule.EntityLogicalName,
                                        _unitGroupId),
                    .DefaultUoMId = New EntityReference(UoM.EntityLogicalName,
                                                        _defaultUnitId)
                }
			_product2Id = _serviceProxy.Create(newProduct2)

			' Create a new discount list
            Dim newDiscountType As DiscountType =
                New DiscountType With
                {
                    .Name = "Example Discount List",
                    .IsAmountType = False
                }
			_discountTypeId = _serviceProxy.Create(newDiscountType)

			' Create a new discount
            Dim newDiscount As Discount =
                New Discount With
                {
                    .DiscountTypeId = New EntityReference(DiscountType.EntityLogicalName,
                                                          _discountTypeId),
                    .LowQuantity = 5,
                    .HighQuantity = 10,
                    .Percentage = 3
                }
			_discountId = _serviceProxy.Create(newDiscount)

			' Create a price list
            Dim newPriceList As PriceLevel = New PriceLevel With
                                             {
                                                 .Name = "Example Price List"
                                             }
			_priceListId = _serviceProxy.Create(newPriceList)

			' Create a price list item for the first product and apply volume discount
            Dim newPriceListItem1 As ProductPriceLevel =
                New ProductPriceLevel With
                {
                    .PriceLevelId = New EntityReference(PriceLevel.EntityLogicalName,
                                                        _priceListId),
                    .ProductId = New EntityReference(Product.EntityLogicalName,
                                                     _product1Id),
                    .UoMId = New EntityReference(UoM.EntityLogicalName,
                                                 _defaultUnitId),
                    .Amount = New Money(20),
                    .DiscountTypeId = New EntityReference(DiscountType.EntityLogicalName,
                                                          _discountTypeId)
                }
			_priceListItem1Id = _serviceProxy.Create(newPriceListItem1)

			' Create a price list item for the second product
            Dim newPriceListItem2 As ProductPriceLevel =
                New ProductPriceLevel With
                {
                    .PriceLevelId = New EntityReference(PriceLevel.EntityLogicalName,
                                                        _priceListId),
                    .ProductId = New EntityReference(Product.EntityLogicalName,
                                                     _product2Id),
                    .UoMId = New EntityReference(UoM.EntityLogicalName, _defaultUnitId),
                    .Amount = New Money(15)
                }
			_priceListItem2Id = _serviceProxy.Create(newPriceListItem2)

			' Create an account record for the opportunity's potential customerid 
            Dim newAccount As Account = New Account With
                                        {
                                            .Name = "Litware, Inc.",
                                            .Address1_PostalCode = "60661"
                                        }
			_accountIds.Add(_serviceProxy.Create(newAccount))

            newAccount = New Account With
                         {
                             .Name = "Margie's Travel",
                             .Address1_PostalCode = "99999"
                         }
			_accountIds.Add(_serviceProxy.Create(newAccount))

'			#End Region ' Create records to support Opportunity records

'			#Region "Create Opportunity records"
			' Create a new opportunity with user specified estimated revenue
            Dim newOpportunity As Opportunity =
                New Opportunity With
                {
                    .Name = "Example Opportunity",
                    .CustomerId = New EntityReference(Account.EntityLogicalName,
                                                      _accountIds(0)),
                    .PriceLevelId = New EntityReference(PriceLevel.EntityLogicalName,
                                                        _priceListId),
                    .IsRevenueSystemCalculated = False,
                    .EstimatedValue = New Money(400D),
                    .FreightAmount = New Money(10D),
                    .DiscountAmount = New Money(0.1D),
                    .DiscountPercentage = 0.2D,
                    .ActualValue = New Money(400D),
                    .OwnerId = New EntityReference With
                               {
                                   .Id = _salesRepresentativeIds(0),
                                   .LogicalName = SystemUser.EntityLogicalName
                               }
                }
			_opportunityIds.Add(_serviceProxy.Create(newOpportunity))

            Dim secondOpportunity As Opportunity =
                New Opportunity With
                {
                    .Name = "Example Opportunity 2",
                    .CustomerId = New EntityReference(Account.EntityLogicalName,
                                                      _accountIds(1)),
                    .PriceLevelId = New EntityReference(PriceLevel.EntityLogicalName,
                                                        _priceListId),
                    .IsRevenueSystemCalculated = False,
                    .EstimatedValue = New Money(400D),
                    .FreightAmount = New Money(10D),
                    .DiscountAmount = New Money(0.1D),
                    .DiscountPercentage = 0.2D,
                    .ActualValue = New Money(400D),
                    .OwnerId = New EntityReference With
                               {
                                   .Id = _salesRepresentativeIds(1),
                                   .LogicalName = SystemUser.EntityLogicalName
                               }
                }
			_opportunityIds.Add(_serviceProxy.Create(secondOpportunity))

			' Create a catalog product
            Dim catalogProduct As OpportunityProduct =
                New OpportunityProduct With
                {
                    .OpportunityId = New EntityReference(Opportunity.EntityLogicalName,
                                                         _opportunityIds(0)),
                    .ProductId = New EntityReference(Product.EntityLogicalName,
                                                     _product1Id),
                    .UoMId = New EntityReference(UoM.EntityLogicalName, _defaultUnitId),
                    .Quantity = 8,
                    .Tax = New Money(12.42D)
                }
			_catalogProductId = _serviceProxy.Create(catalogProduct)

			' Create another catalog product and override the list price
            Dim catalogProductPriceOverride As OpportunityProduct =
                New OpportunityProduct With
                {
                    .OpportunityId = New EntityReference(Opportunity.EntityLogicalName,
                                                         _opportunityIds(1)),
                    .ProductId = New EntityReference(Product.EntityLogicalName,
                                                     _product2Id),
                    .UoMId = New EntityReference(UoM.EntityLogicalName, _defaultUnitId),
                    .Quantity = 3, .Tax = New Money(2.88D),
                    .IsPriceOverridden = True,
                    .PricePerUnit = New Money(12)
                }
			_catalogProductPriceOverrideId = _serviceProxy.Create(catalogProductPriceOverride)

			' create a new write-in opportunity product with a manual discount applied
            Dim writeInProduct As OpportunityProduct =
                New OpportunityProduct With
                {
                    .OpportunityId = New EntityReference(Opportunity.EntityLogicalName,
                                                         _opportunityIds(1)),
                    .IsProductOverridden = True,
                    .ProductDescription = "Example Write-in Product",
                    .PricePerUnit = New Money(20D),
                    .Quantity = 5,
                    .ManualDiscountAmount = New Money(10.5D),
                    .Tax = New Money(7.16D)
                }
			_writeInProductId = _serviceProxy.Create(writeInProduct)

			' Close the opportunities as 'Won'
            Dim winRequest As New WinOpportunityRequest() With
                {
                    .OpportunityClose =
                    New OpportunityClose() With
                    {
                        .OpportunityId =
                        New EntityReference With
                        {
                            .Id = _opportunityIds(0),
                            .LogicalName = Opportunity.EntityLogicalName
                        },
                        .ActualRevenue = New Money(400D),
                        .ActualEnd = Date.Today
                    },
                    .Status = New OptionSetValue(3)
                }
			_serviceProxy.Execute(winRequest)

            winRequest = New WinOpportunityRequest() With
                         {
                             .OpportunityClose =
                             New OpportunityClose() With
                             {
                                 .OpportunityId =
                                 New EntityReference With
                                 {
                                     .Id = _opportunityIds(1),
                                     .LogicalName = Opportunity.EntityLogicalName
                                 },
                                 .ActualRevenue = New Money(400D),
                                 .ActualEnd = Date.Today
                             },
                             .Status = New OptionSetValue(3)
                         }
			_serviceProxy.Execute(winRequest)

'			#End Region ' Create Opportunity records
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
				_serviceProxy.Delete("opportunityproduct", _writeInProductId)
				_serviceProxy.Delete("opportunityproduct", _catalogProductPriceOverrideId)
				_serviceProxy.Delete("opportunityproduct", _catalogProductId)
				_serviceProxy.Delete("opportunity", _opportunityIds(0))
				_serviceProxy.Delete("opportunity", _opportunityIds(1))
				_serviceProxy.Delete("account", _accountIds(0))
				_serviceProxy.Delete("account", _accountIds(1))
				_serviceProxy.Delete("productpricelevel", _priceListItem1Id)
				_serviceProxy.Delete("productpricelevel", _priceListItem2Id)
				_serviceProxy.Delete("pricelevel", _priceListId)
				_serviceProxy.Delete("product", _product1Id)
				_serviceProxy.Delete("product", _product2Id)
				_serviceProxy.Delete("discount", _discountId)
				_serviceProxy.Delete("discounttype", _discountTypeId)
				_serviceProxy.Delete("uomschedule", _unitGroupId)
				_serviceProxy.Delete("rollupfield", _inProgressId)
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
				Dim config As ServerConnection.Configuration = serverConnect.GetServerConfiguration()

				Dim app As New RollupAllGoalsForCustomPeriodAgainstTargetRevenue()
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
'</snippetRollupAllGoalsForCustomPeriodAgainstTargetRevenue>