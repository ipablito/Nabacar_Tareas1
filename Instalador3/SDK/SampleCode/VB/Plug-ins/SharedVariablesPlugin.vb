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

'<snippetSharedVariablesPlugin>

' Microsoft Dynamics CRM namespace(s)
Imports Microsoft.Xrm.Sdk

Namespace Microsoft.Crm.Sdk.Samples

    ''' <summary>
    ''' A plug-in that sends data to another plug-in through the SharedVariables
    ''' property of IPluginExecutionContext.
    ''' </summary>
    ''' <remarks>Register the PreEventPlugin for a pre-operation stage and the 
    ''' PostEventPlugin plug-in on a post-operation stage.
    ''' </remarks>
    Public Class PreEventPlugin
        Implements IPlugin

        Public Sub Execute(ByVal serviceProvider As IServiceProvider) _
            Implements IPlugin.Execute

            ' Obtain the execution context from the service provider.
            Dim context As Microsoft.Xrm.Sdk.IPluginExecutionContext =
                CType(serviceProvider.GetService(
                        GetType(Microsoft.Xrm.Sdk.IPluginExecutionContext)), 
                    Microsoft.Xrm.Sdk.IPluginExecutionContext)

            ' Create or retrieve some data that will be needed by the post event
            ' plug-in. You could run a query, create an entity, or perform a calculation.
            'In this sample, the data to be passed to the post plug-in is
            ' represented by a GUID.
            Dim contact As New Guid("{74882D5C-381A-4863-A5B9-B8604615C2D0}")

            ' Pass the data to the post event plug-in in an execution context shared
            ' variable named PrimaryContact.
            context.SharedVariables.Add("PrimaryContact", CType(contact.ToString(),
                                        Object))

        End Sub
    End Class

    Public Class PostEventPlugin
        Implements IPlugin

        Public Sub Execute(ByVal serviceProvider As IServiceProvider) _
            Implements IPlugin.Execute

            ' Obtain the execution context from the service provider.
            Dim context As Microsoft.Xrm.Sdk.IPluginExecutionContext =
                CType(serviceProvider.GetService(
                        GetType(Microsoft.Xrm.Sdk.IPluginExecutionContext)), 
                    Microsoft.Xrm.Sdk.IPluginExecutionContext)

            ' Obtain the contact from the execution context shared variables.
            If context.SharedVariables.Contains("PrimaryContact") Then

                Dim contact As New Guid(CStr(context.SharedVariables("PrimaryContact")))

                ' Do something with the contact.

            End If

        End Sub
    End Class

End Namespace
'</snippetSharedVariablesPlugin>
