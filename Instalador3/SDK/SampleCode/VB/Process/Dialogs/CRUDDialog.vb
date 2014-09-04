' =====================================================================
'  This file is part of the Microsoft Dynamics CRM 2011 SDK code samples.
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

'<snippetCRUDDialog>
Imports System.IO
Imports System.ServiceModel
Imports System.ServiceModel.Description

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Query
Imports Microsoft.Xrm.Sdk.Client

' This namespace is found in the Microsoft.Crm.Sdk.Proxy.dll assembly
' located in the SDK\bin folder.
Imports Microsoft.Crm.Sdk.Messages

Namespace Microsoft.Crm.Sdk.Samples
    ''' <summary>
    ''' Demonstrates how to create, retrieve, update, and delete.
    ''' a dialog process.</summary>
    ''' <remarks>
    ''' At run-time, you will be given the option to delete all the
    ''' database records created by this program.</remarks>
    Public Class CRUDDialog
        #Region "Class Level Members"

        Private _dialogId As Guid
        Private _serviceProxy As OrganizationServiceProxy

        ''' <summary>
        ''' TODO: Change the location and file name of the sample XAML file
        ''' containing the dialog definition.
        ''' e.g. Use the sample xml file located in the SDK\SampleCode\CS\Dialogs folder.
        ''' </summary>
        Private pathToXAML As String = Path.Combine(Environment.CurrentDirectory, "CallCategorization.xml")

        #End Region ' Class Level Members

        #Region "How-To Sample Code"
        ''' <summary>
        ''' This method first connects to the Organization service. Afterwards,
        ''' create, retrieve, update, and delete operations are performed on a  
        ''' dialog process.
        ''' </summary>
        ''' <param name="serverConfig">Contains server connection information.</param>
        ''' <param name="promptforDelete">When True, the user will be prompted to delete all
        ''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration, ByVal promptforDelete As Boolean)
            Try
                '<snippetCRUDDialog1>
                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy will be properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    ' This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes()

                    CreateRequiredRecords()

                    ' Define an anonymous type to define the possible values for 
                    ' workflow category
                    Dim WorkflowCategory = New With {Key .Workflow = 0, Key .Dialog = 1}

                    ' Instantiate a Workflow object.
                    ' See the Entity Metadata topic in the SDK documentation to determine 
                    ' which attributes must be set for each entity.
                    Dim sampleDialog As Workflow = New Workflow With { _
                        .Category = New OptionSetValue(CInt(Fix(WorkflowCategory.Dialog))), .Name = "Sample Dialog: Call Categorization", _
                        .PrimaryEntity = PhoneCall.EntityLogicalName, .LanguageCode = 1033, .Xaml = File.ReadAllText(pathToXAML)}
                        'Language code for U.S. English

                    ' Create a dialog record.
                    _dialogId = _serviceProxy.Create(sampleDialog)
                    Console.Write("{0} created,", sampleDialog.Name)

                    ' Activate the dialog.
                    Dim activateRequest As SetStateRequest = New SetStateRequest With { _
                        .EntityMoniker = New EntityReference(Workflow.EntityLogicalName, _dialogId), _
                        .State = New OptionSetValue(CInt(Fix(WorkflowState.Activated))), .Status = New OptionSetValue(2)}
                    _serviceProxy.Execute(activateRequest)
                    Console.WriteLine(" and activated.")

                    ' Retrieve the dialog containing several of its attributes.
                    Dim cols As New ColumnSet("name", "statecode", "statuscode")

                    Dim retrievedDialog As Workflow = CType(_serviceProxy.Retrieve(Workflow.EntityLogicalName, _dialogId, cols), Workflow)
                    Console.Write("Retrieved,")

                    ' Update the dialog.
                    ' Deactivate the dialog before you can update it.
                    Dim deactivateRequest As SetStateRequest = New SetStateRequest With { _
                        .EntityMoniker = New EntityReference(Workflow.EntityLogicalName, _dialogId), _
                        .State = New OptionSetValue(CInt(Fix(WorkflowState.Draft))), .Status = New OptionSetValue(1)}
                    _serviceProxy.Execute(deactivateRequest)

                    ' Retrieve the dialog record again to get the unpublished 
                    ' instance in order to update.
                    Dim retrievedDialogDeactivated As Workflow = _
                        CType(_serviceProxy.Retrieve(Workflow.EntityLogicalName, _dialogId, cols), Workflow)

                    ' Update the dialog.
                    retrievedDialogDeactivated.Name = "Updated Dialog: Call Categorization"
                    _serviceProxy.Update(retrievedDialogDeactivated)

                    Console.Write(" updated,")

                    ' Activate the dialog.
                    Dim updateActivateRequest As SetStateRequest = New SetStateRequest With { _
                        .EntityMoniker = New EntityReference(Workflow.EntityLogicalName, _dialogId), _
                        .State = New OptionSetValue(CInt(Fix(WorkflowState.Activated))), .Status = New OptionSetValue(2)}
                    _serviceProxy.Execute(updateActivateRequest)
                    Console.WriteLine(" and activated again.")

                    DeleteRequiredRecords(promptforDelete)
                End Using
                '</snippetCRUDDialog1>

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
            ' For this sample, all required entities are created in the Run() method.
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

                deleteRecords = (answer.StartsWith("y") OrElse answer.StartsWith("Y") OrElse answer = String.Empty)
            End If

            If deleteRecords Then
                ' Deactivate the dialog, and then delete it.
                Dim deactivateRequest As SetStateRequest = New SetStateRequest With { _
                    .EntityMoniker = New EntityReference(Workflow.EntityLogicalName, _dialogId), _
                    .State = New OptionSetValue(CInt(Fix(WorkflowState.Draft))), .Status = New OptionSetValue(1)}
                _serviceProxy.Execute(deactivateRequest)
                _serviceProxy.Delete(Workflow.EntityLogicalName, _dialogId)
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

                Dim app As New CRUDDialog()
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
            Finally
                Console.WriteLine("Press <Enter> to exit.")
                Console.ReadLine()
            End Try
        End Sub
        #End Region ' Main method
    End Class
End Namespace
'</snippetCRUDDialog>