' =====================================================================
'  File:		ErrorCodes.vb
'  Summary:	Contains helper types for building queries.
' =====================================================================
'
'  This file is part of the Microsoft CRM V4 SDK Code Samples.
'
'  Copyright (C) 2007 Microsoft Corporation.  All rights reserved.
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
Imports System
Imports System.Collections

Namespace CrmSdk
    Public NotInheritable Class ErrorCodes
        ' To prevent instantiation
        Private Sub New()
        End Sub

        Private Shared ErrorMessages As New Hashtable()

        Shared Sub New()
            ErrorMessages.Add(CustomImageAttributeOnlyAllowedOnCustomEntity, "A custom image attribute can only be added to a custom entity.")
            ErrorMessages.Add(SqlEncryptionSymmetricKeyCannotOpenBecauseWrongPassword, "Cannot open encryption Symmetric Key because the password is wrong.")
            ErrorMessages.Add(SqlEncryptionSymmetricKeyDoesNotExistOrNoPermission, "Cannot open encryption Symmetric Key because it does not exist in the database or user does not have permission.")
            ErrorMessages.Add(SqlEncryptionSymmetricKeyPasswordDoesNotExistInConfigDB, "Encryption Symmetric Key password does not exist in Config DB.")
            ErrorMessages.Add(SqlEncryptionSymmetricKeySourceDoesNotExistInConfigDB, "Encryption Symmetric Key Source does not exist in Config DB.")
            ErrorMessages.Add(CannotExecuteRequestBecauseHttpsIsRequired, "HTTPS protocol is required for this type of request, please enable HTTPS protocol and try again.")
            ErrorMessages.Add(SqlEncryptionRestoreEncryptionKeyCannotDecryptExistingData, "Cannot perform 'activate' because the encryption key doesn’t match the original encryption key that was used to encrypt the data.")
            ErrorMessages.Add(SqlEncryptionSetEncryptionKeyIsAlreadyRunningCannotRunItInParallel, "The system is currently running a request to 'change' or 'activate' the encryption key. Please wait before making another request.")
            ErrorMessages.Add(SqlEncryptionChangeEncryptionKeyExceededQuotaForTheInterval, "'Change' encryption key has already been executed {0} times in the last {1} minutes. Please wait a couple of minutes and then try again.")
            ErrorMessages.Add(SqlEncryptionEncryptionKeyValidationError, "The new encryption key does not meet the strong encryption key requirements. The key must be between 10 and 100 characters in length, and must have at least one numeral, at least one letter, and one symbol or special character. {0}")
            ErrorMessages.Add(SqlEncryptionIsInactiveCannotChangeEncryptionKey, "Cannot perform 'change' encryption key because the encryption key is not already set or is not working. First use 'activate' encryption key instead to set the correct current encryption key and then use 'change' encryption if you want to re-encrypt data using a new encryption key.")
            ErrorMessages.Add(SqlEncryptionDeleteEncryptionKeyError, "Cannot delete the encryption key.")
            ErrorMessages.Add(SqlEncryptionIsActiveCannotRestoreEncryptionKey, "Cannot perform 'activate' encryption key because the encryption key is already set and is working. Use 'change' encryption key instead.")
            ErrorMessages.Add(SqlEncryptionKeyCannotDecryptExistingData, "Cannot decrypt existing encrypted data (Entity='{0}', Attribute='{1}') using the current encryption key. Use 'activate' encryption key to set the correct encryption key.")
            ErrorMessages.Add(SqlEncryptionEncryptionDecryptionTestError, "Error while testing data encryption and decryption.")
            ErrorMessages.Add(SqlEncryptionDeleteSymmetricKeyError, "Cannot delete Symmetric Key with Name='{0}' because it does not exist.")
            ErrorMessages.Add(SqlEncryptionCreateSymmetricKeyError, "Cannot create Symmetric Key with Name='{0}' because it already exists.")
            ErrorMessages.Add(SqlEncryptionSymmetricKeyDoesNotExist, "Symmetric Key with Name='{0}' does not exist in the database.")
            ErrorMessages.Add(SqlEncryptionDeleteCertificateError, "Cannot delete Certificate with Name='{0}' because it does not exist.")
            ErrorMessages.Add(SqlEncryptionCreateCertificateError, "Cannot create Certificate with Name='{0}' because it already exists.")
            ErrorMessages.Add(SqlEncryptionCertificateDoesNotExist, "Certificate with Name='{0}' does not exist in the database.")
            ErrorMessages.Add(SqlEncryptionDeleteDatabaseMasterKeyError, "Cannot delete Database Master Key because it does not exist.")
            ErrorMessages.Add(SqlEncryptionCreateDatabaseMasterKeyError, "Cannot create Database Master Key because already exists.")
            ErrorMessages.Add(SqlEncryptionCannotOpenSymmetricKeyBecauseDatabaseMasterKeyDoesNotExistOrIsNotOpened, "Cannot open Symmetric Key because Database Master Key does not exist in the database or is not opened.")
            ErrorMessages.Add(SqlEncryptionDatabaseMasterKeyDoesNotExist, "Database Master Key does not exist in the database.")
            ErrorMessages.Add(SqlEncryption, "There was an error in Data Encryption.")
            ErrorMessages.Add(ManifestParsingFailure, "Failed to parse the specified manifest file to retrieve OrganizationId")
            ErrorMessages.Add(InvalidManifestFilePath, "Failed to locate the manifest file in the specified location")
            ErrorMessages.Add(OnPremiseRestoreOrganizationManifestFailed, "Failed to restore Organization's configdb state from manifest")
            ErrorMessages.Add(InvalidAuth, "Organization Authentication does not match the current discovery service Role.")
            ErrorMessages.Add(CannotUpdateOrgDBOrgSettingWhenOffline, "Organization Settings stored in Organization Database cannot be set when offline.")
            ErrorMessages.Add(InvalidOrgDBOrgSetting, "Invalid Organization Setting passed in. Please check the datatype and pass in an appropriate value.")
            ErrorMessages.Add(UnknownInvalidTransformationParameterGeneric, "One or more input transformation parameter values are invalid: {0}.")
            ErrorMessages.Add(InvalidTransformationParameterOutsideRangeGeneric, "One or more input transformation parameter values are outside the permissible range: {0}.")
            ErrorMessages.Add(InvalidTransformationParameterEmptyCollection, "The transformation parameter: {0} has an invalid input value length: {1}. The parameter length cannot be an empty collection.")
            ErrorMessages.Add(InvalidTransformationParameterOutsideRange, "The transformation parameter: {0} has an invalid input value: {1}. The parameter is out of the permissible range: {2}. ")
            ErrorMessages.Add(InvalidTransformationParameterZeroToRange, "The transformation parameter: {0} has an invalid input value: {1}. The parameter value must be greater than 0 and less than the length of the parameter 1.")
            ErrorMessages.Add(InvalidTransformationParameterString, "The transformation parameter: {0} has an invalid input value: {1}. The parameter must be a string that is not empty.")
            ErrorMessages.Add(InvalidTransformationParametersGeneric, "The transformation parameter: {0} has an invalid input value: {1}. The parameter must be of type: {2}.")
            ErrorMessages.Add(InsufficientTransformationParameters, "Insufficient parameters to execute transformation mapping.")
            ErrorMessages.Add(MaximumNumberHandlersExceeded, "This solution adds form event handlers so the number of event handlers for a form event exceeds the maximum number.")
            ErrorMessages.Add(ErrorInUnzipAlternate, "An error occurred while the uploaded .zip file was being extracted. Try to upload the file again. If the problem persists, contact your system administrator.")
            ErrorMessages.Add(IncorrectSingleFileMultipleEntityMap, "There should be two or more Entity Mappings defined when EntitiesPerFile in ImportMap is set to Multiple")
            ErrorMessages.Add(ActivityEntityCannotBeActivityParty, "An activity entity cannot be also an activity party")
            ErrorMessages.Add(TargetAttributeInvalidForIgnore, "Target attribute name should be empty when the processcode is ignore.")
            ErrorMessages.Add(MaxUnzipFolderSizeExceeded, "File cannot be unzipped because it exceeded the maximum server storage size limit. Please try again.")
            ErrorMessages.Add(InvalidMultipleMapping, "A source field is mapped to more than one CRM fields of lookup/picklist type.")
            ErrorMessages.Add(ErrorInStoringImportFile, "An error occurred while storing the import file in database.")
            ErrorMessages.Add(UnzipTimeout, "Timeout happened in unzipping the zip file uploaded for import.")
            ErrorMessages.Add(UnsupportedZipFileForImport, "The structure of the zip file is not support for import.")
            ErrorMessages.Add(UnzipProcessCountLimitReached, "Cannot start a new process to unzip.")
            ErrorMessages.Add(AttachmentNotFound, "The reference to the attachment could not be found.")
            ErrorMessages.Add(TooManyPicklistValues, "Number of distinct picklist values exceed the limit.")
            ErrorMessages.Add(VeryLargeFileInZipImport, "One of the files in the .zip file that you are trying to import exceeds the size limit.")
            ErrorMessages.Add(InvalidAttachmentsFolder, "The .zip file cannot be uploaded because the folder ""Attachments"" contains one or more subfolders. Remove the subfolders and try again.")
            ErrorMessages.Add(ZipInsideZip, "The zip file that you are trying to upload contains another zip file within it.")
            ErrorMessages.Add(InvalidZipFileFormat, "The file that you are trying to upload is not a valid file. Check the file and try again.")
            ErrorMessages.Add(EmptyFileForImport, "The selected file contains no data.")
            ErrorMessages.Add(EmptyFilesInZip, "One or more files in the .zip file do not contain data. Check the files and try again.")
            ErrorMessages.Add(ZipFileHasMixOfCsvAndXmlFiles, "The zip file that you are trying to upload contains both CSV and XML Spreadsheet 2003 format. The zip file can contain either CSV files or XML Spreadsheet 2003 files, but not both.")
            ErrorMessages.Add(DuplicateFileNamesInZip, "Two or more files have the same name. File names must be unique.")
            ErrorMessages.Add(ErrorInUnzip, "An error occurred while unzipping the selected file.Try importing file again.If this problem persists, contact your system administrator.")
            ErrorMessages.Add(InvalidZipFileForImport, "Unzip file has invalid files for import.Zip file should contain either csv or xml files.")
            ErrorMessages.Add(InvalidLookupMapNode, "The lookup entity provided is not valid for the given target attribute.")
            ErrorMessages.Add(ImportMailMergeTemplateEntityMissingError, "The {0} mail merge template was not imported because the {1} entity associated with this template is not in the target system.")
            ErrorMessages.Add(CannotUpdateOpportunityCurrency, "The currency cannot be changed because this opportunity has Products Quotes, and/ or Orders associated with it.  If you want to change the currency please delete all of the Products/quotes/orders and then change the currency or create a new opportunity with the appropriate currency.")
            ErrorMessages.Add(ParentRecordAlreadyExists, "This record cannot be added because it already has a parent record.")
            ErrorMessages.Add(MissingWebToLeadRedirect, "The redirectto is missing for web2lead redirect.")
            ErrorMessages.Add(InvalidWebToLeadRedirect, "The redirectto is invalid for web2lead redirect.")
            ErrorMessages.Add(TemplateNotAllowedForInternetMarketing, "Creating Templates with Internet Marketing Campaign Activities is not allowed")
            ErrorMessages.Add(CopyNotAllowedForInternetMarketing, "Duplicating campaigns that have Internet Marketing Campaign Activities is not allowed")
            ErrorMessages.Add(MissingOrInvalidRedirectId, "The RedirId parameter is missing for the partner redirect.")
            ErrorMessages.Add(ImportNotComplete, "One or more imports are not in completed state. Imported records can only be deleted from completed jobs. Wait until job completes, and then try again.")
            ErrorMessages.Add(UIDataMissingInWorkflow, "The workflow does not contain UIData.")
            ErrorMessages.Add(RefEntityRelationshipRoleRequired, "The entity relationship role of the referencing entity is required when creating a new one-to-many entity relationship.")
            ErrorMessages.Add(ImportTemplateLanguageIgnored, "You cannot import this template because its language is not enabled in your Microsoft Dynamics CRM organization.")
            ErrorMessages.Add(ImportTemplatePersonalIgnored, "You cannot import this template because it is set as ""personal"" in your Microsoft Dynamics CRM organization.")
            ErrorMessages.Add(ImportComponentDeletedIgnored, "You cannot update this component because it does not exist in this Microsoft Dynamics CRM organization.")
            ErrorMessages.Add(RelationshipRoleNodeNumberInvalid, "There must be two entity relationship role nodes when creating a new many-to-many entity relationship.")
            ErrorMessages.Add(AssociationRoleOrdinalInvalid, "The association role ordinal is not valid - it must be 1 or 2.")
            ErrorMessages.Add(RelationshipRoleMismatch, "The relationship role name {0} does not match either expected entity name of {1} or {2}.")
            ErrorMessages.Add(ImportMapInUse, "One or more of the selected data maps cannot be deleted because it is currently used in a data import.")
            ErrorMessages.Add(PreviousOperationNotComplete, "An operation on which this operation depends did not complete successfully.")
            ErrorMessages.Add(TransformationResumeNotSupported, "The resume/retry of Transformation job of Import is not supported.")
            ErrorMessages.Add(CannotDisableDuplicateDetection, "Duplicate detection cannot be disabled because a duplicate detection job is currently in progress. Try again later.")
            ErrorMessages.Add(TargetEntityNotMapped, "Target Entity Name not defined for source:{0} file.")
            ErrorMessages.Add(BulkDeleteChildFailure, "One of the Bulk Delete Child Jobs Failed")
            ErrorMessages.Add(CannotRemoveNonListMember, "Specified Item not a member of the specified List.")
            ErrorMessages.Add(JobNameIsEmptyOrNull, "Job Name can not be null or empty.")
            ErrorMessages.Add(ImportMailMergeTemplateError, "There was an error in parsing the mail merge templates in Import Xml")
            ErrorMessages.Add(ErrorsInWorkflowDefinition, "The selected workflow has errors and cannot be published. Please open the workflow, remove the errors and try again.")
            ErrorMessages.Add(DistributeNoListAssociated, "This campaign activity cannot be distributed. No marketing lists are associated with it. Add at least one marketing list and try again.")
            ErrorMessages.Add(DistributeListAssociatedVary, "This campaign activity cannot be distributed. Mail merge activities can be done only on marketing lists that are all the same record type. For this campaign activity, remove marketing lists so that the remaining ones are the same record type, and then try again.")
            ErrorMessages.Add(OfflineFilterParentDownloaded, "You cannot use the Parent Downloaded condition in a local data group.")
            ErrorMessages.Add(OfflineFilterNestedDateTimeOR, "You cannot use nested date time conditions within an OR clause in a local data group.")
            ErrorMessages.Add(DuplicateOfflineFilter, "You can create only one local data group for each record type.")
            ErrorMessages.Add(CannotAssignAddressBookFilters, "Cannot assign address book filters")
            ErrorMessages.Add(CannotCreateAddressBookFilters, "Cannot create address book filters")
            ErrorMessages.Add(CannotGrantAccessToAddressBookFilters, "Cannot grant access to address book filters")
            ErrorMessages.Add(CannotModifyAccessToAddressBookFilters, "Cannot modify access for address book filters")
            ErrorMessages.Add(CannotRevokeAccessToAddressBookFilters, "Cannot revoke access for address book filters")
            ErrorMessages.Add(DuplicateMapName, "A data map with the specified name already exists.")
            ErrorMessages.Add(InvalidWordXmlFile, "Only Microsoft Word xml format files can be uploaded.")
            ErrorMessages.Add(FileNotFound, "The attachment file was not found.")
            ErrorMessages.Add(MultipleFilesFound, "The attachment file name is not unique.")
            ErrorMessages.Add(InvalidAttributeMapping, "One or more attribute mappings is invalid.")
            ErrorMessages.Add(FileReadError, "There was an error reading the file from the file system. Make sure you have read permission for this file, and then try migrating the file again.")
            ErrorMessages.Add(ViewForDuplicateDetectionNotDefined, "Required view for viewing duplicates of an entity not defined.")
            ErrorMessages.Add(FileInUse, "Could not read the file because another application is using the file.")
            ErrorMessages.Add(NoPublishedDuplicateDetectionRules, "There are no published duplicate detection rules in the system. To run duplicate detection, you must create and publish one or more rules.")
            ErrorMessages.Add(NoEntitiesForBulkDelete, "The Bulk Delete Wizard cannot be opened because there are no valid entities for deletion.")
            ErrorMessages.Add(BulkDeleteRecordDeletionFailure, "The record cannot be deleted.")
            ErrorMessages.Add(RuleAlreadyPublishing, "The selected duplicate detection rule is already being published.")
            ErrorMessages.Add(RuleNotFound, "No rules were found that match the criteria.")
            ErrorMessages.Add(CannotDeleteSystemEmailTemplate, "System e-mail templates cannot be deleted.")
            ErrorMessages.Add(EntityDupCheckNotSupportedSystemWide, "Duplicate detection is not enabled for one or more of the selected entities. The duplicate detection job cannot be started.")
            ErrorMessages.Add(DuplicateDetectionNotSupportedOnAttributeType, "The rule condition cannot be created or updated because duplicate detection is not supported on the data type of the selected attribute.")
            ErrorMessages.Add(MaxMatchCodeLengthExceeded, "The rule condition cannot be created or updated because it would cause the matchcode length to exceed the maximum limit.")
            ErrorMessages.Add(CannotDeleteUpdateInUseRule, "The duplicate detection rule is currently in use and cannot be updated or deleted. Please try again later.")
            ErrorMessages.Add(ImportMappingsInvalidIdSpecified, "The XML file has one or more invalid IDs. The specified ID cannot be used as a unique identifier.")
            ErrorMessages.Add(NotAWellFormedXml, "The input XML is not well-formed XML.")
            ErrorMessages.Add(NoncompliantXml, "The input XML does not comply with the XML schema.")
            ErrorMessages.Add(DuplicateDetectionTemplateNotFound, "Microsoft Dynamics CRM could not retrieve the e-mail notification template.")
            ErrorMessages.Add(RulesInInconsistentStateFound, "One or more rules cannot be unpublished, either because they are in the process of being published, or are in a state where they cannot be unpublished.")
            ErrorMessages.Add(BulkDetectInvalidEmailRecipient, "The e-mail recipient either does not exist or the e-mail address for the e-mail recipient is not valid.")
            ErrorMessages.Add(CannotEnableDuplicateDetection, "Duplicate detection cannot be enabled because one or more rules are being published.")
            ErrorMessages.Add(CannotDeleteInUseEntity, "The selected entity cannot be deleted because it is referenced by one or more duplicate detection rules that are in process of being published.")
            ErrorMessages.Add(StringAttributeIndexError, "One of the attributes of the selected entity is a part of database index and so it cannot be greater than 900 bytes.")
            ErrorMessages.Add(CannotChangeAttributeRequiredLevel, "An attribute's required level cannot be changed from SystemRequired")
            ErrorMessages.Add(MaximumNumberOfAttributesForEntityReached, "The maximum number of attributes allowed for an entity has already been reached. The attribute cannot be created.")
            ErrorMessages.Add(CannotPublishMoreRules, "The selected record type already has the maximum number of published rules. Unpublish or delete existing rules for this record type, and then try again.")
            ErrorMessages.Add(CannotDeleteInUseAttribute, "The selected attribute cannot be deleted because it is referenced by one or more duplicate detection rules that are being published.")
            ErrorMessages.Add(CannotDeleteInUseOptionSet, "This option set cannot be deleted. The current set of entities that reference this option set are: {0}. These references must be removed before this option set can be deleted")
            ErrorMessages.Add(InvalidEntityName, "The record type does not match the base record type and the matching record type of the duplicate detection rule.")
            ErrorMessages.Add(InvalidOperatorCode, "The operator is not valid or it is not supported.")
            ErrorMessages.Add(CannotPublishEmptyRule, "No criteria have been specified. Add criteria, and then publish the duplicate detection rule.")
            ErrorMessages.Add(CannotPublishInactiveRule, "The selected duplicate detection rule is marked as Inactive. Before publishing, you must activate the rule.")
            ErrorMessages.Add(DuplicateCheckNotEnabled, "Duplicate detection is not enabled. To enable duplicate detection, click Settings, click Data Management, and then click Duplicate Detection Settings.")
            ErrorMessages.Add(DuplicateCheckNotSupportedOnEntity, "Duplicate detection is not supported on this record type.")
            ErrorMessages.Add(InvalidStateCodeStatusCode, "State code is invalid or state code is valid but status code is invalid for a specified state code.")
            ErrorMessages.Add(SyncToMsdeFailure, "Failed to start or connect to the offline mode MSDE database.")
            ErrorMessages.Add(FormDoesNotExist, "Form doesn't exist")
            ErrorMessages.Add(AccessDenied, "Access is denied.")
            ErrorMessages.Add(CannotDeleteOptionSet, "The selected OptionSet cannot be deleted")
            ErrorMessages.Add(InvalidOptionSetOperation, "Invalid OptionSet")
            ErrorMessages.Add(OptionValuePrefixOutOfRange, "CustomizationOptionValuePrefix must be a number between {0} and {1}")
            ErrorMessages.Add(CheckPrivilegeGroupForUserOnPremiseError, "Please select an account that is a member of the PrivUserGroup security group and try again.")
            ErrorMessages.Add(CheckPrivilegeGroupForUserOnSplaError, "Please select a CRM System Administrator account that belongs to the root business unit and try again.")
            ErrorMessages.Add(unManagedIdsAccessDenied, "Not enough privilege to access the Microsoft Dynamics CRM object or perform the requested operation.")
            ErrorMessages.Add(EntityIsIntersect, "The specified entity is intersect entity")
            ErrorMessages.Add(CannotDeleteTeamOwningRecords, "Can't delete a team which owns records. Reassign the records and try again.")
            ErrorMessages.Add(CannotRemoveMembersFromDefaultTeam, "Can't remove members from the default business unit team.")
            ErrorMessages.Add(CannotAddMembersToDefaultTeam, "Can't add members to the default business unit team.")
            ErrorMessages.Add(CannotUpdateNameDefaultTeam, "The default business unit team name can't be updated.")
            ErrorMessages.Add(CannotSetParentDefaultTeam, "The default business unit team parent can't be set.")
            ErrorMessages.Add(CannotDeleteDefaultTeam, "The default business unit team can't be deleted.")
            ErrorMessages.Add(TeamNameTooLong, "The specified name for the team is too long.")
            ErrorMessages.Add(CannotAssignRolesOrProfilesToAccessTeam, "Cannot assign roles or profiles to an access team.")
            ErrorMessages.Add(TooManyEntitiesEnabledForAutoCreatedAccessTeams, "Too many entities enabled for auto created access teams.")
            ErrorMessages.Add(TooManyTeamTemplatesForEntityAccessTeams, "Too many team templates for auto created access teams for this entity.")
            ErrorMessages.Add(EntityNotEnabledForAutoCreatedAccessTeams, "This entity is not enabled for auto created access teams.")
            ErrorMessages.Add(InvalidAccessMaskForTeamTemplate, "Invalid access mask is specified for team template.")
            ErrorMessages.Add(CannotChangeTeamTypeDueToRoleOrProfile, "You cannot modify the type of the team because there are security roles or field security profiles assigned to the team.")
            ErrorMessages.Add(CannotChangeTeamTypeDueToOwnership, "You cannot modify the type of the team because there are records owned by the team.")
            ErrorMessages.Add(CannotDisableAutoCreateAccessTeams, "You cannot disable the auto create access team setting while there are associated team templates.")
            ErrorMessages.Add(CannotShareSystemManagedTeam, "You can't share or unshare a record with a system-generated access team.")
            ErrorMessages.Add(CannotAssignToAccessTeam, "You cannot assign a record to the access team. You can assign a record to the owner team.")
            ErrorMessages.Add(DuplicateSalesTeamMember, "The user you're trying to add is already a member of the sales team.")
            ErrorMessages.Add(TargetUserInsufficientPrivileges, """The user can�t be added to the team because the user has insufficient privileges on the entity. The minimum access level the user must have is User.")
            ErrorMessages.Add(InvalidDomainName, "The domain logon for this user is invalid. Select another domain logon and try again.")
            ErrorMessages.Add(InvalidUserName, "You must enter the user name in the format <name>@<domain>. Correct the format and try again.")
            ErrorMessages.Add(BulkMailServiceNotAccessible, "The Microsoft Dynamics CRM Bulk E-Mail Service is not running.")
            ErrorMessages.Add(RSMoveItemError, "Cannot move report item {0} to {1}")
            ErrorMessages.Add(ReportParentChildNotCustomizable, "The report could not be updated because either the parent report or the child report is not customizable.")
            ErrorMessages.Add(ConvertFetchDataSetError, "An unexpected error occurred while processing the Fetch data set.")
            ErrorMessages.Add(ConvertReportToCrmError, "An unexpected error occurred while converting supplied report to CRM format.")
            ErrorMessages.Add(ReportViewerError, "An error occurred during report rendering.ReportId:{0}")
            ErrorMessages.Add(RSGetItemTypeError, "Error occurred while fetching the report.")
            ErrorMessages.Add(RSSetPropertiesError, "Error occurred while setting property values for the report.")
            ErrorMessages.Add(RSReportParameterTypeMismatchError, "The parameter type of the report is not valid.")
            ErrorMessages.Add(RSUpdateReportExecutionSnapshotError, "Error occurred while taking snapshot of a report.")
            ErrorMessages.Add(RSSetReportHistoryLimitError, "Error occurred while setting report history snapshot limit.")
            ErrorMessages.Add(RSSetReportHistoryOptionsError, "Error occurred while setting report history snapshot options.")
            ErrorMessages.Add(RSSetExecutionOptionsError, "Error occurred while setting execution options.")
            ErrorMessages.Add(RSSetReportParametersError, "Error occurred while setting report parameters.")
            ErrorMessages.Add(RSGetReportParametersError, "Error occurred while getting report parameters.")
            ErrorMessages.Add(RSSetItemDataSourcesError, "Error occurred while setting the data source.")
            ErrorMessages.Add(RSGetItemDataSourcesError, "Error occurred while fetching current data sources.")
            ErrorMessages.Add(RSCreateBatchError, "Error occurred while creating a batch operation.")
            ErrorMessages.Add(RSListReportHistoryError, "Error occurred while fetching the report history snapshots.")
            ErrorMessages.Add(RSGetReportHistoryLimitError, "Error occurred while fetching the number of snapshots stored for the report.")
            ErrorMessages.Add(RSExecuteBatchError, "Error occurred while performing the batch operation.")
            ErrorMessages.Add(RSCancelBatchError, "Error occurred while canceling the batch operation.")
            ErrorMessages.Add(RSListExtensionsError, "Error occurred while fetching the list of data extensions installed on the report server.")
            ErrorMessages.Add(RSGetDataSourceContentsError, "Error occurred while getting the data source contents.")
            ErrorMessages.Add(RSSetDataSourceContentsError, "Error occurred while setting the data source contents.")
            ErrorMessages.Add(RSFindItemsError, "Error occurred while finding an item on the report server.")
            ErrorMessages.Add(RSDeleteItemError, "Error occurred while deleting an item from the report server.")
            ErrorMessages.Add(ReportSecurityError, "The report contains a security violation.ReportId:{0}")
            ErrorMessages.Add(ReportMissingReportSourceError, "No source has been specified for the report.ReportId:{0}")
            ErrorMessages.Add(ReportMissingParameterError, "A parameter expected by the report has not been supplied.ReportId:{0}")
            ErrorMessages.Add(ReportMissingEndpointError, "The SOAP endpoint used by the ReportViewer control could not be accessed.")
            ErrorMessages.Add(ReportMissingDataSourceError, "A data source expected by the report has not been supplied.ReportId:{0}")
            ErrorMessages.Add(ReportMissingDataSourceCredentialsError, "Credentials have not been supplied for a data source used by the report.ReportId:{0}")
            ErrorMessages.Add(ReportLocalProcessingError, "Error occurred while viewing locally processed report.ReportId:{0}")
            ErrorMessages.Add(ReportServerSP2HotFixNotApplied, "Report server SP2 Workgroup does not have the hotfix for role creation")
            ErrorMessages.Add(DataSourceProhibited, "A non fetch based data source is not permitted on this report")
            ErrorMessages.Add(ReportServerVersionLow, "Report server does not meet the minimal version requirement")
            ErrorMessages.Add(ReportServerNoPrivilege, "Not enough privilege to configure report server")
            ErrorMessages.Add(ReportServerInvalidUrl, "Cannot contact report server from given URL")
            ErrorMessages.Add(ReportServerUnknownException, "Unknown exception thrown by report server")
            ErrorMessages.Add(ReportNotAvailable, "Report not available")
            ErrorMessages.Add(ErrorUploadingReport, "An error occurred while trying to add the report to Microsoft Dynamics CRM. Try adding the report again. If this problem persists, contact your system administrator.")
            ErrorMessages.Add(ReportFileTooBig, "The file is too large and cannot be uploaded. Please reduce the size of the file and try again.")
            ErrorMessages.Add(ReportFileZeroLength, "You have uploaded an empty file.  Please select a new file and try again.")
            ErrorMessages.Add(ReportTypeBlocked, "The report is not a valid type.  It cannot be uploaded or downloaded.")
            ErrorMessages.Add(ReportUploadDisabled, "Reporting Services reports cannot be uploaded. If you want to create a new report, please use the Report Wizard.")
            ErrorMessages.Add(BothConnectionSidesAreNeeded, "You must provide a name or select a role for both sides of this connection.")
            ErrorMessages.Add(CannotConnectToSelf, "Cannot connect a record to itself.")
            ErrorMessages.Add(UnrelatedConnectionRoles, "The connection roles are not related.")
            ErrorMessages.Add(ConnectionRoleNotValidForObjectType, "The record type {0} is not defined for use with the connection role {1}.")
            ErrorMessages.Add(ConnectionCannotBeEnabledOnThisEntity, "Connections cannot be enabled on this entity")
            ErrorMessages.Add(ConnectionNotSupported, "The selected record does not support connections. You cannot add the connection.")
            ErrorMessages.Add(ConnectionObjectsMissing, "Both objects being connected are missing.")
            ErrorMessages.Add(ConnectionInvalidStartEndDate, "Start date / end date is invalid.")
            ErrorMessages.Add(ConnectionExists, "Connection already exists.")
            ErrorMessages.Add(DecoupleUserOwnedEntity, "Can only decouple user owned entities.")
            ErrorMessages.Add(DecoupleChildEntity, "Cannot decouple a child entity.")
            ErrorMessages.Add(ExistingParentalRelationship, "A parental relationship already exists.")
            ErrorMessages.Add(InvalidCascadeLinkType, "The cascade link type is not valid for the cascade action.")
            ErrorMessages.Add(InvalidDeleteModification, "A system relationship's delete cascading action cannot be modified.")
            ErrorMessages.Add(CustomerOpportunityRoleExists, "Customer opportunity role exists.")
            ErrorMessages.Add(CustomerRelationshipExists, "Customer relationship already exists.")
            ErrorMessages.Add(MultipleRelationshipsNotSupported, "Multiple relationships are not supported")
            ErrorMessages.Add(ImportDuplicateEntity, "This import has failed because a different entity with the identical name, {0}, already exists in the target organization.")
            ErrorMessages.Add(CascadeProxyEmptyCallerId, "Empty Caller Id")
            ErrorMessages.Add(CascadeProxyInvalidPrincipalType, "Invalid security principal type")
            ErrorMessages.Add(CascadeProxyInvalidNativeDAPtr, "Invalid pointer of unmanaged data access object")
            ErrorMessages.Add(CascadeFailToCreateNativeDAWrapper, "Failed to create unmanaged data access wrapper")
            ErrorMessages.Add(CascadeReparentOnNonUserOwned, "Cannot perform Cascade Reparent on Non-UserOwned entities")
            ErrorMessages.Add(CascadeMergeInvalidSpecialColumn, "Invalid Column Name for Merge Special Casing")
            ErrorMessages.Add(CascadeRemoveLinkOnNonNullable, "CascadeDelete is defined as RemoveLink while the foreign key is not nullable")
            ErrorMessages.Add(CascadeDeleteNotAllowDelete, "Object is not allowed to be deleted")
            ErrorMessages.Add(CascadeInvalidLinkType, "Invalid CascadeLink Type")
            ErrorMessages.Add(IsvExtensionsPrivilegeNotPresent, "To import ISV.Config, your user account must be associated with a security role that includes the ISV Extensions privilege.")
            ErrorMessages.Add(RelationshipNameLengthExceedsLimit, "Relationship name cannot be more than 50 characters long.")
            ErrorMessages.Add(ImportEmailTemplateErrorMissingFile, "E-mail Template '{0}' import: The attachment '{1}' was not found in the import zip file.")
            ErrorMessages.Add(CascadeInvalidExtraConditionValue, "Invalid Extra-condition value")
            ErrorMessages.Add(ImportWorkflowNameConflictError, "Workflow {0} cannot be imported because a workflow with same name and different unique identifier exists in the target system. Change the name of this workflow, and then try again.")
            ErrorMessages.Add(ImportWorkflowPublishedError, "Workflow {0}({1}) cannot be imported because a workflow with same unique identifier is published on the target system. Unpublish the workflow on the target system before attempting to import this workflow again.")
            ErrorMessages.Add(ImportWorkflowEntityDependencyError, "Cannot import workflow definition. Required entity dependency is missing.")
            ErrorMessages.Add(ImportWorkflowAttributeDependencyError, "Cannot import workflow definition. Required attribute dependency is missing.")
            ErrorMessages.Add(ImportWorkflowError, "Cannot import workflow definition. The workflow with specified workflow id is not updatable or workflow name is not unique.")
            ErrorMessages.Add(ImportGenericEntitiesError, "An error occurred while importing generic entities.")
            ErrorMessages.Add(ImportRolePermissionError, "You do not have the necessary privileges to import security roles.")
            ErrorMessages.Add(ImportRoleError, "Cannot import security role. The role with specified role id is not updatable or role name is not unique.")
            ErrorMessages.Add(ImportOrgSettingsError, "There was an error parsing the Organization Settings during Import.")
            ErrorMessages.Add(InvalidSharePointSiteCollectionUrl, "The URL must conform to the http or https schema.")
            ErrorMessages.Add(InvalidSiteRelativeUrlFormat, "The relative url contains invalid characters. Please use a different name. Valid relative url names cannot end with the following strings: .aspx, .ashx, .asmx, .svc , cannot begin or end with a dot or /, cannot contain consecutive dots or / and cannot contain any of the following characters: ~ "" # % & * : < > ? \ { | }.")
            ErrorMessages.Add(InvalidRelativeUrlFormat, "The relative url contains invalid characters. Please use a different name. Valid relative url names cannot ends with the following strings: .aspx, .ashx, .asmx, .svc , cannot begin or end with a dot, cannot contain consecutive dots and cannot contain any of the following characters: ~ "" # % & * : < > ? / \ { | }.")
            ErrorMessages.Add(InvalidAbsoluteUrlFormat, "The absolute url contains invalid characters. Please use a different name. Valid absolute url cannot ends with the following strings: .aspx, .ashx, .asmx, .svc")
            ErrorMessages.Add(InvalidUrlConsecutiveSlashes, "The Url contains consecutive slashes which is not allowed.")
            ErrorMessages.Add(SharePointRecordWithDuplicateUrl, "There is already a record with the same Url.")
            ErrorMessages.Add(SharePointAbsoluteAndRelativeUrlEmpty, "Both absolute URL and relative URL cannot be null.")
            ErrorMessages.Add(ImportOptionSetsError, "An error occurred while importing OptionSets.")
            ErrorMessages.Add(ImportRibbonsError, "An error occurred while importing Ribbons.")
            ErrorMessages.Add(ImportReportsError, "An error occurred while importing Reports.")
            ErrorMessages.Add(ImportSolutionError, "An error occurred while importing a Solution.")
            ErrorMessages.Add(ImportDependencySolutionError, "{0} requires solutions that are not currently installed. Import the following solutions before Importing this one. {1} ")
            ErrorMessages.Add(ExportSolutionError, "An error occurred while exporting a Solution.")
            ErrorMessages.Add(ExportManagedSolutionError, "An error occurred while exporting a solution. Managed solutions cannot be exported.")
            ErrorMessages.Add(ExportMissingSolutionError, "An error occurred while exporting a solution. The solution does not exist in this system.")
            ErrorMessages.Add(ImportSolutionManagedError, "Solution '{0}' already exists in this system as managed and cannot be upgraded.")
            ErrorMessages.Add(ImportOptionSetAttributeError, "Attribute '{0}' was not imported as it references a non-existing global Option Set ('{1}').")
            ErrorMessages.Add(ImportSolutionManagedToUnmanagedMismatch, "The solution is already installed on this system as an unmanaged solution and the package supplied is attempting to install it in managed mode. Import can only update solutions when the modes match. Uninstall the current solution and try again.")
            ErrorMessages.Add(ImportSolutionUnmanagedToManagedMismatch, "The solution is already installed on this system as a managed solution and the package supplied is attempting to install it in unmanaged mode. Import can only update solutions when the modes match. Uninstall the current solution and try again.")
            ErrorMessages.Add(ImportSolutionIsvConfigWarning, "ISV Config was overwritten.")
            ErrorMessages.Add(ImportSolutionSiteMapWarning, "SiteMap was overwritten.")
            ErrorMessages.Add(ImportSolutionOrganizationSettingsWarning, "Organization settings were overwritten.")
            ErrorMessages.Add(ImportExportDeprecatedError, "This message is no longer available. Please consult the SDK for alternative messages.")
            ErrorMessages.Add(ImportSystemSolutionError, "System solution cannot be imported.")
            ErrorMessages.Add(ImportTranslationMissingSolutionError, "An error occurred while importing the translations. The solution associated with the translations does not exist in this system.")
            ErrorMessages.Add(ExportDefaultAsPackagedError, "The default solution cannot be exported as a package.")
            ErrorMessages.Add(ImportDefaultAsPackageError, "The package supplied for the default solution is trying to install it in managed mode. The default solution cannot be managed. In the XML for the default solution, set the Managed value back to ""false"" and try to import the solution again.")
            ErrorMessages.Add(ImportCustomizationsBadZipFileError, "The solution file is invalid. The compressed file must contain the following files at its root: solution.xml, customizations.xml, and [Content_Types].xml. Customization files exported from previous versions of Microsoft Dynamics CRM are not supported.")
            ErrorMessages.Add(ImportTranslationsBadZipFileError, "The translation file is invalid. The compressed file must contain the following files at its root: CrmTranslations.xml, [Content_Types].xml.")
            ErrorMessages.Add(ImportAttributeNameError, "Invalid name for attribute {0}.  Custom attribute names must start with a valid customization prefix. The prefix for a solution component should match the prefix that is specified for the publisher of the solution.")
            ErrorMessages.Add(ImportFieldSecurityProfileIsSecuredMissingError, "Some field security permissions could not be imported because the following fields are not securable: {0}.")
            ErrorMessages.Add(ImportFieldSecurityProfileAttributesMissingError, "Some field security permissions could not be imported because the following fields are not in the system: {0}.")
            ErrorMessages.Add(ImportFileSignatureInvalid, "The import file has an invalid digital signature.")
            ErrorMessages.Add(ImportSolutionPackageNotValid, "The solution package you are importing was generated on a version of Microsoft Dynamics CRM that cannot be imported into this system. Package Version: {0} {1}, System Version: {2} {3}.")
            ErrorMessages.Add(ImportSolutionPackageNeedsUpgrade, "The solution package you are importing was generated on a different version of Microsoft Dynamics CRM. The system will attempt to transform the package prior to import. Package Version: {0} {1}, System Version: {2} {3}.")
            ErrorMessages.Add(ImportSolutionPackageInvalidSolutionPackageVersion, "Only solutions with a package version of {0} or lower can be imported into this organization. Please export the solution from the original CRM instance, targeting solution package version {0} or lower.")
            ErrorMessages.Add(ImportSolutionPackageMinimumVersionNeeded, "Deprecated, not removing now as it might cause issues during integrations.")
            ErrorMessages.Add(ImportSolutionPackageRequiresOptInAvailable, "Some components in the solution package you are importing require opt in. Opt in is available, please consult your administrator.")
            ErrorMessages.Add(ImportSolutionPackageRequiresOptInNotAvailable, "The solution package you are importing was generated on a SKU of Microsoft Dynamics CRM that supports opt in. It cannot be imported in your system.")
            ErrorMessages.Add(ImportSdkMessagesError, "An error occurred while importing Sdk Messages.")
            ErrorMessages.Add(ImportEmailTemplatePersonalError, "E-mail Template was not imported. The Template is a personal template on the target system; import cannot overwrite personal templates.")
            ErrorMessages.Add(ImportNonWellFormedFileError, "Invalid customization file. This file is not well formed.")
            ErrorMessages.Add(ImportPluginTypesError, "An error occurred while importing plug-in types.")
            ErrorMessages.Add(ImportSiteMapError, "An error occurred while importing the Site Map.")
            ErrorMessages.Add(ImportMappingsMissingEntityMapError, "This customization file contains a reference to an entity map that does not exist on the target system.")
            ErrorMessages.Add(ImportMappingsSystemMapError, "Import cannot create system attribute mappings")
            ErrorMessages.Add(ImportIsvConfigError, "There was an error parsing the IsvConfig during Import")
            ErrorMessages.Add(ImportArticleTemplateError, "There was an error in parsing the article templates in Import Xml")
            ErrorMessages.Add(ImportEmailTemplateError, "There was an error in parsing the email templates in Import Xml")
            ErrorMessages.Add(ImportContractTemplateError, "There was an error in parsing the contract templates in Import Xml")
            ErrorMessages.Add(ImportRelationshipRoleMapsError, "The number of format parameters passed into the input string is incorrect")
            ErrorMessages.Add(ImportRelationshipRolesError, "The number of format parameters passed into the input string is incorrect")
            ErrorMessages.Add(ImportRelationshipRolesPrivilegeError, "{0} cannot be imported. The {1} privilege is required to import this component.")
            ErrorMessages.Add(ImportEntityNameMismatchError, "The number of format parameters passed into the input string is incorrect")
            ErrorMessages.Add(ImportFormXmlError, "The number of format parameters passed into the input string is incorrect")
            ErrorMessages.Add(ImportFieldXmlError, "The number of format parameters passed into the input string is incorrect")
            ErrorMessages.Add(ImportSavedQueryExistingError, "The number of format parameters passed into the input string is incorrect")
            ErrorMessages.Add(ImportSavedQueryOtcMismatchError, "There was an error processing saved queries of the same object type code (unresolvable system collision)")
            ErrorMessages.Add(ImportEntityCustomResourcesNewStringError, "Invalid Entity new string in the Custom Resources")
            ErrorMessages.Add(ImportEntityCustomResourcesError, "Invalid Custom Resources in the Import File")
            ErrorMessages.Add(ImportEntityIconError, "Invalid Icon in the Import File")
            ErrorMessages.Add(ImportSavedQueryDeletedError, "A saved query with the same id is marked as deleted in the system. Please first publish the customized entity and import again.")
            ErrorMessages.Add(ImportEntitySystemUserOnPremiseMismatchError, "The systemuser entity was imported, but customized forms for the entity were not imported. Systemuser entity forms from Microsoft Dynamics CRM Online cannot be imported into on-premises or hosted versions of Microsoft Dynamics CRM.")
            ErrorMessages.Add(ImportEntitySystemUserLiveMismatchError, "The systemuser entity was imported but customized forms for the entity were not imported. Systemuser entity forms from on-premises or hosted versions of Microsoft Dynamics CRM cannot be imported into Microsoft Dynamics CRM Online.")
            ErrorMessages.Add(ImportLanguagesIgnoredError, "Translated labels for the following languages could not be imported because they have not been enabled for this organization: {0}")
            ErrorMessages.Add(ImportInvalidFileError, "Invalid Import File")
            ErrorMessages.Add(ImportXsdValidationError, "The import file is invalid. XSD validation failed with the following error: '{0}'. The validation failed at: '...{1} <<<<<ERROR LOCATION>>>>> {2}...'.""")
            ErrorMessages.Add(ImportInvalidXmlError, "This solution package cannot be imported because it contains invalid XML. You can attempt to repair the file by manually editing the XML contents using the information found in the schema validation errors, or you can contact your solution provider.")
            ErrorMessages.Add(ImportWrongPublisherError, "The following managed solution cannot be imported: {0}. The publisher name cannot be changed from {1} to {2}.")
            ErrorMessages.Add(ImportMissingDependenciesError, "The following solution cannot be imported: {0}. Some dependencies are missing.")
            ErrorMessages.Add(ImportGenericError, "The import failed. For more information, see the related error messages.")
            ErrorMessages.Add(ImportMissingComponent, "Cannot add a Root Component {0} of type {1} because it is not in the target system.")
            ErrorMessages.Add(ImportMissingRootComponentEntry, "The import has failed because component {0} of type {1} is not declared in the solution file as a root component. To fix this, import again using the XML file that was generated when you exported the solution.")
            ErrorMessages.Add(UnmanagedComponentParentsManagedComponent, "Found {0} dependency records where unmanaged component is the parent of a managed component. First record (dependentcomponentobjectid = {1}, type = {2}, requiredcomponentobjectid = {3}, type= {4}, solution = {5}).")
            ErrorMessages.Add(FailedToGetNetworkServiceName, "Failed to obtain the localized name for NetworkService account")
            ErrorMessages.Add(CustomParentingSystemNotSupported, "A custom entity can not have a parental relationship to a system entity")
            ErrorMessages.Add(InvalidFormatParameters, "The number of format parameters passed into the input string is incorrect")
            ErrorMessages.Add(DuplicatePrimaryNameAttribute, "The new {2} attribute is set as the primary name attribute for the {1} entity. The {1} entity already has the {0} attribute set as the primary name attribute. An entity can only have one primary name attribute.")
            ErrorMessages.Add(ConfigurationPageNotValidForSolution, "The solution configuration page must exist within the solution it represents.")
            ErrorMessages.Add(SolutionConfigurationPageMustBeHtmlWebResource, "The solution configuration page must exist within the solution it represents.")
            ErrorMessages.Add(InvalidSolutionConfigurationPage, "The specified configuration page for this solution is invalid.")
            ErrorMessages.Add(InvalidLanguageForSolution, "Solution and Publisher Options are not available since your language does not match system base language.")
            ErrorMessages.Add(CannotHaveDuplicateYomi, "One attribute can be tied to only one yomi at a time")
            ErrorMessages.Add(SavedQueryIsNotCustomizable, "The specified view is not customizable")
            ErrorMessages.Add(CannotDeleteChildAttribute, "The Child Attribute is not valid for deletion")
            ErrorMessages.Add(EntityHasNoStateCode, "Specified entity does not have a statecode.")
            ErrorMessages.Add(NoAttributesForEntityCreate, "No attributes for Create Entity action.")
            ErrorMessages.Add(DuplicateAttributeSchemaName, "An attribute with the specified name already exists")
            ErrorMessages.Add(DuplicateDisplayCollectionName, "An object with the specified display collection name already exists.")
            ErrorMessages.Add(DuplicateDisplayName, "An object with the specified display name already exists.")
            ErrorMessages.Add(DuplicateName, "An object with the specified name already exists")
            ErrorMessages.Add(InvalidRelationshipType, "The specified relationship type is not valid for this operation")
            ErrorMessages.Add(InvalidPrimaryFieldType, "Primary UI Attribute has to be of type String")
            ErrorMessages.Add(InvalidOwnershipTypeMask, "The specified ownership type mask is not valid for this operation")
            ErrorMessages.Add(InvalidDisplayName, "The specified display name is not valid")
            ErrorMessages.Add(InvalidSchemaName, "An entity with the specified name already exists. Please specify a unique name.")
            ErrorMessages.Add(RelationshipIsNotCustomRelationship, "The specified relationship is not a custom relationship")
            ErrorMessages.Add(AttributeIsNotCustomAttribute, "The specified attribute is not a custom attribute")
            ErrorMessages.Add(EntityIsNotCustomizable, "The specified entity is not customizable")
            ErrorMessages.Add(MultipleParentsNotSupported, "An entity can have only one parental relationship")
            ErrorMessages.Add(CannotCreateActivityRelationship, "Relationship with activities cannot be created through this operation")
            ErrorMessages.Add(CyclicalRelationship, "The specified relationship will result in a cycle.")
            ErrorMessages.Add(InvalidRelationshipDescription, "The specified relationship cannot be created")
            ErrorMessages.Add(CannotDeletePrimaryUIAttribute, "The Primary UI Attribute is not valid for deletion")
            ErrorMessages.Add(RowGuidIsNotValidName, "rowguid is a reserved name and cannot be used as an identifier")
            ErrorMessages.Add(FailedToScheduleActivity, "Failed to schedule activity.")
            ErrorMessages.Add(CannotDeleteLastEmailAttribute, "You cannot delete this field because the record type has been enabled for e-mail.")
            ErrorMessages.Add(SystemAttributeMap, "SystemAttributeMap Error Occurred")
            ErrorMessages.Add(UpdateAttributeMap, "UpdateAttributeMap Error Occurred")
            ErrorMessages.Add(InvalidAttributeMap, "InvalidAttributeMap Error Occurred")
            ErrorMessages.Add(SystemEntityMap, "SystemEntityMap Error Occurred")
            ErrorMessages.Add(UpdateEntityMap, "UpdateEntityMap Error Occurred")
            ErrorMessages.Add(NonMappableEntity, "NonMappableEntity Error Occurred")
            ErrorMessages.Add(unManagedidsCalloutException, "Callout code throws exception")
            ErrorMessages.Add(unManagedidscalloutinvalidevent, "Invalid callout event")
            ErrorMessages.Add(unManagedidscalloutinvalidconfig, "Invalid callout configuration")
            ErrorMessages.Add(unManagedidscalloutisvstop, "Callout ISV code stopped the operation")
            ErrorMessages.Add(unManagedidscalloutisvabort, "Callout ISV code aborted the operation")
            ErrorMessages.Add(unManagedidscalloutisvexception, "Callout ISV code throws exception")
            ErrorMessages.Add(unManagedidscustomentityambiguousrelationship, "More than one relationship between the requested entities exists.")
            ErrorMessages.Add(unManagedidscustomentitynorelationship, "No relationship exists between the requested entities.")
            ErrorMessages.Add(unManagedidscustomentityparentchildidentical, "The supplied parent and child entities are identical.")
            ErrorMessages.Add(unManagedidscustomentityinvalidparent, "The supplied parent passed in is not a valid entity.")
            ErrorMessages.Add(unManagedidscustomentityinvalidchild, "The supplied child passed in is not a valid entity.")
            ErrorMessages.Add(unManagedidscustomentitywouldcreateloop, "This association would create a loop in the database.")
            ErrorMessages.Add(unManagedidscustomentityexistingloop, "There is an existing loop in the database.")
            ErrorMessages.Add(unManagedidscustomentitystackunderflow, "Custom entity MD stack underflow.")
            ErrorMessages.Add(unManagedidscustomentitystackoverflow, "Custom entity MD stack overflow.")
            ErrorMessages.Add(unManagedidscustomentitytlsfailure, "Custom entity MD TLS not initialized.")
            ErrorMessages.Add(unManagedidscustomentityinvalidownership, "Custom entity ownership type mask is improperly set.")
            ErrorMessages.Add(unManagedidscustomentitynotinitialized, "Custom entity interface was not properly initialized.")
            ErrorMessages.Add(unManagedidscustomentityalreadyinitialized, "Custom entity interface already initialized on this thread.")
            ErrorMessages.Add(unManagedidscustomentitynameviolation, "Supplied entity found, but it is not a custom entity.")
            ErrorMessages.Add(unManagedidscascadeunexpectederror, "Unexpected error occurred in cascading operation")
            ErrorMessages.Add(unManagedidscascadeemptylinkerror, "The relationship link is empty")
            ErrorMessages.Add(unManagedidscascadeundefinedrelationerror, "Relationship type is not supported")
            ErrorMessages.Add(unManagedidscascadeinconsistencyerror, "Cascade map information is inconsistent.")
            ErrorMessages.Add(MergeLossOfParentingWarning, "Merge warning: sub-entity might lose parenting")
            ErrorMessages.Add(MergeDifferentlyParentedWarning, "Merge warning: sub-entity will be differently parented.")
            ErrorMessages.Add(MergeEntitiesIdenticalError, "Merge cannot be performed on master and sub-entities that are identical.")
            ErrorMessages.Add(MergeEntityNotActiveError, "Merge cannot be performed on entity that is inactive.")
            ErrorMessages.Add(unManagedidsmergedifferentbizorgerror, "Merge cannot be performed on entities from different business entity.")
            ErrorMessages.Add(MergeActiveQuoteError, "Merge cannot be performed on sub-entity that has active quote.")
            ErrorMessages.Add(MergeSecurityError, "Merge is not allowed: caller does not have the privilege or access.")
            ErrorMessages.Add(MergeCyclicalParentingError, "Merge could create cyclical parenting.")
            ErrorMessages.Add(unManagedidscalendarruledoesnotexist, "The calendar rule does not exist.")
            ErrorMessages.Add(unManagedidscalendarinvalidcalendar, "The calendar is invalid.")
            ErrorMessages.Add(AttachmentInvalidFileName, "Attachment file name contains invalid characters.")
            ErrorMessages.Add(unManagedidsattachmentcannottruncatetempfile, "Cannot truncate temporary attachment file.")
            ErrorMessages.Add(unManagedidsattachmentcannotunmaptempfile, "Cannot unmap temporary attachment file.")
            ErrorMessages.Add(unManagedidsattachmentcannotcreatetempfile, "Cannot create temporary attachment file.")
            ErrorMessages.Add(unManagedidsattachmentisempty, "Attachment is empty.")
            ErrorMessages.Add(unManagedidsattachmentcannotreadtempfile, "Cannot read temporary attachment file.")
            ErrorMessages.Add(unManagedidsattachmentinvalidfilesize, "Attachment file size is too big.")
            ErrorMessages.Add(unManagedidsattachmentcannotgetfilesize, "Cannot get temporary attachment file size.")
            ErrorMessages.Add(unManagedidsattachmentcannotopentempfile, "Cannot open temporary attachment file.")
            ErrorMessages.Add(unManagedidscustomizationtransformationnotsupported, "Transformation is not supported for this object.")
            ErrorMessages.Add(ContractDetailDiscountAmountAndPercent, "Both 'amount' and 'percentage' cannot be set.")
            ErrorMessages.Add(ContractDetailDiscountAmount, "The contract's discount type does not support 'percentage' discounts.")
            ErrorMessages.Add(ContractDetailDiscountPercent, "The contract's discount type does not support 'amount' discounts.")
            ErrorMessages.Add(IncidentIsAlreadyClosedOrCancelled, "Already Closed or Canceled")
            ErrorMessages.Add(unManagedidsincidentparentaccountandparentcontactnotpresent, "You should specify a parent contact or account.")
            ErrorMessages.Add(unManagedidsincidentparentaccountandparentcontactpresent, "You can either specify a parent contact or account, but not both.")
            ErrorMessages.Add(IncidentCannotCancel, "The incident can not be cancelled because there are open activities for this incident.")
            ErrorMessages.Add(IncidentInvalidContractLineStateForCreate, "The case can not be created against this contract line item because the contract line item is cancelled or expired.")
            ErrorMessages.Add(IncidentNullSpentTimeOrBilled, "The timespent on the Incident is NULL or IncidentResolution Activity's IsBilled is NULL.")
            ErrorMessages.Add(IncidentInvalidAllotmentType, "The allotment type for the contract is invalid.")
            ErrorMessages.Add(unManagedidsincidentcannotclose, "The incident can not be closed because there are open activities for this incident.")
            ErrorMessages.Add(IncidentMissingActivityRegardingObject, "The incident id is missing.")
            ErrorMessages.Add(unManagedidsincidentmissingactivityobjecttype, "Missing object type code.")
            ErrorMessages.Add(unManagedidsincidentnullactivitytypecode, "The activitytypecode can't be NULL.")
            ErrorMessages.Add(unManagedidsincidentinvalidactivitytypecode, "The activitytypecode is wrong.")
            ErrorMessages.Add(unManagedidsincidentassociatedactivitycorrupted, "The activity associated with this case is corrupted.")
            ErrorMessages.Add(unManagedidsincidentinvalidstate, "Incident state is invalid.")
            ErrorMessages.Add(IncidentContractDoesNotHaveAllotments, "The contract does not have enough allotments. The case can not be created against this contract.")
            ErrorMessages.Add(unManagedidsincidentcontractdetaildoesnotmatchcontract, "The contract line item is not in the specified contract.")
            ErrorMessages.Add(IncidentMissingContractDetail, "The contract detail id is missing.")
            ErrorMessages.Add(IncidentInvalidContractStateForCreate, "The case can not be created against this contract because of the contract state.")
            ErrorMessages.Add(DisabledCRMGoingOffline, "Microsoft Dynamics CRM functionality is not available while Offline Synchronization is occuring")
            ErrorMessages.Add(DisabledCRMGoingOnline, "Microsoft Dynamics CRM functionality is not available while Online Synchronization is occuring")
            ErrorMessages.Add(DisabledCRMAddinLoadFailure, "An error occurred loading Microsoft Dynamics CRM functionality. Try restarting Outlook. Contact your system administrator if errors persist.")
            ErrorMessages.Add(DisabledCRMClientVersionLower, "Offline functionality is not supported in this earlier version of Microsoft Dynamics CRM for Outlook and this Microsoft Dynamics CRM organization {0}. Download a compatible Outlook Client version.")
            ErrorMessages.Add(DisabledCRMClientVersionHigher, "The Microsoft Dynamics CRM server needs to be upgraded before Microsoft Dynamics CRM client can be used. Contact your system administrator for assistance.")
            ErrorMessages.Add(DisabledCRMPostOfflineUpgrade, "Microsoft Dynamics CRM functionality is not available until the Microsoft Dynamics CRM client is taken back online")
            ErrorMessages.Add(DisabledCRMOnlineCrmNotAvailable, "Microsoft Dynamics CRM server is not available")
            ErrorMessages.Add(GoOfflineMetadataVersionsMismatch, "Client and Server metadata versions are different due to new customization on the server. Please try going offline again.")
            ErrorMessages.Add(GoOfflineGetBCPFileException, "CRM server was not able to process your request. Contact your system administrator for assistance and try going offline again.")
            ErrorMessages.Add(GoOfflineDbSizeLimit, "You have exceeded the storage limit for your offline database. You must reduce the amount of data to be taken offline by changing your Local Data Groups.")
            ErrorMessages.Add(GoOfflineServerFailedGenerateBCPFile, "CRM server was not able to generate BCP file. Contact your system administrator for assistance and try going offline again.")
            ErrorMessages.Add(GoOfflineBCPFileSize, "Client was not able to download BCP file. Contact your system administrator for assistance and try going offline again.")
            ErrorMessages.Add(GoOfflineFailedMoveData, "Client was not able to download data. Contact your system administrator for assistance and try going offline again.")
            ErrorMessages.Add(GoOfflineFailedPrepareMsde, "Prepare MSDE failed. Contact your system administrator for assistance and try going offline again.")
            ErrorMessages.Add(GoOfflineFailedReloadMetadataCache, "The Microsoft Dynamics CRM for Outlook was unable to go offline. Please try going offline again.")
            ErrorMessages.Add(DoNotTrackItem, "Selected item will not be tracked.")
            ErrorMessages.Add(GoOfflineFileWasDeleted, "Data file was deleted on server before it was sent to client.")
            ErrorMessages.Add(GoOfflineEmptyFileForDelete, "Data file for delete is empty.")
            ErrorMessages.Add(OrganizationUIDeprecated, "The OrganizationUI entity is deprecated. It has been replaced by the SystemForm entity.")
            ErrorMessages.Add(unManagedidssqltimeouterror, "SQL timeout expired.")
            ErrorMessages.Add(unManagedidssqlerror, "Generic SQL error.")
            ErrorMessages.Add(unManagedidsrcsyncinvalidfiltererror, "Invalid filter specified.")
            ErrorMessages.Add(unManagedidsrcsyncnotprimary, "Cannot sync: not the primary OutlookSync client.")
            ErrorMessages.Add(unManagedidsrcsyncnoprimary, "No primary client exists.")
            ErrorMessages.Add(unManagedidsrcsyncnoclient, "Client does not exist.")
            ErrorMessages.Add(unManagedidsrcsyncmethodnone, "Synchronization tasks can�t be performed on this computer since the synchronization method is set to None.")
            ErrorMessages.Add(unManagedidsrcsyncfilternoaccess, "Cannot go offline: missing access rights on required entity.")
            ErrorMessages.Add(InvalidOfflineOperation, "Operation not valid when offline.")
            ErrorMessages.Add(unManagedidsrcsyncsqlgenericerror, "unManagedidsrcsyncsqlgenericerror")
            ErrorMessages.Add(unManagedidsrcsyncsqlpausederror, "unManagedidsrcsyncsqlpausederror")
            ErrorMessages.Add(unManagedidsrcsyncsqlstoppederror, "unManagedidsrcsyncsqlstoppederror")
            ErrorMessages.Add(unManagedidsrcsyncsubscriptionowner, "The caller id does not match the subscription owner id.  Only subscription owners may perform subscription operations.")
            ErrorMessages.Add(unManagedidsrcsyncinvalidsubscription, "The specified subscription does not exist.")
            ErrorMessages.Add(unManagedidsrcsyncsoapparseerror, "unManagedidsrcsyncsoapparseerror")
            ErrorMessages.Add(unManagedidsrcsyncsoapreaderror, "unManagedidsrcsyncsoapreaderror")
            ErrorMessages.Add(unManagedidsrcsyncsoapfaulterror, "unManagedidsrcsyncsoapfaulterror")
            ErrorMessages.Add(unManagedidsrcsyncsoapservererror, "unManagedidsrcsyncsoapservererror")
            ErrorMessages.Add(unManagedidsrcsyncsoapsendfailed, "unManagedidsrcsyncsoapsendfailed")
            ErrorMessages.Add(unManagedidsrcsyncsoapconnfailed, "unManagedidsrcsyncsoapconnfailed")
            ErrorMessages.Add(unManagedidsrcsyncsoapgenfailed, "unManagedidsrcsyncsoapgenfailed")
            ErrorMessages.Add(unManagedidsrcsyncmsxmlfailed, "unManagedidsrcsyncmsxmlfailed")
            ErrorMessages.Add(unManagedidsrcsyncinvalidsynctime, "The specified sync time is invalid.  Sync times must not be earlier than those returned by the previous sync.  Please reinitialize your subscription.")
            ErrorMessages.Add(AttachmentBlocked, "The attachment is either not a valid type or is too large. It cannot be uploaded or downloaded.")
            ErrorMessages.Add(unManagedidsarticletemplateisnotactive, "KB article template is inactive.")
            ErrorMessages.Add(unManagedidsfulltextoperationfailed, "Full text operation failed.")
            ErrorMessages.Add(unManagedidsarticletemplatecontainsarticles, "Cannot change article template because there are knowledge base articles using it.")
            ErrorMessages.Add(unManagedidsqueueorganizationidnotmatch, "Callers' organization Id does not match businessunit's organization Id.")
            ErrorMessages.Add(unManagedidsqueuemissingbusinessunitid, "Missing businessunitid.")
            ErrorMessages.Add(SubjectDoesNotExist, "Subject does not exist.")
            ErrorMessages.Add(SubjectLoopBeingCreated, "Creating this parental association would create a loop in Subjects hierarchy.")
            ErrorMessages.Add(SubjectLoopExists, "Loop exists in the subjects hierarchy.")
            ErrorMessages.Add(InvalidSubmitFromUnapprovedArticle, "You are trying to submit an article that has a status of unapproved. You can only submit an article with the status of draft.")
            ErrorMessages.Add(InvalidUnpublishFromUnapprovedArticle, "You are trying to unpublish an article that has a status of unapproved. You can only unpublish an article with the status of publish.")
            ErrorMessages.Add(InvalidApproveFromDraftArticle, "You are trying to approve an article that has a status of draft. You can only approve an article with the status of unapproved.")
            ErrorMessages.Add(InvalidUnpublishFromDraftArticle, "You are trying to unpublish an article that has a status of draft. You can only unpublish an article with the status of published.")
            ErrorMessages.Add(InvalidApproveFromPublishedArticle, "You are trying to approve an article that has a status of published. You can only approve an article with the status of unapproved.")
            ErrorMessages.Add(InvalidSubmitFromPublishedArticle, "You are trying to submit an article that has a status of published. You can only submit an article with the status of draft.")
            ErrorMessages.Add(QuoteReviseExistingActiveQuote, "Quote cannot be revised as there already exists another quote in Draft/Active state and with same quote number.")
            ErrorMessages.Add(BaseCurrencyNotDeletable, "The base currency of an organization cannot be deleted.")
            ErrorMessages.Add(CannotDeleteBaseMoneyCalculationAttribute, "The base money calculation Attribute is not valid for deletion")
            ErrorMessages.Add(InvalidExchangeRate, "The exchange rate is invalid.")
            ErrorMessages.Add(InvalidCurrency, "The currency is invalid.")
            ErrorMessages.Add(CurrencyCannotBeNullDueToNonNullMoneyFields, "The currency cannot be null.")
            ErrorMessages.Add(CannotUpdateProductCurrency, "The currency of the product cannot be updated because there are associated price list items with pricing method percentage.")
            ErrorMessages.Add(InvalidPriceLevelCurrencyForPricingMethod, "The currency of the price list needs to match the currency of the product for pricing method percentage.")
            ErrorMessages.Add(DiscountTypeAndPriceLevelCurrencyNotEqual, "The currency of the discount needs to match the currency of the price list for discount type amount.")
            ErrorMessages.Add(CurrencyRequiredForDiscountTypeAmount, "The currency cannot be null for discount type amount.")
            ErrorMessages.Add(RecordAndPricelistCurrencyNotEqual, "The currency of the record does not match the currency of the price list.")
            ErrorMessages.Add(ExchangeRateOfBaseCurrencyNotUpdatable, "The exchange rate of the base currency cannot be modified.")
            ErrorMessages.Add(BaseCurrencyCannotBeDeactivated, "The base currency cannot be deactivated.")
            ErrorMessages.Add(DuplicateIsoCurrencyCode, "Cannot insert duplicate currency record. Currency with the same currency code already exist in the system.")
            ErrorMessages.Add(InvalidIsoCurrencyCode, "Invalid ISO currency code.")
            ErrorMessages.Add(PercentageDiscountCannotHaveCurrency, "Currency cannot be set when discount type is percentage.")
            ErrorMessages.Add(RecordAndOpportunityCurrencyNotEqual, "The currency of the record does not match the currency of the price list.")
            ErrorMessages.Add(QuoteAndSalesOrderCurrencyNotEqual, "The currency of the record does not match the currency of the price list.")
            ErrorMessages.Add(SalesOrderAndInvoiceCurrencyNotEqual, "The currency of the record does not match the currency of the price list.")
            ErrorMessages.Add(BaseCurrencyOverflow, "The exchange rate set for the currency specified in this record has generated a value for {0} that is larger than the maximum allowed for the base currency ({1}).")
            ErrorMessages.Add(BaseCurrencyUnderflow, "The exchange rate set for the currency specified in this record has generated a value for {0} that is smaller than the minimum allowed for the base currency ({1}).")
            ErrorMessages.Add(CurrencyNotEqual, "The currency of the {0} does not match the currency of the {1}.")
            ErrorMessages.Add(UnitNoName, "The unit name cannot be null.")
            ErrorMessages.Add(unManagedidsinvoicecloseapideprecated, "The Invoice Close API is deprecated. It has been replaced by the Pay and Cancel APIs.")
            ErrorMessages.Add(ProductDoesNotExist, "The product does not exist.")
            ErrorMessages.Add(ProductKitLoopBeingCreated, "Using this product would create a loop in the kit hierarchy.")
            ErrorMessages.Add(ProductKitLoopExists, "Loop exists in the kit hierarchy.")
            ErrorMessages.Add(DiscountPercent, "The discount type does not support 'amount' discounts.")
            ErrorMessages.Add(DiscountAmount, "The discount type does not support 'percentage' discounts.")
            ErrorMessages.Add(DiscountAmountAndPercent, "Both 'amount' and 'percentage' cannot be set.")
            ErrorMessages.Add(EntityIsUnlocked, "This entity is already unlocked.")
            ErrorMessages.Add(EntityIsLocked, "This entity is already locked.")
            ErrorMessages.Add(BaseUnitDoesNotExist, "The base unit does not exist.")
            ErrorMessages.Add(UnitDoesNotExist, "The unit does not exist.")
            ErrorMessages.Add(UnitLoopBeingCreated, "Using this base unit would create a loop in the unit hierarchy.")
            ErrorMessages.Add(UnitLoopExists, "Loop exists in the unit hierarchy.")
            ErrorMessages.Add(QuantityReadonly, "Do not modify the Quantity field when you update the primary unit.")
            ErrorMessages.Add(BaseUnitNotNull, "Do not use a base unit as the value for a primary unit. This value should always be null.")
            ErrorMessages.Add(UnitNotInSchedule, "The unit does not exist in the specified unit schedule.")
            ErrorMessages.Add(MissingOpportunityId, "The opportunity id is missing or invalid.")
            ErrorMessages.Add(ProductInvalidUnit, "The specified unit is not valid for this product.")
            ErrorMessages.Add(ProductMissingUomSheduleId, "The unit schedule id of the product is missing.")
            ErrorMessages.Add(MissingPriceLevelId, "The price level id is missing.")
            ErrorMessages.Add(MissingProductId, "The product id is missing.")
            ErrorMessages.Add(InvalidPricePerUnit, "The price per unit is invalid.")
            ErrorMessages.Add(PriceLevelNameExists, "The name already exists.")
            ErrorMessages.Add(PriceLevelNoName, "The name can not be null.")
            ErrorMessages.Add(MissingUomId, "The unit id is missing.")
            ErrorMessages.Add(ProductInvalidPriceLevelPercentage, "The pricing percentage must be greater than or equal to zero and less than 100000.")
            ErrorMessages.Add(InvalidBaseUnit, "The base unit does not belong to the schedule.")
            ErrorMessages.Add(MissingUomScheduleId, "The unit schedule id is missing.")
            ErrorMessages.Add(ParentReadOnly, "The parent is read only and cannot be edited.")
            ErrorMessages.Add(DuplicateProductPriceLevel, "This product and unit combination has a price for this price list.")
            ErrorMessages.Add(ProductInvalidQuantityDecimal, "The number of decimal places on the quantity is invalid.")
            ErrorMessages.Add(ProductProductNumberExists, "The product number already exists.")
            ErrorMessages.Add(ProductNoProductNumber, "The product number can not be null.")
            ErrorMessages.Add(unManagedidscannotdeactivatepricelevel, "The price level cannot be deactivated because it is the default price level of an account, contact or product.")
            ErrorMessages.Add(BaseUnitNotDeletable, "The base unit of a schedule cannot be deleted.")
            ErrorMessages.Add(DiscountRangeOverlap, "The new quantities overlap the range covered by existing quantities.")
            ErrorMessages.Add(LowQuantityGreaterThanHighQuantity, "Low quantity should be less than high quantity.")
            ErrorMessages.Add(LowQuantityLessThanZero, "Low quantity should be greater than zero.")
            ErrorMessages.Add(InvalidSubstituteProduct, "A product cannot be a substitute of itself.")
            ErrorMessages.Add(InvalidKitProduct, "The product cannot be added to itself.")
            ErrorMessages.Add(InvalidKit, "The product is not a kit.")
            ErrorMessages.Add(InvalidQuantityDecimalCode, "The quantity decimal code is invalid.")
            ErrorMessages.Add(CannotSpecifyBothProductAndProductDesc, "You cannot set both 'productid' and 'productdescription' for the same record.")
            ErrorMessages.Add(CannotSpecifyBothUomAndProductDesc, "You cannot set both 'uomid' and 'productdescription' for the same record.")
            ErrorMessages.Add(unManagedidsstatedoesnotexist, "The state is not valid for this object.")
            ErrorMessages.Add(FiscalSettingsAlreadyUpdated, "Fiscal settings have already been updated. They can be updated only once.")
            ErrorMessages.Add(unManagedidssalespeopleinvalidfiscalcalendartype, "Invalid fiscal calendar type")
            ErrorMessages.Add(unManagedidssalespeopleinvalidfiscalperiodindex, "Invalid fiscal period index")
            ErrorMessages.Add(SalesPeopleManagerNotAllowed, "Territory manager cannot belong to other territory")
            ErrorMessages.Add(unManagedidssalespeopleinvalidterritoryobjecttype, "Territories cannot be retrieved by this kind of object")
            ErrorMessages.Add(SalesPeopleDuplicateCalendarNotAllowed, "Fiscal calendar already exists for this salesperson/year")
            ErrorMessages.Add(unManagedidssalespeopleduplicatecalendarfound, "Duplicate fiscal calendars found for this salesperson/year")
            ErrorMessages.Add(SalesPeopleEmptyEffectiveDate, "Fiscal calendar effective date cannot be empty")
            ErrorMessages.Add(SalesPeopleEmptySalesPerson, "Parent salesperson cannot be empty")
            ErrorMessages.Add(InvalidNumberGroupFormat, "Invalid input string for numbergroupformat. The input string should contain an array of integers. Every element in the value array should be between one and nine, except for the last element, which can be zero.")
            ErrorMessages.Add(BaseUomNameNotSpecified, "baseuomname not specified")
            ErrorMessages.Add(InvalidActivityPartyAddress, "One or more activity parties have invalid addresses.")
            ErrorMessages.Add(FaxNoSupport, "The fax cannot be sent because this type of attachment is not allowed or does not support virtual printing to a fax device.")
            ErrorMessages.Add(FaxNoData, "The fax cannot be sent because there is no data to send. Specify at least one of the following: a cover page, a fax attachment, a fax description.")
            ErrorMessages.Add(InvalidPartyMapping, "Invalid party mapping.")
            ErrorMessages.Add(InvalidActivityXml, "Invalid Xml in an activity config file.")
            ErrorMessages.Add(ActivityInvalidObjectTypeCode, "An Invalid type code was specified by the throwing method")
            ErrorMessages.Add(ActivityInvalidSessionToken, "An Invalid session token was passed into the throwing method")
            ErrorMessages.Add(FaxServiceNotRunning, "The Microsoft Windows fax service is not running or is not installed.")
            ErrorMessages.Add(FaxSendBlocked, "The recipient is marked as ""Do Not Fax"".")
            ErrorMessages.Add(NoDialNumber, "There is no fax number specified on the fax or for the recipient.")
            ErrorMessages.Add(TooManyRecipients, "Sending to multiple recipients is not supported.")
            ErrorMessages.Add(MissingRecipient, "The fax must have a recipient before it can be sent.")
            ErrorMessages.Add(unManagedidsactivitynotroutable, "This type of activity is not routable")
            ErrorMessages.Add(unManagedidsactivitydurationdoesnotmatch, "Activity duration does not match start/end time")
            ErrorMessages.Add(unManagedidsactivityinvalidduration, "Invalid activity duration")
            ErrorMessages.Add(unManagedidsactivityinvalidtimeformat, "Invalid activity time, check format")
            ErrorMessages.Add(unManagedidsactivityinvalidregardingobject, "Invalid activity regarding object, it probably does not exist")
            ErrorMessages.Add(ActivityPartyObjectTypeNotAllowed, "Cannot create activity party of specified object type.")
            ErrorMessages.Add(unManagedidsactivityinvalidpartyobjecttype, "Activity party object type is invalid")
            ErrorMessages.Add(unManagedidsactivitypartyobjectidortypemissing, "Activity party object Id or type is missing")
            ErrorMessages.Add(unManagedidsactivityinvalidobjecttype, "Activity regarding object type is invalid")
            ErrorMessages.Add(unManagedidsactivityobjectidortypemissing, "Activity regarding object Id or type is missing")
            ErrorMessages.Add(unManagedidsactivityinvalidtype, "Invalid activity type code")
            ErrorMessages.Add(unManagedidsactivityinvalidstate, "Invalid activity state")
            ErrorMessages.Add(ContractInvalidDatesForRenew, "The start date / end date of this renewed contract can not overlap with any other invoiced / active contracts with the same contract number.")
            ErrorMessages.Add(unManagedidscontractinvalidstartdateforrenewedcontract, "The start date of the renewed contract can not be earlier than the end date of the originating contract.")
            ErrorMessages.Add(unManagedidscontracttemplateabbreviationexists, "The value for abbreviation already exists.")
            ErrorMessages.Add(ContractInvalidPrice, "The price is invalid.")
            ErrorMessages.Add(unManagedidscontractinvalidtotalallotments, "The totalallotments is invalid.")
            ErrorMessages.Add(ContractInvalidContract, "The contract is invalid.")
            ErrorMessages.Add(unManagedidscontractinvalidowner, "The owner of the contract is invalid.")
            ErrorMessages.Add(ContractInvalidContractTemplate, "The contract template is invalid.")
            ErrorMessages.Add(ContractInvalidBillToCustomer, "The bill-to customer of the contract is invalid.")
            ErrorMessages.Add(ContractInvalidBillToAddress, "The bill-to address of the contract is invalid.")
            ErrorMessages.Add(ContractInvalidServiceAddress, "The service address of the contract is invalid.")
            ErrorMessages.Add(ContractInvalidCustomer, "The customer of the contract is invalid.")
            ErrorMessages.Add(ContractNoLineItems, "There are no contract line items for this contract.")
            ErrorMessages.Add(ContractTemplateNoAbbreviation, "Abbreviation can not be NULL.")
            ErrorMessages.Add(unManagedidscontractopencasesexist, "There are open cases against this contract line item.")
            ErrorMessages.Add(unManagedidscontractlineitemdoesnotexist, "The contract line item does not exist.")
            ErrorMessages.Add(unManagedidscontractdoesnotexist, "The contract does not exist.")
            ErrorMessages.Add(ContractTemplateDoesNotExist, "The contract template does not exist.")
            ErrorMessages.Add(ContractInvalidAllotmentTypeCode, "The allotment type code is invalid.")
            ErrorMessages.Add(ContractLineInvalidState, "The state of the contract line item is invalid.")
            ErrorMessages.Add(ContractInvalidState, "The state of the contract is invalid.")
            ErrorMessages.Add(ContractInvalidStartEndDate, "Start date / end date or billing start date / billing end date is invalid.")
            ErrorMessages.Add(unManagedidscontractaccountmissing, "Account is required to save a contract.")
            ErrorMessages.Add(unManagedidscontractunexpected, "An unexpected error occurred in Contracts.")
            ErrorMessages.Add(unManagedidsevalerrorformatlookupparameter, "Error happens when evaluating WFPM_FORMAT_LOOKUP parameter.")
            ErrorMessages.Add(unManagedidsevalerrorformattimezonecodeparameter, "unManagedidsevalerrorformattimezonecodeparameter")
            ErrorMessages.Add(unManagedidsevalerrorformatdecimalparameter, "Error happens when evaluating WFPM_FORMAT_DECIMAL parameter.")
            ErrorMessages.Add(unManagedidsevalerrorformatintegerparameter, "Error happens when evaluating WFPM_FORMAT_INTEGER parameter.")
            ErrorMessages.Add(unManagedidsevalerrorobjecttype, "Error happens when evaluating WFPM_GetObjectType parameter.")
            ErrorMessages.Add(unManagedidsevalerrorqueueidparameter, "unManagedidsevalerrorqueueidparameter")
            ErrorMessages.Add(unManagedidsevalerrorformatpicklistparameter, "Error happens when evaluating WFPM_FORMAT_PICKLIST parameter.")
            ErrorMessages.Add(unManagedidsevalerrorformatbooleanparameter, "Error happens when evaluating WFPM_FORMAT_BOOLEAN parameter.")
            ErrorMessages.Add(unManagedidsevalerrorformatdatetimeparameter, "Error happens when evaluating WFPM_FORMAT_DATETIME parameter.")
            ErrorMessages.Add(unManagedidsevalerrorisnulllistparameter, "unManagedidsevalerrorisnulllistparameter")
            ErrorMessages.Add(unManagedidsevalerrorinlistparameter, "unManagedidsevalerrorinlistparameter")
            ErrorMessages.Add(unManagedidsevalerrorsetactivityparty, "unManagedidsevalerrorsetactivityparty")
            ErrorMessages.Add(unManagedidsevalerrorremovefromactivityparty, "unManagedidsevalerrorremovefromactivityparty")
            ErrorMessages.Add(unManagedidsevalerrorappendtoactivityparty, "unManagedidsevalerrorappendtoactivityparty")
            ErrorMessages.Add(unManagedidsevaltimererrorcalculatescheduletime, "Failed to calculate the schedule time for the timer action.")
            ErrorMessages.Add(unManagedidsevaltimerinvalidparameternumber, "Invalid parameters for Timer action.")
            ErrorMessages.Add(unManagedidsevalcreateshouldhave2parameters, "Create action should have 2 parameters.")
            ErrorMessages.Add(unManagedidsevalerrorcreate, "Error in create update.")
            ErrorMessages.Add(unManagedidsevalerrorcontainparameter, "Error occurred when evaluating a WFPM_CONTAIN parameter.")
            ErrorMessages.Add(unManagedidsevalerrorendwithparameter, "Error occurred when evaluating a WFPM_END_WITH parameter.")
            ErrorMessages.Add(unManagedidsevalerrorbeginwithparameter, "Error occurred when evaluating a WFPM_BEGIN_WITH parameter.")
            ErrorMessages.Add(unManagedidsevalerrorstrlenparameter, "Error occurred when evaluating a WFPM_STRLEN parameter.")
            ErrorMessages.Add(unManagedidsevalerrorsubstrparameter, "Error occurred when evaluating a WFPM_SUBSTR parameter.")
            ErrorMessages.Add(unManagedidsevalerrorinvalidrecipient, "Invalid email recipient.")
            ErrorMessages.Add(unManagedidsevalerrorinparameter, "Error occurred when evaluating a WFPM_IN parameter.")
            ErrorMessages.Add(unManagedidsevalerrorbetweenparameter, "Error occurred when evaluating a WFPM_BETWEEN parameter.")
            ErrorMessages.Add(unManagedidsevalerrorneqparameter, "Error occurred when evaluating a WFPM_NEQ parameter.")
            ErrorMessages.Add(unManagedidsevalerroreqparameter, "Error occurred when evaluating a WFPM_EQ parameter.")
            ErrorMessages.Add(unManagedidsevalerrorleqparameter, "Error occurred when evaluating a WFPM_LEQ parameter.")
            ErrorMessages.Add(unManagedidsevalerrorltparameter, "Error occurred when evaluating a WFPM_LT parameter.")
            ErrorMessages.Add(unManagedidsevalerrorgeqparameter, "Error occurred when evaluating a WFPM_GEQ parameter.")
            ErrorMessages.Add(unManagedidsevalerrorgtparameter, "Error occurred when evaluating a WFPM_GT parameter.")
            ErrorMessages.Add(unManagedidsevalerrorabsparameter, "Error occurred when evaluating a WFPM_ABS parameter.")
            ErrorMessages.Add(unManagedidsevalerrorinvalidparameter, "Invalid parameter.")
            ErrorMessages.Add(unManagedidsevalgenericerror, "Evaluation error.")
            ErrorMessages.Add(unManagedidsevalerrorincidentqueue, "Failed to evaluate INCIDENT_QUEUE.")
            ErrorMessages.Add(unManagedidsevalerrorhalt, "Error in action halt.")
            ErrorMessages.Add(unManagedidsevalerrorexec, "Error in action exec.")
            ErrorMessages.Add(unManagedidsevalerrorposturl, "Error in action posturl.")
            ErrorMessages.Add(unManagedidsevalerrorsetstate, "Error in action set state.")
            ErrorMessages.Add(unManagedidsevalerrorroute, "Error in action route.")
            ErrorMessages.Add(unManagedidsevalerrorupdate, "Error in action update.")
            ErrorMessages.Add(unManagedidsevalerrorassign, "Error in action assign.")
            ErrorMessages.Add(unManagedidsevalerroremailtemplate, "Error in action email template.")
            ErrorMessages.Add(unManagedidsevalerrorsendemail, "Error in action send email.")
            ErrorMessages.Add(unManagedidsevalerrorunhandleincident, "Error in action unhandle incident.")
            ErrorMessages.Add(unManagedidsevalerrorhandleincident, "Error in action handle incident.")
            ErrorMessages.Add(unManagedidsevalerrorcreateincident, "Error in action create incident.")
            ErrorMessages.Add(unManagedidsevalerrornoteattachment, "Error in action note attachment.")
            ErrorMessages.Add(unManagedidsevalerrorcreatenote, "Error in action create note.")
            ErrorMessages.Add(unManagedidsevalerrorunhandleactivity, "Error in action unhandle activity.")
            ErrorMessages.Add(unManagedidsevalerrorhandleactivity, "Error in action handle activity.")
            ErrorMessages.Add(unManagedidsevalerroractivityattachment, "Error in action activity attachment.")
            ErrorMessages.Add(unManagedidsevalerrorcreateactivity, "Error in action create activity.")
            ErrorMessages.Add(unManagedidsevalerrordividedbyzero, "Divided by zero.")
            ErrorMessages.Add(unManagedidsevalerrormodulusparameter, "Error occurred when evaluating a WFPM_MODULUR parameter.")
            ErrorMessages.Add(unManagedidsevalerrormodulusparameters, "Modulus parameter can have only two subparameters.")
            ErrorMessages.Add(unManagedidsevalerrordivisionparameter, "Error occurred when evaluating a WFPM_DIVISION parameter.")
            ErrorMessages.Add(unManagedidsevalerrordivisionparameters, "Division parameter can have only two subparameters.")
            ErrorMessages.Add(unManagedidsevalerrormultiplicationparameter, "Error occurred when evaluating a WFPM_MULTIPLICATION parameter.")
            ErrorMessages.Add(unManagedidsevalerrorsubtractionparameter, "Error occurred when evaluating a WFPM_SUBTRACTION parameter.")
            ErrorMessages.Add(unManagedidsevalerroraddparameter, "Error occurred when evaluating a WFPM_ADD parameter.")
            ErrorMessages.Add(unManagedidsevalmissselectquery, "Missing the query subparameter in a select parameter.")
            ErrorMessages.Add(unManagedidsevalchangetypeerror, "Change type error.")
            ErrorMessages.Add(unManagedidsevalallcompleted, "Evaluation completed and stop further processing.")
            ErrorMessages.Add(unManagedidsevalmetabaseattributenotmatchquery, "The specified refattributeid does not the query for a WFPM_SELECT parameter.")
            ErrorMessages.Add(unManagedidsevalmetabaseentitynotmatchquery, "The specified refentityid does not the query for a WFPM_SELECT parameter.")
            ErrorMessages.Add(unManagedidsevalpropertyisnull, "The required property of the object was not set.")
            ErrorMessages.Add(unManagedidsevalmetabaseattributenotfound, "The specified metabase attribute does not exist.")
            ErrorMessages.Add(unManagedidsevalmetabaseentitycompoundkeys, "The specified metabase object has compound keys. We do not support compound-key entities yet.")
            ErrorMessages.Add(unManagedidsevalpropertynotfound, "The required property of the object was not found.")
            ErrorMessages.Add(unManagedidsevalobjectnotfound, "The required object does not exist.")
            ErrorMessages.Add(unManagedidsevalcompleted, "Evaluation completed.")
            ErrorMessages.Add(unManagedidsevalaborted, "Evaluation aborted.")
            ErrorMessages.Add(unManagedidsevalallaborted, "Evaluation aborted and stop further processing.")
            ErrorMessages.Add(unManagedidsevalassignshouldhave4parameters, "Assign action should have 4 parameters.")
            ErrorMessages.Add(unManagedidsevalupdateshouldhave3parameters, "Update action should have 3 parameters.")
            ErrorMessages.Add(unManagedidscpdecryptfailed, "Decryption of the password failed.")
            ErrorMessages.Add(unManagedidscpencryptfailed, "Encryption of the supplied password failed.")
            ErrorMessages.Add(unManagedidscpbadpassword, "Incorrect password for the specified customer portal user.")
            ErrorMessages.Add(unManagedidscpuserdoesnotexist, "The customer portal user does not exist, or the password was incorrect.")
            ErrorMessages.Add(unManagedidsdataaccessunexpected, "Unexpected error in data access.  DB Connection may not have been opened successfully.")
            ErrorMessages.Add(unManagedidspropbagattributealreadyset, "One of the attributes passed has already been set")
            ErrorMessages.Add(unManagedidspropbagattributenotnullable, "One of the attributes passed cannot be NULL")
            ErrorMessages.Add(unManagedidsrspropbagdbinfoalreadyset, "The DB info for the recordset property bag has already been set.")
            ErrorMessages.Add(unManagedidsrspropbagdbinfonotset, "The DB info for the recordset property bag has not been set.")
            ErrorMessages.Add(unManagedidspropbagcolloutofrange, "The bag index in the collection was out of range.")
            ErrorMessages.Add(unManagedidspropbagnullproperty, "The specified property was null in the property bag.")
            ErrorMessages.Add(unManagedidspropbagnointerface, "The property bag interface could not be found.")
            ErrorMessages.Add(unManagedMissingObjectType, "Object type must be specified for one of the attributes.")
            ErrorMessages.Add(unManagedObjectTypeUnexpected, "Object type was specified for one of the attributes that does not allow it.")
            ErrorMessages.Add(BusinessUnitCannotBeDisabled, "Business unit cannot be disabled: no active user with system admin role exists outside of business unit subtree.")
            ErrorMessages.Add(BusinessUnitIsNotDisabledAndCannotBeDeleted, "Not disabled business unit cannot be deleted.")
            ErrorMessages.Add(BusinessUnitHasChildAndCannotBeDeleted, "Business unit has a child business unit and cannot be deleted.")
            ErrorMessages.Add(BusinessUnitDefaultTeamOwnsRecords, "Business unit default team owns records. Business unit cannot be deleted.")
            ErrorMessages.Add(RootBusinessUnitCannotBeDisabled, "Root Business unit cannot be disabled.")
            ErrorMessages.Add(unManagedidspropbagpropertynotfound, "The specified property was not found in the property bag.")
            ErrorMessages.Add(ReadOnlyUserNotSupported, "The read-only access mode is not supported")
            ErrorMessages.Add(SupportUserCannotBeCreateNorUpdated, "The support user cannot not be updated")
            ErrorMessages.Add(ApplicationUserCannotBeUpdated, "The user representing an OAuth application cannot not be updated")
            ErrorMessages.Add(ApplicationNotRegisteredWithDeployment, "Application needs to be registered and enabled at deployment level before it can be created for this organization")
            ErrorMessages.Add(InvalidOAuthToken, "The OAuth token is invalid")
            ErrorMessages.Add(ExpiredOAuthToken, "The OAuth token has expired")
            ErrorMessages.Add(CannotAssignRolesToSupportUser, "The support user are read-only, which cannot be assigned with other roles")
            ErrorMessages.Add(CannotMakeSelfReadOnlyUser, "You cannot make yourself a read only user")
            ErrorMessages.Add(CannotMakeReadOnlyUser, "A user cannot be made a read only user if they are the last non read only user that has the System Administrator Role.")
            ErrorMessages.Add(unManagedidsbizmgmtcantchangeorgname, "The organization name cannot be changed.")
            ErrorMessages.Add(MultipleOrganizationsNotAllowed, "Only one organization and one root business are allowed.")
            ErrorMessages.Add(UserSettingsInvalidAdvancedFindStartupMode, "Invalid advanced find startup mode.")
            ErrorMessages.Add(CannotModifySpecialUser, "No modifications to the 'SYSTEM' or 'INTEGRATION' user are permitted.")
            ErrorMessages.Add(unManagedidsbizmgmtcannotaddlocaluser, "A local user cannot be added to the CRM.")
            ErrorMessages.Add(CannotModifySysAdmin, "The System Administrator Role cannot be modified.")
            ErrorMessages.Add(CannotModifySupportUser, "The Support User Role cannot be modified.")
            ErrorMessages.Add(CannotAssignSupportUser, "The Support User Role cannot be assigned to a user.")
            ErrorMessages.Add(CannotRemoveFromSupportUser, "A user cannot be removed from the Support User Role.")
            ErrorMessages.Add(CannotCreateFromSupportUser, "Cannot create a role from Support User Role.")
            ErrorMessages.Add(CannotUpdateSupportUser, "Cannot update the Support User Role.")
            ErrorMessages.Add(CannotRemoveFromSysAdmin, "A user cannot be removed from the System Administrator Role if they are the only user that has the role.")
            ErrorMessages.Add(CannotDisableSysAdmin, "A user cannot be disabled if they are the only user that has the System Administrator Role.")
            ErrorMessages.Add(CannotDeleteSysAdmin, "The System Administrator Role cannot be deleted.")
            ErrorMessages.Add(CannotDeleteSupportUser, "The Support User Role cannot be deleted.")
            ErrorMessages.Add(CannotDeleteSystemCustomizer, "The System Customizer Role cannot be deleted.")
            ErrorMessages.Add(CannotCreateSyncUserObjectMissing, "This is not a valid Microsoft Online Services ID for this organization.")
            ErrorMessages.Add(CannotUpdateSyncUserIsLicensedField, "The property IsLicensed cannot be modified.")
            ErrorMessages.Add(CannotCreateSyncUserIsLicensedField, "The property IsLicensed cannot be set for Sync User Creation.")
            ErrorMessages.Add(CannotUpdateSyncUserIsSyncWithDirectoryField, "The property IsSyncUserWithDirectory cannot be modified.")
            ErrorMessages.Add(unManagedidsbizmgmtcannotreadaccountcontrol, "Insufficient permissions to the specified Active Directory user. Contact your System Administrator.")
            ErrorMessages.Add(UserAlreadyExists, "The specified Active Directory user already exists as a CRM user.")
            ErrorMessages.Add(unManagedidsbizmgmtusersettingsnotcreated, "The specified user's settings have not yet been created.")
            ErrorMessages.Add(ObjectNotFoundInAD, "The object does not exist in active directory.")
            ErrorMessages.Add(GenericActiveDirectoryError, "Active Directory Error.")
            ErrorMessages.Add(unManagedidsbizmgmtnoparentbusiness, "The specified business does not have a parent business.")
            ErrorMessages.Add(ParentUserDoesNotExist, "The parent user Id is invalid.")
            ErrorMessages.Add(ChildUserDoesNotExist, "The child user Id is invalid.")
            ErrorMessages.Add(UserLoopBeingCreated, "You cannot set the selected user as the manager for this user because the selected user is either already the manager or is in the user's immediate management hierarchy.  Either select another user to be the manager or do not update the record.")
            ErrorMessages.Add(UserLoopExists, "A manager for this user cannot be set because an existing relationship in the management hierarchy is causing a circular relationship.  This is usually caused by a manual edit of the Microsoft Dynamics CRM database. To fix this, the hierarchy in the database must be changed to remove the circular relationship.")
            ErrorMessages.Add(ParentBusinessDoesNotExist, "The parent business Id is invalid.")
            ErrorMessages.Add(ChildBusinessDoesNotExist, "The child businesss Id is invalid.")
            ErrorMessages.Add(BusinessManagementLoopBeingCreated, "Creating this parental association would create a loop in business hierarchy.")
            ErrorMessages.Add(BusinessManagementLoopExists, "Loop exists in the business hierarchy.")
            ErrorMessages.Add(BusinessManagementInvalidUserId, "The user Id is invalid.")
            ErrorMessages.Add(unManagedidsbizmgmtuserdoesnothaveparent, "This user does not have a parent user.")
            ErrorMessages.Add(unManagedidsbizmgmtcannotenableprovision, "This is a provisioned root-business. Use IBizProvision::Enable to enable this root-business.")
            ErrorMessages.Add(unManagedidsbizmgmtcannotenablebusiness, "This is a sub-business. Use IBizMerchant::Enable to enable this sub-business.")
            ErrorMessages.Add(unManagedidsbizmgmtcannotdisableprovision, "This is a provisioned root-business. Use IBizProvision::Disable to disable this root-business.")
            ErrorMessages.Add(unManagedidsbizmgmtcannotdisablebusiness, "This business unit cannot be disabled.")
            ErrorMessages.Add(unManagedidsbizmgmtcannotdeleteprovision, "This is a provisioned root-business. Use IBizProvision::Delete to delete this root-business.")
            ErrorMessages.Add(unManagedidsbizmgmtcannotdeletebusiness, "This is a sub-business. Use IBizMerchant::Delete to delete this sub-business.")
            ErrorMessages.Add(unManagedidsbizmgmtcannotremovepartnershipdefaultuser, "The default user of a partnership can not be removed.")
            ErrorMessages.Add(unManagedidsbizmgmtpartnershipnotinpendingstatus, "The partnership has been accepted or declined.")
            ErrorMessages.Add(unManagedidsbizmgmtdefaultusernotinpartnerbusiness, "The default user is not from partner business.")
            ErrorMessages.Add(unManagedidsbizmgmtcallernotinpartnerbusiness, "The caller is not from partner business.")
            ErrorMessages.Add(unManagedidsbizmgmtdefaultusernotinprimarybusiness, "The default user is not from primary business.")
            ErrorMessages.Add(unManagedidsbizmgmtcallernotinprimarybusiness, "The caller is not from primary business.")
            ErrorMessages.Add(unManagedidsbizmgmtpartnershipalreadyexists, "A partnership between specified primary business and partner business already exists.")
            ErrorMessages.Add(unManagedidsbizmgmtprimarysameaspartner, "The primary business is the same as partner business.")
            ErrorMessages.Add(unManagedidsbizmgmtmisspartnerbusiness, "The partnership partner business was unexpectedly missing.")
            ErrorMessages.Add(unManagedidsbizmgmtmissprimarybusiness, "The partnership primary business was unexpectedly missing.")
            ErrorMessages.Add(InvalidAccessModeTransition, "The client access license cannot be changed because the user does not have a Microsoft Dynamics CRM Online license. To change the access mode, you must first add a license for this user in the Microsoft Online Service portal.")
            ErrorMessages.Add(MissingTeamName, "The team name was unexpectedly missing.")
            ErrorMessages.Add(TeamAdministratorMissedPrivilege, "The team administrator does not have privilege read team.")
            ErrorMessages.Add(CannotDisableTenantAdmin, "Users who are granted the Microsoft Office 365 Global administrator or Service administrator role cannot be disabled in Microsoft Dynamics CRM Online. You must first remove the Microsoft Office 365 role, and then try again.")
            ErrorMessages.Add(CannotRemoveTenantAdminFromSysAdminRole, "Users who are granted the Microsoft Office 365 Global administrator or Service administrator role cannot be removed from the Microsoft Dynamics CRM System Administrator security role. You must first remove the Microsoft Office 365 role, and then try again.")
            ErrorMessages.Add(UserNotInParentHierarchy, "The user is not in parent user's business hierarchy.")
            ErrorMessages.Add(unManagedidsbizmgmtusercannotbeownparent, "The user can not be its own parent user.")
            ErrorMessages.Add(unManagedidsbizmgmtcannotmovedefaultuser, "unManagedidsbizmgmtcannotmovedefaultuser")
            ErrorMessages.Add(unManagedidsbizmgmtbusinessparentdiffmerchant, "The business is not in the same merchant as parent business.")
            ErrorMessages.Add(unManagedidsbizmgmtdefaultusernotinbusiness, "The default user is not in the business.")
            ErrorMessages.Add(unManagedidsbizmgmtmissparentbusiness, "The parent business was unexpectedly missing.")
            ErrorMessages.Add(unManagedidsbizmgmtmissuserdomainname, "The user's domain name was unexpectedly missing.")
            ErrorMessages.Add(unManagedidsbizmgmtmissbusinessname, "The business name was unexpectedly missing.")
            ErrorMessages.Add(unManagedidsxmlinvalidread, "A field that is not valid for read was specified")
            ErrorMessages.Add(unManagedidsxmlinvalidfield, "An invalid value was passed in for a field")
            ErrorMessages.Add(unManagedidsxmlinvalidentityattributes, "Invalid attributes")
            ErrorMessages.Add(unManagedidsxmlunexpected, "An unexpected error has occurred")
            ErrorMessages.Add(unManagedidsxmlparseerror, "A parse error was encountered in the XML")
            ErrorMessages.Add(unManagedidsxmlinvalidcollectionname, "The collection name specified is incorrect")
            ErrorMessages.Add(unManagedidsxmlinvalidupdate, "A field that is not valid for update was specified")
            ErrorMessages.Add(unManagedidsxmlinvalidcreate, "A field that is not valid for create was specified")
            ErrorMessages.Add(unManagedidsxmlinvalidentityname, "The entity name specified is incorrect")
            ErrorMessages.Add(unManagedidsnotesnoattachment, "The specified note has no attachments.")
            ErrorMessages.Add(unManagedidsnotesloopbeingcreated, "Creating this parental association would create a loop in the annotation hierarchy.")
            ErrorMessages.Add(unManagedidsnotesloopexists, "A loop exists in the annotation hierarchy.")
            ErrorMessages.Add(unManagedidsnotesalreadyattached, "The specified note is already attached to an object.")
            ErrorMessages.Add(unManagedidsnotesnotedoesnotexist, "The specified note does not exist.")
            ErrorMessages.Add(DuplicatedPrivilege, "Privilege {0} is duplicated.")
            ErrorMessages.Add(MemberHasAlreadyBeenContacted, "This marketing list member was not contacted, because the member has previously received this communication.")
            ErrorMessages.Add(TeamInWrongBusiness, "The team belongs to a different business unit than the role.")
            ErrorMessages.Add(unManagedidsrolesdeletenonparentrole, "Cannot delete a role that is inherited from a parent business.")
            ErrorMessages.Add(InvalidPrivilegeDepth, "Invalid privilege depth.")
            ErrorMessages.Add(unManagedidsrolesinvalidrolename, "The role name is invalid.")
            ErrorMessages.Add(UserInWrongBusiness, "The user belongs to a different business unit than the role.")
            ErrorMessages.Add(unManagedidsrolesmissprivid, "The privilege ID was unexpectedly missing.")
            ErrorMessages.Add(unManagedidsrolesmissrolename, "The role name was unexpectedly missing.")
            ErrorMessages.Add(unManagedidsrolesmissbusinessid, "The role's business unit ID was unexpectedly missing.")
            ErrorMessages.Add(unManagedidsrolesmissroleid, "The role ID was unexpectedly missing.")
            ErrorMessages.Add(unManagedidsrolesinvalidtemplateid, "Invalid role template ID.")
            ErrorMessages.Add(RoleAlreadyExists, "A role with the specified name already exists.")
            ErrorMessages.Add(unManagedidsrolesroledoesnotexist, "The specified role does not exist.")
            ErrorMessages.Add(unManagedidsrolesinvalidroleid, "Invalid role ID.")
            ErrorMessages.Add(unManagedidsrolesinvalidroledata, "The role data is invalid.")
            ErrorMessages.Add(QueryBuilderSerializationInvalidIsQuickFindFilter, "The only valid values for isquickfindfields attribute are 'true', 'false', '1', and '0'.")
            ErrorMessages.Add(QueryBuilderAttributeCannotBeGroupByAndAggregate, "An attribute can either be an aggregate or a Group By but not both")
            ErrorMessages.Add(SqlArithmeticOverflowError, "A SQL arithmetic overflow error occurred")
            ErrorMessages.Add(QueryBuilderInvalidDateGrouping, "An invalid value was specified for dategrouping.")
            ErrorMessages.Add(QueryBuilderAliasRequiredForAggregateOrderBy, "An alias is required for an order clause for an aggregate Query.")
            ErrorMessages.Add(QueryBuilderAttributeRequiredForNonAggregateOrderBy, "An attribute is required for an order clause for a non-aggregate Query.")
            ErrorMessages.Add(QueryBuilderAliasNotAllowedForNonAggregateOrderBy, "An alias cannot be specified for an order clause for a non-aggregate Query. Use an attribute.")
            ErrorMessages.Add(QueryBuilderAttributeNotAllowedForAggregateOrderBy, "An attribute cannot be specified for an order clause for an aggregate Query. Use an alias.")
            ErrorMessages.Add(QueryBuilderDuplicateAlias, "FetchXML should have unique aliases.")
            ErrorMessages.Add(QueryBuilderInvalidAggregateAttribute, "Aggregate {0} is not supported for attribute of type {1}.")
            ErrorMessages.Add(QueryBuilderDeserializeInvalidGroupBy, "The only valid values for groupby attribute are 'true', 'false', '1', and '0'.")
            ErrorMessages.Add(QueryBuilderNoAttrsDistinctConflict, "The no-attrs tag cannot be used in conjuction with Distinct set to true.")
            ErrorMessages.Add(QueryBuilderInvalidPagingCookie, "Invalid page number in paging cookie.")
            ErrorMessages.Add(QueryBuilderPagingOrderBy, "Order by columns do not match those in paging cookie.")
            ErrorMessages.Add(QueryBuilderEntitiesDontMatch, "The entity name specified in fetchxml does not match the entity name specified in the Entity or Query Expression.")
            ErrorMessages.Add(QueryBuilderLinkNodeForOrderNotFound, "Converting from Query to EntityExpression failed. Link Node for order was not found.")
            ErrorMessages.Add(QueryBuilderDeserializeNoDocElemXml, "Document Element can't be null.")
            ErrorMessages.Add(QueryBuilderDeserializeEmptyXml, "Xml String can't be null.")
            ErrorMessages.Add(QueryBuilderElementNotFound, "A required element was not specified.")
            ErrorMessages.Add(QueryBuilderInvalidFilterType, "Unsupported filter type. Valid values are 'and', or 'or'.")
            ErrorMessages.Add(QueryBuilderInvalidJoinOperator, "Unsupported join operator.")
            ErrorMessages.Add(QueryBuilderInvalidConditionOperator, "Unsupported condition operator.")
            ErrorMessages.Add(QueryBuilderInvalidOrderType, "A valid order type must be set in the order before calling this method.")
            ErrorMessages.Add(QueryBuilderAttributeNotFound, "A required attribute was not specified.")
            ErrorMessages.Add(QueryBuilderDeserializeInvalidUtcOffset, "The utc-offset attribute is not supported for deserialization.")
            ErrorMessages.Add(QueryBuilderDeserializeInvalidNode, "The element node encountered is invalid.")
            ErrorMessages.Add(QueryBuilderDeserializeInvalidGetMinActiveRowVersion, "The only valid values for GetMinActiveRowVersion attribute are 'true', 'false', '1', and '0'.")
            ErrorMessages.Add(QueryBuilderDeserializeInvalidAggregate, "An error occurred while processing Aggregates in Query")
            ErrorMessages.Add(QueryBuilderDeserializeInvalidDescending, "The only valid values for descending attribute are 'true', 'false', '1', and '0'.")
            ErrorMessages.Add(QueryBuilderDeserializeInvalidNoLock, "The only valid values for no-lock attribute are 'true', 'false', '1', and '0'.")
            ErrorMessages.Add(QueryBuilderDeserializeInvalidLinkType, "The only valid values for link-type attribute are 'natural', 'inner', and 'outer'.")
            ErrorMessages.Add(QueryBuilderDeserializeInvalidMapping, "The only valid values for mapping are 'logical' or 'internal' which is deprecated.")
            ErrorMessages.Add(QueryBuilderDeserializeInvalidDistinct, "The only valid values for distinct attribute are 'true', 'false', '1', and '0'.")
            ErrorMessages.Add(QueryBuilderSerialzeLinkTopCriteria, "Fetch does not support where clause with conditions from linkentity.")
            ErrorMessages.Add(QueryBuilderColumnSetVersionMissing, "The specified columnset version is invalid.")
            ErrorMessages.Add(QueryBuilderInvalidColumnSetVersion, "The specified columnset version is invalid.")
            ErrorMessages.Add(QueryBuilderAttributePairMismatch, "AttributeFrom and AttributeTo must be either both specified or both omitted.")
            ErrorMessages.Add(QueryBuilderByAttributeNonEmpty, "QueryByAttribute must specify a non-empty attribute array.")
            ErrorMessages.Add(QueryBuilderByAttributeMismatch, "QueryByAttribute must specify a non-empty value array with the same number of elements as in the attributes array.")
            ErrorMessages.Add(QueryBuilderMultipleIntersectEntities, "More than one intersect entity exists between the two entities specified.")
            ErrorMessages.Add(QueryBuilderReportView_Does_Not_Exist, "A report view does not exist for the specified entity.")
            ErrorMessages.Add(QueryBuilderValue_GreaterThanZero, "A value greater than zero must be specified.")
            ErrorMessages.Add(QueryBuilderNoAlias, "No alias for the given entity in the condition was found.")
            ErrorMessages.Add(QueryBuilderAlias_Does_Not_Exist, "The specified alias for the given entity in the condition does not exist.")
            ErrorMessages.Add(QueryBuilderInvalid_Alias, "Invalid alias for aggregate operation.")
            ErrorMessages.Add(QueryBuilderInvalid_Value, "Invalid value specified for type.")
            ErrorMessages.Add(QueryBuilderAttribute_With_Aggregate, "Attributes can not be returned when aggregate operation is specified.")
            ErrorMessages.Add(QueryBuilderBad_Condition, "Incorrect filter condition or conditions.")
            ErrorMessages.Add(QueryBuilderNoAttribute, "The specified attribute does not exist on this entity.")
            ErrorMessages.Add(QueryBuilderNoEntity, "The specified entity was not found.")
            ErrorMessages.Add(QueryBuilderUnexpected, "An unexpected error occurred.")
            ErrorMessages.Add(QueryBuilderInvalidUpdate, "An attempt was made to update a non-updateable field.")
            ErrorMessages.Add(QueryBuilderInvalidLogicalOperator, "Unsupported logical operator: {0}.  Accepted values are ('and', 'or').")
            ErrorMessages.Add(unManagedidsmetadatanorelationship, "The relationship does not exist")
            ErrorMessages.Add(MetadataNoMapping, "The mapping between specified entities does not exist")
            ErrorMessages.Add(MetadataNotSerializable, "The given metadata entity is not serializable")
            ErrorMessages.Add(unManagedidsmetadatanoentity, "The specified entity does not exist")
            ErrorMessages.Add(unManagedidscommunicationsnosenderaddress, "The sender does not have an email address on the party record")
            ErrorMessages.Add(unManagedidscommunicationstemplateinvalidtemplate, "The template body is invalid")
            ErrorMessages.Add(unManagedidscommunicationsnoparticipationmask, "Participation type is missing from an activity")
            ErrorMessages.Add(unManagedidscommunicationsnorecipients, "At least one system user or queue in the organization must be a recipient")
            ErrorMessages.Add(EmailRecipientNotSpecified, "The e-mail must have at least one recipient before it can be sent")
            ErrorMessages.Add(unManagedidscommunicationsnosender, "No email address was specified, and the calling user does not have an email address set")
            ErrorMessages.Add(unManagedidscommunicationsbadsender, "More than one sender specified")
            ErrorMessages.Add(unManagedidscommunicationsnopartyaddress, "Object address not found on party or party is marked as non-emailable")
            ErrorMessages.Add(unManagedidsjournalingmissingincidentid, "Incident Id missed.")
            ErrorMessages.Add(unManagedidsjournalingmissingcontactid, "Contact Id missed.")
            ErrorMessages.Add(unManagedidsjournalingmissingopportunityid, "Opportunity Id missed.")
            ErrorMessages.Add(unManagedidsjournalingmissingaccountid, "Account Id missed.")
            ErrorMessages.Add(unManagedidsjournalingmissingleadid, "Lead Id missed.")
            ErrorMessages.Add(unManagedidsjournalingmissingeventtype, "Event type missed.")
            ErrorMessages.Add(unManagedidsjournalinginvalideventtype, "Invalid event type.")
            ErrorMessages.Add(unManagedidsjournalingmissingeventdirection, "Event direction code missed.")
            ErrorMessages.Add(unManagedidsjournalingunsupportedobjecttype, "Unsupported type of objects passed in operation.")
            ErrorMessages.Add(SdkEntityDoesNotSupportMessage, "The method being invoked does not support provided entity type.")
            ErrorMessages.Add(OpportunityAlreadyInOpenState, "The opportunity is already in the open state.")
            ErrorMessages.Add(LeadAlreadyInClosedState, "The lead is already closed.")
            ErrorMessages.Add(LeadAlreadyInOpenState, "The lead is already in the open state.")
            ErrorMessages.Add(CustomerIsInactive, "An inactive customer cannot be set as the parent of an object.")
            ErrorMessages.Add(OpportunityCannotBeClosed, "The opportunity cannot be closed.")
            ErrorMessages.Add(OpportunityIsAlreadyClosed, "The opportunity is already closed.")
            ErrorMessages.Add(unManagedidscustomeraddresstypeinvalid, "Invalid customer address type.")
            ErrorMessages.Add(unManagedidsleadnotassignedtocaller, "The lead is not being assigned to the caller for acceptance.")
            ErrorMessages.Add(unManagedidscontacthaschildopportunities, "The Contact has child opportunities.")
            ErrorMessages.Add(unManagedidsaccounthaschildopportunities, "The Account has child opportunities.")
            ErrorMessages.Add(unManagedidsleadoneaccount, "A lead can be associated with only one account.")
            ErrorMessages.Add(unManagedidsopportunityorphan, "Removing this association will make the opportunity an orphan.")
            ErrorMessages.Add(unManagedidsopportunityoneaccount, "An opportunity can be associated with only one account.")
            ErrorMessages.Add(unManagedidsleadusercannotreject, "The user does not have the privilege to reject a lead, so he cannot be assigned the lead for acceptance.")
            ErrorMessages.Add(unManagedidsleadnotassigned, "The lead has not been assigned.")
            ErrorMessages.Add(unManagedidsleadnoparent, "The lead does not have a parent.")
            ErrorMessages.Add(ContactLoopBeingCreated, "Creating this parental association would create a loop in Contacts hierarchy.")
            ErrorMessages.Add(ContactLoopExists, "Loop exists in the contacts hierarchy.")
            ErrorMessages.Add(PresentParentAccountAndParentContact, "You can either specify a contacts parent contact or its account, but not both.")
            ErrorMessages.Add(AccountLoopBeingCreated, "Creating this parental association would create a loop in Accounts hierarchy.")
            ErrorMessages.Add(AccountLoopExists, "Loop exists in the accounts hierarchy.")
            ErrorMessages.Add(unManagedidsopportunitymissingparent, "The parent of the opportunity is missing.")
            ErrorMessages.Add(unManagedidsopportunityinvalidparent, "The parent of an opportunity must be an account or contact.")
            ErrorMessages.Add(ContactDoesNotExist, "Contact does not exist.")
            ErrorMessages.Add(AccountDoesNotExist, "Account does not exist.")
            ErrorMessages.Add(unManagedidsleaddoesnotexist, "Lead does not exist.")
            ErrorMessages.Add(unManagedidsopportunitydoesnotexist, "Opportunity does not exist.")
            ErrorMessages.Add(ReportDoesNotExist, "Report does not exist. ReportId:{0}")
            ErrorMessages.Add(ReportLoopBeingCreated, "Creating this parental association would create a loop in Reports hierarchy.")
            ErrorMessages.Add(ReportLoopExists, "Loop exists in the reports hierarchy.")
            ErrorMessages.Add(ParentReportLinksToSameNameChild, "Parent report already links to another report with the same name.")
            ErrorMessages.Add(DuplicateReportVisibility, "A ReportVisibility with the same ReportId and VisibilityCode already exists. Duplicates are not allowed.")
            ErrorMessages.Add(ReportRenderError, "An error occurred during report rendering.")
            ErrorMessages.Add(SubReportDoesNotExist, "Subreport does not exist. ReportId:{0}")
            ErrorMessages.Add(SrsDataConnectorNotInstalled, "MSCRM Data Connector Not Installed")
            ErrorMessages.Add(InvalidCustomReportingWizardXml, "Invalid wizard xml")
            ErrorMessages.Add(UpdateNonCustomReportFromTemplate, "Cannot update a report from a template if the report was not created from a template")
            ErrorMessages.Add(SnapshotReportNotReady, "The selected report is not ready for viewing. The report is still being created or a report snapshot is not available. ReportId:{0}")
            ErrorMessages.Add(ExistingExternalReport, "The report could not be published for external use because a report of the same name already exists. Delete that report in SQL Server Reporting Services or rename this report, and try again.")
            ErrorMessages.Add(ParentReportNotSupported, "Parent report is not supported for the type of report specified. Only SQL Reporting Services reports can have parent reports.")
            ErrorMessages.Add(ParentReportDoesNotReferenceChild, "Specified parent report does not reference the current one. Only SQL Reporting Services reports can have parent reports.")
            ErrorMessages.Add(MultipleParentReportsFound, "More than one report link found. Each report can have only one parent.")
            ErrorMessages.Add(ReportingServicesReportExpected, "The report is not a Reporting Services report.")
            ErrorMessages.Add(InvalidTransformationParameter, "A parameter for the transformation is either missing or invalid.")
            ErrorMessages.Add(ReflexiveEntityParentOrChildDoesNotExist, "Either the parent or child entity does not exist")
            ErrorMessages.Add(EntityLoopBeingCreated, "Creating this parental association would create a loop in this entity hierarchy.")
            ErrorMessages.Add(EntityLoopExists, "Loop exists in this entity hierarchy.")
            ErrorMessages.Add(UnsupportedProcessCode, "The process code is not supported on this entity.")
            ErrorMessages.Add(NoOutputTransformationParameterMappingFound, "There is no output transformation parameter mapping defined. A transformation mapping must have atleast one output transformation parameter mapping.")
            ErrorMessages.Add(RequiredColumnsNotFoundInImportFile, "One or more source columns used in the transformation do not exist in the source file.")
            ErrorMessages.Add(InvalidTransformationParameterMapping, "The transformation parameter mapping defined is invalid. Check that the target attribute name exists.")
            ErrorMessages.Add(UnmappedTransformationOutputDataFound, "One or more outputs returned by the transformation is not mapped to target fields.")
            ErrorMessages.Add(InvalidTransformationParameterDataType, "The data type of one or more of the transformation parameters is unsupported.")
            ErrorMessages.Add(ArrayMappingFoundForSingletonParameter, "An array transformation parameter mapping is defined for a single parameter.")
            ErrorMessages.Add(SingletonMappingFoundForArrayParameter, "A single transformation parameter mapping is defined for an array parameter.")
            ErrorMessages.Add(IncompleteTransformationParameterMappingsFound, "One or more mandatory transformation parameters do not have mappings defined for them.")
            ErrorMessages.Add(InvalidTransformationParameterMappings, "One or more transformation parameter mappings are invalid or do not match the transformation parameter description.")
            ErrorMessages.Add(GenericTransformationInvocationError, "The transformation returned invalid data.")
            ErrorMessages.Add(InvalidTransformationType, "The specified transformation type is not supported.")
            ErrorMessages.Add(UnableToLoadTransformationType, "Unable to load the transformation type.")
            ErrorMessages.Add(UnableToLoadTransformationAssembly, "Unable to load the transformation assembly.")
            ErrorMessages.Add(InvalidColumnMapping, "ColumnMapping is Invalid. Check that the target attribute exists.")
            ErrorMessages.Add(CannotModifyOldDataFromImport, "The corresponding record in Microsoft Dynamics CRM has more recent data, so this record was ignored.")
            ErrorMessages.Add(ImportFileTooLargeToUpload, "The import file is too large to upload.")
            ErrorMessages.Add(InvalidImportFileContent, "The content of the import file is not valid. You must select a text file.")
            ErrorMessages.Add(EmptyRecord, "The record is empty")
            ErrorMessages.Add(LongParseRow, "The row is too long to import")
            ErrorMessages.Add(ParseMustBeCalledBeforeTransform, "Cannot call transform before parse.")
            ErrorMessages.Add(HeaderValueDoesNotMatchAttributeDisplayLabel, "The column heading does not match the attribute display label.")
            ErrorMessages.Add(InvalidTargetEntity, "The specified target record type does not exist.")
            ErrorMessages.Add(NoHeaderColumnFound, "A column heading is missing.")
            ErrorMessages.Add(ParsingMetadataNotFound, "Data required to parse the file, such as the data delimiter, field delimiter, or column headings, was not found.")
            ErrorMessages.Add(EmptyHeaderRow, "The first row of the file is empty.")
            ErrorMessages.Add(EmptyContent, "The file is empty.")
            ErrorMessages.Add(InvalidIsFirstRowHeaderForUseSystemMap, "The first row of the file does not contain column headings.")
            ErrorMessages.Add(InvalidGuid, "The globally unique identifier (GUID) in this row is invalid")
            ErrorMessages.Add(GuidNotPresent, "The required globally unique identifier (GUID) in this row is not present")
            ErrorMessages.Add(OwnerValueNotMapped, "The owner value is not mapped")
            ErrorMessages.Add(PicklistValueNotMapped, "The record could not be processed as the Option set value could not be mapped.")
            ErrorMessages.Add(ErrorInDelete, "The Microsoft Dynamics CRM record could not be deleted")
            ErrorMessages.Add(ErrorIncreate, "The Microsoft Dynamics CRM record could not be created")
            ErrorMessages.Add(ErrorInUpdate, "The Microsoft Dynamics CRM record could not be updated")
            ErrorMessages.Add(ErrorInSetState, "The status or status reason of the Microsoft Dynamics CRM record could not be set")
            ErrorMessages.Add(InvalidDataFormat, "The source data is not in the required format")
            ErrorMessages.Add(InvalidFormatForDataDelimiter, "Mismatched data delimiter: only one delimiter was found.")
            ErrorMessages.Add(CRMUserDoesNotExist, "No Microsoft Dynamics CRM user exists with the specified domain name and user ID")
            ErrorMessages.Add(LookupNotFound, "The lookup reference could not be resolved")
            ErrorMessages.Add(DuplicateLookupFound, "A duplicate lookup reference was found")
            ErrorMessages.Add(InvalidImportFileData, "The data is not in the required format")
            ErrorMessages.Add(InvalidXmlSSContent, "The file could not be uploaded because the file format is not valid. Save the file in the XML Spreadsheet 2003 (.xml) format and then upload.")
            ErrorMessages.Add(InvalidImportFileParseData, "Field and data delimiters for this file are not specified.")
            ErrorMessages.Add(InvalidValueForFileType, "The file type is invalid.")
            ErrorMessages.Add(EmptyImportFileRow, "Empty row.")
            ErrorMessages.Add(ErrorInParseRow, "The row could not be parsed. This is typically caused by a row that is too long.")
            ErrorMessages.Add(DataColumnsNumberMismatch, "The number of fields differs from the number of column headings.")
            ErrorMessages.Add(InvalidHeaderColumn, "The column heading contains an invalid combination of data delimiters.")
            ErrorMessages.Add(OwnerMappingExistsWithSourceSystemUserName, "The data map already contains this owner mapping.")
            ErrorMessages.Add(PickListMappingExistsWithSourceValue, "The data map already contains this list value mapping.")
            ErrorMessages.Add(InvalidValueForDataDelimiter, "The data delimiter is invalid.")
            ErrorMessages.Add(InvalidValueForFieldDelimiter, "The field delimiter is invalid.")
            ErrorMessages.Add(PickListMappingExistsForTargetValue, "This list value is mapped more than once. Remove any duplicate mappings, and then import this data map again.")
            ErrorMessages.Add(MappingExistsForTargetAttribute, "This attribute is mapped more than once. Remove any duplicate mappings, and then import this data map again.")
            ErrorMessages.Add(SourceEntityMappedToMultipleTargets, "This source entity is mapped to more than one Microsoft Dynamics CRM entity. Remove any duplicate mappings, and then import this data map again.")
            ErrorMessages.Add(AttributeNotOfTypePicklist, "This attribute is not mapped to a drop-down list, Boolean, or state/status attribute. However, you have included a ListValueMap element for it.  Fix this inconsistency, and then import this data map again.")
            ErrorMessages.Add(AttributeNotOfTypeReference, "This attribute is not mapped as a reference attribute. However, you have included a ReferenceMap for it.  Fix this inconsistency, and then import this data map again.")
            ErrorMessages.Add(TargetEntityNotFound, "The file specifies an entity that does not exist in Microsoft Dynamics CRM.")
            ErrorMessages.Add(TargetAttributeNotFound, "The file specifies an attribute that does not exist in Microsoft Dynamics CRM.")
            ErrorMessages.Add(PicklistValueNotFound, "The file specifies a list value that does not exist in Microsoft Dynamics CRM.")
            ErrorMessages.Add(TargetAttributeInvalidForMap, "This attribute is not valid for mapping.")
            ErrorMessages.Add(TargetEntityInvalidForMap, "The file specifies an entity that is not valid for data migration.")
            ErrorMessages.Add(InvalidFileBadCharacters, "The file could not be uploaded because it contains invalid character(s)")
            ErrorMessages.Add(ErrorsInImportFiles, "Invalid File(s) for Import")
            ErrorMessages.Add(InvalidOperationWhenListIsNotActive, "List is not active. Cannot perform this operation.")
            ErrorMessages.Add(InvalidOperationWhenPartyIsNotActive, "The party is not active. Cannot perform this operation.")
            ErrorMessages.Add(AsyncOperationSuspendedOrLocked, ">A background job associated with this import is either suspended or locked. In order to delete this import, in the Workplace, click Imports, open the import, click System Jobs, and resume any suspended jobs.")
            ErrorMessages.Add(DuplicateHeaderColumn, "A duplicate column heading exists.")
            ErrorMessages.Add(EmptyHeaderColumn, "The column heading cannot be empty.")
            ErrorMessages.Add(InvalidColumnNumber, "The column number specified in the data map does not exist.")
            ErrorMessages.Add(TransformMustBeCalledBeforeImport, "Cannot call import before transform.")
            ErrorMessages.Add(OperationCanBeCalledOnlyOnce, "The specified action can be done only one time.")
            ErrorMessages.Add(DuplicateRecordsFound, "A record was not created or updated because a duplicate of the current record already exists.")
            ErrorMessages.Add(CampaignActivityClosed, "This Campaign Activity is closed or canceled. Campaign activities cannot be distributed after they have been closed or canceled.")
            ErrorMessages.Add(UnexpectedErrorInMailMerge, "There was an unexpected error during mail merge.")
            ErrorMessages.Add(UserCancelledMailMerge, "The mail merge operation was cancelled by the user.")
            ErrorMessages.Add(FilteredDuetoMissingEmailAddress, "This customer is filtered due to missing email address.")
            ErrorMessages.Add(CannotDeleteAsBackgroundOperationInProgress, "This record is currently being used by Microsoft Dynamics CRM and cannot be deleted. Try again later. If  the problem persists, contact your system administrator.")
            ErrorMessages.Add(FilteredDuetoInactiveState, "This customer is filtered due to inactive state.")
            ErrorMessages.Add(MissingBOWFRules, "Bulk Operation related workflow rules are missing.")
            ErrorMessages.Add(CannotSpecifyOwnerForActivityPropagation, "Cannot specify owner on activity for distribution")
            ErrorMessages.Add(CampaignActivityAlreadyPropagated, "This campaign activity has been distributed already. Campaign activities cannot be distributed more than one time.")
            ErrorMessages.Add(FilteredDuetoAntiSpam, "This customer is filtered due to AntiSpam settings.")
            ErrorMessages.Add(TemplateTypeNotSupportedForUnsubscribeAcknowledgement, "This template type is not supported for unsubscribe acknowledgement.")
            ErrorMessages.Add(ErrorInImportConfig, "Cannot process with Bulk Import as Import Configuration has some errors.")
            ErrorMessages.Add(ImportConfigNotSpecified, "Cannot process with Bulk Import as Import Configuration not specified.")
            ErrorMessages.Add(InvalidActivityType, "An invalid object type was specified for distributing activities.")
            ErrorMessages.Add(UnsupportedParameter, "A parameter specified is not supported by the Bulk Operation")
            ErrorMessages.Add(MissingParameter, "A required parameter is missing for the Bulk Operation")
            ErrorMessages.Add(CannotSpecifyCommunicationAttributeOnActivityForPropagation, "Cannot specify communication attribute on activity for distribution")
            ErrorMessages.Add(CannotSpecifyRecipientForActivityPropagation, "Cannot specify a recipient for activity distribution.")
            ErrorMessages.Add(CannotSpecifyAttendeeForAppointmentPropagation, "Cannot specify an attendee for appointment distribution.")
            ErrorMessages.Add(CannotSpecifySenderForActivityPropagation, "Cannot specify a sender for appointment distribution")
            ErrorMessages.Add(CannotSpecifyOrganizerForAppointmentPropagation, "Cannot specify an organizer for appointment distribution")
            ErrorMessages.Add(InvalidRegardingObjectTypeCode, "The regarding Object Type Code is not valid for the Bulk Operation.")
            ErrorMessages.Add(UnspecifiedActivityXmlForCampaignActivityPropagate, "Must specify an Activity Xml for CampaignActivity Execute/Distribute")
            ErrorMessages.Add(MoneySizeExceeded, "Supplied value exceeded the MIN/MAX value of Money Type field.")
            ErrorMessages.Add(ExtraPartyInformation, "Extra party information should not be provided for this operation.")
            ErrorMessages.Add(NotSupported, "This action is not supported.")
            ErrorMessages.Add(InvalidOperationForClosedOrCancelledCampaignActivity, "Can not add items to closed (cancelled) campaignactivity.")
            ErrorMessages.Add(InvalidEmailTemplate, "Must specify a valid Template Id")
            ErrorMessages.Add(CannotCreateResponseForTemplate, "CampaignResponse can not be created for Template Campaign.")
            ErrorMessages.Add(CannotPropagateCamapaignActivityForTemplate, "Cannot execute (distribute) a CampaignActivity for a template Campaign.")
            ErrorMessages.Add(InvalidChannelForCampaignActivityPropagate, "Cannot distribute activities for campaign activities of the specified channel type.")
            ErrorMessages.Add(InvalidActivityTypeForCampaignActivityPropagate, "Must specify a valid CommunicationActivity")
            ErrorMessages.Add(ObjectNotRelatedToCampaign, "Specified Object not related to the parent Campaign")
            ErrorMessages.Add(CannotRelateObjectTypeToCampaignActivity, "Specified Object Type not supported")
            ErrorMessages.Add(CannotUpdateCampaignForCampaignResponse, "Parent campaign is not updatable.")
            ErrorMessages.Add(CannotUpdateCampaignForCampaignActivity, "Parent campaign is not updatable.")
            ErrorMessages.Add(CampaignNotSpecifiedForCampaignResponse, "RegardingObjectId is a required field.")
            ErrorMessages.Add(CampaignNotSpecifiedForCampaignActivity, "RegardingObjectId is a required field.")
            ErrorMessages.Add(CannotRelateObjectTypeToCampaign, "Specified Object Type not supported")
            ErrorMessages.Add(CannotCopyIncompatibleListType, "Cannot copy lists of different types.")
            ErrorMessages.Add(InvalidActivityTypeForList, "Cannot create activities of the specified list type.")
            ErrorMessages.Add(CannotAssociateInactiveItemToCampaign, "Cannot associate an inactive item to a Campaign.")
            ErrorMessages.Add(InvalidFetchXml, "Malformed FetchXml.")
            ErrorMessages.Add(InvalidOperationWhenListLocked, "List is Locked. Cannot perform this action.")
            ErrorMessages.Add(UnsupportedListMemberType, "Unsupported list member type.")
            ErrorMessages.Add(CannotMoveDisabledUser, "Disabled users cannot be moved to a different business unit.")
            ErrorMessages.Add(InvalidPrimaryKey, "Invalid primary key.")
            ErrorMessages.Add(IsvAborted, "ISV code aborted the operation.")
            ErrorMessages.Add(CannotAssignOutlookFilters, "Cannot assign outlook filters")
            ErrorMessages.Add(CannotCreateOutlookFilters, "Cannot create outlook filters")
            ErrorMessages.Add(CannotGrantAccessToOutlookFilters, "Cannot grant access to outlook filters")
            ErrorMessages.Add(CannotModifyAccessToOutlookFilters, "Cannot modify access for outlook filters")
            ErrorMessages.Add(CannotRevokeAccessToOutlookFilters, "Cannot revoke access for outlook filters")
            ErrorMessages.Add(CannotGrantAccessToOfflineFilters, "Cannot grant access to offline filters")
            ErrorMessages.Add(CannotModifyAccessToOfflineFilters, "Cannot modify access for offline filters")
            ErrorMessages.Add(CannotRevokeAccessToOfflineFilters, "Cannot revoke access for offline filters")
            ErrorMessages.Add(DuplicateOutlookAppointment, "The Appointment being promoted from Outlook is already tracked in CRM")
            ErrorMessages.Add(AppointmentScheduleNotSet, "Scheduled End and Scheduled Start must be set for Appointments in order to sync with Outlook.")
            ErrorMessages.Add(PrivilegeCreateIsDisabledForOrganization, "Privilege Create is disabled for organization.")
            ErrorMessages.Add(UnauthorizedAccess, "Attempted to perform an unauthorized operation.")
            ErrorMessages.Add(InvalidCharactersInField, "The field '{0}' contains one or more invalid characters.")
            ErrorMessages.Add(CannotChangeStateOfNonpublicView, "Only public views can be deactivated and activated.")
            ErrorMessages.Add(CannotDeactivateDefaultView, "Default views cannot be deactivated.")
            ErrorMessages.Add(CannotSetInactiveViewAsDefault, "Inactive views cannot be set as default view.")
            ErrorMessages.Add(CannotExceedFilterLimit, "Cannot exceed synchronization filter limit.")
            ErrorMessages.Add(CannotHaveMultipleDefaultFilterTemplates, "Cannot have multiple default synchronization templates for a single entity.")
            ErrorMessages.Add(CrmConstraintParsingError, "Crm constraint parsing error occurred.")
            ErrorMessages.Add(CrmConstraintEvaluationError, "Crm constraint evaluation error occurred.")
            ErrorMessages.Add(CrmExpressionEvaluationError, "Crm expression evaluation error occurred.")
            ErrorMessages.Add(CrmExpressionParametersParsingError, "Crm expression parameters parsing error occurred.")
            ErrorMessages.Add(CrmExpressionBodyParsingError, "Crm expression body parsing error occurred.")
            ErrorMessages.Add(CrmExpressionParsingError, "Crm expression parsing error occurred.")
            ErrorMessages.Add(CrmMalformedExpressionError, "Crm malformed expression error occurred.")
            ErrorMessages.Add(CalloutException, "Callout Exception occurred.")
            ErrorMessages.Add(DateTimeFormatFailed, "Failed to produce a formatted datetime value.")
            ErrorMessages.Add(NumberFormatFailed, "Failed to produce a formatted numeric value.")
            ErrorMessages.Add(InvalidRestore, "RestoreCaller must be called after SwitchToSystemUser.")
            ErrorMessages.Add(InvalidCaller, "Cannot switch ExecutionContext to system user without setting Caller first.")
            ErrorMessages.Add(CrmSecurityError, "A failure occurred in CrmSecurity.")
            ErrorMessages.Add(TransactionAborted, "Transaction Aborted.")
            ErrorMessages.Add(CannotBindToSession, "Cannot bind to another session, session already bound.")
            ErrorMessages.Add(SessionTokenUnavailable, "Session token is not available unless there is a transaction in place.")
            ErrorMessages.Add(TransactionNotCommited, "Transaction not committed.")
            ErrorMessages.Add(TransactionNotStarted, "Transaction not started.")
            ErrorMessages.Add(MultipleChildPicklist, "Crm Internal Exception: Picklists with more than one childAttribute are not supported.")
            ErrorMessages.Add(InvalidSingletonResults, "Crm Internal Exception: Singleton Retrieve Query should not return more than 1 record.")
            ErrorMessages.Add(FailedToLoadAssembly, "Failed to load assembly")
            ErrorMessages.Add(CrmQueryExpressionNotInitialized, "The QueryExpression has not been initialized. Please use the constructor that takes in the entity name to create a correctly initialized instance")
            ErrorMessages.Add(InvalidRegistryKey, "Invalid registry key specified.")
            ErrorMessages.Add(InvalidPriv, "Invalid privilege type.")
            ErrorMessages.Add(MetadataNotFound, "Metadata not found.")
            ErrorMessages.Add(InvalidEntityClassException, "Invalid entity class.")
            ErrorMessages.Add(InvalidXmlEntityNameException, "Invalid Xml entity name.")
            ErrorMessages.Add(InvalidXmlCollectionNameException, "Invalid Xml collection name.")
            ErrorMessages.Add(InvalidRecurrenceRule, "Error in RecurrencePatternFactory.")
            ErrorMessages.Add(CrmImpersonationError, "Error occurred in the Crm AutoReimpersonator.")
            ErrorMessages.Add(ServiceInstantiationFailed, "Instantiation of an Entity failed.")
            ErrorMessages.Add(EntityInstantiationFailed, "Instantiation of an Entity instance Service failed.")
            ErrorMessages.Add(FormTransitionError, "The import has failed because the system cannot transition the entity form {0} from unmanaged to managed. Add at least one full (root) component to the managed solution, and then try to import it again.")
            ErrorMessages.Add(UserTimeConvertException, "Failed to convert user time zone information.")
            ErrorMessages.Add(UserTimeZoneException, "Failed to retrieve user time zone information.")
            ErrorMessages.Add(InvalidConnectionString, "The connection string not found or invalid.")
            ErrorMessages.Add(OpenCrmDBConnection, "Db Connection is Open, when it should be Closed.")
            ErrorMessages.Add(UnpopulatedPrimaryKey, "Primary Key must be populated for calls to platform on rich client in offline mode.")
            ErrorMessages.Add(InvalidVersion, "Unhandled Version mismatch found.")
            ErrorMessages.Add(InvalidOperation, "Invalid Operation performed.")
            ErrorMessages.Add(InvalidMetadata, "Invalid Metadata.")
            ErrorMessages.Add(InvalidDateTime, "The date-time format is invalid, or value is outside the supported range.")
            ErrorMessages.Add(unManagedidscannotdefaultprivateview, "Private views cannot be default.")
            ErrorMessages.Add(DuplicateRecord, "Operation failed due to a SQL integrity violation.")
            ErrorMessages.Add(unManagedidsnorelationship, "No relationship exists between the objects specified.")
            ErrorMessages.Add(MissingQueryType, "The query type is missing.")
            ErrorMessages.Add(InvalidRollupType, "The rollup type is invalid.")
            ErrorMessages.Add(InvalidState, "The object is not in a valid state to perform this operation.")
            ErrorMessages.Add(unManagedidsviewisnotsharable, "The view is not sharable.")
            ErrorMessages.Add(PrincipalPrivilegeDenied, "Target user or team does not hold required privileges.")
            ErrorMessages.Add(CannotUpdateObjectBecauseItIsInactive, "The object cannot be updated because it is inactive.")
            ErrorMessages.Add(CannotDeleteCannedView, "System-defined views cannot be deleted.")
            ErrorMessages.Add(CannotUpdateBecauseItIsReadOnly, "The object cannot be updated because it is read-only.")
            ErrorMessages.Add(CaseAlreadyResolved, "This case has already been resolved. Close and reopen the case record to see the updates.")
            ErrorMessages.Add(InvalidCustomer, "The customer is invalid.")
            ErrorMessages.Add(unManagedidsdataoutofrange, "Data out of range")
            ErrorMessages.Add(unManagedidsownernotenabled, "The specified owner has been disabled.")
            ErrorMessages.Add(BusinessManagementObjectAlreadyExists, "An object with the specified name already exists.")
            ErrorMessages.Add(InvalidOwnerID, "The owner ID is invalid or missing.")
            ErrorMessages.Add(CannotDeleteAsItIsReadOnly, "The object cannot be deleted because it is read-only.")
            ErrorMessages.Add(CannotDeleteDueToAssociation, "The object you tried to delete is associated with another object and cannot be deleted.")
            ErrorMessages.Add(unManagedidsanonymousenabled, "The logged-in user was not found in the Active Directory.")
            ErrorMessages.Add(unManagedidsusernotenabled, "The specified user is either disabled or is not a member of any business unit.")
            ErrorMessages.Add(BusinessNotEnabled, "The specified business unit is disabled.")
            ErrorMessages.Add(CannotAssignToDisabledBusiness, "The specified business unit cannot be assigned to because it is disabled.")
            ErrorMessages.Add(IsvUnExpected, "An unexpected error occurred from ISV code.")
            ErrorMessages.Add(OnlyOwnerCanRevoke, "Only the owner of an object can revoke the owner's access to that object.")
            ErrorMessages.Add(unManagedidsoutofmemory, "Out of memory.")
            ErrorMessages.Add(unManagedidscannotassigntobusiness, "Cannot assign an object to a merchant.")
            ErrorMessages.Add(PrivilegeDenied, "The user does not hold the necessary privileges.")
            ErrorMessages.Add(InvalidObjectTypes, "Invalid object type.")
            ErrorMessages.Add(unManagedidscannotgrantorrevokeaccesstobusiness, "Cannot grant or revoke access rights to a merchant.")
            ErrorMessages.Add(unManagedidsinvaliduseridorbusinessidorusersbusinessinvalid, "One of the following occurred: invalid user id, invalid business id or the user does not belong to the business.")
            ErrorMessages.Add(unManagedidspresentuseridandteamid, "Both the user id and team id are present. Only one should be present.")
            ErrorMessages.Add(MissingUserId, "The user id or the team id is missing.")
            ErrorMessages.Add(MissingBusinessId, "The business id is missing or invalid.")
            ErrorMessages.Add(NotImplemented, "The requested functionality is not yet implemented.")
            ErrorMessages.Add(InvalidPointer, "The object is disposed.")
            ErrorMessages.Add(ObjectDoesNotExist, "The specified object was not found.")
            ErrorMessages.Add(UnExpected, "An unexpected error occurred.")
            ErrorMessages.Add(MissingOwner, "Item does not have an owner.")
            ErrorMessages.Add(CannotShareWithOwner, "An item cannot be shared with the owning user.")
            ErrorMessages.Add(unManagedidsinvalidvisibilitymodificationaccess, "User does not have access to modify the visibility of this item.")
            ErrorMessages.Add(unManagedidsinvalidowninguser, "Item does not have an owning user.")
            ErrorMessages.Add(unManagedidsinvalidassociation, "Invalid association.")
            ErrorMessages.Add(InvalidAssigneeId, "Invalid assignee id.")
            ErrorMessages.Add(unManagedidsfailureinittoken, "Failure in obtaining user token.")
            ErrorMessages.Add(unManagedidsinvalidvisibility, "Invalid visibility.")
            ErrorMessages.Add(InvalidAccessRights, "Invalid access rights.")
            ErrorMessages.Add(InvalidSharee, "Invalid share id.")
            ErrorMessages.Add(unManagedidsinvaliditemid, "Invalid item id.")
            ErrorMessages.Add(unManagedidsinvalidorgid, "Invalid organization id.")
            ErrorMessages.Add(unManagedidsinvalidbusinessid, "Invalid business id.")
            ErrorMessages.Add(unManagedidsinvalidteamid, "Invalid team id.")
            ErrorMessages.Add(unManagedidsinvaliduserid, "The user id is invalid or missing.")
            ErrorMessages.Add(InvalidParentId, "The parent id is invalid or missing.")
            ErrorMessages.Add(InvalidParent, "The parent object is invalid or missing.")
            ErrorMessages.Add(InvalidUserAuth, "User does not have the privilege to act on behalf another user.")
            ErrorMessages.Add(InvalidArgument, "Invalid argument.")
            ErrorMessages.Add(EmptyXml, "Empty XML.")
            ErrorMessages.Add(InvalidXml, "Invalid XML.")
            ErrorMessages.Add(RequiredFieldMissing, "Required field missing.")
            ErrorMessages.Add(SearchTextLenExceeded, "Search Text Length Exceeded.")
            ErrorMessages.Add(CannotAssignOfflineFilters, "Cannot assign offline filters")
            ErrorMessages.Add(ArticleIsPublished, "The article cannot be updated or deleted because it is in published state")
            ErrorMessages.Add(InvalidArticleTemplateState, "The article template state is undefined")
            ErrorMessages.Add(InvalidArticleStateTransition, "This article state transition is invalid because of the current state of the article")
            ErrorMessages.Add(InvalidArticleState, "The article state is undefined")
            ErrorMessages.Add(NullKBArticleTemplateId, "The kbarticletemplateid cannot be NULL")
            ErrorMessages.Add(NullArticleTemplateStructureXml, "The article template structurexml cannot be NULL")
            ErrorMessages.Add(NullArticleTemplateFormatXml, "The article template formatxml cannot be NULL")
            ErrorMessages.Add(NullArticleXml, "The article xml cannot be NULL")
            ErrorMessages.Add(InvalidContractDetailId, "The Contract detail id is invalid")
            ErrorMessages.Add(InvalidTotalPrice, "The total price is invalid")
            ErrorMessages.Add(InvalidTotalDiscount, "The total discount is invalid")
            ErrorMessages.Add(InvalidNetPrice, "The net price is invalid")
            ErrorMessages.Add(InvalidAllotmentsRemaining, "The allotments remaining is invalid")
            ErrorMessages.Add(InvalidAllotmentsUsed, "The allotments used is invalid")
            ErrorMessages.Add(InvalidAllotmentsTotal, "The total allotments is invalid")
            ErrorMessages.Add(InvalidAllotmentsCalc, "Allotments: remaining + used != total")
            ErrorMessages.Add(CannotRouteToSameQueue, "The queue item cannot be routed to the same queue")
            ErrorMessages.Add(CannotAddSingleQueueEnabledEntityToQueue, "The entity record cannot be added to the queue as it already exists in other queue.")
            ErrorMessages.Add(CannotUpdateDeactivatedQueueItem, "This item is deactivated. To work with this item, reactivate it and then try again.")
            ErrorMessages.Add(CannotCreateQueueItemInactiveObject, "Deactivated object cannot be added to queue.")
            ErrorMessages.Add(InsufficientPrivilegeToQueueOwner, "The owner of this queue does not have sufficient privileges to work with the queue.")
            ErrorMessages.Add(NoPrivilegeToWorker, "You cannot add items to an inactive queue. Select another queue and try again.")
            ErrorMessages.Add(CannotAddQueueItemsToInactiveQueue, "The selected user does not have sufficient permissions to work on items in this queue.")
            ErrorMessages.Add(EmailAlreadyExistsInDestinationQueue, "You cannot add this e-mail to the selected queue. A queue item for this e-mail already exists in the queue. You can delete the item from the queue, and then try again.")
            ErrorMessages.Add(CouldNotFindQueueItemInQueue, "Could not find any queue item associated with the Target in the specified SourceQueueId. Either the SourceQueueId or Target is invalid or the queue item does not exist.")
            ErrorMessages.Add(MultipleQueueItemsFound, "This item occurs in more than one queue and cannot be routed from this list. Locate the item in a queue and try to route the item again.")
            ErrorMessages.Add(ActiveQueueItemAlreadyExists, "An active queue item already exists for the given object. Cannot create more than one active queue item for this object.")
            ErrorMessages.Add(CannotRouteInactiveQueueItem, "You can't route a queue item that has been deactivated.")
            ErrorMessages.Add(QueueIdNotPresent, "You must enter the target queue. Provide a valid value in the Queue field and try again.")
            ErrorMessages.Add(QueueItemNotPresent, "You must enter the name of the record that you would like to put in the queue. Provide a valid value in the Queue Item field and try again.")
            ErrorMessages.Add(CannotUpdatePrivateOrWIPQueue, "The private or WIP Bin queue is not allowed to be updated or deleted")
            ErrorMessages.Add(CannotFindUserQueue, "Cannot find user queue")
            ErrorMessages.Add(CannotFindObjectInQueue, "The object was not found in the given queue")
            ErrorMessages.Add(CannotRouteToQueue, "Cannot route to Work in progress queue")
            ErrorMessages.Add(RouteTypeUnsupported, "The route type is unsupported")
            ErrorMessages.Add(UserIdOrQueueNotSet, "Primary User Id or Destination Queue Type code not set")
            ErrorMessages.Add(RoutingNotAllowed, "This object type can not be routed.")
            ErrorMessages.Add(CannotUpdateMetricOnChildGoal, "You cannot update metric on a child goal.")
            ErrorMessages.Add(CannotUpdateGoalPeriodInfoChildGoal, "You cannot update goal period related attributes on a child goal.")
            ErrorMessages.Add(CannotUpdateMetricOnGoalWithChildren, "You cannot update metric on a goal which has associated child goals.")
            ErrorMessages.Add(FiscalPeriodGoalMissingInfo, "For a goal of fiscal period type, the fiscal period attribute must be set.")
            ErrorMessages.Add(CustomPeriodGoalHavingExtraInfo, "For a goal of custom period type, fiscal year and fiscal period attributes must be left blank.")
            ErrorMessages.Add(ParentChildMetricIdDiffers, "The metricid of child goal should be same as the parent goal.")
            ErrorMessages.Add(ParentChildPeriodAttributesDiffer, "The period settings of child goal should be same as the parent goal.")
            ErrorMessages.Add(CustomPeriodGoalMissingInfo, "For a goal of custom period type, goalstartdate and goalenddate attributes must have data.")
            ErrorMessages.Add(GoalMissingPeriodTypeInfo, "Goal Period Type needs to be specified when creating a goal. This field cannot be null.")
            ErrorMessages.Add(ParticipatingQueryEntityMismatch, "The entitytype of participating query should be the same as the entity specified in fetchxml.")
            ErrorMessages.Add(CannotUpdateGoalPeriodInfoClosedGoal, "You cannot change the time period of this goal because there are one or more closed subordinate goals.")
            ErrorMessages.Add(CannotUpdateRollupFields, "You cannot write on rollup fields if isoverride is not set to true in your create/update request.")
            ErrorMessages.Add(CannotDeleteMetricWithGoals, "This goal metric is being used by one or more goals and cannot be deleted.")
            ErrorMessages.Add(CannotUpdateRollupAttributeWithClosedGoals, "The changes made to the roll-up field definition cannot be saved because the related goal metric is being used by one or more closed goals.")
            ErrorMessages.Add(MetricNameAlreadyExists, "A goal metric with the same name already exists. Specify a different name, and try again.")
            ErrorMessages.Add(CannotUpdateMetricWithGoals, "The changes made to this record cannot be saved because this goal metric is being used by one or more goals.")
            ErrorMessages.Add(CannotCreateUpdateSourceAttribute, "Source Attribute Not Valid For Create/Update if Metric Type is Count.")
            ErrorMessages.Add(InvalidDateAttribute, "Date Attribute specified is not an attribute of Source Entity.")
            ErrorMessages.Add(InvalidSourceEntityAttribute, "Attribute {0} is not an attribute of Entity {1}.")
            ErrorMessages.Add(GoalAttributeAlreadyMapped, "The Metric Detail for Specified Goal Attribute already exists.")
            ErrorMessages.Add(InvalidSourceAttributeType, "Source Attribute Type does not match the Amount Data Type specified.")
            ErrorMessages.Add(MaxLimitForRollupAttribute, "Only three metric details per metric can be created.")
            ErrorMessages.Add(InvalidGoalAttribute, "Goal Attribute does not match the specified metric type.")
            ErrorMessages.Add(CannotUpdateParentAndDependents, "Cannot update metric or period attributes when parent is being updated.")
            ErrorMessages.Add(UserDoesNotHaveSendAsAllowed, "User does not have send-as privilege")
            ErrorMessages.Add(CannotUpdateQuoteCurrency, "The currency cannot be changed because this quote has Products associated with it. If you want to change the currency please delete all of the Products and then change the currency or create a new quote with the appropriate currency.")
            ErrorMessages.Add(UserDoesNotHaveSendAsForQueue, "You do not have sufficient privileges to send e-mail as the selected queue. Contact your system administrator for assistance.")
            ErrorMessages.Add(InvalidSourceStateValue, "The source state specified for the entity is invalid.")
            ErrorMessages.Add(InvalidSourceStatusValue, "The source status specified for the entity is invalid.")
            ErrorMessages.Add(InvalidEntityForDateAttribute, "Entity For Date Attribute can be either source entity or its parent.")
            ErrorMessages.Add(InvalidEntityForRollup, "The entity {0} is not a valid entity for rollup.")
            ErrorMessages.Add(InvalidFiscalPeriod, "The fiscal period {0} does not fall in the permitted range of fiscal periods as per organization's fiscal settings.")
            ErrorMessages.Add(unManagedchildentityisnotchild, "The child entity supplied is not a child.")
            ErrorMessages.Add(unManagedmissingparententity, "The parent entity could not be located.")
            ErrorMessages.Add(unManagedunablegetexecutioncontext, "Failed to retrieve execution context (TLS).")
            ErrorMessages.Add(unManagedpendingtrxexists, "A pending transaction already exists.")
            ErrorMessages.Add(unManagedinvalidtrxcountforcommit, "The transaction count was expected to be 1 in order to commit.")
            ErrorMessages.Add(unManagedinvalidtrxcountforrollback, "The transaction count was expected to be 1 in order to rollback.")
            ErrorMessages.Add(unManagedunableswitchusercontext, "Cannot set to a different user context.")
            ErrorMessages.Add(unManagedmissingdataaccess, "The data access could not be retrieved from the ExecutionContext.")
            ErrorMessages.Add(unManagedinvalidcharacterdataforaggregate, "Character data is not valid when clearing an aggregate.")
            ErrorMessages.Add(unManagedtrxinterophandlerset, "The TrxInteropHandler has already been set.")
            ErrorMessages.Add(unManagedinvalidbinaryfield, "The platform cannot handle binary fields.")
            ErrorMessages.Add(unManagedinvaludidispatchfield, "The platform cannot handle idispatch fields.")
            ErrorMessages.Add(unManagedinvaliddbdatefield, "The platform cannot handle dbdate fields.")
            ErrorMessages.Add(unManagedinvalddbtimefield, "The platform cannot handle dbtime fields.")
            ErrorMessages.Add(unManagedinvalidfieldtype, "The platform cannot handle the specified field type.")
            ErrorMessages.Add(unManagedinvalidstreamfield, "The platform cannot handle stream fields.")
            ErrorMessages.Add(unManagedinvalidparametertypeforparameterizedquery, "A parameterized query is not supported for the supplied parameter type.")
            ErrorMessages.Add(unManagedinvaliddynamicparameteraccessor, "SetParam failed processing the DynamicParameterAccessor parameter.")
            ErrorMessages.Add(unManagedunablegetsessiontokennotrx, "Unable to retrieve the session token as there are no pending transactions.")
            ErrorMessages.Add(unManagedunablegetsessiontoken, "Unable to retrieve the session token.")
            ErrorMessages.Add(unManagedinvalidsecurityprincipal, "The security principal is invalid or missing.")
            ErrorMessages.Add(unManagedmissingpreviousownertype, "Unable to determine the previous owner's type.")
            ErrorMessages.Add(unManagedinvalidprivilegeid, "The privilege id is invalid or missing.")
            ErrorMessages.Add(unManagedinvalidprivilegeusergroup, "The privilege user group id is invalid or missing.")
            ErrorMessages.Add(unManagedunexpectedpropertytype, "Unexpected type for the property.")
            ErrorMessages.Add(unManagedmissingaddressentity, "The address entity could not be found.")
            ErrorMessages.Add(unManagederroraddingfiltertoqueryplan, "An error occurred adding a filter to the query plan.")
            ErrorMessages.Add(unManagedmissingreferencesfromrelationship, "Unable to access a relationship in an entity's ReferencesFrom collection.")
            ErrorMessages.Add(unManagedmissingreferencingattribute, "The relationship's ReferencingAttribute is missing or invalid.")
            ErrorMessages.Add(unManagedinvalidoperator, "The operator provided is not valid.")
            ErrorMessages.Add(unManagedunabletoaccessqueryplanfilter, "Unable to access a filter in the query plan.")
            ErrorMessages.Add(unManagedmissingattributefortag, "An expected attribute was not found for the tag specified.")
            ErrorMessages.Add(unManagederrorprocessingfilternodes, "An unexpected error occurred processing the filter nodes.")
            ErrorMessages.Add(unManagedunabletolocateconditionfilter, "Unexpected error locating the filter for the condition.")
            ErrorMessages.Add(unManagedinvalidpagevalue, "The page value is invalid or missing.")
            ErrorMessages.Add(unManagedinvalidcountvalue, "The count value is invalid or missing.")
            ErrorMessages.Add(unManagedinvalidversionvalue, "The version value is invalid or missing.")
            ErrorMessages.Add(unManagedinvalidvaluettagoutsideconditiontag, "A invalid value tag was found outside of it's condition tag.")
            ErrorMessages.Add(unManagedinvalidorganizationid, "The organizationid is missing or invalid.")
            ErrorMessages.Add(unManagedinvalidowninguser, "The owninguser is mising or invalid.")
            ErrorMessages.Add(unManagedinvalidowningbusinessunitorbusinessunitid, "The owningbusinessunit or businessunitid is missing or invalid.")
            ErrorMessages.Add(unManagedinvalidprivilegeedepth, "Invalid privilege depth for user.")
            ErrorMessages.Add(unManagedinvalidlinkobjects, "Invalid link entity, link to attribute, or link from attribute.")
            ErrorMessages.Add(unManagedpartylistattributenotsupported, "Attributes of type partylist are not supported.")
            ErrorMessages.Add(unManagedinvalidargumentsforcondition, "An invalid number of arguments was supplied to a condition.")
            ErrorMessages.Add(unManagedunknownaggregateoperation, "An unknown aggregate operation was supplied.")
            ErrorMessages.Add(unManagedmissingparentattributeonentity, "The parent attribute was not found on the expected entity.")
            ErrorMessages.Add(unManagedinvalidprocesschildofcondition, "ProcessChildOfCondition was called with non-child-of condition.")
            ErrorMessages.Add(unManagedunexpectedrimarykey, "Primary key attribute was not as expected.")
            ErrorMessages.Add(unManagedmissinglinkentity, "Unexpected error locating link entity.")
            ErrorMessages.Add(unManagedinvalidprocessliternalcondition, "ProcessLiteralCondition is only valid for use with Rollup queries.")
            ErrorMessages.Add(unManagedemptyprocessliteralcondition, "No data specified for ProcessLiteralCondition.")
            ErrorMessages.Add(unManagedunusablevariantdata, "Variant supplied contains data in an unusable format.")
            ErrorMessages.Add(unManagedfieldnotvalidatedbyplatform, "A field was not validated by the platform.")
            ErrorMessages.Add(unManagedmissingfilterattribute, "Missing filter attribute.")
            ErrorMessages.Add(unManagedinvalidequalityoperand, "Only QB_LITERAL is supported for equality operand.")
            ErrorMessages.Add(unManagedfilterindexoutofrange, "The filter index is out of range.")
            ErrorMessages.Add(unManagedentityisnotintersect, "The entity is not an intersect entity.")
            ErrorMessages.Add(unManagedcihldofconditionforoffilefilters, "Child-of condition is only allowed on offline filters.")
            ErrorMessages.Add(unManagedinvalidowningbusinessunit, "The owningbusinessunit is missing or invalid.")
            ErrorMessages.Add(unManagedinvalidbusinessunitid, "The businessunitid is missing or invalid.")
            ErrorMessages.Add(unManagedmorethanonesortattribute, "More than one sort attributes were defined.")
            ErrorMessages.Add(unManagedunabletoaccessqueryplan, "Unable to access the query plan.")
            ErrorMessages.Add(unManagedparentattributenotfound, "The parent attribute was not found for the child attribute.")
            ErrorMessages.Add(unManagedinvalidtlsmananger, "Failed to retrieve TLS Manager.")
            ErrorMessages.Add(unManagedinvalidescapedxml, "Escaped xml size not as expected.")
            ErrorMessages.Add(unManagedunabletoretrieveprivileges, "Failed to retrieve privileges.")
            ErrorMessages.Add(unManagedproxycreationfailed, "Cannot create an instance of managed proxy.")
            ErrorMessages.Add(unManagedinvalidprincipal, "The principal id is missing or invalid.")
            ErrorMessages.Add(RestrictInheritedRole, "Inherited roles cannot be modified.")
            ErrorMessages.Add(unManagedidsfetchbetweentext, "between, not-between, in, and not-in operators are not allowed on attributes of type text or ntext.")
            ErrorMessages.Add(unManagedidscantdisable, "The user cannot be disabled because they have workflow rules running under their context.")
            ErrorMessages.Add(CascadeInvalidLinkTypeTransition, "Invalid link type for system entity cascading actions.")
            ErrorMessages.Add(InvalidOrgOwnedCascadeLinkType, "Cascade User-Owned is not a valid cascade link type for org-owned entity relationships.")
            ErrorMessages.Add(CallerCannotChangeOwnDomainName, "The caller cannot change their own domain name")
            ErrorMessages.Add(AsyncOperationInvalidStateChange, "The target state could not be set because the state transition is not valid.")
            ErrorMessages.Add(AsyncOperationInvalidStateChangeUnexpected, "The target state could not be set because the state was changed by another process.")
            ErrorMessages.Add(AsyncOperationMissingId, "The AsyncOperationId is required to do the update.")
            ErrorMessages.Add(AsyncOperationInvalidStateChangeToComplete, "The target state could not be set to complete because the state transition is not valid.")
            ErrorMessages.Add(AsyncOperationInvalidStateChangeToReady, "The target state could not be set to ready because the state transition is not valid.")
            ErrorMessages.Add(AsyncOperationInvalidStateChangeToSuspended, "The target state could not be set to suspended because the state transition is not valid.")
            ErrorMessages.Add(AsyncOperationCannotUpdateNonrecurring, "Cannot update recurrence pattern for a job that is not recurring.")
            ErrorMessages.Add(AsyncOperationCannotUpdateRecurring, "Cannot update recurrence pattern for a job type that is not supported.")
            ErrorMessages.Add(AsyncOperationCannotDeleteUnlessCompleted, "Cannot delete async operation unless it is in Completed state.")
            ErrorMessages.Add(SdkInvalidMessagePropertyName, "Message property name '{0}' is not valid on message {1}.")
            ErrorMessages.Add(PluginAssemblyMustHavePublicKeyToken, "Public assembly must have public key token.")
            ErrorMessages.Add(SdkMessageInvalidImageTypeRegistration, "Message {0} does not support this image type.")
            ErrorMessages.Add(SdkMessageDoesNotSupportPostImageRegistration, "PreEvent step registration does not support Post Image.")
            ErrorMessages.Add(CannotDeserializeRequest, "The SDK request could not be deserialized.")
            ErrorMessages.Add(InvalidPluginRegistrationConfiguration, "The plug-in assembly registration configuration is invalid.")
            ErrorMessages.Add(SandboxClientPluginTimeout, "The plug-in execution failed because the operation has timed-out at the Sandbox Client.")
            ErrorMessages.Add(SandboxHostPluginTimeout, "The plug-in execution failed because the operation has timed-out at the Sandbox Host.")
            ErrorMessages.Add(SandboxWorkerPluginTimeout, "The plug-in execution failed because the operation has timed-out at the Sandbox Worker.")
            ErrorMessages.Add(SandboxSdkListenerStartFailed, "The plug-in execution failed because the Sandbox Client encountered an error during initialization.")
            ErrorMessages.Add(ServiceBusPostFailed, "The service bus post failed.")
            ErrorMessages.Add(ServiceBusIssuerNotFound, "Cannot find service integration issuer information.")
            ErrorMessages.Add(ServiceBusIssuerCertificateError, "Service integration issuer certificate error.")
            ErrorMessages.Add(ServiceBusExtendedTokenFailed, "Failed to retrieve the additional token for service bus post.")
            ErrorMessages.Add(ServiceBusPostPostponed, "Service bus post is being postponed.")
            ErrorMessages.Add(ServiceBusPostDisabled, "Service bus post is disabled for the organization.")
            ErrorMessages.Add(SdkMessageNotSupportedOnServer, "The message requested is not supported on the server.")
            ErrorMessages.Add(SdkMessageNotSupportedOnClient, "The message requested is not supported on the client.")
            ErrorMessages.Add(SdkCorrelationTokenDepthTooHigh, "This workflow job was canceled because the workflow that started it included an infinite loop. Correct the workflow logic and try again. For information about workflow logic, see Help.")
            ErrorMessages.Add(OnlyStepInPredefinedStagesCanBeModified, "Invalid plug-in registration stage. Steps can only be modified in stages BeforeMainOperationOutsideTransaction, BeforeMainOperationInsideTransaction, AfterMainOperationInsideTransaction and AfterMainOperationOutsideTransaction.")
            ErrorMessages.Add(OnlyStepInServerOnlyCanHaveSecureConfiguration, "Only SdkMessageProcessingStep with ServerOnly supported deployment can have secure configuration.")
            ErrorMessages.Add(OnlyStepOutsideTransactionCanCreateCrmService, "Only SdkMessageProcessingStep in parent pipeline and in stages outside transaction can create CrmService to prevent deadlock.")
            ErrorMessages.Add(SdkCustomProcessingStepIsNotAllowed, "Custom SdkMessageProcessingStep is not allowed on the specified message and entity.")
            ErrorMessages.Add(SdkEntityOfflineQueuePlaybackIsNotAllowed, "Entity '{0}' is not allowed in offline queue playback.")
            ErrorMessages.Add(SdkMessageDoesNotSupportImageRegistration, "Message '{0}' does not support image registration.")
            ErrorMessages.Add(RequestLengthTooLarge, "Request message length is too large.")
            ErrorMessages.Add(SandboxWorkerNotAvailable, "The plug-in execution failed because no Sandbox Worker processes are currently available. Please try again.")
            ErrorMessages.Add(SandboxHostNotAvailable, "The plug-in execution failed because no Sandbox Hosts are currently available. Please check that you have a Sandbox server configured and that it is running.")
            ErrorMessages.Add(PluginAssemblyContentSizeExceeded, """The assembly content size '{0} bytes' has exceeded the maximum value allowed for isolated plug-ins '{1} bytes'.""")
            ErrorMessages.Add(UnableToLoadPluginType, "Unable to load plug-in type.")
            ErrorMessages.Add(UnableToLoadPluginAssembly, "Unable to load plug-in assembly.")
            ErrorMessages.Add(InvalidPluginAssemblyContent, "Plug-in assembly does not contain the required types or assembly content cannot be updated.")
            ErrorMessages.Add(InvalidPluginTypeImplementation, "Plug-in type must implement exactly one of the following classes or interfaces: Microsoft.Crm.Sdk.IPlugin, Microsoft.Xrm.Sdk.IPlugin, System.Activities.Activity and System.Workflow.ComponentModel.Activity.")
            ErrorMessages.Add(InvalidPluginAssemblyVersion, "Plug-in assembly fullnames must be unique (ignoring the version build and revision number).")
            ErrorMessages.Add(PluginTypeMustBeUnique, "Multiple plug-in types from the same assembly and with the same typename are not allowed.")
            ErrorMessages.Add(InvalidAssemblySourceType, "The given plugin assembly source type is not supported for isolated plugin assemblies.")
            ErrorMessages.Add(InvalidAssemblyProcessorArchitecture, "The given plugin assembly was built with an unsupported target platform and cannot be loaded.")
            ErrorMessages.Add(CyclicReferencesNotSupported, "The input contains a cyclic reference, which is not supported.")
            ErrorMessages.Add(InvalidQuery, "The query specified for this operation is invalid")
            ErrorMessages.Add(InvalidEmailAddressFormat, "Invalid e-mail address. For more information, contact your system administrator.")
            ErrorMessages.Add(ContractInvalidDiscount, "Discount cannot be greater than total price.")
            ErrorMessages.Add(InvalidLanguageCode, "The specified language code is not valid for this organization.")
            ErrorMessages.Add(ConfigNullPrimaryKey, "Primary Key cannot be nullable.")
            ErrorMessages.Add(ConfigMissingDescription, "Description must be specified.")
            ErrorMessages.Add(AttributeDoesNotSupportLocalizedLabels, "The specified attribute does not support localized labels.")
            ErrorMessages.Add(NoLanguageProvisioned, "There is no language provisioned for this organization.")
            ErrorMessages.Add(CannotImportNullStringsForBaseLanguage, "The base language translation string present in worksheet {0}, row {1}, column {2} is null.")
            ErrorMessages.Add(CannotUpdateNonCustomizableString, "The translation string present in worksheet {0}, row {1}, column {2} is not customizable.")
            ErrorMessages.Add(InvalidOrganizationId, "The organization id present in the translations file does not match with the current organization id.")
            ErrorMessages.Add(InvalidTranslationsFile, "The translations file is invalid or does not confirm to the required schema.")
            ErrorMessages.Add(MetadataRecordNotDeletable, "The metadata record being deleted cannot be deleted by the end user")
            ErrorMessages.Add(InvalidImportJobTemplateFile, "The ImportJobTemplate.xml file is invalid.")
            ErrorMessages.Add(InvalidImportJobId, "The requested importjob does not exist.")
            ErrorMessages.Add(MissingCrmAuthenticationToken, "CrmAuthenticationToken is missing.")
            ErrorMessages.Add(IntegratedAuthenticationIsNotAllowed, "Integrated authentication is not allowed.")
            ErrorMessages.Add(RequestIsNotAuthenticated, "Request is not authenticated.")
            ErrorMessages.Add(AsyncOperationTypeIsNotRecognized, "The operation type of the async operation was not recognized.")
            ErrorMessages.Add(FailedToDeserializeAsyncOperationData, "Failed to deserialize async operation data.")
            ErrorMessages.Add(UserSettingsOverMaxPagingLimit, "Paging limit over maximum configured value.")
            ErrorMessages.Add(AsyncNetworkError, "An error occurred while accessing the network.")
            ErrorMessages.Add(AsyncCommunicationError, "A communication error occurred while processing the async operation.")
            ErrorMessages.Add(MissingCrmAuthenticationTokenOrganizationName, "Organization Name must be specified in CrmAuthenticationToken.")
            ErrorMessages.Add(SdkNotEnoughPrivilegeToSetCallerOriginToken, "Caller does not have enough privilege to set CallerOriginToken to the specified value.")
            ErrorMessages.Add(OverRetrievalUpperLimitWithoutPagingCookie, "Over upper limit of records that can be requested without a paging cookie. A paging cookie is required when retrieving a high page number.")
            ErrorMessages.Add(InvalidAllotmentsOverage, "Allotment overage is invalid.")
            ErrorMessages.Add(TooManyConditionsInQuery, "Number of conditions in query exceeded maximum limit.")
            ErrorMessages.Add(TooManyLinkEntitiesInQuery, "Number of link entities in query exceeded maximum limit.")
            ErrorMessages.Add(TooManyConditionParametersInQuery, "Number of parameters in a condition exceeded maximum limit.")
            ErrorMessages.Add(InvalidOneToManyRelationshipForRelatedEntitiesQuery, "An invalid OneToManyRelationship has been specified for RelatedEntitiesQuery. Referenced Entity {0} should be the same as primary entity {1}")
            ErrorMessages.Add(PicklistValueNotUnique, "The picklist value already exists.  Picklist values must be unique.")
            ErrorMessages.Add(UnableToLogOnUserFromUserNameAndPassword, "The specified user name and password can not logon.")
            ErrorMessages.Add(PicklistValueOutOfRange, "The picklist value is out of the range.")
            ErrorMessages.Add(WrongNumberOfBooleanOptions, "Boolean attributes must have exactly two option values.")
            ErrorMessages.Add(BooleanOptionOutOfRange, "Boolean attribute options must have a value of either 0 or 1.")
            ErrorMessages.Add(CannotAddNewBooleanValue, "You cannot add an option to a Boolean attribute.")
            ErrorMessages.Add(CannotAddNewStateValue, "You cannot add state options to a State attribute.")
            ErrorMessages.Add(NoMoreCustomOptionValuesExist, "All available custom option values have been used.")
            ErrorMessages.Add(InsertOptionValueInvalidType, "You can add option values only to picklist and status attributes.")
            ErrorMessages.Add(NewStatusRequiresAssociatedState, "The new status option must have an associated state value.")
            ErrorMessages.Add(NewStatusHasInvalidState, "The state value that was provided for this new status option does not exist.")
            ErrorMessages.Add(CannotDeleteEnumOptionsFromAttributeType, "You can delete options only from picklist and status attributes.")
            ErrorMessages.Add(OptionReorderArrayIncorrectLength, "The array of option values that were provided for reordering does not match the number of options for the attribute.")
            ErrorMessages.Add(ValueMissingInOptionOrderArray, "The options array is missing a value.")
            ErrorMessages.Add(NavPaneOrderValueNotAllowed, "The provided nav pane order value is not allowed")
            ErrorMessages.Add(EntityRelationshipRoleCustomLabelsMissing, "Custom labels must be provided if an entity relationship role has a display option of UseCustomLabels")
            ErrorMessages.Add(NavPaneNotCustomizable, "The nav pane properties for this relationship are not customizable")
            ErrorMessages.Add(EntityRelationshipSchemaNameRequired, "Entity relationships require a name")
            ErrorMessages.Add(EntityRelationshipSchemaNameNotUnique, "A relationship with the specified name already exists. Please specify a unique name.")
            ErrorMessages.Add(CustomReflexiveRelationshipNotAllowedForEntity, "This entity is not valid for a custom reflexive relationship")
            ErrorMessages.Add(EntityCannotBeChildInCustomRelationship, "This entity is either not valid as a child in a custom parental relationship or is already a child in a parental relationship")
            ErrorMessages.Add(ReferencedEntityHasLogicalPrimaryNameField, "This entity has a primary field that is logical and therefore cannot be the referenced entity in a one-to-many relationship")
            ErrorMessages.Add(IntegerValueOutOfRange, "A validation error occurred. An integer provided is outside of the allowed values for this attribute.")
            ErrorMessages.Add(DecimalValueOutOfRange, "A validation error occurred. A decimal value provided is outside of the allowed values for this attribute.")
            ErrorMessages.Add(StringLengthTooLong, "A validation error occurred. A string value provided is too long.")
            ErrorMessages.Add(EntityCannotParticipateInEntityAssociation, "This entity cannot participate in an entity association")
            ErrorMessages.Add(DataMigrationManagerUnknownProblem, "The Data Migration Manager encountered an unknown problem and cannot continue. To try again, restart the Data Migration Manager.")
            ErrorMessages.Add(ImportOperationChildFailure, "One or more of the Import Child Jobs Failed")
            ErrorMessages.Add(AttributeDeprecated, """Attribute '{0}' on entity '{1}' is deprecated.""")
            ErrorMessages.Add(DataMigrationManagerMandatoryUpdatesNotInstalled, "First-time configuration of the Data Migration Manager has been canceled. You will not be able to use the Data Migration Manager until configuration is completed.")
            ErrorMessages.Add(ReferencedEntityMustHaveLookupView, "Referenced entities of a relationship must have a lookup view")
            ErrorMessages.Add(ReferencingEntityMustHaveAssociationView, "Referencing entities of a relationship must have an association view")
            ErrorMessages.Add(CouldNotObtainLockOnResource, "Database resource lock could not be obtained")
            ErrorMessages.Add(SourceAttributeHeaderTooBig, "Column headers must be 160 or fewer characters. Fix the column headers, and then run Data Migration Manager again.")
            ErrorMessages.Add(CannotDeleteDefaultStatusOption, "Default Status options cannot be deleted.")
            ErrorMessages.Add(CannotFindDomainAccount, "Invalid domain account")
            ErrorMessages.Add(CannotUpdateAppDefaultValueForStateAttribute, "The default value for a status (statecode) attribute cannot be updated.")
            ErrorMessages.Add(CannotUpdateAppDefaultValueForStatusAttribute, "The default value for a status reason (statuscode) attribute is not used. The default status reason is set in the associated status (statecode) attribute option.")
            ErrorMessages.Add(InvalidOptionSetSchemaName, "An OptionSet with the specified name already exists. Please specify a unique name.")
            ErrorMessages.Add(ReferencingEntityCannotBeSolutionAware, "Referencing entities in a relationship cannot be a component in a solution.")
            ErrorMessages.Add(ErrorInFieldWidthIncrease, "An error occurred while increasing the field width.")
            ErrorMessages.Add(ExpiredVersionStamp, "Version stamp associated with the client has expired. Please perform a full sync.")
            ErrorMessages.Add(AsyncOperationCannotCancel, "This system job cannot be canceled.")
            ErrorMessages.Add(AsyncOperationCannotPause, "This system job cannot be paused.")
            ErrorMessages.Add(WorkflowCompileFailure, "An error has occurred during compilation of the workflow.")
            ErrorMessages.Add(UpdatePublishedWorkflowDefinition, "Cannot update a published workflow definition.")
            ErrorMessages.Add(UpdateWorkflowActivation, "Cannot update a workflow activation.")
            ErrorMessages.Add(DeleteWorkflowActivation, "Cannot delete a workflow activation.")
            ErrorMessages.Add(DeleteWorkflowActivationWorkflowDependency, "Cannot delete a workflow dependency associated with a workflow activation.")
            ErrorMessages.Add(DeletePublishedWorkflowDefinitionWorkflowDependency, "Cannot delete a workflow dependency for a published workflow definition.")
            ErrorMessages.Add(UpdateWorkflowActivationWorkflowDependency, "Cannot update a workflow dependency associated with a workflow activation.")
            ErrorMessages.Add(UpdatePublishedWorkflowDefinitionWorkflowDependency, "Cannot update a workflow dependency for a published workflow definition.")
            ErrorMessages.Add(CreateWorkflowActivationWorkflowDependency, "Cannot create a workflow dependency associated with a workflow activation.")
            ErrorMessages.Add(CreatePublishedWorkflowDefinitionWorkflowDependency, "Cannot create a workflow dependency for a published workflow definition.")
            ErrorMessages.Add(WorkflowPublishedByNonOwner, "The workflow cannot be published or unpublished by someone who is not its owner.")
            ErrorMessages.Add(PublishedWorkflowOwnershipChange, "A published workflow can only be assigned to the caller.")
            ErrorMessages.Add(OnlyWorkflowDefinitionOrTemplateCanBePublished, "Only workflow definition or draft workflow template can be published.")
            ErrorMessages.Add(OnlyWorkflowDefinitionOrTemplateCanBeUnpublished, "Only workflow definition or workflow template can be unpublished.")
            ErrorMessages.Add(DeleteWorkflowActiveDefinition, "Cannot delete an active workflow definition.")
            ErrorMessages.Add(WorkflowConditionIncorrectUnaryOperatorFormation, "Incorrect formation of unary operator.")
            ErrorMessages.Add(WorkflowConditionIncorrectBinaryOperatorFormation, "Incorrect formation of binary operator.")
            ErrorMessages.Add(WorkflowConditionOperatorNotSupported, "Condition operator not supported for specified type.")
            ErrorMessages.Add(WorkflowConditionTypeNotSupport, "Invalid type specified on condition.")
            ErrorMessages.Add(WorkflowValidationFailure, "Validation failed on the specified workflow.")
            ErrorMessages.Add(PublishedWorkflowLimitForSkuReached, "This workflow cannot be published because your organization has reached its limit for the number of workflows that can be published at the same time. (There is no limit on the number of draft workflows.) You can publish this workflow by unpublishing a different workflow, or by upgrading your license to a license that supports more workflows.")
            ErrorMessages.Add(NoPrivilegeToPublishWorkflow, "User does not have sufficient privileges to publish workflows.")
            ErrorMessages.Add(WorkflowSystemPaused, "Workflow should be paused by system.")
            ErrorMessages.Add(WorkflowPublishNoActivationParameters, "Automatic workflow cannot be published if no activation parameters have been specified.")
            ErrorMessages.Add(CreateWorkflowDependencyForPublishedTemplate, "Cannot create a workflow dependency for a published workflow template.")
            ErrorMessages.Add(DeleteActiveWorkflowTemplateDependency, "Cannot delete workflow dependency from a published workflow template .")
            ErrorMessages.Add(UpdatePublishedWorkflowTemplate, "Cannot update a published workflow template.")
            ErrorMessages.Add(DeleteWorkflowActiveTemplate, "Cannot delete an active workflow template.")
            ErrorMessages.Add(CustomActivityInvalid, "Invalid custom activity.")
            ErrorMessages.Add(PrimaryEntityInvalid, "Invalid primary entity.")
            ErrorMessages.Add(CannotDeserializeWorkflowInstance, "Workflow instance cannot be deserialized. A possible reason for this failure is a workflow referencing a custom activity that has been unregistered.")
            ErrorMessages.Add(CannotDeserializeXamlWorkflow, "Xaml representing workflow cannot be deserialized into a DynamicActivity.")
            ErrorMessages.Add(CannotDeleteCustomEntityUsedInWorkflow, "Cannot delete entity because it is used in a workflow.")
            ErrorMessages.Add(BulkMailOperationFailed, "The bulk e-mail job completed with {0} failures. The failures might be caused by missing e-mail addresses or because you do not have permission to send e-mail. To find records with missing e-mail addresses, use Advanced Find. If you need increased e-mail permissions, contact your system administrator.")
            ErrorMessages.Add(WorkflowExpressionOperatorNotSupported, "Expression operator not supported for specified type.")
            ErrorMessages.Add(ChildWorkflowNotFound, "This workflow cannot run because one or more child workflows it uses have not been published or have been deleted. Please check the child workflows and try running this workflow again.")
            ErrorMessages.Add(CannotDeleteAttributeUsedInWorkflow, "This attribute cannot be deleted because it is used in one or more workflows. Cancel any system jobs for workflows that use this attribute, then delete or modify any workflows that use the attribute, and then try to delete the attribute again.")
            ErrorMessages.Add(CannotLocateRecordForWorkflowActivity, "A record required by this workflow job could not be found.")
            ErrorMessages.Add(PublishWorkflowWhileActingOnBehalfOfAnotherUserError, "Publishing Workflows while acting on behalf of another user is not allowed.")
            ErrorMessages.Add(CannotDisableInternetMarketingUser, "You cannot disable the Internet Marketing Partner user. This user does not consume a user license and is not charged to your organization.")
            ErrorMessages.Add(CannotSetWindowsLiveIdForInternetMarketingUser, "You cannot change the Windows Live ID for the Internet Marketing Partner user. This user does not consume a user license and is not charged to your organization.")
            ErrorMessages.Add(CannotChangeAccessModeForInternetMarketingUser, "Internet Marketing User is a system user. You cannot change its access mode.")
            ErrorMessages.Add(CannotChangeInvitationStatusForInternetMarketingUser, "Internet Marketing User is a system user. You cannot change its invitation status.")
            ErrorMessages.Add(UIDataGenerationFailed, "There was an error generating the UIData from XAML.")
            ErrorMessages.Add(WorkflowReferencesInvalidActivity, "The workflow definition contains a step that references and invalid custom activity. Remove the invalid references and try again.")
            ErrorMessages.Add(PublishWorkflowWhileImpersonatingError, "Publishing Workflows while impersonating another user is not allowed.")
            ErrorMessages.Add(ExchangeAutodiscoverError, "Autodiscover could not find the Exchange Web Services URL for the specified mailbox. Verify that the mailbox address and the credentials provided are correct and that Autodiscover is enabled and has been configured correctly.")
            ErrorMessages.Add(NonCrmUIWorkflowsNotSupported, "This workflow cannot be created, updated or published because it was created outside the Microsoft Dynamics CRM Web application. Your organization does not allow this type of workflow.")
            ErrorMessages.Add(NotEnoughPrivilegesForXamlWorkflows, "Not enough privileges to complete the operation. Only the deployment administrator can create or update workflows that are created outside the Microsoft Dynamics CRM Web application.")
            ErrorMessages.Add(WorkflowAutomaticallyDeactivated, "The original workflow definition has been deactivated and replaced.")
            ErrorMessages.Add(StepAutomaticallyDisabled, "The original sdkmessageprocessingstep has been disabled and replaced.")
            ErrorMessages.Add(NonCrmUIInteractiveWorkflowNotSupported, "This interactive workflow cannot be created, updated or published because it was created outside the Microsoft Dynamics CRM Web application.")
            ErrorMessages.Add(WorkflowActivityNotSupported, "This workflow cannot be created, updated or published because it's referring unsupported workflow step.")
            ErrorMessages.Add(ExecuteNotOnDemandWorkflow, "Workflow must be marked as on-demand or child workflow.")
            ErrorMessages.Add(ExecuteUnpublishedWorkflow, "Workflow must be in Published state.")
            ErrorMessages.Add(ChildWorkflowParameterMismatch, "This workflow cannot run because arguments provided by parent workflow does not match with the specified parameters in linked child workflow. Check the child workflow reference in parent workflow and try running this workflow again.")
            ErrorMessages.Add(InvalidProcessStateData, "ProcessState is not valid for given ProcessSession instance.")
            ErrorMessages.Add(OutOfScopeSlug, "The data required to display the next dialog page cannot be found. To resolve this issue, contact the dialog owner or the system administrator.")
            ErrorMessages.Add(CustomWorkflowActivitiesNotSupported, "Custom Workflow Activities are not enabled.")
            ErrorMessages.Add(CrmSqlGovernorDatabaseRequestDenied, "The server is busy and the request was not completed. Try again later.")
            ErrorMessages.Add(InvalidAuthTicket, "The ticket specified for authentication didn't pass validation")
            ErrorMessages.Add(ExpiredAuthTicket, "The ticket specified for authentication is expired")
            ErrorMessages.Add(BadAuthTicket, "The ticket specified for authentication is invalid")
            ErrorMessages.Add(InsufficientAuthTicket, "The ticket specified for authentication didn't meet policy")
            ErrorMessages.Add(OrganizationDisabled, "The CRM organization you are attempting to access is currently disabled.  Please contact your system administrator")
            ErrorMessages.Add(TamperedAuthTicket, "The ticket specified for authentication has been tampered with or invalidated.")
            ErrorMessages.Add(ExpiredKey, "The key specified to compute a hash value is expired, only active keys are valid.  Expired Key : {0}.")
            ErrorMessages.Add(ScaleGroupDisabled, "The specified scalegroup is disabled. Access to organizations in this scalegroup are not allowed.")
            ErrorMessages.Add(SupportLogOnExpired, "Support login is expired")
            ErrorMessages.Add(InvalidPartnerSolutionCustomizationProvider, "Invalid partner solution customization provider type")
            ErrorMessages.Add(MultiplePartnerSecurityRoleWithSameInformation, "More than one security role found for partner user")
            ErrorMessages.Add(MultiplePartnerUserWithSameInformation, "More than one partner user found with same information")
            ErrorMessages.Add(MultipleRootBusinessUnit, "More than one root business unit found")
            ErrorMessages.Add(CannotDeletePartnerWithPartnerSolutions, "Can not delete partner as one or more solutions are associated with it")
            ErrorMessages.Add(CannotDeletePartnerSolutionWithOrganizations, "Can not delete partner solution as one or more organizations are associated with it")
            ErrorMessages.Add(CannotProvisionPartnerSolution, "Can not provision partner solution as it is either already provisioned or going through provisioning.")
            ErrorMessages.Add(CannotActOnBehalfOfAnotherUser, "User does not have the privilege to act on behalf another user.")
            ErrorMessages.Add(SystemUserDisabled, "The system user was disabled therefore the ticket expired.")
            ErrorMessages.Add(PluginDoesNotImplementCorrectInterface, "The plug-in specified does not implement the required interface Microsoft.Xrm.Sdk.IPlugin or Microsoft.Crm.Sdk.IPlugin.")
            ErrorMessages.Add(CannotCreatePluginInstance, "Can not create instance of a plug-in. Verify that plug-in type is not defined as abstract and it has a public constructor supported by CRM SDK.")
            ErrorMessages.Add(CrmLiveGenericError, "An error has occurred while processing your request.")
            ErrorMessages.Add(CrmLiveOrganizationProvisioningFailed, "An error has occurred when provisioning the organization.")
            ErrorMessages.Add(CrmLiveMissingActiveDirectoryGroup, "The specified Active Directory Group does not exist.")
            ErrorMessages.Add(CrmLiveInternalProvisioningError, "An unexpected error happened in the provisioning system.")
            ErrorMessages.Add(CrmLiveQueueItemDoesNotExist, "The specified queue item does not exist in the queue. It may have been deleted or its ID may not have been specified correctly")
            ErrorMessages.Add(CrmLiveInvalidSetupParameter, "The parameter to CRM Online Setup is incorrect or not specified.")
            ErrorMessages.Add(CrmLiveMultipleWitnessServersInScaleGroup, "The ScaleGroup has multiple witness servers specified. There should be only 1 witness server in a scale group.")
            ErrorMessages.Add(CrmLiveMissingServerRolesInScaleGroup, "The scalegroup is missing some required server roles. 1 Witness Server and 2 Sql Servers are required for Provisioning.")
            ErrorMessages.Add(CrmLiveServerCannotHaveWitnessAndDataServerRoles, "A server cannot have both Witness and Data Server Roles.")
            ErrorMessages.Add(IsNotLiveToSendInvitation, "This functionality is not supported, its only available for Online solution.")
            ErrorMessages.Add(MissingOrganizationFriendlyName, "Cannot install CRM without an organization friendly name.")
            ErrorMessages.Add(MissingOrganizationUniqueName, "Cannot install CRM without an organization unique name.")
            ErrorMessages.Add(OfferingCategoryAndTokenNull, "Offer category and Billing Token are both missing, but at least one is required.")
            ErrorMessages.Add(OfferingIdNotSupported, "This version does not support search for offering id.")
            ErrorMessages.Add(OrganizationTakenByYou, "The organization {0} is already purchased by you.")
            ErrorMessages.Add(OrganizationTakenBySomeoneElse, "The organization {0} is already purchased by another customer.")
            ErrorMessages.Add(InvalidTemplate, "The Invitation Email template is not valid")
            ErrorMessages.Add(InvalidUserQuota, "You have reached the maximum number of user quota")
            ErrorMessages.Add(InvalidRole, "You have not assigned roles to this user")
            ErrorMessages.Add(ErrorGeneratingInvitation, "Some Internal error occurred in generating invitation token, Please try again later")
            ErrorMessages.Add(CrmLiveOrganizationUpgradeFailed, "Upgrade Of Crm Organization Failed.")
            ErrorMessages.Add(UnableToSendEmail, "Some Internal error occurred in sending invitation, Please try again later")
            ErrorMessages.Add(InvalidEmail, "Email generated from the template is not valid")
            ErrorMessages.Add(VersionMismatch, "Unsupported version - This is {0} version {1}, but version {2} was requested.")
            ErrorMessages.Add(MissingParameterToMethod, "Missing parameter {0} to method {1}")
            ErrorMessages.Add(InvalidValueForCountryCode, "Account Country/Region code must not be {0}")
            ErrorMessages.Add(InvalidValueForCurrency, "Account currency code must not be {0}")
            ErrorMessages.Add(InvalidValueForLocale, "Account locale code must not be {0}")
            ErrorMessages.Add(CrmLiveSupportOrganizationExistsInScaleGroup, "Only one support organization is allowed in a scalegroup.")
            ErrorMessages.Add(CrmLiveMonitoringOrganizationExistsInScaleGroup, "Only one monitoring organization is allowed in a scalegroup.")
            ErrorMessages.Add(InvalidUserLicenseCount, "Cannot purchase {0} user licenses for the Offering {1}.")
            ErrorMessages.Add(MissingColumn, "The property bag is missing an entry for {0}.")
            ErrorMessages.Add(InvalidResourceType, "The requested action is not valid for resource type {0}.")
            ErrorMessages.Add(InvalidMinimumResourceLimit, "The resource type {0} cannot have a minimum limit of {1}.")
            ErrorMessages.Add(InvalidMaximumResourceLimit, "The resource type {0} cannot have a maximum limit of {1}.")
            ErrorMessages.Add(ConflictingProvisionTypes, "The service component {0} has conflicting provision types.")
            ErrorMessages.Add(InvalidAmountProvided, "The service component {0} cannot have a provide {1} of resource type {2}.")
            ErrorMessages.Add(CrmLiveOrganizationDeleteFailed, "An error has occurred when deleting the organization.")
            ErrorMessages.Add(OnlyDisabledOrganizationCanBeDeleted, "Can not delete enabled organization. Organization must be disabled before it can be deleted.")
            ErrorMessages.Add(CrmLiveOrganizationDetailsNotFound, "Unable to find organization details.")
            ErrorMessages.Add(CrmLiveOrganizationFriendlyNameTooShort, "The organization name provided is too short.")
            ErrorMessages.Add(CrmLiveOrganizationFriendlyNameTooLong, "The organization name provided is too long.")
            ErrorMessages.Add(CrmLiveOrganizationUniqueNameTooShort, "The unique name provided is too short.")
            ErrorMessages.Add(CrmLiveOrganizationUniqueNameTooLong, "The unique name provided is too long.")
            ErrorMessages.Add(CrmLiveOrganizationUniqueNameInvalid, "The unique name provided is not valid.")
            ErrorMessages.Add(CrmLiveOrganizationUniqueNameReserved, "The unique name is already reserved.")
            ErrorMessages.Add(ValueParsingError, "Error parsing parameter {0} of type {1} with value {2}")
            ErrorMessages.Add(InvalidGranularityValue, "The Granularity column value is incorrect. Each rule part must be a name-value pair separated by an equal sign (=). For example: FREQ=Minutes;INTERVAL=15")
            ErrorMessages.Add(CrmLiveInvalidQueueItemSchedule, "The QueueItem has an invalid schedule of start time {0} and end time {1}.")
            ErrorMessages.Add(CrmLiveQueueItemTimeInPast, "A QueueItem cannot be scheduled to start or end in the past.")
            ErrorMessages.Add(CrmLiveUnknownSku, "This Sku specified is not valid.")
            ErrorMessages.Add(ExceedCustomEntityQuota, "The custom entity limit has been reached.")
            ErrorMessages.Add(ImportWillExceedCustomEntityQuota, "This import process is trying to import {0} new custom entities. This would exceed the custom entity limits for this organization.")
            ErrorMessages.Add(OrganizationMigrationUnderway, "Organization migration is already underway.")
            ErrorMessages.Add(CrmLiveInvoicingAccountIdMissing, "Invoicing Account Number (SAP Id) cannot be empty for an invoicing sku.")
            ErrorMessages.Add(CrmLiveDuplicateWindowsLiveId, "A user with this username already exists.")
            ErrorMessages.Add(CrmLiveDnsDomainNotFound, "Domain was not found in the DNS table.")
            ErrorMessages.Add(CrmLiveDnsDomainAlreadyExists, "Domain already exists in the DNS table.")
            ErrorMessages.Add(InvalidInteractiveUserQuota, "You have reached the maximum number of interactive/full users.")
            ErrorMessages.Add(InvalidNonInteractiveUserQuota, "You have reached the maximum number of non-interactive users/")
            ErrorMessages.Add(CrmLiveCannotFindExternalMessageProvider, "External Message Provider could not be located for queue item type of: {0}.")
            ErrorMessages.Add(CrmLiveInvalidExternalMessageData, "External Message Data has some invalid data.  Data: {0} External Message: {1}")
            ErrorMessages.Add(CrmLiveOrganizationEnableFailed, "Enabling Organization Failed.")
            ErrorMessages.Add(CrmLiveOrganizationDisableFailed, "Disabling Organization Failed.")
            ErrorMessages.Add(CrmLiveAddOnUnexpectedError, "There was an error contacting the billing system.  Your request cannot be processed at this time.  No changes have been made to your account.  Close this wizard, and try again later.  If the problem persists, please contact our sales organization at 1-877-CRM-CHOICE (276-2464).")
            ErrorMessages.Add(CrmLiveAddOnAddLicenseLimitReached, "Your subscription has the maximum number of user licenses available.  For additional licenses, please contact our sales organization at 1-877-CRM-CHOICE (276-2464).")
            ErrorMessages.Add(CrmLiveAddOnAddStorageLimitReached, "Your subscription has the maximum amount of storage available.  For additional storage, please contact our sales organization at 1-877-CRM-CHOICE (276-2464).")
            ErrorMessages.Add(CrmLiveAddOnRemoveStorageLimitReached, "Your organization has the minimum amount of storage allowed.  You can remove only storage that has been added to your organization, and  is not being used.")
            ErrorMessages.Add(CrmLiveAddOnOrgInNoUpdateMode, "Your changes cannot be processed at this time. Your organization is currently being updated.  No changes have been made to your account.  Close this wizard, and try again later.  If the problem persists, please contact our sales organization at 1-877-CRM-CHOICE (276-2464).")
            ErrorMessages.Add(CrmLiveUnknownCategory, "This Category specified is not valid.")
            ErrorMessages.Add(CrmLiveInvalidInvoicingAccountNumber, "This Invoicing Account Number is not valid because it contains an invalid character.")
            ErrorMessages.Add(CrmLiveAddOnDataChanged, "Due to recent changes you have made to your account, these changes cannot be made at this time.   Close this wizard, and try again later.  If the problem persists, please contact our sales organization at 1-877-CRM-CHOICE (276-2464).")
            ErrorMessages.Add(CrmLiveInvalidEmail, "Invalid email address entered.")
            ErrorMessages.Add(CrmLiveInvalidPhone, "Invalid phone number entered.")
            ErrorMessages.Add(CrmLiveInvalidZipCode, "Invalid zip code entered.")
            ErrorMessages.Add(InvalidAmountFreeResourceLimit, "The resource type {0} cannot have an amount free value of {1}.")
            ErrorMessages.Add(InvalidToken, "The token is invalid.")
            ErrorMessages.Add(CrmLiveRegisterCustomCodeDisabled, "Registration of custom code feature for this organization is disabled.")
            ErrorMessages.Add(CrmLiveExecuteCustomCodeDisabled, "Execution of custom code feature for this organization is disabled.")
            ErrorMessages.Add(CrmLiveInvalidTaxId, "Invalid TaxId entered.")
            ErrorMessages.Add(DatacenterNotAvailable, "This datacenter endpoint is not currently available for this organization.")
            ErrorMessages.Add(ErrorConnectingToDiscoveryService, "Error when trying to connect to customer's discovery service.")
            ErrorMessages.Add(OrgDoesNotExistInDiscoveryService, "Organization not found in customer's discovery service")
            ErrorMessages.Add(ErrorConnectingToOrganizationService, "Error when trying to connect to customer's organization service.")
            ErrorMessages.Add(UserIsNotSystemAdminInOrganization, "Current user is not a system admin in customer's organization")
            ErrorMessages.Add(MobileServiceError, "Error communicating with mobile service")
            ErrorMessages.Add(LivePlatformGeneralEmailError, "An Email Error Occurred")
            ErrorMessages.Add(LivePlatformEmailInvalidTo, "The ""To"" parameter is blank or null")
            ErrorMessages.Add(LivePlatformEmailInvalidFrom, "The ""From"" parameter is blank or null")
            ErrorMessages.Add(LivePlatformEmailInvalidSubject, "The ""Subject"" parameter is blank or null")
            ErrorMessages.Add(LivePlatformEmailInvalidBody, "The ""Body"" parameter is blank or null")
            ErrorMessages.Add(BillingPartnerCertificate, "Could not determine the right Partner certificate to use with Billing.  Issuer: {0}  Subject: {1}  Distinguished matches: [{2}]  Name matches: [{3}]  All valid certificates: [{4}].")
            ErrorMessages.Add(BillingNoSettingError, "No Billing application configuration setting [{0}] was found.")
            ErrorMessages.Add(BillingTestConnectionError, "Billing is not available: Call to IsServiceAvailable returned 'False'.")
            ErrorMessages.Add(BillingTestConnectionException, "Billing TestConnection exception.")
            ErrorMessages.Add(BillingUserPuidNullError, "User Puid is required, but is null.")
            ErrorMessages.Add(BillingUnmappedErrorCode, "Billing error code [{0}] was thrown with exception {1}")
            ErrorMessages.Add(BillingUnknownErrorCode, "Billing error code [{0}] was thrown with exception {1}")
            ErrorMessages.Add(BillingUnknownException, "Billing error was thrown with exception {0}")
            ErrorMessages.Add(BillingRetrieveKeyError, "Could not retrieve Billing session key: ""{0}""")
            ErrorMessages.Add(BDK_E_ADDRESS_VALIDATION_FAILURE, "{0}  ")
            ErrorMessages.Add(BDK_E_AGREEMENT_ALREADY_SIGNED, "{0}  ")
            ErrorMessages.Add(BDK_E_AUTHORIZATION_FAILED, "{0}  ")
            ErrorMessages.Add(BDK_E_AVS_FAILED, "{0}  ")
            ErrorMessages.Add(BDK_E_BAD_CITYNAME_LENGTH, "{0}  ")
            ErrorMessages.Add(BDK_E_BAD_STATECODE_LENGTH, "{0}  ")
            ErrorMessages.Add(BDK_E_BAD_ZIPCODE_LENGTH, "{0}  ")
            ErrorMessages.Add(BDK_E_BADXML, "{0}  ")
            ErrorMessages.Add(BDK_E_BANNED_PAYMENT_INSTRUMENT, "{0}  ")
            ErrorMessages.Add(BDK_E_BANNEDPERSON, "{0}  ")
            ErrorMessages.Add(BDK_E_CANNOT_EXCEED_MAX_OWNERSHIP, "{0}  ")
            ErrorMessages.Add(BDK_E_COUNTRY_CURRENCY_PI_MISMATCH, "{0}  ")
            ErrorMessages.Add(BDK_E_CREDIT_CARD_EXPIRED, "{0}  ")
            ErrorMessages.Add(BDK_E_DATE_EXPIRED, "{0}  ")
            ErrorMessages.Add(BDK_E_ERROR_COUNTRYCODE_MISMATCH, "{0}  ")
            ErrorMessages.Add(BDK_E_ERROR_COUNTRYCODE_REQUIRED, "{0}  ")
            ErrorMessages.Add(BDK_E_EXTRA_REFERRAL_DATA, "{0}  ")
            ErrorMessages.Add(BDK_E_GUID_EXISTS, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_ADDRESS_ID, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_BILLABLE_ACCOUNT_ID, "{0}  The specified Billing account is invalid.  Or, although the objectID is of the correct type, the account it identifies does not exist in the system.")
            ErrorMessages.Add(BDK_E_INVALID_BUF_SIZE, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_CATEGORY_NAME, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_COUNTRY_CODE, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_CURRENCY, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_CUSTOMER_TYPE, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_DATE, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_EMAIL_ADDRESS, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_FILTER, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_GUID, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_INPUT_TO_TAXWARE_OR_VERAZIP, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_LOCALE, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_OBJECT_ID, "{0}  The Billing system cannot find the object (e.g. account or subscription or offering).")
            ErrorMessages.Add(BDK_E_INVALID_OFFERING_GUID, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_PAYMENT_INSTRUMENT_STATUS, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_PAYMENT_METHOD_ID, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_PHONE_TYPE, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_POLICY_ID, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_REFERRALDATA_XML, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_STATE_FOR_COUNTRY, "{0}  ")
            ErrorMessages.Add(BDK_E_INVALID_SUBSCRIPTION_ID, "{0}  The subscription id specified is invalid.  Or, although the objectID is of correct type and also points to a valid account in SCS, the subscription it identifies does not exist in SCS.")
            ErrorMessages.Add(BDK_E_INVALID_TAX_EXEMPT_TYPE, "{0}  ")
            ErrorMessages.Add(BDK_E_MEG_CONFLICT, "{0}  ")
            ErrorMessages.Add(BDK_E_MULTIPLE_CITIES_FOUND, "{0}  ")
            ErrorMessages.Add(BDK_E_MULTIPLE_COUNTIES_FOUND, "{0}  ")
            ErrorMessages.Add(BDK_E_NON_ACTIVE_ACCOUNT, "{0}  ")
            ErrorMessages.Add(BDK_E_NOPERMISSION, "{0}  The calling partner does not have access to this method or when the requester does not have permission to search against the supplied search PUID.")
            ErrorMessages.Add(BDK_E_OBJECT_ROLE_LIMIT_EXCEEDED, "{0}  ")
            ErrorMessages.Add(BDK_E_OFFERING_ACCOUNT_CURRENCY_MISMATCH, "{0}  ")
            ErrorMessages.Add(BDK_E_OFFERING_COUNTRY_ACCOUNT_MISMATCH, "{0}  ")
            ErrorMessages.Add(BDK_E_OFFERING_NOT_PURCHASEABLE, "{0}  ")
            ErrorMessages.Add(BDK_E_OFFERING_PAYMENT_INSTRUMENT_MISMATCH, "{0}  ")
            ErrorMessages.Add(BDK_E_OFFERING_REQUIRES_PI, "{0}  ")
            ErrorMessages.Add(BDK_E_PARTNERNOTINBILLING, "{0}  ")
            ErrorMessages.Add(BDK_E_PAYMENT_PROVIDER_CONNECTION_FAILED, "{0}  ")
            ErrorMessages.Add(BDK_E_PRIMARY_PHONE_REQUIRED, "{0}  ")
            ErrorMessages.Add(BDK_E_POLICY_DEAL_COUNTRY_MISMATCH, "{0}  ")
            ErrorMessages.Add(BDK_E_PUID_ROLE_LIMIT_EXCEEDED, "{0}  ")
            ErrorMessages.Add(BDK_E_RATING_FAILURE, "{0}  ")
            ErrorMessages.Add(BDK_E_REQUIRED_FIELD_MISSING, "{0}  ")
            ErrorMessages.Add(BDK_E_STATE_CITY_INVALID, "{0}  ")
            ErrorMessages.Add(BDK_E_STATE_INVALID, "{0}  ")
            ErrorMessages.Add(BDK_E_STATE_ZIP_CITY_INVALID, "{0}  ")
            ErrorMessages.Add(BDK_E_STATE_ZIP_CITY_INVALID2, "{0}  ")
            ErrorMessages.Add(BDK_E_STATE_ZIP_CITY_INVALID3, "{0}  ")
            ErrorMessages.Add(BDK_E_STATE_ZIP_CITY_INVALID4, "{0}  ")
            ErrorMessages.Add(BDK_E_STATE_ZIP_COVERS_MULTIPLE_CITIES, "{0}  ")
            ErrorMessages.Add(BDK_E_STATE_ZIP_INVALID, "{0}  ")
            ErrorMessages.Add(BDK_E_TAXID_EXPDATE, "{0}  ")
            ErrorMessages.Add(BDK_E_TOKEN_BLACKLISTED, "{0}  ")
            ErrorMessages.Add(BDK_E_TOKEN_EXPIRED, "{0}  ")
            ErrorMessages.Add(BDK_E_TOKEN_NOT_VALID_FOR_OFFERING, "{0}  ")
            ErrorMessages.Add(BDK_E_TOKEN_RANGE_BLACKLISTED, "{0}  ")
            ErrorMessages.Add(BDK_E_TRANS_BALANCE_TO_PI_INVALID, "{0}  ")
            ErrorMessages.Add(BDK_E_UNKNOWN_SERVER_FAILURE, "{0}  Unknown server failure.")
            ErrorMessages.Add(BDK_E_UNSUPPORTED_CHAR_EXIST, "{0}  ")
            ErrorMessages.Add(BDK_E_VATID_DOESNOTHAVEEXPDATE, "{0}  ")
            ErrorMessages.Add(BDK_E_ZIP_CITY_MISSING, "{0}  ")
            ErrorMessages.Add(BDK_E_ZIP_INVALID, "{0}  Billing zip code error.")
            ErrorMessages.Add(BDK_E_ZIP_INVALID_FOR_ENTERED_STATE, "{0}  Billing zip code error.")
            ErrorMessages.Add(BDK_E_USAGE_COUNT_FOR_TOKEN_EXCEEDED, "{0}  Billing token is already spent.")
            ErrorMessages.Add(MissingParameterToStoredProcedure, "Missing parameter to stored procedure:  {0}")
            ErrorMessages.Add(SqlErrorInStoredProcedure, "SQL error {0} occurred in stored procedure {1}")
            ErrorMessages.Add(StoredProcedureContext, "CRM error {0} in {1}:{2}")
            ErrorMessages.Add(InvitingOrganizationNotFound, "{0} -- Inviting organization not found -- {1}")
            ErrorMessages.Add(InvitingUserNotInOrganization, "{0} -- Inviting user is not in the inviting organization -- {1}")
            ErrorMessages.Add(InvitedUserAlreadyExists, "{0} -- Invited user is already in an organization -- {1}")
            ErrorMessages.Add(InvitedUserIsOrganization, "{0} -- The user {1} has authentication {2} and is already related to organization {3} with relation id {4}")
            ErrorMessages.Add(InvitationNotFound, "{0} -- Invitation not found or status is not Open -- Token={1} Puid={2} Id={3} Status={4}")
            ErrorMessages.Add(InvitedUserAlreadyAdded, "{0} -- The crm user {1} is already added, but to organization {2} instead of the inviting organization {3}")
            ErrorMessages.Add(InvitationWrongUserOrgRelation, "{0} -- The pre-created userorg relation {1} is wrong.  Authentication {2} is already used by another user")
            ErrorMessages.Add(InvitationIsExpired, "{0} -- Invitation is expired -- Token={1} Puid={2} Id={3} Status={4}")
            ErrorMessages.Add(InvitationIsAccepted, "{0} -- Invitation has already been accepted -- Token={1} Puid={2} Id={3} Status={4}")
            ErrorMessages.Add(InvitationIsRejected, "{0} -- Invitation has already been rejected by the new user-- Token={1} Puid={2} Id={3} Status={4}")
            ErrorMessages.Add(InvitationIsRevoked, "{0} -- Invitation has been revoked by the organization -- Token={1} Puid={2} Id={3} Status={4}")
            ErrorMessages.Add(InvitedUserMultipleTimes, "The CRM user {0} has been invited multiple times.")
            ErrorMessages.Add(InvitationStatusError, """The invitation has status {0}.""")
            ErrorMessages.Add(InvalidInvitationToken, "The invitation token {0} is not correctly formatted.")
            ErrorMessages.Add(InvalidInvitationLiveId, "A user with this e-mail address was not found. Sign in to Windows Live ID with the same e-mail address where you received the invitation.  If you do not have a Windows Live ID, please create one using that e-mail address.")
            ErrorMessages.Add(InvitationSendToSelf, "The invitation cannot be sent to yourself.")
            ErrorMessages.Add(InvitationCannotBeReset, "The invitation for the user cannot be reset.")
            ErrorMessages.Add(UserDataNotFound, "The user data could not be found.")
            ErrorMessages.Add(CannotInviteDisabledUser, "An invitation cannot be sent to a disabled user")
            ErrorMessages.Add(InvitationBillingAdminUnknown, "You are not a billing administrator for this organization and therefore, you cannot send invitations.  You can either contact your billing administrator and ask him or her to send the invitation, or the billing administrator can visit https://billing.microsoft.com and make you a delegate billing administrator. You can then send invitations.")
            ErrorMessages.Add(CannotResetSysAdminInvite, "An invitation cannot be reset for a user if they are the only user that has the System Administrator Role.")
            ErrorMessages.Add(CannotSendInviteToDuplicateWindowsLiveId, "An invitation cannot be sent because there are multiple users with this WLID.")
            ErrorMessages.Add(UserInviteDisabled, "Invitation cannot be sent because user invitations are disabled.")
            ErrorMessages.Add(InvitationOrganizationNotEnabled, "The organization for the invitation is not enabled.")
            ErrorMessages.Add(ClientAuthSignedOut, "The user signed out.")
            ErrorMessages.Add(ClientAuthSyncIssue, "Synchronization between processes failed.")
            ErrorMessages.Add(ClientAuthCanceled, "Authentication was canceled by the user.")
            ErrorMessages.Add(ClientAuthNoConnectivityOffline, "There is no connectivity when running in offline mode.")
            ErrorMessages.Add(ClientAuthNoConnectivity, "There is no connectivity.")
            ErrorMessages.Add(ClientAuthOfflineInvalidCallerId, "Offline SDK calls must be made in the offline user context.")
            ErrorMessages.Add(ConfigDBObjectDoesNotExist, "'{0}' with Value = ({1}) does not exist in MSCRM_CONFIG database")
            ErrorMessages.Add(ConfigDBDuplicateRecord, "Duplicate '{0}' with Value = ({1}) exists in MSCRM_CONFIG database")
            ErrorMessages.Add(ConfigDBCannotDeleteObjectDueState, "Cannot delete '{0}' with Value = ({1}) in this State = ({2}) from MSCRM_CONFIG database")
            ErrorMessages.Add(ConfigDBCascadeDeleteNotAllowDelete, "Cannot delete '{0}' with Value = ({1}) due to child '{2}' references from MSCRM_CONFIG database")
            ErrorMessages.Add(MoveBothToPrimary, "Move operation would put both instances on the same server:  Database = {0}  Old Primary = {1}  Old Secondary = {2}  New Secondary = {3}")
            ErrorMessages.Add(MoveBothToSecondary, "Move operation would put both instances on the same server:  Database = {0}  Old Primary = {1}  Old Secondary = {2}  New Secondary = {3}")
            ErrorMessages.Add(MoveOrganizationFailedNotDisabled, "Move operation failed because organization {0} is not disabled")
            ErrorMessages.Add(ConfigDBCannotUpdateObjectDueState, "Cannot update '{0}' with Value = ({1}) in this State = ({2}) from MSCRM_CONFIG database")
            ErrorMessages.Add(LiveAdminUnknownObject, "Unknown administration target {0}")
            ErrorMessages.Add(LiveAdminUnknownCommand, "Unknown administration command {0}")
            ErrorMessages.Add(OperationOrganizationNotFullyDisabled, "The {1} operation failed because organization {0} is not fully disabled yet.  Use FORCE to override")
            ErrorMessages.Add(ConfigDBCannotDeleteDefaultOrganization, "The default {0} organization cannot be deleted from the MSCRM_CONFIG database.")
            ErrorMessages.Add(LicenseNotEnoughToActivate, "There are not enough licenses available for the number of users being activated.")
            ErrorMessages.Add(UserNotAssignedRoles, "The user has not been assigned any roles.")
            ErrorMessages.Add(TeamNotAssignedRoles, "The team has not been assigned any roles.")
            ErrorMessages.Add(InvalidLicenseKey, "Invalid license key ({0}).")
            ErrorMessages.Add(NoLicenseInConfigDB, "No license exists in MSCRM_CONFIG database.")
            ErrorMessages.Add(InvalidLicensePid, "Invalid license. Invalid PID (Product Id) ({0}).")
            ErrorMessages.Add(InvalidLicensePidGenCannotLoad, "Invalid license. PidGen.dll cannot be loaded from this path {0}")
            ErrorMessages.Add(InvalidLicensePidGenOtherError, "Invalid license. Cannot generate PID (Product Id) from License key. PidGen error code ({0}).")
            ErrorMessages.Add(InvalidLicenseCannotReadMpcFile, "Invalid license. MPC code cannot be read from MPC.txt file with this path {0}.")
            ErrorMessages.Add(InvalidLicenseMpcCode, "Invalid license. Invalid MPC code ({0}).")
            ErrorMessages.Add(LicenseUpgradePathNotAllowed, "Cannot upgrade to specified license type.")
            ErrorMessages.Add(OrgsInaccessible, "The client access license (CAL) results were not returned because one or more organizations in the deployment cannot be accessed.")
            ErrorMessages.Add(UserNotAssignedLicense, "The user has not been assigned any License")
            ErrorMessages.Add(UserCannotEnableWithoutLicense, "Cannot enable an unlicensed user")
            ErrorMessages.Add(LicenseConfigFileInvalid, "The provided configuration file {0} has invalid formatting.")
            ErrorMessages.Add(LicenseTrialExpired, "The trial installation of Microsoft Dynamics CRM has expired.")
            ErrorMessages.Add(LicenseRegistrationExpired, "The registration period for Microsoft Dynamics CRM has expired.")
            ErrorMessages.Add(LicenseTampered, "The licensing for this installation of Microsoft Dynamics CRM has been tampered with. The system is unusable. Please contact Microsoft Product Support Services.")
            ErrorMessages.Add(NonInteractiveUserCannotAccessUI, "Non-interactive users cannot access the web user interface. Contact your organization system administrator.")
            ErrorMessages.Add(InvalidOrganizationUniqueName, "Invalid organization unique name ({0}). Reason: ({1})")
            ErrorMessages.Add(InvalidOrganizationFriendlyName, "Invalid organization friendly name ({0}). Reason: ({1})")
            ErrorMessages.Add(OrganizationNotConfigured, "Organization is not configured yet")
            ErrorMessages.Add(InvalidDeviceToConfigureOrganization, "Mobile device cannot be used to configured organization")
            ErrorMessages.Add(InvalidBrowserToConfigureOrganization, "Browser not compatible to configure organization")
            ErrorMessages.Add(DeploymentServiceNotAllowSetToThisState, "Deployment Service for {0} allows the state Enabled or Disabled. Cannot set state to {1}.")
            ErrorMessages.Add(DeploymentServiceNotAllowOperation, "Deployment Service for {0} does not allow {1} operation.")
            ErrorMessages.Add(DeploymentServiceCannotChangeStateForDeploymentService, "You cannot change the state of this server because it contains the Deployment Service server role.")
            ErrorMessages.Add(DeploymentServiceRequestValidationFailure, "The Deployment Service cannot process the request because one or more validation checks failed.")
            ErrorMessages.Add(DeploymentServiceOperationIdentifierNotFound, "The Deployment Service could not find a deferred operation having the specified identifier.")
            ErrorMessages.Add(DeploymentServiceCannotDeleteOperationInProgress, "The Deployment Service cannot delete the specified operation because it is currently in progress.")
            ErrorMessages.Add(ConfigureClaimsBeforeIfd, "You must configure claims-based authentication before you can configure an Internet-facing deployment.")
            ErrorMessages.Add(EndUserNotificationTypeNotValidForEmail, "Cannot send Email for EndUserNotification Type: {0}.")
            ErrorMessages.Add(InvalidRecurrenceRuleForBulkDeleteAndDuplicateDetection, "Bulk Delete and Duplicate Detection recurrence must be specified as daily.")
            ErrorMessages.Add(InvalidRecurrenceInterval, "To set recurrence, you must specify an interval that is between 1 and 365.")
            ErrorMessages.Add(QueriesForDifferentEntities, "The Inner and Outer Queries must be for the same entity.")
            ErrorMessages.Add(AggregateInnerQuery, "The Inner Query must not be an aggregate query.")
            ErrorMessages.Add(InvalidDataDescription, "The data description for the visualization is invalid.")
            ErrorMessages.Add(NonPrimaryEntityDataDescriptionFound, "The data description for the visualization is invalid .The data description for the visualization can only have attributes either from the primary entity of the view or the linked entities.")
            ErrorMessages.Add(InvalidPresentationDescription, "The presentation description is invalid.")
            ErrorMessages.Add(SeriesMeasureCollectionMismatch, "Number of series for chart area and number of measure collections for category should be same.")
            ErrorMessages.Add(YValuesPerPointMeasureMismatch, "Number of YValuesPerPoint for series and number of measures for measure collection for category should be same.")
            ErrorMessages.Add(ChartAreaCategoryMismatch, "Number of chart areas and number of categories should be same.")
            ErrorMessages.Add(MultipleSubcategoriesFound, "The data XML for the visualization cannot contain more than two Group By clauses.")
            ErrorMessages.Add(MultipleMeasuresFound, "More than one measure is not supported for charts with subcategory i.e. comparison charts")
            ErrorMessages.Add(MultipleChartAreasFound, "Multiple Chart Areas are not supported.")
            ErrorMessages.Add(InvalidCategory, "Category is invalid. All the measures in the category either do not have same primary group by or are a mix of aggregate and non-aggregate data.")
            ErrorMessages.Add(InvalidMeasureCollection, "Measure collection is invalid. Not all the measures in the measure collection have the same group bys.")
            ErrorMessages.Add(DuplicateAliasFound, "Data Description is invalid. Duplicate alias found.")
            ErrorMessages.Add(EntityNotEnabledForCharts, "Charts are not enabled on the specified primary entity type code: {0}.")
            ErrorMessages.Add(InvalidPageResponse, "Invalid Page Response generated.")
            ErrorMessages.Add(VisualizationRenderingError, "An error occurred while the chart was rendering")
            ErrorMessages.Add(InvalidGroupByAlias, "Data Description is invalid. Same group by alias cannot be used for different attributes.")
            ErrorMessages.Add(MeasureDataTypeInvalid, "The Data Description for the visualization is invalid. The attribute type for one of the non aggregate measures is invalid. Correct the Data Description.")
            ErrorMessages.Add(NoDataForVisualization, "There is no data to create this visualization.")
            ErrorMessages.Add(VisualizationModuleNotFound, "No visualization module found with the given name.")
            ErrorMessages.Add(ImportVisualizationDeletedError, "A saved query visualization with id {0} is marked for deletion in the system. Please publish the customized entity first and then import again.")
            ErrorMessages.Add(ImportVisualizationExistingError, "A saved query visualization with id {0} already exists in the system, and cannot be resused by a new custom entity.")
            ErrorMessages.Add(VisualizationOtcNotFoundError, "Object type code is not specified for the visualization.")
            ErrorMessages.Add(InvalidDundasPresentationDescription, "The presentation description is not valid for dundas chart.")
            ErrorMessages.Add(InvalidWebResourceForVisualization, "The web resource type {0} is not supported for visualizations.")
            ErrorMessages.Add(ChartTypeNotSupportedForComparisonChart, "This chart type is not supported for comparison charts.")
            ErrorMessages.Add(InvalidFetchCollection, "The fetch collection for the visualization is invalid.")
            ErrorMessages.Add(CategoryDataTypeInvalid, "The Data Description for the visualization is invalid. The attribute type for the group by of one of the categories is invalid. Correct the Data Description.")
            ErrorMessages.Add(DuplicateGroupByFound, "Data Description is invalid. Same attribute cannot be used as a group by more than once.")
            ErrorMessages.Add(MultipleMeasureCollectionsFound, "More than one measure collection is not supported for charts with subcategory i.e. comparison charts")
            ErrorMessages.Add(InvalidGroupByColumn, "Group by not allowed on the attribute.")
            ErrorMessages.Add(InvalidFilterCriteriaForVisualization, "The visualization cannot be rendered for the given filter criteria.")
            ErrorMessages.Add(CountSpecifiedWithoutOrder, "The Data Description for the visualization is invalid as it does not specify an order node for the count attribute.")
            ErrorMessages.Add(NoPreviewForCustomWebResource, "This chart uses a custom Web resource. You cannot preview this chart.")
            ErrorMessages.Add(ChartTypeNotSupportedForMultipleSeriesChart, "Series of chart type {0} is not supported for multi-series charts.")
            ErrorMessages.Add(InsufficientColumnsInSubQuery, "One or more columns required by the outer query are not available from the sub-query.")
            ErrorMessages.Add(AggregateQueryRecordLimitExceeded, "AggregateQueryRecordLimit exceeded. Cannot perform this operation.")
            ErrorMessages.Add(QuickFindQueryRecordLimitExceeded, "QuickFindQueryRecordLimit exceeded. Cannot perform this operation.")
            ErrorMessages.Add(CannotAddOrActonBehalfAnotherUserPrivilege, "Act on Behalf of Another User privilege cannot be added or removed.")
            ErrorMessages.Add(HipNoSettingError, "No Hip application configuration setting [{0}] was found.")
            ErrorMessages.Add(HipInvalidCertificate, "Invalid Certificate for using HIP.")
            ErrorMessages.Add(NoSettingError, "No configdb configuration setting [{0}] was found.")
            ErrorMessages.Add(AppLockTimeout, "Timeout expired before applock could be acquired.")
            ErrorMessages.Add(InvalidRecurrencePattern, "Invalid recurrence pattern.")
            ErrorMessages.Add(CreateRecurrenceRuleFailed, "Cannot create the recurrence rule.")
            ErrorMessages.Add(PartialExpansionSettingLoadError, "Failed to retrieve partial expansion settings from the configuration database.")
            ErrorMessages.Add(InvalidCrmDateTime, "Invalid CrmDateTime.")
            ErrorMessages.Add(InvalidAppointmentInstance, "Invalid appointment entity instance.")
            ErrorMessages.Add(InvalidSeriesId, "SeriesId is null or invalid.")
            ErrorMessages.Add(AppointmentDeleted, "The appointment entity instance is already deleted.")
            ErrorMessages.Add(InvalidInstanceTypeCode, "Invalid instance type code.")
            ErrorMessages.Add(OverlappingInstances, "Two instances of the series cannot overlap.")
            ErrorMessages.Add(InvalidSeriesIdOriginalStart, "Invalid seriesid or original start date.")
            ErrorMessages.Add(ValidateNotSupported, "Validate method is not supported for recurring appointment master.")
            ErrorMessages.Add(RecurringSeriesCompleted, "The series has invalid ExpansionStateCode.")
            ErrorMessages.Add(ExpansionRequestIsOutsideExpansionWindow, "The series is already expanded for CutOffWindow.")
            ErrorMessages.Add(InvalidInstanceEntityName, "Invalid instance entity name.")
            ErrorMessages.Add(BookFirstInstanceFailed, "Failed to book first instance.")
            ErrorMessages.Add(InvalidSeriesStatus, "Invalid series status.")
            ErrorMessages.Add(RecurrenceRuleUpdateFailure, "Cannot update a rule that is attached to an existing rule master. Update the rule by using the parent entity.")
            ErrorMessages.Add(RecurrenceRuleDeleteFailure, "Cannot delete a rule that is attached to an existing rule master. Delete the rule by using the parent entity.")
            ErrorMessages.Add(EntityNotRule, "The collection name is not a recurrence rule.")
            ErrorMessages.Add(RecurringSeriesMasterIsLocked, "The recurring series master record is locked by some other process.")
            ErrorMessages.Add(UpdateRecurrenceRuleFailed, "Failed to update the recurrence rule. A corresponding recurrence rule cannot be found.")
            ErrorMessages.Add(InstanceOutsideEffectiveRange, "Cannot perform the operation. An instance is outside of series effective expansion range.")
            ErrorMessages.Add(RecurrenceCalendarTypeNotSupported, "The calendar type is not supported.")
            ErrorMessages.Add(RecurrenceHasNoOccurrence, "The recurrence pattern has no occurrences.")
            ErrorMessages.Add(RecurrenceStartDateTooSmall, "The recurrence pattern start date is invalid.")
            ErrorMessages.Add(RecurrenceEndDateTooBig, "The recurrence pattern end date is invalid.")
            ErrorMessages.Add(OccurrenceCrossingBoundary, "Two occurrences cannot overlap.")
            ErrorMessages.Add(OccurrenceTimeSpanTooBig, "Cannot perform the operation. An instance is outside of series effective expansion range.")
            ErrorMessages.Add(OccurrenceSkipsOverForward, "Cannot reschedule an occurrence of the recurring appointment if it skips over a later occurrence of the same appointment.")
            ErrorMessages.Add(OccurrenceSkipsOverBackward, "Cannot reschedule an occurrence of the recurring appointment if it skips over an earlier occurrence of the same appointment.")
            ErrorMessages.Add(InvalidDaysInFebruary, "February 29 can occur only when pattern start date is in a leap year.")
            ErrorMessages.Add(InvalidOccurrenceNumber, "The effective end date of the series cannot be earlier than today. Select a valid occurrence number.")
            ErrorMessages.Add(InvalidNumberOfPartitions, "You cannot delete audit data in the partitions that are currently in use, or delete the partitions that are created for storing future audit data.")
            ErrorMessages.Add(InvalidElementFound, "A dashboard Form XML cannot contain element: {0}.")
            ErrorMessages.Add(MaximumControlsLimitExceeded, "The dashboard Form XML contains more than the maximum allowed number of control elements: {0}.")
            ErrorMessages.Add(UserViewsOrVisualizationsFound, "A system dashboard cannot contain user views and visualizations.")
            ErrorMessages.Add(InvalidAttributeFound, "A dashboard Form XML cannot contain attribute: {0}.")
            ErrorMessages.Add(MultipleFormElementsFound, "A dashboard Form XML can contain only one form element.")
            ErrorMessages.Add(NullDashboardName, "The name of a dashboard cannot be null.")
            ErrorMessages.Add(InvalidFormType, "The type of the form must be set to {0} in the Form XML.")
            ErrorMessages.Add(InvalidControlClass, "The dashboard Form XML cannot contain controls elements with class id: {0}.")
            ErrorMessages.Add(ImportDashboardDeletedError, "A dashboard with the same id is marked as deleted in the system. Please first publish the system form entity and import again.")
            ErrorMessages.Add(PersonalReportFound, "A system dashboard cannot contain personal reports.")
            ErrorMessages.Add(ObjectAlreadyExists, "An object with id {0} already exists. Please change the id and try again.")
            ErrorMessages.Add(EntityTypeSpecifiedForDashboard, "An entity type cannot be specified for a dashboard.")
            ErrorMessages.Add(UnrestrictedIFrameInUserDashboard, "A user dashboard Form XML cannot have Security = false.")
            ErrorMessages.Add(MultipleLabelsInUserDashboard, "A user dashboard can have at most one label for a form element.")
            ErrorMessages.Add(UnsupportedDashboardInEditor, "The dashboard could not be opened.")
            ErrorMessages.Add(InvalidUrlProtocol, "The specified URL is invalid.")
            ErrorMessages.Add(CannotRemoveComponentFromDefaultSolution, "A Solution Component cannot be removed from the Default Solution.")
            ErrorMessages.Add(InvalidSolutionUniqueName, "Invalid character specified for solution unique name. Only characters within the ranges [A-Z], [a-z], [0-9] or _ are allowed. The first character may only be in the ranges [A-Z], [a-z] or _.")
            ErrorMessages.Add(CannotUndeleteLabel, "Attempting to undelete a label that is not marked as delete.")
            ErrorMessages.Add(ErrorReactivatingComponentInstance, "After undeleting a label, there is no underlying label to reactivate.")
            ErrorMessages.Add(CannotDeleteRestrictedSolution, "Attempting to delete a restricted solution.")
            ErrorMessages.Add(CannotDeleteRestrictedPublisher, "Attempting to delete a restricted publisher.")
            ErrorMessages.Add(ImportRestrictedSolutionError, "Solution ID provided is restricted and cannot be imported.")
            ErrorMessages.Add(CannotSetSolutionSystemAttributes, "System attributes ({0}) cannot be set outside of installation or upgrade.")
            ErrorMessages.Add(CannotUpdateDefaultSolution, "Default solution attribute{0} {1} can only be set on installation or upgrade.  The value{0} cannot be modified.")
            ErrorMessages.Add(CannotUpdateRestrictedSolution, "Restricted solution ({0}) cannot be updated.")
            ErrorMessages.Add(CannotAddWorkflowActivationToSolution, "Cannot add Workflow Activation to solution ")
            ErrorMessages.Add(CannotQueryBaseTableWithAggregates, "Invalid query on base table.  Aggregates cannot be included in base table query.")
            ErrorMessages.Add(InvalidStateTransition, "The {0} (Id={1}) entity or component has attempted to transition from an invalid state: {2}.")
            ErrorMessages.Add(CannotUpdateUnpublishedDeleteInstance, "The component that you are trying to update has been deleted.")
            ErrorMessages.Add(UnsupportedComponentOperation, "{0} is not recognized as a supported operation.")
            ErrorMessages.Add(InvalidCreateOnProtectedComponent, "You cannot create {0} {1}. Creation cannot be performed when {0} is managed.")
            ErrorMessages.Add(InvalidUpdateOnProtectedComponent, "You cannot update {0} {1}. Updates cannot be performed when {0} is managed.")
            ErrorMessages.Add(InvalidDeleteOnProtectedComponent, "You cannot delete {0} {1}. Deletion cannot be performed when {0} is managed.")
            ErrorMessages.Add(InvalidPublishOnProtectedComponent, "You cannot publish {0} {1}. Publish cannot be performed when {0} is managed.")
            ErrorMessages.Add(CannotAddNonCustomizableComponent, "The component {0} {1} cannot be added to the solution because it is not customizable")
            ErrorMessages.Add(CannotOverwriteActiveComponent, "A managed solution cannot overwrite the {0} component with Id={1} which has an unmanaged base instance.  The most likely scenario for this error is that an unmanaged solution has installed a new unmanaged {0} component on the target system, and now a managed solution from the same publisher is trying to install that same {0} component as managed.  This will cause an invalid layering of solutions on the target system and is not allowed.")
            ErrorMessages.Add(CannotUpdateRestrictedPublisher, "Restricted publisher ({0}) cannot be updated.")
            ErrorMessages.Add(CannotAddSolutionComponentWithoutRoots, "This item is not a valid solution component. For more information about solution components, see the Microsoft Dynamics CRM SDK documentation.")
            ErrorMessages.Add(ComponentDefinitionDoesNotExists, "No component definition exists for the component type {0}.")
            ErrorMessages.Add(DependencyAlreadyExists, "A {0} dependency already exists between {1}({2}) and {3}({4}).  Cannot also create {5} dependency.")
            ErrorMessages.Add(DependencyTableNotEmpty, "The dependency table must be empty for initialization to complete successfully.")
            ErrorMessages.Add(InvalidPublisherUniqueName, "Publisher uniquename is required.")
            ErrorMessages.Add(CannotUninstallWithDependencies, "Solution dependencies exist, cannot uninstall.")
            ErrorMessages.Add(InvalidSolutionVersion, "An invalid solution version was specified.")
            ErrorMessages.Add(CannotDeleteInUseComponent, "The {0}({1}) component cannot be deleted because it is referenced by {2} other components. For a list of referenced components, use the RetrieveDependenciesForDeleteRequest.")
            ErrorMessages.Add(CannotUninstallReferencedProtectedSolution, "This solution cannot be uninstalled because the '{0}' with id '{1}'  is required by the '{2}' solution. Uninstall the {2} solution and try again.")
            ErrorMessages.Add(CannotRemoveComponentFromSolution, "Cannot find solution component {0} {1} in solution {2}.")
            ErrorMessages.Add(RestrictedSolutionName, "The solution unique name '{0}' is restricted and can only be used by internal solutions.")
            ErrorMessages.Add(SolutionUniqueNameViolation, "The solution unique name '{0}' is already being used and cannot be used again.")
            ErrorMessages.Add(CannotUpdateManagedSolution, "Cannot update solution '{0}' because it is a managed solution.")
            ErrorMessages.Add(DependencyTrackingClosed, "Invalid attempt to process a dependency after the current transaction context has been closed.")
            ErrorMessages.Add(GenericManagedPropertyFailure, "The evaluation of the current component(name={0}, id={1}) in the current operation ({2}) failed during managed property evaluation of condition: {3}")
            ErrorMessages.Add(CombinedManagedPropertyFailure, "The evaluation of the current component(name={0}, id={1}) in the current operation ({2}) failed during at least one managed property evaluations: {3}")
            ErrorMessages.Add(ReportImportCategoryOptionNotFound, "A category option for the reports was not found.")
            ErrorMessages.Add(RequiredChildReportHasOtherParent, "A category option for the reports was not found.")
            ErrorMessages.Add(InvalidManagedPropertyException, "Managed property {0} does not contain enough information to be created.  Please provide (assembly, class), or (entity, attribute) or set the managed property to custom.")
            ErrorMessages.Add(OnlyOwnerCanSetManagedProperties, "Cannot import component {0}: {1}. The publisher of the solution that is being imported does not match the publisher of the solution that installed this component.")
            ErrorMessages.Add(CannotDeleteMetadata, "The '{2}' operation on the current component(name='{0}', id='{1}') failed during managed property evaluation of condition: '{3}'")
            ErrorMessages.Add(CannotUpdateReadOnlyPublisher, "Attempting to update a readonly publisher.")
            ErrorMessages.Add(CannotSelectReadOnlyPublisher, "Attempting to  select a readonly publisher for solution.")
            ErrorMessages.Add(CannotRemoveComponentFromSystemSolution, "A Solution Component cannot be removed from the System Solution.")
            ErrorMessages.Add(InvalidDependency, "The {2} component {1} (Id={0}) does not exist.  Failure trying to associate it with {3} (Id={4}) as a dependency. Missing dependency lookup type = {5}.")
            ErrorMessages.Add(InvalidDependencyFetchXml, "The FetchXml ({2}) is invalid.  Failure while calculating dependencies for {1} (Id={0}).")
            ErrorMessages.Add(CannotModifyReportOutsideSolutionIfManaged, "Managed solution cannot update reports which are not present in solution package.")
            ErrorMessages.Add(SharePointUnableToAddUserToGroup, "Microsoft Dynamics CRM cannot add this user {0} to the group {1} in SharePoint. Verify that the information for this user and group are correct and that the group exists in SharePoint, and then try again.")
            ErrorMessages.Add(SharePointUnableToRemoveUserFromGroup, "Unable to remove user {0} from group {1} in SharePoint.")
            ErrorMessages.Add(SharePointSiteNotPresentInSharePoint, "Site {0} does not exists in SharePoint.")
            ErrorMessages.Add(SharePointUnableToRetrieveGroup, "Unable to retrieve the group {0} from SharePoint.")
            ErrorMessages.Add(SharePointUnableToAclSiteWithPrivilege, "Unable to ACL site {0} with privilege {1} in SharePoint.")
            ErrorMessages.Add(SharePointUnableToAclSite, "Unable to ACL site {0} in SharePoint.")
            ErrorMessages.Add(SharePointUnableToCreateSiteGroup, "Unable to create site group {0} in SharePoint.")
            ErrorMessages.Add(SharePointSiteCreationFailure, "Failed to create the site {0} in SharePoint.")
            ErrorMessages.Add(SharePointTeamProvisionJobAlreadyExists, "A system job to provision the selected team is pending. Any changes made to the team record before this system job starts will be applied to this system job.")
            ErrorMessages.Add(SharePointRoleProvisionJobAlreadyExists, "A system job to provision the selected security role is pending. Any changes made to the security role record before this system job starts will be applied to this system job.")
            ErrorMessages.Add(SharePointSiteWideProvisioningJobFailed, "SharePoint provisioning job has failed.")
            ErrorMessages.Add(DataTypeMismatchForLinkedAttribute, "Data type mismatch found for linked attribute.")
            ErrorMessages.Add(InvalidEntityForLinkedAttribute, "Not a valid entity for linked attribute.")
            ErrorMessages.Add(AlreadyLinkedToAnotherAttribute, "Given linked attribute is alreadly linked to other attribute.")
            ErrorMessages.Add(DocumentManagementDisabled, "Document Management has been disabled for this organization.")
            ErrorMessages.Add(DefaultSiteCollectionUrlChanged, "Default site collection url has been changed this organization after this operation was created.")
            ErrorMessages.Add(RibbonImportHidingBasicHomeTab, "The definition of the ribbon being imported will remove the Microsoft Dynamics CRM home tab. Include a home tab definition, or a ribbon will not be displayed in areas of the application that display the home tab.")
            ErrorMessages.Add(RibbonImportInvalidPrivilegeName, "The RibbonDiffXml in this solution contains a reference to an invalid privilege: {0}. Update the RibbonDiffXml to reference a valid privilege and try importing again.")
            ErrorMessages.Add(RibbonImportEntityNotSupported, "The solution cannot be imported because the {0} entity contains a Ribbon definition, which is not supported for that entity. Remove the RibbonDiffXml node from the entity definition and try to import again.")
            ErrorMessages.Add(RibbonImportDependencyMissingEntity, "The ribbon item '{0}' is dependent on entity {1}.")
            ErrorMessages.Add(RibbonImportDependencyMissingRibbonElement, "The ribbon item '{0}' is dependent on <{1} Id=""{2}"" />.")
            ErrorMessages.Add(RibbonImportDependencyMissingWebResource, "The ribbon item '{0}' is dependent on Web resource id='{1}'.")
            ErrorMessages.Add(RibbonImportDependencyMissingRibbonControl, "The ribbon item '{0}' is dependent on ribbon control id='{1}'.")
            ErrorMessages.Add(RibbonImportModifyingTopLevelNode, "Ribbon customizations cannot be made to the following top-level ribbon nodes: <Ribbon>, <ContextualGroups>, and <Tabs>.")
            ErrorMessages.Add(RibbonImportLocationAndIdDoNotMatch, "CustomAction Id '{0}' cannot override '{1}' because '{2}' does not match the CustomAction Location value.")
            ErrorMessages.Add(RibbonImportHidingJewel, "Ribbon customizations cannot hide the <Jewel> node. Any ribbon customization that hides this node is ignored during import and will not be exported.")
            ErrorMessages.Add(RibbonImportDuplicateElementId, "The ribbon element with the Id:{0} cannot be imported because an existing ribbon element with the same Id already exists.")
            ErrorMessages.Add(WebResourceInvalidType, "Invalid web resource type specified.")
            ErrorMessages.Add(WebResourceEmptySilverlightVersion, "Silverlight version cannot be empty for silverlight web resources.")
            ErrorMessages.Add(WebResourceInvalidSilverlightVersion, "Silverlight version can only be of the format xx.xx[.xx.xx].")
            ErrorMessages.Add(WebResourceContentSizeExceeded, "Webresource content size is too big.")
            ErrorMessages.Add(WebResourceDuplicateName, "A webresource with the same name already exists. Use a different name.")
            ErrorMessages.Add(WebResourceEmptyName, "Webresource name cannot be null or empty.")
            ErrorMessages.Add(WebResourceNameInvalidCharacters, "Web resource names may only include letters, numbers, periods, and nonconsecutive forward slash characters.")
            ErrorMessages.Add(WebResourceNameInvalidPrefix, "Webresource name does not contain a valid prefix.")
            ErrorMessages.Add(WebResourceNameInvalidFileExtension, "A Web resource cannot have the following file extensions: .aspx, .ascx, .asmx or .ashx.")
            ErrorMessages.Add(WebResourceImportMissingFile, "The file for this Web resource does not exist in the solution file.")
            ErrorMessages.Add(WebResourceImportError, "An error occurred while importing a Web resource. Try importing this solution again. For further assistance, contact Microsoft Dynamics CRM technical support.")
            ErrorMessages.Add(InvalidActivityOwnershipTypeMask, "A custom entity defined as an activity must be user or team owned.")
            ErrorMessages.Add(ActivityCannotHaveRelatedActivities, "A custom entity defined as an activity must not have a relationship with Activities.")
            ErrorMessages.Add(CustomActivityMustHaveOfflineAvailability, "A custom entity defined as an activity must have Offline Availability.")
            ErrorMessages.Add(ActivityMustHaveRelatedNotes, "A custom entity defined as an activity must have a relationship to Notes by default.")
            ErrorMessages.Add(CustomActivityCannotBeMailMergeEnabled, "A custom entity defined as an activity already cannot have MailMerge enabled.")
            ErrorMessages.Add(InvalidCustomActivityType, "A custom entity defined as an activity must be of communicaton activity type.")
            ErrorMessages.Add(ActivityMetadataUpdate, "The metadata specified for activity is invalid.")
            ErrorMessages.Add(InvalidPrimaryFieldForActivity, "A custom entity defined as an activity cannot have primary attribute other than subject.")
            ErrorMessages.Add(CannotDeleteNonLeafNode, "Only a leaf statement can be deleted. This statement is parenting some other statement.")
            ErrorMessages.Add(DuplicateUIStatementRootsFound, "There can be only one root statement for a given uiscript.")
            ErrorMessages.Add(ErrorUpdateStatementTextIsReferenced, "You cannot update this UI script statement text because it is being referred to by one or more published ui scripts.")
            ErrorMessages.Add(ErrorDeleteStatementTextIsReferenced, "You cannot delete the UI script statement text because it is being referred by one or more ui script statements.")
            ErrorMessages.Add(ErrorScriptSessionCannotCreateForDraftScript, "You cannot create a UI script session for a UI script which is not published.")
            ErrorMessages.Add(ErrorScriptSessionCannotUpdateForDraftScript, "You cannot update a UI script session for a UI script which is not published.")
            ErrorMessages.Add(ErrorScriptLanguageNotInstalled, "The language specified is not supported in your CRM install. Please check with your system administrator on the list of ""enabled"" languages.")
            ErrorMessages.Add(ErrorScriptInitialStatementNotInScript, "The initial statement for this script does not belong to this script.")
            ErrorMessages.Add(ErrorScriptInitialStatementNotRoot, "The initial statement should the root statement and cannot have a previous statement set.")
            ErrorMessages.Add(ErrorScriptCannotDeletePublishedScript, "You cannot delete a UI script that is published. You must unpublish it first.")
            ErrorMessages.Add(ErrorScriptPublishMissingInitialStatement, "The selected UI script cannot be published. Provide a value for ""First statement number"" and try to publish again.")
            ErrorMessages.Add(ErrorScriptPublishMalformedScript, "The selected UI script cannot be published. The UI script contains one or more paths which do not end in an end-script or next-script action node. Correct the paths and try to publish again.")
            ErrorMessages.Add(ErrorScriptUnpublishActiveScript, "This script is in use and has active sessions (status-reason=incomplete). Please terminate the active sessions (i.e. status-reason=cancelled) and try to unpublish again.")
            ErrorMessages.Add(ErrorScriptSessionCannotSetStateForDraftScript, "You cannot set the state of a UI script session for a UI script which is not published.")
            ErrorMessages.Add(ErrorScriptStatementResponseTypeOnlyForPrompt, "You cannot associate the response control type for a statement which is not a prompt.")
            ErrorMessages.Add(ErrorStatementOnlyForDraftScript, "You cannot create a UI script statement for a UI script which is not draft.")
            ErrorMessages.Add(ErrorStatementDeleteOnlyForDraftScript, "You cannot delete a UI script statement for a UI script which is not draft.")
            ErrorMessages.Add(ErrorInvalidUIScriptImportFile, "File type is not supported. Select an xml file for import.")
            ErrorMessages.Add(ErrorScriptFileParse, "Error occurred while parsing the XML file.")
            ErrorMessages.Add(ErrorScriptCannotUpdatePublishedScript, "You cannot update a UI script that is published. You must unpublish it first.")
            ErrorMessages.Add(ErrorInvalidFileNameChars, "The Microsoft Excel file name cannot contain the following characters: *  \ : > < | ? "" /. Rename the file using valid characters, and try again.")
            ErrorMessages.Add(ErrorMimeTypeNullOrEmpty, "The MimeType property value of the UploadFromBase64DataUIScriptRequest method is null or empty. Specify a valid property value, and try again.")
            ErrorMessages.Add(ErrorImportInvalidForPublishedScript, "You cannot save data to a published UI script. Unpublish the UI script, and try again.")
            ErrorMessages.Add(UIScriptIdentifierDuplicate, "A variable or input argument with the same name already exists. Choose a different name, and try again.")
            ErrorMessages.Add(UIScriptIdentifierInvalid, "The variable or input argument name is invalid. The name can only contain '_', numerical, and alphabetical characters. Choose a different name, and try again.")
            ErrorMessages.Add(UIScriptIdentifierInvalidLength, "The variable or input argument name is too long. Choose a smaller name, and try again.")
            ErrorMessages.Add(ErrorNoQueryData, "An error has occurred. Either the data does not exist or you do not have sufficient privileges to view the data. Contact your system administrator for help.")
            ErrorMessages.Add(ErrorUIScriptPromptMissing, "The dialog that is being activated has no prompt/response.")
            ErrorMessages.Add(SharePointUrlHostValidator, "The URL cannot be resolved into an IP.")
            ErrorMessages.Add(SharePointCrmDomainValidator, "The SharePoint and Microsoft Dynamics CRM Servers are on different domains. Please ensure a trust relationship between the two domains.")
            ErrorMessages.Add(SharePointServerDiscoveryValidator, "The URL is incorrect or the site is not running.")
            ErrorMessages.Add(SharePointServerVersionValidator, "The SharePoint Site Collection must be running a supported version of Microsoft Office SharePoint Server or Microsoft Windows SharePoint Services. Please refer the implementation guide.")
            ErrorMessages.Add(SharePointSiteCollectionIsAccessibleValidator, "The URL is incorrect or the site is not running.")
            ErrorMessages.Add(SharePointUrlIsRootWebValidator, "The URL is not valid. The URL must be a valid site collection and cannot include a subsite. The URL must be in a valid form, such as http://SharePointServer/sites/CrmSite.")
            ErrorMessages.Add(SharePointSitePermissionsValidator, "The current user does not have the appropriate privileges. You must be a SharePoint site administrator on the SharePoint site.")
            ErrorMessages.Add(SharePointServerLanguageValidator, "Microsoft Dynamics CRM and Microsoft Office SharePoint Server must have the same base language.")
            ErrorMessages.Add(SharePointCrmGridIsInstalledValidator, "The Microsoft Dynamics CRM Grid component must be installed on the SharePoint server. This component is required for SharePoint integration to work correctly.")
            ErrorMessages.Add(SharePointErrorRetrieveAbsoluteUrl, "An error occurred while retrieving the absolute and site collection url for a SharePoint object.")
            ErrorMessages.Add(SharePointInvalidEntityForValidation, "Entity Does not support SharePoint Url Validation.")
            ErrorMessages.Add(DocumentManagementIsDisabled, "Document Management is not enabled for this Organization.")
            ErrorMessages.Add(DocumentManagementNotEnabledNoPrimaryField, "Document management could not be enabled because a primary field is not defined for this entity.")
            ErrorMessages.Add(SharePointErrorAbsoluteUrlClipped, "The URL exceeds the maximum number of 256 characters. Use shorter names for sites and folders, and try again.")
            ErrorMessages.Add(SiteMapXsdValidationError, "Sitemap xml failed XSD validation with the following error: '{0}' at line {1} position {2}.")
            ErrorMessages.Add(LinkedAttributeCannotBeSecured, "The linked attribute cannot be a secured attribute.")
            ErrorMessages.Add(CannotSecureAttribute, "The field '{0}' is not securable")
            ErrorMessages.Add(AttributePrivilegeCreateIsMissing, "The user does not have create permissions to a secured field. The requested operation could not be completed.")
            ErrorMessages.Add(AttributePermissionUpdateIsMissingDuringShare, "The user does not have update permissions to a secured field. The requested operation could not be completed.")
            ErrorMessages.Add(AttributePermissionReadIsMissing, "The user does not have read permissions to a secured field. The requested operation could not be completed.")
            ErrorMessages.Add(CannotRemoveSysAdminProfileFromSysAdminUser, "The Sys Admin Profile cannot be removed from a user with a Sys Admin Role")
            ErrorMessages.Add(QueryContainedSecuredAttributeWithoutAccess, "The Query contained a secured attribute to which the caller does not have access")
            ErrorMessages.Add(AttributePermissionUpdateIsMissingDuringUpdate, "The user doesn't have AttributePrivilegeUpdate and not granted shared access for a secured attribute during update operation")
            ErrorMessages.Add(AttributeNotSecured, "One or more fields are not enabled for field level security. Field level security is not enabled until you publish the customizations.")
            ErrorMessages.Add(AttributeSharingCreateShouldSetReadOrAndUpdateAccess, "You must set read and/or update access when you share a secured attribute. Attribute ID: {0}")
            ErrorMessages.Add(AttributeSharingUpdateInvalid, "Both readAccess and updateAccess are false: call Delete instead of Update.")
            ErrorMessages.Add(AttributeSharingCreateDuplicate, "Attribute has already been shared.")
            ErrorMessages.Add(AdminProfileCannotBeEditedOrDeleted, "The System Administrator field security profile cannot be modified or deleted.")
            ErrorMessages.Add(AttributePrivilegeInvalidToUnsecure, "You must have sufficient permissions for a secured field before you can change its field level security.")
            ErrorMessages.Add(RequireValidImportMapForUpdate, "The update operation cannot be completed because the import map used for the update is invalid.")
            ErrorMessages.Add(InvalidFormatForUpdateMode, "The file that you uploaded is invalid and cannot be used for updating records.")
            ErrorMessages.Add(MaximumCountForUpdateModeExceeded, "In an update operation, you can import only one file at a time.")
            ErrorMessages.Add(RecordResolutionFailed, "The record could not be updated because the original record no longer exists in Microsoft Dynamics CRM.")
            ErrorMessages.Add(InvalidOperationForDynamicList, "This action is not available for a dynamic marketing list.")
            ErrorMessages.Add(QueryNotValidForStaticList, "Query cannot be specified for a static list.")
            ErrorMessages.Add(LockStatusNotValidForDynamicList, "Lock Status cannot be specified for a dynamic list.")
            ErrorMessages.Add(CannotCopyStaticList, "This action is valid only for dynamic list.")
            ErrorMessages.Add(CannotDeleteSystemForm, "System forms cannot be deleted.")
            ErrorMessages.Add(CannotUpdateSystemEntityIcons, "System entity icons cannot be updated.")
            ErrorMessages.Add(FallbackFormDeletion, "You cannot delete this form because it is the only fallback form of type {0} for the {1} entity. Each entity must have at least one fallback form for each form type.")
            ErrorMessages.Add(SystemFormImportMissingRoles, "The unmanaged solution you are importing has displaycondition XML attributes that refer to security roles that are missing from the target system. Any displaycondition attributes that refer to these security roles will be removed.")
            ErrorMessages.Add(SystemFormCopyUnmatchedEntity, "The entity for the Target and the SourceId must match.")
            ErrorMessages.Add(SystemFormCopyUnmatchedFormType, "The form type of the SourceId is not valid for the Target entity.")
            ErrorMessages.Add(SystemFormCreateWithExistingLabel, "The label '{0}', id: '{1}' already exists. Supply unique labelid values.")
            ErrorMessages.Add(QuickFormNotCustomizableThroughSdk, "The SDK does not support creating a form of type ""Quick"". This form type is reserved for internal use only.")
            ErrorMessages.Add(InvalidDeactivateFormType, "You can�t deactivate {0} forms. Only Main forms can be inactive.")
            ErrorMessages.Add(FallbackFormDeactivation, "This operation can�t be completed. You must have at least one active Main form.")
            ErrorMessages.Add(RuntimeRibbonXmlValidation, "The most recent customized ribbon for a tab on this page cannot be generated. The out-of-box version of the ribbon is displayed instead.")
            ErrorMessages.Add(InitializeErrorNoReadOnSource, "The operation could not be completed because you donot have read access on some of the fields in source record.")
            ErrorMessages.Add(NoRollupAttributesDefined, "For rollup to succeed atleast one rollup attribute needs to be associated with the goal metric")
            ErrorMessages.Add(GoalPercentageAchievedValueOutOfRange, "The percentage achieved value has been set to 0 because the calculated value is not in the allowed range.")
            ErrorMessages.Add(InvalidRollupQueryAttributeSet, "A Rollup Query cannot be set for a Rollup Field that is not defined in the Goal Metric.")
            ErrorMessages.Add(InvalidGoalManager, "The manager of a goal can only be a user and not a team.")
            ErrorMessages.Add(InactiveRollupQuerySetOnGoal, "An inactive rollup query cannot be set on a goal.")
            ErrorMessages.Add(InactiveMetricSetOnGoal, "An inactive metric cannot be set on a goal.")
            ErrorMessages.Add(MetricEntityOrFieldDeleted, "The entity or field that is referenced in the goal metric is not valid")
            ErrorMessages.Add(ExceededNumberOfRecordsCanFollow, "You have exceeded the number of records you can follow. Please unfollow some records to start following again.")
            ErrorMessages.Add(EntityIsNotEnabledForFollowUser, "This entity is not enabled to be followed. ")
            ErrorMessages.Add(EntityIsNotEnabledForFollow, "This entity is not enabled to be followed. ")
            ErrorMessages.Add(CannotFollowInactiveEntity, "Can't follow inactive record. ")
            ErrorMessages.Add(MustContainAtLeastACharInMention, "The display name must contain atleast one non-whitespace character.")
            ErrorMessages.Add(LanguageProvisioningSrsDataConnectorNotInstalled, "The Microsoft Dynamics CRM Reporting Extensions must be installed before the language can be provisioned for this organization.")
            ErrorMessages.Add(BidsInvalidConnectionString, "Input connection string is invalid. Usage: ServerUrl[;OrganizationName][;HomeRealmUrl]")
            ErrorMessages.Add(BidsInvalidUrl, "Input url {0} is invalid.")
            ErrorMessages.Add(BidsServerConnectionFailed, "Failed to connect to server {0}.")
            ErrorMessages.Add(BidsAuthenticationError, "An error occured while authenticating with server {0}.")
            ErrorMessages.Add(BidsNoOrganizationsFound, "No organizations found for the user.")
            ErrorMessages.Add(BidsOrganizationNotFound, "Organization {0} cannot be found for the user.")
            ErrorMessages.Add(BidsAuthenticationFailed, "Authentication failed when trying to connect to server {0}. The username or password is incorrect.")
            ErrorMessages.Add(TransactionNotSupported, "The operation that you are trying to perform does not support transactions.")
            ErrorMessages.Add(IndexOutOfRange, "The index {0} is out of range for {1}. Number of elements present are {2}.")
            ErrorMessages.Add(InvalidAttribute, "Attribute {0} cannot be found for entity {1}.")
            ErrorMessages.Add(MultiValueParameterFound, "Fetch xml parameter {0} cannot obtain multiple values. Change report parameter {0} to single value parameter and try again.")
            ErrorMessages.Add(QueryParameterNotUnique, "Query parameter {0} must be defined only once within the data set.")
            ErrorMessages.Add(InvalidEntity, "Entity {0} cannot be found.")
            ErrorMessages.Add(UnsupportedAttributeType, "Attribute type {0} is not supported. Remove attribute {1} from the query and try again.")
            ErrorMessages.Add(FetchDataSetQueryTimeout, "The fetch data set query timed out after {0} seconds. Increase the query timeout, and try again.")
            ErrorMessages.Add(InvalidCommand, "Invalid command.")
            ErrorMessages.Add(InvalidDataXml, "Invalid data xml.")
            ErrorMessages.Add(InvalidLanguageForProcessConfiguration, "Process configuration is not available since your language does not match system base language.")
            ErrorMessages.Add(InvalidComplexControlId, "The complex control id is invalid.")
            ErrorMessages.Add(InvalidProcessControlEntity, "The process control definition contains an invalid entity or invalid entity order.")
            ErrorMessages.Add(InvalidProcessControlAttribute, "The process control definition contains an invalid attribute.")
            ErrorMessages.Add(BadRequest, "The request could not be understood by the server.")
            ErrorMessages.Add(AccessTokenExpired, "The requested resource requires authentication.")
            ErrorMessages.Add(Forbidden, "The server refuses to fulfill the request.")
            ErrorMessages.Add(Throttling, "Too many requests.")
            ErrorMessages.Add(NetworkIssue, "Request failed due to unknown network issues or GateWay issues or server issues.")
            ErrorMessages.Add(CouldNotReadAccessToken, "The system was not able to read users Yammer access token although a non-empty code was passed.")
            ErrorMessages.Add(NotVerifiedAdmin, "You need an enterprise account with Yammer in order to complete this setup. Please sign in with a Yammer administrator account or contact a Yammer administrator for help.")
            ErrorMessages.Add(YammerAuthTimedOut, "You have waited too long to complete the Yammer authorization. Please try again.")
            ErrorMessages.Add(NoYammerNetworksFound, "You are not authorized for any Yammer network. Please reauthorize the Yammer setup with a Yammer administrator account or contact a Yammer administrator for help.")
            ErrorMessages.Add(OAuthTokenNotFound, "Yammer OAuth token is not found. You should configure Yammer before accessing any related feature.")
            ErrorMessages.Add(CouldNotDecryptOAuthToken, "Yammer OAuth token could not be decrypted. Please try to reconfigure Yammer once again.")
            ErrorMessages.Add(UserNeverLoggedIntoYammer, "To follow other users, you must be logged in to Yammer. Log in to your Yammer account, and try again.")
            ErrorMessages.Add(StepNotSupportedForClientBusinessRule, "Step {0} is not supported for client side business rule.")
            ErrorMessages.Add(EventNotSupportedForBusinessRule, "Event {0} is not supported for client side business rule.")
            ErrorMessages.Add(CannotUpdateTriggerForPublishedRules, "A trigger cannot be added/deleted/updated for a published rule.")
            ErrorMessages.Add(EventTypeAndControlNameAreMismatched, "This combination of event type and control name is unexpected")
            ErrorMessages.Add(ExpressionNotSupportedForEditor, "Rule contain an expression that is not supported by the editor.")
            ErrorMessages.Add(EditorOnlySupportAndOperatorForLogicalConditions, "The rule expression contains logical operator which is not supported. The editor only support And operator for Logical conditions.")
            ErrorMessages.Add(UnexpectedRightOperandCount, "The right operand array in the expression contain unexpected no. of operand.")
            ErrorMessages.Add(RuleNotSupportedForEditor, "The current rule definition cannot be edited in the Business rule editor.")
            ErrorMessages.Add(BusinessRuleEditorSupportsOnlyIfConditionBranch, "The business rule editor only supports one if condition. Please fix the rule.")
            ErrorMessages.Add(UnsupportedStepForBusinessRuleEditor, "The rule contain a step which is not supported by the editor.")
            ErrorMessages.Add(UnsupportedAttributeForEditor, "The rule contain an attribute which is not supported.")
            ErrorMessages.Add(ExpectingAtLeastOneBusinessRuleStep, "There should be a minimum of one Business rule step.")
            ErrorMessages.Add(EntityLimitExceeded, "MultiEntitySearch exceeded Entity Limit defined for the Organization.")
            ErrorMessages.Add(InvalidSearchEntity, "Invalid Search Entity - {0}.")
            ErrorMessages.Add(InvalidSearchEntities, "Search - {0} did not find any valid Entities.")
            ErrorMessages.Add(NoQuickFindFound, "Entity - {0} did not have a valid Quickfind query.")
            ErrorMessages.Add(InvalidSearchName, "Invalid Search Name - {0}.")
            ErrorMessages.Add(EntityGroupNameOrEntityNamesMustBeProvided, "Missing parameter. You must provide EntityGroupName or EntityNames.")
            ErrorMessages.Add(OnlyOneSearchParameterMustBeProvided, "Extra parameter. You only need to provide EntityGroupName or EntityNames, not both.")
            ErrorMessages.Add(WorkflowIdIsNull, "Workflow Id cannot be NULL while creating business process flow category")
            ErrorMessages.Add(PrimaryEntityIsNull, "Primary Entity cannot be NULL while creating business process flow category")
            ErrorMessages.Add(TypeNotSetToDefinition, "Type should be set to Definition while creating business process flow category")
            ErrorMessages.Add(ScopeNotSetToGlobal, "Scope should be set to Global while creating business process flow category")
            ErrorMessages.Add(CategoryNotSetToBusinessProcessFlow, "Category should be set to BusinessProcessFlow while creating business process flow category")
            ErrorMessages.Add(BusinessProcessFlowStepHasInvalidParent, "{0} parent is not of type {1}")
            ErrorMessages.Add(NullOrEmptyAttributeInXaml, "Attribute - {0} of {1} cannot be null or empty")
            ErrorMessages.Add(InvalidGuidInXaml, "Guid - {0} in the Xaml is not valid")
            ErrorMessages.Add(NoLabelsAssociatedWithStep, "{0} does not have any labels associated with it")
            ErrorMessages.Add(StepStepDoesNotHaveAnyControlStepAsItsChildren, "StepStep does not have any ControlStep as its children")
            ErrorMessages.Add(InvalidXmlForParameters, "Parameters node for ControlStep have invalid XML in it")
            ErrorMessages.Add(ControlIdIsNotUnique, "Control id {0} in the Xaml is not unique")
            ErrorMessages.Add(InvalidAttributeInXaml, "Attribute - {0} in the XAML is invalid")
            ErrorMessages.Add(AttributeCannotBeUpdated, "Attribute - {0} cannot be updated for a Business Process Flow")
            ErrorMessages.Add(StepCountInXamlExceedsMaxAllowed, "There are {0} {1} in the Xaml. Max allowed is {3}")
            ErrorMessages.Add(StepDoesNotHaveAnyChildInXaml, "{0} does not have atleast one {1} as its child")
            ErrorMessages.Add(InvalidXaml, "XAML for workflow is NULL or Empty")
            ErrorMessages.Add(ProcessNameIsNullOrEmpty, "The business process flow name is NULL or empty. ")
            ErrorMessages.Add(LabelIdDoesNotMatchStepId, "The label ID {0} doesn�t match the step ID {1}.")
            ErrorMessages.Add(EntityExceedsMaxActiveBusinessProcessFlows, "The {0} entity exceeds the maximum number of active business process flows. The limit is {1}.")
            ErrorMessages.Add(EntityIsNotBusinessProcessFlowEnabled, "The IsBusinessProcessEnabled property of the {0} entity is false.")
            ErrorMessages.Add(CannotDeleteUserMailbox, "The mailbox associated to a user or a queue cannot be deleted.")
            ErrorMessages.Add(EmailServerProfileSslRequiredForOnline, "You cannot set SSL as false for Microsoft Dynamics CRM Online.")
            ErrorMessages.Add(EmailServerProfileInvalidCredentialRetrievalForOnline, "Windows integrated or Anonymous authentication cannot be used as a connection type for Microsoft Dynamics CRM Online.")
            ErrorMessages.Add(EmailServerProfileInvalidCredentialRetrievalForExchange, "No credentials (Anonymous) cannot be used a connection type for exchange e-mail server type.")
            ErrorMessages.Add(EmailServerProfileAutoDiscoverNotAllowed, "Auto discover server URL can location can only be used for an exchange e-mail server type.")
            ErrorMessages.Add(EmailServerProfileLocationNotRequired, "You cannot specify the incoming/outgoing e-mail server location when Autodiscover server location has been set to true.")
            ErrorMessages.Add(ForwardMailboxCannotAssociateWithUser, "A forward mailbox cannot be created for a specific user or a queue.  Please remove the regarding field and try again.")
            ErrorMessages.Add(MailboxCannotModifyEmailAddress, "E-mail Address of a mailbox cannot be updated when associated with an user/queue.")
            ErrorMessages.Add(MailboxCredentialNotSpecified, "Username is not specified")
            ErrorMessages.Add(EmailServerProfileInvalidServerLocation, "The specified server location {0} is invalid. Correct the server location and try again.")
            ErrorMessages.Add(CannotAcceptEmail, "The email that you are trying to deliver cannot be accepted by Microsoft Dynamics CRM. Reason Code: {0}.")
            ErrorMessages.Add(QueueMailboxUnexpectedDeliveryMethod, "Delivery method for mailbox associated with a queue cannot be outlook client.")
            ErrorMessages.Add(ForwardMailboxEmailAddressRequired, "An e-mail address is a required field in case of forward mailbox.")
            ErrorMessages.Add(ForwardMailboxUnexpectedIncomingDeliveryMethod, "Forward mailbox incoming delivery method can only be none or router.")
            ErrorMessages.Add(ForwardMailboxUnexpectedOutgoingDeliveryMethod, "Forward mailbox outgoing delivery method can only be none.")
            ErrorMessages.Add(InvalidCredentialTypeForNonExchangeIncomingConnection, "For a POP3 email server type, you can only connect using credentials that are specified by a user or queue.")
            ErrorMessages.Add(Pop3UnexpectedException, "Exception occur while polling mails using Pop3 protocol.")
            ErrorMessages.Add(OpenMailboxException, "Exception occurs while opening mailbox for Exchaange mail server.")
            ErrorMessages.Add(InvalidMailbox, "Invalid mailboxId passed in. Please check the mailboxid.")
            ErrorMessages.Add(InvalidEmailServerLocation, "The server location is either not present or is not valid. Please correct the server location.")
            ErrorMessages.Add(InactiveMailbox, "The mailbox is in inactive state. Send/Receive mails are allowed only for active mailboxes.")
            ErrorMessages.Add(UnapprovedMailbox, "The mailbox is not in approved state. Send/Receive mails are allowed only for approved mailboxes.")
            ErrorMessages.Add(InvalidEmailAddressInMailbox, "The email address in the mailbox is not correct. Please enter the correct email address to process mails.")
            ErrorMessages.Add(EmailServerProfileNotAssociated, "Email Server Profile is not associated with the current mailbox. Please associate a valid profile to send/receive mails.")
            ErrorMessages.Add(IncomingDeliveryIsForwardMailbox, "Cannot poll mails from the mailbox. Its incoming delivery method is Forward mailbox.")
            ErrorMessages.Add(InvalidIncomingDeliveryExpectingEmailConnector, "The incoming delivery method is not email connector. To receive mails its incoming delivery method should be Email Connector.")
            ErrorMessages.Add(OutgoingNotAllowedForForwardMailbox, "Mailbox is a forward mailbox. A forward mailbox cannot send the mails.")
            ErrorMessages.Add(InvalidOutgoingDeliveryExpectingEmailConnector, "The outgoing delivery method is not email connector. To send mails its outgoing delivery method should be Email Connector.")
            ErrorMessages.Add(InaccessibleSmtpServer, "Cannot reach to the smtp server. Please check that the smtp server is accessible.")
            ErrorMessages.Add(InactiveEmailServerProfile, "Email server profile is disabled. Cannot process email for disabled profile.")
            ErrorMessages.Add(CannotUseUserCredentials, "Email connector cannot use the credentials specified in the mailbox entity. This might be because user has disallowed it. Please use other mode of credential retrieval or allow the use of credential by email connector.")
            ErrorMessages.Add(CannotActivateMailboxForDisabledUserOrQueue, "Mailbox cannot be activated because the user or queue associated with the mailbox is in disabled state. Mailbox can only be activated for Active User/Queue.")
            ErrorMessages.Add(ZeroEmailReceived, "There were no email available in the mailbox or could not be retrieved.")
            ErrorMessages.Add(NoTestEmailAccessPrivilege, "There is not sufficient privilege to perform the test access.")
            ErrorMessages.Add(MailboxCannotDeleteEmails, "The Delete Emails after Processing option cannot be set to Yes for user mailboxes.")
            ErrorMessages.Add(EmailServerProfileSslRequiredForOnPremise, "Usage of SSL while contacting external email servers is mandatory for this CRM deployment.")
            ErrorMessages.Add(EmailServerProfileDelegateAccessNotAllowed, "For an SMTP email server type, auto-granted delegate access cannot be used.")
            ErrorMessages.Add(EmailServerProfileImpersonationNotAllowed, "For a Non Exchange email server type, impersonation cannot be used.")
            ErrorMessages.Add(EmailMessageSizeExceeded, "Email Size Exceeds the MaximumMessageSizeLimit specified by the deployment.")
            ErrorMessages.Add(OutgoingSettingsUpdateNotAllowed, "Different outgoing connection settings cannot be specified because the " + "\Use Same Settings for Outgoing Connections\" + "flag is set to True.")
            ErrorMessages.Add(CertificateNotFound, "The given certificate cannot be found.")
            ErrorMessages.Add(InvalidCertificate, "The given certificate is invalid.")
            ErrorMessages.Add(EmailServerProfileInvalidAuthenticationProtocol, "The authentication protocol is invalid for the specified server and connection type. For more information, contact your system administrator.")
            ErrorMessages.Add(EmailServerProfileADBasedAuthenticationProtocolNotAllowed, "The authentication protocol cannot be set to Negotiate or NTLM for your organization because these require Active Directory. Use a different authentication protocol or contact your system administrator to enable an Active Directory-based authentication protocol.")
            ErrorMessages.Add(EmailServerProfileBasicAuthenticationProtocolNotAllowed, "The specified authentication protocol cannot be used because the protocol requires sending credentials on a secure channel. Use a different authentication protocol or contact your administrator to enable Basic authentication protocol on a non-secure channel.")
            ErrorMessages.Add(IncomingServerLocationAndSslSetToNo, "The URL specified for Incoming Server Location uses HTTPS but the Use SSL for Incoming Connection option is set to No. Set this option to Yes, and then try again.")
            ErrorMessages.Add(OutgoingServerLocationAndSslSetToNo, "The URL specified for Outgoing Server Location uses HTTPS but the Use SSL for Outgoing Connection option is set to No. Set this option to Yes, and then try again.")
            ErrorMessages.Add(IncomingServerLocationAndSslSetToYes, "The URL specified for Incoming Server Location uses HTTP but the Use SSL for Incoming Connection option is set to Yes. Specify a server location that uses HTTPS, and then try again.")
            ErrorMessages.Add(OutgoingServerLocationAndSslSetToYes, "The URL specified for Outgoing Server Location uses HTTP but the Use SSL for Outgoing Connection option is set to Yes. Specify a server location that uses HTTPS, and then try again.")
            ErrorMessages.Add(UnsupportedEmailServer, "The email server isn't supported.")
            ErrorMessages.Add(S2SAccessTokenCannotBeAcquired, "Failed to acquire S2S access token from authorization server.")
            ErrorMessages.Add(InvalidValueProcessEmailAfter, "The date in the Process Email From field is earlier than what is allowed for your organization. Enter a date that is later than the one specified, and try again.")
            ErrorMessages.Add(InvalidS2SAuthentication, "You can use server-to-server authentication only for email server profiles created for a Microsoft Dynamics CRM Online organization that was set up through the Microsoft online services environment (Office 365).")
            ErrorMessages.Add(RouterIsDisabled, "Microsoft Dynamics CRM has been configured to use server-side synchronization to process email. If you want to use the Email Router to process email, go to System Settings and change the Process Email Using field to Microsoft Dynamics CRM 2013 Email Router.")
            ErrorMessages.Add(TraceMessageConstructionError, "The trace record has an invalid trace code or an insufficient number of trace parameters.")
            ErrorMessages.Add(TooManyBytesInInputStream, "The stream being read from has too many bytes.")
            ErrorMessages.Add(EmailRouterFileTooLargeToProcess, "One or more of the email router configuration files is too large to get processed.")
            ErrorMessages.Add(ErrorsInEmailRouterMigrationFiles, "Invalid File(s) for Email Router Migration")
            ErrorMessages.Add(InvalidMigrationFileContent, "The content of the import file is not valid. You must select a text file.")
            ErrorMessages.Add(ErrorMigrationProcessExcessOnServer, "The server is busy handling other migration processes. Please try after some time.")
            ErrorMessages.Add(EntityNotEnabledForThisDevice, "Entity not enabled to be viewed in this device")
            ErrorMessages.Add(MobileClientLanguageNotSupported, "The application could not find a supported language on the server. Contact an administrator to enable a supported language")
            ErrorMessages.Add(MobileClientVersionNotSupported, "Mobile Client version is not supported")
            ErrorMessages.Add(RoleNotEnabledForTabletApp, "You haven't been authorized to use this app.\\nCheck with your system administrator to update your settings.")
            ErrorMessages.Add(NoMinimumRequiredPrivilegesForTabletApp, "You do not have sufficient permissions on the server to load the application.\\nPlease contact your administrator to update your permissions.")
            ErrorMessages.Add(FilePickerErrorAttachmentTypeBlocked, "Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.")
            ErrorMessages.Add(FilePickerErrorFileSizeBreached, "Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.")
            ErrorMessages.Add(FilePickerErrorFileSizeCannotBeZero, "Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.")
            ErrorMessages.Add(FilePickerErrorUnableToOpenFile, "Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.")
            ErrorMessages.Add(GetPhotoFromGalleryFailed, "Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.")
            ErrorMessages.Add(SaveDataFileErrorOutOfSpace, "Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.")
            ErrorMessages.Add(OpenDocumentErrorCodeUnableToFindAnActivity, "Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.")
            ErrorMessages.Add(OpenDocumentErrorCodeUnableToFindTheDataId, "Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.")
            ErrorMessages.Add(OpenDocumentErrorCodeGeneric, "Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.")
            ErrorMessages.Add(FilePickerErrorApplicationInSnapView, "Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.")
            ErrorMessages.Add(MobileClientNotConfiguredForCurrentUser, "Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.")
            ErrorMessages.Add(DataSourceInitializeFailedErrorCode, "Try this action again. If the problem continues, check the {0} for solutions or contact your organization's {#Brand_CRM} Administrator. Finally, you can contact {1}.")
            ErrorMessages.Add(DataSourceOfflineErrorCode, "This operation failed because you're offline. Reconnect and try again.")
            ErrorMessages.Add(PingFailureErrorCode, "The system couldn't reconnect with your {#Brand_CRM} server.")
            ErrorMessages.Add(RetrieveRecordOfflineErrorCode, "This record isn't available while you're offline.  Reconnect and try again.")
            ErrorMessages.Add(NotMobileEnabled, "You can't view this type of record on your tablet. Contact your system administrator.")
        End Sub

        Public Shared Function GetErrorMessage(ByVal hResult As Integer) As String
            Dim errorMessage As String = TryCast(ErrorMessages(hResult), String)
            If String.IsNullOrEmpty(errorMessage) Then
                errorMessage = "Server was unable to process request."
            End If
            Return errorMessage
        End Function

        Public Const CustomImageAttributeOnlyAllowedOnCustomEntity As Integer = CInt(&H80048531) ' -2147187407
        Public Const SqlEncryptionSymmetricKeyCannotOpenBecauseWrongPassword As Integer = CInt(&H80048530) ' -2147187408
        Public Const SqlEncryptionSymmetricKeyDoesNotExistOrNoPermission As Integer = CInt(&H8004852F) ' -2147187409
        Public Const SqlEncryptionSymmetricKeyPasswordDoesNotExistInConfigDB As Integer = CInt(&H8004852E) ' -2147187410
        Public Const SqlEncryptionSymmetricKeySourceDoesNotExistInConfigDB As Integer = CInt(&H8004852D) ' -2147187411
        Public Const CannotExecuteRequestBecauseHttpsIsRequired As Integer = CInt(&H8004852C) ' -2147187412
        Public Const SqlEncryptionRestoreEncryptionKeyCannotDecryptExistingData As Integer = CInt(&H8004852B) ' -2147187413
        Public Const SqlEncryptionSetEncryptionKeyIsAlreadyRunningCannotRunItInParallel As Integer = CInt(&H8004852A) ' -2147187414
        Public Const SqlEncryptionChangeEncryptionKeyExceededQuotaForTheInterval As Integer = CInt(&H80048529) ' -2147187415
        Public Const SqlEncryptionEncryptionKeyValidationError As Integer = CInt(&H80048528) ' -2147187416
        Public Const SqlEncryptionIsInactiveCannotChangeEncryptionKey As Integer = CInt(&H80048527) ' -2147187417
        Public Const SqlEncryptionDeleteEncryptionKeyError As Integer = CInt(&H80048526) ' -2147187418
        Public Const SqlEncryptionIsActiveCannotRestoreEncryptionKey As Integer = CInt(&H80048525) ' -2147187419
        Public Const SqlEncryptionKeyCannotDecryptExistingData As Integer = CInt(&H80048524) ' -2147187420
        Public Const SqlEncryptionEncryptionDecryptionTestError As Integer = CInt(&H80048523) ' -2147187421
        Public Const SqlEncryptionDeleteSymmetricKeyError As Integer = CInt(&H80048522) ' -2147187422
        Public Const SqlEncryptionCreateSymmetricKeyError As Integer = CInt(&H80048521) ' -2147187423
        Public Const SqlEncryptionSymmetricKeyDoesNotExist As Integer = CInt(&H80048520) ' -2147187424
        Public Const SqlEncryptionDeleteCertificateError As Integer = CInt(&H8004851F) ' -2147187425
        Public Const SqlEncryptionCreateCertificateError As Integer = CInt(&H8004851E) ' -2147187426
        Public Const SqlEncryptionCertificateDoesNotExist As Integer = CInt(&H8004851D) ' -2147187427
        Public Const SqlEncryptionDeleteDatabaseMasterKeyError As Integer = CInt(&H8004851C) ' -2147187428
        Public Const SqlEncryptionCreateDatabaseMasterKeyError As Integer = CInt(&H8004851B) ' -2147187429
        Public Const SqlEncryptionCannotOpenSymmetricKeyBecauseDatabaseMasterKeyDoesNotExistOrIsNotOpened As Integer = CInt(&H8004851A) ' -2147187430
        Public Const SqlEncryptionDatabaseMasterKeyDoesNotExist As Integer = CInt(&H80048519) ' -2147187431
        Public Const SqlEncryption As Integer = CInt(&H80048518) ' -2147187432
        Public Const ManifestParsingFailure As Integer = CInt(&H80048534) ' -2147187404
        Public Const InvalidManifestFilePath As Integer = CInt(&H80048533) ' -2147187405
        Public Const OnPremiseRestoreOrganizationManifestFailed As Integer = CInt(&H80048532) ' -2147187406 
        Public Const InvalidAuth As Integer = CInt(&H80048516) ' -2147187434
        Public Const CannotUpdateOrgDBOrgSettingWhenOffline As Integer = CInt(&H80048515) ' -2147187435
        Public Const InvalidOrgDBOrgSetting As Integer = CInt(&H80048514) ' -2147187436
        Public Const UnknownInvalidTransformationParameterGeneric As Integer = CInt(&H80048513) ' -2147187437
        Public Const InvalidTransformationParameterOutsideRangeGeneric As Integer = CInt(&H80048512) ' -2147187438
        Public Const InvalidTransformationParameterEmptyCollection As Integer = CInt(&H80048511) ' -2147187439
        Public Const InvalidTransformationParameterOutsideRange As Integer = CInt(&H80048510) ' -2147187440
        Public Const InvalidTransformationParameterZeroToRange As Integer = CInt(&H80048509) ' -2147187447
        Public Const InvalidTransformationParameterString As Integer = CInt(&H80048508) ' -2147187448
        Public Const InvalidTransformationParametersGeneric As Integer = CInt(&H80048507) ' -2147187449
        Public Const InsufficientTransformationParameters As Integer = CInt(&H80048506) ' -2147187450
        Public Const MaximumNumberHandlersExceeded As Integer = CInt(&H80048505) ' -2147187451
        Public Const ErrorInUnzipAlternate As Integer = CInt(&H80048503) ' -2147187453
        Public Const IncorrectSingleFileMultipleEntityMap As Integer = CInt(&H80048502) ' -2147187454
        Public Const ActivityEntityCannotBeActivityParty As Integer = CInt(&H80048501) ' -2147187455
        Public Const TargetAttributeInvalidForIgnore As Integer = CInt(&H80048500) ' -2147187456
        Public Const MaxUnzipFolderSizeExceeded As Integer = CInt(&H80048499) ' -2147187559
        Public Const InvalidMultipleMapping As Integer = CInt(&H80048498) ' -2147187560
        Public Const ErrorInStoringImportFile As Integer = CInt(&H80048497) ' -2147187561
        Public Const UnzipTimeout As Integer = CInt(&H80048496) ' -2147187562
        Public Const UnsupportedZipFileForImport As Integer = CInt(&H80048495) ' -2147187563
        Public Const UnzipProcessCountLimitReached As Integer = CInt(&H80048494) ' -2147187564
        Public Const AttachmentNotFound As Integer = CInt(&H80048493) ' -2147187565
        Public Const TooManyPicklistValues As Integer = CInt(&H80048492) ' -2147187566
        Public Const VeryLargeFileInZipImport As Integer = CInt(&H80048491) ' -2147187567
        Public Const InvalidAttachmentsFolder As Integer = CInt(&H80048490) ' -2147187568
        Public Const ZipInsideZip As Integer = CInt(&H80048489) ' -2147187575
        Public Const InvalidZipFileFormat As Integer = CInt(&H80048488) ' -2147187576
        Public Const EmptyFileForImport As Integer = CInt(&H80048487) ' -2147187577
        Public Const EmptyFilesInZip As Integer = CInt(&H80048486) ' -2147187578
        Public Const ZipFileHasMixOfCsvAndXmlFiles As Integer = CInt(&H80048485) ' -2147187579
        Public Const DuplicateFileNamesInZip As Integer = CInt(&H80048484) ' -2147187580
        Public Const ErrorInUnzip As Integer = CInt(&H80048483) ' -2147187581
        Public Const InvalidZipFileForImport As Integer = CInt(&H80048482) ' -2147187582
        Public Const InvalidLookupMapNode As Integer = CInt(&H80048481) ' -2147187583
        Public Const ImportMailMergeTemplateEntityMissingError As Integer = CInt(&H80048480) ' -2147187584
        Public Const CannotUpdateOpportunityCurrency As Integer = CInt(&H80048479) ' -2147187591
        Public Const ParentRecordAlreadyExists As Integer = CInt(&H80048478) ' -2147187592
        Public Const MissingWebToLeadRedirect As Integer = CInt(&H80048477) ' -2147187593
        Public Const InvalidWebToLeadRedirect As Integer = CInt(&H80048476) ' -2147187594
        Public Const TemplateNotAllowedForInternetMarketing As Integer = CInt(&H80048475) ' -2147187595
        Public Const CopyNotAllowedForInternetMarketing As Integer = CInt(&H80048474) ' -2147187596
        Public Const MissingOrInvalidRedirectId As Integer = CInt(&H80048473) ' -2147187597
        Public Const ImportNotComplete As Integer = CInt(&H80048472) ' -2147187598
        Public Const UIDataMissingInWorkflow As Integer = CInt(&H80048471) ' -2147187599
        Public Const RefEntityRelationshipRoleRequired As Integer = CInt(&H80048470) ' -2147187600
        Public Const ImportTemplateLanguageIgnored As Integer = CInt(&H8004847a) ' -2147187590
        Public Const ImportTemplatePersonalIgnored As Integer = CInt(&H8004847b) ' -2147187589
        Public Const ImportComponentDeletedIgnored As Integer = CInt(&H8004847c) ' -2147187588
        Public Const RelationshipRoleNodeNumberInvalid As Integer = CInt(&H80048469) ' -2147187607
        Public Const AssociationRoleOrdinalInvalid As Integer = CInt(&H80048468) ' -2147187608
        Public Const RelationshipRoleMismatch As Integer = CInt(&H80048467) ' -2147187609
        Public Const ImportMapInUse As Integer = CInt(&H80048465) ' -2147187611
        Public Const PreviousOperationNotComplete As Integer = CInt(&H80048464) ' -2147187612
        Public Const TransformationResumeNotSupported As Integer = CInt(&H80048463) ' -2147187613
        Public Const CannotDisableDuplicateDetection As Integer = CInt(&H80048462) ' -2147187614
        Public Const TargetEntityNotMapped As Integer = CInt(&H80048460) ' -2147187616
        Public Const BulkDeleteChildFailure As Integer = CInt(&H80048459) ' -2147187623
        Public Const CannotRemoveNonListMember As Integer = CInt(&H80048458) ' -2147187624
        Public Const JobNameIsEmptyOrNull As Integer = CInt(&H80048457) ' -2147187625
        Public Const ImportMailMergeTemplateError As Integer = CInt(&H80048456) ' -2147187626
        Public Const ErrorsInWorkflowDefinition As Integer = CInt(&H80048455) ' -2147187627
        Public Const DistributeNoListAssociated As Integer = CInt(&H80048454) ' -2147187628
        Public Const DistributeListAssociatedVary As Integer = CInt(&H80048453) ' -2147187629
        Public Const OfflineFilterParentDownloaded As Integer = CInt(&H80048451) ' -2147187631
        Public Const OfflineFilterNestedDateTimeOR As Integer = CInt(&H80048450) ' -2147187632
        Public Const DuplicateOfflineFilter As Integer = CInt(&H80048449) ' -2147187639
        Public Const CannotAssignAddressBookFilters As Integer = CInt(&H80048448) ' -2147187640
        Public Const CannotCreateAddressBookFilters As Integer = CInt(&H80048447) ' -2147187641
        Public Const CannotGrantAccessToAddressBookFilters As Integer = CInt(&H80048446) ' -2147187642
        Public Const CannotModifyAccessToAddressBookFilters As Integer = CInt(&H80048445) ' -2147187643
        Public Const CannotRevokeAccessToAddressBookFilters As Integer = CInt(&H80048444) ' -2147187644
        Public Const DuplicateMapName As Integer = CInt(&H80048443) ' -2147187645
        Public Const InvalidWordXmlFile As Integer = CInt(&H80048441) ' -2147187647
        Public Const FileNotFound As Integer = CInt(&H80048440) ' -2147187648
        Public Const MultipleFilesFound As Integer = CInt(&H80048439) ' -2147187655
        Public Const InvalidAttributeMapping As Integer = CInt(&H80048438) ' -2147187656
        Public Const FileReadError As Integer = CInt(&H80048437) ' -2147187657
        Public Const ViewForDuplicateDetectionNotDefined As Integer = CInt(&H80048838) ' -2147186632
        Public Const FileInUse As Integer = CInt(&H80048837) ' -2147186633
        Public Const NoPublishedDuplicateDetectionRules As Integer = CInt(&H80048436) ' -2147187658
        Public Const NoEntitiesForBulkDelete As Integer = CInt(&H80048442) ' -2147187646
        Public Const BulkDeleteRecordDeletionFailure As Integer = CInt(&H80048435) ' -2147187659
        Public Const RuleAlreadyPublishing As Integer = CInt(&H80048434) ' -2147187660
        Public Const RuleNotFound As Integer = CInt(&H80048433) ' -2147187661
        Public Const CannotDeleteSystemEmailTemplate As Integer = CInt(&H80048432) ' -2147187662
        Public Const EntityDupCheckNotSupportedSystemWide As Integer = CInt(&H80048431) ' -2147187663
        Public Const DuplicateDetectionNotSupportedOnAttributeType As Integer = CInt(&H80048430) ' -2147187664
        Public Const MaxMatchCodeLengthExceeded As Integer = CInt(&H80048429) ' -2147187671
        Public Const CannotDeleteUpdateInUseRule As Integer = CInt(&H80048428) ' -2147187672
        Public Const ImportMappingsInvalidIdSpecified As Integer = CInt(&H80048427) ' -2147187673
        Public Const NotAWellFormedXml As Integer = CInt(&H80048426) ' -2147187674
        Public Const NoncompliantXml As Integer = CInt(&H80048425) ' -2147187675
        Public Const DuplicateDetectionTemplateNotFound As Integer = CInt(&H80048424) ' -2147187676
        Public Const RulesInInconsistentStateFound As Integer = CInt(&H80048423) ' -2147187677
        Public Const BulkDetectInvalidEmailRecipient As Integer = CInt(&H80048422) ' -2147187678
        Public Const CannotEnableDuplicateDetection As Integer = CInt(&H80048421) ' -2147187679
        Public Const CannotDeleteInUseEntity As Integer = CInt(&H80048420) ' -2147187680
        Public Const StringAttributeIndexError As Integer = CInt(&H8004d292) ' -2147167598
        Public Const CannotChangeAttributeRequiredLevel As Integer = CInt(&H8004d293) ' -2147167597
        Public Const MaximumNumberOfAttributesForEntityReached As Integer = CInt(&H8004841a) ' -2147187686
        Public Const CannotPublishMoreRules As Integer = CInt(&H80048419) ' -2147187687
        Public Const CannotDeleteInUseAttribute As Integer = CInt(&H80048418) ' -2147187688
        Public Const CannotDeleteInUseOptionSet As Integer = CInt(&H80048417) ' -2147187689
        Public Const InvalidEntityName As Integer = CInt(&H80048416) ' -2147187690
        Public Const InvalidOperatorCode As Integer = CInt(&H80048415) ' -2147187691
        Public Const CannotPublishEmptyRule As Integer = CInt(&H80048414) ' -2147187692
        Public Const CannotPublishInactiveRule As Integer = CInt(&H80048413) ' -2147187693
        Public Const DuplicateCheckNotEnabled As Integer = CInt(&H80048412) ' -2147187694
        Public Const DuplicateCheckNotSupportedOnEntity As Integer = CInt(&H80048410) ' -2147187696
        Public Const InvalidStateCodeStatusCode As Integer = CInt(&H80048408) ' -2147187704
        Public Const SyncToMsdeFailure As Integer = CInt(&H80048407) ' -2147187705
        Public Const FormDoesNotExist As Integer = CInt(&H80048406) ' -2147187706
        Public Const AccessDenied As Integer = CInt(&H80048405) ' -2147187707
        Public Const CannotDeleteOptionSet As Integer = CInt(&H80048404) ' -2147187708
        Public Const InvalidOptionSetOperation As Integer = CInt(&H80048403) ' -2147187709
        Public Const OptionValuePrefixOutOfRange As Integer = CInt(&H80048402) ' -2147187710
        Public Const CheckPrivilegeGroupForUserOnPremiseError As Integer = CInt(&H80048401) ' -2147187711
        Public Const CheckPrivilegeGroupForUserOnSplaError As Integer = CInt(&H80048400) ' -2147187712
        Public Const unManagedIdsAccessDenied As Integer = CInt(&H80048306) ' -2147187962
        Public Const EntityIsIntersect As Integer = CInt(&H8004830f) ' -2147187953
        Public Const CannotDeleteTeamOwningRecords As Integer = CInt(&H8004830e) ' -2147187954
        Public Const CannotRemoveMembersFromDefaultTeam As Integer = CInt(&H8004830c) ' -2147187956
        Public Const CannotAddMembersToDefaultTeam As Integer = CInt(&H8004830b) ' -2147187957
        Public Const CannotUpdateNameDefaultTeam As Integer = CInt(&H8004830a) ' -2147187958
        Public Const CannotSetParentDefaultTeam As Integer = CInt(&H80048308) ' -2147187960
        Public Const CannotDeleteDefaultTeam As Integer = CInt(&H80048307) ' -2147187961
        Public Const TeamNameTooLong As Integer = CInt(&H80048305) ' -2147187963
        Public Const CannotAssignRolesOrProfilesToAccessTeam As Integer = CInt(&H80048331) ' -2147187919
        Public Const TooManyEntitiesEnabledForAutoCreatedAccessTeams As Integer = CInt(&H80048332) ' -2147187918
        Public Const TooManyTeamTemplatesForEntityAccessTeams As Integer = CInt(&H80048333) ' -2147187917
        Public Const EntityNotEnabledForAutoCreatedAccessTeams As Integer = CInt(&H80048334) ' -2147187916
        Public Const InvalidAccessMaskForTeamTemplate As Integer = CInt(&H80048335) ' -2147187915
        Public Const CannotChangeTeamTypeDueToRoleOrProfile As Integer = CInt(&H80048336) ' -2147187914
        Public Const CannotChangeTeamTypeDueToOwnership As Integer = CInt(&H80048337) ' -2147187913
        Public Const CannotDisableAutoCreateAccessTeams As Integer = CInt(&H80048338) ' -2147187912
        Public Const CannotShareSystemManagedTeam As Integer = CInt(&H80048339) ' -2147187911
        Public Const CannotAssignToAccessTeam As Integer = CInt(&H80048340) ' -2147187904
        Public Const DuplicateSalesTeamMember As Integer = CInt(&H80048341) ' -2147187903
        Public Const TargetUserInsufficientPrivileges As Integer = CInt(&H80048342) ' -2147187902
        Public Const InvalidDomainName As Integer = CInt(&H80048015) ' -2147188715
        Public Const InvalidUserName As Integer = CInt(&H80048095) ' -2147188587
        Public Const BulkMailServiceNotAccessible As Integer = CInt(&H80048304) ' -2147187964
        Public Const RSMoveItemError As Integer = CInt(&H80048330) ' -2147187920
        Public Const ReportParentChildNotCustomizable As Integer = CInt(&H8004832f) ' -2147187921
        Public Const ConvertFetchDataSetError As Integer = CInt(&H8004832e) ' -2147187922
        Public Const ConvertReportToCrmError As Integer = CInt(&H8004832d) ' -2147187923
        Public Const ReportViewerError As Integer = CInt(&H8004832c) ' -2147187924
        Public Const RSGetItemTypeError As Integer = CInt(&H8004832b) ' -2147187925
        Public Const RSSetPropertiesError As Integer = CInt(&H8004832a) ' -2147187926
        Public Const RSReportParameterTypeMismatchError As Integer = CInt(&H80048329) ' -2147187927
        Public Const RSUpdateReportExecutionSnapshotError As Integer = CInt(&H80048328) ' -2147187928
        Public Const RSSetReportHistoryLimitError As Integer = CInt(&H80048327) ' -2147187929
        Public Const RSSetReportHistoryOptionsError As Integer = CInt(&H80048326) ' -2147187930
        Public Const RSSetExecutionOptionsError As Integer = CInt(&H80048325) ' -2147187931
        Public Const RSSetReportParametersError As Integer = CInt(&H80048324) ' -2147187932
        Public Const RSGetReportParametersError As Integer = CInt(&H80048323) ' -2147187933
        Public Const RSSetItemDataSourcesError As Integer = CInt(&H80048322) ' -2147187934
        Public Const RSGetItemDataSourcesError As Integer = CInt(&H80048321) ' -2147187935
        Public Const RSCreateBatchError As Integer = CInt(&H80048320) ' -2147187936
        Public Const RSListReportHistoryError As Integer = CInt(&H8004831f) ' -2147187937
        Public Const RSGetReportHistoryLimitError As Integer = CInt(&H8004831e) ' -2147187938
        Public Const RSExecuteBatchError As Integer = CInt(&H8004831d) ' -2147187939
        Public Const RSCancelBatchError As Integer = CInt(&H8004831c) ' -2147187940
        Public Const RSListExtensionsError As Integer = CInt(&H8004831b) ' -2147187941
        Public Const RSGetDataSourceContentsError As Integer = CInt(&H8004831a) ' -2147187942
        Public Const RSSetDataSourceContentsError As Integer = CInt(&H80048319) ' -2147187943
        Public Const RSFindItemsError As Integer = CInt(&H80048318) ' -2147187944
        Public Const RSDeleteItemError As Integer = CInt(&H80048317) ' -2147187945
        Public Const ReportSecurityError As Integer = CInt(&H80048316) ' -2147187946
        Public Const ReportMissingReportSourceError As Integer = CInt(&H80048315) ' -2147187947
        Public Const ReportMissingParameterError As Integer = CInt(&H80048314) ' -2147187948
        Public Const ReportMissingEndpointError As Integer = CInt(&H80048313) ' -2147187949
        Public Const ReportMissingDataSourceError As Integer = CInt(&H80048312) ' -2147187950
        Public Const ReportMissingDataSourceCredentialsError As Integer = CInt(&H80048311) ' -2147187951
        Public Const ReportLocalProcessingError As Integer = CInt(&H80048310) ' -2147187952
        Public Const ReportServerSP2HotFixNotApplied As Integer = CInt(&H80048309) ' -2147187959
        Public Const DataSourceProhibited As Integer = CInt(&H8004830d) ' -2147187955
        Public Const ReportServerVersionLow As Integer = CInt(&H80048303) ' -2147187965
        Public Const ReportServerNoPrivilege As Integer = CInt(&H80048302) ' -2147187966
        Public Const ReportServerInvalidUrl As Integer = CInt(&H80048301) ' -2147187967
        Public Const ReportServerUnknownException As Integer = CInt(&H80048300) ' -2147187968
        Public Const ReportNotAvailable As Integer = CInt(&H80048299) ' -2147188071
        Public Const ErrorUploadingReport As Integer = CInt(&H80048298) ' -2147188072
        Public Const ReportFileTooBig As Integer = CInt(&H80048297) ' -2147188073
        Public Const ReportFileZeroLength As Integer = CInt(&H80048296) ' -2147188074
        Public Const ReportTypeBlocked As Integer = CInt(&H80048295) ' -2147188075
        Public Const ReportUploadDisabled As Integer = CInt(&H80048294) ' -2147188076
        Public Const BothConnectionSidesAreNeeded As Integer = CInt(&H80048218) ' -2147188200
        Public Const CannotConnectToSelf As Integer = CInt(&H80048217) ' -2147188201
        Public Const UnrelatedConnectionRoles As Integer = CInt(&H80048216) ' -2147188202
        Public Const ConnectionRoleNotValidForObjectType As Integer = CInt(&H80048215) ' -2147188203
        Public Const ConnectionCannotBeEnabledOnThisEntity As Integer = CInt(&H80048214) ' -2147188204
        Public Const ConnectionNotSupported As Integer = CInt(&H80048213) ' -2147188205
        Public Const ConnectionObjectsMissing As Integer = CInt(&H80048210) ' -2147188208
        Public Const ConnectionInvalidStartEndDate As Integer = CInt(&H80048209) ' -2147188215
        Public Const ConnectionExists As Integer = CInt(&H80048208) ' -2147188216
        Public Const DecoupleUserOwnedEntity As Integer = CInt(&H80048207) ' -2147188217
        Public Const DecoupleChildEntity As Integer = CInt(&H80048206) ' -2147188218
        Public Const ExistingParentalRelationship As Integer = CInt(&H80048205) ' -2147188219
        Public Const InvalidCascadeLinkType As Integer = CInt(&H80048204) ' -2147188220
        Public Const InvalidDeleteModification As Integer = CInt(&H80048203) ' -2147188221
        Public Const CustomerOpportunityRoleExists As Integer = CInt(&H80048202) ' -2147188222
        Public Const CustomerRelationshipExists As Integer = CInt(&H80048201) ' -2147188223
        Public Const MultipleRelationshipsNotSupported As Integer = CInt(&H80048200) ' -2147188224
        Public Const ImportDuplicateEntity As Integer = CInt(&H8004810c) ' -2147188468
        Public Const CascadeProxyEmptyCallerId As Integer = CInt(&H8004810b) ' -2147188469
        Public Const CascadeProxyInvalidPrincipalType As Integer = CInt(&H8004810a) ' -2147188470
        Public Const CascadeProxyInvalidNativeDAPtr As Integer = CInt(&H80048109) ' -2147188471
        Public Const CascadeFailToCreateNativeDAWrapper As Integer = CInt(&H80048108) ' -2147188472
        Public Const CascadeReparentOnNonUserOwned As Integer = CInt(&H80048107) ' -2147188473
        Public Const CascadeMergeInvalidSpecialColumn As Integer = CInt(&H80048106) ' -2147188474
        Public Const CascadeRemoveLinkOnNonNullable As Integer = CInt(&H80048104) ' -2147188476
        Public Const CascadeDeleteNotAllowDelete As Integer = CInt(&H80048103) ' -2147188477
        Public Const CascadeInvalidLinkType As Integer = CInt(&H80048102) ' -2147188478
        Public Const IsvExtensionsPrivilegeNotPresent As Integer = CInt(&H80048029) ' -2147188695
        Public Const RelationshipNameLengthExceedsLimit As Integer = CInt(&H8004802a) ' -2147188694
        Public Const ImportEmailTemplateErrorMissingFile As Integer = CInt(&H8004802b) ' -2147188693
        Public Const CascadeInvalidExtraConditionValue As Integer = CInt(&H80048101) ' -2147188479
        Public Const ImportWorkflowNameConflictError As Integer = CInt(&H80048027) ' -2147188697
        Public Const ImportWorkflowPublishedError As Integer = CInt(&H80048028) ' -2147188696
        Public Const ImportWorkflowEntityDependencyError As Integer = CInt(&H80048023) ' -2147188701
        Public Const ImportWorkflowAttributeDependencyError As Integer = CInt(&H80048022) ' -2147188702
        Public Const ImportWorkflowError As Integer = CInt(&H80048021) ' -2147188703
        Public Const ImportGenericEntitiesError As Integer = CInt(&H80048020) ' -2147188704
        Public Const ImportRolePermissionError As Integer = CInt(&H80048018) ' -2147188712
        Public Const ImportRoleError As Integer = CInt(&H80048017) ' -2147188713
        Public Const ImportOrgSettingsError As Integer = CInt(&H80048019) ' -2147188711
        Public Const InvalidSharePointSiteCollectionUrl As Integer = CInt(&H80048052) ' -2147188654
        Public Const InvalidSiteRelativeUrlFormat As Integer = CInt(&H80048053) ' -2147188653
        Public Const InvalidRelativeUrlFormat As Integer = CInt(&H80048054) ' -2147188652
        Public Const InvalidAbsoluteUrlFormat As Integer = CInt(&H80048055) ' -2147188651
        Public Const InvalidUrlConsecutiveSlashes As Integer = CInt(&H80048056) ' -2147188650
        Public Const SharePointRecordWithDuplicateUrl As Integer = CInt(&H80048057) ' -2147188649
        Public Const SharePointAbsoluteAndRelativeUrlEmpty As Integer = CInt(&H80048149) ' -2147188407
        Public Const ImportOptionSetsError As Integer = CInt(&H80048030) ' -2147188688
        Public Const ImportRibbonsError As Integer = CInt(&H80048031) ' -2147188687
        Public Const ImportReportsError As Integer = CInt(&H80048032) ' -2147188686
        Public Const ImportSolutionError As Integer = CInt(&H80048033) ' -2147188685
        Public Const ImportDependencySolutionError As Integer = CInt(&H80048034) ' -2147188684
        Public Const ExportSolutionError As Integer = CInt(&H80048035) ' -2147188683
        Public Const ExportManagedSolutionError As Integer = CInt(&H80048036) ' -2147188682
        Public Const ExportMissingSolutionError As Integer = CInt(&H80048037) ' -2147188681
        Public Const ImportSolutionManagedError As Integer = CInt(&H80048038) ' -2147188680
        Public Const ImportOptionSetAttributeError As Integer = CInt(&H80048039) ' -2147188679
        Public Const ImportSolutionManagedToUnmanagedMismatch As Integer = CInt(&H80048040) ' -2147188672
        Public Const ImportSolutionUnmanagedToManagedMismatch As Integer = CInt(&H80048041) ' -2147188671
        Public Const ImportSolutionIsvConfigWarning As Integer = CInt(&H80048042) ' -2147188670
        Public Const ImportSolutionSiteMapWarning As Integer = CInt(&H80048043) ' -2147188669
        Public Const ImportSolutionOrganizationSettingsWarning As Integer = CInt(&H80048044) ' -2147188668
        Public Const ImportExportDeprecatedError As Integer = CInt(&H80048045) ' -2147188667
        Public Const ImportSystemSolutionError As Integer = CInt(&H80048046) ' -2147188666
        Public Const ImportTranslationMissingSolutionError As Integer = CInt(&H80048047) ' -2147188665
        Public Const ExportDefaultAsPackagedError As Integer = CInt(&H80048048) ' -2147188664
        Public Const ImportDefaultAsPackageError As Integer = CInt(&H80048049) ' -2147188663
        Public Const ImportCustomizationsBadZipFileError As Integer = CInt(&H80048060) ' -2147188640
        Public Const ImportTranslationsBadZipFileError As Integer = CInt(&H80048061) ' -2147188639
        Public Const ImportAttributeNameError As Integer = CInt(&H80048062) ' -2147188638
        Public Const ImportFieldSecurityProfileIsSecuredMissingError As Integer = CInt(&H80048063) ' -2147188637
        Public Const ImportFieldSecurityProfileAttributesMissingError As Integer = CInt(&H80048064) ' -2147188636
        Public Const ImportFileSignatureInvalid As Integer = CInt(&H80048065) ' -2147188635
        Public Const ImportSolutionPackageNotValid As Integer = CInt(&H80048066) ' -2147188634
        Public Const ImportSolutionPackageNeedsUpgrade As Integer = CInt(&H80048067) ' -2147188633
        Public Const ImportSolutionPackageInvalidSolutionPackageVersion As Integer = CInt(&H80048068) ' -2147188632
        Public Const ImportSolutionPackageMinimumVersionNeeded As Integer = CInt(&H1) ' 1
        Public Const ImportSolutionPackageRequiresOptInAvailable As Integer = CInt(&H80048069)  ' -2147188631
		Public Const ImportSolutionPackageRequiresOptInNotAvailable As Integer = CInt(&H8004806A) ' -2147188630
		Public Const ImportSdkMessagesError As Integer = CInt(&H80048016) ' -2147188714
        Public Const ImportEmailTemplatePersonalError As Integer = CInt(&H80048014) ' -2147188716
        Public Const ImportNonWellFormedFileError As Integer = CInt(&H80048013) ' -2147188717
        Public Const ImportPluginTypesError As Integer = CInt(&H80048012) ' -2147188718
        Public Const ImportSiteMapError As Integer = CInt(&H80048011) ' -2147188719
        Public Const ImportMappingsMissingEntityMapError As Integer = CInt(&H80048010) ' -2147188720
        Public Const ImportMappingsSystemMapError As Integer = CInt(&H8004800f) ' -2147188721
        Public Const ImportIsvConfigError As Integer = CInt(&H8004800e) ' -2147188722
        Public Const ImportArticleTemplateError As Integer = CInt(&H8004800d) ' -2147188723
        Public Const ImportEmailTemplateError As Integer = CInt(&H8004800c) ' -2147188724
        Public Const ImportContractTemplateError As Integer = CInt(&H8004800b) ' -2147188725
        Public Const ImportRelationshipRoleMapsError As Integer = CInt(&H8004800a) ' -2147188726
        Public Const ImportRelationshipRolesError As Integer = CInt(&H80048009) ' -2147188727
        Public Const ImportRelationshipRolesPrivilegeError As Integer = CInt(&H8004802f) ' -2147188689
        Public Const ImportEntityNameMismatchError As Integer = CInt(&H80048008) ' -2147188728
        Public Const ImportFormXmlError As Integer = CInt(&H80048007) ' -2147188729
        Public Const ImportFieldXmlError As Integer = CInt(&H80048006) ' -2147188730
        Public Const ImportSavedQueryExistingError As Integer = CInt(&H80048005) ' -2147188731
        Public Const ImportSavedQueryOtcMismatchError As Integer = CInt(&H80048004) ' -2147188732
        Public Const ImportEntityCustomResourcesNewStringError As Integer = CInt(&H80048003) ' -2147188733
        Public Const ImportEntityCustomResourcesError As Integer = CInt(&H80048002) ' -2147188734
        Public Const ImportEntityIconError As Integer = CInt(&H80048001) ' -2147188735
        Public Const ImportSavedQueryDeletedError As Integer = CInt(&H8004801b) ' -2147188709
        Public Const ImportEntitySystemUserOnPremiseMismatchError As Integer = CInt(&H80048024) ' -2147188700
        Public Const ImportEntitySystemUserLiveMismatchError As Integer = CInt(&H80048025) ' -2147188699
        Public Const ImportLanguagesIgnoredError As Integer = CInt(&H80048026) ' -2147188698
        Public Const ImportInvalidFileError As Integer = CInt(&H80048000) ' -2147188736
        Public Const ImportXsdValidationError As Integer = CInt(&H8004801a) ' -2147188710
        Public Const ImportInvalidXmlError As Integer = CInt(&H8004802c) ' -2147188692
        Public Const ImportWrongPublisherError As Integer = CInt(&H8004801c) ' -2147188708
        Public Const ImportMissingDependenciesError As Integer = CInt(&H8004801d) ' -2147188707
        Public Const ImportGenericError As Integer = CInt(&H8004801e) ' -2147188706
        Public Const ImportMissingComponent As Integer = CInt(&H8004801f) ' -2147188705
        Public Const ImportMissingRootComponentEntry As Integer = CInt(&H8004803a) ' -2147188678
        Public Const UnmanagedComponentParentsManagedComponent As Integer = CInt(&H8004803b) ' -2147188677
        Public Const FailedToGetNetworkServiceName As Integer = CInt(&H80047103) ' -2147192573
        Public Const CustomParentingSystemNotSupported As Integer = CInt(&H80047102) ' -2147192574
        Public Const InvalidFormatParameters As Integer = CInt(&H80047101) ' -2147192575
        Public Const DuplicatePrimaryNameAttribute As Integer = CInt(&H8004701E) ' -2147192802
        Public Const ConfigurationPageNotValidForSolution As Integer = CInt(&H8004701d) ' -2147192803
        Public Const SolutionConfigurationPageMustBeHtmlWebResource As Integer = CInt(&H8004701c) ' -2147192804
        Public Const InvalidSolutionConfigurationPage As Integer = CInt(&H8004701b) ' -2147192805
        Public Const InvalidLanguageForSolution As Integer = CInt(&H80047019) ' -2147192807
        Public Const CannotHaveDuplicateYomi As Integer = CInt(&H80047018) ' -2147192808
        Public Const SavedQueryIsNotCustomizable As Integer = CInt(&H80047017) ' -2147192809
        Public Const CannotDeleteChildAttribute As Integer = CInt(&H80047016) ' -2147192810
        Public Const EntityHasNoStateCode As Integer = CInt(&H80047015) ' -2147192811
        Public Const NoAttributesForEntityCreate As Integer = CInt(&H80047014) ' -2147192812
        Public Const DuplicateAttributeSchemaName As Integer = CInt(&H80047013) ' -2147192813
        Public Const DuplicateDisplayCollectionName As Integer = CInt(&H80047012) ' -2147192814
        Public Const DuplicateDisplayName As Integer = CInt(&H80047011) ' -2147192815
        Public Const DuplicateName As Integer = CInt(&H80047010) ' -2147192816
        Public Const InvalidRelationshipType As Integer = CInt(&H8004700f) ' -2147192817
        Public Const InvalidPrimaryFieldType As Integer = CInt(&H8004700e) ' -2147192818
        Public Const InvalidOwnershipTypeMask As Integer = CInt(&H8004700d) ' -2147192819
        Public Const InvalidDisplayName As Integer = CInt(&H8004700c) ' -2147192820
        Public Const InvalidSchemaName As Integer = CInt(&H8004700b) ' -2147192821
        Public Const RelationshipIsNotCustomRelationship As Integer = CInt(&H8004700a) ' -2147192822
        Public Const AttributeIsNotCustomAttribute As Integer = CInt(&H80047009) ' -2147192823
        Public Const EntityIsNotCustomizable As Integer = CInt(&H80047008) ' -2147192824
        Public Const MultipleParentsNotSupported As Integer = CInt(&H80047007) ' -2147192825
        Public Const CannotCreateActivityRelationship As Integer = CInt(&H80047006) ' -2147192826
        Public Const CyclicalRelationship As Integer = CInt(&H80047004) ' -2147192828
        Public Const InvalidRelationshipDescription As Integer = CInt(&H80047003) ' -2147192829
        Public Const CannotDeletePrimaryUIAttribute As Integer = CInt(&H80047002) ' -2147192830
        Public Const RowGuidIsNotValidName As Integer = CInt(&H80047001) ' -2147192831
        Public Const FailedToScheduleActivity As Integer = CInt(&H80047000) ' -2147192832
        Public Const CannotDeleteLastEmailAttribute As Integer = CInt(&H80046fff) ' -2147192833
        Public Const SystemAttributeMap As Integer = CInt(&H80046205) ' -2147196411
        Public Const UpdateAttributeMap As Integer = CInt(&H80046204) ' -2147196412
        Public Const InvalidAttributeMap As Integer = CInt(&H80046203) ' -2147196413
        Public Const SystemEntityMap As Integer = CInt(&H80046202) ' -2147196414
        Public Const UpdateEntityMap As Integer = CInt(&H80046201) ' -2147196415
        Public Const NonMappableEntity As Integer = CInt(&H80046200) ' -2147196416
        Public Const unManagedidsCalloutException As Integer = CInt(&H80045f05) ' -2147197179
        Public Const unManagedidscalloutinvalidevent As Integer = CInt(&H80045f04) ' -2147197180
        Public Const unManagedidscalloutinvalidconfig As Integer = CInt(&H80045f03) ' -2147197181
        Public Const unManagedidscalloutisvstop As Integer = CInt(&H80045f02) ' -2147197182
        Public Const unManagedidscalloutisvabort As Integer = CInt(&H80045f01) ' -2147197183
        Public Const unManagedidscalloutisvexception As Integer = CInt(&H80045f00) ' -2147197184
        Public Const unManagedidscustomentityambiguousrelationship As Integer = CInt(&H8004590d) ' -2147198707
        Public Const unManagedidscustomentitynorelationship As Integer = CInt(&H8004590c) ' -2147198708
        Public Const unManagedidscustomentityparentchildidentical As Integer = CInt(&H8004590b) ' -2147198709
        Public Const unManagedidscustomentityinvalidparent As Integer = CInt(&H8004590a) ' -2147198710
        Public Const unManagedidscustomentityinvalidchild As Integer = CInt(&H80045909) ' -2147198711
        Public Const unManagedidscustomentitywouldcreateloop As Integer = CInt(&H80045908) ' -2147198712
        Public Const unManagedidscustomentityexistingloop As Integer = CInt(&H80045907) ' -2147198713
        Public Const unManagedidscustomentitystackunderflow As Integer = CInt(&H80045906) ' -2147198714
        Public Const unManagedidscustomentitystackoverflow As Integer = CInt(&H80045905) ' -2147198715
        Public Const unManagedidscustomentitytlsfailure As Integer = CInt(&H80045904) ' -2147198716
        Public Const unManagedidscustomentityinvalidownership As Integer = CInt(&H80045903) ' -2147198717
        Public Const unManagedidscustomentitynotinitialized As Integer = CInt(&H80045902) ' -2147198718
        Public Const unManagedidscustomentityalreadyinitialized As Integer = CInt(&H80045901) ' -2147198719
        Public Const unManagedidscustomentitynameviolation As Integer = CInt(&H80045900) ' -2147198720
        Public Const unManagedidscascadeunexpectederror As Integer = CInt(&H80045603) ' -2147199485
        Public Const unManagedidscascadeemptylinkerror As Integer = CInt(&H80045602) ' -2147199486
        Public Const unManagedidscascadeundefinedrelationerror As Integer = CInt(&H80045601) ' -2147199487
        Public Const unManagedidscascadeinconsistencyerror As Integer = CInt(&H80045600) ' -2147199488
        Public Const MergeLossOfParentingWarning As Integer = CInt(&H80045317) ' -2147200233
        Public Const MergeDifferentlyParentedWarning As Integer = CInt(&H80045316) ' -2147200234
        Public Const MergeEntitiesIdenticalError As Integer = CInt(&H80045305) ' -2147200251
        Public Const MergeEntityNotActiveError As Integer = CInt(&H80045304) ' -2147200252
        Public Const unManagedidsmergedifferentbizorgerror As Integer = CInt(&H80045303) ' -2147200253
        Public Const MergeActiveQuoteError As Integer = CInt(&H80045302) ' -2147200254
        Public Const MergeSecurityError As Integer = CInt(&H80045301) ' -2147200255
        Public Const MergeCyclicalParentingError As Integer = CInt(&H80045300) ' -2147200256
        Public Const unManagedidscalendarruledoesnotexist As Integer = CInt(&H80045100) ' -2147200768
        Public Const unManagedidscalendarinvalidcalendar As Integer = CInt(&H80044d00) ' -2147201792
        Public Const AttachmentInvalidFileName As Integer = CInt(&H80044a08) ' -2147202552
        Public Const unManagedidsattachmentcannottruncatetempfile As Integer = CInt(&H80044a07) ' -2147202553
        Public Const unManagedidsattachmentcannotunmaptempfile As Integer = CInt(&H80044a06) ' -2147202554
        Public Const unManagedidsattachmentcannotcreatetempfile As Integer = CInt(&H80044a05) ' -2147202555
        Public Const unManagedidsattachmentisempty As Integer = CInt(&H80044a04) ' -2147202556
        Public Const unManagedidsattachmentcannotreadtempfile As Integer = CInt(&H80044a03) ' -2147202557
        Public Const unManagedidsattachmentinvalidfilesize As Integer = CInt(&H80044a02) ' -2147202558
        Public Const unManagedidsattachmentcannotgetfilesize As Integer = CInt(&H80044a01) ' -2147202559
        Public Const unManagedidsattachmentcannotopentempfile As Integer = CInt(&H80044a00) ' -2147202560
        Public Const unManagedidscustomizationtransformationnotsupported As Integer = CInt(&H80044700) ' -2147203328
        Public Const ContractDetailDiscountAmountAndPercent As Integer = CInt(&H80044414) ' -2147204076
        Public Const ContractDetailDiscountAmount As Integer = CInt(&H80044413) ' -2147204077
        Public Const ContractDetailDiscountPercent As Integer = CInt(&H80044412) ' -2147204078
        Public Const IncidentIsAlreadyClosedOrCancelled As Integer = CInt(&H80044411) ' -2147204079
        Public Const unManagedidsincidentparentaccountandparentcontactnotpresent As Integer = CInt(&H80044410) ' -2147204080
        Public Const unManagedidsincidentparentaccountandparentcontactpresent As Integer = CInt(&H8004440f) ' -2147204081
        Public Const IncidentCannotCancel As Integer = CInt(&H8004440e) ' -2147204082
        Public Const IncidentInvalidContractLineStateForCreate As Integer = CInt(&H8004440d) ' -2147204083
        Public Const IncidentNullSpentTimeOrBilled As Integer = CInt(&H8004440c) ' -2147204084
        Public Const IncidentInvalidAllotmentType As Integer = CInt(&H8004440b) ' -2147204085
        Public Const unManagedidsincidentcannotclose As Integer = CInt(&H8004440a) ' -2147204086
        Public Const IncidentMissingActivityRegardingObject As Integer = CInt(&H80044409) ' -2147204087
        Public Const unManagedidsincidentmissingactivityobjecttype As Integer = CInt(&H80044408) ' -2147204088
        Public Const unManagedidsincidentnullactivitytypecode As Integer = CInt(&H80044407) ' -2147204089
        Public Const unManagedidsincidentinvalidactivitytypecode As Integer = CInt(&H80044406) ' -2147204090
        Public Const unManagedidsincidentassociatedactivitycorrupted As Integer = CInt(&H80044405) ' -2147204091
        Public Const unManagedidsincidentinvalidstate As Integer = CInt(&H80044404) ' -2147204092
        Public Const IncidentContractDoesNotHaveAllotments As Integer = CInt(&H80044403) ' -2147204093
        Public Const unManagedidsincidentcontractdetaildoesnotmatchcontract As Integer = CInt(&H80044402) ' -2147204094
        Public Const IncidentMissingContractDetail As Integer = CInt(&H80044401) ' -2147204095
        Public Const IncidentInvalidContractStateForCreate As Integer = CInt(&H80044400) ' -2147204096
        Public Const DisabledCRMGoingOffline As Integer = CInt(&H80044200) ' -2147204608
        Public Const DisabledCRMGoingOnline As Integer = CInt(&H80044201) ' -2147204607
        Public Const DisabledCRMAddinLoadFailure As Integer = CInt(&H80044202) ' -2147204606
        Public Const DisabledCRMClientVersionLower As Integer = CInt(&H80044203) ' -2147204605
        Public Const DisabledCRMClientVersionHigher As Integer = CInt(&H80044204) ' -2147204604
        Public Const DisabledCRMPostOfflineUpgrade As Integer = CInt(&H80044205) ' -2147204603
        Public Const DisabledCRMOnlineCrmNotAvailable As Integer = CInt(&H80044206) ' -2147204602
        Public Const GoOfflineMetadataVersionsMismatch As Integer = CInt(&H80044220) ' -2147204576
        Public Const GoOfflineGetBCPFileException As Integer = CInt(&H80044221) ' -2147204575
        Public Const GoOfflineDbSizeLimit As Integer = CInt(&H80044222) ' -2147204574
        Public Const GoOfflineServerFailedGenerateBCPFile As Integer = CInt(&H80044223) ' -2147204573
        Public Const GoOfflineBCPFileSize As Integer = CInt(&H80044224) ' -2147204572
        Public Const GoOfflineFailedMoveData As Integer = CInt(&H80044225) ' -2147204571
        Public Const GoOfflineFailedPrepareMsde As Integer = CInt(&H80044226) ' -2147204570
        Public Const GoOfflineFailedReloadMetadataCache As Integer = CInt(&H80044227) ' -2147204569
        Public Const DoNotTrackItem As Integer = CInt(&H80044228) ' -2147204568
        Public Const GoOfflineFileWasDeleted As Integer = CInt(&H80044229) ' -2147204567
        Public Const GoOfflineEmptyFileForDelete As Integer = CInt(&H80044230) ' -2147204560
        Public Const OrganizationUIDeprecated As Integer = CInt(&H80044159) ' -2147204775
        Public Const unManagedidssqltimeouterror As Integer = CInt(&H80044151) ' -2147204783
        Public Const unManagedidssqlerror As Integer = CInt(&H80044150) ' -2147204784
        Public Const unManagedidsrcsyncinvalidfiltererror As Integer = CInt(&H8004410d) ' -2147204851
        Public Const unManagedidsrcsyncnotprimary As Integer = CInt(&H80044111) ' -2147204847
        Public Const unManagedidsrcsyncnoprimary As Integer = CInt(&H80044112) ' -2147204846
        Public Const unManagedidsrcsyncnoclient As Integer = CInt(&H80044113) ' -2147204845
        Public Const unManagedidsrcsyncmethodnone As Integer = CInt(&H80044114) ' -2147204844
        Public Const unManagedidsrcsyncfilternoaccess As Integer = CInt(&H8004410f) ' -2147204849
        Public Const InvalidOfflineOperation As Integer = CInt(&H8004410e) ' -2147204850
        Public Const unManagedidsrcsyncsqlgenericerror As Integer = CInt(&H80044110) ' -2147204848
        Public Const unManagedidsrcsyncsqlpausederror As Integer = CInt(&H8004410c) ' -2147204852
        Public Const unManagedidsrcsyncsqlstoppederror As Integer = CInt(&H8004410b) ' -2147204853
        Public Const unManagedidsrcsyncsubscriptionowner As Integer = CInt(&H8004410a) ' -2147204854
        Public Const unManagedidsrcsyncinvalidsubscription As Integer = CInt(&H80044109) ' -2147204855
        Public Const unManagedidsrcsyncsoapparseerror As Integer = CInt(&H80044108) ' -2147204856
        Public Const unManagedidsrcsyncsoapreaderror As Integer = CInt(&H80044107) ' -2147204857
        Public Const unManagedidsrcsyncsoapfaulterror As Integer = CInt(&H80044106) ' -2147204858
        Public Const unManagedidsrcsyncsoapservererror As Integer = CInt(&H80044105) ' -2147204859
        Public Const unManagedidsrcsyncsoapsendfailed As Integer = CInt(&H80044104) ' -2147204860
        Public Const unManagedidsrcsyncsoapconnfailed As Integer = CInt(&H80044103) ' -2147204861
        Public Const unManagedidsrcsyncsoapgenfailed As Integer = CInt(&H80044102) ' -2147204862
        Public Const unManagedidsrcsyncmsxmlfailed As Integer = CInt(&H80044101) ' -2147204863
        Public Const unManagedidsrcsyncinvalidsynctime As Integer = CInt(&H80044100) ' -2147204864
        Public Const AttachmentBlocked As Integer = CInt(&H80043e09) ' -2147205623
        Public Const unManagedidsarticletemplateisnotactive As Integer = CInt(&H80043e07) ' -2147205625
        Public Const unManagedidsfulltextoperationfailed As Integer = CInt(&H80043e06) ' -2147205626
        Public Const unManagedidsarticletemplatecontainsarticles As Integer = CInt(&H80043e05) ' -2147205627
        Public Const unManagedidsqueueorganizationidnotmatch As Integer = CInt(&H80043e04) ' -2147205628
        Public Const unManagedidsqueuemissingbusinessunitid As Integer = CInt(&H80043e03) ' -2147205629
        Public Const SubjectDoesNotExist As Integer = CInt(&H80043e02) ' -2147205630
        Public Const SubjectLoopBeingCreated As Integer = CInt(&H80043e01) ' -2147205631
        Public Const SubjectLoopExists As Integer = CInt(&H80043e00) ' -2147205632
        Public Const InvalidSubmitFromUnapprovedArticle As Integer = CInt(&H80048dff) ' -2147185153
        Public Const InvalidUnpublishFromUnapprovedArticle As Integer = CInt(&H80048dfe) ' -2147185154
        Public Const InvalidApproveFromDraftArticle As Integer = CInt(&H80048dfd) ' -2147185155
        Public Const InvalidUnpublishFromDraftArticle As Integer = CInt(&H80048dfc) ' -2147185156
        Public Const InvalidApproveFromPublishedArticle As Integer = CInt(&H80048dfb) ' -2147185157
        Public Const InvalidSubmitFromPublishedArticle As Integer = CInt(&H80048dfa) ' -2147185158
        Public Const QuoteReviseExistingActiveQuote As Integer = CInt(&H80048d00) ' -2147185408
        Public Const BaseCurrencyNotDeletable As Integer = CInt(&H80048cff) ' -2147185409
        Public Const CannotDeleteBaseMoneyCalculationAttribute As Integer = CInt(&H80048cfe) ' -2147185410
        Public Const InvalidExchangeRate As Integer = CInt(&H80048cfd) ' -2147185411
        Public Const InvalidCurrency As Integer = CInt(&H80048cfc) ' -2147185412
        Public Const CurrencyCannotBeNullDueToNonNullMoneyFields As Integer = CInt(&H80048cfb) ' -2147185413
        Public Const CannotUpdateProductCurrency As Integer = CInt(&H80048cfa) ' -2147185414
        Public Const InvalidPriceLevelCurrencyForPricingMethod As Integer = CInt(&H80048cf9) ' -2147185415
        Public Const DiscountTypeAndPriceLevelCurrencyNotEqual As Integer = CInt(&H80048cf8) ' -2147185416
        Public Const CurrencyRequiredForDiscountTypeAmount As Integer = CInt(&H80048cf7) ' -2147185417
        Public Const RecordAndPricelistCurrencyNotEqual As Integer = CInt(&H80048cf6) ' -2147185418
        Public Const ExchangeRateOfBaseCurrencyNotUpdatable As Integer = CInt(&H80048cf5) ' -2147185419
        Public Const BaseCurrencyCannotBeDeactivated As Integer = CInt(&H80048cf4) ' -2147185420
        Public Const DuplicateIsoCurrencyCode As Integer = CInt(&H80048cf3) ' -2147185421
        Public Const InvalidIsoCurrencyCode As Integer = CInt(&H80048cf2) ' -2147185422
        Public Const PercentageDiscountCannotHaveCurrency As Integer = CInt(&H80048cf1) ' -2147185423
        Public Const RecordAndOpportunityCurrencyNotEqual As Integer = CInt(&H80048cef) ' -2147185425
        Public Const QuoteAndSalesOrderCurrencyNotEqual As Integer = CInt(&H80048cee) ' -2147185426
        Public Const SalesOrderAndInvoiceCurrencyNotEqual As Integer = CInt(&H80048ced) ' -2147185427
        Public Const BaseCurrencyOverflow As Integer = CInt(&H80048cec) ' -2147185428
        Public Const BaseCurrencyUnderflow As Integer = CInt(&H80048ceb) ' -2147185429
        Public Const CurrencyNotEqual As Integer = CInt(&H80048cea) ' -2147185430
        Public Const UnitNoName As Integer = CInt(&H80043b26) ' -2147206362
        Public Const unManagedidsinvoicecloseapideprecated As Integer = CInt(&H80043b25) ' -2147206363
        Public Const ProductDoesNotExist As Integer = CInt(&H80043b24) ' -2147206364
        Public Const ProductKitLoopBeingCreated As Integer = CInt(&H80043b23) ' -2147206365
        Public Const ProductKitLoopExists As Integer = CInt(&H80043b22) ' -2147206366
        Public Const DiscountPercent As Integer = CInt(&H80043b21) ' -2147206367
        Public Const DiscountAmount As Integer = CInt(&H80043b20) ' -2147206368
        Public Const DiscountAmountAndPercent As Integer = CInt(&H80043b1f) ' -2147206369
        Public Const EntityIsUnlocked As Integer = CInt(&H80043b1e) ' -2147206370
        Public Const EntityIsLocked As Integer = CInt(&H80043b1d) ' -2147206371
        Public Const BaseUnitDoesNotExist As Integer = CInt(&H80043b1c) ' -2147206372
        Public Const UnitDoesNotExist As Integer = CInt(&H80043b1b) ' -2147206373
        Public Const UnitLoopBeingCreated As Integer = CInt(&H80043b1a) ' -2147206374
        Public Const UnitLoopExists As Integer = CInt(&H80043b19) ' -2147206375
        Public Const QuantityReadonly As Integer = CInt(&H80043b18) ' -2147206376
        Public Const BaseUnitNotNull As Integer = CInt(&H80043b17) ' -2147206377
        Public Const UnitNotInSchedule As Integer = CInt(&H80043b16) ' -2147206378
        Public Const MissingOpportunityId As Integer = CInt(&H80043b15) ' -2147206379
        Public Const ProductInvalidUnit As Integer = CInt(&H80043b14) ' -2147206380
        Public Const ProductMissingUomSheduleId As Integer = CInt(&H80043b13) ' -2147206381
        Public Const MissingPriceLevelId As Integer = CInt(&H80043b12) ' -2147206382
        Public Const MissingProductId As Integer = CInt(&H80043b11) ' -2147206383
        Public Const InvalidPricePerUnit As Integer = CInt(&H80043b10) ' -2147206384
        Public Const PriceLevelNameExists As Integer = CInt(&H80043b0f) ' -2147206385
        Public Const PriceLevelNoName As Integer = CInt(&H80043b0e) ' -2147206386
        Public Const MissingUomId As Integer = CInt(&H80043b0d) ' -2147206387
        Public Const ProductInvalidPriceLevelPercentage As Integer = CInt(&H80043b0c) ' -2147206388
        Public Const InvalidBaseUnit As Integer = CInt(&H80043b0b) ' -2147206389
        Public Const MissingUomScheduleId As Integer = CInt(&H80043b0a) ' -2147206390
        Public Const ParentReadOnly As Integer = CInt(&H80043b09) ' -2147206391
        Public Const DuplicateProductPriceLevel As Integer = CInt(&H80043b08) ' -2147206392
        Public Const ProductInvalidQuantityDecimal As Integer = CInt(&H80043b07) ' -2147206393
        Public Const ProductProductNumberExists As Integer = CInt(&H80043b06) ' -2147206394
        Public Const ProductNoProductNumber As Integer = CInt(&H80043b05) ' -2147206395
        Public Const unManagedidscannotdeactivatepricelevel As Integer = CInt(&H80043b04) ' -2147206396
        Public Const BaseUnitNotDeletable As Integer = CInt(&H80043b03) ' -2147206397
        Public Const DiscountRangeOverlap As Integer = CInt(&H80043b02) ' -2147206398
        Public Const LowQuantityGreaterThanHighQuantity As Integer = CInt(&H80043b01) ' -2147206399
        Public Const LowQuantityLessThanZero As Integer = CInt(&H80043b00) ' -2147206400
        Public Const InvalidSubstituteProduct As Integer = CInt(&H80043aff) ' -2147206401
        Public Const InvalidKitProduct As Integer = CInt(&H80043afe) ' -2147206402
        Public Const InvalidKit As Integer = CInt(&H80043afd) ' -2147206403
        Public Const InvalidQuantityDecimalCode As Integer = CInt(&H80043afc) ' -2147206404
        Public Const CannotSpecifyBothProductAndProductDesc As Integer = CInt(&H80043afb) ' -2147206405
        Public Const CannotSpecifyBothUomAndProductDesc As Integer = CInt(&H80043afa) ' -2147206406
        Public Const unManagedidsstatedoesnotexist As Integer = CInt(&H80043af9) ' -2147206407
        Public Const FiscalSettingsAlreadyUpdated As Integer = CInt(&H80043809) ' -2147207159
        Public Const unManagedidssalespeopleinvalidfiscalcalendartype As Integer = CInt(&H80043808) ' -2147207160
        Public Const unManagedidssalespeopleinvalidfiscalperiodindex As Integer = CInt(&H80043807) ' -2147207161
        Public Const SalesPeopleManagerNotAllowed As Integer = CInt(&H80043805) ' -2147207163
        Public Const unManagedidssalespeopleinvalidterritoryobjecttype As Integer = CInt(&H80043804) ' -2147207164
        Public Const SalesPeopleDuplicateCalendarNotAllowed As Integer = CInt(&H80043803) ' -2147207165
        Public Const unManagedidssalespeopleduplicatecalendarfound As Integer = CInt(&H80043802) ' -2147207166
        Public Const SalesPeopleEmptyEffectiveDate As Integer = CInt(&H80043801) ' -2147207167
        Public Const SalesPeopleEmptySalesPerson As Integer = CInt(&H80043800) ' -2147207168
        Public Const InvalidNumberGroupFormat As Integer = CInt(&H80043700) ' -2147207424
        Public Const BaseUomNameNotSpecified As Integer = CInt(&H80043810) ' -2147207152
        Public Const InvalidActivityPartyAddress As Integer = CInt(&H80043518) ' -2147207912
        Public Const FaxNoSupport As Integer = CInt(&H80043517) ' -2147207913
        Public Const FaxNoData As Integer = CInt(&H80043516) ' -2147207914
        Public Const InvalidPartyMapping As Integer = CInt(&H80043515) ' -2147207915
        Public Const InvalidActivityXml As Integer = CInt(&H80043514) ' -2147207916
        Public Const ActivityInvalidObjectTypeCode As Integer = CInt(&H80043513) ' -2147207917
        Public Const ActivityInvalidSessionToken As Integer = CInt(&H80043512) ' -2147207918
        Public Const FaxServiceNotRunning As Integer = CInt(&H80043511) ' -2147207919
        Public Const FaxSendBlocked As Integer = CInt(&H80043510) ' -2147207920
        Public Const NoDialNumber As Integer = CInt(&H8004350f) ' -2147207921
        Public Const TooManyRecipients As Integer = CInt(&H8004350e) ' -2147207922
        Public Const MissingRecipient As Integer = CInt(&H8004350d) ' -2147207923
        Public Const unManagedidsactivitynotroutable As Integer = CInt(&H8004350b) ' -2147207925
        Public Const unManagedidsactivitydurationdoesnotmatch As Integer = CInt(&H8004350a) ' -2147207926
        Public Const unManagedidsactivityinvalidduration As Integer = CInt(&H80043509) ' -2147207927
        Public Const unManagedidsactivityinvalidtimeformat As Integer = CInt(&H80043508) ' -2147207928
        Public Const unManagedidsactivityinvalidregardingobject As Integer = CInt(&H80043507) ' -2147207929
        Public Const ActivityPartyObjectTypeNotAllowed As Integer = CInt(&H80043506) ' -2147207930
        Public Const unManagedidsactivityinvalidpartyobjecttype As Integer = CInt(&H80043505) ' -2147207931
        Public Const unManagedidsactivitypartyobjectidortypemissing As Integer = CInt(&H80043504) ' -2147207932
        Public Const unManagedidsactivityinvalidobjecttype As Integer = CInt(&H80043503) ' -2147207933
        Public Const unManagedidsactivityobjectidortypemissing As Integer = CInt(&H80043502) ' -2147207934
        Public Const unManagedidsactivityinvalidtype As Integer = CInt(&H80043501) ' -2147207935
        Public Const unManagedidsactivityinvalidstate As Integer = CInt(&H80043500) ' -2147207936
        Public Const ContractInvalidDatesForRenew As Integer = CInt(&H80043218) ' -2147208680
        Public Const unManagedidscontractinvalidstartdateforrenewedcontract As Integer = CInt(&H80043217) ' -2147208681
        Public Const unManagedidscontracttemplateabbreviationexists As Integer = CInt(&H80043216) ' -2147208682
        Public Const ContractInvalidPrice As Integer = CInt(&H80043215) ' -2147208683
        Public Const unManagedidscontractinvalidtotalallotments As Integer = CInt(&H80043214) ' -2147208684
        Public Const ContractInvalidContract As Integer = CInt(&H80043213) ' -2147208685
        Public Const unManagedidscontractinvalidowner As Integer = CInt(&H80043212) ' -2147208686
        Public Const ContractInvalidContractTemplate As Integer = CInt(&H80043211) ' -2147208687
        Public Const ContractInvalidBillToCustomer As Integer = CInt(&H80043210) ' -2147208688
        Public Const ContractInvalidBillToAddress As Integer = CInt(&H8004320f) ' -2147208689
        Public Const ContractInvalidServiceAddress As Integer = CInt(&H8004320e) ' -2147208690
        Public Const ContractInvalidCustomer As Integer = CInt(&H8004320d) ' -2147208691
        Public Const ContractNoLineItems As Integer = CInt(&H8004320c) ' -2147208692
        Public Const ContractTemplateNoAbbreviation As Integer = CInt(&H8004320b) ' -2147208693
        Public Const unManagedidscontractopencasesexist As Integer = CInt(&H8004320a) ' -2147208694
        Public Const unManagedidscontractlineitemdoesnotexist As Integer = CInt(&H80043208) ' -2147208696
        Public Const unManagedidscontractdoesnotexist As Integer = CInt(&H80043207) ' -2147208697
        Public Const ContractTemplateDoesNotExist As Integer = CInt(&H80043206) ' -2147208698
        Public Const ContractInvalidAllotmentTypeCode As Integer = CInt(&H80043205) ' -2147208699
        Public Const ContractLineInvalidState As Integer = CInt(&H80043204) ' -2147208700
        Public Const ContractInvalidState As Integer = CInt(&H80043203) ' -2147208701
        Public Const ContractInvalidStartEndDate As Integer = CInt(&H80043202) ' -2147208702
        Public Const unManagedidscontractaccountmissing As Integer = CInt(&H80043201) ' -2147208703
        Public Const unManagedidscontractunexpected As Integer = CInt(&H80043200) ' -2147208704
        Public Const unManagedidsevalerrorformatlookupparameter As Integer = CInt(&H80042c4c) ' -2147210164
        Public Const unManagedidsevalerrorformattimezonecodeparameter As Integer = CInt(&H80042c4b) ' -2147210165
        Public Const unManagedidsevalerrorformatdecimalparameter As Integer = CInt(&H80042c4a) ' -2147210166
        Public Const unManagedidsevalerrorformatintegerparameter As Integer = CInt(&H80042c49) ' -2147210167
        Public Const unManagedidsevalerrorobjecttype As Integer = CInt(&H80042c48) ' -2147210168
        Public Const unManagedidsevalerrorqueueidparameter As Integer = CInt(&H80042c47) ' -2147210169
        Public Const unManagedidsevalerrorformatpicklistparameter As Integer = CInt(&H80042c46) ' -2147210170
        Public Const unManagedidsevalerrorformatbooleanparameter As Integer = CInt(&H80042c45) ' -2147210171
        Public Const unManagedidsevalerrorformatdatetimeparameter As Integer = CInt(&H80042c44) ' -2147210172
        Public Const unManagedidsevalerrorisnulllistparameter As Integer = CInt(&H80042c43) ' -2147210173
        Public Const unManagedidsevalerrorinlistparameter As Integer = CInt(&H80042c42) ' -2147210174
        Public Const unManagedidsevalerrorsetactivityparty As Integer = CInt(&H80042c41) ' -2147210175
        Public Const unManagedidsevalerrorremovefromactivityparty As Integer = CInt(&H80042c40) ' -2147210176
        Public Const unManagedidsevalerrorappendtoactivityparty As Integer = CInt(&H80042c3f) ' -2147210177
        Public Const unManagedidsevaltimererrorcalculatescheduletime As Integer = CInt(&H80042c3e) ' -2147210178
        Public Const unManagedidsevaltimerinvalidparameternumber As Integer = CInt(&H80042c3d) ' -2147210179
        Public Const unManagedidsevalcreateshouldhave2parameters As Integer = CInt(&H80042c3c) ' -2147210180
        Public Const unManagedidsevalerrorcreate As Integer = CInt(&H80042c3b) ' -2147210181
        Public Const unManagedidsevalerrorcontainparameter As Integer = CInt(&H80042c3a) ' -2147210182
        Public Const unManagedidsevalerrorendwithparameter As Integer = CInt(&H80042c39) ' -2147210183
        Public Const unManagedidsevalerrorbeginwithparameter As Integer = CInt(&H80042c38) ' -2147210184
        Public Const unManagedidsevalerrorstrlenparameter As Integer = CInt(&H80042c37) ' -2147210185
        Public Const unManagedidsevalerrorsubstrparameter As Integer = CInt(&H80042c36) ' -2147210186
        Public Const unManagedidsevalerrorinvalidrecipient As Integer = CInt(&H80042c35) ' -2147210187
        Public Const unManagedidsevalerrorinparameter As Integer = CInt(&H80042c34) ' -2147210188
        Public Const unManagedidsevalerrorbetweenparameter As Integer = CInt(&H80042c33) ' -2147210189
        Public Const unManagedidsevalerrorneqparameter As Integer = CInt(&H80042c32) ' -2147210190
        Public Const unManagedidsevalerroreqparameter As Integer = CInt(&H80042c31) ' -2147210191
        Public Const unManagedidsevalerrorleqparameter As Integer = CInt(&H80042c30) ' -2147210192
        Public Const unManagedidsevalerrorltparameter As Integer = CInt(&H80042c2f) ' -2147210193
        Public Const unManagedidsevalerrorgeqparameter As Integer = CInt(&H80042c2e) ' -2147210194
        Public Const unManagedidsevalerrorgtparameter As Integer = CInt(&H80042c2d) ' -2147210195
        Public Const unManagedidsevalerrorabsparameter As Integer = CInt(&H80042c2c) ' -2147210196
        Public Const unManagedidsevalerrorinvalidparameter As Integer = CInt(&H80042c2b) ' -2147210197
        Public Const unManagedidsevalgenericerror As Integer = CInt(&H80042c2a) ' -2147210198
        Public Const unManagedidsevalerrorincidentqueue As Integer = CInt(&H80042c29) ' -2147210199
        Public Const unManagedidsevalerrorhalt As Integer = CInt(&H80042c28) ' -2147210200
        Public Const unManagedidsevalerrorexec As Integer = CInt(&H80042c27) ' -2147210201
        Public Const unManagedidsevalerrorposturl As Integer = CInt(&H80042c26) ' -2147210202
        Public Const unManagedidsevalerrorsetstate As Integer = CInt(&H80042c25) ' -2147210203
        Public Const unManagedidsevalerrorroute As Integer = CInt(&H80042c24) ' -2147210204
        Public Const unManagedidsevalerrorupdate As Integer = CInt(&H80042c23) ' -2147210205
        Public Const unManagedidsevalerrorassign As Integer = CInt(&H80042c22) ' -2147210206
        Public Const unManagedidsevalerroremailtemplate As Integer = CInt(&H80042c21) ' -2147210207
        Public Const unManagedidsevalerrorsendemail As Integer = CInt(&H80042c20) ' -2147210208
        Public Const unManagedidsevalerrorunhandleincident As Integer = CInt(&H80042c1f) ' -2147210209
        Public Const unManagedidsevalerrorhandleincident As Integer = CInt(&H80042c1e) ' -2147210210
        Public Const unManagedidsevalerrorcreateincident As Integer = CInt(&H80042c1d) ' -2147210211
        Public Const unManagedidsevalerrornoteattachment As Integer = CInt(&H80042c1c) ' -2147210212
        Public Const unManagedidsevalerrorcreatenote As Integer = CInt(&H80042c1b) ' -2147210213
        Public Const unManagedidsevalerrorunhandleactivity As Integer = CInt(&H80042c1a) ' -2147210214
        Public Const unManagedidsevalerrorhandleactivity As Integer = CInt(&H80042c19) ' -2147210215
        Public Const unManagedidsevalerroractivityattachment As Integer = CInt(&H80042c18) ' -2147210216
        Public Const unManagedidsevalerrorcreateactivity As Integer = CInt(&H80042c17) ' -2147210217
        Public Const unManagedidsevalerrordividedbyzero As Integer = CInt(&H80042c16) ' -2147210218
        Public Const unManagedidsevalerrormodulusparameter As Integer = CInt(&H80042c15) ' -2147210219
        Public Const unManagedidsevalerrormodulusparameters As Integer = CInt(&H80042c14) ' -2147210220
        Public Const unManagedidsevalerrordivisionparameter As Integer = CInt(&H80042c13) ' -2147210221
        Public Const unManagedidsevalerrordivisionparameters As Integer = CInt(&H80042c12) ' -2147210222
        Public Const unManagedidsevalerrormultiplicationparameter As Integer = CInt(&H80042c11) ' -2147210223
        Public Const unManagedidsevalerrorsubtractionparameter As Integer = CInt(&H80042c10) ' -2147210224
        Public Const unManagedidsevalerroraddparameter As Integer = CInt(&H80042c0f) ' -2147210225
        Public Const unManagedidsevalmissselectquery As Integer = CInt(&H80042c0e) ' -2147210226
        Public Const unManagedidsevalchangetypeerror As Integer = CInt(&H80042c0d) ' -2147210227
        Public Const unManagedidsevalallcompleted As Integer = CInt(&H80042c0c) ' -2147210228
        Public Const unManagedidsevalmetabaseattributenotmatchquery As Integer = CInt(&H80042c0b) ' -2147210229
        Public Const unManagedidsevalmetabaseentitynotmatchquery As Integer = CInt(&H80042c0a) ' -2147210230
        Public Const unManagedidsevalpropertyisnull As Integer = CInt(&H80042c09) ' -2147210231
        Public Const unManagedidsevalmetabaseattributenotfound As Integer = CInt(&H80042c08) ' -2147210232
        Public Const unManagedidsevalmetabaseentitycompoundkeys As Integer = CInt(&H80042c07) ' -2147210233
        Public Const unManagedidsevalpropertynotfound As Integer = CInt(&H80042c06) ' -2147210234
        Public Const unManagedidsevalobjectnotfound As Integer = CInt(&H80042c05) ' -2147210235
        Public Const unManagedidsevalcompleted As Integer = CInt(&H80042c04) ' -2147210236
        Public Const unManagedidsevalaborted As Integer = CInt(&H80042c03) ' -2147210237
        Public Const unManagedidsevalallaborted As Integer = CInt(&H80042c02) ' -2147210238
        Public Const unManagedidsevalassignshouldhave4parameters As Integer = CInt(&H80042c01) ' -2147210239
        Public Const unManagedidsevalupdateshouldhave3parameters As Integer = CInt(&H80042c00) ' -2147210240
        Public Const unManagedidscpdecryptfailed As Integer = CInt(&H80042903) ' -2147211005
        Public Const unManagedidscpencryptfailed As Integer = CInt(&H80042902) ' -2147211006
        Public Const unManagedidscpbadpassword As Integer = CInt(&H80042901) ' -2147211007
        Public Const unManagedidscpuserdoesnotexist As Integer = CInt(&H80042900) ' -2147211008
        Public Const unManagedidsdataaccessunexpected As Integer = CInt(&H80042300) ' -2147212544
        Public Const unManagedidspropbagattributealreadyset As Integer = CInt(&H8004203f) ' -2147213249
        Public Const unManagedidspropbagattributenotnullable As Integer = CInt(&H8004203e) ' -2147213250
        Public Const unManagedidsrspropbagdbinfoalreadyset As Integer = CInt(&H8004203d) ' -2147213251
        Public Const unManagedidsrspropbagdbinfonotset As Integer = CInt(&H8004203c) ' -2147213252
        Public Const unManagedidspropbagcolloutofrange As Integer = CInt(&H8004201e) ' -2147213282
        Public Const unManagedidspropbagnullproperty As Integer = CInt(&H80042002) ' -2147213310
        Public Const unManagedidspropbagnointerface As Integer = CInt(&H80042001) ' -2147213311
        Public Const unManagedMissingObjectType As Integer = CInt(&H80042003) ' -2147213309
        Public Const unManagedObjectTypeUnexpected As Integer = CInt(&H80042004) ' -2147213308
        Public Const BusinessUnitCannotBeDisabled As Integer = CInt(&H80041d59) ' -2147213991
        Public Const BusinessUnitIsNotDisabledAndCannotBeDeleted As Integer = CInt(&H80041d60) ' -2147213984
        Public Const BusinessUnitHasChildAndCannotBeDeleted As Integer = CInt(&H80041d61) ' -2147213983
        Public Const BusinessUnitDefaultTeamOwnsRecords As Integer = CInt(&H80041d62) ' -2147213982
        Public Const RootBusinessUnitCannotBeDisabled As Integer = CInt(&H80041d63) ' -2147213981
        Public Const unManagedidspropbagpropertynotfound As Integer = CInt(&H80042000) ' -2147213312
        Public Const ReadOnlyUserNotSupported As Integer = CInt(&H80041d40) ' -2147214016
        Public Const SupportUserCannotBeCreateNorUpdated As Integer = CInt(&H80041D41) ' -2147214015
        Public Const ApplicationUserCannotBeUpdated As Integer = CInt(&H80041D48) ' -2147214008
        Public Const ApplicationNotRegisteredWithDeployment As Integer = CInt(&H80041D49) ' -2147214007
        Public Const InvalidOAuthToken As Integer = CInt(&H80041D50) ' -2147214000
        Public Const ExpiredOAuthToken As Integer = CInt(&H80041D52) ' -2147213998
        Public Const CannotAssignRolesToSupportUser As Integer = CInt(&H80041d51) ' -2147213999
        Public Const CannotMakeSelfReadOnlyUser As Integer = CInt(&H80041d39) ' -2147214023
        Public Const CannotMakeReadOnlyUser As Integer = CInt(&H80041d38) ' -2147214024
        Public Const unManagedidsbizmgmtcantchangeorgname As Integer = CInt(&H80041d36) ' -2147214026
        Public Const MultipleOrganizationsNotAllowed As Integer = CInt(&H80041d35) ' -2147214027
        Public Const UserSettingsInvalidAdvancedFindStartupMode As Integer = CInt(&H80041d34) ' -2147214028
        Public Const CannotModifySpecialUser As Integer = CInt(&H80041d33) ' -2147214029
        Public Const unManagedidsbizmgmtcannotaddlocaluser As Integer = CInt(&H80041d32) ' -2147214030
        Public Const CannotModifySysAdmin As Integer = CInt(&H80041d31) ' -2147214031
        Public Const CannotModifySupportUser As Integer = CInt(&H80041d43) ' -2147214013
        Public Const CannotAssignSupportUser As Integer = CInt(&H80041d44) ' -2147214012
        Public Const CannotRemoveFromSupportUser As Integer = CInt(&H80041d45) ' -2147214011
        Public Const CannotCreateFromSupportUser As Integer = CInt(&H80041d46) ' -2147214010
        Public Const CannotUpdateSupportUser As Integer = CInt(&H80041d47) ' -2147214009
        Public Const CannotRemoveFromSysAdmin As Integer = CInt(&H80041d30) ' -2147214032
        Public Const CannotDisableSysAdmin As Integer = CInt(&H80041d2f) ' -2147214033
        Public Const CannotDeleteSysAdmin As Integer = CInt(&H80041d2e) ' -2147214034
        Public Const CannotDeleteSupportUser As Integer = CInt(&H80041d42) ' -2147214014
        Public Const CannotDeleteSystemCustomizer As Integer = CInt(&H80041d4a) ' -2147214006
        Public Const CannotCreateSyncUserObjectMissing As Integer = CInt(&H80041d4b) ' -2147214005
        Public Const CannotUpdateSyncUserIsLicensedField As Integer = CInt(&H80041d4c) ' -2147214004
        Public Const CannotCreateSyncUserIsLicensedField As Integer = CInt(&H80041d4d) ' -2147214003
        Public Const CannotUpdateSyncUserIsSyncWithDirectoryField As Integer = CInt(&H80041d4e) ' -2147214002
        Public Const unManagedidsbizmgmtcannotreadaccountcontrol As Integer = CInt(&H80041d2d) ' -2147214035
        Public Const UserAlreadyExists As Integer = CInt(&H80041d2c) ' -2147214036
        Public Const unManagedidsbizmgmtusersettingsnotcreated As Integer = CInt(&H80041d2b) ' -2147214037
        Public Const ObjectNotFoundInAD As Integer = CInt(&H80041d2a) ' -2147214038
        Public Const GenericActiveDirectoryError As Integer = CInt(&H80041d37) ' -2147214025
        Public Const unManagedidsbizmgmtnoparentbusiness As Integer = CInt(&H80041d29) ' -2147214039
        Public Const ParentUserDoesNotExist As Integer = CInt(&H80041d27) ' -2147214041
        Public Const ChildUserDoesNotExist As Integer = CInt(&H80041d26) ' -2147214042
        Public Const UserLoopBeingCreated As Integer = CInt(&H80041d25) ' -2147214043
        Public Const UserLoopExists As Integer = CInt(&H80041d24) ' -2147214044
        Public Const ParentBusinessDoesNotExist As Integer = CInt(&H80041d23) ' -2147214045
        Public Const ChildBusinessDoesNotExist As Integer = CInt(&H80041d22) ' -2147214046
        Public Const BusinessManagementLoopBeingCreated As Integer = CInt(&H80041d21) ' -2147214047
        Public Const BusinessManagementLoopExists As Integer = CInt(&H80041d20) ' -2147214048
        Public Const BusinessManagementInvalidUserId As Integer = CInt(&H80041d1f) ' -2147214049
        Public Const unManagedidsbizmgmtuserdoesnothaveparent As Integer = CInt(&H80041d1e) ' -2147214050
        Public Const unManagedidsbizmgmtcannotenableprovision As Integer = CInt(&H80041d1d) ' -2147214051
        Public Const unManagedidsbizmgmtcannotenablebusiness As Integer = CInt(&H80041d1c) ' -2147214052
        Public Const unManagedidsbizmgmtcannotdisableprovision As Integer = CInt(&H80041d1b) ' -2147214053
        Public Const unManagedidsbizmgmtcannotdisablebusiness As Integer = CInt(&H80041d1a) ' -2147214054
        Public Const unManagedidsbizmgmtcannotdeleteprovision As Integer = CInt(&H80041d19) ' -2147214055
        Public Const unManagedidsbizmgmtcannotdeletebusiness As Integer = CInt(&H80041d18) ' -2147214056
        Public Const unManagedidsbizmgmtcannotremovepartnershipdefaultuser As Integer = CInt(&H80041d17) ' -2147214057
        Public Const unManagedidsbizmgmtpartnershipnotinpendingstatus As Integer = CInt(&H80041d16) ' -2147214058
        Public Const unManagedidsbizmgmtdefaultusernotinpartnerbusiness As Integer = CInt(&H80041d15) ' -2147214059
        Public Const unManagedidsbizmgmtcallernotinpartnerbusiness As Integer = CInt(&H80041d14) ' -2147214060
        Public Const unManagedidsbizmgmtdefaultusernotinprimarybusiness As Integer = CInt(&H80041d13) ' -2147214061
        Public Const unManagedidsbizmgmtcallernotinprimarybusiness As Integer = CInt(&H80041d12) ' -2147214062
        Public Const unManagedidsbizmgmtpartnershipalreadyexists As Integer = CInt(&H80041d11) ' -2147214063
        Public Const unManagedidsbizmgmtprimarysameaspartner As Integer = CInt(&H80041d10) ' -2147214064
        Public Const unManagedidsbizmgmtmisspartnerbusiness As Integer = CInt(&H80041d0f) ' -2147214065
        Public Const unManagedidsbizmgmtmissprimarybusiness As Integer = CInt(&H80041d0e) ' -2147214066
        Public Const InvalidAccessModeTransition As Integer = CInt(&H80041D66)  ' -2147213978
		Public Const MissingTeamName As Integer = CInt(&H80041D0B) ' -2147214069
        Public Const TeamAdministratorMissedPrivilege As Integer = CInt(&H80041d0a) ' -2147214070
		Public Const CannotDisableTenantAdmin As Integer = (&H80041D65)	' -2147213979
		Public Const CannotRemoveTenantAdminFromSysAdminRole As Integer = CInt(&H80041D64) ' -2147213980
		Public Const UserNotInParentHierarchy As Integer = CInt(&H80041D07)	' -2147214073
        Public Const unManagedidsbizmgmtusercannotbeownparent As Integer = CInt(&H80041d06) ' -2147214074
        Public Const unManagedidsbizmgmtcannotmovedefaultuser As Integer = CInt(&H80041d05) ' -2147214075
        Public Const unManagedidsbizmgmtbusinessparentdiffmerchant As Integer = CInt(&H80041d04) ' -2147214076
        Public Const unManagedidsbizmgmtdefaultusernotinbusiness As Integer = CInt(&H80041d03) ' -2147214077
        Public Const unManagedidsbizmgmtmissparentbusiness As Integer = CInt(&H80041d02) ' -2147214078
        Public Const unManagedidsbizmgmtmissuserdomainname As Integer = CInt(&H80041d01) ' -2147214079
        Public Const unManagedidsbizmgmtmissbusinessname As Integer = CInt(&H80041d00) ' -2147214080
        Public Const unManagedidsxmlinvalidread As Integer = CInt(&H80041a08) ' -2147214840
        Public Const unManagedidsxmlinvalidfield As Integer = CInt(&H80041a07) ' -2147214841
        Public Const unManagedidsxmlinvalidentityattributes As Integer = CInt(&H80041a06) ' -2147214842
        Public Const unManagedidsxmlunexpected As Integer = CInt(&H80041a05) ' -2147214843
        Public Const unManagedidsxmlparseerror As Integer = CInt(&H80041a04) ' -2147214844
        Public Const unManagedidsxmlinvalidcollectionname As Integer = CInt(&H80041a03) ' -2147214845
        Public Const unManagedidsxmlinvalidupdate As Integer = CInt(&H80041a02) ' -2147214846
        Public Const unManagedidsxmlinvalidcreate As Integer = CInt(&H80041a01) ' -2147214847
        Public Const unManagedidsxmlinvalidentityname As Integer = CInt(&H80041a00) ' -2147214848
        Public Const unManagedidsnotesnoattachment As Integer = CInt(&H80041704) ' -2147215612
        Public Const unManagedidsnotesloopbeingcreated As Integer = CInt(&H80041703) ' -2147215613
        Public Const unManagedidsnotesloopexists As Integer = CInt(&H80041702) ' -2147215614
        Public Const unManagedidsnotesalreadyattached As Integer = CInt(&H80041701) ' -2147215615
        Public Const unManagedidsnotesnotedoesnotexist As Integer = CInt(&H80041700) ' -2147215616
        Public Const DuplicatedPrivilege As Integer = CInt(&H8004140f) ' -2147216369
        Public Const MemberHasAlreadyBeenContacted As Integer = CInt(&H8004140e) ' -2147216370
        Public Const TeamInWrongBusiness As Integer = CInt(&H8004140d) ' -2147216371
        Public Const unManagedidsrolesdeletenonparentrole As Integer = CInt(&H8004140c) ' -2147216372
        Public Const InvalidPrivilegeDepth As Integer = CInt(&H8004140b) ' -2147216373
        Public Const unManagedidsrolesinvalidrolename As Integer = CInt(&H8004140a) ' -2147216374
        Public Const UserInWrongBusiness As Integer = CInt(&H80041409) ' -2147216375
        Public Const unManagedidsrolesmissprivid As Integer = CInt(&H80041408) ' -2147216376
        Public Const unManagedidsrolesmissrolename As Integer = CInt(&H80041407) ' -2147216377
        Public Const unManagedidsrolesmissbusinessid As Integer = CInt(&H80041406) ' -2147216378
        Public Const unManagedidsrolesmissroleid As Integer = CInt(&H80041405) ' -2147216379
        Public Const unManagedidsrolesinvalidtemplateid As Integer = CInt(&H80041404) ' -2147216380
        Public Const RoleAlreadyExists As Integer = CInt(&H80041403) ' -2147216381
        Public Const unManagedidsrolesroledoesnotexist As Integer = CInt(&H80041402) ' -2147216382
        Public Const unManagedidsrolesinvalidroleid As Integer = CInt(&H80041401) ' -2147216383
        Public Const unManagedidsrolesinvalidroledata As Integer = CInt(&H80041400) ' -2147216384
        Public Const QueryBuilderSerializationInvalidIsQuickFindFilter As Integer = CInt(&H80041138) ' -2147217096
        Public Const QueryBuilderAttributeCannotBeGroupByAndAggregate As Integer = CInt(&H80041137) ' -2147217097
        Public Const SqlArithmeticOverflowError As Integer = CInt(&H80041136) ' -2147217098
        Public Const QueryBuilderInvalidDateGrouping As Integer = CInt(&H80041135) ' -2147217099
        Public Const QueryBuilderAliasRequiredForAggregateOrderBy As Integer = CInt(&H80041134) ' -2147217100
        Public Const QueryBuilderAttributeRequiredForNonAggregateOrderBy As Integer = CInt(&H80041133) ' -2147217101
        Public Const QueryBuilderAliasNotAllowedForNonAggregateOrderBy As Integer = CInt(&H80041132) ' -2147217102
        Public Const QueryBuilderAttributeNotAllowedForAggregateOrderBy As Integer = CInt(&H80041131) ' -2147217103
        Public Const QueryBuilderDuplicateAlias As Integer = CInt(&H80041130) ' -2147217104
        Public Const QueryBuilderInvalidAggregateAttribute As Integer = CInt(&H8004112f) ' -2147217105
        Public Const QueryBuilderDeserializeInvalidGroupBy As Integer = CInt(&H8004112e) ' -2147217106
        Public Const QueryBuilderNoAttrsDistinctConflict As Integer = CInt(&H8004112c) ' -2147217108
        Public Const QueryBuilderInvalidPagingCookie As Integer = CInt(&H8004112a) ' -2147217110
        Public Const QueryBuilderPagingOrderBy As Integer = CInt(&H80041129) ' -2147217111
        Public Const QueryBuilderEntitiesDontMatch As Integer = CInt(&H80041128) ' -2147217112
		Public Const QueryBuilderLinkNodeForOrderNotFound As Integer = CInt(&H80041126)	' -2147217114
        Public Const QueryBuilderDeserializeNoDocElemXml As Integer = CInt(&H80041125) ' -2147217115
        Public Const QueryBuilderDeserializeEmptyXml As Integer = CInt(&H80041124) ' -2147217116
        Public Const QueryBuilderElementNotFound As Integer = CInt(&H80041123) ' -2147217117
        Public Const QueryBuilderInvalidFilterType As Integer = CInt(&H80041122) ' -2147217118
        Public Const QueryBuilderInvalidJoinOperator As Integer = CInt(&H80041121) ' -2147217119
        Public Const QueryBuilderInvalidConditionOperator As Integer = CInt(&H80041120) ' -2147217120
        Public Const QueryBuilderInvalidOrderType As Integer = CInt(&H8004111f) ' -2147217121
        Public Const QueryBuilderAttributeNotFound As Integer = CInt(&H8004111e) ' -2147217122
        Public Const QueryBuilderDeserializeInvalidUtcOffset As Integer = CInt(&H8004111d) ' -2147217123
        Public Const QueryBuilderDeserializeInvalidNode As Integer = CInt(&H8004111c) ' -2147217124
        Public Const QueryBuilderDeserializeInvalidGetMinActiveRowVersion As Integer = CInt(&H8004111b) ' -2147217125
        Public Const QueryBuilderDeserializeInvalidAggregate As Integer = CInt(&H8004111a) ' -2147217126
        Public Const QueryBuilderDeserializeInvalidDescending As Integer = CInt(&H80041119) ' -2147217127
        Public Const QueryBuilderDeserializeInvalidNoLock As Integer = CInt(&H80041118) ' -2147217128
        Public Const QueryBuilderDeserializeInvalidLinkType As Integer = CInt(&H80041117) ' -2147217129
        Public Const QueryBuilderDeserializeInvalidMapping As Integer = CInt(&H80041116) ' -2147217130
        Public Const QueryBuilderDeserializeInvalidDistinct As Integer = CInt(&H80041115) ' -2147217131
        Public Const QueryBuilderSerialzeLinkTopCriteria As Integer = CInt(&H80041114) ' -2147217132
        Public Const QueryBuilderColumnSetVersionMissing As Integer = CInt(&H80041113) ' -2147217133
        Public Const QueryBuilderInvalidColumnSetVersion As Integer = CInt(&H80041112) ' -2147217134
        Public Const QueryBuilderAttributePairMismatch As Integer = CInt(&H80041111) ' -2147217135
        Public Const QueryBuilderByAttributeNonEmpty As Integer = CInt(&H80041110) ' -2147217136
        Public Const QueryBuilderByAttributeMismatch As Integer = CInt(&H8004110f) ' -2147217137
        Public Const QueryBuilderMultipleIntersectEntities As Integer = CInt(&H8004110e) ' -2147217138
        Public Const QueryBuilderReportView_Does_Not_Exist As Integer = CInt(&H8004110d) ' -2147217139
        Public Const QueryBuilderValue_GreaterThanZero As Integer = CInt(&H8004110c) ' -2147217140
        Public Const QueryBuilderNoAlias As Integer = CInt(&H8004110b) ' -2147217141
        Public Const QueryBuilderAlias_Does_Not_Exist As Integer = CInt(&H8004110a) ' -2147217142
        Public Const QueryBuilderInvalid_Alias As Integer = CInt(&H80041109) ' -2147217143
        Public Const QueryBuilderInvalid_Value As Integer = CInt(&H80041108) ' -2147217144
        Public Const QueryBuilderAttribute_With_Aggregate As Integer = CInt(&H80041107) ' -2147217145
        Public Const QueryBuilderBad_Condition As Integer = CInt(&H80041106) ' -2147217146
        Public Const QueryBuilderNoAttribute As Integer = CInt(&H80041103) ' -2147217149
        Public Const QueryBuilderNoEntity As Integer = CInt(&H80041102) ' -2147217150
        Public Const QueryBuilderUnexpected As Integer = CInt(&H80041101) ' -2147217151
        Public Const QueryBuilderInvalidUpdate As Integer = CInt(&H80041100) ' -2147217152
        Public Const QueryBuilderInvalidLogicalOperator As Integer = CInt(&H800410fe) ' -2147217154
        Public Const unManagedidsmetadatanorelationship As Integer = CInt(&H80040e02) ' -2147217918
        Public Const MetadataNoMapping As Integer = CInt(&H80040e01) ' -2147217919
        Public Const MetadataNotSerializable As Integer = CInt(&H80040e03) ' -2147217917
        Public Const unManagedidsmetadatanoentity As Integer = CInt(&H80040e00) ' -2147217920
        Public Const unManagedidscommunicationsnosenderaddress As Integer = CInt(&H80040b08) ' -2147218680
        Public Const unManagedidscommunicationstemplateinvalidtemplate As Integer = CInt(&H80040b07) ' -2147218681
        Public Const unManagedidscommunicationsnoparticipationmask As Integer = CInt(&H80040b06) ' -2147218682
        Public Const unManagedidscommunicationsnorecipients As Integer = CInt(&H80040b05) ' -2147218683
        Public Const EmailRecipientNotSpecified As Integer = CInt(&H80040b04) ' -2147218684
        Public Const unManagedidscommunicationsnosender As Integer = CInt(&H80040b02) ' -2147218686
        Public Const unManagedidscommunicationsbadsender As Integer = CInt(&H80040b01) ' -2147218687
        Public Const unManagedidscommunicationsnopartyaddress As Integer = CInt(&H80040b00) ' -2147218688
        Public Const unManagedidsjournalingmissingincidentid As Integer = CInt(&H80040809) ' -2147219447
        Public Const unManagedidsjournalingmissingcontactid As Integer = CInt(&H80040808) ' -2147219448
        Public Const unManagedidsjournalingmissingopportunityid As Integer = CInt(&H80040807) ' -2147219449
        Public Const unManagedidsjournalingmissingaccountid As Integer = CInt(&H80040806) ' -2147219450
        Public Const unManagedidsjournalingmissingleadid As Integer = CInt(&H80040805) ' -2147219451
        Public Const unManagedidsjournalingmissingeventtype As Integer = CInt(&H80040804) ' -2147219452
        Public Const unManagedidsjournalinginvalideventtype As Integer = CInt(&H80040803) ' -2147219453
        Public Const unManagedidsjournalingmissingeventdirection As Integer = CInt(&H80040802) ' -2147219454
        Public Const unManagedidsjournalingunsupportedobjecttype As Integer = CInt(&H80040801) ' -2147219455
        Public Const SdkEntityDoesNotSupportMessage As Integer = CInt(&H80040800) ' -2147219456
        Public Const OpportunityAlreadyInOpenState As Integer = CInt(&H8004051a) ' -2147220198
        Public Const LeadAlreadyInClosedState As Integer = CInt(&H80040519) ' -2147220199
        Public Const LeadAlreadyInOpenState As Integer = CInt(&H80040518) ' -2147220200
        Public Const CustomerIsInactive As Integer = CInt(&H80040517) ' -2147220201
        Public Const OpportunityCannotBeClosed As Integer = CInt(&H80040516) ' -2147220202
        Public Const OpportunityIsAlreadyClosed As Integer = CInt(&H80040515) ' -2147220203
        Public Const unManagedidscustomeraddresstypeinvalid As Integer = CInt(&H80040514) ' -2147220204
        Public Const unManagedidsleadnotassignedtocaller As Integer = CInt(&H80040513) ' -2147220205
        Public Const unManagedidscontacthaschildopportunities As Integer = CInt(&H80040512) ' -2147220206
        Public Const unManagedidsaccounthaschildopportunities As Integer = CInt(&H80040511) ' -2147220207
        Public Const unManagedidsleadoneaccount As Integer = CInt(&H80040510) ' -2147220208
        Public Const unManagedidsopportunityorphan As Integer = CInt(&H8004050f) ' -2147220209
        Public Const unManagedidsopportunityoneaccount As Integer = CInt(&H8004050e) ' -2147220210
        Public Const unManagedidsleadusercannotreject As Integer = CInt(&H8004050d) ' -2147220211
        Public Const unManagedidsleadnotassigned As Integer = CInt(&H8004050c) ' -2147220212
        Public Const unManagedidsleadnoparent As Integer = CInt(&H8004050b) ' -2147220213
        Public Const ContactLoopBeingCreated As Integer = CInt(&H8004050a) ' -2147220214
        Public Const ContactLoopExists As Integer = CInt(&H80040509) ' -2147220215
        Public Const PresentParentAccountAndParentContact As Integer = CInt(&H80040508) ' -2147220216
        Public Const AccountLoopBeingCreated As Integer = CInt(&H80040507) ' -2147220217
        Public Const AccountLoopExists As Integer = CInt(&H80040506) ' -2147220218
        Public Const unManagedidsopportunitymissingparent As Integer = CInt(&H80040505) ' -2147220219
        Public Const unManagedidsopportunityinvalidparent As Integer = CInt(&H80040504) ' -2147220220
        Public Const ContactDoesNotExist As Integer = CInt(&H80040503) ' -2147220221
        Public Const AccountDoesNotExist As Integer = CInt(&H80040502) ' -2147220222
        Public Const unManagedidsleaddoesnotexist As Integer = CInt(&H80040501) ' -2147220223
        Public Const unManagedidsopportunitydoesnotexist As Integer = CInt(&H80040500) ' -2147220224
        Public Const ReportDoesNotExist As Integer = CInt(&H80040499) ' -2147220327
        Public Const ReportLoopBeingCreated As Integer = CInt(&H80040498) ' -2147220328
        Public Const ReportLoopExists As Integer = CInt(&H80040497) ' -2147220329
        Public Const ParentReportLinksToSameNameChild As Integer = CInt(&H80040496) ' -2147220330
        Public Const DuplicateReportVisibility As Integer = CInt(&H80040495) ' -2147220331
        Public Const ReportRenderError As Integer = CInt(&H80040494) ' -2147220332
        Public Const SubReportDoesNotExist As Integer = CInt(&H80040493) ' -2147220333
        Public Const SrsDataConnectorNotInstalled As Integer = CInt(&H80040492) ' -2147220334
        Public Const InvalidCustomReportingWizardXml As Integer = CInt(&H80040491) ' -2147220335
        Public Const UpdateNonCustomReportFromTemplate As Integer = CInt(&H80040490) ' -2147220336
        Public Const SnapshotReportNotReady As Integer = CInt(&H80040489) ' -2147220343
        Public Const ExistingExternalReport As Integer = CInt(&H80040488) ' -2147220344
        Public Const ParentReportNotSupported As Integer = CInt(&H80040487) ' -2147220345
        Public Const ParentReportDoesNotReferenceChild As Integer = CInt(&H80040486) ' -2147220346
        Public Const MultipleParentReportsFound As Integer = CInt(&H80040485) ' -2147220347
        Public Const ReportingServicesReportExpected As Integer = CInt(&H80040484) ' -2147220348
        Public Const InvalidTransformationParameter As Integer = CInt(&H80040389) ' -2147220599
        Public Const ReflexiveEntityParentOrChildDoesNotExist As Integer = CInt(&H80040388) ' -2147220600
        Public Const EntityLoopBeingCreated As Integer = CInt(&H80040387) ' -2147220601
        Public Const EntityLoopExists As Integer = CInt(&H80040386) ' -2147220602
        Public Const UnsupportedProcessCode As Integer = CInt(&H80040385) ' -2147220603
        Public Const NoOutputTransformationParameterMappingFound As Integer = CInt(&H80040384) ' -2147220604
        Public Const RequiredColumnsNotFoundInImportFile As Integer = CInt(&H80040383) ' -2147220605
        Public Const InvalidTransformationParameterMapping As Integer = CInt(&H80040382) ' -2147220606
        Public Const UnmappedTransformationOutputDataFound As Integer = CInt(&H80040381) ' -2147220607
        Public Const InvalidTransformationParameterDataType As Integer = CInt(&H80040380) ' -2147220608
        Public Const ArrayMappingFoundForSingletonParameter As Integer = CInt(&H8004037f) ' -2147220609
        Public Const SingletonMappingFoundForArrayParameter As Integer = CInt(&H8004037e) ' -2147220610
        Public Const IncompleteTransformationParameterMappingsFound As Integer = CInt(&H8004037d) ' -2147220611
        Public Const InvalidTransformationParameterMappings As Integer = CInt(&H8004037c) ' -2147220612
        Public Const GenericTransformationInvocationError As Integer = CInt(&H8004037b) ' -2147220613
        Public Const InvalidTransformationType As Integer = CInt(&H8004037a) ' -2147220614
        Public Const UnableToLoadTransformationType As Integer = CInt(&H80040379) ' -2147220615
        Public Const UnableToLoadTransformationAssembly As Integer = CInt(&H80040378) ' -2147220616
        Public Const InvalidColumnMapping As Integer = CInt(&H80040377) ' -2147220617
        Public Const CannotModifyOldDataFromImport As Integer = CInt(&H80040376) ' -2147220618
        Public Const ImportFileTooLargeToUpload As Integer = CInt(&H80040375) ' -2147220619
        Public Const InvalidImportFileContent As Integer = CInt(&H80040374) ' -2147220620
        Public Const EmptyRecord As Integer = CInt(&H80040373) ' -2147220621
        Public Const LongParseRow As Integer = CInt(&H80040372) ' -2147220622
        Public Const ParseMustBeCalledBeforeTransform As Integer = CInt(&H80040371) ' -2147220623
        Public Const HeaderValueDoesNotMatchAttributeDisplayLabel As Integer = CInt(&H80040370) ' -2147220624
        Public Const InvalidTargetEntity As Integer = CInt(&H80040369) ' -2147220631
        Public Const NoHeaderColumnFound As Integer = CInt(&H80040368) ' -2147220632
        Public Const ParsingMetadataNotFound As Integer = CInt(&H80040367) ' -2147220633
        Public Const EmptyHeaderRow As Integer = CInt(&H80040366) ' -2147220634
        Public Const EmptyContent As Integer = CInt(&H80040365) ' -2147220635
        Public Const InvalidIsFirstRowHeaderForUseSystemMap As Integer = CInt(&H80040364) ' -2147220636
        Public Const InvalidGuid As Integer = CInt(&H80040363) ' -2147220637
        Public Const GuidNotPresent As Integer = CInt(&H80040362) ' -2147220638
        Public Const OwnerValueNotMapped As Integer = CInt(&H80040361) ' -2147220639
        Public Const PicklistValueNotMapped As Integer = CInt(&H80040360) ' -2147220640
        Public Const ErrorInDelete As Integer = CInt(&H8004035a) ' -2147220646
        Public Const ErrorIncreate As Integer = CInt(&H80040359) ' -2147220647
        Public Const ErrorInUpdate As Integer = CInt(&H80040358) ' -2147220648
        Public Const ErrorInSetState As Integer = CInt(&H80040357) ' -2147220649
        Public Const InvalidDataFormat As Integer = CInt(&H80040356) ' -2147220650
        Public Const InvalidFormatForDataDelimiter As Integer = CInt(&H80040355) ' -2147220651
        Public Const CRMUserDoesNotExist As Integer = CInt(&H80040354) ' -2147220652
        Public Const LookupNotFound As Integer = CInt(&H80040353) ' -2147220653
        Public Const DuplicateLookupFound As Integer = CInt(&H80040352) ' -2147220654
        Public Const InvalidImportFileData As Integer = CInt(&H80040351) ' -2147220655
        Public Const InvalidXmlSSContent As Integer = CInt(&H80040350) ' -2147220656
        Public Const InvalidImportFileParseData As Integer = CInt(&H80040349) ' -2147220663
        Public Const InvalidValueForFileType As Integer = CInt(&H80040348) ' -2147220664
        Public Const EmptyImportFileRow As Integer = CInt(&H80040347) ' -2147220665
        Public Const ErrorInParseRow As Integer = CInt(&H80040346) ' -2147220666
        Public Const DataColumnsNumberMismatch As Integer = CInt(&H80040345) ' -2147220667
        Public Const InvalidHeaderColumn As Integer = CInt(&H80040344) ' -2147220668
        Public Const OwnerMappingExistsWithSourceSystemUserName As Integer = CInt(&H80040343) ' -2147220669
        Public Const PickListMappingExistsWithSourceValue As Integer = CInt(&H80040342) ' -2147220670
        Public Const InvalidValueForDataDelimiter As Integer = CInt(&H80040341) ' -2147220671
        Public Const InvalidValueForFieldDelimiter As Integer = CInt(&H80040340) ' -2147220672
        Public Const PickListMappingExistsForTargetValue As Integer = CInt(&H8004033f) ' -2147220673
        Public Const MappingExistsForTargetAttribute As Integer = CInt(&H8004033e) ' -2147220674
        Public Const SourceEntityMappedToMultipleTargets As Integer = CInt(&H8004033d) ' -2147220675
        Public Const AttributeNotOfTypePicklist As Integer = CInt(&H8004033c) ' -2147220676
        Public Const AttributeNotOfTypeReference As Integer = CInt(&H80040390) ' -2147220592
        Public Const TargetEntityNotFound As Integer = CInt(&H80040391) ' -2147220591
        Public Const TargetAttributeNotFound As Integer = CInt(&H80040392) ' -2147220590
        Public Const PicklistValueNotFound As Integer = CInt(&H80040393) ' -2147220589
        Public Const TargetAttributeInvalidForMap As Integer = CInt(&H80040394) ' -2147220588
        Public Const TargetEntityInvalidForMap As Integer = CInt(&H80040395) ' -2147220587
        Public Const InvalidFileBadCharacters As Integer = CInt(&H80040396) ' -2147220586
        Public Const ErrorsInImportFiles As Integer = CInt(&H8004034a) ' -2147220662
        Public Const InvalidOperationWhenListIsNotActive As Integer = CInt(&H8004033a) ' -2147220678
        Public Const InvalidOperationWhenPartyIsNotActive As Integer = CInt(&H8004033b) ' -2147220677
        Public Const AsyncOperationSuspendedOrLocked As Integer = CInt(&H80040339) ' -2147220679
        Public Const DuplicateHeaderColumn As Integer = CInt(&H80040338) ' -2147220680
        Public Const EmptyHeaderColumn As Integer = CInt(&H80040337) ' -2147220681
        Public Const InvalidColumnNumber As Integer = CInt(&H80040336) ' -2147220682
        Public Const TransformMustBeCalledBeforeImport As Integer = CInt(&H80040335) ' -2147220683
        Public Const OperationCanBeCalledOnlyOnce As Integer = CInt(&H80040334) ' -2147220684
        Public Const DuplicateRecordsFound As Integer = CInt(&H80040333) ' -2147220685
        Public Const CampaignActivityClosed As Integer = CInt(&H80040331) ' -2147220687
        Public Const UnexpectedErrorInMailMerge As Integer = CInt(&H80040330) ' -2147220688
        Public Const UserCancelledMailMerge As Integer = CInt(&H8004032f) ' -2147220689
        Public Const FilteredDuetoMissingEmailAddress As Integer = CInt(&H8004032e) ' -2147220690
        Public Const CannotDeleteAsBackgroundOperationInProgress As Integer = CInt(&H8004032b) ' -2147220693
        Public Const FilteredDuetoInactiveState As Integer = CInt(&H8004032a) ' -2147220694
        Public Const MissingBOWFRules As Integer = CInt(&H80040329) ' -2147220695
        Public Const CannotSpecifyOwnerForActivityPropagation As Integer = CInt(&H80040327) ' -2147220697
        Public Const CampaignActivityAlreadyPropagated As Integer = CInt(&H80040326) ' -2147220698
        Public Const FilteredDuetoAntiSpam As Integer = CInt(&H80040325) ' -2147220699
        Public Const TemplateTypeNotSupportedForUnsubscribeAcknowledgement As Integer = CInt(&H80040324) ' -2147220700
        Public Const ErrorInImportConfig As Integer = CInt(&H80040323) ' -2147220701
        Public Const ImportConfigNotSpecified As Integer = CInt(&H80040322) ' -2147220702
        Public Const InvalidActivityType As Integer = CInt(&H80040321) ' -2147220703
        Public Const UnsupportedParameter As Integer = CInt(&H80040320) ' -2147220704
        Public Const MissingParameter As Integer = CInt(&H8004031f) ' -2147220705
        Public Const CannotSpecifyCommunicationAttributeOnActivityForPropagation As Integer = CInt(&H8004031e) ' -2147220706
        Public Const CannotSpecifyRecipientForActivityPropagation As Integer = CInt(&H8004031d) ' -2147220707
        Public Const CannotSpecifyAttendeeForAppointmentPropagation As Integer = CInt(&H8004031c) ' -2147220708
        Public Const CannotSpecifySenderForActivityPropagation As Integer = CInt(&H8004031b) ' -2147220709
        Public Const CannotSpecifyOrganizerForAppointmentPropagation As Integer = CInt(&H8004031a) ' -2147220710
        Public Const InvalidRegardingObjectTypeCode As Integer = CInt(&H80040319) ' -2147220711
        Public Const UnspecifiedActivityXmlForCampaignActivityPropagate As Integer = CInt(&H80040318) ' -2147220712
        Public Const MoneySizeExceeded As Integer = CInt(&H80040317) ' -2147220713
        Public Const ExtraPartyInformation As Integer = CInt(&H80040316) ' -2147220714
        Public Const NotSupported As Integer = CInt(&H80040315) ' -2147220715
        Public Const InvalidOperationForClosedOrCancelledCampaignActivity As Integer = CInt(&H80040314) ' -2147220716
        Public Const InvalidEmailTemplate As Integer = CInt(&H80040313) ' -2147220717
        Public Const CannotCreateResponseForTemplate As Integer = CInt(&H80040312) ' -2147220718
        Public Const CannotPropagateCamapaignActivityForTemplate As Integer = CInt(&H80040311) ' -2147220719
        Public Const InvalidChannelForCampaignActivityPropagate As Integer = CInt(&H80040310) ' -2147220720
        Public Const InvalidActivityTypeForCampaignActivityPropagate As Integer = CInt(&H8004030f) ' -2147220721
        Public Const ObjectNotRelatedToCampaign As Integer = CInt(&H8004030e) ' -2147220722
        Public Const CannotRelateObjectTypeToCampaignActivity As Integer = CInt(&H8004030d) ' -2147220723
        Public Const CannotUpdateCampaignForCampaignResponse As Integer = CInt(&H8004030c) ' -2147220724
        Public Const CannotUpdateCampaignForCampaignActivity As Integer = CInt(&H8004030b) ' -2147220725
        Public Const CampaignNotSpecifiedForCampaignResponse As Integer = CInt(&H8004030a) ' -2147220726
        Public Const CampaignNotSpecifiedForCampaignActivity As Integer = CInt(&H80040309) ' -2147220727
        Public Const CannotRelateObjectTypeToCampaign As Integer = CInt(&H80040307) ' -2147220729
        Public Const CannotCopyIncompatibleListType As Integer = CInt(&H80040306) ' -2147220730
        Public Const InvalidActivityTypeForList As Integer = CInt(&H80040305) ' -2147220731
        Public Const CannotAssociateInactiveItemToCampaign As Integer = CInt(&H80040304) ' -2147220732
        Public Const InvalidFetchXml As Integer = CInt(&H80040303) ' -2147220733
        Public Const InvalidOperationWhenListLocked As Integer = CInt(&H80040302) ' -2147220734
        Public Const UnsupportedListMemberType As Integer = CInt(&H80040301) ' -2147220735
        Public Const CannotMoveDisabledUser As Integer = CInt(&H80040267) ' -2147220889
        Public Const InvalidPrimaryKey As Integer = CInt(&H80040266) ' -2147220890
        Public Const IsvAborted As Integer = CInt(&H80040265) ' -2147220891
        Public Const CannotAssignOutlookFilters As Integer = CInt(&H80040264) ' -2147220892
        Public Const CannotCreateOutlookFilters As Integer = CInt(&H80040263) ' -2147220893
        Public Const CannotGrantAccessToOutlookFilters As Integer = CInt(&H80040268) ' -2147220888
        Public Const CannotModifyAccessToOutlookFilters As Integer = CInt(&H80040269) ' -2147220887
        Public Const CannotRevokeAccessToOutlookFilters As Integer = CInt(&H80040270) ' -2147220880
        Public Const CannotGrantAccessToOfflineFilters As Integer = CInt(&H80040271) ' -2147220879
        Public Const CannotModifyAccessToOfflineFilters As Integer = CInt(&H80040272) ' -2147220878
        Public Const CannotRevokeAccessToOfflineFilters As Integer = CInt(&H80040273) ' -2147220877
        Public Const DuplicateOutlookAppointment As Integer = CInt(&H80040274) ' -2147220876
        Public Const AppointmentScheduleNotSet As Integer = CInt(&H80040275) ' -2147220875
        Public Const PrivilegeCreateIsDisabledForOrganization As Integer = CInt(&H80040276) ' -2147220874
        Public Const UnauthorizedAccess As Integer = CInt(&H80040277) ' -2147220873
        Public Const InvalidCharactersInField As Integer = CInt(&H80040278) ' -2147220872
        Public Const CannotChangeStateOfNonpublicView As Integer = CInt(&H80040279) ' -2147220871
        Public Const CannotDeactivateDefaultView As Integer = CInt(&H8004027a) ' -2147220870
        Public Const CannotSetInactiveViewAsDefault As Integer = CInt(&H8004027b) ' -2147220869
        Public Const CannotExceedFilterLimit As Integer = CInt(&H8004027c) ' -2147220868
        Public Const CannotHaveMultipleDefaultFilterTemplates As Integer = CInt(&H8004027d) ' -2147220867
        Public Const CrmConstraintParsingError As Integer = CInt(&H80040262) ' -2147220894
        Public Const CrmConstraintEvaluationError As Integer = CInt(&H80040261) ' -2147220895
        Public Const CrmExpressionEvaluationError As Integer = CInt(&H80040260) ' -2147220896
        Public Const CrmExpressionParametersParsingError As Integer = CInt(&H8004025f) ' -2147220897
        Public Const CrmExpressionBodyParsingError As Integer = CInt(&H8004025e) ' -2147220898
        Public Const CrmExpressionParsingError As Integer = CInt(&H8004025d) ' -2147220899
        Public Const CrmMalformedExpressionError As Integer = CInt(&H8004025c) ' -2147220900
        Public Const CalloutException As Integer = CInt(&H8004025b) ' -2147220901
        Public Const DateTimeFormatFailed As Integer = CInt(&H8004025a) ' -2147220902
        Public Const NumberFormatFailed As Integer = CInt(&H80040259) ' -2147220903
        Public Const InvalidRestore As Integer = CInt(&H80040258) ' -2147220904
        Public Const InvalidCaller As Integer = CInt(&H80040257) ' -2147220905
        Public Const CrmSecurityError As Integer = CInt(&H80040256) ' -2147220906
        Public Const TransactionAborted As Integer = CInt(&H80040255) ' -2147220907
        Public Const CannotBindToSession As Integer = CInt(&H80040254) ' -2147220908
        Public Const SessionTokenUnavailable As Integer = CInt(&H80040253) ' -2147220909
        Public Const TransactionNotCommited As Integer = CInt(&H80040252) ' -2147220910
        Public Const TransactionNotStarted As Integer = CInt(&H80040251) ' -2147220911
        Public Const MultipleChildPicklist As Integer = CInt(&H80040250) ' -2147220912
        Public Const InvalidSingletonResults As Integer = CInt(&H8004024f) ' -2147220913
        Public Const FailedToLoadAssembly As Integer = CInt(&H8004024e) ' -2147220914
        Public Const CrmQueryExpressionNotInitialized As Integer = CInt(&H8004024d) ' -2147220915
        Public Const InvalidRegistryKey As Integer = CInt(&H8004024c) ' -2147220916
        Public Const InvalidPriv As Integer = CInt(&H8004024b) ' -2147220917
        Public Const MetadataNotFound As Integer = CInt(&H8004024a) ' -2147220918
        Public Const InvalidEntityClassException As Integer = CInt(&H80040249) ' -2147220919
        Public Const InvalidXmlEntityNameException As Integer = CInt(&H80040248) ' -2147220920
        Public Const InvalidXmlCollectionNameException As Integer = CInt(&H80040247) ' -2147220921
        Public Const InvalidRecurrenceRule As Integer = CInt(&H80040246) ' -2147220922
        Public Const CrmImpersonationError As Integer = CInt(&H80040245) ' -2147220923
        Public Const ServiceInstantiationFailed As Integer = CInt(&H80040244) ' -2147220924
        Public Const EntityInstantiationFailed As Integer = CInt(&H80040243) ' -2147220925
        Public Const FormTransitionError As Integer = CInt(&H80040242) ' -2147220926
        Public Const UserTimeConvertException As Integer = CInt(&H80040241) ' -2147220927
        Public Const UserTimeZoneException As Integer = CInt(&H80040240) ' -2147220928
        Public Const InvalidConnectionString As Integer = CInt(&H8004023f) ' -2147220929
        Public Const OpenCrmDBConnection As Integer = CInt(&H8004023e) ' -2147220930
        Public Const UnpopulatedPrimaryKey As Integer = CInt(&H8004023d) ' -2147220931
        Public Const InvalidVersion As Integer = CInt(&H8004023c) ' -2147220932
        Public Const InvalidOperation As Integer = CInt(&H8004023b) ' -2147220933
        Public Const InvalidMetadata As Integer = CInt(&H8004023a) ' -2147220934
        Public Const InvalidDateTime As Integer = CInt(&H80040239) ' -2147220935
        Public Const unManagedidscannotdefaultprivateview As Integer = CInt(&H80040238) ' -2147220936
        Public Const DuplicateRecord As Integer = CInt(&H80040237) ' -2147220937
        Public Const unManagedidsnorelationship As Integer = CInt(&H80040236) ' -2147220938
        Public Const MissingQueryType As Integer = CInt(&H80040235) ' -2147220939
        Public Const InvalidRollupType As Integer = CInt(&H80040234) ' -2147220940
        Public Const InvalidState As Integer = CInt(&H80040233) ' -2147220941
        Public Const unManagedidsviewisnotsharable As Integer = CInt(&H80040232) ' -2147220942
        Public Const PrincipalPrivilegeDenied As Integer = CInt(&H80040231) ' -2147220943
        Public Const CannotUpdateObjectBecauseItIsInactive As Integer = CInt(&H80040230) ' -2147220944
        Public Const CannotDeleteCannedView As Integer = CInt(&H8004022f) ' -2147220945
        Public Const CannotUpdateBecauseItIsReadOnly As Integer = CInt(&H8004022e) ' -2147220946
		Public Const CaseAlreadyResolved As Integer = CInt(&H800404CF) ' -2147220273
		Public Const InvalidCustomer As Integer = CInt(&H8004022D) ' -2147220947
        Public Const unManagedidsdataoutofrange As Integer = CInt(&H8004022c) ' -2147220948
        Public Const unManagedidsownernotenabled As Integer = CInt(&H8004022b) ' -2147220949
        Public Const BusinessManagementObjectAlreadyExists As Integer = CInt(&H8004022a) ' -2147220950
        Public Const InvalidOwnerID As Integer = CInt(&H80040229) ' -2147220951
        Public Const CannotDeleteAsItIsReadOnly As Integer = CInt(&H80040228) ' -2147220952
        Public Const CannotDeleteDueToAssociation As Integer = CInt(&H80040227) ' -2147220953
        Public Const unManagedidsanonymousenabled As Integer = CInt(&H80040226) ' -2147220954
        Public Const unManagedidsusernotenabled As Integer = CInt(&H80040225) ' -2147220955
        Public Const BusinessNotEnabled As Integer = CInt(&H8004032c) ' -2147220692
        Public Const CannotAssignToDisabledBusiness As Integer = CInt(&H8004032d) ' -2147220691
        Public Const IsvUnExpected As Integer = CInt(&H80040224) ' -2147220956
        Public Const OnlyOwnerCanRevoke As Integer = CInt(&H80040223) ' -2147220957
        Public Const unManagedidsoutofmemory As Integer = CInt(&H80040222) ' -2147220958
        Public Const unManagedidscannotassigntobusiness As Integer = CInt(&H80040221) ' -2147220959
        Public Const PrivilegeDenied As Integer = CInt(&H80040220) ' -2147220960
        Public Const InvalidObjectTypes As Integer = CInt(&H8004021f) ' -2147220961
        Public Const unManagedidscannotgrantorrevokeaccesstobusiness As Integer = CInt(&H8004021e) ' -2147220962
        Public Const unManagedidsinvaliduseridorbusinessidorusersbusinessinvalid As Integer = CInt(&H8004021d) ' -2147220963
        Public Const unManagedidspresentuseridandteamid As Integer = CInt(&H8004021c) ' -2147220964
        Public Const MissingUserId As Integer = CInt(&H8004021b) ' -2147220965
        Public Const MissingBusinessId As Integer = CInt(&H8004021a) ' -2147220966
        Public Const NotImplemented As Integer = CInt(&H80040219) ' -2147220967
        Public Const InvalidPointer As Integer = CInt(&H80040218) ' -2147220968
        Public Const ObjectDoesNotExist As Integer = CInt(&H80040217) ' -2147220969
        Public Const UnExpected As Integer = CInt(&H80040216) ' -2147220970
        Public Const MissingOwner As Integer = CInt(&H80040215) ' -2147220971
        Public Const CannotShareWithOwner As Integer = CInt(&H80040214) ' -2147220972
        Public Const unManagedidsinvalidvisibilitymodificationaccess As Integer = CInt(&H80040213) ' -2147220973
        Public Const unManagedidsinvalidowninguser As Integer = CInt(&H80040212) ' -2147220974
        Public Const unManagedidsinvalidassociation As Integer = CInt(&H80040211) ' -2147220975
        Public Const InvalidAssigneeId As Integer = CInt(&H80040210) ' -2147220976
        Public Const unManagedidsfailureinittoken As Integer = CInt(&H8004020f) ' -2147220977
        Public Const unManagedidsinvalidvisibility As Integer = CInt(&H8004020e) ' -2147220978
        Public Const InvalidAccessRights As Integer = CInt(&H8004020d) ' -2147220979
        Public Const InvalidSharee As Integer = CInt(&H8004020c) ' -2147220980
        Public Const unManagedidsinvaliditemid As Integer = CInt(&H8004020b) ' -2147220981
        Public Const unManagedidsinvalidorgid As Integer = CInt(&H8004020a) ' -2147220982
        Public Const unManagedidsinvalidbusinessid As Integer = CInt(&H80040209) ' -2147220983
        Public Const unManagedidsinvalidteamid As Integer = CInt(&H80040208) ' -2147220984
        Public Const unManagedidsinvaliduserid As Integer = CInt(&H80040207) ' -2147220985
        Public Const InvalidParentId As Integer = CInt(&H80040206) ' -2147220986
        Public Const InvalidParent As Integer = CInt(&H80040205) ' -2147220987
        Public Const InvalidUserAuth As Integer = CInt(&H80040204) ' -2147220988
        Public Const InvalidArgument As Integer = CInt(&H80040203) ' -2147220989
        Public Const EmptyXml As Integer = CInt(&H80040202) ' -2147220990
        Public Const InvalidXml As Integer = CInt(&H80040201) ' -2147220991
        Public Const RequiredFieldMissing As Integer = CInt(&H80040200) ' -2147220992
        Public Const SearchTextLenExceeded As Integer = CInt(&H800401ff) ' -2147220993
        Public Const CannotAssignOfflineFilters As Integer = CInt(&H800404ff) ' -2147220225
        Public Const ArticleIsPublished As Integer = CInt(&H800404fe) ' -2147220226
        Public Const InvalidArticleTemplateState As Integer = CInt(&H800404fd) ' -2147220227
        Public Const InvalidArticleStateTransition As Integer = CInt(&H800404fc) ' -2147220228
        Public Const InvalidArticleState As Integer = CInt(&H800404fb) ' -2147220229
        Public Const NullKBArticleTemplateId As Integer = CInt(&H800404fa) ' -2147220230
        Public Const NullArticleTemplateStructureXml As Integer = CInt(&H800404f9) ' -2147220231
        Public Const NullArticleTemplateFormatXml As Integer = CInt(&H800404f8) ' -2147220232
        Public Const NullArticleXml As Integer = CInt(&H800404f7) ' -2147220233
        Public Const InvalidContractDetailId As Integer = CInt(&H800404f6) ' -2147220234
        Public Const InvalidTotalPrice As Integer = CInt(&H800404f5) ' -2147220235
        Public Const InvalidTotalDiscount As Integer = CInt(&H800404f4) ' -2147220236
        Public Const InvalidNetPrice As Integer = CInt(&H800404f3) ' -2147220237
        Public Const InvalidAllotmentsRemaining As Integer = CInt(&H800404f2) ' -2147220238
        Public Const InvalidAllotmentsUsed As Integer = CInt(&H800404f1) ' -2147220239
        Public Const InvalidAllotmentsTotal As Integer = CInt(&H800404f0) ' -2147220240
        Public Const InvalidAllotmentsCalc As Integer = CInt(&H800404ef) ' -2147220241
        Public Const CannotRouteToSameQueue As Integer = CInt(&H8004051b) ' -2147220197
        Public Const CannotAddSingleQueueEnabledEntityToQueue As Integer = CInt(&H8004051c) ' -2147220196
        Public Const CannotUpdateDeactivatedQueueItem As Integer = CInt(&H8004051d) ' -2147220195
        Public Const CannotCreateQueueItemInactiveObject As Integer = CInt(&H8004051e) ' -2147220194
        Public Const InsufficientPrivilegeToQueueOwner As Integer = CInt(&H80040520) ' -2147220192
        Public Const NoPrivilegeToWorker As Integer = CInt(&H80040521) ' -2147220191
        Public Const CannotAddQueueItemsToInactiveQueue As Integer = CInt(&H80040522) ' -2147220190
        Public Const EmailAlreadyExistsInDestinationQueue As Integer = CInt(&H80040523) ' -2147220189
        Public Const CouldNotFindQueueItemInQueue As Integer = CInt(&H80040524) ' -2147220188
        Public Const MultipleQueueItemsFound As Integer = CInt(&H80040525) ' -2147220187
        Public Const ActiveQueueItemAlreadyExists As Integer = CInt(&H80040526) ' -2147220186
        Public Const CannotRouteInactiveQueueItem As Integer = CInt(&H80040527) ' -2147220185
        Public Const QueueIdNotPresent As Integer = CInt(&H80040528) ' -2147220184
        Public Const QueueItemNotPresent As Integer = CInt(&H80040529) ' -2147220183
        Public Const CannotUpdatePrivateOrWIPQueue As Integer = CInt(&H800404ee) ' -2147220242
        Public Const CannotFindUserQueue As Integer = CInt(&H800404ec) ' -2147220244
        Public Const CannotFindObjectInQueue As Integer = CInt(&H800404eb) ' -2147220245
        Public Const CannotRouteToQueue As Integer = CInt(&H800404ea) ' -2147220246
        Public Const RouteTypeUnsupported As Integer = CInt(&H800404e9) ' -2147220247
        Public Const UserIdOrQueueNotSet As Integer = CInt(&H800404e8) ' -2147220248
        Public Const RoutingNotAllowed As Integer = CInt(&H800404e7) ' -2147220249
        Public Const CannotUpdateMetricOnChildGoal As Integer = CInt(&H80044900) ' -2147202816
        Public Const CannotUpdateGoalPeriodInfoChildGoal As Integer = CInt(&H80044901) ' -2147202815
        Public Const CannotUpdateMetricOnGoalWithChildren As Integer = CInt(&H80044902) ' -2147202814
        Public Const FiscalPeriodGoalMissingInfo As Integer = CInt(&H80044903) ' -2147202813
        Public Const CustomPeriodGoalHavingExtraInfo As Integer = CInt(&H80044904) ' -2147202812
        Public Const ParentChildMetricIdDiffers As Integer = CInt(&H80044905) ' -2147202811
        Public Const ParentChildPeriodAttributesDiffer As Integer = CInt(&H80044906) ' -2147202810
        Public Const CustomPeriodGoalMissingInfo As Integer = CInt(&H80044907) ' -2147202809
        Public Const GoalMissingPeriodTypeInfo As Integer = CInt(&H80044908) ' -2147202808
        Public Const ParticipatingQueryEntityMismatch As Integer = CInt(&H80044909) ' -2147202807
        Public Const CannotUpdateGoalPeriodInfoClosedGoal As Integer = CInt(&H80044910) ' -2147202800
        Public Const CannotUpdateRollupFields As Integer = CInt(&H80044911) ' -2147202799
        Public Const CannotDeleteMetricWithGoals As Integer = CInt(&H80044800) ' -2147203072
        Public Const CannotUpdateRollupAttributeWithClosedGoals As Integer = CInt(&H80044801) ' -2147203071
        Public Const MetricNameAlreadyExists As Integer = CInt(&H80044802) ' -2147203070
        Public Const CannotUpdateMetricWithGoals As Integer = CInt(&H80044803) ' -2147203069
        Public Const CannotCreateUpdateSourceAttribute As Integer = CInt(&H80044804) ' -2147203068
        Public Const InvalidDateAttribute As Integer = CInt(&H80044805) ' -2147203067
        Public Const InvalidSourceEntityAttribute As Integer = CInt(&H80044806) ' -2147203066
        Public Const GoalAttributeAlreadyMapped As Integer = CInt(&H80044807) ' -2147203065
        Public Const InvalidSourceAttributeType As Integer = CInt(&H80044808) ' -2147203064
        Public Const MaxLimitForRollupAttribute As Integer = CInt(&H8004480a) ' -2147203062
        Public Const InvalidGoalAttribute As Integer = CInt(&H8004480b) ' -2147203061
        Public Const CannotUpdateParentAndDependents As Integer = CInt(&H8004480c) ' -2147203060
        Public Const UserDoesNotHaveSendAsAllowed As Integer = CInt(&H8004480d) ' -2147203059
        Public Const CannotUpdateQuoteCurrency As Integer = CInt(&H8004480e) ' -2147203058
        Public Const UserDoesNotHaveSendAsForQueue As Integer = CInt(&H8004480f) ' -2147203057
        Public Const InvalidSourceStateValue As Integer = CInt(&H80044810) ' -2147203056
        Public Const InvalidSourceStatusValue As Integer = CInt(&H80044811) ' -2147203055
        Public Const InvalidEntityForDateAttribute As Integer = CInt(&H80044812) ' -2147203054
        Public Const InvalidEntityForRollup As Integer = CInt(&H80044813) ' -2147203053
        Public Const InvalidFiscalPeriod As Integer = CInt(&H80044814) ' -2147203052
        Public Const unManagedchildentityisnotchild As Integer = CInt(&H800404e6) ' -2147220250
        Public Const unManagedmissingparententity As Integer = CInt(&H800404e5) ' -2147220251
        Public Const unManagedunablegetexecutioncontext As Integer = CInt(&H800404e4) ' -2147220252
        Public Const unManagedpendingtrxexists As Integer = CInt(&H800404e3) ' -2147220253
        Public Const unManagedinvalidtrxcountforcommit As Integer = CInt(&H800404e2) ' -2147220254
        Public Const unManagedinvalidtrxcountforrollback As Integer = CInt(&H800404e1) ' -2147220255
        Public Const unManagedunableswitchusercontext As Integer = CInt(&H800404e0) ' -2147220256
        Public Const unManagedmissingdataaccess As Integer = CInt(&H800404df) ' -2147220257
        Public Const unManagedinvalidcharacterdataforaggregate As Integer = CInt(&H800404de) ' -2147220258
        Public Const unManagedtrxinterophandlerset As Integer = CInt(&H800404dd) ' -2147220259
        Public Const unManagedinvalidbinaryfield As Integer = CInt(&H800404dc) ' -2147220260
        Public Const unManagedinvaludidispatchfield As Integer = CInt(&H800404db) ' -2147220261
        Public Const unManagedinvaliddbdatefield As Integer = CInt(&H800404da) ' -2147220262
        Public Const unManagedinvalddbtimefield As Integer = CInt(&H800404d9) ' -2147220263
        Public Const unManagedinvalidfieldtype As Integer = CInt(&H800404d8) ' -2147220264
        Public Const unManagedinvalidstreamfield As Integer = CInt(&H800404d7) ' -2147220265
        Public Const unManagedinvalidparametertypeforparameterizedquery As Integer = CInt(&H800404d6) ' -2147220266
        Public Const unManagedinvaliddynamicparameteraccessor As Integer = CInt(&H800404d5) ' -2147220267
        Public Const unManagedunablegetsessiontokennotrx As Integer = CInt(&H800404d4) ' -2147220268
        Public Const unManagedunablegetsessiontoken As Integer = CInt(&H800404d3) ' -2147220269
        Public Const unManagedinvalidsecurityprincipal As Integer = CInt(&H800404d2) ' -2147220270
        Public Const unManagedmissingpreviousownertype As Integer = CInt(&H800404d0) ' -2147220272
        Public Const unManagedinvalidprivilegeid As Integer = CInt(&H800404ce) ' -2147220274
        Public Const unManagedinvalidprivilegeusergroup As Integer = CInt(&H800404cd) ' -2147220275
        Public Const unManagedunexpectedpropertytype As Integer = CInt(&H800404cc) ' -2147220276
        Public Const unManagedmissingaddressentity As Integer = CInt(&H800404cb) ' -2147220277
        Public Const unManagederroraddingfiltertoqueryplan As Integer = CInt(&H800404ca) ' -2147220278
        Public Const unManagedmissingreferencesfromrelationship As Integer = CInt(&H800404c9) ' -2147220279
        Public Const unManagedmissingreferencingattribute As Integer = CInt(&H800404c8) ' -2147220280
        Public Const unManagedinvalidoperator As Integer = CInt(&H800404c7) ' -2147220281
        Public Const unManagedunabletoaccessqueryplanfilter As Integer = CInt(&H800404c6) ' -2147220282
        Public Const unManagedmissingattributefortag As Integer = CInt(&H800404c5) ' -2147220283
        Public Const unManagederrorprocessingfilternodes As Integer = CInt(&H800404c4) ' -2147220284
        Public Const unManagedunabletolocateconditionfilter As Integer = CInt(&H800404c3) ' -2147220285
        Public Const unManagedinvalidpagevalue As Integer = CInt(&H800404c2) ' -2147220286
        Public Const unManagedinvalidcountvalue As Integer = CInt(&H800404c1) ' -2147220287
        Public Const unManagedinvalidversionvalue As Integer = CInt(&H800404c0) ' -2147220288
        Public Const unManagedinvalidvaluettagoutsideconditiontag As Integer = CInt(&H800404bf) ' -2147220289
        Public Const unManagedinvalidorganizationid As Integer = CInt(&H800404be) ' -2147220290
        Public Const unManagedinvalidowninguser As Integer = CInt(&H800404bd) ' -2147220291
        Public Const unManagedinvalidowningbusinessunitorbusinessunitid As Integer = CInt(&H800404bc) ' -2147220292
        Public Const unManagedinvalidprivilegeedepth As Integer = CInt(&H800404bb) ' -2147220293
        Public Const unManagedinvalidlinkobjects As Integer = CInt(&H800404ba) ' -2147220294
        Public Const unManagedpartylistattributenotsupported As Integer = CInt(&H800404b8) ' -2147220296
        Public Const unManagedinvalidargumentsforcondition As Integer = CInt(&H800404b7) ' -2147220297
        Public Const unManagedunknownaggregateoperation As Integer = CInt(&H800404b6) ' -2147220298
        Public Const unManagedmissingparentattributeonentity As Integer = CInt(&H800404b5) ' -2147220299
        Public Const unManagedinvalidprocesschildofcondition As Integer = CInt(&H800404b4) ' -2147220300
        Public Const unManagedunexpectedrimarykey As Integer = CInt(&H800404b3) ' -2147220301
        Public Const unManagedmissinglinkentity As Integer = CInt(&H800404b2) ' -2147220302
        Public Const unManagedinvalidprocessliternalcondition As Integer = CInt(&H800404b1) ' -2147220303
        Public Const unManagedemptyprocessliteralcondition As Integer = CInt(&H800404b0) ' -2147220304
        Public Const unManagedunusablevariantdata As Integer = CInt(&H800404af) ' -2147220305
        Public Const unManagedfieldnotvalidatedbyplatform As Integer = CInt(&H800404ae) ' -2147220306
        Public Const unManagedmissingfilterattribute As Integer = CInt(&H800404ad) ' -2147220307
        Public Const unManagedinvalidequalityoperand As Integer = CInt(&H800404ac) ' -2147220308
        Public Const unManagedfilterindexoutofrange As Integer = CInt(&H800404ab) ' -2147220309
        Public Const unManagedentityisnotintersect As Integer = CInt(&H800404aa) ' -2147220310
        Public Const unManagedcihldofconditionforoffilefilters As Integer = CInt(&H800404a9) ' -2147220311
        Public Const unManagedinvalidowningbusinessunit As Integer = CInt(&H800404a8) ' -2147220312
        Public Const unManagedinvalidbusinessunitid As Integer = CInt(&H800404a7) ' -2147220313
        Public Const unManagedmorethanonesortattribute As Integer = CInt(&H800404a6) ' -2147220314
        Public Const unManagedunabletoaccessqueryplan As Integer = CInt(&H800404a5) ' -2147220315
        Public Const unManagedparentattributenotfound As Integer = CInt(&H800404a4) ' -2147220316
        Public Const unManagedinvalidtlsmananger As Integer = CInt(&H800404a2) ' -2147220318
        Public Const unManagedinvalidescapedxml As Integer = CInt(&H800404a1) ' -2147220319
        Public Const unManagedunabletoretrieveprivileges As Integer = CInt(&H800404a0) ' -2147220320
        Public Const unManagedproxycreationfailed As Integer = CInt(&H8004049f) ' -2147220321
        Public Const unManagedinvalidprincipal As Integer = CInt(&H8004049e) ' -2147220322
        Public Const RestrictInheritedRole As Integer = CInt(&H80044152) ' -2147204782
        Public Const unManagedidsfetchbetweentext As Integer = CInt(&H80044153) ' -2147204781
        Public Const unManagedidscantdisable As Integer = CInt(&H80044154) ' -2147204780
        Public Const CascadeInvalidLinkTypeTransition As Integer = CInt(&H80044155) ' -2147204779
        Public Const InvalidOrgOwnedCascadeLinkType As Integer = CInt(&H80044156) ' -2147204778
        Public Const CallerCannotChangeOwnDomainName As Integer = CInt(&H80044161) ' -2147204767
        Public Const AsyncOperationInvalidStateChange As Integer = CInt(&H80044162) ' -2147204766
        Public Const AsyncOperationInvalidStateChangeUnexpected As Integer = CInt(&H80044163) ' -2147204765
        Public Const AsyncOperationMissingId As Integer = CInt(&H80044164) ' -2147204764
        Public Const AsyncOperationInvalidStateChangeToComplete As Integer = CInt(&H80044165) ' -2147204763
        Public Const AsyncOperationInvalidStateChangeToReady As Integer = CInt(&H80044166) ' -2147204762
        Public Const AsyncOperationInvalidStateChangeToSuspended As Integer = CInt(&H80044167) ' -2147204761
        Public Const AsyncOperationCannotUpdateNonrecurring As Integer = CInt(&H80044168) ' -2147204760
        Public Const AsyncOperationCannotUpdateRecurring As Integer = CInt(&H80044169) ' -2147204759
        Public Const AsyncOperationCannotDeleteUnlessCompleted As Integer = CInt(&H8004416a) ' -2147204758
        Public Const SdkInvalidMessagePropertyName As Integer = CInt(&H8004416b) ' -2147204757
        Public Const PluginAssemblyMustHavePublicKeyToken As Integer = CInt(&H8004416c) ' -2147204756
        Public Const SdkMessageInvalidImageTypeRegistration As Integer = CInt(&H8004416d) ' -2147204755
        Public Const SdkMessageDoesNotSupportPostImageRegistration As Integer = CInt(&H8004416e) ' -2147204754
        Public Const CannotDeserializeRequest As Integer = CInt(&H8004416f) ' -2147204753
        Public Const InvalidPluginRegistrationConfiguration As Integer = CInt(&H80044170) ' -2147204752
        Public Const SandboxClientPluginTimeout As Integer = CInt(&H80044171) ' -2147204751
        Public Const SandboxHostPluginTimeout As Integer = CInt(&H80044172) ' -2147204750
        Public Const SandboxWorkerPluginTimeout As Integer = CInt(&H80044173) ' -2147204749
        Public Const SandboxSdkListenerStartFailed As Integer = CInt(&H80044174) ' -2147204748
        Public Const ServiceBusPostFailed As Integer = CInt(&H80044175) ' -2147204747
        Public Const ServiceBusIssuerNotFound As Integer = CInt(&H80044176) ' -2147204746
        Public Const ServiceBusIssuerCertificateError As Integer = CInt(&H80044177) ' -2147204745
        Public Const ServiceBusExtendedTokenFailed As Integer = CInt(&H80044178) ' -2147204744
        Public Const ServiceBusPostPostponed As Integer = CInt(&H80044179) ' -2147204743
        Public Const ServiceBusPostDisabled As Integer = CInt(&H8004417a) ' -2147204742
        Public Const SdkMessageNotSupportedOnServer As Integer = CInt(&H80044180) ' -2147204736
        Public Const SdkMessageNotSupportedOnClient As Integer = CInt(&H80044181) ' -2147204735
        Public Const SdkCorrelationTokenDepthTooHigh As Integer = CInt(&H80044182) ' -2147204734
        Public Const OnlyStepInPredefinedStagesCanBeModified As Integer = CInt(&H80044184) ' -2147204732
        Public Const OnlyStepInServerOnlyCanHaveSecureConfiguration As Integer = CInt(&H80044185) ' -2147204731
        Public Const OnlyStepOutsideTransactionCanCreateCrmService As Integer = CInt(&H80044186) ' -2147204730
        Public Const SdkCustomProcessingStepIsNotAllowed As Integer = CInt(&H80044187) ' -2147204729
        Public Const SdkEntityOfflineQueuePlaybackIsNotAllowed As Integer = CInt(&H80044188) ' -2147204728
        Public Const SdkMessageDoesNotSupportImageRegistration As Integer = CInt(&H80044189) ' -2147204727
        Public Const RequestLengthTooLarge As Integer = CInt(&H8004418a) ' -2147204726
        Public Const SandboxWorkerNotAvailable As Integer = CInt(&H8004418d) ' -2147204723
        Public Const SandboxHostNotAvailable As Integer = CInt(&H8004418e) ' -2147204722
        Public Const PluginAssemblyContentSizeExceeded As Integer = CInt(&H8004418f) ' -2147204721
        Public Const UnableToLoadPluginType As Integer = CInt(&H80044190) ' -2147204720
        Public Const UnableToLoadPluginAssembly As Integer = CInt(&H80044191) ' -2147204719
        Public Const InvalidPluginAssemblyContent As Integer = CInt(&H8004418b) ' -2147204725
        Public Const InvalidPluginTypeImplementation As Integer = CInt(&H8004418c) ' -2147204724
        Public Const InvalidPluginAssemblyVersion As Integer = CInt(&H8004417b) ' -2147204741
        Public Const PluginTypeMustBeUnique As Integer = CInt(&H8004417c) ' -2147204740
        Public Const InvalidAssemblySourceType As Integer = CInt(&H8004417d) ' -2147204739
        Public Const InvalidAssemblyProcessorArchitecture As Integer = CInt(&H8004417e) ' -2147204738
        Public Const CyclicReferencesNotSupported As Integer = CInt(&H8004417f) ' -2147204737
		Public Const InvalidQuery As Integer = CInt(&H80044183)	' -2147204733
		Public Const InvalidEmailAddressFormat As Integer = CInt(&H80044192) ' -2147204718
        Public Const ContractInvalidDiscount As Integer = CInt(&H80044193) ' -2147204717
        Public Const InvalidLanguageCode As Integer = CInt(&H80044195) ' -2147204715
        Public Const ConfigNullPrimaryKey As Integer = CInt(&H80044196) ' -2147204714
        Public Const ConfigMissingDescription As Integer = CInt(&H80044197) ' -2147204713
        Public Const AttributeDoesNotSupportLocalizedLabels As Integer = CInt(&H80044198) ' -2147204712
        Public Const NoLanguageProvisioned As Integer = CInt(&H80044199) ' -2147204711
        Public Const CannotImportNullStringsForBaseLanguage As Integer = CInt(&H80044246) ' -2147204538
        Public Const CannotUpdateNonCustomizableString As Integer = CInt(&H80044247) ' -2147204537
        Public Const InvalidOrganizationId As Integer = CInt(&H80044248) ' -2147204536
        Public Const InvalidTranslationsFile As Integer = CInt(&H80044249) ' -2147204535
        Public Const MetadataRecordNotDeletable As Integer = CInt(&H80044250) ' -2147204528
        Public Const InvalidImportJobTemplateFile As Integer = CInt(&H80044251) ' -2147204527
        Public Const InvalidImportJobId As Integer = CInt(&H80044252) ' -2147204526
        Public Const MissingCrmAuthenticationToken As Integer = CInt(&H80044300) ' -2147204352
        Public Const IntegratedAuthenticationIsNotAllowed As Integer = CInt(&H80044301) ' -2147204351
        Public Const RequestIsNotAuthenticated As Integer = CInt(&H80044302) ' -2147204350
        Public Const AsyncOperationTypeIsNotRecognized As Integer = CInt(&H80044303) ' -2147204349
        Public Const FailedToDeserializeAsyncOperationData As Integer = CInt(&H80044304) ' -2147204348
        Public Const UserSettingsOverMaxPagingLimit As Integer = CInt(&H80044305) ' -2147204347
        Public Const AsyncNetworkError As Integer = CInt(&H80044306) ' -2147204346
        Public Const AsyncCommunicationError As Integer = CInt(&H80044307) ' -2147204345
        Public Const MissingCrmAuthenticationTokenOrganizationName As Integer = CInt(&H80044308) ' -2147204344
        Public Const SdkNotEnoughPrivilegeToSetCallerOriginToken As Integer = CInt(&H80044309) ' -2147204343
        Public Const OverRetrievalUpperLimitWithoutPagingCookie As Integer = CInt(&H8004430a) ' -2147204342
        Public Const InvalidAllotmentsOverage As Integer = CInt(&H8004430b) ' -2147204341
        Public Const TooManyConditionsInQuery As Integer = CInt(&H8004430c) ' -2147204340
        Public Const TooManyLinkEntitiesInQuery As Integer = CInt(&H8004430d) ' -2147204339
        Public Const TooManyConditionParametersInQuery As Integer = CInt(&H8004430e) ' -2147204338
        Public Const InvalidOneToManyRelationshipForRelatedEntitiesQuery As Integer = CInt(&H8004430f) ' -2147204337
        Public Const PicklistValueNotUnique As Integer = CInt(&H80044310) ' -2147204336
        Public Const UnableToLogOnUserFromUserNameAndPassword As Integer = CInt(&H80044311) ' -2147204335
        Public Const PicklistValueOutOfRange As Integer = CInt(&H8004431a) ' -2147204326
        Public Const WrongNumberOfBooleanOptions As Integer = CInt(&H8004431b) ' -2147204325
        Public Const BooleanOptionOutOfRange As Integer = CInt(&H8004431c) ' -2147204324
        Public Const CannotAddNewBooleanValue As Integer = CInt(&H8004431d) ' -2147204323
        Public Const CannotAddNewStateValue As Integer = CInt(&H8004431e) ' -2147204322
        Public Const NoMoreCustomOptionValuesExist As Integer = CInt(&H8004431f) ' -2147204321
        Public Const InsertOptionValueInvalidType As Integer = CInt(&H80044320) ' -2147204320
        Public Const NewStatusRequiresAssociatedState As Integer = CInt(&H80044321) ' -2147204319
        Public Const NewStatusHasInvalidState As Integer = CInt(&H80044322) ' -2147204318
        Public Const CannotDeleteEnumOptionsFromAttributeType As Integer = CInt(&H80044323) ' -2147204317
        Public Const OptionReorderArrayIncorrectLength As Integer = CInt(&H80044324) ' -2147204316
        Public Const ValueMissingInOptionOrderArray As Integer = CInt(&H80044325) ' -2147204315
        Public Const NavPaneOrderValueNotAllowed As Integer = CInt(&H80044327) ' -2147204313
        Public Const EntityRelationshipRoleCustomLabelsMissing As Integer = CInt(&H80044328) ' -2147204312
        Public Const NavPaneNotCustomizable As Integer = CInt(&H80044329) ' -2147204311
        Public Const EntityRelationshipSchemaNameRequired As Integer = CInt(&H8004432a) ' -2147204310
        Public Const EntityRelationshipSchemaNameNotUnique As Integer = CInt(&H8004432b) ' -2147204309
        Public Const CustomReflexiveRelationshipNotAllowedForEntity As Integer = CInt(&H8004432c) ' -2147204308
        Public Const EntityCannotBeChildInCustomRelationship As Integer = CInt(&H8004432d) ' -2147204307
        Public Const ReferencedEntityHasLogicalPrimaryNameField As Integer = CInt(&H8004432e) ' -2147204306
        Public Const IntegerValueOutOfRange As Integer = CInt(&H8004432f) ' -2147204305
        Public Const DecimalValueOutOfRange As Integer = CInt(&H80044330) ' -2147204304
        Public Const StringLengthTooLong As Integer = CInt(&H80044331) ' -2147204303
        Public Const EntityCannotParticipateInEntityAssociation As Integer = CInt(&H80044332) ' -2147204302
        Public Const DataMigrationManagerUnknownProblem As Integer = CInt(&H80044333) ' -2147204301
        Public Const ImportOperationChildFailure As Integer = CInt(&H80044334) ' -2147204300
        Public Const AttributeDeprecated As Integer = CInt(&H80044335) ' -2147204299
        Public Const DataMigrationManagerMandatoryUpdatesNotInstalled As Integer = CInt(&H80044336) ' -2147204298
        Public Const ReferencedEntityMustHaveLookupView As Integer = CInt(&H80044337) ' -2147204297
        Public Const ReferencingEntityMustHaveAssociationView As Integer = CInt(&H80044338) ' -2147204296
        Public Const CouldNotObtainLockOnResource As Integer = CInt(&H80044339) ' -2147204295
        Public Const SourceAttributeHeaderTooBig As Integer = CInt(&H80044340) ' -2147204288
        Public Const CannotDeleteDefaultStatusOption As Integer = CInt(&H80044341) ' -2147204287
        Public Const CannotFindDomainAccount As Integer = CInt(&H80044342) ' -2147204286
        Public Const CannotUpdateAppDefaultValueForStateAttribute As Integer = CInt(&H80044343) ' -2147204285
        Public Const CannotUpdateAppDefaultValueForStatusAttribute As Integer = CInt(&H80044344) ' -2147204284
        Public Const InvalidOptionSetSchemaName As Integer = CInt(&H80044345) ' -2147204283
        Public Const ReferencingEntityCannotBeSolutionAware As Integer = CInt(&H80044350) ' -2147204272
        Public Const ErrorInFieldWidthIncrease As Integer = CInt(&H80044351) ' -2147204271
		Public Const ExpiredVersionStamp As Integer = CInt(&H80044352) ' -2147204270
		Public Const AsyncOperationCannotCancel As Integer = CInt(&H80044F00) ' -2147201280
        Public Const AsyncOperationCannotPause As Integer = CInt(&H80044f01) ' -2147201279
        Public Const WorkflowCompileFailure As Integer = CInt(&H80045001) ' -2147201023
        Public Const UpdatePublishedWorkflowDefinition As Integer = CInt(&H80045002) ' -2147201022
        Public Const UpdateWorkflowActivation As Integer = CInt(&H80045003) ' -2147201021
        Public Const DeleteWorkflowActivation As Integer = CInt(&H80045004) ' -2147201020
        Public Const DeleteWorkflowActivationWorkflowDependency As Integer = CInt(&H80045005) ' -2147201019
        Public Const DeletePublishedWorkflowDefinitionWorkflowDependency As Integer = CInt(&H80045006) ' -2147201018
        Public Const UpdateWorkflowActivationWorkflowDependency As Integer = CInt(&H80045007) ' -2147201017
        Public Const UpdatePublishedWorkflowDefinitionWorkflowDependency As Integer = CInt(&H80045008) ' -2147201016
        Public Const CreateWorkflowActivationWorkflowDependency As Integer = CInt(&H80045009) ' -2147201015
        Public Const CreatePublishedWorkflowDefinitionWorkflowDependency As Integer = CInt(&H8004500a) ' -2147201014
        Public Const WorkflowPublishedByNonOwner As Integer = CInt(&H8004500b) ' -2147201013
        Public Const PublishedWorkflowOwnershipChange As Integer = CInt(&H8004500c) ' -2147201012
        Public Const OnlyWorkflowDefinitionOrTemplateCanBePublished As Integer = CInt(&H8004500d) ' -2147201011
        Public Const OnlyWorkflowDefinitionOrTemplateCanBeUnpublished As Integer = CInt(&H8004500e) ' -2147201010
        Public Const DeleteWorkflowActiveDefinition As Integer = CInt(&H8004500f) ' -2147201009
        Public Const WorkflowConditionIncorrectUnaryOperatorFormation As Integer = CInt(&H80045010) ' -2147201008
        Public Const WorkflowConditionIncorrectBinaryOperatorFormation As Integer = CInt(&H80045011) ' -2147201007
        Public Const WorkflowConditionOperatorNotSupported As Integer = CInt(&H80045012) ' -2147201006
        Public Const WorkflowConditionTypeNotSupport As Integer = CInt(&H80045013) ' -2147201005
        Public Const WorkflowValidationFailure As Integer = CInt(&H80045014) ' -2147201004
        Public Const PublishedWorkflowLimitForSkuReached As Integer = CInt(&H80045015) ' -2147201003
        Public Const NoPrivilegeToPublishWorkflow As Integer = CInt(&H80045016) ' -2147201002
        Public Const WorkflowSystemPaused As Integer = CInt(&H80045017) ' -2147201001
        Public Const WorkflowPublishNoActivationParameters As Integer = CInt(&H80045018) ' -2147201000
        Public Const CreateWorkflowDependencyForPublishedTemplate As Integer = CInt(&H80045019) ' -2147200999
        Public Const DeleteActiveWorkflowTemplateDependency As Integer = CInt(&H8004501a) ' -2147200998
        Public Const UpdatePublishedWorkflowTemplate As Integer = CInt(&H8004501b) ' -2147200997
        Public Const DeleteWorkflowActiveTemplate As Integer = CInt(&H8004501c) ' -2147200996
        Public Const CustomActivityInvalid As Integer = CInt(&H8004501d) ' -2147200995
        Public Const PrimaryEntityInvalid As Integer = CInt(&H8004501e) ' -2147200994
        Public Const CannotDeserializeWorkflowInstance As Integer = CInt(&H8004501f) ' -2147200993
        Public Const CannotDeserializeXamlWorkflow As Integer = CInt(&H80045020) ' -2147200992
        Public Const CannotDeleteCustomEntityUsedInWorkflow As Integer = CInt(&H8004502c) ' -2147200980
        Public Const BulkMailOperationFailed As Integer = CInt(&H8004502d) ' -2147200979
        Public Const WorkflowExpressionOperatorNotSupported As Integer = CInt(&H8004502e) ' -2147200978
        Public Const ChildWorkflowNotFound As Integer = CInt(&H8004502f) ' -2147200977
        Public Const CannotDeleteAttributeUsedInWorkflow As Integer = CInt(&H80045030) ' -2147200976
        Public Const CannotLocateRecordForWorkflowActivity As Integer = CInt(&H80045031) ' -2147200975
        Public Const PublishWorkflowWhileActingOnBehalfOfAnotherUserError As Integer = CInt(&H80045032) ' -2147200974
        Public Const CannotDisableInternetMarketingUser As Integer = CInt(&H80045033) ' -2147200973
        Public Const CannotSetWindowsLiveIdForInternetMarketingUser As Integer = CInt(&H80045034) ' -2147200972
        Public Const CannotChangeAccessModeForInternetMarketingUser As Integer = CInt(&H80045035) ' -2147200971
        Public Const CannotChangeInvitationStatusForInternetMarketingUser As Integer = CInt(&H80045036) ' -2147200970
        Public Const UIDataGenerationFailed As Integer = CInt(&H80045037) ' -2147200969
        Public Const WorkflowReferencesInvalidActivity As Integer = CInt(&H80045038) ' -2147200968
        Public Const PublishWorkflowWhileImpersonatingError As Integer = CInt(&H80045039) ' -2147200967
        Public Const ExchangeAutodiscoverError As Integer = CInt(&H8004503a) ' -2147200966
        Public Const NonCrmUIWorkflowsNotSupported As Integer = CInt(&H80045040) ' -2147200960
        Public Const NotEnoughPrivilegesForXamlWorkflows As Integer = CInt(&H80045041) ' -2147200959
        Public Const WorkflowAutomaticallyDeactivated As Integer = CInt(&H80045042) ' -2147200958
        Public Const StepAutomaticallyDisabled As Integer = CInt(&H80045043) ' -2147200957
        Public Const NonCrmUIInteractiveWorkflowNotSupported As Integer = CInt(&H80045044) ' -2147200956
        Public Const WorkflowActivityNotSupported As Integer = CInt(&H80045045) ' -2147200955
        Public Const ExecuteNotOnDemandWorkflow As Integer = CInt(&H80045046) ' -2147200954
        Public Const ExecuteUnpublishedWorkflow As Integer = CInt(&H80045047) ' -2147200953
        Public Const ChildWorkflowParameterMismatch As Integer = CInt(&H80045048) ' -2147200952
        Public Const InvalidProcessStateData As Integer = CInt(&H80045049) ' -2147200951
        Public Const OutOfScopeSlug As Integer = CInt(&H80045050) ' -2147200944
		Public Const CustomWorkflowActivitiesNotSupported As Integer = CInt(&H80045051)	' -2147200943
		Public Const CrmSqlGovernorDatabaseRequestDenied As Integer = CInt(&H8004A001) ' -2147180543
        Public Const InvalidAuthTicket As Integer = CInt(&H8004a100) ' -2147180288
        Public Const ExpiredAuthTicket As Integer = CInt(&H8004a101) ' -2147180287
        Public Const BadAuthTicket As Integer = CInt(&H8004a102) ' -2147180286
        Public Const InsufficientAuthTicket As Integer = CInt(&H8004a103) ' -2147180285
        Public Const OrganizationDisabled As Integer = CInt(&H8004a104) ' -2147180284
        Public Const TamperedAuthTicket As Integer = CInt(&H8004a105) ' -2147180283
        Public Const ExpiredKey As Integer = CInt(&H8004a106) ' -2147180282
        Public Const ScaleGroupDisabled As Integer = CInt(&H8004a107) ' -2147180281
        Public Const SupportLogOnExpired As Integer = CInt(&H8004a108) ' -2147180280
        Public Const InvalidPartnerSolutionCustomizationProvider As Integer = CInt(&H8004a109) ' -2147180279
        Public Const MultiplePartnerSecurityRoleWithSameInformation As Integer = CInt(&H8004a10a) ' -2147180278
        Public Const MultiplePartnerUserWithSameInformation As Integer = CInt(&H8004a10b) ' -2147180277
        Public Const MultipleRootBusinessUnit As Integer = CInt(&H8004a10c) ' -2147180276
        Public Const CannotDeletePartnerWithPartnerSolutions As Integer = CInt(&H8004a10d) ' -2147180275
        Public Const CannotDeletePartnerSolutionWithOrganizations As Integer = CInt(&H8004a10e) ' -2147180274
        Public Const CannotProvisionPartnerSolution As Integer = CInt(&H8004a10f) ' -2147180273
        Public Const CannotActOnBehalfOfAnotherUser As Integer = CInt(&H8004a110) ' -2147180272
        Public Const SystemUserDisabled As Integer = CInt(&H8004a112) ' -2147180270
        Public Const PluginDoesNotImplementCorrectInterface As Integer = CInt(&H8004a200) ' -2147180032
        Public Const CannotCreatePluginInstance As Integer = CInt(&H8004a201) ' -2147180031
        Public Const CrmLiveGenericError As Integer = CInt(&H8004b000) ' -2147176448
        Public Const CrmLiveOrganizationProvisioningFailed As Integer = CInt(&H8004b001) ' -2147176447
        Public Const CrmLiveMissingActiveDirectoryGroup As Integer = CInt(&H8004b002) ' -2147176446
        Public Const CrmLiveInternalProvisioningError As Integer = CInt(&H8004b003) ' -2147176445
        Public Const CrmLiveQueueItemDoesNotExist As Integer = CInt(&H8004b004) ' -2147176444
        Public Const CrmLiveInvalidSetupParameter As Integer = CInt(&H8004b005) ' -2147176443
        Public Const CrmLiveMultipleWitnessServersInScaleGroup As Integer = CInt(&H8004b006) ' -2147176442
        Public Const CrmLiveMissingServerRolesInScaleGroup As Integer = CInt(&H8004b007) ' -2147176441
        Public Const CrmLiveServerCannotHaveWitnessAndDataServerRoles As Integer = CInt(&H8004b008) ' -2147176440
        Public Const IsNotLiveToSendInvitation As Integer = CInt(&H8004b009) ' -2147176439
        Public Const MissingOrganizationFriendlyName As Integer = CInt(&H8004b00a) ' -2147176438
        Public Const MissingOrganizationUniqueName As Integer = CInt(&H8004b00b) ' -2147176437
        Public Const OfferingCategoryAndTokenNull As Integer = CInt(&H8004b00c) ' -2147176436
        Public Const OfferingIdNotSupported As Integer = CInt(&H8004b00d) ' -2147176435
        Public Const OrganizationTakenByYou As Integer = CInt(&H8004b00e) ' -2147176434
        Public Const OrganizationTakenBySomeoneElse As Integer = CInt(&H8004b00f) ' -2147176433
        Public Const InvalidTemplate As Integer = CInt(&H8004b010) ' -2147176432
        Public Const InvalidUserQuota As Integer = CInt(&H8004b011) ' -2147176431
        Public Const InvalidRole As Integer = CInt(&H8004b012) ' -2147176430
        Public Const ErrorGeneratingInvitation As Integer = CInt(&H8004b013) ' -2147176429
        Public Const CrmLiveOrganizationUpgradeFailed As Integer = CInt(&H8004b014) ' -2147176428
        Public Const UnableToSendEmail As Integer = CInt(&H8004b015) ' -2147176427
        Public Const InvalidEmail As Integer = CInt(&H8004b016) ' -2147176426
        Public Const VersionMismatch As Integer = CInt(&H8004b020) ' -2147176416
        Public Const MissingParameterToMethod As Integer = CInt(&H8004b021) ' -2147176415
        Public Const InvalidValueForCountryCode As Integer = CInt(&H8004b022) ' -2147176414
        Public Const InvalidValueForCurrency As Integer = CInt(&H8004b023) ' -2147176413
        Public Const InvalidValueForLocale As Integer = CInt(&H8004b024) ' -2147176412
        Public Const CrmLiveSupportOrganizationExistsInScaleGroup As Integer = CInt(&H8004b025) ' -2147176411
        Public Const CrmLiveMonitoringOrganizationExistsInScaleGroup As Integer = CInt(&H8004b026) ' -2147176410
        Public Const InvalidUserLicenseCount As Integer = CInt(&H8004b027) ' -2147176409
        Public Const MissingColumn As Integer = CInt(&H8004b028) ' -2147176408
        Public Const InvalidResourceType As Integer = CInt(&H8004b029) ' -2147176407
        Public Const InvalidMinimumResourceLimit As Integer = CInt(&H8004b02a) ' -2147176406
        Public Const InvalidMaximumResourceLimit As Integer = CInt(&H8004b02b) ' -2147176405
        Public Const ConflictingProvisionTypes As Integer = CInt(&H8004b02c) ' -2147176404
        Public Const InvalidAmountProvided As Integer = CInt(&H8004b02d) ' -2147176403
        Public Const CrmLiveOrganizationDeleteFailed As Integer = CInt(&H8004b02e) ' -2147176402
        Public Const OnlyDisabledOrganizationCanBeDeleted As Integer = CInt(&H8004b02f) ' -2147176401
        Public Const CrmLiveOrganizationDetailsNotFound As Integer = CInt(&H8004b030) ' -2147176400
        Public Const CrmLiveOrganizationFriendlyNameTooShort As Integer = CInt(&H8004b031) ' -2147176399
        Public Const CrmLiveOrganizationFriendlyNameTooLong As Integer = CInt(&H8004b032) ' -2147176398
        Public Const CrmLiveOrganizationUniqueNameTooShort As Integer = CInt(&H8004b033) ' -2147176397
        Public Const CrmLiveOrganizationUniqueNameTooLong As Integer = CInt(&H8004b034) ' -2147176396
        Public Const CrmLiveOrganizationUniqueNameInvalid As Integer = CInt(&H8004b035) ' -2147176395
        Public Const CrmLiveOrganizationUniqueNameReserved As Integer = CInt(&H8004b036) ' -2147176394
        Public Const ValueParsingError As Integer = CInt(&H8004b037) ' -2147176393
        Public Const InvalidGranularityValue As Integer = CInt(&H8004b038) ' -2147176392
        Public Const CrmLiveInvalidQueueItemSchedule As Integer = CInt(&H8004b039) ' -2147176391
        Public Const CrmLiveQueueItemTimeInPast As Integer = CInt(&H8004b040) ' -2147176384
        Public Const CrmLiveUnknownSku As Integer = CInt(&H8004b041) ' -2147176383
        Public Const ExceedCustomEntityQuota As Integer = CInt(&H8004b042) ' -2147176382
        Public Const ImportWillExceedCustomEntityQuota As Integer = CInt(&H8004b043) ' -2147176381
        Public Const OrganizationMigrationUnderway As Integer = CInt(&H8004b044) ' -2147176380
        Public Const CrmLiveInvoicingAccountIdMissing As Integer = CInt(&H8004b045) ' -2147176379
        Public Const CrmLiveDuplicateWindowsLiveId As Integer = CInt(&H8004b046) ' -2147176378
        Public Const CrmLiveDnsDomainNotFound As Integer = CInt(&H8004b047) ' -2147176377
        Public Const CrmLiveDnsDomainAlreadyExists As Integer = CInt(&H8004b048) ' -2147176376
        Public Const InvalidInteractiveUserQuota As Integer = CInt(&H8004b049) ' -2147176375
        Public Const InvalidNonInteractiveUserQuota As Integer = CInt(&H8004b050) ' -2147176368
        Public Const CrmLiveCannotFindExternalMessageProvider As Integer = CInt(&H8004b051) ' -2147176367
        Public Const CrmLiveInvalidExternalMessageData As Integer = CInt(&H8004b052) ' -2147176366
        Public Const CrmLiveOrganizationEnableFailed As Integer = CInt(&H8004b053) ' -2147176365
        Public Const CrmLiveOrganizationDisableFailed As Integer = CInt(&H8004b054) ' -2147176364
        Public Const CrmLiveAddOnUnexpectedError As Integer = CInt(&H8004b055) ' -2147176363
        Public Const CrmLiveAddOnAddLicenseLimitReached As Integer = CInt(&H8004b056) ' -2147176362
        Public Const CrmLiveAddOnAddStorageLimitReached As Integer = CInt(&H8004b057) ' -2147176361
        Public Const CrmLiveAddOnRemoveStorageLimitReached As Integer = CInt(&H8004b058) ' -2147176360
        Public Const CrmLiveAddOnOrgInNoUpdateMode As Integer = CInt(&H8004b059) ' -2147176359
        Public Const CrmLiveUnknownCategory As Integer = CInt(&H8004b05a) ' -2147176358
        Public Const CrmLiveInvalidInvoicingAccountNumber As Integer = CInt(&H8004b05b) ' -2147176357
        Public Const CrmLiveAddOnDataChanged As Integer = CInt(&H8004b05c) ' -2147176356
        Public Const CrmLiveInvalidEmail As Integer = CInt(&H8004b05d) ' -2147176355
        Public Const CrmLiveInvalidPhone As Integer = CInt(&H8004b05e) ' -2147176354
        Public Const CrmLiveInvalidZipCode As Integer = CInt(&H8004b05f) ' -2147176353
        Public Const InvalidAmountFreeResourceLimit As Integer = CInt(&H8004b060) ' -2147176352
        Public Const InvalidToken As Integer = CInt(&H8004b061) ' -2147176351
        Public Const CrmLiveRegisterCustomCodeDisabled As Integer = CInt(&H8004b062) ' -2147176350
        Public Const CrmLiveExecuteCustomCodeDisabled As Integer = CInt(&H8004b063) ' -2147176349
        Public Const CrmLiveInvalidTaxId As Integer = CInt(&H8004b064) ' -2147176348
        Public Const DatacenterNotAvailable As Integer = CInt(&H8004b065) ' -2147176347
		Public Const ErrorConnectingToDiscoveryService As Integer = CInt(&H8004B066) ' -2147176346
		Public Const OrgDoesNotExistInDiscoveryService As Integer = CInt(&H8004B067) ' -2147176345
		Public Const ErrorConnectingToOrganizationService As Integer = CInt(&H8004B068)	' -2147176344
		Public Const UserIsNotSystemAdminInOrganization As Integer = CInt(&H8004B069) ' -2147176343
		Public Const MobileServiceError As Integer = CInt(&H8004B070) ' -2147176336
		Public Const LivePlatformGeneralEmailError As Integer = CInt(&H8005B520) ' -2147109600
        Public Const LivePlatformEmailInvalidTo As Integer = CInt(&H8004b521) ' -2147175135
        Public Const LivePlatformEmailInvalidFrom As Integer = CInt(&H8004b522) ' -2147175134
        Public Const LivePlatformEmailInvalidSubject As Integer = CInt(&H8004b523) ' -2147175133
        Public Const LivePlatformEmailInvalidBody As Integer = CInt(&H8004b524) ' -2147175132
        Public Const BillingPartnerCertificate As Integer = CInt(&H8004b530) ' -2147175120
        Public Const BillingNoSettingError As Integer = CInt(&H8004b531) ' -2147175119
        Public Const BillingTestConnectionError As Integer = CInt(&H8004b532) ' -2147175118
        Public Const BillingTestConnectionException As Integer = CInt(&H8004b533) ' -2147175117
        Public Const BillingUserPuidNullError As Integer = CInt(&H8004b534) ' -2147175116
        Public Const BillingUnmappedErrorCode As Integer = CInt(&H8004b535) ' -2147175115
        Public Const BillingUnknownErrorCode As Integer = CInt(&H8004b536) ' -2147175114
        Public Const BillingUnknownException As Integer = CInt(&H8004b537) ' -2147175113
        Public Const BillingRetrieveKeyError As Integer = CInt(&H8004b538) ' -2147175112
        Public Const BDK_E_ADDRESS_VALIDATION_FAILURE As Integer = CInt(&H8004b540) ' -2147175104
        Public Const BDK_E_AGREEMENT_ALREADY_SIGNED As Integer = CInt(&H8004b541) ' -2147175103
        Public Const BDK_E_AUTHORIZATION_FAILED As Integer = CInt(&H8004b542) ' -2147175102
        Public Const BDK_E_AVS_FAILED As Integer = CInt(&H8004b543) ' -2147175101
        Public Const BDK_E_BAD_CITYNAME_LENGTH As Integer = CInt(&H8004b544) ' -2147175100
        Public Const BDK_E_BAD_STATECODE_LENGTH As Integer = CInt(&H8004b545) ' -2147175099
        Public Const BDK_E_BAD_ZIPCODE_LENGTH As Integer = CInt(&H8004b546) ' -2147175098
        Public Const BDK_E_BADXML As Integer = CInt(&H8004b547) ' -2147175097
        Public Const BDK_E_BANNED_PAYMENT_INSTRUMENT As Integer = CInt(&H8004b548) ' -2147175096
        Public Const BDK_E_BANNEDPERSON As Integer = CInt(&H8004b549) ' -2147175095
        Public Const BDK_E_CANNOT_EXCEED_MAX_OWNERSHIP As Integer = CInt(&H8004b54a) ' -2147175094
        Public Const BDK_E_COUNTRY_CURRENCY_PI_MISMATCH As Integer = CInt(&H8004b54b) ' -2147175093
        Public Const BDK_E_CREDIT_CARD_EXPIRED As Integer = CInt(&H8004b54c) ' -2147175092
        Public Const BDK_E_DATE_EXPIRED As Integer = CInt(&H8004b54d) ' -2147175091
        Public Const BDK_E_ERROR_COUNTRYCODE_MISMATCH As Integer = CInt(&H8004b54e) ' -2147175090
        Public Const BDK_E_ERROR_COUNTRYCODE_REQUIRED As Integer = CInt(&H8004b54f) ' -2147175089
        Public Const BDK_E_EXTRA_REFERRAL_DATA As Integer = CInt(&H8004b550) ' -2147175088
        Public Const BDK_E_GUID_EXISTS As Integer = CInt(&H8004b551) ' -2147175087
        Public Const BDK_E_INVALID_ADDRESS_ID As Integer = CInt(&H8004b552) ' -2147175086
        Public Const BDK_E_INVALID_BILLABLE_ACCOUNT_ID As Integer = CInt(&H8004b553) ' -2147175085
        Public Const BDK_E_INVALID_BUF_SIZE As Integer = CInt(&H8004b554) ' -2147175084
        Public Const BDK_E_INVALID_CATEGORY_NAME As Integer = CInt(&H8004b555) ' -2147175083
        Public Const BDK_E_INVALID_COUNTRY_CODE As Integer = CInt(&H8004b556) ' -2147175082
        Public Const BDK_E_INVALID_CURRENCY As Integer = CInt(&H8004b557) ' -2147175081
        Public Const BDK_E_INVALID_CUSTOMER_TYPE As Integer = CInt(&H8004b558) ' -2147175080
        Public Const BDK_E_INVALID_DATE As Integer = CInt(&H8004b559) ' -2147175079
        Public Const BDK_E_INVALID_EMAIL_ADDRESS As Integer = CInt(&H8004b55a) ' -2147175078
        Public Const BDK_E_INVALID_FILTER As Integer = CInt(&H8004b55b) ' -2147175077
        Public Const BDK_E_INVALID_GUID As Integer = CInt(&H8004b55c) ' -2147175076
        Public Const BDK_E_INVALID_INPUT_TO_TAXWARE_OR_VERAZIP As Integer = CInt(&H8004b55d) ' -2147175075
        Public Const BDK_E_INVALID_LOCALE As Integer = CInt(&H8004b55e) ' -2147175074
        Public Const BDK_E_INVALID_OBJECT_ID As Integer = CInt(&H8004b55f) ' -2147175073
        Public Const BDK_E_INVALID_OFFERING_GUID As Integer = CInt(&H8004b560) ' -2147175072
        Public Const BDK_E_INVALID_PAYMENT_INSTRUMENT_STATUS As Integer = CInt(&H8004b561) ' -2147175071
        Public Const BDK_E_INVALID_PAYMENT_METHOD_ID As Integer = CInt(&H8004b562) ' -2147175070
        Public Const BDK_E_INVALID_PHONE_TYPE As Integer = CInt(&H8004b563) ' -2147175069
        Public Const BDK_E_INVALID_POLICY_ID As Integer = CInt(&H8004b564) ' -2147175068
        Public Const BDK_E_INVALID_REFERRALDATA_XML As Integer = CInt(&H8004b565) ' -2147175067
        Public Const BDK_E_INVALID_STATE_FOR_COUNTRY As Integer = CInt(&H8004b566) ' -2147175066
        Public Const BDK_E_INVALID_SUBSCRIPTION_ID As Integer = CInt(&H8004b567) ' -2147175065
        Public Const BDK_E_INVALID_TAX_EXEMPT_TYPE As Integer = CInt(&H8004b568) ' -2147175064
        Public Const BDK_E_MEG_CONFLICT As Integer = CInt(&H8004b569) ' -2147175063
        Public Const BDK_E_MULTIPLE_CITIES_FOUND As Integer = CInt(&H8004b56a) ' -2147175062
        Public Const BDK_E_MULTIPLE_COUNTIES_FOUND As Integer = CInt(&H8004b56b) ' -2147175061
        Public Const BDK_E_NON_ACTIVE_ACCOUNT As Integer = CInt(&H8004b56c) ' -2147175060
        Public Const BDK_E_NOPERMISSION As Integer = CInt(&H8004b56d) ' -2147175059
        Public Const BDK_E_OBJECT_ROLE_LIMIT_EXCEEDED As Integer = CInt(&H8004b56e) ' -2147175058
        Public Const BDK_E_OFFERING_ACCOUNT_CURRENCY_MISMATCH As Integer = CInt(&H8004b56f) ' -2147175057
        Public Const BDK_E_OFFERING_COUNTRY_ACCOUNT_MISMATCH As Integer = CInt(&H8004b570) ' -2147175056
        Public Const BDK_E_OFFERING_NOT_PURCHASEABLE As Integer = CInt(&H8004b571) ' -2147175055
        Public Const BDK_E_OFFERING_PAYMENT_INSTRUMENT_MISMATCH As Integer = CInt(&H8004b572) ' -2147175054
        Public Const BDK_E_OFFERING_REQUIRES_PI As Integer = CInt(&H8004b573) ' -2147175053
        Public Const BDK_E_PARTNERNOTINBILLING As Integer = CInt(&H8004b574) ' -2147175052
        Public Const BDK_E_PAYMENT_PROVIDER_CONNECTION_FAILED As Integer = CInt(&H8004b575) ' -2147175051
        Public Const BDK_E_PRIMARY_PHONE_REQUIRED As Integer = CInt(&H8004b576) ' -2147175050
        Public Const BDK_E_POLICY_DEAL_COUNTRY_MISMATCH As Integer = CInt(&H8004b577) ' -2147175049
        Public Const BDK_E_PUID_ROLE_LIMIT_EXCEEDED As Integer = CInt(&H8004b578) ' -2147175048
        Public Const BDK_E_RATING_FAILURE As Integer = CInt(&H8004b579) ' -2147175047
        Public Const BDK_E_REQUIRED_FIELD_MISSING As Integer = CInt(&H8004b57a) ' -2147175046
        Public Const BDK_E_STATE_CITY_INVALID As Integer = CInt(&H8004b57b) ' -2147175045
        Public Const BDK_E_STATE_INVALID As Integer = CInt(&H8004b57c) ' -2147175044
        Public Const BDK_E_STATE_ZIP_CITY_INVALID As Integer = CInt(&H8004b57d) ' -2147175043
        Public Const BDK_E_STATE_ZIP_CITY_INVALID2 As Integer = CInt(&H8004b57e) ' -2147175042
        Public Const BDK_E_STATE_ZIP_CITY_INVALID3 As Integer = CInt(&H8004b57f) ' -2147175041
        Public Const BDK_E_STATE_ZIP_CITY_INVALID4 As Integer = CInt(&H8004b580) ' -2147175040
        Public Const BDK_E_STATE_ZIP_COVERS_MULTIPLE_CITIES As Integer = CInt(&H8004b581) ' -2147175039
        Public Const BDK_E_STATE_ZIP_INVALID As Integer = CInt(&H8004b582) ' -2147175038
        Public Const BDK_E_TAXID_EXPDATE As Integer = CInt(&H8004b583) ' -2147175037
        Public Const BDK_E_TOKEN_BLACKLISTED As Integer = CInt(&H8004b584) ' -2147175036
        Public Const BDK_E_TOKEN_EXPIRED As Integer = CInt(&H8004b585) ' -2147175035
        Public Const BDK_E_TOKEN_NOT_VALID_FOR_OFFERING As Integer = CInt(&H8004b586) ' -2147175034
        Public Const BDK_E_TOKEN_RANGE_BLACKLISTED As Integer = CInt(&H8004b587) ' -2147175033
        Public Const BDK_E_TRANS_BALANCE_TO_PI_INVALID As Integer = CInt(&H8004b588) ' -2147175032
        Public Const BDK_E_UNKNOWN_SERVER_FAILURE As Integer = CInt(&H8004b589) ' -2147175031
        Public Const BDK_E_UNSUPPORTED_CHAR_EXIST As Integer = CInt(&H8004b58a) ' -2147175030
        Public Const BDK_E_VATID_DOESNOTHAVEEXPDATE As Integer = CInt(&H8004b58b) ' -2147175029
        Public Const BDK_E_ZIP_CITY_MISSING As Integer = CInt(&H8004b58c) ' -2147175028
        Public Const BDK_E_ZIP_INVALID As Integer = CInt(&H8004b58d) ' -2147175027
        Public Const BDK_E_ZIP_INVALID_FOR_ENTERED_STATE As Integer = CInt(&H8004b58e) ' -2147175026
        Public Const BDK_E_USAGE_COUNT_FOR_TOKEN_EXCEEDED As Integer = CInt(&H8004b58f) ' -2147175025
        Public Const MissingParameterToStoredProcedure As Integer = CInt(&H8004c000) ' -2147172352
        Public Const SqlErrorInStoredProcedure As Integer = CInt(&H8004c001) ' -2147172351
        Public Const StoredProcedureContext As Integer = CInt(&H8004c002) ' -2147172350
        Public Const InvitingOrganizationNotFound As Integer = CInt(&H8004d200) ' -2147167744
        Public Const InvitingUserNotInOrganization As Integer = CInt(&H8004d201) ' -2147167743
        Public Const InvitedUserAlreadyExists As Integer = CInt(&H8004d202) ' -2147167742
        Public Const InvitedUserIsOrganization As Integer = CInt(&H8004d203) ' -2147167741
        Public Const InvitationNotFound As Integer = CInt(&H8004d204) ' -2147167740
        Public Const InvitedUserAlreadyAdded As Integer = CInt(&H8004d205) ' -2147167739
        Public Const InvitationWrongUserOrgRelation As Integer = CInt(&H8004d206) ' -2147167738
        Public Const InvitationIsExpired As Integer = CInt(&H8004d207) ' -2147167737
        Public Const InvitationIsAccepted As Integer = CInt(&H8004d208) ' -2147167736
        Public Const InvitationIsRejected As Integer = CInt(&H8004d209) ' -2147167735
        Public Const InvitationIsRevoked As Integer = CInt(&H8004d20a) ' -2147167734
        Public Const InvitedUserMultipleTimes As Integer = CInt(&H8004d20b) ' -2147167733
        Public Const InvitationStatusError As Integer = CInt(&H8004d20c) ' -2147167732
        Public Const InvalidInvitationToken As Integer = CInt(&H8004d20d) ' -2147167731
        Public Const InvalidInvitationLiveId As Integer = CInt(&H8004d20e) ' -2147167730
        Public Const InvitationSendToSelf As Integer = CInt(&H8004d20f) ' -2147167729
        Public Const InvitationCannotBeReset As Integer = CInt(&H8004d210) ' -2147167728
        Public Const UserDataNotFound As Integer = CInt(&H8004d211) ' -2147167727
        Public Const CannotInviteDisabledUser As Integer = CInt(&H8004d212) ' -2147167726
        Public Const InvitationBillingAdminUnknown As Integer = CInt(&H8004d213) ' -2147167725
        Public Const CannotResetSysAdminInvite As Integer = CInt(&H8004d214) ' -2147167724
        Public Const CannotSendInviteToDuplicateWindowsLiveId As Integer = CInt(&H8004d215) ' -2147167723
        Public Const UserInviteDisabled As Integer = CInt(&H8004d216) ' -2147167722
        Public Const InvitationOrganizationNotEnabled As Integer = CInt(&H8004d217) ' -2147167721
        Public Const ClientAuthSignedOut As Integer = CInt(&H8004d221) ' -2147167711
        Public Const ClientAuthSyncIssue As Integer = CInt(&H8004d223) ' -2147167709
        Public Const ClientAuthCanceled As Integer = CInt(&H8004d224) ' -2147167708
        Public Const ClientAuthNoConnectivityOffline As Integer = CInt(&H8004d225) ' -2147167707
        Public Const ClientAuthNoConnectivity As Integer = CInt(&H8004d226) ' -2147167706
        Public Const ClientAuthOfflineInvalidCallerId As Integer = CInt(&H8004d227) ' -2147167705
        Public Const ConfigDBObjectDoesNotExist As Integer = CInt(&H8004d230) ' -2147167696
        Public Const ConfigDBDuplicateRecord As Integer = CInt(&H8004d231) ' -2147167695
        Public Const ConfigDBCannotDeleteObjectDueState As Integer = CInt(&H8004d232) ' -2147167694
        Public Const ConfigDBCascadeDeleteNotAllowDelete As Integer = CInt(&H8004d233) ' -2147167693
        Public Const MoveBothToPrimary As Integer = CInt(&H8004d234) ' -2147167692
        Public Const MoveBothToSecondary As Integer = CInt(&H8004d235) ' -2147167691
        Public Const MoveOrganizationFailedNotDisabled As Integer = CInt(&H8004d236) ' -2147167690
        Public Const ConfigDBCannotUpdateObjectDueState As Integer = CInt(&H8004d237) ' -2147167689
        Public Const LiveAdminUnknownObject As Integer = CInt(&H8004d238) ' -2147167688
        Public Const LiveAdminUnknownCommand As Integer = CInt(&H8004d239) ' -2147167687
        Public Const OperationOrganizationNotFullyDisabled As Integer = CInt(&H8004d23a) ' -2147167686
        Public Const ConfigDBCannotDeleteDefaultOrganization As Integer = CInt(&H8004d23b) ' -2147167685
        Public Const LicenseNotEnoughToActivate As Integer = CInt(&H80042f14) ' -2147209452
        Public Const UserNotAssignedRoles As Integer = CInt(&H80042f09) ' -2147209463
        Public Const TeamNotAssignedRoles As Integer = CInt(&H80042f0a) ' -2147209462
        Public Const InvalidLicenseKey As Integer = CInt(&H8004d240) ' -2147167680
        Public Const NoLicenseInConfigDB As Integer = CInt(&H8004d241) ' -2147167679
        Public Const InvalidLicensePid As Integer = CInt(&H8004d242) ' -2147167678
        Public Const InvalidLicensePidGenCannotLoad As Integer = CInt(&H8004d243) ' -2147167677
        Public Const InvalidLicensePidGenOtherError As Integer = CInt(&H8004d244) ' -2147167676
        Public Const InvalidLicenseCannotReadMpcFile As Integer = CInt(&H8004d245) ' -2147167675
        Public Const InvalidLicenseMpcCode As Integer = CInt(&H8004d246) ' -2147167674
        Public Const LicenseUpgradePathNotAllowed As Integer = CInt(&H8004d247) ' -2147167673
        Public Const OrgsInaccessible As Integer = CInt(&H8004d24a) ' -2147167670
        Public Const UserNotAssignedLicense As Integer = CInt(&H8004d24b) ' -2147167669
        Public Const UserCannotEnableWithoutLicense As Integer = CInt(&H8004d24c) ' -2147167668
        Public Const LicenseConfigFileInvalid As Integer = CInt(&H8004d250) ' -2147167664
        Public Const LicenseTrialExpired As Integer = CInt(&H8004415c) ' -2147204772
        Public Const LicenseRegistrationExpired As Integer = CInt(&H8004415d) ' -2147204771
        Public Const LicenseTampered As Integer = CInt(&H8004415f) ' -2147204769
        Public Const NonInteractiveUserCannotAccessUI As Integer = CInt(&H80044160) ' -2147204768
        Public Const InvalidOrganizationUniqueName As Integer = CInt(&H8004d251) ' -2147167663
        Public Const InvalidOrganizationFriendlyName As Integer = CInt(&H8004d252) ' -2147167662
        Public Const OrganizationNotConfigured As Integer = CInt(&H8004d253) ' -2147167661
        Public Const InvalidDeviceToConfigureOrganization As Integer = CInt(&H8004d254) ' -2147167660
        Public Const InvalidBrowserToConfigureOrganization As Integer = CInt(&H8004d255) ' -2147167659
        Public Const DeploymentServiceNotAllowSetToThisState As Integer = CInt(&H8004d260) ' -2147167648
        Public Const DeploymentServiceNotAllowOperation As Integer = CInt(&H8004d261) ' -2147167647
        Public Const DeploymentServiceCannotChangeStateForDeploymentService As Integer = CInt(&H8004d262) ' -2147167646
        Public Const DeploymentServiceRequestValidationFailure As Integer = CInt(&H8004d263) ' -2147167645
        Public Const DeploymentServiceOperationIdentifierNotFound As Integer = CInt(&H8004d264) ' -2147167644
        Public Const DeploymentServiceCannotDeleteOperationInProgress As Integer = CInt(&H8004d265) ' -2147167643
        Public Const ConfigureClaimsBeforeIfd As Integer = CInt(&H8004d266) ' -2147167642
        Public Const EndUserNotificationTypeNotValidForEmail As Integer = CInt(&H8004d291) ' -2147167599
        Public Const InvalidRecurrenceRuleForBulkDeleteAndDuplicateDetection As Integer = CInt(&H8004d2a0) ' -2147167584
        Public Const InvalidRecurrenceInterval As Integer = CInt(&H8004d2a1) ' -2147167583
        Public Const QueriesForDifferentEntities As Integer = CInt(&H8004d2b0) ' -2147167568
        Public Const AggregateInnerQuery As Integer = CInt(&H8004d2b1) ' -2147167567
        Public Const InvalidDataDescription As Integer = CInt(&H8004e000) ' -2147164160
        Public Const NonPrimaryEntityDataDescriptionFound As Integer = CInt(&H8004e001) ' -2147164159
        Public Const InvalidPresentationDescription As Integer = CInt(&H8004e002) ' -2147164158
        Public Const SeriesMeasureCollectionMismatch As Integer = CInt(&H8004e003) ' -2147164157
        Public Const YValuesPerPointMeasureMismatch As Integer = CInt(&H8004e004) ' -2147164156
        Public Const ChartAreaCategoryMismatch As Integer = CInt(&H8004e005) ' -2147164155
        Public Const MultipleSubcategoriesFound As Integer = CInt(&H8004e006) ' -2147164154
        Public Const MultipleMeasuresFound As Integer = CInt(&H8004e007) ' -2147164153
        Public Const MultipleChartAreasFound As Integer = CInt(&H8004e008) ' -2147164152
        Public Const InvalidCategory As Integer = CInt(&H8004e009) ' -2147164151
        Public Const InvalidMeasureCollection As Integer = CInt(&H8004e00a) ' -2147164150
        Public Const DuplicateAliasFound As Integer = CInt(&H8004e00b) ' -2147164149
        Public Const EntityNotEnabledForCharts As Integer = CInt(&H8004e00c) ' -2147164148
        Public Const InvalidPageResponse As Integer = CInt(&H8004e00d) ' -2147164147
        Public Const VisualizationRenderingError As Integer = CInt(&H8004e00e) ' -2147164146
        Public Const InvalidGroupByAlias As Integer = CInt(&H8004e00f) ' -2147164145
        Public Const MeasureDataTypeInvalid As Integer = CInt(&H8004e010) ' -2147164144
        Public Const NoDataForVisualization As Integer = CInt(&H8004e011) ' -2147164143
        Public Const VisualizationModuleNotFound As Integer = CInt(&H8004e012) ' -2147164142
        Public Const ImportVisualizationDeletedError As Integer = CInt(&H8004e013) ' -2147164141
        Public Const ImportVisualizationExistingError As Integer = CInt(&H8004e014) ' -2147164140
        Public Const VisualizationOtcNotFoundError As Integer = CInt(&H8004e015) ' -2147164139
        Public Const InvalidDundasPresentationDescription As Integer = CInt(&H8004e016) ' -2147164138
        Public Const InvalidWebResourceForVisualization As Integer = CInt(&H8004e017) ' -2147164137
        Public Const ChartTypeNotSupportedForComparisonChart As Integer = CInt(&H8004e018) ' -2147164136
        Public Const InvalidFetchCollection As Integer = CInt(&H8004e019) ' -2147164135
        Public Const CategoryDataTypeInvalid As Integer = CInt(&H8004e01a) ' -2147164134
        Public Const DuplicateGroupByFound As Integer = CInt(&H8004e01b) ' -2147164133
        Public Const MultipleMeasureCollectionsFound As Integer = CInt(&H8004e01c) ' -2147164132
        Public Const InvalidGroupByColumn As Integer = CInt(&H8004e01d) ' -2147164131
        Public Const InvalidFilterCriteriaForVisualization As Integer = CInt(&H8004e01e) ' -2147164130
        Public Const CountSpecifiedWithoutOrder As Integer = CInt(&H8004e01f) ' -2147164129
        Public Const NoPreviewForCustomWebResource As Integer = CInt(&H8004e020) ' -2147164128
        Public Const ChartTypeNotSupportedForMultipleSeriesChart As Integer = CInt(&H8004e021) ' -2147164127
        Public Const InsufficientColumnsInSubQuery As Integer = CInt(&H8004e022) ' -2147164126
        Public Const AggregateQueryRecordLimitExceeded As Integer = CInt(&H8004e023) ' -2147164125
        Public Const QuickFindQueryRecordLimitExceeded As Integer = CInt(&H8004e024) ' -2147164124
        Public Const CannotAddOrActonBehalfAnotherUserPrivilege As Integer = CInt(&H8004ed43) ' -2147160765
        Public Const HipNoSettingError As Integer = CInt(&H8004ed44) ' -2147160764
        Public Const HipInvalidCertificate As Integer = CInt(&H8004ed45) ' -2147160763
        Public Const NoSettingError As Integer = CInt(&H8004ed46) ' -2147160762
		Public Const AppLockTimeout As Integer = CInt(&H8004ED47) ' -2147160761
		Public Const InvalidRecurrencePattern As Integer = CInt(&H8004E100)	' -2147163904
        Public Const CreateRecurrenceRuleFailed As Integer = CInt(&H8004e101) ' -2147163903
        Public Const PartialExpansionSettingLoadError As Integer = CInt(&H8004e102) ' -2147163902
        Public Const InvalidCrmDateTime As Integer = CInt(&H8004e103) ' -2147163901
        Public Const InvalidAppointmentInstance As Integer = CInt(&H8004e104) ' -2147163900
        Public Const InvalidSeriesId As Integer = CInt(&H8004e105) ' -2147163899
        Public Const AppointmentDeleted As Integer = CInt(&H8004e106) ' -2147163898
        Public Const InvalidInstanceTypeCode As Integer = CInt(&H8004e107) ' -2147163897
        Public Const OverlappingInstances As Integer = CInt(&H8004e108) ' -2147163896
        Public Const InvalidSeriesIdOriginalStart As Integer = CInt(&H8004e109) ' -2147163895
        Public Const ValidateNotSupported As Integer = CInt(&H8004e10a) ' -2147163894
        Public Const RecurringSeriesCompleted As Integer = CInt(&H8004e10b) ' -2147163893
        Public Const ExpansionRequestIsOutsideExpansionWindow As Integer = CInt(&H8004e10c) ' -2147163892
        Public Const InvalidInstanceEntityName As Integer = CInt(&H8004e10d) ' -2147163891
        Public Const BookFirstInstanceFailed As Integer = CInt(&H8004e10e) ' -2147163890
        Public Const InvalidSeriesStatus As Integer = CInt(&H8004e10f) ' -2147163889
        Public Const RecurrenceRuleUpdateFailure As Integer = CInt(&H8004e110) ' -2147163888
        Public Const RecurrenceRuleDeleteFailure As Integer = CInt(&H8004e111) ' -2147163887
        Public Const EntityNotRule As Integer = CInt(&H8004e112) ' -2147163886
        Public Const RecurringSeriesMasterIsLocked As Integer = CInt(&H8004e113) ' -2147163885
        Public Const UpdateRecurrenceRuleFailed As Integer = CInt(&H8004e114) ' -2147163884
        Public Const InstanceOutsideEffectiveRange As Integer = CInt(&H8004e115) ' -2147163883
        Public Const RecurrenceCalendarTypeNotSupported As Integer = CInt(&H8004e116) ' -2147163882
        Public Const RecurrenceHasNoOccurrence As Integer = CInt(&H8004e117) ' -2147163881
        Public Const RecurrenceStartDateTooSmall As Integer = CInt(&H8004e118) ' -2147163880
        Public Const RecurrenceEndDateTooBig As Integer = CInt(&H8004e119) ' -2147163879
        Public Const OccurrenceCrossingBoundary As Integer = CInt(&H8004e120) ' -2147163872
        Public Const OccurrenceTimeSpanTooBig As Integer = CInt(&H8004e121) ' -2147163871
        Public Const OccurrenceSkipsOverForward As Integer = CInt(&H8004e122) ' -2147163870
        Public Const OccurrenceSkipsOverBackward As Integer = CInt(&H8004e123) ' -2147163869
        Public Const InvalidDaysInFebruary As Integer = CInt(&H8004e124) ' -2147163868
        Public Const InvalidOccurrenceNumber As Integer = CInt(&H8004e125) ' -2147163867
        Public Const InvalidNumberOfPartitions As Integer = CInt(&H8004e200) ' -2147163648
        Public Const InvalidElementFound As Integer = CInt(&H8004e300) ' -2147163392
        Public Const MaximumControlsLimitExceeded As Integer = CInt(&H8004e301) ' -2147163391
        Public Const UserViewsOrVisualizationsFound As Integer = CInt(&H8004e302) ' -2147163390
        Public Const InvalidAttributeFound As Integer = CInt(&H8004e303) ' -2147163389
        Public Const MultipleFormElementsFound As Integer = CInt(&H8004e304) ' -2147163388
        Public Const NullDashboardName As Integer = CInt(&H8004e305) ' -2147163387
        Public Const InvalidFormType As Integer = CInt(&H8004e306) ' -2147163386
        Public Const InvalidControlClass As Integer = CInt(&H8004e307) ' -2147163385
        Public Const ImportDashboardDeletedError As Integer = CInt(&H8004e308) ' -2147163384
        Public Const PersonalReportFound As Integer = CInt(&H8004e309) ' -2147163383
        Public Const ObjectAlreadyExists As Integer = CInt(&H8004e30a) ' -2147163382
        Public Const EntityTypeSpecifiedForDashboard As Integer = CInt(&H8004e30b) ' -2147163381
        Public Const UnrestrictedIFrameInUserDashboard As Integer = CInt(&H8004e30c) ' -2147163380
        Public Const MultipleLabelsInUserDashboard As Integer = CInt(&H8004e30d) ' -2147163379
        Public Const UnsupportedDashboardInEditor As Integer = CInt(&H8004e30e) ' -2147163378
		Public Const InvalidUrlProtocol As Integer = CInt(&H8004E30F) ' -2147163377
		Public Const CannotRemoveComponentFromDefaultSolution As Integer = CInt(&H8004F000)	' -2147160064
        Public Const InvalidSolutionUniqueName As Integer = CInt(&H8004f002) ' -2147160062
        Public Const CannotUndeleteLabel As Integer = CInt(&H8004f003) ' -2147160061
        Public Const ErrorReactivatingComponentInstance As Integer = CInt(&H8004f004) ' -2147160060
        Public Const CannotDeleteRestrictedSolution As Integer = CInt(&H8004f005) ' -2147160059
        Public Const CannotDeleteRestrictedPublisher As Integer = CInt(&H8004f006) ' -2147160058
        Public Const ImportRestrictedSolutionError As Integer = CInt(&H8004f007) ' -2147160057
        Public Const CannotSetSolutionSystemAttributes As Integer = CInt(&H8004f008) ' -2147160056
        Public Const CannotUpdateDefaultSolution As Integer = CInt(&H8004f009) ' -2147160055
        Public Const CannotUpdateRestrictedSolution As Integer = CInt(&H8004f00a) ' -2147160054
        Public Const CannotAddWorkflowActivationToSolution As Integer = CInt(&H8004f00c) ' -2147160052
        Public Const CannotQueryBaseTableWithAggregates As Integer = CInt(&H8004f00d) ' -2147160051
        Public Const InvalidStateTransition As Integer = CInt(&H8004f00e) ' -2147160050
        Public Const CannotUpdateUnpublishedDeleteInstance As Integer = CInt(&H8004f00f) ' -2147160049
        Public Const UnsupportedComponentOperation As Integer = CInt(&H8004f010) ' -2147160048
        Public Const InvalidCreateOnProtectedComponent As Integer = CInt(&H8004f011) ' -2147160047
        Public Const InvalidUpdateOnProtectedComponent As Integer = CInt(&H8004f012) ' -2147160046
        Public Const InvalidDeleteOnProtectedComponent As Integer = CInt(&H8004f013) ' -2147160045
        Public Const InvalidPublishOnProtectedComponent As Integer = CInt(&H8004f014) ' -2147160044
        Public Const CannotAddNonCustomizableComponent As Integer = CInt(&H8004f015) ' -2147160043
        Public Const CannotOverwriteActiveComponent As Integer = CInt(&H8004f016) ' -2147160042
        Public Const CannotUpdateRestrictedPublisher As Integer = CInt(&H8004f017) ' -2147160041
        Public Const CannotAddSolutionComponentWithoutRoots As Integer = CInt(&H8004f018) ' -2147160040
        Public Const ComponentDefinitionDoesNotExists As Integer = CInt(&H8004f019) ' -2147160039
        Public Const DependencyAlreadyExists As Integer = CInt(&H8004f01a) ' -2147160038
        Public Const DependencyTableNotEmpty As Integer = CInt(&H8004f01b) ' -2147160037
        Public Const InvalidPublisherUniqueName As Integer = CInt(&H8004f01c) ' -2147160036
        Public Const CannotUninstallWithDependencies As Integer = CInt(&H8004f01d) ' -2147160035
        Public Const InvalidSolutionVersion As Integer = CInt(&H8004f01e) ' -2147160034
        Public Const CannotDeleteInUseComponent As Integer = CInt(&H8004f01f) ' -2147160033
        Public Const CannotUninstallReferencedProtectedSolution As Integer = CInt(&H8004f020) ' -2147160032
        Public Const CannotRemoveComponentFromSolution As Integer = CInt(&H8004f021) ' -2147160031
        Public Const RestrictedSolutionName As Integer = CInt(&H8004f022) ' -2147160030
        Public Const SolutionUniqueNameViolation As Integer = CInt(&H8004f023) ' -2147160029
        Public Const CannotUpdateManagedSolution As Integer = CInt(&H8004f024) ' -2147160028
        Public Const DependencyTrackingClosed As Integer = CInt(&H8004f025) ' -2147160027
        Public Const GenericManagedPropertyFailure As Integer = CInt(&H8004f026) ' -2147160026
        Public Const CombinedManagedPropertyFailure As Integer = CInt(&H8004f027) ' -2147160025
        Public Const ReportImportCategoryOptionNotFound As Integer = CInt(&H8004f028) ' -2147160024
        Public Const RequiredChildReportHasOtherParent As Integer = CInt(&H8004f029) ' -2147160023
        Public Const InvalidManagedPropertyException As Integer = CInt(&H8004f030) ' -2147160016
        Public Const OnlyOwnerCanSetManagedProperties As Integer = CInt(&H8004f031) ' -2147160015
        Public Const CannotDeleteMetadata As Integer = CInt(&H8004f032) ' -2147160014
        Public Const CannotUpdateReadOnlyPublisher As Integer = CInt(&H8004f033) ' -2147160013
        Public Const CannotSelectReadOnlyPublisher As Integer = CInt(&H8004f034) ' -2147160012
        Public Const CannotRemoveComponentFromSystemSolution As Integer = CInt(&H8004f035) ' -2147160011
        Public Const InvalidDependency As Integer = CInt(&H8004f036) ' -2147160010
        Public Const InvalidDependencyFetchXml As Integer = CInt(&H8004f037) ' -2147160009
        Public Const CannotModifyReportOutsideSolutionIfManaged As Integer = CInt(&H8004f038) ' -2147160008
        Public Const SharePointUnableToAddUserToGroup As Integer = CInt(&H8004f0f1) ' -2147159823
        Public Const SharePointUnableToRemoveUserFromGroup As Integer = CInt(&H8004f0f2) ' -2147159822
        Public Const SharePointSiteNotPresentInSharePoint As Integer = CInt(&H8004f0f3) ' -2147159821
        Public Const SharePointUnableToRetrieveGroup As Integer = CInt(&H8004f0f4) ' -2147159820
        Public Const SharePointUnableToAclSiteWithPrivilege As Integer = CInt(&H8004f0f5) ' -2147159819
        Public Const SharePointUnableToAclSite As Integer = CInt(&H8004f0f6) ' -2147159818
        Public Const SharePointUnableToCreateSiteGroup As Integer = CInt(&H8004f0f7) ' -2147159817
        Public Const SharePointSiteCreationFailure As Integer = CInt(&H8004f0f8) ' -2147159816
        Public Const SharePointTeamProvisionJobAlreadyExists As Integer = CInt(&H8004f0f9) ' -2147159815
        Public Const SharePointRoleProvisionJobAlreadyExists As Integer = CInt(&H8004f0fa) ' -2147159814
        Public Const SharePointSiteWideProvisioningJobFailed As Integer = CInt(&H8004f0fb) ' -2147159813
        Public Const DataTypeMismatchForLinkedAttribute As Integer = CInt(&H8004f0fc) ' -2147159812
        Public Const InvalidEntityForLinkedAttribute As Integer = CInt(&H8004f0fd) ' -2147159811
        Public Const AlreadyLinkedToAnotherAttribute As Integer = CInt(&H8004f0fe) ' -2147159810
        Public Const DocumentManagementDisabled As Integer = CInt(&H8004f0ff) ' -2147159809
        Public Const DefaultSiteCollectionUrlChanged As Integer = CInt(&H8004f100) ' -2147159808
        Public Const RibbonImportHidingBasicHomeTab As Integer = CInt(&H8004f101) ' -2147159807
        Public Const RibbonImportInvalidPrivilegeName As Integer = CInt(&H8004f102) ' -2147159806
        Public Const RibbonImportEntityNotSupported As Integer = CInt(&H8004f103) ' -2147159805
        Public Const RibbonImportDependencyMissingEntity As Integer = CInt(&H8004f104) ' -2147159804
        Public Const RibbonImportDependencyMissingRibbonElement As Integer = CInt(&H8004f105) ' -2147159803
        Public Const RibbonImportDependencyMissingWebResource As Integer = CInt(&H8004f106) ' -2147159802
        Public Const RibbonImportDependencyMissingRibbonControl As Integer = CInt(&H8004f107) ' -2147159801
        Public Const RibbonImportModifyingTopLevelNode As Integer = CInt(&H8004f108) ' -2147159800
        Public Const RibbonImportLocationAndIdDoNotMatch As Integer = CInt(&H8004f109) ' -2147159799
        Public Const RibbonImportHidingJewel As Integer = CInt(&H8004f10a) ' -2147159798
        Public Const RibbonImportDuplicateElementId As Integer = CInt(&H8004f10b) ' -2147159797
        Public Const WebResourceInvalidType As Integer = CInt(&H8004f111) ' -2147159791
        Public Const WebResourceEmptySilverlightVersion As Integer = CInt(&H8004f112) ' -2147159790
        Public Const WebResourceInvalidSilverlightVersion As Integer = CInt(&H8004f113) ' -2147159789
        Public Const WebResourceContentSizeExceeded As Integer = CInt(&H8004f114) ' -2147159788
        Public Const WebResourceDuplicateName As Integer = CInt(&H8004f115) ' -2147159787
        Public Const WebResourceEmptyName As Integer = CInt(&H8004f116) ' -2147159786
        Public Const WebResourceNameInvalidCharacters As Integer = CInt(&H8004f117) ' -2147159785
        Public Const WebResourceNameInvalidPrefix As Integer = CInt(&H8004f118) ' -2147159784
        Public Const WebResourceNameInvalidFileExtension As Integer = CInt(&H8004f119) ' -2147159783
        Public Const WebResourceImportMissingFile As Integer = CInt(&H8004f11a) ' -2147159782
        Public Const WebResourceImportError As Integer = CInt(&H8004f11b) ' -2147159781
        Public Const InvalidActivityOwnershipTypeMask As Integer = CInt(&H8004f120) ' -2147159776
        Public Const ActivityCannotHaveRelatedActivities As Integer = CInt(&H8004f121) ' -2147159775
        Public Const CustomActivityMustHaveOfflineAvailability As Integer = CInt(&H8004f122) ' -2147159774
        Public Const ActivityMustHaveRelatedNotes As Integer = CInt(&H8004f123) ' -2147159773
        Public Const CustomActivityCannotBeMailMergeEnabled As Integer = CInt(&H8004f124) ' -2147159772
        Public Const InvalidCustomActivityType As Integer = CInt(&H8004f125) ' -2147159771
        Public Const ActivityMetadataUpdate As Integer = CInt(&H8004f126) ' -2147159770
        Public Const InvalidPrimaryFieldForActivity As Integer = CInt(&H8004f127) ' -2147159769
        Public Const CannotDeleteNonLeafNode As Integer = CInt(&H8004f200) ' -2147159552
        Public Const DuplicateUIStatementRootsFound As Integer = CInt(&H8004f201) ' -2147159551
        Public Const ErrorUpdateStatementTextIsReferenced As Integer = CInt(&H8004f202) ' -2147159550
        Public Const ErrorDeleteStatementTextIsReferenced As Integer = CInt(&H8004f203) ' -2147159549
        Public Const ErrorScriptSessionCannotCreateForDraftScript As Integer = CInt(&H8004f204) ' -2147159548
        Public Const ErrorScriptSessionCannotUpdateForDraftScript As Integer = CInt(&H8004f205) ' -2147159547
        Public Const ErrorScriptLanguageNotInstalled As Integer = CInt(&H8004f206) ' -2147159546
        Public Const ErrorScriptInitialStatementNotInScript As Integer = CInt(&H8004f207) ' -2147159545
        Public Const ErrorScriptInitialStatementNotRoot As Integer = CInt(&H8004f208) ' -2147159544
        Public Const ErrorScriptCannotDeletePublishedScript As Integer = CInt(&H8004f209) ' -2147159543
        Public Const ErrorScriptPublishMissingInitialStatement As Integer = CInt(&H8004f20a) ' -2147159542
        Public Const ErrorScriptPublishMalformedScript As Integer = CInt(&H8004f20b) ' -2147159541
        Public Const ErrorScriptUnpublishActiveScript As Integer = CInt(&H8004f20c) ' -2147159540
        Public Const ErrorScriptSessionCannotSetStateForDraftScript As Integer = CInt(&H8004f20d) ' -2147159539
        Public Const ErrorScriptStatementResponseTypeOnlyForPrompt As Integer = CInt(&H8004f20e) ' -2147159538
        Public Const ErrorStatementOnlyForDraftScript As Integer = CInt(&H8004f20f) ' -2147159537
        Public Const ErrorStatementDeleteOnlyForDraftScript As Integer = CInt(&H8004f210) ' -2147159536
        Public Const ErrorInvalidUIScriptImportFile As Integer = CInt(&H8004f211) ' -2147159535
        Public Const ErrorScriptFileParse As Integer = CInt(&H8004f212) ' -2147159534
        Public Const ErrorScriptCannotUpdatePublishedScript As Integer = CInt(&H8004f213) ' -2147159533
        Public Const ErrorInvalidFileNameChars As Integer = CInt(&H8004f214) ' -2147159532
        Public Const ErrorMimeTypeNullOrEmpty As Integer = CInt(&H8004f215) ' -2147159531
        Public Const ErrorImportInvalidForPublishedScript As Integer = CInt(&H8004f216) ' -2147159530
        Public Const UIScriptIdentifierDuplicate As Integer = CInt(&H8004f217) ' -2147159529
        Public Const UIScriptIdentifierInvalid As Integer = CInt(&H8004f218) ' -2147159528
        Public Const UIScriptIdentifierInvalidLength As Integer = CInt(&H8004f219) ' -2147159527
        Public Const ErrorNoQueryData As Integer = CInt(&H8004f220) ' -2147159520
        Public Const ErrorUIScriptPromptMissing As Integer = CInt(&H8004f221) ' -2147159519
        Public Const SharePointUrlHostValidator As Integer = CInt(&H8004f301) ' -2147159295
        Public Const SharePointCrmDomainValidator As Integer = CInt(&H8004f302) ' -2147159294
        Public Const SharePointServerDiscoveryValidator As Integer = CInt(&H8004f303) ' -2147159293
        Public Const SharePointServerVersionValidator As Integer = CInt(&H8004f304) ' -2147159292
        Public Const SharePointSiteCollectionIsAccessibleValidator As Integer = CInt(&H8004f305) ' -2147159291
        Public Const SharePointUrlIsRootWebValidator As Integer = CInt(&H8004f306) ' -2147159290
        Public Const SharePointSitePermissionsValidator As Integer = CInt(&H8004f307) ' -2147159289
        Public Const SharePointServerLanguageValidator As Integer = CInt(&H8004f308) ' -2147159288
        Public Const SharePointCrmGridIsInstalledValidator As Integer = CInt(&H8004f309) ' -2147159287
        Public Const SharePointErrorRetrieveAbsoluteUrl As Integer = CInt(&H8004f310) ' -2147159280
        Public Const SharePointInvalidEntityForValidation As Integer = CInt(&H8004f311) ' -2147159279
        Public Const DocumentManagementIsDisabled As Integer = CInt(&H8004f312) ' -2147159278
        Public Const DocumentManagementNotEnabledNoPrimaryField As Integer = CInt(&H8004f313) ' -2147159277
        Public Const SharePointErrorAbsoluteUrlClipped As Integer = CInt(&H8004f314) ' -2147159276
        Public Const SiteMapXsdValidationError As Integer = CInt(&H8004f401) ' -2147159039
        Public Const LinkedAttributeCannotBeSecured As Integer = CInt(&H8004f500) ' -2147158784
        Public Const CannotSecureAttribute As Integer = CInt(&H8004f501) ' -2147158783
        Public Const AttributePrivilegeCreateIsMissing As Integer = CInt(&H8004f502) ' -2147158782
        Public Const AttributePermissionUpdateIsMissingDuringShare As Integer = CInt(&H8004f503) ' -2147158781
        Public Const AttributePermissionReadIsMissing As Integer = CInt(&H8004f504) ' -2147158780
        Public Const CannotRemoveSysAdminProfileFromSysAdminUser As Integer = CInt(&H8004f505) ' -2147158779
        Public Const QueryContainedSecuredAttributeWithoutAccess As Integer = CInt(&H8004f506) ' -2147158778
        Public Const AttributePermissionUpdateIsMissingDuringUpdate As Integer = CInt(&H8004f507) ' -2147158777
        Public Const AttributeNotSecured As Integer = CInt(&H8004f508) ' -2147158776
        Public Const AttributeSharingCreateShouldSetReadOrAndUpdateAccess As Integer = CInt(&H8004f509) ' -2147158775
        Public Const AttributeSharingUpdateInvalid As Integer = CInt(&H8004f50a) ' -2147158774
        Public Const AttributeSharingCreateDuplicate As Integer = CInt(&H8004f50b) ' -2147158773
        Public Const AdminProfileCannotBeEditedOrDeleted As Integer = CInt(&H8004f50c) ' -2147158772
        Public Const AttributePrivilegeInvalidToUnsecure As Integer = CInt(&H8004f50d) ' -2147158771
        Public Const RequireValidImportMapForUpdate As Integer = CInt(&H8004f600) ' -2147158528
        Public Const InvalidFormatForUpdateMode As Integer = CInt(&H8004f601) ' -2147158527
        Public Const MaximumCountForUpdateModeExceeded As Integer = CInt(&H8004f602) ' -2147158526
        Public Const RecordResolutionFailed As Integer = CInt(&H8004f603) ' -2147158525
        Public Const InvalidOperationForDynamicList As Integer = CInt(&H8004f701) ' -2147158271
        Public Const QueryNotValidForStaticList As Integer = CInt(&H8004f702) ' -2147158270
        Public Const LockStatusNotValidForDynamicList As Integer = CInt(&H8004f703) ' -2147158269
        Public Const CannotCopyStaticList As Integer = CInt(&H8004f704) ' -2147158268
        Public Const CannotDeleteSystemForm As Integer = CInt(&H8004f652) ' -2147158446
        Public Const CannotUpdateSystemEntityIcons As Integer = CInt(&H8004f653) ' -2147158445
        Public Const FallbackFormDeletion As Integer = CInt(&H8004f654) ' -2147158444
        Public Const SystemFormImportMissingRoles As Integer = CInt(&H8004f655) ' -2147158443
        Public Const SystemFormCopyUnmatchedEntity As Integer = CInt(&H8004f656) ' -2147158442
        Public Const SystemFormCopyUnmatchedFormType As Integer = CInt(&H8004f657) ' -2147158441
        Public Const SystemFormCreateWithExistingLabel As Integer = CInt(&H8004f658) ' -2147158440
        Public Const QuickFormNotCustomizableThroughSdk As Integer = CInt(&H8004F659) ' -2147158439
        Public Const InvalidDeactivateFormType As Integer = CInt(&H8004F660) ' -2147158432
        Public Const FallbackFormDeactivation As Integer = CInt(&H8004F661) ' -2147158431
		Public Const RuntimeRibbonXmlValidation As Integer = CInt(&H8004F671) ' -2147158415
        Public Const InitializeErrorNoReadOnSource As Integer = CInt(&H8004f800) ' -2147158016
        Public Const NoRollupAttributesDefined As Integer = CInt(&H8004f681) ' -2147158399
        Public Const GoalPercentageAchievedValueOutOfRange As Integer = CInt(&H8004f682) ' -2147158398
        Public Const InvalidRollupQueryAttributeSet As Integer = CInt(&H8004f683) ' -2147158397
        Public Const InvalidGoalManager As Integer = CInt(&H8004f684) ' -2147158396
        Public Const InactiveRollupQuerySetOnGoal As Integer = CInt(&H8004f685) ' -2147158395
        Public Const InactiveMetricSetOnGoal As Integer = CInt(&H8004f686) ' -2147158394
        Public Const MetricEntityOrFieldDeleted As Integer = CInt(&H8004f687) ' -2147158393
        Public Const ExceededNumberOfRecordsCanFollow As Integer = CInt(&H8004f6a0) ' -2147158368
        Public Const EntityIsNotEnabledForFollowUser As Integer = CInt(&H8004f6a1) ' -2147158367
        Public Const EntityIsNotEnabledForFollow As Integer = CInt(&H8004f6a2) ' -2147158366
        Public Const CannotFollowInactiveEntity As Integer = CInt(&H8004f6a3) ' -2147158365
        Public Const MustContainAtLeastACharInMention As Integer = CInt(&H8004f6a4) ' -2147158364
        Public Const LanguageProvisioningSrsDataConnectorNotInstalled As Integer = CInt(&H8004f710) ' -2147158256
        Public Const BidsInvalidConnectionString As Integer = CInt(&H8005e000) ' -2147098624
        Public Const BidsInvalidUrl As Integer = CInt(&H8005e001) ' -2147098623
        Public Const BidsServerConnectionFailed As Integer = CInt(&H8005e002) ' -2147098622
        Public Const BidsAuthenticationError As Integer = CInt(&H8005e003) ' -2147098621
        Public Const BidsNoOrganizationsFound As Integer = CInt(&H8005e004) ' -2147098620
        Public Const BidsOrganizationNotFound As Integer = CInt(&H8005e005) ' -2147098619
        Public Const BidsAuthenticationFailed As Integer = CInt(&H8005e006) ' -2147098618
        Public Const TransactionNotSupported As Integer = CInt(&H8005e007) ' -2147098617
        Public Const IndexOutOfRange As Integer = CInt(&H8005e008) ' -2147098616
        Public Const InvalidAttribute As Integer = CInt(&H8005e009) ' -2147098615
        Public Const MultiValueParameterFound As Integer = CInt(&H8005e00a) ' -2147098614
        Public Const QueryParameterNotUnique As Integer = CInt(&H8005e00b) ' -2147098613
        Public Const InvalidEntity As Integer = CInt(&H8005e00c) ' -2147098612
        Public Const UnsupportedAttributeType As Integer = CInt(&H8005e00d) ' -2147098611
        Public Const FetchDataSetQueryTimeout As Integer = CInt(&H8005e00e) ' -2147098610
		Public Const InvalidCommand As Integer = CInt(&H8005E100) ' -2147098368
		Public Const InvalidDataXml As Integer = CInt(&H8005E101) ' -2147098367
		Public Const InvalidLanguageForProcessConfiguration As Integer = CInt(&H8005E102) ' -2147098366
		Public Const InvalidComplexControlId As Integer = CInt(&H8005E103) ' -2147098365
		Public Const InvalidProcessControlEntity As Integer = CInt(&H8005E104) ' -2147098364
		Public Const InvalidProcessControlAttribute As Integer = CInt(&H8005E105) ' -2147098363
        Public Const BadRequest As Integer = CInt(&H8005F100) ' -2147094272
        Public Const AccessTokenExpired As Integer = CInt(&H8005F101) ' -2147094271
        Public Const Forbidden As Integer = CInt(&H8005F102) ' -2147094270
        Public Const Throttling As Integer = CInt(&H8005F103) ' -2147094269
        Public Const NetworkIssue As Integer = CInt(&H8005F104) ' -2147094268
        Public Const CouldNotReadAccessToken As Integer = CInt(&H8005F105) ' -2147094267
        Public Const NotVerifiedAdmin As Integer = CInt(&H8005F106) ' -2147094266
        Public Const YammerAuthTimedOut As Integer = CInt(&H8005F107) ' -2147094265
        Public Const NoYammerNetworksFound As Integer = CInt(&H8005F108) ' -2147094264
        Public Const OAuthTokenNotFound As Integer = CInt(&H8005F109) ' -2147094263
        Public Const CouldNotDecryptOAuthToken As Integer = CInt(&H8005F110) ' -2147094256
        Public Const UserNeverLoggedIntoYammer As Integer = CInt(&H8005F111) ' -2147094255           
        Public Const StepNotSupportedForClientBusinessRule As Integer = CInt(&H80060000) ' -2147090432
        Public Const EventNotSupportedForBusinessRule As Integer = CInt(&H80060001) ' -2147090431
        Public Const CannotUpdateTriggerForPublishedRules As Integer = CInt(&H80060002) ' -2147090430
        Public Const EventTypeAndControlNameAreMismatched As Integer = CInt(&H80060003) ' -2147090429
        Public Const ExpressionNotSupportedForEditor As Integer = CInt(&H80060004) ' -2147090428
        Public Const EditorOnlySupportAndOperatorForLogicalConditions As Integer = CInt(&H80060005) ' -2147090427
        Public Const UnexpectedRightOperandCount As Integer = CInt(&H80060006) ' -2147090426
        Public Const RuleNotSupportedForEditor As Integer = CInt(&H80060007) ' -2147090425
        Public Const BusinessRuleEditorSupportsOnlyIfConditionBranch As Integer = CInt(&H80060008) ' -2147090424
        Public Const UnsupportedStepForBusinessRuleEditor As Integer = CInt(&H80060009) ' -2147090423
        Public Const UnsupportedAttributeForEditor As Integer = CInt(&H80060010) ' -2147090416
        Public Const ExpectingAtLeastOneBusinessRuleStep As Integer = CInt(&H80060011) ' -2147090415
        Public Const EntityLimitExceeded As Integer = CInt(&H80060200) ' -2147089920
        Public Const InvalidSearchEntity As Integer = CInt(&H80060201) ' -2147089919
        Public Const InvalidSearchEntities As Integer = CInt(&H80060202) ' -2147089918
        Public Const NoQuickFindFound As Integer = CInt(&H80060203) ' -2147089917
        Public Const InvalidSearchName As Integer = CInt(&H80060204) ' -2147089916
        Public Const EntityGroupNameOrEntityNamesMustBeProvided As Integer = CInt(&H80060205) ' -2147089915
        Public Const OnlyOneSearchParameterMustBeProvided As Integer = CInt(&H80060206) ' -2147089914
        Public Const WorkflowIdIsNull As Integer = CInt(&H80060400) ' -2147089408
        Public Const PrimaryEntityIsNull As Integer = CInt(&H80060401) ' -2147089407
        Public Const TypeNotSetToDefinition As Integer = CInt(&H80060402) ' -2147089406
        Public Const ScopeNotSetToGlobal As Integer = CInt(&H80060403) ' -2147089405
        Public Const CategoryNotSetToBusinessProcessFlow As Integer = CInt(&H80060404) ' -2147089404
        Public Const BusinessProcessFlowStepHasInvalidParent As Integer = CInt(&H80060405) ' -2147089403
        Public Const NullOrEmptyAttributeInXaml As Integer = CInt(&H80060406) ' -2147089402
        Public Const InvalidGuidInXaml As Integer = CInt(&H80060407) ' -2147089401
        Public Const NoLabelsAssociatedWithStep As Integer = CInt(&H80060408) ' -2147089400
        Public Const StepStepDoesNotHaveAnyControlStepAsItsChildren As Integer = CInt(&H80060409) ' -2147089399
        Public Const InvalidXmlForParameters As Integer = CInt(&H80060410) ' -2147089392
        Public Const ControlIdIsNotUnique As Integer = CInt(&H80060411) ' -2147089391
        Public Const InvalidAttributeInXaml As Integer = CInt(&H80060412) ' -2147089390
        Public Const AttributeCannotBeUpdated As Integer = CInt(&H80060413) ' -2147089389
        Public Const StepCountInXamlExceedsMaxAllowed As Integer = CInt(&H80060414) ' -2147089388
        Public Const StepDoesNotHaveAnyChildInXaml As Integer = CInt(&H80060416) ' -2147089386
        Public Const InvalidXaml As Integer = CInt(&H80060417) ' -2147089385
        Public Const ProcessNameIsNullOrEmpty As Integer = CInt(&H80060418) ' -2147089384
        Public Const LabelIdDoesNotMatchStepId As Integer = CInt(&H80060419) ' -2147089383
        Public Const EntityExceedsMaxActiveBusinessProcessFlows As Integer = CInt(&H80060420) ' -2147089376
        Public Const EntityIsNotBusinessProcessFlowEnabled As Integer = CInt(&H80060421) ' -2147089375
        Public Const CannotDeleteUserMailbox As Integer = CInt(&H8005E200) ' -2147098112
        Public Const EmailServerProfileSslRequiredForOnline As Integer = CInt(&H8005E201) ' -2147098111
        Public Const EmailServerProfileInvalidCredentialRetrievalForOnline As Integer = CInt(&H8005E202) ' -2147098110
        Public Const EmailServerProfileInvalidCredentialRetrievalForExchange As Integer = CInt(&H8005E203) ' -2147098109
        Public Const EmailServerProfileAutoDiscoverNotAllowed As Integer = CInt(&H8005E204) ' -2147098108
        Public Const EmailServerProfileLocationNotRequired As Integer = CInt(&H8005E205) ' -2147098107
        Public Const ForwardMailboxCannotAssociateWithUser As Integer = CInt(&H8005E207) ' -2147098105
        Public Const MailboxCannotModifyEmailAddress As Integer = CInt(&H8005E208) ' -2147098104
        Public Const MailboxCredentialNotSpecified As Integer = CInt(&H8005E209) ' -2147098103
        Public Const EmailServerProfileInvalidServerLocation As Integer = CInt(&H8005E20A) ' -2147098102
        Public Const CannotAcceptEmail As Integer = CInt(&H8005E20B) ' -2147098101
        Public Const QueueMailboxUnexpectedDeliveryMethod As Integer = CInt(&H8005E210) ' -2147098096
        Public Const ForwardMailboxEmailAddressRequired As Integer = CInt(&H8005E211) ' -2147098095
        Public Const ForwardMailboxUnexpectedIncomingDeliveryMethod As Integer = CInt(&H8005E212) ' -2147098094
        Public Const ForwardMailboxUnexpectedOutgoingDeliveryMethod As Integer = CInt(&H8005E213) ' -2147098093
        Public Const InvalidCredentialTypeForNonExchangeIncomingConnection As Integer = CInt(&H8005E214) ' -2147098092
        Public Const Pop3UnexpectedException As Integer = CInt(&H8005E215) ' -2147098091
        Public Const OpenMailboxException As Integer = CInt(&H8005E216) ' -2147098090
        Public Const InvalidMailbox As Integer = CInt(&H8005E217) ' -2147098089
        Public Const InvalidEmailServerLocation As Integer = CInt(&H8005E218) ' -2147098088
        Public Const InactiveMailbox As Integer = CInt(&H8005E219) ' -2147098087
        Public Const UnapprovedMailbox As Integer = CInt(&H8005E220) ' -2147098080
        Public Const InvalidEmailAddressInMailbox As Integer = CInt(&H8005E221) ' -2147098079
        Public Const EmailServerProfileNotAssociated As Integer = CInt(&H8005E222) ' -2147098078
        Public Const IncomingDeliveryIsForwardMailbox As Integer = CInt(&H8005E223) ' -2147098077
        Public Const InvalidIncomingDeliveryExpectingEmailConnector As Integer = CInt(&H8005E224) ' -2147098076
        Public Const OutgoingNotAllowedForForwardMailbox As Integer = CInt(&H8005E225) ' -2147098075
        Public Const InvalidOutgoingDeliveryExpectingEmailConnector As Integer = CInt(&H8005E226) ' -2147098074
        Public Const InaccessibleSmtpServer As Integer = CInt(&H8005E227) ' -2147098073
        Public Const InactiveEmailServerProfile As Integer = CInt(&H8005E228) ' -2147098072
        Public Const CannotUseUserCredentials As Integer = CInt(&H8005E229) ' -2147098071
        Public Const CannotActivateMailboxForDisabledUserOrQueue As Integer = CInt(&H8005E230) ' -2147098064
        Public Const ZeroEmailReceived As Integer = CInt(&H8005E231) ' -2147098063
        Public Const NoTestEmailAccessPrivilege As Integer = CInt(&H8005E232) ' -2147098062
        Public Const MailboxCannotDeleteEmails As Integer = CInt(&H8005E233) ' -2147098061
        Public Const EmailServerProfileSslRequiredForOnPremise As Integer = CInt(&H8005E234) ' -2147098060
        Public Const EmailServerProfileDelegateAccessNotAllowed As Integer = CInt(&H8005E235) ' -2147098059
        Public Const EmailServerProfileImpersonationNotAllowed As Integer = CInt(&H8005E236) ' -2147098058
        Public Const EmailMessageSizeExceeded As Integer = CInt(&H8005E237) ' -2147098057
        Public Const OutgoingSettingsUpdateNotAllowed As Integer = CInt(&H8005E238) ' -2147098056
        Public Const CertificateNotFound As Integer = CInt(&H8005E239) ' -2147098055
        Public Const InvalidCertificate As Integer = CInt(&H8005E23A) ' -2147098054
        Public Const EmailServerProfileInvalidAuthenticationProtocol As Integer = CInt(&H8005E23B) ' -2147098053
        Public Const EmailServerProfileADBasedAuthenticationProtocolNotAllowed As Integer = CInt(&H8005E23C) ' -2147098052
        Public Const EmailServerProfileBasicAuthenticationProtocolNotAllowed As Integer = CInt(&H8005E23D) ' -2147098051
        Public Const IncomingServerLocationAndSslSetToNo As Integer = CInt(&H8005E23E) ' -2147098050
        Public Const OutgoingServerLocationAndSslSetToNo As Integer = CInt(&H8005E23F) ' -2147098049
        Public Const IncomingServerLocationAndSslSetToYes As Integer = CInt(&H8005E240) ' -2147098048
        Public Const OutgoingServerLocationAndSslSetToYes As Integer = CInt(&H8005E241) ' -2147098047
        Public Const UnsupportedEmailServer As Integer = CInt(&H8005E242) ' -2147098046
        Public Const S2SAccessTokenCannotBeAcquired As Integer = CInt(&H8005E243) ' -2147098045
        Public Const InvalidValueProcessEmailAfter As Integer = CInt(&H8005E244) ' -2147098044
        Public Const InvalidS2SAuthentication As Integer = CInt(&H8005E245) ' -2147098043
        Public Const RouterIsDisabled As Integer = CInt(&H8005E246) ' -2147098042
        Public Const TraceMessageConstructionError As Integer = CInt(&H8004F900) ' -2147157760
        Public Const TooManyBytesInInputStream As Integer = CInt(&H8004F901) ' -2147157759
        Public Const EmailRouterFileTooLargeToProcess As Integer = CInt(&H8005F031) ' -2147094479
        Public Const ErrorsInEmailRouterMigrationFiles As Integer = CInt(&H8005F032) ' -2147094478
        Public Const InvalidMigrationFileContent As Integer = CInt(&H8005F033) ' -2147094477
        Public Const ErrorMigrationProcessExcessOnServer As Integer = CInt(&H8005F034) ' -2147094476
        Public Const EntityNotEnabledForThisDevice As Integer = CInt(&H8005F200) ' -2147094016
        Public Const MobileClientLanguageNotSupported As Integer = CInt(&H8005F201) ' -2147094015
        Public Const MobileClientVersionNotSupported As Integer = CInt(&H8005F202) ' -2147094014
        Public Const RoleNotEnabledForTabletApp As Integer = CInt(&H8005F203) ' -2147094013
        Public Const NoMinimumRequiredPrivilegesForTabletApp As Integer = CInt(&H8005F20F) ' -2147094001
        Public Const FilePickerErrorAttachmentTypeBlocked As Integer = CInt(&H8005F204) ' -2147094012
        Public Const FilePickerErrorFileSizeBreached As Integer = CInt(&H8005F205) ' -2147094011
        Public Const FilePickerErrorFileSizeCannotBeZero As Integer = CInt(&H8005F206) ' -2147094010
        Public Const FilePickerErrorUnableToOpenFile As Integer = CInt(&H8005F207) ' -2147094009
        Public Const GetPhotoFromGalleryFailed As Integer = CInt(&H8005F208) ' -2147094008
        Public Const SaveDataFileErrorOutOfSpace As Integer = CInt(&H8005F209) ' -2147094007
        Public Const OpenDocumentErrorCodeUnableToFindAnActivity As Integer = CInt(&H8005F20A) ' -2147094006
        Public Const OpenDocumentErrorCodeUnableToFindTheDataId As Integer = CInt(&H8005F20B) ' -2147094005
        Public Const OpenDocumentErrorCodeGeneric As Integer = CInt(&H8005F20C) ' -2147094004
        Public Const FilePickerErrorApplicationInSnapView As Integer = CInt(&H8005F20D) ' -2147094003
        Public Const MobileClientNotConfiguredForCurrentUser As Integer = CInt(&H8005F20E) ' -2147094002
        Public Const DataSourceInitializeFailedErrorCode As Integer = CInt(&H8005F210) ' -2147094000
        Public Const DataSourceOfflineErrorCode As Integer = CInt(&H8005F211) ' -2147093999
        Public Const PingFailureErrorCode As Integer = CInt(&H8005F212) ' -2147093998
        Public Const RetrieveRecordOfflineErrorCode As Integer = CInt(&H8005F213) ' -2147093997
        Public Const NotMobileEnabled As Integer = CInt(&H8005F215) ' -2147093995
    End Class
End Namespace