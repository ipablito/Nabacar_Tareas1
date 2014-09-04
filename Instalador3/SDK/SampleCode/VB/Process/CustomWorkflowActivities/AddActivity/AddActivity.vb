﻿' =====================================================================
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

'<snippetAddActivity>
Imports System.Activities

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk

' These namespaces are found in the Microsoft.Xrm.Sdk.Workflow.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk.Workflow

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' Performs the addition of two summands and returns the result.
	''' Input arguments:
	'''   "First summand". Type: Int. Is the first summand of the addition.
	'''   "Second summand". Type: Int. Is the second summand of the addition.
	''' Output argument:
	'''   "Result". Type: Int. Is the result of the addition.
	''' </summary>
	Public NotInheritable Partial Class AddActivity
		Inherits CodeActivity
		''' <summary>
		''' Performs the addition of two summands
		''' </summary>
		Protected Overrides Sub Execute(ByVal executionContext As CodeActivityContext)
			Dim context As IWorkflowContext = executionContext.GetExtension(Of IWorkflowContext)()
			Dim serviceFactory As IOrganizationServiceFactory = executionContext.GetExtension(Of IOrganizationServiceFactory)()
			Dim service As IOrganizationService = serviceFactory.CreateOrganizationService(context.UserId)

			' Retrieve the summands and perform addition
			Me.result.Set(executionContext, Me.firstSummand.Get(executionContext) + Me.secondSummand.Get(executionContext))
		End Sub

		' Define Input/Output Arguments
		<Input("First summand")> _
		Public Property firstSummand() As InArgument(Of Integer)

		<Input("Second summand")> _
		Public Property secondSummand() As InArgument(Of Integer)

		<Output("Result")> _
		Public Property result() As OutArgument(Of Integer)
	End Class
End Namespace
'</snippetAddActivity>