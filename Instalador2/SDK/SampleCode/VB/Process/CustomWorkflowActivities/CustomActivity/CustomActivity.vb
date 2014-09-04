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

'<snippetCustomActivity>
Imports System.Activities

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk

' These namespaces are found in the Microsoft.Xrm.Sdk.Workflow.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk.Workflow

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' Creates a task with a subject equal to the ID of the input entity.
	''' Input arguments:
	'''   "Input Entity". Type: EntityReference. Is the account entity.
	''' Output argument:
	'''   "Task Created". Type: EntityReference. Is the task created.
	''' </summary>
	Public NotInheritable Partial Class CustomActivity
		Inherits CodeActivity
		''' <summary>
		''' Creates a task with a subject equal to the ID of the input EntityReference
		''' </summary>
		Protected Overrides Sub Execute(ByVal executionContext As CodeActivityContext)
			Dim context As IWorkflowContext = executionContext.GetExtension(Of IWorkflowContext)()
			Dim serviceFactory As IOrganizationServiceFactory = executionContext.GetExtension(Of IOrganizationServiceFactory)()
			Dim service As IOrganizationService = serviceFactory.CreateOrganizationService(context.UserId)

			' Retrieve the id
			Dim accountId As Guid = Me.inputEntity.Get(executionContext).Id

			' Create a task entity
			Dim task As New Entity()
			task.LogicalName = "task"
			task("subject") = accountId.ToString()
			task("regardingobjectid") = New EntityReference("account", accountId)
			Dim taskId As Guid = service.Create(task)
			Me.taskCreated.Set(executionContext, New EntityReference("task", taskId))
		End Sub

		' Define Input/Output Arguments
		<RequiredArgument, Input("InputEntity"), ReferenceTarget("account")> _
		Public Property inputEntity() As InArgument(Of EntityReference)

		<Output("TaskCreated"), ReferenceTarget("task")> _
		Public Property taskCreated() As OutArgument(Of EntityReference)
	End Class
End Namespace
'</snippetCustomActivity>