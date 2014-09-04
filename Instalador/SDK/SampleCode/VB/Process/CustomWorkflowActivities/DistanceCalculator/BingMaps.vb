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
Imports System.Runtime.Serialization

Namespace Microsoft.Crm.Sdk.Samples.BingMapsRestV1
	<DataContract> _
	Public Class Address
		<DataMember(Name := "addressLine")> _
		Public Property AddressLine() As String
		<DataMember(Name := "adminDistrict")> _
		Public Property AdminDistrict() As String
		<DataMember(Name := "adminDistrict2")> _
		Public Property AdminDistrict2() As String
		<DataMember(Name := "countryRegion")> _
		Public Property CountryRegion() As String
		<DataMember(Name := "formattedAddress")> _
		Public Property FormattedAddress() As String
		<DataMember(Name := "locality")> _
		Public Property Locality() As String
		<DataMember(Name := "postalCode")> _
		Public Property PostalCode() As String
	End Class

	Public Enum AuthenticationResultCode
		None
		NoCredentials
		ValidCredentials
		InvalidCredentials
		CredentialsExpired
		NotAuthorized
	End Enum

	<DataContract> _
	Public Class BoundingBox
		<DataMember(Name := "southLatitude")> _
		Public Property SouthLatitude() As Double
		<DataMember(Name := "westLongitude")> _
		Public Property WestLongitude() As Double
		<DataMember(Name := "northLatitude")> _
		Public Property NorthLatitude() As Double
		<DataMember(Name := "eastLongitude")> _
		Public Property EastLongitude() As Double
	End Class

	Public Enum Confidence
		High
		Medium
		Low
		Unknown
	End Enum

	<DataContract(Namespace := "http://schemas.microsoft.com/search/local/ws/rest/v1")> _
	Public Class DataflowJob
		Inherits Resource
		<DataMember(Name := "completedDate")> _
		Public Property CompletedDate() As String
		<DataMember(Name := "createdDate")> _
		Public Property CreatedDate() As String
		<DataMember(Name := "status")> _
		Public Property Status() As String
		<DataMember(Name := "errorMessage")> _
		Public Property ErrorMessge() As String
		<DataMember(Name := "failedEntityCount")> _
		Public Property FailedEntityCount() As Integer
		<DataMember(Name := "processedEntityCount")> _
		Public Property ProcessedEntityCount() As Integer
		<DataMember(Name := "totalEntityCount")> _
		Public Property TotalEntityCount() As Integer
	End Class

	<DataContract> _
	Public Class Hint
		<DataMember(Name := "hintType")> _
		Public Property HintType() As String

		<DataMember(Name := "value")> _
		Public Property Value() As String
	End Class

	<DataContract> _
	Public Class Instruction
		<DataMember(Name := "maneuverType")> _
		Public Property ManeuverType() As String
		<DataMember(Name := "text")> _
		Public Property Text() As String
		'[DataMember(Name = "value")]
		'public string Value { get; set; }
	End Class

	<DataContract> _
	Public Class ItineraryItem
		<DataMember(Name := "travelMode")> _
		Public Property TravelMode() As String
		<DataMember(Name := "travelDistance")> _
		Public Property TravelDistance() As Double
		<DataMember(Name := "travelDuration")> _
		Public Property TravelDuration() As Long
		<DataMember(Name := "maneuverPoint")> _
		Public Property ManeuverPoint() As Point
		<DataMember(Name := "instruction")> _
		Public Property Instruction() As Instruction
		<DataMember(Name := "compassDirection")> _
		Public Property CompassDirection() As String
		<DataMember(Name := "hint")> _
		Public Property Hint() As Hint()
		<DataMember(Name := "warning")> _
		Public Property Warning() As Warning()
	End Class

	<DataContract> _
	Public Class Line
		<DataMember(Name := "point")> _
		Public Property Point() As Point()
		<DataMember(Name := "coordinates")> _
		Public Property Coordinates() As Double()()
	End Class

	<DataContract> _
	Public Class Link
		<DataMember(Name := "role")> _
		Public Property Role() As String
		<DataMember(Name := "name")> _
		Public Property Name() As String
		<DataMember(Name := "value")> _
		Public Property Value() As String
		<DataMember(Name := "url")> _
		Public Property Url() As String
	End Class

	<DataContract(Namespace := "http://schemas.microsoft.com/search/local/ws/rest/v1")> _
	Public Class Location
		Inherits Resource
		<DataMember(Name := "entityType")> _
		Public Property EntityType() As String
		<DataMember(Name := "address")> _
		Public Property Address() As Address
		<DataMember(Name := "confidence")> _
		Public Property Confidence() As String
	End Class

	<DataContract> _
	Public Class Point
		Inherits Shape
		''' <summary>
		''' Latitude,Longitude
		''' </summary>
		<DataMember(Name := "coordinates")> _
		Public Property Coordinates() As Double()
		'[DataMember(Name = "latitude")]
		'public double Latitude { get; set; }
		'[DataMember(Name = "longitude")]
		'public double Longitude { get; set; }
	End Class

	<DataContract, KnownType(GetType(Location)), KnownType(GetType(Route)), KnownType(GetType(DataflowJob))> _
	Public Class Resource
		<DataMember(Name := "name")> _
		Public Property Name() As String
		<DataMember(Name := "id")> _
		Public Property Id() As String
		<DataMember(Name := "link")> _
		Public Property Link() As Link()
		<DataMember(Name := "links")> _
		Public Property Links() As Link()
		<DataMember(Name := "point")> _
		Public Property Point() As Point
		<DataMember(Name := "boundingBox")> _
		Public Property BoundingBox() As BoundingBox
	End Class

	<DataContract> _
	Public Class ResourceSet
		<DataMember(Name := "estimatedTotal")> _
		Public Property EstimatedTotal() As Long
		<DataMember(Name := "resources")> _
		Public Property Resources() As Resource()
	End Class

	<DataContract> _
	Public Class Response
		<DataMember(Name := "copyright")> _
		Public Property Copyright() As String
		<DataMember(Name := "brandLogoUri")> _
		Public Property BrandLogoUri() As String
		<DataMember(Name := "statusCode")> _
		Public Property StatusCode() As Integer
		<DataMember(Name := "statusDescription")> _
		Public Property StatusDescription() As String
		<DataMember(Name := "authenticationResultCode")> _
		Public Property AuthenticationResultCode() As String
		<DataMember(Name := "errorDetails")> _
		Public Property errorDetails() As String()
		<DataMember(Name := "traceId")> _
		Public Property TraceId() As String
		<DataMember(Name := "resourceSets")> _
		Public Property ResourceSets() As ResourceSet()
	End Class

	<DataContract(Namespace := "http://schemas.microsoft.com/search/local/ws/rest/v1")> _
	Public Class Route
		Inherits Resource
		<DataMember(Name := "distanceUnit")> _
		Public Property DistanceUnit() As String
		<DataMember(Name := "durationUnit")> _
		Public Property DurationUnit() As String
		<DataMember(Name := "travelDistance")> _
		Public Property TravelDistance() As Double
		<DataMember(Name := "travelDuration")> _
		Public Property TravelDuration() As Long
		<DataMember(Name := "routeLegs")> _
		Public Property RouteLegs() As RouteLeg()
		<DataMember(Name := "routePath")> _
		Public Property RoutePath() As RoutePath
	End Class

	<DataContract> _
	Public Class RouteLeg
		<DataMember(Name := "travelDistance")> _
		Public Property TravelDistance() As Double
		<DataMember(Name := "travelDuration")> _
		Public Property TravelDuration() As Long
		<DataMember(Name := "actualStart")> _
		Public Property ActualStart() As Point
		<DataMember(Name := "actualEnd")> _
		Public Property ActualEnd() As Point
		<DataMember(Name := "startLocation")> _
		Public Property StartLocation() As Location
		<DataMember(Name := "endLocation")> _
		Public Property EndLocation() As Location
		<DataMember(Name := "itineraryItems")> _
		Public Property ItineraryItems() As ItineraryItem()
	End Class

	<DataContract> _
	Public Class RoutePath
		<DataMember(Name := "line")> _
		Public Property Line() As Line
	End Class

	<DataContract, KnownType(GetType(Point))> _
	Public Class Shape
		<DataMember(Name := "boundingBox")> _
		Public Property BoundingBox() As Double()
	End Class
	<DataContract> _
	Public Class Warning
		<DataMember(Name := "warningType")> _
		Public Property WarningType() As String
		<DataMember(Name := "severity")> _
		Public Property Severity() As String
		<DataMember(Name := "value")> _
		Public Property Value() As String
	End Class

End Namespace