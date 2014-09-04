' =====================================================================
'  This file is part of the Microsoft CRM SDK Code Samples.
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

'<snippetFollowupPlugin>
Imports System.ServiceModel

' Microsoft Dynamics CRM namespace(s)
Imports Microsoft.Xrm.Sdk

Namespace Microsoft.Crm.Sdk.Samples

    Public Class FollowupPlugin
        Implements IPlugin

        ''' <summary>
        ''' A plug-in that creates a follow-up task activity when a new account is created.
        ''' </summary>
        ''' <remarks>Register this plug-in on the Create message, account entity,
        ''' and asynchronous mode.
        ''' </remarks>
        Public Sub Execute(ByVal serviceProvider As IServiceProvider) _
            Implements IPlugin.Execute

            'Extract the tracing service for use in debugging sandboxed plug-ins.
            Dim tracingService As ITracingService =
                CType(serviceProvider.GetService(GetType(ITracingService)), 
                    ITracingService)

            '<snippetFollowupPlugin1>
            ' Obtain the execution context from the service provider.
            Dim context As IPluginExecutionContext =
                CType(serviceProvider.GetService(GetType(IPluginExecutionContext)), 
                    IPluginExecutionContext)
            '</snippetFollowupPlugin1>

            '<snippetFollowupPlugin2>
            ' The InputParameters collection contains all the data passed in the message request.
            If context.InputParameters.Contains("Target") AndAlso
                TypeOf context.InputParameters("Target") Is Entity Then

                ' Obtain the target entity from the input parameters.
                Dim entity As Entity = CType(context.InputParameters("Target"), Entity)
                '</snippetFollowupPlugin2>

                ' Verify that the target entity represents an account.
                ' If not, this plug-in was not registered correctly.
                If entity.LogicalName.Equals("account") Then

                    Try

                        ' Create a task activity to follow up with the account customer in 7 days. 
                        Dim followup As New Entity("task")

                        followup("subject") = "Send e-mail to the new customer."
                        followup("description") = "Follow up with the customer. " _
                            & "Check if there are any new issues that need resolution."
                        followup("scheduledstart") = Date.Now.AddDays(7)
                        followup("scheduledend") = Date.Now.AddDays(7)
                        followup("category") = context.PrimaryEntityName

                        ' Refer to the account in the task activity.
                        If context.OutputParameters.Contains("id") Then

                            Dim regardingobjectid As _
                                New Guid(context.OutputParameters("id").ToString())
                            Dim regardingobjectidType As String = "account"

                            followup("regardingobjectid") =
                                New EntityReference(regardingobjectidType,
                                                    regardingobjectid)

                        End If

                        '<snippetFollowupPlugin4>
                        ' Obtain the organization service reference.
                        Dim serviceFactory As IOrganizationServiceFactory =
                            CType(serviceProvider.GetService(GetType(IOrganizationServiceFactory)), 
                                IOrganizationServiceFactory)
                        Dim service As IOrganizationService =
                            serviceFactory.CreateOrganizationService(context.UserId)
                        '</snippetFollowupPlugin4>

                        ' Create the task in Microsoft Dynamics CRM.
                        tracingService.Trace("FollowupPlugin: Creating the task activity.")
                        service.Create(followup)

                        '<snippetFollowupPlugin3>
                    Catch ex As FaultException(Of OrganizationServiceFault)

                        Throw New InvalidPluginExecutionException(
                            "An error occurred in the FollupupPlugin plug-in.", ex)

                        '</snippetFollowupPlugin3>

                    Catch ex As Exception

                        tracingService.Trace("FollowupPlugin: {0}", ex.ToString())
                        Throw

                    End Try

                End If
            End If

        End Sub
    End Class

End Namespace
'</snippetFollowupPlugin>

