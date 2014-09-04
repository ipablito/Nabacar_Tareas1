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

'<snippetSearchAndRetrieveArticles>
Imports Microsoft.VisualBasic
Imports System
Imports System.Linq
Imports System.ServiceModel

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' located in the SDK\bin folder of the SDK download.
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Query
Imports Microsoft.Xrm.Sdk.Client

' This namespace is found in Microsoft.Crm.Sdk.Proxy.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Crm.Sdk.Messages

Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' Demonstrates how to search by body, keyword and title, and retrieve articles by
	''' topic incident subject and topic incident product.
	''' </summary>
	''' <param name="serverConfig">Contains server connection information.</param>
	''' <param name="promptforDelete">When True, the user will be prompted to delete all
	''' created entities.</param>
	Public Class SearchAndRetrieveArticles
		#Region "Class Level Members"

		Private _serviceProxy As OrganizationServiceProxy
		Private _context As ServiceContext

		Private _articles(2) As KbArticle
		Private _subjectId As Guid
		Private _incident As Incident
		Private _account As Account
		Private _product As Product
		Private _uom As UoM
		Private _uomSchedule As UoMSchedule

		#End Region ' Class Level Members

		#Region "How To Sample Code"
		''' <summary>
		''' This method first creates sample articles and publishes them, then searches
		''' for the articles by body, keyword and title. Finally, it retrieves the 
		''' articles by top incident subject and top incident product.
		''' </summary>
		''' <param name="serverConfig">Contains server connection information.</param>
		''' <param name="promptforDelete">When True, the user will be prompted to delete all
		''' created entities.</param>

		Public Sub Run(ByVal serverConfig As ServerConnection.Configuration, ByVal promptforDelete As Boolean)
			Try
				'<snippetSearchAndRetrieveArticles1>
				' Connect to the Organization service. 
				' The using statement assures that the service proxy will be properly disposed.
				_serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
				Using _serviceProxy
				' Using the ServiceContext class makes the queries easier
				_context = New ServiceContext(_serviceProxy)
				Using _context
					' This statement is required to enable early-bound type support.
					_serviceProxy.EnableProxyTypes()

					CreateRequiredRecords()

'					#Region "Search Knowledge base by Body"
                        '<snippetSearchByBodyKbArticle>
					' Create the request
                        Dim searchByBodyRequest As New SearchByBodyKbArticleRequest() With
                            {
                                .SubjectId = _subjectId,
                                .UseInflection = True,
                                .SearchText = "contained",
                                .QueryExpression = New QueryExpression() With
                                                   {
                                                       .ColumnSet = New ColumnSet("articlexml"),
                                                       .EntityName = KbArticle.EntityLogicalName
                                                   }
                            }
						' inflection to be substituted for the search text

					' Execute the request
					Console.WriteLine("  Searching for published article with 'contained' in the body")

                        Dim seachByBodyResponse As SearchByBodyKbArticleResponse =
                            CType(_context.Execute(searchByBodyRequest), 
                                SearchByBodyKbArticleResponse)

					' Check success
                        Dim retrievedArticleBodies =
                            seachByBodyResponse.EntityCollection.Entities.Select(
                                Function(entity) (CType(entity, KbArticle)).ArticleXml)

					If retrievedArticleBodies.Count() = 0 Then
						Throw New Exception("No articles found")
					End If

					Console.WriteLine("  Results of search (article bodies found):")
					For Each body In retrievedArticleBodies
						Console.WriteLine(body)
					Next body
                        '</snippetSearchByBodyKbArticle>

'					#End Region

'					#Region "Search knowledge base by Keyword"
                        '<snippetSearchByKeywordsKbArticle>

					' Create the request
                        Dim searchByKeywordRequest As New SearchByKeywordsKbArticleRequest() With
                            {
                                .SubjectId = _subjectId,
                                .UseInflection = True,
                                .SearchText = "Search",
                                .QueryExpression = New QueryExpression() With
                                                   {
                                                       .ColumnSet = New ColumnSet("keywords"),
                                                       .EntityName = KbArticle.EntityLogicalName
                                                   }
                            }

					' Execute the request
					Console.WriteLine()
					Console.WriteLine("  Searching for published article with 'search' as a keyword")
                        Dim searchByKeywordResponse =
                            CType(_context.Execute(searchByKeywordRequest), 
                                SearchByKeywordsKbArticleResponse)

					' Check success
                        Dim retrievedArticleKeywords =
                            searchByKeywordResponse.EntityCollection.Entities.Select(
                                Function(entity) CType(entity, KbArticle))

					If retrievedArticleKeywords.Count() = 0 Then
						Throw New Exception("No articles found")
					End If

					Console.WriteLine("  Results of search (keywords found):")
					For Each article In retrievedArticleKeywords
						Console.WriteLine(article.KeyWords)
					Next article
                        '</snippetSearchByKeywordsKbArticle>

'					#End Region

'					#Region "Search knowledge base by Title"
                        '<snippetSearchByTitleKbArticle>

					' create the request
                        Dim searchByTitleRequest As New SearchByTitleKbArticleRequest() With
                            {
                                .SubjectId = _subjectId,
                                .UseInflection = False,
                                .SearchText = "code",
                                .QueryExpression = New QueryExpression() With
                                                   {
                                                       .ColumnSet = New ColumnSet("title"),
                                                       .EntityName = KbArticle.EntityLogicalName
                                                   }
                            }

					' execute the request
					Console.WriteLine()
					Console.WriteLine("  Searching for published articles with 'code' in the title")
                        Dim searchByTitleResponse =
                            CType(_context.Execute(searchByTitleRequest), 
                                SearchByTitleKbArticleResponse)

					' check success
                        Dim retrievedArticles = searchByTitleResponse.EntityCollection.Entities.Select(
                            Function(entity) CType(entity, KbArticle))
					Console.WriteLine("  Results of search (titles found):")
					For Each article In retrievedArticles
						Console.WriteLine(article.Title)
					Next article
                        '</snippetSearchByTitleKbArticle>

'					#End Region

'					#Region "Retrieve by top incident subject"
                        '<snippetRetrieveByTopIncidentSubjectKbArticle>

					' create the request
                        Dim retrieveByTopIncidentSubjectRequest =
                            New RetrieveByTopIncidentSubjectKbArticleRequest() With
                            {
                                .SubjectId = _subjectId
                            }

					' execute request
					Console.WriteLine()
					Console.WriteLine("  Searching for the top articles in subject 'Default Subject'")
                        Dim retrieveByTopIncidentSubjectResponse =
                            CType(_context.Execute(retrieveByTopIncidentSubjectRequest), 
                                RetrieveByTopIncidentSubjectKbArticleResponse)

					' check success
                        Dim articles = retrieveByTopIncidentSubjectResponse.EntityCollection.Entities.Select(
                            Function(entity) CType(entity, KbArticle))
					Console.WriteLine("  Top articles in subject 'Default Subject':")
					For Each article In articles
						Console.WriteLine(article.Title)
					Next article
                        '</snippetRetrieveByTopIncidentSubjectKbArticle>

'					#End Region

'					#Region "Retrieve by top incident product"
                        '<snippetRetrieveByTopIncidentProductKbArticle>

					' create the request
                        Dim retrieveByTopIncidentProductRequest =
                            New RetrieveByTopIncidentProductKbArticleRequest() With
                            {
                                .ProductId = _product.Id
                            }

					' execute request
					Console.WriteLine()
					Console.WriteLine("  Searching for the top articles for product 'Sample Product'")
                        Dim retrieveByTopIncidentProductResponse =
                            CType(_context.Execute(retrieveByTopIncidentProductRequest), 
                                RetrieveByTopIncidentProductKbArticleResponse)

					' check success
                        articles = retrieveByTopIncidentProductResponse.EntityCollection.Entities.Select(
                            Function(entity) CType(entity, KbArticle))
					Console.WriteLine("  Top articles for product 'Sample Product':")
					For Each article In articles
						Console.WriteLine(article.Title)
					Next article
                        '</snippetRetrieveByTopIncidentProductKbArticle>

'					#End Region

					DeleteRequiredRecords(promptforDelete)
				End Using
				End Using
				'</snippetSearchAndRetrieveArticles1>

			' Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            Catch fe As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
                ' You can handle an exception here or pass it back to the calling method.
                Throw
			End Try
		End Sub

		#Region "Public methods"

		''' <summary>
		''' Creates any entity records that this sample requires.
		''' </summary>
		Public Sub CreateRequiredRecords()
'			#Region "create kb articles"

			Console.WriteLine("  Creating KB Articles")

            _subjectId = ( _
                From subject In _context.SubjectSet _
                Where subject.Title.Equals("Default Subject") _
                Select subject.Id).First()

            Dim kbArticleTemplateId = ( _
                From articleTemplate In _context.KbArticleTemplateSet _
                Where articleTemplate.Title.Equals("Standard KB Article") _
                Select articleTemplate.Id).First()

			' create a KB article
            _articles(0) = New KbArticle() With
                           {
                               .Title = "Searching the knowledge base",
                               .ArticleXml = " <articledata> " _
                                            + "<section id='0'>" _
                                            + " <content>" _
                                            + "<![CDATA[This is a sample article about searching the knowledge base.]]>" _
                                            + "</content>" _
                                            + " </section>" _
                                            + " <section id='1'>" _
                                            + " <content>" _
                                            + "<![CDATA[Knowledge bases contain information useful for various people.]]>" _
                                            + "</content>" _
                                            + " </section>" _
                                            + " </articledata>",
                               .KbArticleTemplateId = New EntityReference(
                                   KbArticleTemplate.EntityLogicalName,
                                   kbArticleTemplateId),
                               .SubjectId = New EntityReference(
                                   Subject.EntityLogicalName,
                                   _subjectId),
                               .KeyWords = "Searching Knowledge base"
                           }
				' set the article properties
				' use the built-in "Standard KB Article" template
				' use the default subject
			_context.AddObject(_articles(0))

            _articles(1) = New KbArticle() With
                           {
                               .Title = "What's in a knowledge base",
                               .ArticleXml = " <articledata>" _
                                            + " <section id='0'>" _
                                            + " <content>" _
                                            + "<![CDATA[This is a sample article about what would be in a knowledge base.]]>" _
                                            + "</content>" _
                                            + " </section>" _
                                            + " <section id='1'>" _
                                            + " <content>" _
                                            + "<![CDATA[This section contains more information.]]>" _
                                            + "</content> " _
                                            + "</section> " _
                                            + "</articledata>",
                               .KbArticleTemplateId = New EntityReference(
                                   KbArticleTemplate.EntityLogicalName,
                                   kbArticleTemplateId),
                               .SubjectId = New EntityReference(
                                   Subject.EntityLogicalName,
                                   _subjectId),
                               .KeyWords = "Knowledge base"
                           }
			_context.AddObject(_articles(1))

            _articles(2) = New KbArticle() With
                           {
                               .Title = "Searching the knowledge base from code",
                               .ArticleXml = " <articledata>" _
                                             + " <section id='0'>" _
                                             + " <content>" _
                                             + "<![CDATA[This article covers searching the knowledge base from code.]]>" _
                                             + "</content>" _
                                             + " </section>" _
                                             + " <section id='1'>" _
                                             + " <content>" _
                                             + "<![CDATA[This section contains more information.]]>" _
                                             + "</content>" _
                                             + " </section>" _
                                             + " </articledata>",
                               .KbArticleTemplateId = New EntityReference(
                                   KbArticleTemplate.EntityLogicalName,
                                   kbArticleTemplateId),
                               .SubjectId = New EntityReference(
                                   Subject.EntityLogicalName,
                                   _subjectId),
                               .KeyWords = "Knowledge base code"
                           }
			_context.AddObject(_articles(2))
			_context.SaveChanges()

'			#End Region

'			#Region "Submit the articles"

			Console.WriteLine("  Submitting the articles")

			For Each article In _articles
                _context.Execute(New SetStateRequest With
                                 {
                                     .EntityMoniker = article.ToEntityReference(),
                                     .State = New OptionSetValue(
                                         CInt(Fix(KbArticleState.Unapproved))),
                                     .Status = New OptionSetValue(
                                         CInt(Fix(kbarticle_statuscode.Unapproved)))
                                 })
			Next article

'			#End Region

'			#Region "Approve and Publish the article"

			Console.WriteLine("  Publishing articles")

			For Each article In _articles
                _context.Execute(New SetStateRequest With
                                 {
                                     .EntityMoniker = article.ToEntityReference(),
                                     .State = New OptionSetValue(
                                         CInt(Fix(KbArticleState.Published))),
                                     .Status = New OptionSetValue(
                                         CInt(Fix(kbarticle_statuscode.Published)))
                                 })
			Next article

'			#End Region

'			#Region "Waiting for publishing to finish"

			' Wait 20 seconds to ensure that data will be available
			' Full-text indexing
			Console.WriteLine("  Waiting 20 seconds to ensure indexing has completed on the new records.")
			System.Threading.Thread.Sleep(20000)
			Console.WriteLine()

'			#End Region

'			#Region "Add cases to KbArticles"

			' Create UoM
            _uomSchedule = New UoMSchedule() With
                           {
                               .Name = "Sample unit group",
                               .BaseUoMName = "Sample base unit"
                           }
			_context.AddObject(_uomSchedule)
			_context.SaveChanges()

            _uom = ( _
                From uom In _context.UoMSet _
                Where uom.Name.Equals(_uomSchedule.BaseUoMName) _
                Select uom).First()

			Console.WriteLine("  Creating an account and incidents for the KB articles")
			Dim whoami = CType(_context.Execute(New WhoAmIRequest()), WhoAmIResponse)

            _account = New Account() With
                       {
                           .Name = "Coho Winery"
                       }
			_context.AddObject(_account)
			_context.SaveChanges()

            _product = New Product() With
                       {
                           .Name = "Sample Product",
                           .ProductNumber = "0",
                           .DefaultUoMScheduleId = _uomSchedule.ToEntityReference(),
                           .DefaultUoMId = _uom.ToEntityReference()
                       }
			_context.AddObject(_product)
			_context.SaveChanges()

            _incident = New Incident() With
                        {
                            .Title = "A sample incident",
                            .OwnerId = New EntityReference(SystemUser.EntityLogicalName,
                                                           whoami.UserId),
                            .KbArticleId = _articles(0).ToEntityReference(),
                            .CustomerId = _account.ToEntityReference(),
                            .SubjectId = New EntityReference(Subject.EntityLogicalName,
                                                             _subjectId),
                            .ProductId = _product.ToEntityReference()
                        }
			_context.AddObject(_incident)
			_context.SaveChanges()

'			#End Region
		End Sub

		''' <summary>
		''' Deletes any entity records that were created for this sample.
		''' <param name="prompt">Indicates whether to prompt the user 
		''' to delete the records created in this sample.</param>
		''' </summary>
		Public Sub DeleteRequiredRecords(ByVal prompt As Boolean)
			Dim toBeDeleted As Boolean = True

			If prompt Then
				' Ask the user if the created entities should be deleted.
				Console.Write(vbLf & "Do you want these entity records deleted? (y/n) [y]: ")
				Dim answer As String = Console.ReadLine()
                If answer.StartsWith("y") OrElse
                    answer.StartsWith("Y") OrElse
                    answer = String.Empty Then
                    toBeDeleted = True
                Else
                    toBeDeleted = False
                End If
			End If

			If toBeDeleted Then
'				#Region "Delete incidents, accounts and units of measure"

				_serviceProxy.Delete(Incident.EntityLogicalName, _incident.Id)

				_serviceProxy.Delete(Product.EntityLogicalName, _product.Id)

				_serviceProxy.Delete(Account.EntityLogicalName, _account.Id)

				_serviceProxy.Delete(UoMSchedule.EntityLogicalName, _uomSchedule.Id)

'				#End Region

'				#Region "Unpublish articles"

				For Each article In _articles
                    _serviceProxy.Execute(New SetStateRequest With
                                          {
                                              .EntityMoniker = article.ToEntityReference(),
                                              .Status = New OptionSetValue(
                                                  CInt(Fix(kbarticle_statuscode.Unapproved))),
                                              .State = New OptionSetValue(
                                                  CInt(Fix(KbArticleState.Unapproved)))
                                          })
				Next article

'				#End Region

'				#Region "Delete articles"

				For Each article In _articles
					_serviceProxy.Delete(KbArticle.EntityLogicalName, article.Id)
				Next article

                '				#End Region
                Console.WriteLine("Entity records have been deleted.")
			End If
		End Sub
		#End Region ' Public Methods

		#End Region ' How To Sample Code

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

				Dim app As New SearchAndRetrieveArticles()
				app.Run(config, True)
			Catch ex As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp)
				Console.WriteLine("Code: {0}", ex.Detail.ErrorCode)
				Console.WriteLine("Message: {0}", ex.Detail.Message)
				Console.WriteLine("Plugin Trace: {0}", ex.Detail.TraceText)
                Console.WriteLine("Inner Fault: {0}",
                                  If(Nothing Is ex.Detail.InnerFault, "No Inner Fault", "Has Inner Fault"))
			Catch ex As System.TimeoutException
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine("Message: {0}", ex.Message)
				Console.WriteLine("Stack Trace: {0}", ex.StackTrace)
                Console.WriteLine("Inner Fault: {0}",
                                  If(Nothing Is ex.InnerException.Message, "No Inner Fault", ex.InnerException.Message))
			Catch ex As System.Exception
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine(ex.Message)

				' Display the details of the inner exception.
				If ex.InnerException IsNot Nothing Then
					Console.WriteLine(ex.InnerException.Message)

                    Dim fe As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault) =
                        TryCast(ex.InnerException, 
                            FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault))
					If fe IsNot Nothing Then
						Console.WriteLine("Timestamp: {0}", fe.Detail.Timestamp)
						Console.WriteLine("Code: {0}", fe.Detail.ErrorCode)
						Console.WriteLine("Message: {0}", fe.Detail.Message)
						Console.WriteLine("Plugin Trace: {0}", fe.Detail.TraceText)
                        Console.WriteLine("Inner Fault: {0}",
                                          If(Nothing Is fe.Detail.InnerFault, "No Inner Fault", "Has Inner Fault"))
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
'</snippetSearchAndRetrieveArticles>