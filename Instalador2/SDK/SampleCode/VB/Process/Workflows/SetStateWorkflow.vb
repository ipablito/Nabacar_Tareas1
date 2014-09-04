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

'<snippetSetStateWorkflow>
Imports Microsoft.VisualBasic
Imports System
Imports System.ServiceModel

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Client
Imports Microsoft.Xrm.Sdk.Query

' These namespaces are found in the Microsoft.Crm.Sdk.Proxy.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Crm.Sdk.Messages

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' This sample shows how to change the state of a workflow.
	''' </summary>
	''' <remarks>
	''' At run-time, you will be given the option to delete all the
	''' database records created by this program.
	''' </remarks>
	Public Class SetStateWorkflow
		#Region "Class Level Members"

		Private _workflowId As Guid
		Private _serviceProxy As OrganizationServiceProxy

		#End Region ' Class Level Members

		#Region "How To Sample Code"
		''' <summary>
		''' This method first creates XAML to define the custom workflow. Afterwards, 
		''' it creates the workflow record with this XAML and then activates it. Finally 
		''' it checks if it is activated and, if so, deactivates it.
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

                    CreateRequiredRecords()

                    ' Activate the workflow.
                    Console.WriteLine(vbLf & "Activating the workflow...")
                    '<snippetSetStateWorkflow1>
                    Dim activateRequest = New SetStateRequest With
                                          {
                                              .EntityMoniker = New EntityReference(Workflow.EntityLogicalName,
                                                                                   _workflowId),
                                              .State = New OptionSetValue(CInt(Fix(WorkflowState.Activated))),
                                              .Status = New OptionSetValue(CInt(Fix(workflow_statuscode.Activated)))
                                          }
                    _serviceProxy.Execute(activateRequest)
                    '</snippetSetStateWorkflow1>

                    ' Verify that the workflow is activated.
                    Dim retrievedWorkflow As Workflow =
                        _serviceProxy.Retrieve("workflow",
                                               _workflowId,
                                               New ColumnSet("statecode", "name")).ToEntity(Of Workflow)()

                    Console.WriteLine("The state of workflow {0} is: {1}.",
                                      retrievedWorkflow.Name,
                                      retrievedWorkflow.StateCode)

                    ' Deactivate the workflow.
                    If retrievedWorkflow.StateCode = WorkflowState.Activated Then
                        Console.WriteLine(vbLf & "Deactivating the workflow...")
                        '<snippetSetStateWorkflow2>
                        Dim deactivateRequest As SetStateRequest =
                            New SetStateRequest With
                            {
                                .EntityMoniker = New EntityReference(Workflow.EntityLogicalName, _workflowId),
                                .State = New OptionSetValue(CInt(Fix(WorkflowState.Draft))),
                                .Status = New OptionSetValue(CInt(Fix(workflow_statuscode.Draft)))
                            }
                        _serviceProxy.Execute(deactivateRequest)
                        '</snippetSetStateWorkflow2>
                    End If

                    ' Verify that the workflow is deactivated (in a draft state).
                    retrievedWorkflow = _serviceProxy.Retrieve("workflow",
                                                               _workflowId,
                                                               New ColumnSet("statecode", "name")).ToEntity(Of Workflow)()

                    Console.WriteLine("The state of workflow {0} is: {1}.",
                                      retrievedWorkflow.Name,
                                      retrievedWorkflow.StateCode)

                    DeleteRequiredRecords(promptforDelete)
                End Using

                ' Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            Catch fe As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
                ' You can handle an exception here or pass it back to the calling method.
                Throw
            End Try
        End Sub

		#Region "Public methods"

		''' <summary>
		''' Creates any entity records that this sample requires.
		''' </summary>
		Public Sub CreateRequiredRecords()
'			#Region "Create XAML"

			' Define the workflow XAML.
			Dim xamlWF As String

			' This xml defines a workflow that creates a new task when executed
            xamlWF = "<?xml version=""1.0"" encoding=""utf-16""?>" & _
                "<Activity x:Class=""CreateTask"" " & _
                "          xmlns=""http://schemas.microsoft.com/netfx/2009/xaml/activities"" " & _
                "          xmlns:mva=""clr-namespace:Microsoft.VisualBasic.Activities;assembly=System.Activities, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" " & _
                "          xmlns:mxs=""clr-namespace:Microsoft.Xrm.Sdk;assembly=Microsoft.Xrm.Sdk, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" " & _
                "          xmlns:mxswa=""clr-namespace:Microsoft.Xrm.Sdk.Workflow.Activities;assembly=Microsoft.Xrm.Sdk.Workflow, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"" " & _
                "          xmlns:s=""clr-namespace:System;assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"" " & _
                "          xmlns:scg=""clr-namespace:System.Collections.Generic;assembly=mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"" " & _
                "          xmlns:srs=""clr-namespace:System.Runtime.Serialization;assembly=System.Runtime.Serialization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"" " & _
                "          xmlns:this=""clr-namespace:"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">" & _
                "    <x:Members>" & _
                "        <x:Property Name=""InputEntities"" Type=""InArgument(scg:IDictionary(x:String, mxs:Entity))"" />" & _
                "        <x:Property Name=""CreatedEntities"" Type=""InArgument(scg:IDictionary(x:String, mxs:Entity))"" />" & _
                "    </x:Members>" & _
                "    <this:CreateTask.InputEntities>" & _
                "        <InArgument x:TypeArguments=""scg:IDictionary(x:String, mxs:Entity)"" />" & _
                "    </this:CreateTask.InputEntities>" & _
                "    <this:CreateTask.CreatedEntities>" & _
                "      <InArgument x:TypeArguments=""scg:IDictionary(x:String, mxs:Entity)"" />" & _
                "   </this:CreateTask.CreatedEntities>" & _
                "    <mva:VisualBasic.Settings>Assembly references and imported namespaces for internal implementation</mva:VisualBasic.Settings>" & _
                "    <mxswa:Workflow>" & _
                "        <Sequence>" & _
                "            <Assign x:TypeArguments=""mxs:Entity"" To=""[CreatedEntities(&quot;newTask&quot;)]"" Value=""[New Entity(&quot;task&quot;)]"" />" & _
                "            <mxswa:SetEntityProperty Attribute=""subject"" Entity=""[CreatedEntities(&quot;newTask&quot;)]"" " & _
                "                EntityName=""task"" TargetType=""{x:Null}"" " & _
                "                               Value=""[&quot;New Task&quot;]"">" & _
                "            </mxswa:SetEntityProperty>" & _
                "            <mxswa:SetEntityProperty Attribute=""regardingobjectid"" Entity=""[CreatedEntities(&quot;newTask&quot;)]"" " & _
                "                EntityName=""task"" TargetType=""{x:Null}"" " & _
                "                               Value=""[New EntityReference(&quot;lead&quot;, New Guid(InputEntities(&quot;primaryEntity&quot;).Id.ToString()))]"">" & _
                "            </mxswa:SetEntityProperty>" & _
                "            <mxswa:CreateEntity EntityId=""{x:Null}"" Entity=""[CreatedEntities(&quot;newTask&quot;)]"" " & _
                "                EntityName=""task"" />" & _
                "        </Sequence>" & _
                "    </mxswa:Workflow>" & _
                "</Activity>"

'			#End Region ' Create XAML

'			#Region "Create Workflow"

            ' Create the workflow
            '<snippetSetStateWorkflow3>
            Dim workflow As New Workflow() With
                {
                    .Name = "CreateTaskXAML",
                    .Type = New OptionSetValue(CInt(Fix(WorkflowType.Definition))),
                    .Category = New OptionSetValue(CInt(Fix(WorkflowCategory.Workflow))),
                    .Scope = New OptionSetValue(CInt(Fix(WorkflowScope.User))),
                    .LanguageCode = 1033,
                    .TriggerOnCreate = True,
                    .OnDemand = True,
                    .PrimaryEntity = Lead.EntityLogicalName,
                    .Description = "Test workflow for the SetStateWorkflow SDK sample",
                    .Xaml = xamlWF
                }
            _workflowId = _serviceProxy.Create(workflow)
            '</snippetSetStateWorkflow3>

			Console.WriteLine("Created workflow {0}.", workflow.Name)

'			#End Region ' Create Workflow
		End Sub

		''' <summary>
		''' Deletes any entity records that were created for this sample.
		''' <param name="prompt">Indicates whether to prompt the user 
		''' to delete the records created in this sample.</param>
		''' </summary>
		Public Sub DeleteRequiredRecords(ByVal prompt As Boolean)
			Dim toBeDeleted As Boolean = True

			If prompt Then
				' Ask the user if the created entities should be deleted.
				Console.Write(vbLf & "Do you want these entity records deleted? (y/n) [y]: ")
				Dim answer As String = Console.ReadLine()
                If answer.StartsWith("y") OrElse
                    answer.StartsWith("Y") OrElse
                    answer = String.Empty Then
                    toBeDeleted = True
                Else
                    toBeDeleted = False
                End If
			End If

			If toBeDeleted Then
				' Delete the workflow.
				_serviceProxy.Delete(Workflow.EntityLogicalName, _workflowId)
				Console.WriteLine("The workflow has been deleted.")
			End If
		End Sub
		#End Region ' Public Methods

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

				Dim app As New SetStateWorkflow()
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
'</snippetSetStateWorkflow>