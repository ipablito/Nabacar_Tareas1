' =====================================================================
'
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
'
' =====================================================================
'<snippetStateModelTransitions>

Imports System
Imports System.Linq
Imports System.Xml.Linq
Imports System.ServiceModel
Imports System.ServiceModel.Description
Imports System.Collections.Generic
Imports System.Xml.Serialization
Imports System.Xml

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Query
Imports Microsoft.Xrm.Sdk.Metadata
Imports Microsoft.Xrm.Sdk.Metadata.Query
Imports Microsoft.Xrm.Sdk.Client
Imports Microsoft.Xrm.Sdk.Messages

' This namespace is found in Microsoft.Crm.Sdk.Proxy.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Crm.Sdk.Messages

Namespace Microsoft.Crm.Sdk.Samples
    Friend Class StateModelTransitions
#Region "Class Level Members"
        ''' <summary>
        ''' Stores the organization service proxy.
        ''' </summary>
        Private _serviceProxy As OrganizationServiceProxy
#End Region ' Class Level Members

#Region "How To Sample Code"
        ''' <summary>
        ''' Create and configure the organization service proxy.
        ''' Retrieve status options for the Incident entity
        ''' Use GetValidStatusOptions to get valid status transitions for each status option
        ''' </summary>
        ''' <param name="serverConfig">Contains server connection information.</param>
        ''' <param name="promptForDelete">When True, the user will be prompted to delete all
        ''' created entities.</param>
        Public Sub Run(ByVal serverConfig As ServerConnection.Configuration, ByVal promptForDelete As Boolean)
            Try

                ' Connect to the Organization service. 
                ' The using statement assures that the service proxy will be properly disposed.
                _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
                Using _serviceProxy
                    ' This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes()
                    '<snippetStateModelTransitions.run>
                    Dim entityLogicalName As String = "incident"
                    ' Retrieve status options for the Incident entity

                    'Retrieve just the incident entity and its attributes
                    Dim entityFilter As New MetadataFilterExpression(LogicalOperator.And)
                    entityFilter.Conditions.Add(New MetadataConditionExpression("LogicalName", MetadataConditionOperator.Equals, entityLogicalName))
                    Dim entityProperties As New MetadataPropertiesExpression(New String() {"Attributes"})

                    'Retrieve just the status attribute and the OptionSet property
                    Dim attributeFilter As New MetadataFilterExpression(LogicalOperator.And)
                    attributeFilter.Conditions.Add(New MetadataConditionExpression("AttributeType", MetadataConditionOperator.Equals, AttributeTypeCode.Status))
                    Dim attributeProperties As New MetadataPropertiesExpression(New String() {"OptionSet"})

                    'Instantiate the entity query
                    Dim query As New EntityQueryExpression() With {.Criteria = entityFilter, .Properties = entityProperties, .AttributeQuery = New AttributeQueryExpression() With {.Criteria = attributeFilter, .Properties = attributeProperties}}

                    'Retrieve the metadata
                    Dim request As New RetrieveMetadataChangesRequest() With {.Query = query}
                    Dim response As RetrieveMetadataChangesResponse = CType(_serviceProxy.Execute(request), RetrieveMetadataChangesResponse)


                    Dim statusAttribute As StatusAttributeMetadata = CType(response.EntityMetadata(0).Attributes(0), StatusAttributeMetadata)
                    Dim statusOptions As OptionMetadataCollection = statusAttribute.OptionSet.Options
                    'Loop through each of the status options
                    For Each [option] As StatusOptionMetadata In statusOptions
                        Dim StatusOptionLabel As String = GetOptionSetLabel(statusAttribute, [option].Value.Value)
                        Console.WriteLine("[{0}] {1} records can transition to:", StatusOptionLabel, entityLogicalName)
                        Dim validStatusOptions As List(Of StatusOption) = GetValidStatusOptions(entityLogicalName, [option].Value.Value)
                        'Loop through each valid transition for the option
                        For Each opt As StatusOption In validStatusOptions
                            Console.WriteLine("{0,-3}{1,-10}{2,-5}{3,-10}", opt.StateValue, opt.StateLabel, opt.StatusValue, opt.StatusLabel)
                        Next opt
                        Console.WriteLine("")
                    Next [option]
                    '</snippetStateModelTransitions.run>
                End Using

                ' Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            Catch ex As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
                ' You can handle an exception here or pass it back to the calling method.
                Throw
            End Try
        End Sub


#End Region ' How To Sample Code

#Region "Main"
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

                Dim app As New StateModelTransitions()
                app.Run(config, True)
            Catch ex As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
                Console.WriteLine("The application terminated with an error.")
                Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp)
                Console.WriteLine("Code: {0}", ex.Detail.ErrorCode)
                Console.WriteLine("Message: {0}", ex.Detail.Message)
                Console.WriteLine("Plugin Trace: {0}", ex.Detail.TraceText)
                Console.WriteLine("Inner Fault: {0}", If(Nothing Is ex.Detail.InnerFault, "No Inner Fault", "Has Inner Fault"))
            Catch ex As System.TimeoutException
                Console.WriteLine("The application terminated with an error.")
                Console.WriteLine("Message: {0}", ex.Message)
                Console.WriteLine("Stack Trace: {0}", ex.StackTrace)
                Console.WriteLine("Inner Fault: {0}", If(Nothing Is ex.InnerException.Message, "No Inner Fault", ex.InnerException.Message))
            Catch ex As System.Exception
                Console.WriteLine("The application terminated with an error.")
                Console.WriteLine(ex.Message)

                ' Display the details of the inner exception.
                If ex.InnerException IsNot Nothing Then
                    Console.WriteLine(ex.InnerException.Message)

                    Dim fe As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault) = TryCast(ex.InnerException, FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault))
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
#End Region ' Main

#Region "Methods used in Sample"
        '<snippetStateModelTransitions.GetValidStatusOptions>
        ''' <summary>
        ''' Returns valid status option transitions regardless of whether state transitions are enabled for the entity
        ''' </summary>
        ''' <param name="entityLogicalName">The logical name of the entity</param>
        ''' <param name="currentStatusValue">The current status of the entity instance</param>
        ''' <returns>A list of StatusOptions that represent the valid transitions</returns>
        Public Function GetValidStatusOptions(ByVal entityLogicalName As String, ByVal currentStatusValue As Integer) As List(Of StatusOption)

            Dim validStatusOptions As New List(Of StatusOption)()

            'Check entity Metadata

            'Retrieve just one entity definition
            Dim entityFilter As New MetadataFilterExpression(LogicalOperator.And)
            entityFilter.Conditions.Add(New MetadataConditionExpression("LogicalName", MetadataConditionOperator.Equals, entityLogicalName))
            'Return the attributes and the EnforceStateTransitions property
            Dim entityProperties As New MetadataPropertiesExpression(New String() {"Attributes", "EnforceStateTransitions"})

            'Retrieve only State or Status attributes
            Dim attributeFilter As New MetadataFilterExpression(LogicalOperator.Or)
            attributeFilter.Conditions.Add(New MetadataConditionExpression("AttributeType", MetadataConditionOperator.Equals, AttributeTypeCode.Status))
            attributeFilter.Conditions.Add(New MetadataConditionExpression("AttributeType", MetadataConditionOperator.Equals, AttributeTypeCode.State))

            'Retrieve only the OptionSet property of the attributes
            Dim attributeProperties As New MetadataPropertiesExpression(New String() {"OptionSet"})

            'Set the query
            Dim query As New EntityQueryExpression() With {.Criteria = entityFilter, .Properties = entityProperties, .AttributeQuery = New AttributeQueryExpression() With {.Criteria = attributeFilter, .Properties = attributeProperties}}

            'Retrieve the metadata
            Dim request As New RetrieveMetadataChangesRequest() With {.Query = query}
            Dim response As RetrieveMetadataChangesResponse = CType(_serviceProxy.Execute(request), RetrieveMetadataChangesResponse)

            'Check the value of EnforceStateTransitions
            Dim EnforceStateTransitions? As Boolean = response.EntityMetadata(0).EnforceStateTransitions

            'Capture the state and status attributes
            Dim statusAttribute As New StatusAttributeMetadata()
            Dim stateAttribute As New StateAttributeMetadata()

            For Each attributeMetadata As AttributeMetadata In response.EntityMetadata(0).Attributes
                Select Case attributeMetadata.AttributeType
                    Case AttributeTypeCode.Status
                        statusAttribute = CType(attributeMetadata, StatusAttributeMetadata)
                    Case AttributeTypeCode.State
                        stateAttribute = CType(attributeMetadata, StateAttributeMetadata)
                End Select
            Next attributeMetadata


            If EnforceStateTransitions.HasValue AndAlso EnforceStateTransitions.Value = True Then
                'When EnforceStateTransitions is true use the TransitionData to filter the valid options
                For Each [option] As StatusOptionMetadata In statusAttribute.OptionSet.Options
                    If [option].Value = currentStatusValue Then
                        If [option].TransitionData <> String.Empty Then
                            Dim transitionData As XDocument = XDocument.Parse([option].TransitionData)

                            Dim elements As IEnumerable(Of XElement) = ((CType(transitionData.FirstNode, XElement))).Descendants()

                            For Each e As XElement In elements
                                Dim statusOptionValue As Integer = Convert.ToInt32(e.Attribute("tostatusid").Value)
                                Dim statusLabel As String = GetOptionSetLabel(statusAttribute, statusOptionValue)

                                Dim stateLabel As String = String.Empty
                                Dim stateValue? As Integer = Nothing
                                For Each statusOption As StatusOptionMetadata In statusAttribute.OptionSet.Options
                                    If statusOption.Value.Value = statusOptionValue Then
                                        stateValue = statusOption.State.Value
                                        stateLabel = GetOptionSetLabel(stateAttribute, stateValue.Value)
                                    End If

                                Next statusOption


                                validStatusOptions.Add(New StatusOption() With {.StateLabel = stateLabel, .StateValue = stateValue.Value, .StatusLabel = statusLabel, .StatusValue = [option].Value.Value})
                            Next e
                        End If
                    End If
                Next [option]

            Else
                '//When EnforceStateTransitions is false do not filter the available options

                For Each [option] As StatusOptionMetadata In statusAttribute.OptionSet.Options
                    If [option].Value <> currentStatusValue Then

                        Dim statusLabel As String = ""
                        Try
                            statusLabel = [option].Label.UserLocalizedLabel.Label
                        Catch e1 As Exception
                            statusLabel = [option].Label.LocalizedLabels(0).Label
                        End Try

                        Dim stateLabel As String = GetOptionSetLabel(stateAttribute, [option].State.Value)

                        validStatusOptions.Add(New StatusOption() With {.StateLabel = stateLabel, .StateValue = [option].State.Value, .StatusLabel = statusLabel, .StatusValue = [option].Value.Value})
                    End If
                Next [option]
            End If
            Return validStatusOptions

        End Function
        '</snippetStateModelTransitions.GetValidStatusOptions>

        '<snippetStateModelTransitions.GetOptionSetLabel>
        ''' <summary>
        ''' Returns a string representing the label of an option in an optionset
        ''' </summary>
        ''' <param name="attribute">The metadata for an an attribute with options</param>
        ''' <param name="value">The value of the option</param>
        ''' <returns>The label for the option</returns>
        Public Function GetOptionSetLabel(ByVal attribute As EnumAttributeMetadata, ByVal value As Integer) As String
            Dim label As String = ""
            For Each [option] As OptionMetadata In attribute.OptionSet.Options
                If [option].Value.Value = value Then
                    Try
                        label = [option].Label.UserLocalizedLabel.Label
                    Catch e1 As Exception
                        label = [option].Label.LocalizedLabels(0).Label
                    End Try
                End If
            Next [option]
            Return label
        End Function
        '</snippetStateModelTransitions.GetOptionSetLabel>
#End Region ' Methods used in Sample
    End Class
    '<snippetStateModelTransitions.StatusOption>
    Public Class StatusOption
        Public Property StatusValue() As Integer
        Public Property StatusLabel() As String
        Public Property StateValue() As Integer
        Public Property StateLabel() As String
    End Class
    '</snippetStateModelTransitions.StatusOption>
End Namespace
'</snippetStateModelTransitions>