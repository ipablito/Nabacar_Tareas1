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

'<snippetDateChecker>
Imports System.Activities

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Query

' These namespaces are found in the Microsoft.Xrm.Sdk.Workflow.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk.Workflow

' These namespaces are found in the Microsoft.Crm.Sdk.Proxy.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Crm.Sdk.Messages

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' Checks if the "Est. Close Date" is greater than 10 days. If it is,
	''' send an email to the administrator so that s/he can verify close date
	''' with the owner of the opportunity.
	''' Input arguments:
	'''   "Opportunity". Type: EntityReference. Is the opportunity entity.
	''' Output argument:
	'''   None.
	''' </summary>
	Public NotInheritable Partial Class DateChecker
		Inherits CodeActivity
		''' <summary>
		''' Checks if the "Est. Close Date" is greater than 10 days. If it is,
		''' send an email to the administrator so that s/he can verify close date
		''' with the owner of the opportunity.
		''' </summary>
		Protected Overrides Sub Execute(ByVal executionContext As CodeActivityContext)
			Dim context As IWorkflowContext = executionContext.GetExtension(Of IWorkflowContext)()
				Dim serviceFactory As IOrganizationServiceFactory = executionContext.GetExtension(Of IOrganizationServiceFactory)()
				Dim service As IOrganizationService = serviceFactory.CreateOrganizationService(context.UserId)

			' Get opportunity entity
			Dim opportunity As Entity = service.Retrieve("opportunity", Me.inputOpportunity.Get(executionContext).Id, New ColumnSet("estimatedclosedate"))

			' Calulate 10 days from today
			Dim todayPlusTen As Date = Date.UtcNow.AddDays(10.0)

			' Check "Est. Close Date"
			If opportunity.Contains("estimatedclosedate") Then
				Dim estCloseDate As Date = CDate(opportunity("estimatedclosedate"))
				If Date.Compare(estCloseDate, todayPlusTen) > 0 Then
					' Need system user id for activity party
					Dim systemUserRequest As New WhoAmIRequest()
					Dim systemUser As WhoAmIResponse = CType(service.Execute(systemUserRequest), WhoAmIResponse)

					' Create an activity party for the email
					Dim activityParty(0) As Entity
					activityParty(0) = New Entity("activityparty")
					activityParty(0)("partyid") = New EntityReference("systemuser", systemUser.UserId)

					' Create an email message
					Dim email As New Entity("email")
					email.LogicalName = "email"
					email("subject") = "Warning: Close date has been extended more than 10 days."
					email("description") = "Verify close date is correct."
					email("to") = activityParty
					email("from") = activityParty
					email("regardingobjectid") = opportunity.ToEntityReference()
					Dim emailId As Guid = service.Create(email)

					' Send email
					Dim sendEmailRequest As New SendEmailRequest()
					sendEmailRequest.EmailId = emailId
					sendEmailRequest.IssueSend = True
					sendEmailRequest.TrackingToken = ""
					Dim sendEmailResponse As SendEmailResponse = CType(service.Execute(sendEmailRequest), SendEmailResponse)
				End If
			End If
		End Sub

		' Define Input/Output Arguments
		<RequiredArgument, Input("Opportunity"), ReferenceTarget("opportunity")> _
		Public Property inputOpportunity() As InArgument(Of EntityReference)
	End Class
End Namespace
'</snippetDateChecker>