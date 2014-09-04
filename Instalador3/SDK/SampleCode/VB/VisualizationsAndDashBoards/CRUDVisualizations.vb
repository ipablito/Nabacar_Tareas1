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
'<snippetCRUDVisualization>
Imports System.ServiceModel
Imports System.ServiceModel.Description

' These namespaces are found in the Microsoft.Xrm.Sdk.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Xrm.Sdk
Imports Microsoft.Xrm.Sdk.Client
Imports Microsoft.Xrm.Sdk.Query
Imports Microsoft.Xrm.Sdk.Discovery
Imports Microsoft.Xrm.Sdk.Messages

' This namespace is found in Microsoft.Crm.Sdk.Proxy.dll assembly
' found in the SDK\bin folder.
Imports Microsoft.Crm.Sdk.Messages

Namespace Microsoft.Crm.Sdk.Samples
 ''' <summary>
 ''' This sample shows how to create retrieve, update, and delete an 
 ''' organization-owned visualization. The visualization will be attached
 ''' to the 'Opportunity' entity. As part of updating the visualization,
 ''' we will also set it to be the default visualization for the Opportunity
 ''' entity.
 ''' </summary>
 Public Class CRUDVisualization
#Region "Class Level Members"


  ''' <summary>
  ''' Stores the organization service proxy.
  ''' </summary>
  Private _serviceProxy As OrganizationServiceProxy

  ' Define the IDs needed for this sample.
  Private _orgOwnedVisualizationId As Guid
  Private _accountId As Guid
  Private _opportunitiyIds() As Guid

#End Region ' Class Level Members

#Region "How To Sample Code"
  ''' <summary>
  ''' Create and configure the organization service proxy.
  ''' Initiate the method to create any data that this sample requires.
  ''' Create an organization-owned visualization.
  ''' Retrieve the visualization.
  ''' Update the visualization; update the name and set it as the default
  ''' visualization for the Opportunity entity.
  ''' Optionally delete any entity records that were created for this sample.
  ''' </summary>
  ''' <param name="serverConfig">Contains server connection information.</param>
  ''' <param name="promptforDelete">When True, the user will be prompted to delete all
  ''' created entities.</param>

  Public Sub Run(ByVal serverConfig As ServerConnection.Configuration,
                 ByVal promptForDelete As Boolean)
   Try
    ' Connect to the Organization service. 
    ' The using statement assures that the service proxy will be properly disposed.
    _serviceProxy = ServerConnection.GetOrganizationProxy(serverConfig)
    Using _serviceProxy
     ' This statement is required to enable early-bound type support.
     _serviceProxy.EnableProxyTypes()


     ' Call the method to create any data that this sample requires.
     CreateRequiredRecords()

     '<snippetCRUDVisualization1>
     ' Create a visualization

     ' Set The presentation XML string.
     Dim presentationXml As String = "" & ControlChars.CrLf & _
         "<Chart Palette='BrightPastel'>" & ControlChars.CrLf & _
         "    <Series>" & ControlChars.CrLf & _
         "        <Series _Template_='All' ShadowOffset='2' " & ControlChars.CrLf & _
         "            BorderColor='64, 64, 64' BorderDashStyle='Solid'" & ControlChars.CrLf & _
         "            BorderWidth='1' IsValueShownAsLabel='true' " & ControlChars.CrLf & _
         "            Font='Tahoma, 6.75pt, GdiCharSet=0' " & ControlChars.CrLf & _
         "            LabelForeColor='100, 100, 100'" & ControlChars.CrLf & _
         "            CustomProperties='FunnelLabelStyle=Outside' " & ControlChars.CrLf & _
         "            ChartType='Funnel'>" & ControlChars.CrLf & _
         "            <SmartLabelStyle Enabled='True' />" & ControlChars.CrLf & _
         "            <Points />" & ControlChars.CrLf & _
         "        </Series>" & ControlChars.CrLf & _
         "     </Series>" & ControlChars.CrLf & _
         "    <ChartAreas>" & ControlChars.CrLf & _
         "        <ChartArea _Template_='All' BackColor='Transparent'" & ControlChars.CrLf & _
         "            BorderColor='Transparent' " & ControlChars.CrLf & _
         "            BorderDashStyle='Solid'>" & ControlChars.CrLf & _
         "            <Area3DStyle Enable3D='True' " & ControlChars.CrLf & _
         "                IsClustered='True'/>" & ControlChars.CrLf & _
         "        </ChartArea>" & ControlChars.CrLf & _
         "    </ChartAreas>" & ControlChars.CrLf & _
         "    <Legends>" & ControlChars.CrLf & _
         "        <Legend _Template_='All' Alignment='Center' " & ControlChars.CrLf & _
         "            LegendStyle='Table' Docking='Bottom' " & ControlChars.CrLf & _
         "            IsEquallySpacedItems='True' BackColor='White'" & ControlChars.CrLf & _
         "            BorderColor='228, 228, 228' BorderWidth='0' " & ControlChars.CrLf & _
         "            Font='Tahoma, 8pt, GdiCharSet=0' " & ControlChars.CrLf & _
         "            ShadowColor='0, 0, 0, 0' " & ControlChars.CrLf & _
         "            ForeColor='100, 100, 100'>" & ControlChars.CrLf & _
         "        </Legend>" & ControlChars.CrLf & _
         "    </Legends>" & ControlChars.CrLf & _
         "    <Titles>" & ControlChars.CrLf & _
         "        <Title _Template_='All'" & ControlChars.CrLf & _
         "            Font='Tahoma, 9pt, style=Bold, GdiCharSet=0'" & ControlChars.CrLf & _
         "            ForeColor='102, 102, 102'>" & ControlChars.CrLf & _
         "        </Title>" & ControlChars.CrLf & _
         "    </Titles>" & ControlChars.CrLf & _
         "    <BorderSkin PageColor='Control'" & ControlChars.CrLf & _
         "        BackColor='CornflowerBlue'" & ControlChars.CrLf & _
         "        BackSecondaryColor='CornflowerBlue' />" & ControlChars.CrLf & _
         "</Chart>"

     ' Set the data XML string.
     Dim dataXml As String = "" & ControlChars.CrLf & _
         "<datadefinition>" & ControlChars.CrLf & _
         "    <fetchcollection>" & ControlChars.CrLf & _
         "        <fetch mapping='logical' count='10' " & ControlChars.CrLf & _
         "            aggregate='true'>" & ControlChars.CrLf & _
         "            <entity name='opportunity'>" & ControlChars.CrLf & _
         "                <attribute name='actualvalue_base' " & ControlChars.CrLf & _
         "                    aggregate='sum' " & ControlChars.CrLf & _
         "                    alias='sum_actualvalue_base' />" & ControlChars.CrLf & _
         "                <attribute name='stepname' groupby='true' " & ControlChars.CrLf & _
         "                    alias='stepname' />" & ControlChars.CrLf & _
         "                <order alias='stepname' descending='false'/>" & ControlChars.CrLf & _
         "            </entity>" & ControlChars.CrLf & _
         "        </fetch>" & ControlChars.CrLf & _
         "    </fetchcollection>" & ControlChars.CrLf & _
         "    <categorycollection>" & ControlChars.CrLf & _
         "        <category>" & ControlChars.CrLf & _
         "            <measurecollection>" & ControlChars.CrLf & _
         "                <measure alias='sum_actualvalue_base'/>" & ControlChars.CrLf & _
         "            </measurecollection>" & ControlChars.CrLf & _
         "        </category>" & ControlChars.CrLf & _
         "    </categorycollection>" & ControlChars.CrLf & _
         "</datadefinition>"
     '<snippetCRUDVisualization2>
     ' Create the visualization entity instance.
     Dim newOrgOwnedVisualization As SavedQueryVisualization =
      New SavedQueryVisualization With {
       .Name = "Sample Visualization",
       .Description = "Sample organization-owned visualization.",
       .PresentationDescription = presentationXml,
       .DataDescription = dataXml,
       .PrimaryEntityTypeCode = Opportunity.EntityLogicalName,
       .IsDefault = False
      }
     _orgOwnedVisualizationId = _serviceProxy.Create(newOrgOwnedVisualization)
     '</snippetCRUDVisualization2>
     Console.WriteLine("Created {0}.", newOrgOwnedVisualization.Name)
     '</snippetCRUDVisualization1>

     ' Retrieve the visualization
     Dim retrievedOrgOwnedVisualization As SavedQueryVisualization =
      CType(_serviceProxy.Retrieve(SavedQueryVisualization.EntityLogicalName, _orgOwnedVisualizationId, New ColumnSet(True)), SavedQueryVisualization)
     Console.WriteLine("Retrieved the visualization.")

     ' Update the retrieved visualization
     ' 1.  Update the name.
     ' 2.  Update the data description string.                    

     Dim newDataXml As String =
      "<datadefinition>" & ControlChars.CrLf & _
      "    <fetchcollection>" & ControlChars.CrLf & _
      "        <fetch mapping='logical' count='10' " & ControlChars.CrLf & _
      "            aggregate='true'>" & ControlChars.CrLf & _
      "            <entity name='opportunity'>" & ControlChars.CrLf & _
      "                <attribute name='estimatedvalue_base' " & ControlChars.CrLf & _
      "                    aggregate='sum' " & ControlChars.CrLf & _
      "                    alias='sum_estimatedvalue_base' />" & ControlChars.CrLf & _
      "                <attribute name='name' " & ControlChars.CrLf & _
      "                    groupby='true' " & ControlChars.CrLf & _
      "                    alias='name' />" & ControlChars.CrLf & _
      "                <order alias='name' " & ControlChars.CrLf & _
      "                    descending='false'/>" & ControlChars.CrLf & _
      "            </entity>" & ControlChars.CrLf & _
      "        </fetch>" & ControlChars.CrLf & _
      "    </fetchcollection>" & ControlChars.CrLf & _
      "    <categorycollection>" & ControlChars.CrLf & _
      "        <category>" & ControlChars.CrLf & _
      "            <measurecollection>" & ControlChars.CrLf & _
      "                <measure alias='sum_estimatedvalue_base'/>" & ControlChars.CrLf & _
      "            </measurecollection>" & ControlChars.CrLf & _
      "        </category>" & ControlChars.CrLf & _
      "    </categorycollection>" & ControlChars.CrLf & _
      "</datadefinition>"

     retrievedOrgOwnedVisualization.Name = "Updated Sample Visualization"
     retrievedOrgOwnedVisualization.DataDescription = newDataXml

     _serviceProxy.Update(retrievedOrgOwnedVisualization)

     ' Publish the changes to the solution. This step is only required
     ' for organization-owned visualizations.
     Dim updateRequest As New PublishAllXmlRequest()
     _serviceProxy.Execute(updateRequest)

     Console.WriteLine("Updated the visualization.")

     DeleteRequiredRecords(promptForDelete)

    End Using
    ' Catch any service fault exceptions that Microsoft Dynamics CRM throws.
   Catch fe As FaultException(Of Microsoft.Xrm.Sdk.OrganizationServiceFault)
    ' You can handle an exception here or pass it back to the calling method.
    Throw
   End Try
  End Sub

  ''' <summary>
  ''' This method creates any entity records that this sample requires.
  ''' Create an account and few opportuntiy records for sample data 
  ''' for the chart.
  ''' </summary>
  Public Sub CreateRequiredRecords()
   ' Create a sample account
   Dim setupAccount As Account = New Account With {.Name = "Sample Account"}
   _accountId = _serviceProxy.Create(setupAccount)
   Console.WriteLine("Created {0}.", setupAccount.Name)

   ' Create some oppotunity records for the visualization
   Dim setupOpportunities() As Opportunity = {
    New Opportunity With {
      .Name = "Sample Opp 01",
      .EstimatedValue = New Money(120000D),
      .ActualValue = New Money(100000D),
      .CustomerId = New EntityReference(Account.EntityLogicalName, _accountId),
      .StepName = "Open"},
     New Opportunity With {
      .Name = "Sample Opp 02",
      .EstimatedValue = New Money(240000D),
      .ActualValue = New Money(200000D),
      .CustomerId = New EntityReference(Account.EntityLogicalName, _accountId),
      .StepName = "Open"},
     New Opportunity With {
      .Name = "Sample Opp 03",
      .EstimatedValue = New Money(360000D),
      .ActualValue = New Money(300000D),
      .CustomerId = New EntityReference(Account.EntityLogicalName, _accountId),
      .StepName = "Open"},
    New Opportunity With {
     .Name = "Sample Opp 04",
     .EstimatedValue = New Money(500000D),
     .ActualValue = New Money(500000D),
     .CustomerId = New EntityReference(Account.EntityLogicalName, _accountId),
     .StepName = "Open"},
    New Opportunity With {
     .Name = "Sample Opp 05",
     .EstimatedValue = New Money(110000D),
     .ActualValue = New Money(60000D),
     .CustomerId = New EntityReference(Account.EntityLogicalName, _accountId),
     .StepName = "Open"},
    New Opportunity With {
     .Name = "Sample Opp 06",
     .EstimatedValue = New Money(90000D),
     .ActualValue = New Money(70000D),
     .CustomerId = New EntityReference(Account.EntityLogicalName, _accountId),
     .StepName = "Open"},
    New Opportunity With {
     .Name = "Sample Opp 07",
     .EstimatedValue = New Money(620000D),
     .ActualValue = New Money(480000D),
     .CustomerId = New EntityReference(Account.EntityLogicalName, _accountId),
     .StepName = "Open"},
    New Opportunity With {
     .Name = "Sample Opp 08",
     .EstimatedValue = New Money(440000D),
     .ActualValue = New Money(400000D),
     .CustomerId = New EntityReference(Account.EntityLogicalName, _accountId),
     .StepName = "Open"},
    New Opportunity With {
     .Name = "Sample Opp 09",
     .EstimatedValue = New Money(410000D),
     .ActualValue = New Money(400000D),
     .CustomerId = New EntityReference(Account.EntityLogicalName, _accountId),
     .StepName = "Open"},
    New Opportunity With {
     .Name = "Sample Opp 10",
     .EstimatedValue = New Money(650000D),
     .ActualValue = New Money(650000D),
     .CustomerId = New EntityReference(Account.EntityLogicalName, _accountId),
     .StepName = "Open"}
   }

   _opportunitiyIds = ( _
       From opp In setupOpportunities _
       Select _serviceProxy.Create(opp)).ToArray()

   Console.WriteLine("Created few opportunity records for {0}.", setupAccount.Name)

   Return
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
    _serviceProxy.Delete(SavedQueryVisualization.EntityLogicalName, _orgOwnedVisualizationId)
    _serviceProxy.Delete("account", _accountId)
    Console.WriteLine("Entity records have been deleted.")
   End If
  End Sub


#End Region ' How To Sample Code

#Region "Main"
  ''' <summary>
  ''' Main. Runs the sample and provides error output.
  ''' <param name="args">Array of arguments to Main method.</param>
  ''' </summary>
  Public Shared Sub Main(ByVal args() As String)
   Try
    ' Obtain the target organization's Web address and client logon 
    ' credentials from the user.
    Dim serverConnect As New ServerConnection()
    Dim config As ServerConnection.Configuration = serverConnect.GetServerConfiguration()

    Dim app As New CRUDVisualization()
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
 End Class
End Namespace
'</snippetCRUDVisualization>
