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

'<snippetUploadAndDownloadAttachment>
Imports System.IO
Imports System.Text
Imports System.ServiceModel

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Xrm.Sdk.Client
Imports Microsoft.Xrm.Sdk.Query

Namespace Microsoft.Crm.Sdk.Samples
    ''' <summary>
    ''' Demonstrates how to upload attachments to an annotation and how to download attachments.
    ''' </summary>
    Public Class UploadAndDownloadAttachment

        #Region "Class Level Members"

        Private _annotationId As Guid
        Private _serviceProxy As OrganizationServiceProxy
        Private _fileName As String

        #End Region ' Class Level Members

        #Region "How-To Sample Code"
        ''' <summary>
        ''' This method first connects to the Organization service. Afterwards,
        ''' an annotation is created, uploaded, then finally downloaded.
        ''' </summary>
        ''' <param name="serverConfig">Contains server connection information.</param>
        ''' <param name="promptforDelete">When True, the user will be prompted to delete all
        ''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration, ByVal promptforDelete As Boolean)
            Try
                '<snippetUploadAndDownloadAttachment1>
                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy will be properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    ' This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes()

'                    #Region "Create and Upload annotation attachment"

                    ' Instantiate an Annotation object.
                    ' See the Entity Metadata topic in the SDK documentation to determine
                    ' which attributes must be set for each entity.
                    Dim setupAnnotation As New Annotation() With {.Subject = "Example Annotation", _
                            .FileName = "ExampleAnnotationAttachment.txt", _
                            .DocumentBody = Convert.ToBase64String(New UnicodeEncoding().GetBytes("Sample Annotation Text")), _
                            .MimeType = "text/plain"}

                    ' Create the Annotation object.
                    _annotationId = _serviceProxy.Create(setupAnnotation)

                    Console.Write("{0} created with an attachment", setupAnnotation.Subject)

'                    #End Region ' Create and Upload annotation attachment

'                    #Region "Download attachment from annotation record"

                    ' Define columns to retrieve from the annotation record.
                    Dim cols As New ColumnSet("filename", "documentbody")


                    ' Retrieve the annotation record.
                    Dim retrievedAnnotation As Annotation = CType(_serviceProxy.Retrieve("annotation", _annotationId, cols), Annotation)
                    Console.WriteLine(", and retrieved.")
                    _fileName = retrievedAnnotation.FileName

                    ' Download the attachment in the current execution folder.
                    Using fileStream As New FileStream(retrievedAnnotation.FileName, FileMode.OpenOrCreate)
                        Dim fileContent() As Byte = Convert.FromBase64String(retrievedAnnotation.DocumentBody)
                        fileStream.Write(fileContent, 0, fileContent.Length)
                    End Using

                    Console.WriteLine("Attachment downloaded.")

'                    #End Region ' Download attachment from annotation record

                    DeleteRequiredRecords(promptforDelete)
                End Using
                '</snippetUploadAndDownloadAttachment1>

            ' Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            Catch fe As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
                ' You can handle an exception here or pass it back to the calling method.
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Deletes any entity records and files that were created for this sample.
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
                _serviceProxy.Delete(Annotation.EntityLogicalName, _annotationId)
                File.Delete(_fileName)
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

                Dim app As New UploadAndDownloadAttachment()
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

                    Dim fe As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault) = TryCast(ex.InnerException,  _
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
'</snippetUploadAndDownloadAttachment>