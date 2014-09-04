' ==============================================================================
'  This file is part of the Microsoft Dynamics CRM SDK Code Samples.
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
' ==============================================================================

'<snippetExecuteWorkflow>
Imports System.ServiceModel

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Client
Imports Microsoft.Xrm.Sdk.Query

' This namespace is found in Microsoft.Crm.Sdk.Proxy.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Crm.Sdk.Messages

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' Execute a workflow programmatically.
	''' </summary>
	Public Class ExecuteWorkflow

		#Region "Class Level Members"

		Private _workflowId As Guid
		Private _leadId As Guid
		Private _asyncOperationId As Guid
		Private _serviceProxy As OrganizationServiceProxy

		#End Region ' Class Level Members

		#Region "How-To Sample Code"
		''' <summary>
		''' Demonstrates how to programmatically execute a workflow.
		''' </summary>
		''' <param name="serverConfig">Contains server connection information.</param>
		''' <param name="promptforDelete">When True, the user will be prompted to delete all
		''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                       ByVal promptforDelete As Boolean)
            Try

                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy will be properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    ' This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes()

                    Dim _orgContext As New OrganizationServiceContext(_serviceProxy)

                    CreateRequiredRecords()

                    '<snippetExecuteWorkflow1>
                    ' Create an ExecuteWorkflow request.
                    Dim request As New ExecuteWorkflowRequest() With
                        {
                            .WorkflowId = _workflowId,
                            .EntityId = _leadId
                        }
                    Console.Write("Created ExecuteWorkflow request, ")

                    ' Execute the workflow.
                    Dim response As ExecuteWorkflowResponse =
                        CType(_serviceProxy.Execute(request), ExecuteWorkflowResponse)
                    Console.WriteLine("and sent request to service.")
                    '</snippetExecuteWorkflow1>

                    '					#Region "Check success"

                    Dim cols As New ColumnSet("statecode")
                    Dim retrieveOpQuery As New QueryByAttribute()
                    retrieveOpQuery.EntityName = AsyncOperation.EntityLogicalName
                    retrieveOpQuery.ColumnSet = cols
                    retrieveOpQuery.AddAttributeValue("asyncoperationid", response.Id)

                    ' Wait for the asyncoperation to complete.
                    ' (wait no longer than 1 minute)
                    For i As Integer = 0 To 59
                        System.Threading.Thread.Sleep(1000)

                        Dim retrieveOpResults As EntityCollection =
                            _serviceProxy.RetrieveMultiple(retrieveOpQuery)

                        If retrieveOpResults.Entities.Count() > 0 Then
                            Dim op As AsyncOperation =
                                CType(retrieveOpResults.Entities(0), AsyncOperation)
                            If op.StateCode = AsyncOperationState.Completed Then
                                _asyncOperationId = op.AsyncOperationId.Value
                                Console.WriteLine("AsyncOperation completed successfully.")
                                Exit For
                            End If
                        End If

                        If i = 59 Then
                            Throw New TimeoutException(
                                "AsyncOperation failed to complete in under one minute.")
                        End If
                    Next i

                    ' Retrieve the task that was created by the workflow.
                    cols = New ColumnSet("activityid")
                    Dim retrieveActivityQuery As New QueryByAttribute()
                    retrieveActivityQuery.EntityName = PhoneCall.EntityLogicalName
                    retrieveActivityQuery.ColumnSet = cols
                    retrieveActivityQuery.AddAttributeValue("subject",
                                                            "First call to Diogo Andrade")

                    Dim results As EntityCollection =
                        _serviceProxy.RetrieveMultiple(retrieveActivityQuery)

                    If results.Entities.Count() = 0 Then
                        Throw New InvalidOperationException(
                            "Phone call activity was not successfully created")
                    Else
                        Console.WriteLine(
                            "Phone call activity successfully created from workflow.")
                    End If

                    '					#End Region ' Check success

                    DeleteRequiredRecords(promptforDelete)
                End Using


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
			' Create a Lead record on which we will execute the Workflow.
			Dim lead As New Lead() With {.FirstName = "Diogo", .LastName = "Andrade"}
			_leadId = _serviceProxy.Create(lead)
			Console.WriteLine("Created Lead for workflow request.")

			' Define an anonymous type to define the possible values for
			' process type
            Dim ProcessType = New With {
                                            Key .Definition = 1,
                                            Key .Activation = 2,
                                            Key .Template = 3
                                       }

			' Define an anonymous type to define the possible values for
			' process category
			Dim ProcessCategory = New With {Key .Workflow = 0, Key .Dialog = 1}

			' Define an anonymous type to define the possible values for
			' process scope
            Dim ProcessScope = New With {
                                            Key .User = 1,
                                            Key .BusinessUnit = 2,
                                            Key .Deep = 3,
                                            Key .Global = 4
                                        }

			' Create the Workflow that we will execute.
            Dim workflow As New Workflow() With
                {
                    .Name = "Sample Workflow",
                    .Type = New OptionSetValue(ProcessType.Definition),
                    .Category = New OptionSetValue(ProcessCategory.Workflow),
                    .Scope = New OptionSetValue(ProcessScope.User),
                    .OnDemand = True,
                    .PrimaryEntity = lead.EntityLogicalName,
                    .Xaml = "<?xml version=""1.0"" encoding=""utf-16""?>" _
 & "<Activity x:Class=""ExecuteWorkflowSample"" xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" xmlns:mva=""clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" xmlns:mxs=""clr-namespace:Microsoft.Xrm.Sdk;assembly=Microsoft.Xrm.Sdk, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" xmlns:mxswa=""clr-namespace:Microsoft.Xrm.Sdk.Workflow.Activities;assembly=Microsoft.Xrm.Sdk.Workflow, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" xmlns:s=""clr-namespace:System;assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"" xmlns:scg=""clr-namespace:System.Collections.Generic;assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"" xmlns:srs=""clr-namespace:System.Runtime.Serialization;assembly=System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"" xmlns:this=""clr-namespace:"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">" _
 & "  <x:Members>" _
 & "    <x:Property Name=""InputEntities"" Type=""InArgument(scg:IDictionary(x:String, mxs:Entity))"" />" _
 & "    <x:Property Name=""CreatedEntities"" Type=""InArgument(scg:IDictionary(x:String, mxs:Entity))"" />" _
 & "  </x:Members>" _
 & "  <this:ExecuteWorkflowSample.InputEntities>" _
 & "    <InArgument x:TypeArguments=""scg:IDictionary(x:String, mxs:Entity)"" />" _
 & "  </this:ExecuteWorkflowSample.InputEntities>" _
 & "  <this:ExecuteWorkflowSample.CreatedEntities>" _
 & "    <InArgument x:TypeArguments=""scg:IDictionary(x:String, mxs:Entity)"" />" _
 & "  </this:ExecuteWorkflowSample.CreatedEntities>" _
 & "  <mva:VisualBasic.Settings>Assembly references and imported namespaces for internal implementation</mva:VisualBasic.Settings>" _
 & "  <mxswa:Workflow>" _
 & "    <Assign x:TypeArguments=""mxs:Entity"" To=""[CreatedEntities(&quot;CreateStep1_localParameter#Temp&quot;)]"" Value=""[New Entity(&quot;phonecall&quot;)]"" />" _
 & "    <Sequence DisplayName=""CreateStep1: Set first activity for lead."">" _
 & "      <Sequence.Variables>" _
 & "        <Variable x:TypeArguments=""x:Object"" Name=""CreateStep1_1"" />" _
 & "        <Variable x:TypeArguments=""x:Object"" Name=""CreateStep1_2"" />" _
 & "        <Variable x:TypeArguments=""x:Object"" Name=""CreateStep1_3"" />" _
 & "        <Variable x:TypeArguments=""x:Object"" Name=""CreateStep1_4"" />" _
 & "        <Variable x:TypeArguments=""x:Object"" Name=""CreateStep1_5"" />" _
 & "        <Variable x:TypeArguments=""x:Object"" Name=""CreateStep1_6"" />" _
 & "        <Variable x:TypeArguments=""x:Object"" Name=""CreateStep1_7"" />" _
 & "        <Variable x:TypeArguments=""x:Object"" Name=""CreateStep1_8"" />" _
 & "      </Sequence.Variables>" _
 & "      <mxswa:ActivityReference AssemblyQualifiedName=""Microsoft.Crm.Workflow.Activities.EvaluateExpression, Microsoft.Crm.Workflow, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" DisplayName=""EvaluateExpression"">" _
 & "        <mxswa:ActivityReference.Arguments>" _
 & "          <InArgument x:TypeArguments=""x:String"" x:Key=""ExpressionOperator"">CreateCrmType</InArgument>" _
 & "          <InArgument x:TypeArguments=""s:Object[]"" x:Key=""Parameters"">[New Object() { Microsoft.Xrm.Sdk.Workflow.WorkflowPropertyType.Boolean, ""True"" }]</InArgument>" _
 & "          <InArgument x:TypeArguments=""s:Type"" x:Key=""TargetType"">" _
 & "            <mxswa:ReferenceLiteral x:TypeArguments=""s:Type"" Value=""x:Boolean"" />" _
 & "          </InArgument>" _
 & "          <OutArgument x:TypeArguments=""x:Object"" x:Key=""Result"">[CreateStep1_1]</OutArgument>" _
 & "        </mxswa:ActivityReference.Arguments>" _
 & "      </mxswa:ActivityReference>" _
 & "      <mxswa:SetEntityProperty Attribute=""directioncode"" Entity=""[CreatedEntities(&quot;CreateStep1_localParameter#Temp&quot;)]"" EntityName=""phonecall"" Value=""[CreateStep1_1]"">" _
 & "        <mxswa:SetEntityProperty.TargetType>" _
 & "          <InArgument x:TypeArguments=""s:Type"">" _
 & "            <mxswa:ReferenceLiteral x:TypeArguments=""s:Type"" Value=""x:Boolean"" />" _
 & "          </InArgument>" _
 & "        </mxswa:SetEntityProperty.TargetType>" _
 & "      </mxswa:SetEntityProperty>" _
 & "      <mxswa:ActivityReference AssemblyQualifiedName=""Microsoft.Crm.Workflow.Activities.EvaluateExpression, Microsoft.Crm.Workflow, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" DisplayName=""EvaluateExpression"">" _
 & "        <mxswa:ActivityReference.Arguments>" _
 & "          <InArgument x:TypeArguments=""x:String"" x:Key=""ExpressionOperator"">CreateCrmType</InArgument>" _
 & "          <InArgument x:TypeArguments=""s:Object[]"" x:Key=""Parameters"">[New Object() { Microsoft.Xrm.Sdk.Workflow.WorkflowPropertyType.String, ""First call to "", ""String"" }]</InArgument>" _
 & "          <InArgument x:TypeArguments=""s:Type"" x:Key=""TargetType"">" _
 & "            <mxswa:ReferenceLiteral x:TypeArguments=""s:Type"" Value=""x:String"" />" _
 & "          </InArgument>" _
 & "          <OutArgument x:TypeArguments=""x:Object"" x:Key=""Result"">[CreateStep1_3]</OutArgument>" _
 & "        </mxswa:ActivityReference.Arguments>" _
 & "      </mxswa:ActivityReference>" _
 & "      <mxswa:GetEntityProperty Attribute=""fullname"" Entity=""[InputEntities(&quot;primaryEntity&quot;)]"" EntityName=""lead"" Value=""[CreateStep1_5]"">" _
 & "        <mxswa:GetEntityProperty.TargetType>" _
 & "          <InArgument x:TypeArguments=""s:Type"">" _
 & "            <mxswa:ReferenceLiteral x:TypeArguments=""s:Type"" Value=""x:String"" />" _
 & "          </InArgument>" _
 & "        </mxswa:GetEntityProperty.TargetType>" _
 & "      </mxswa:GetEntityProperty>" _
 & "      <mxswa:ActivityReference AssemblyQualifiedName=""Microsoft.Crm.Workflow.Activities.EvaluateExpression, Microsoft.Crm.Workflow, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" DisplayName=""EvaluateExpression"">" _
 & "        <mxswa:ActivityReference.Arguments>" _
 & "          <InArgument x:TypeArguments=""x:String"" x:Key=""ExpressionOperator"">SelectFirstNonNull</InArgument>" _
 & "          <InArgument x:TypeArguments=""s:Object[]"" x:Key=""Parameters"">[New Object() { CreateStep1_5 }]</InArgument>" _
 & "          <InArgument x:TypeArguments=""s:Type"" x:Key=""TargetType"">" _
 & "            <mxswa:ReferenceLiteral x:TypeArguments=""s:Type"" Value=""x:String"" />" _
 & "          </InArgument>" _
 & "          <OutArgument x:TypeArguments=""x:Object"" x:Key=""Result"">[CreateStep1_4]</OutArgument>" _
 & "        </mxswa:ActivityReference.Arguments>" _
 & "      </mxswa:ActivityReference>" _
 & "      <mxswa:ActivityReference AssemblyQualifiedName=""Microsoft.Crm.Workflow.Activities.EvaluateExpression, Microsoft.Crm.Workflow, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" DisplayName=""EvaluateExpression"">" _
 & "        <mxswa:ActivityReference.Arguments>" _
 & "          <InArgument x:TypeArguments=""x:String"" x:Key=""ExpressionOperator"">Add</InArgument>" _
 & "          <InArgument x:TypeArguments=""s:Object[]"" x:Key=""Parameters"">[New Object() { CreateStep1_3, CreateStep1_4 }]</InArgument>" _
 & "          <InArgument x:TypeArguments=""s:Type"" x:Key=""TargetType"">" _
 & "            <mxswa:ReferenceLiteral x:TypeArguments=""s:Type"" Value=""x:String"" />" _
 & "          </InArgument>" _
 & "          <OutArgument x:TypeArguments=""x:Object"" x:Key=""Result"">[CreateStep1_2]</OutArgument>" _
 & "        </mxswa:ActivityReference.Arguments>" _
 & "      </mxswa:ActivityReference>" _
 & "      <mxswa:SetEntityProperty Attribute=""subject"" Entity=""[CreatedEntities(&quot;CreateStep1_localParameter#Temp&quot;)]"" EntityName=""phonecall"" Value=""[CreateStep1_2]"">" _
 & "        <mxswa:SetEntityProperty.TargetType>" _
 & "          <InArgument x:TypeArguments=""s:Type"">" _
 & "            <mxswa:ReferenceLiteral x:TypeArguments=""s:Type"" Value=""x:String"" />" _
 & "          </InArgument>" _
 & "        </mxswa:SetEntityProperty.TargetType>" _
 & "      </mxswa:SetEntityProperty>" _
 & "      <mxswa:GetEntityProperty Attribute=""leadid"" Entity=""[InputEntities(&quot;primaryEntity&quot;)]"" EntityName=""lead"" Value=""[CreateStep1_7]"">" _
 & "        <mxswa:GetEntityProperty.TargetType>" _
 & "          <InArgument x:TypeArguments=""s:Type"">" _
 & "            <mxswa:ReferenceLiteral x:TypeArguments=""s:Type"" Value=""mxs:EntityReference"" />" _
 & "          </InArgument>" _
 & "        </mxswa:GetEntityProperty.TargetType>" _
 & "      </mxswa:GetEntityProperty>" _
 & "      <mxswa:ActivityReference AssemblyQualifiedName=""Microsoft.Crm.Workflow.Activities.EvaluateExpression, Microsoft.Crm.Workflow, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" DisplayName=""EvaluateExpression"">" _
 & "        <mxswa:ActivityReference.Arguments>" _
 & "          <InArgument x:TypeArguments=""x:String"" x:Key=""ExpressionOperator"">SelectFirstNonNull</InArgument>" _
 & "          <InArgument x:TypeArguments=""s:Object[]"" x:Key=""Parameters"">[New Object() { CreateStep1_7 }]</InArgument>" _
 & "          <InArgument x:TypeArguments=""s:Type"" x:Key=""TargetType"">" _
 & "            <mxswa:ReferenceLiteral x:TypeArguments=""s:Type"" Value=""mxs:EntityReference"" />" _
 & "          </InArgument>" _
 & "          <OutArgument x:TypeArguments=""x:Object"" x:Key=""Result"">[CreateStep1_6]</OutArgument>" _
 & "        </mxswa:ActivityReference.Arguments>" _
 & "      </mxswa:ActivityReference>" _
 & "      <mxswa:SetEntityProperty Attribute=""regardingobjectid"" Entity=""[CreatedEntities(&quot;CreateStep1_localParameter#Temp&quot;)]"" EntityName=""phonecall"" Value=""[CreateStep1_6]"">" _
 & "        <mxswa:SetEntityProperty.TargetType>" _
 & "          <InArgument x:TypeArguments=""s:Type"">" _
 & "            <mxswa:ReferenceLiteral x:TypeArguments=""s:Type"" Value=""mxs:EntityReference"" />" _
 & "          </InArgument>" _
 & "        </mxswa:SetEntityProperty.TargetType>" _
 & "      </mxswa:SetEntityProperty>" _
 & "      <mxswa:ActivityReference AssemblyQualifiedName=""Microsoft.Crm.Workflow.Activities.EvaluateExpression, Microsoft.Crm.Workflow, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" DisplayName=""EvaluateExpression"">" _
 & "        <mxswa:ActivityReference.Arguments>" _
 & "          <InArgument x:TypeArguments=""x:String"" x:Key=""ExpressionOperator"">CreateCrmType</InArgument>" _
 & "          <InArgument x:TypeArguments=""s:Object[]"" x:Key=""Parameters"">[New Object() { Microsoft.Xrm.Sdk.Workflow.WorkflowPropertyType.OptionSetValue, ""1"", ""Picklist"" }]</InArgument>" _
 & "          <InArgument x:TypeArguments=""s:Type"" x:Key=""TargetType"">" _
 & "            <mxswa:ReferenceLiteral x:TypeArguments=""s:Type"" Value=""mxs:OptionSetValue"" />" _
 & "          </InArgument>" _
 & "          <OutArgument x:TypeArguments=""x:Object"" x:Key=""Result"">[CreateStep1_8]</OutArgument>" _
 & "        </mxswa:ActivityReference.Arguments>" _
 & "      </mxswa:ActivityReference>" _
 & "      <mxswa:SetEntityProperty Attribute=""prioritycode"" Entity=""[CreatedEntities(&quot;CreateStep1_localParameter#Temp&quot;)]"" EntityName=""phonecall"" Value=""[CreateStep1_8]"">" _
 & "        <mxswa:SetEntityProperty.TargetType>" _
 & "          <InArgument x:TypeArguments=""s:Type"">" _
 & "            <mxswa:ReferenceLiteral x:TypeArguments=""s:Type"" Value=""mxs:OptionSetValue"" />" _
 & "          </InArgument>" _
 & "        </mxswa:SetEntityProperty.TargetType>" _
 & "      </mxswa:SetEntityProperty>" _
 & "      <mxswa:CreateEntity EntityId=""{x:Null}"" DisplayName=""CreateStep1: Set first activity for lead."" Entity=""[CreatedEntities(&quot;CreateStep1_localParameter#Temp&quot;)]"" EntityName=""phonecall"" />" _
 & "      <Assign x:TypeArguments=""mxs:Entity"" To=""[CreatedEntities(&quot;CreateStep1_localParameter&quot;)]"" Value=""[CreatedEntities(&quot;CreateStep1_localParameter#Temp&quot;)]"" />" _
 & "      <Persist />" _
 & "    </Sequence>" _
 & "  </mxswa:Workflow>" _
 & "</Activity>"
            }
			_workflowId = _serviceProxy.Create(workflow)
			Console.Write("Created workflow to call in Execute Workflow request, ")

            Dim setStateRequest As New SetStateRequest() With
                {
                    .EntityMoniker = New EntityReference(workflow.EntityLogicalName,
                                                         _workflowId),
                    .State = New OptionSetValue(CInt(Fix(WorkflowState.Activated))),
                    .Status = New OptionSetValue(2)
                }
			_serviceProxy.Execute(setStateRequest)
			Console.WriteLine("and activated.")
		End Sub

		''' <summary>
		''' Deletes any entity records that were created for this sample.
		''' <param name="prompt">Indicates whether to prompt the user 
		''' to delete the records created in this sample.</param>
		''' </summary>
		Public Sub DeleteRequiredRecords(ByVal prompt As Boolean)
			Dim deleteRecords As Boolean = True

			If prompt Then
				Console.WriteLine(vbLf & "Do you want these entity records deleted? (y/n) [y]: ")
				Dim answer As String = Console.ReadLine()

                deleteRecords = (answer.StartsWith("y") _
                                 OrElse answer.StartsWith("Y") _
                                 OrElse answer = String.Empty)
			End If

			If deleteRecords Then
                Dim setStateRequest As New SetStateRequest() With
                    {
                        .EntityMoniker = New EntityReference(Workflow.EntityLogicalName,
                                                             _workflowId),
                        .State = New OptionSetValue(CInt(Fix(WorkflowState.Draft))),
                        .Status = New OptionSetValue(1)
                    }
				_serviceProxy.Execute(setStateRequest)

				_serviceProxy.Delete(Workflow.EntityLogicalName, _workflowId)
				_serviceProxy.Delete(Lead.EntityLogicalName, _leadId)
				_serviceProxy.Delete(AsyncOperation.EntityLogicalName, _asyncOperationId)
				Console.WriteLine("Entity records have been deleted.")
			End If
		End Sub

		#End Region ' How-To Sample Code

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

				Dim app As New ExecuteWorkflow()
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
'</snippetExecuteWorkflow>