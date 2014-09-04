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

'<snippetSandboxPlugin>
Imports System.Threading
Imports System.Runtime.Serialization

Imports System.ServiceModel
Imports System.ServiceModel.Channels
Imports System.ServiceModel.Description

Imports Microsoft.Xrm.Sdk

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' A custom plug-in that can post the execution context of the current message to the Windows
	''' Azure Service Bus. The plug-in also demonstrates tracing which assist with
	''' debugging for plug-ins that are registered in the sandbox.
	''' </summary>
	''' <remarks>This sample requires that a service endpoint be created first, and its ID passed
	''' to the plug-in constructor through the unsecure configuration parameter when the plug-in
	''' step is registered.</remarks>
	Public NotInheritable Class SandboxPlugin
		Implements IPlugin
		Private serviceEndpointId As Guid

		''' <summary>
		''' Constructor.
		''' </summary>
		Public Sub New(ByVal config As String)
			If String.IsNullOrEmpty(config) OrElse (Not Guid.TryParse(config, serviceEndpointId)) Then
				Throw New InvalidPluginExecutionException("Service endpoint ID should be passed as config.")
			End If
		End Sub

        Public Sub Execute(ByVal serviceProvider As IServiceProvider) Implements IPlugin.Execute
            ' Retrieve the execution context.
            Dim context As IPluginExecutionContext = CType(serviceProvider.GetService(GetType(IPluginExecutionContext)), IPluginExecutionContext)

            ' Extract the tracing service.
            Dim tracingService As ITracingService = CType(serviceProvider.GetService(GetType(ITracingService)), ITracingService)
            If tracingService Is Nothing Then
                Throw New InvalidPluginExecutionException("Failed to retrieve the tracing service.")
            End If

            Dim cloudService As IServiceEndpointNotificationService = CType(serviceProvider.GetService(GetType(IServiceEndpointNotificationService)), IServiceEndpointNotificationService)
            If cloudService Is Nothing Then
                Throw New InvalidPluginExecutionException("Failed to retrieve the service bus service.")
            End If

            Try
                tracingService.Trace("Posting the execution context.")
                Dim response As String = cloudService.Execute(New EntityReference("serviceendpoint", serviceEndpointId), context)
                If Not String.IsNullOrEmpty(response) Then
                    tracingService.Trace("Response = {0}", response)
                End If
                tracingService.Trace("Done.")
            Catch e As Exception
                tracingService.Trace("Exception: {0}", e.ToString())
                Throw
            End Try
        End Sub
	End Class
End Namespace
'</snippetSandboxPlugin>