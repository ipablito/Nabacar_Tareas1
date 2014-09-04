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

'<snippetWorkingWithContracts>
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
    ''' Demonstrates how to do manage contract records programmatically.</summary>
    ''' <remarks>
    ''' At run-time, you will be given the option to delete all the
    ''' database records created by this program.</remarks>
    Public Class WorkingWithContracts
        #Region "Class Level Members"

        Private _accountId As Guid
        Private _contractId As Guid
        Private _contractTemplateId As Guid
        Private _firstCloneId As Guid
        Private _secondCloneId As Guid
        Private _renewedId As Guid
        Private _serviceProxy As OrganizationServiceProxy

        #End Region ' Class Level Members

        #Region "How To Sample Code"
        ''' <summary>
        ''' This method first connects to the Organization service. Afterwards,
        ''' a Contract Template and several Contracts are created, demonstrating how to
        ''' create and work with the Contract entity.
        ''' </summary>
        ''' <param name="serverConfig">Contains server connection information.</param>
        ''' <param name="promptforDelete">When True, the user will be prompted to delete all
        ''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration, ByVal promptforDelete As Boolean)
            Try
                '<snippetWorkingWithContracts1>
                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy will be properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    ' This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes()

                    CreateRequiredRecords()

'                    #Region "Create Contract Template"

                    ' First, attempt to retrieve the Contract Template. Otherwise, 
                    ' create the template.
                    Dim templateQuery As New QueryExpression() With { _
                        .EntityName = ContractTemplate.EntityLogicalName, .ColumnSet = New ColumnSet("contracttemplateid")}
                    templateQuery.Criteria = New FilterExpression()
                    templateQuery.Criteria.AddCondition("abbreviation", ConditionOperator.Equal, {"SCT"})
                    templateQuery.Criteria.FilterOperator = LogicalOperator.And

                    Dim ec As EntityCollection = _serviceProxy.RetrieveMultiple(templateQuery)

                    If ec.Entities.Count > 0 Then
                        _contractTemplateId = ec.Entities(0).Id
                        Console.Write("Template retrieved, ")
                    Else
                        Dim contractTemplate As New ContractTemplate() With { _
                            .Name = "Sample Contract Template", .BillingFrequencyCode = New OptionSetValue(1), .Abbreviation = "SCT", _
                            .AllotmentTypeCode = New OptionSetValue(1), _
                            .EffectivityCalendar = "--------+++++++++---------------+++++++++---------------+++++++++---------------" & _
                                        "+++++++++---------------+++++++++-------------------------------------------------------"}
                        _contractTemplateId = _serviceProxy.Create(contractTemplate)
                        Console.Write("Template created, ")
                    End If

'                    #End Region

'                    #Region "Create Contract"

                    ' Create a Contract from the Contract Template.
                    Dim contract As New Contract() With {.Title = "Sample Contract", .ContractTemplateId = New EntityReference With { _
                            .Id = _contractTemplateId, .LogicalName = ContractTemplate.EntityLogicalName}, _
                            .CustomerId = New EntityReference With {.Id = _accountId, .LogicalName = Account.EntityLogicalName}, _
                            .BillingCustomerId = New EntityReference With {.Id = _accountId, .LogicalName = Account.EntityLogicalName}, _
                            .ActiveOn = New Date(2015, 1, 1), .ExpiresOn = New Date(2020, 1, 1), .BillingStartOn = New Date(2015, 1, 1), _
                            .BillingEndOn = New Date(2020, 1, 1)}
                    _contractId = _serviceProxy.Create(contract)

                    Console.Write("parent contract created, ")

                    ' Create a contract line item.
                    Dim contractLineItem As New ContractDetail() With {.Title = "Sample Contract Line Item", _
                        .ContractId = New EntityReference With {.Id = _contractId, .LogicalName = contract.EntityLogicalName}, _
                        .CustomerId = New EntityReference With {.Id = _accountId, .LogicalName = Account.EntityLogicalName}, _
                        .ActiveOn = New Date(2015, 1, 1), .ExpiresOn = New Date(2020, 1, 1), .Price = New Money(20D), .TotalAllotments = 20}
                    _serviceProxy.Create(contractLineItem)

                    Console.Write("contract line attached, ")

'                    #End Region

'                    #Region "Clone contract twice"

                    '<snippetCloneContract>
                    ' Create the first clone of the contract.
                    Dim cloneRequest As New CloneContractRequest() With {.ContractId = _contractId, .IncludeCanceledLines = False}
                    Dim cloneResponse As CloneContractResponse = CType(_serviceProxy.Execute(cloneRequest), CloneContractResponse)
                    _firstCloneId = (CType(cloneResponse.Entity, Contract)).ContractId.Value

                    '</snippetCloneContract>
                    Console.Write("first clone created, ")

                    ' Create the second clone of the contract.
                    cloneRequest = New CloneContractRequest() With {.ContractId = _contractId, .IncludeCanceledLines = True}
                    cloneResponse = CType(_serviceProxy.Execute(cloneRequest), CloneContractResponse)
                    _secondCloneId = (CType(cloneResponse.Entity, Contract)).ContractId.Value

                    Console.Write("second clone created. " & vbLf)

                    ' Retrieve all Contracts.
                    Dim contractQuery As New QueryExpression() With {.EntityName = contract.EntityLogicalName, _
                                                                     .ColumnSet = New ColumnSet("contractid")}
                    contractQuery.Criteria = New FilterExpression()
                    contractQuery.Criteria.AddCondition("customerid", ConditionOperator.Equal, _accountId)
                    Dim contracts As EntityCollection = _serviceProxy.RetrieveMultiple(contractQuery)

                    ' Display the retrieved Contract Ids.
                    For i As Integer = 0 To contracts.Entities.Count - 1
                        Console.WriteLine("Retrieved contract with Id: {0}", (CType(contracts.Entities(i), Contract)).ContractId)
                    Next i

'                    #End Region

'                    #Region "Deactivate a cloned contract"

                    ' In order to deactivate a contract (put it on hold), it is first
                    ' necessary to invoice the contract.
                    Dim setStateRequest As New SetStateRequest() With {.EntityMoniker = New EntityReference With { _
                            .Id = _firstCloneId, .LogicalName = contract.EntityLogicalName}, _
                            .State = New OptionSetValue(CInt(Fix(ContractState.Invoiced))), .Status = New OptionSetValue(2)}
                    _serviceProxy.Execute(setStateRequest)

                    Console.Write("Contract invoiced, ")

                    ' Now that the contract has been invoiced, it is possible to put
                    ' the contract on hold.
                    setStateRequest = New SetStateRequest() With {.EntityMoniker = New EntityReference With { _
                            .Id = _firstCloneId, .LogicalName = contract.EntityLogicalName}, _
                            .State = New OptionSetValue(CInt(Fix(ContractState.OnHold))), .Status = New OptionSetValue(4)}
                    _serviceProxy.Execute(setStateRequest)

                    Console.Write("and put on hold." & vbLf)

'                    #End Region

'                    #Region "Renew an invoiced contract"

                    ' In order to renew a contract, it must be invoiced first, and
                    ' then canceled.

                    '<snippetSetStateForContract>
                    ' Invoice the contract.
                    setStateRequest = New SetStateRequest() With {.EntityMoniker = New EntityReference With { _
                            .Id = _contractId, .LogicalName = contract.EntityLogicalName}, _
                            .State = New OptionSetValue(CInt(Fix(ContractState.Invoiced))), .Status = New OptionSetValue(3)}
                    _serviceProxy.Execute(setStateRequest)
                    '</snippetSetStateForContract>

                    Console.Write("Contract invoiced, ")

                    '<snippetCancelContract>
                    ' Cancel the contract.
                    setStateRequest = New SetStateRequest() With { _
                        .EntityMoniker = New EntityReference With {.Id = _contractId, .LogicalName = contract.EntityLogicalName}, _
                        .State = New OptionSetValue(CInt(Fix(ContractState.Canceled))), .Status = New OptionSetValue(5)}
                    _serviceProxy.Execute(setStateRequest)

                    '</snippetCancelContract>
                    Console.Write("canceled, ")

                    '<snippetRenewContract>
                    ' Renew the canceled contract.
                    Dim renewRequest As New RenewContractRequest() With {.ContractId = _contractId, .IncludeCanceledLines = True, .Status = 1}
                    Dim renewResponse As RenewContractResponse = CType(_serviceProxy.Execute(renewRequest), RenewContractResponse)

                    ' Retrieve Id of renewed contract.
                    _renewedId = (CType(renewResponse.Entity, Contract)).ContractId.Value

                    '</snippetRenewContract>
                    ' Display the Id of the renewed contract.
                    Console.WriteLine("and renewed.")

'                    #End Region

                    DeleteRequiredRecords(promptforDelete)
                End Using
                '</snippetWorkingWithContracts1>

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
            ' Instantiate an Account object.
            ' See the Entity Metadata topic in the SDK documentation to determine
            ' which attributes must be set for each entity.
            Dim setupAccount As New Account() With {.Name = "Litware, Inc."}
            _accountId = _serviceProxy.Create(setupAccount)
        End Sub

        ''' <summary>
        ''' Deletes any entity records that were created for this sample.
        ''' <param name="prompt">Indicates whether to prompt the user 
        ''' to delete the records created in this sample.</param>
        ''' </summary>
        Public Sub DeleteRequiredRecords(ByVal prompt As Boolean)
            ' Two of the contracts, their associated account and the contract template
            ' records that were created and used in this sample will continue to exist 
            ' on your system because contracts that have been invoiced cannot be deleted 
            ' in Microsoft Dynamics CRM. They can only be put on hold or canceled.

            Dim toBeDeleted As Boolean = True

            If prompt Then
                ' Ask the user if the created entities should be deleted.
                Console.Write(vbLf & "Do you want these entity records deleted? (y/n) [y]: ")
                Dim answer As String = Console.ReadLine()
                If answer.StartsWith("y") OrElse answer.StartsWith("Y") OrElse answer = String.Empty Then
                    toBeDeleted = True
                Else
                    toBeDeleted = False
                End If
            End If

            If toBeDeleted Then
                _serviceProxy.Delete(Contract.EntityLogicalName, _secondCloneId)
                _serviceProxy.Delete(Contract.EntityLogicalName, _renewedId)
                _serviceProxy.Delete(Contract.EntityLogicalName, _contractId)
                Dim setStateRequest As New SetStateRequest() With
                    {
                        .EntityMoniker = New EntityReference With
                                         {
                                             .Id = _firstCloneId,
                                             .LogicalName = Contract.EntityLogicalName
                                         },
                        .State = New OptionSetValue(CInt(Fix(ContractState.Invoiced))),
                        .Status = New OptionSetValue(3)
                    }
                _serviceProxy.Execute(setStateRequest)
                setStateRequest = New SetStateRequest() With
                                  {
                                      .EntityMoniker = New EntityReference With
                                                       {
                                                           .Id = _firstCloneId,
                                                           .LogicalName = Contract.EntityLogicalName
                                                       },
                                      .State = New OptionSetValue(CInt(Fix(ContractState.Canceled))),
                                      .Status = New OptionSetValue(5)
                                  }
                _serviceProxy.Execute(setStateRequest)
                _serviceProxy.Delete(Contract.EntityLogicalName, _firstCloneId)
                _serviceProxy.Delete(Account.EntityLogicalName, _accountId)
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

                Dim app As New WorkingWithContracts()
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
        #End Region ' Main method
    End Class
End Namespace
'</snippetWorkingWithContracts>