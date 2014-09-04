' =====================================================================
'  File:		DiagramBuilder.cs
'  Summary:	This sample command-line tool creates a 
'              Microsoft Office Visio diagram that details relationships
'              between Microsoft CRM entities. First, this sample reads
'              all the entity names from the parameter list or defaults
'              to all entities. Second, it creates a Visio object for 
'              the entity and all of the entities related to the entity,
'              and then it links them together. Finally, the file is saved.
' =====================================================================
'
'  This file is part of the Microsoft CRM 2011 SDK Code Samples.
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
' =====================================================================

Imports System.Text
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Collections

Imports VisioApi = Microsoft.Office.Interop.Visio

Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Xml.Serialization
Imports System.Xml


' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Query
Imports Microsoft.Xrm.Sdk.Metadata
Imports Microsoft.Xrm.Sdk.Client
Imports Microsoft.Xrm.Sdk.Messages


Namespace Microsoft.Crm.Sdk.Samples
	''' <summary>
	''' Create a Visio diagram detailing relationships between Microsoft CRM entities.
	'''
	''' First, this sample reads in all the entity names. It then creates a visio object for
	''' the entity and all of the entities related to the entity, and links them together.
	''' Finally,it saves the file to disk.
	''' </summary>
	Public Class DiagramBuilder
		#Region "Class Level Members"

		''' <summary>
		''' Stores the organization service proxy.
		''' </summary>
		Public Shared _serviceProxy As OrganizationServiceProxy

		' Specify which language code to use in the sample. If you are using a language
		' other than US English, you will need to modify this value accordingly.
		' See http://msdn.microsoft.com/en-us/library/0h88fahh.aspx
		Public Const _languageCode As Integer = 1033


		Private _application As VisioApi.Application
		Private _document As VisioApi.Document
		Private _metadataResponse As RetrieveAllEntitiesResponse
		Private _processedRelationships As ArrayList

		Private Const X_POS1 As Double = 0
		Private Const Y_POS1 As Double = 0
		Private Const X_POS2 As Double = 1.75
		Private Const Y_POS2 As Double = 0.6

		Private Const SHDW_PATTERN As Double = 0
		Private Const BEGIN_ARROW_MANY As Double = 29
		Private Const BEGIN_ARROW As Double = 0
		Private Const END_ARROW As Double = 29
		Private Const LINE_COLOR_MANY As Double = 10
		Private Const LINE_COLOR As Double = 8
		Private Const LINE_PATTERN_MANY As Double = 2
		Private Const LINE_PATTERN As Double = 1
		Private Const LINE_WEIGHT As String = "2pt"
		Private Const ROUNDING As Double = 0.0625
		Private Const HEIGHT As Double = 0.25
		Private Const NAME_CHARACTER_SIZE As Short = 12
        Private Const FONT_STYLE As Short = 225
        Private Const VISIO_SECTION_OJBECT_INDEX As Short = 1
        Private VersionName As String

		' Excluded entities.
		' These entities exist in the metadata but are not to be drawn in the diagram.
		Private Shared _excludedEntityTable As New Hashtable()
        Private Shared _excludedEntities() As String =
            {
                "attributemap",
                "bulkimport",
                "businessunitmap",
                "commitment",
                "displaystringmap",
                "documentindex",
                "entitymap",
                "importconfig",
                "integrationstatus",
                "internaladdress",
                "privilegeobjecttypecodes",
                "roletemplate",
                "roletemplateprivileges",
                "statusmap",
                "stringmap",
                "stringmapbit"
            }

		' Excluded relationship list.
		' Those entity relationships that should not be included in the diagram.
		Private Shared _excludedRelationsTable As New Hashtable()
		Private Shared _excludedRelations() As String = { "owningteam", "organizationid" }
		#End Region ' Class Level Members

		Public Sub New()
			' Build a hashtable from the array of excluded entities. This will
			' allow for faster lookups when determining if an entity is to be excluded.
			For n As Integer = 0 To _excludedEntities.Length - 1
                _excludedEntityTable.Add(_excludedEntities(n).GetHashCode(),
                                         _excludedEntities(n))
			Next n

			' Do the same for excluded relationships.
			For n As Integer = 0 To _excludedRelations.Length - 1
                _excludedRelationsTable.Add(_excludedRelations(n).GetHashCode(),
                                            _excludedRelations(n))
			Next n

			_processedRelationships = New ArrayList(128)
		End Sub



		''' <summary>
		''' Main entry point for the application.
		''' </summary>
		''' <param name="CmdArgs">Entities to place on the diagram</param>
		Public Shared Function Main(ByVal args() As String) As Integer
			Dim filename As String = String.Empty
			Dim application As VisioApi.Application
			Dim document As VisioApi.Document
			Dim builder As New DiagramBuilder()


			Try
				' Obtain the target organization's Web address and client logon 
				' credentials from the user.
				Dim serverConnect As New ServerConnection()
                Dim config As ServerConnection.Configuration =
                    serverConnect.GetServerConfiguration()

				' Connect to the Organization service. 
				' The using statement assures that the service proxy will be properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(config)
				Using _serviceProxy
					' This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes()

					' Load Visio and create a new document.
					application = New VisioApi.Application()
                    application.Visible = False ' Not showing the UI increases rendering speed
                    builder.VersionName = application.Version
					document = application.Documents.Add(String.Empty)

					builder._application = application
					builder._document = document

					' Load the metadata.
					Console.WriteLine("Loading Metadata...")
                    Dim request As New RetrieveAllEntitiesRequest() With
                        {
                            .EntityFilters = EntityFilters.Entity Or _
                            EntityFilters.Attributes Or _
                            EntityFilters.Relationships,
                            .RetrieveAsIfPublished = True
                        }
                    Dim response As RetrieveAllEntitiesResponse =
                        CType(_serviceProxy.Execute(request), RetrieveAllEntitiesResponse)

					builder._metadataResponse = response

					' Diagram all entities if given no command-line parameters, otherwise diagram
					' those entered as command-line parameters.
					If args.Length < 1 Then
						Dim entities As New ArrayList()

						For Each entity As EntityMetadata In response.EntityMetadata
							' Only draw an entity if it does not exist in the excluded entity table.
							If Not _excludedEntityTable.ContainsKey(entity.LogicalName.GetHashCode()) Then
								entities.Add(entity.LogicalName)
							Else
								Console.WriteLine("Excluding entity: {0}", entity.LogicalName)
							End If
						Next entity

                        builder.BuildDiagram(CType(entities.ToArray(GetType(String)), String()),
                                             "All Entities")
						filename = "AllEntities.vsd"
					Else
						builder.BuildDiagram(args, String.Join(", ", args))
						filename = String.Concat(args(0), ".vsd")
					End If

					' Save the diagram in the current directory using the name of the first
					' entity argument or "AllEntities" if none were given. Close the Visio application. 
					document.SaveAs(Directory.GetCurrentDirectory() & "\" & filename)
					application.Quit()
				End Using
			Catch ex As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp)
				Console.WriteLine("Code: {0}", ex.Detail.ErrorCode)
				Console.WriteLine("Message: {0}", ex.Detail.Message)
				Console.WriteLine("Plugin Trace: {0}", ex.Detail.TraceText)
                Console.WriteLine("Inner Fault: {0}", If(Nothing Is ex.Detail.InnerFault,
                                                        "No Inner Fault", "Has Inner Fault"))
			Catch ex As TimeoutException
				Console.WriteLine("The application terminated with an error.")
				Console.WriteLine("Message: {0}", ex.Message)
				Console.WriteLine("Stack Trace: {0}", ex.StackTrace)
                Console.WriteLine("Inner Fault: {0}", If(Nothing Is ex.InnerException.Message,
                                                        "No Inner Fault", ex.InnerException.Message))
			Catch ex As Exception
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
                        Console.WriteLine("Inner Fault: {0}", If(Nothing Is fe.Detail.InnerFault,
                                                                "No Inner Fault", "Has Inner Fault"))
					End If
				End If
                ' Additional exceptions to catch: SecurityTokenValidationException, ExpiredSecurityTokenException,
			' SecurityAccessDeniedException, MessageSecurityException, and SecurityNegotiationException.

			Finally
				'Console.WriteLine("Rendering complete.");
				Console.WriteLine("Rendering complete.  Press any key to continue.")
				Console.ReadLine()
			End Try

			Return 0
		End Function

		''' <summary>
		''' Create a new page in a Visio file showing all the direct entity relationships participated in
		''' by the passed-in array of entities.
		''' </summary>
		''' <param name="entities">Core entities for the diagram</param>
		''' <param name="pageTitle">Page title</param>
		Private Sub BuildDiagram(ByVal entities() As String, ByVal pageTitle As String)
			' Get the default page of our new document
            Dim page As VisioApi.Page = _document.Pages(1)
			page.Name = pageTitle

			' Get the metadata for each passed-in entity, draw it, and draw its relationships.
			For Each entityName As String In entities
				Console.Write("Processing entity: {0} ", entityName)

				Dim entity As EntityMetadata = GetEntityMetadata(entityName)

				' Create a Visio rectangle shape.
                Dim rect As VisioApi.Shape

				Try
					' There is no "Get Try", so we have to rely on an exception to tell us it does not exists
					' We have to skip some entities because they may have already been added by relationships of another entity
                    rect = page.Shapes.ItemU(entity.LogicalName)
				Catch e1 As System.Runtime.InteropServices.COMException
					rect = DrawEntityRectangle(page, entity.LogicalName, entity.OwnershipType.Value)
					Console.Write("."c) ' Show progress
				End Try

				' Draw all relationships TO this entity.
				DrawRelationships(entity, rect, entity.ManyToManyRelationships, False)
				Console.Write("."c) ' Show progress
				DrawRelationships(entity, rect, entity.ManyToOneRelationships, False)

				' Draw all relationshipos FROM this entity
				DrawRelationships(entity, rect, entity.OneToManyRelationships, True)
				Console.WriteLine("."c) ' Show progress
			Next entityName

			' Arrange the shapes to fit the page.
			page.Layout()
			page.ResizeToFitContents()
		End Sub

		''' <summary>
		''' Draw on a Visio page the entity relationships defined in the passed-in relationship collection.
		''' </summary>
		''' <param name="entity">Core entity</param>
		''' <param name="rect">Shape representing the core entity</param>
		''' <param name="relationshipCollection">Collection of entity relationships to draw</param>
		''' <param name="areReferencingRelationships">Whether or not the core entity is the referencing entity in the relationship</param>
        Private Sub DrawRelationships(ByVal entity As EntityMetadata,
                                      ByVal rect As VisioApi.Shape,
                                      ByVal relationshipCollection() As RelationshipMetadataBase,
                                      ByVal areReferencingRelationships As Boolean)
            Dim currentManyToManyRelationship As ManyToManyRelationshipMetadata = Nothing
            Dim currentOneToManyRelationship As OneToManyRelationshipMetadata = Nothing
            Dim entity2 As EntityMetadata = Nothing
            Dim attribute2 As AttributeMetadata = Nothing
            Dim attribute As AttributeMetadata = Nothing
            Dim metadataID As Guid = Guid.NewGuid()
            Dim isManyToMany As Boolean = False

            ' Draw each relationship in the relationship collection.
            For Each entityRelationship As RelationshipMetadataBase In relationshipCollection
                entity2 = Nothing

                If TypeOf entityRelationship Is ManyToManyRelationshipMetadata Then
                    isManyToMany = True
                    currentManyToManyRelationship = TryCast(entityRelationship, ManyToManyRelationshipMetadata)
                    ' The entity passed in is not necessarily the originator of this relationship.
                    If String.Compare(entity.LogicalName,
                                      currentManyToManyRelationship.Entity1LogicalName, True) <> 0 Then
                        entity2 = GetEntityMetadata(currentManyToManyRelationship.Entity1LogicalName)
                    Else
                        entity2 = GetEntityMetadata(currentManyToManyRelationship.Entity2LogicalName)
                    End If
                    attribute2 = GetAttributeMetadata(entity2, entity2.PrimaryIdAttribute)
                    attribute = GetAttributeMetadata(entity, entity.PrimaryIdAttribute)
                    metadataID = currentManyToManyRelationship.MetadataId.Value
                ElseIf TypeOf entityRelationship Is OneToManyRelationshipMetadata Then
                    isManyToMany = False
                    currentOneToManyRelationship = TryCast(entityRelationship, OneToManyRelationshipMetadata)
                    entity2 = GetEntityMetadata(If(areReferencingRelationships,
                                                   currentOneToManyRelationship.ReferencingEntity,
                                                   currentOneToManyRelationship.ReferencedEntity))
                    attribute2 = GetAttributeMetadata(entity2,
                                                      If(areReferencingRelationships,
                                                         currentOneToManyRelationship.ReferencingAttribute,
                                                         currentOneToManyRelationship.ReferencedAttribute))
                    attribute = GetAttributeMetadata(entity,
                                                     If(areReferencingRelationships,
                                                        currentOneToManyRelationship.ReferencedAttribute,
                                                        currentOneToManyRelationship.ReferencingAttribute))
                    metadataID = currentOneToManyRelationship.MetadataId.Value
                End If
                ' Verify relationship is either ManyToManyMetadata or OneToManyMetadata
                If entity2 IsNot Nothing Then
                    If _processedRelationships.Contains(metadataID) Then
                        ' Skip relationships we have already drawn
                        Continue For
                    Else
                        ' Record we are drawing this relationship
                        _processedRelationships.Add(metadataID)

                        ' Define convenience variables based upon the direction of referencing with respect to the core entity.
                        Dim rect2 As VisioApi.Shape


                        ' Do not draw relationships involving the entity itself, SystemUser, BusinessUnit,
                        ' or those that are intentionally excluded.
                        If String.Compare(entity2.LogicalName, "systemuser", True) <> 0 AndAlso
                            String.Compare(entity2.LogicalName, "businessunit", True) <> 0 AndAlso
                            String.Compare(entity2.LogicalName, rect.Name, True) <> 0 AndAlso
                            String.Compare(entity.LogicalName, "systemuser", True) <> 0 AndAlso
                            String.Compare(entity.LogicalName, "businessunit", True) <> 0 AndAlso
                            (Not _excludedRelationsTable.ContainsKey(attribute.LogicalName.GetHashCode())) Then
                            ' Either find or create a shape that represents this secondary entity, and add the name of
                            ' the involved attribute to the shape's text.
                            Try
                                rect2 = rect.ContainingPage.Shapes.ItemU(entity2.LogicalName)

                                If rect2.Text.IndexOf(attribute2.LogicalName) = -1 Then
                                    rect2.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                                                   CShort(Fix(VisioApi.VisRowIndices.visRowXFormOut)),
                                                   CShort(Fix(VisioApi.VisCellIndices.visXFormHeight))).ResultIU += 0.25
                                    rect2.Text += vbLf & attribute2.LogicalName

                                    ' If the attribute is a primary key for the entity, append a [PK] label to the attribute name to indicate this.
                                    If String.Compare(entity2.PrimaryIdAttribute, attribute2.LogicalName) = 0 Then
                                        rect2.Text &= "  [PK]"
                                    End If
                                End If
                            Catch e1 As System.Runtime.InteropServices.COMException
                                rect2 = DrawEntityRectangle(rect.ContainingPage,
                                                            entity2.LogicalName,
                                                            entity2.OwnershipType.Value)
                                rect2.Text += vbLf & attribute2.LogicalName

                                ' If the attribute is a primary key for the entity, append a [PK] label to the attribute name to indicate so.
                                If String.Compare(entity2.PrimaryIdAttribute, attribute2.LogicalName) = 0 Then
                                    rect2.Text &= "  [PK]"
                                End If
                            End Try

                            ' Add the name of the involved attribute to the core entity's text, if not already present.
                            If rect.Text.IndexOf(attribute.LogicalName) = -1 Then
                                rect.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                                              CShort(Fix(VisioApi.VisRowIndices.visRowXFormOut)),
                                              CShort(Fix(VisioApi.VisCellIndices.visXFormHeight))).ResultIU += HEIGHT
                                rect.Text += vbLf & attribute.LogicalName

                                ' If the attribute is a primary key for the entity, append a [PK] label to the attribute name to indicate so.
                                If String.Compare(entity.PrimaryIdAttribute, attribute.LogicalName) = 0 Then
                                    rect.Text &= "  [PK]"
                                End If
                            End If

                            ' Update the style of the entity name
                            Dim characters As VisioApi.Characters = rect.Characters
                            Dim characters2 As VisioApi.Characters = rect2.Characters

                            'set the font family of the text to segoe for the visio 2013.
                            If VersionName = "15.0" Then
                                characters.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterFont))) = CShort(FONT_STYLE)
                                characters2.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterFont))) = CShort(FONT_STYLE)
                            End If
                            Select Case entity2.OwnershipType
                                Case OwnershipTypes.BusinessOwned
                                    ' set the font color of the text
                                    characters.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterColor))) = CShort(Fix(VisioApi.VisDefaultColors.visBlack))
                                    characters2.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterColor))) = CShort(Fix(VisioApi.VisDefaultColors.visBlack))
                                Case OwnershipTypes.OrganizationOwned
                                    ' set the font color of the text
                                    characters.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterColor))) = CShort(Fix(VisioApi.VisDefaultColors.visBlack))
                                    characters2.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterColor))) = CShort(Fix(VisioApi.VisDefaultColors.visBlack))
                                Case OwnershipTypes.UserOwned
                                    ' set the font color of the text
                                    characters.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterColor))) = CShort(Fix(VisioApi.VisDefaultColors.visWhite))
                                    characters2.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterColor))) = CShort(Fix(VisioApi.VisDefaultColors.visWhite))
                                Case Else
                            End Select


                            ' Draw the directional, dynamic connector between the two entity shapes.
                            If areReferencingRelationships Then
                                DrawDirectionalDynamicConnector(rect, rect2, isManyToMany)
                            Else
                                DrawDirectionalDynamicConnector(rect2, rect, isManyToMany)
                            End If
                        Else
                            Debug.WriteLine(String.Format("<{0} - {1}> not drawn.",
                                                          rect.Name,
                                                          entity2.LogicalName),
                                                      "Relationship")
                        End If
                    End If
                End If
            Next entityRelationship
        End Sub

		''' <summary>
		''' Draw an "Entity" Rectangle
		''' </summary>
		''' <param name="page">The Page on which to draw</param>
		''' <param name="entityName">The name of the entity</param>
		''' <param name="ownership">The ownership type of the entity</param>
		''' <returns>The newly drawn rectangle</returns>
        Private Function DrawEntityRectangle(ByVal page As VisioApi.Page,
                                             ByVal entityName As String,
                                             ByVal ownership As OwnershipTypes) As VisioApi.Shape
            Dim rect As VisioApi.Shape = page.DrawRectangle(X_POS1, Y_POS1, X_POS2, Y_POS2)
            rect.Name = entityName
            rect.Text = entityName & " "

            ' Determine the shape fill color based on entity ownership.
            Dim fillColor As String

            ' Update the style of the entity name
            Dim characters As VisioApi.Characters = rect.Characters
            characters.Begin = 0
            characters.End = entityName.Length
            characters.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterStyle))) = CShort(Fix(VisioApi.VisCellVals.visBold))
            characters.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterSize))) = NAME_CHARACTER_SIZE
            'set the font family of the text to segoe for the visio 2013.
            If VersionName = "15.0" Then
                characters.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterFont))) = CShort(FONT_STYLE)
            End If

            Select Case ownership
                Case OwnershipTypes.BusinessOwned
                    fillColor = "RGB(255,140,0)" ' orange
                    characters.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterColor))) = CShort(Fix(VisioApi.VisDefaultColors.visBlack)) ' set the font color of the text
                Case OwnershipTypes.OrganizationOwned
                    fillColor = "RGB(127, 186, 0)" ' green
                    characters.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterColor))) = CShort(Fix(VisioApi.VisDefaultColors.visBlack)) ' set the font color of the text
                Case OwnershipTypes.UserOwned
                    fillColor = "RGB(0,24,143)" ' blue
                    characters.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterColor))) = CShort(Fix(VisioApi.VisDefaultColors.visWhite)) ' set the font color of the text
                Case Else
                    fillColor = "RGB(255,255,255)" ' White
                    characters.CharProps(CShort(Fix(VisioApi.VisCellIndices.visCharacterColor))) = CShort(Fix(VisioApi.VisDefaultColors.visDarkBlue)) ' set the font color of the text
            End Select

            ' Set the fill color, placement properties, and line weight of the shape.
            rect.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                          CShort(Fix(VisioApi.VisRowIndices.visRowMisc)),
                          CShort(Fix(VisioApi.VisCellIndices.visLOFlags))).FormulaU = (CInt(Fix(VisioApi.VisCellVals.visLOFlagsPlacable))).ToString()
            rect.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                          CShort(Fix(VisioApi.VisRowIndices.visRowFill)),
                          CShort(Fix(VisioApi.VisCellIndices.visFillForegnd))).FormulaU = fillColor

            Return rect
        End Function

		''' <summary>
		''' Draw a directional, dynamic connector between two entities, representing an entity relationship.
		''' </summary>
		''' <param name="shapeFrom">Shape initiating the relationship</param>
		''' <param name="shapeTo">Shape referenced by the relationship</param>
		''' <param name="isManyToMany">Whether or not it is a many-to-many entity relationship</param>
		Private Sub DrawDirectionalDynamicConnector(ByVal shapeFrom As VisioApi.Shape, ByVal shapeTo As VisioApi.Shape, ByVal isManyToMany As Boolean)
			' Add a dynamic connector to the page.
			Dim connectorShape As VisioApi.Shape = shapeFrom.ContainingPage.Drop(_application.ConnectorToolDataObject, 0.0, 0.0)

			' Set the connector properties, using different arrows, colors, and patterns for many-to-many relationships.
            connectorShape.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                                    CShort(Fix(VisioApi.VisRowIndices.visRowFill)),
                                    CShort(Fix(VisioApi.VisCellIndices.visFillShdwPattern))).ResultIU = SHDW_PATTERN
            connectorShape.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                                    CShort(Fix(VisioApi.VisRowIndices.visRowLine)),
                                    CShort(Fix(VisioApi.VisCellIndices.visLineBeginArrow))).ResultIU = If(isManyToMany, BEGIN_ARROW_MANY, BEGIN_ARROW)
            connectorShape.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                                    CShort(Fix(VisioApi.VisRowIndices.visRowLine)),
                                    CShort(Fix(VisioApi.VisCellIndices.visLineEndArrow))).ResultIU = END_ARROW
            connectorShape.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                                    CShort(Fix(VisioApi.VisRowIndices.visRowLine)),
                                    CShort(Fix(VisioApi.VisCellIndices.visLineColor))).ResultIU = If(isManyToMany, LINE_COLOR_MANY, LINE_COLOR)
            connectorShape.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                                    CShort(Fix(VisioApi.VisRowIndices.visRowLine)),
                                    CShort(Fix(VisioApi.VisCellIndices.visLinePattern))).ResultIU = If(isManyToMany, LINE_PATTERN, LINE_PATTERN)
            connectorShape.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                                    CShort(Fix(VisioApi.VisRowIndices.visRowFill)),
                                    CShort(Fix(VisioApi.VisCellIndices.visLineRounding))).ResultIU = ROUNDING

			' Connect the starting point.
            Dim cellBeginX As VisioApi.Cell = connectorShape.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                                                                      CShort(Fix(VisioApi.VisRowIndices.visRowXForm1D)),
                                                                      CShort(Fix(VisioApi.VisCellIndices.vis1DBeginX)))
            cellBeginX.GlueTo(shapeFrom.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                                                 CShort(Fix(VisioApi.VisRowIndices.visRowXFormOut)),
                                                 CShort(Fix(VisioApi.VisCellIndices.visXFormPinX))))

			' Connect the ending point.
            Dim cellEndX As VisioApi.Cell = connectorShape.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                                                                    CShort(Fix(VisioApi.VisRowIndices.visRowXForm1D)),
                                                                    CShort(Fix(VisioApi.VisCellIndices.vis1DEndX)))
            cellEndX.GlueTo(shapeTo.CellsSRC(VISIO_SECTION_OJBECT_INDEX,
                                             CShort(Fix(VisioApi.VisRowIndices.visRowXFormOut)),
                                             CShort(Fix(VisioApi.VisCellIndices.visXFormPinX))))
		End Sub

		''' <summary>
		''' Retrieves an entity from the local copy of CRM Metadata
		''' </summary>
		''' <param name="entityName">The name of the entity to find</param>
		''' <returns>NULL if the entity was not found, otherwise the entity's metadata</returns>
		Private Function GetEntityMetadata(ByVal entityName As String) As EntityMetadata
			For Each md As EntityMetadata In _metadataResponse.EntityMetadata
				If md.LogicalName = entityName Then
					Return md
				End If
			Next md

			Return Nothing
		End Function

		''' <summary>
		''' Retrieves an attribute from an EntityMetadata object
		''' </summary>
		''' <param name="entity">The entity metadata that contains the attribute</param>
		''' <param name="attributeName">The name of the attribute to find</param>
		''' <returns>NULL if the attribute was not found, otherwise the attribute's metadata</returns>
		Private Function GetAttributeMetadata(ByVal entity As EntityMetadata, ByVal attributeName As String) As AttributeMetadata
			For Each attrib As AttributeMetadata In entity.Attributes
				If attrib.LogicalName = attributeName Then
					Return attrib
				End If
			Next attrib

			Return Nothing
		End Function
	End Class
End Namespace