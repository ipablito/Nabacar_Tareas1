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
'
' ==============================================================================
'<snippetAzureAwareWorkflowActivity>
' This namespace is found in the System.Activities.dll assembly.
Imports System.Activities

' This namespace is found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk

' This namespace is found in the Microsoft.Xrm.Sdk.Workflow.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk.Workflow

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' This class is able to post the execution context to the Windows Azure 
	''' Service Bus.
	''' </summary>
	Public Class AzureAwareWorkflowActivity
		Inherits CodeActivity
		''' <summary>
		''' This method is called when the workflow executes.
		''' </summary>
		''' <param name="executionContext">The data for the event triggering
		''' the workflow.</param>
		Protected Overrides Sub Execute(ByVal executionContext As CodeActivityContext)
			Dim context As IWorkflowContext = executionContext.GetExtension(Of IWorkflowContext)()

			Dim endpointService As IServiceEndpointNotificationService = executionContext.GetExtension(Of IServiceEndpointNotificationService)()
			endpointService.Execute(ServiceEndpoint.Get(executionContext), context)
		End Sub

		''' <summary>
		''' Enables the service endpoint to be provided when this activity is added as a 
		''' step in a workflow.
		''' </summary>
		<RequiredArgument, ReferenceTarget("serviceendpoint"), Input("Input id")> _
		Public Property ServiceEndpoint() As InArgument(Of EntityReference)
	End Class
End Namespace
'</snippetAzureAwareWorkflowActivity>