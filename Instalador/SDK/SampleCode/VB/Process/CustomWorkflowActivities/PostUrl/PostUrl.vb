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

'<snippetPostUrl>
Imports System.Activities
Imports System.IO
Imports System.Net
Imports System.Text

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk

' These namespaces are found in the Microsoft.Xrm.Sdk.Workflow.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk.Workflow

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' Posts data in a url.
	''' Input arguments:
	'''   "URL". Type: String. Is the URL to which you will be posting data.
	'''   "Account Name". Type: String. Is part of the data to post.
	'''   "Account Num". Type: String. Is part of the data to post.
	''' Output argument:
	'''   None.
	''' </summary>
	Public NotInheritable Partial Class PostUrl
		Inherits CodeActivity
		''' <summary>
		''' NOTE: When you add this activity to a workflow, you must set the following properties:
		'''
		''' URL - manually add the URL to which you will be posting data.  For example: http://myserver.com/ReceivePostURL.aspx  
		'''		 See this sample's companion file 'ReceivePostURL.aspx' for an example of how the receiving page might look.
		'''
		''' AccountName - populate this property with the Account's 'name' attribute.
		'''
		''' AccountNum - populate this property with the Account's 'account number' attribute.
		''' </summary>
		Protected Overrides Sub Execute(ByVal executionContext As CodeActivityContext)
			Dim context As IWorkflowContext = executionContext.GetExtension(Of IWorkflowContext)()
			Dim serviceFactory As IOrganizationServiceFactory = executionContext.GetExtension(Of IOrganizationServiceFactory)()
			Dim service As IOrganizationService = serviceFactory.CreateOrganizationService(context.UserId)

			' Build data that will be posted to a URL
			Dim postData As String = "Name=" & Me.AccountName.Get(executionContext) & "&AccountNum=" & Me.AccountNum.Get(executionContext)

			' Encode the data
			Dim encoding As New ASCIIEncoding()
			Dim encodedPostData() As Byte = encoding.GetBytes(postData)

			' Create a request object for posting our data to a URL
            Dim uri As New Uri(Me.URL.Get(executionContext))
			Dim urlRequest As HttpWebRequest = CType(WebRequest.Create(uri), HttpWebRequest)
			urlRequest.Method = "POST"
			urlRequest.ContentLength = encodedPostData.Length
			urlRequest.ContentType = "application/x-www-form-urlencoded"

			' Add the encoded data to the request	
			Using formWriter As Stream = urlRequest.GetRequestStream()
				formWriter.Write(encodedPostData, 0, encodedPostData.Length)
			End Using

			' Post the data to the URL			
			Dim urlResponse As HttpWebResponse = CType(urlRequest.GetResponse(), HttpWebResponse)
		End Sub

		' Define Input/Output Arguments
        <Input("URL"), [Default]("http://localhost:9999/ReceivePostURL.aspx")> _
  Public Property URL() As InArgument(Of String)

		<Input("Account Name")> _
		Public Property AccountName() As InArgument(Of String)

		<Input("Account Num")> _
		Public Property AccountNum() As InArgument(Of String)
	End Class
End Namespace
'</snippetPostUrl>