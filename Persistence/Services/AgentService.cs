using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.Agent;
using ComplyExchangeCMS.Domain.Services;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class AgentService : IAgentService                                                                                                                                                                                                                                                                                                                             
    {
        private readonly IConfiguration _configuration;
        public AgentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<int> Insert(AgentInsert agents)

        {
            agents.CreatedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();
                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Name", agents.Name, DbType.String);
                parameters.Add("@W9RequestorName", agents.W9RequestorName, DbType.String);
                parameters.Add("@Line1", agents.Line1, DbType.String);
                parameters.Add("@Line2", agents.Line2, DbType.String);
                parameters.Add("@CityTown", agents.CityTown, DbType.String);
                parameters.Add("@StateProvince", agents.StateProvince, DbType.String);
                parameters.Add("@ZipPostalCode", agents.ZipPostalCode, DbType.String);
                parameters.Add("@CountryId", agents.CountryId, DbType.Int32);
                parameters.Add("@Other", agents.Other, DbType.String);
                parameters.Add("@DefaultSelection", agents.DefaultSelection, DbType.Int32);
                parameters.Add("@DefaultLanguageId", agents.DefaultLanguageId, DbType.Int32);
                parameters.Add("@IncludeDefaultEnglish", agents.IncludeDefaultEnglish, DbType.Boolean);
                parameters.Add("@Logo", agents.LogoId, DbType.Int32);
                parameters.Add("@LogoImage", agents.Logo_ImagePath, DbType.String);
                parameters.Add("@LogoNavigateURL", agents.LogoNavigateURL, DbType.String);
                parameters.Add("@PDFWatermark", agents.PDFWatermark, DbType.String);
                parameters.Add("@DisplayVersion", agents.DisplayVersion, DbType.String);
                parameters.Add("@DisplayRenderTime", agents.DisplayRenderTime, DbType.Boolean);
                parameters.Add("@FormFeedUsername", agents.FormFeedUsername, DbType.String);
                parameters.Add("@FormFeedPassword", agents.FormFeedPassword, DbType.String);
                parameters.Add("@TINCheckUsername", agents.TINCheckUsername, DbType.String);
                parameters.Add("@TINCheckPassword", agents.TINCheckPassword, DbType.String);
                parameters.Add("@SupportsTINValidation", agents.SupportsTINValidation, DbType.Boolean);
                parameters.Add("@ContinueAfterTINValidationFailure", agents.ContinueAfterTINValidationFailure, DbType.Boolean);
                parameters.Add("@TermsAndConditions", agents.TermsAndConditions, DbType.String);
                parameters.Add("@TokenEmail", agents.TokenEmail, DbType.String);
                parameters.Add("@OnboardingAPIActive", agents.OnboardingAPIActive, DbType.Boolean);
                parameters.Add("@SkipAHDPage", agents.SkipAHDPage, DbType.Boolean);
                parameters.Add("@SkipTCPage", agents.SkipTCPage, DbType.Boolean);
                parameters.Add("@StoreCDFOnTheFly", agents.StoreCDFOnTheFly, DbType.Boolean);
                parameters.Add("@StoreCDFAndFormOnAfterFormSubmission", agents.StoreCDFAndFormOnAfterFormSubmission, DbType.Boolean);
                parameters.Add("@ShowUIDEntryFieldInTheEntityDetailsScreen", agents.ShowUIDEntryFieldInTheEntityDetailsScreen, DbType.Boolean);
                parameters.Add("@ShowUIDEntryFieldInTheEntityDetailsScreenRequiredFormat", agents.ShowUIDEntryFieldInTheEntityDetailsScreenRequiredFormat, DbType.String);
                parameters.Add("@ShowUIDEntryFieldInTheEntityDetailsScreenIncludeUIDOnReferenceLine", agents.ShowUIDEntryFieldInTheEntityDetailsScreenIncludeUIDOnReferenceLine, DbType.Boolean);
                parameters.Add("@UpdateCDFRecordOnTheFly", agents.UpdateCDFRecordOnTheFly, DbType.Boolean);
                parameters.Add("@AllowSecondSignatureSubmission", agents.AllowSecondSignatureSubmission, DbType.Boolean);
                parameters.Add("@AllowSecondSignatureSubmissionUseSameAgent", agents.AllowSecondSignatureSubmissionUseSameAgent, DbType.Boolean);
                parameters.Add("@AllowSecondSignatureSubmissionUseSameAgent", agents.AllowSecondSignatureSubmissionUseSameAgent, DbType.Boolean);
                parameters.Add("AllowSecondSignatureSubmissionSecondSubmissionMandatory", agents.AllowSecondSignatureSubmissionSecondSubmissionMandatory, DbType.Boolean);
                parameters.Add("@IncludeAdditionalInformationOnESubmitPDF", agents.IncludeAdditionalInformationOnESubmitPDF, DbType.Boolean);
                parameters.Add("@ShowUSourcedIncomeDeclaration", agents.ShowUSourcedIncomeDeclaration, DbType.Boolean);
                parameters.Add("@ShowUSourcedIncomeDeclarationAndWhenNoUSSourcedIncomeHideChapter4AndDREPage", agents.ShowUSourcedIncomeDeclarationAndWhenNoUSSourcedIncomeHideChapter4AndDREPage, DbType.Boolean);
                parameters.Add("@ShowRetroactiveStatementOnlyShowApplyForW8Forms", agents.ShowRetroactiveStatementOnlyShowApplyForW8Forms, DbType.Boolean);
                parameters.Add("@ShowRetroactiveStatementOnlyShowApplyForW8FormsRenderFullScreen", agents.ShowRetroactiveStatementOnlyShowApplyForW8FormsRenderFullScreen, DbType.Boolean);
                parameters.Add("@ShowRetroactiveStatementOnlyShowApplyForW8FormsMakeMandatory", agents.ShowRetroactiveStatementOnlyShowApplyForW8FormsMakeMandatory, DbType.Boolean);
                parameters.Add("@EnableSaveExitProcess", agents.EnableSaveExitProcess, DbType.Boolean);
                parameters.Add("@EnableAllocationStatementCreation", agents.EnableAllocationStatementCreation, DbType.Boolean);
                parameters.Add("@ConsentToSendAnElectronic1042SOr1099", agents.ConsentToSendAnElectronic1042SOr1099, DbType.Boolean);
                parameters.Add("@EnableExemptFromBackupWithholdingPagePopUp", agents.EnableExemptFromBackupWithholdingPagePopUp, DbType.Boolean);
                parameters.Add("@HideDownloadTemplateToCompleteWithholdingStatement", agents.HideDownloadTemplateToCompleteWithholdingStatement, DbType.Boolean);
                parameters.Add("@RequestBankAccountInformation", agents.RequestBankAccountInformation, DbType.Boolean);
                parameters.Add("@RequestBankAccountInformationAndWhenYesMakeMandatory", agents.RequestBankAccountInformationAndWhenYesMakeMandatory, DbType.Boolean);
                parameters.Add("@HideW8BENETreaty14C", agents.HideW8BENETreaty14C, DbType.Boolean);
                parameters.Add("@Forms", agents.Forms, DbType.Boolean);
                parameters.Add("@FederalTax", agents.FederalTax, DbType.Boolean);
                parameters.Add("@SingleUSOwnerDetails", agents.SingleUSOwnerDetails, DbType.Boolean);
                parameters.Add("@TaxpayerInformation_IndividualOnly", agents.TaxpayerInformation_IndividualOnly, DbType.Boolean);
                parameters.Add("@TaxpayerInformation_EntityOnly", agents.TaxpayerInformation_EntityOnly, DbType.Boolean);
                parameters.Add("@TaxpayerInformation_NonUSEntityOnly", agents.TaxpayerInformation_NonUSEntityOnly, DbType.Boolean);
                parameters.Add("@TaxpayerInformation_GIIN", agents.TaxpayerInformation_GIIN, DbType.Boolean);
                parameters.Add("@FormSelection", agents.FormSelection, DbType.Boolean);
                parameters.Add("@W8IMYRelatedFiles", agents.W8IMYRelatedFiles, DbType.Boolean);
                parameters.Add("@W8BENJuly2017PartIIWhenTreatyClaimNo", agents.W8BENJuly2017PartIIWhenTreatyClaimNo, DbType.Boolean);
                parameters.Add("@W8BENEPartIIIWhenTreatyClaimNo", agents.W8BENEPartIIIWhenTreatyClaimNo, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertification8233", agents.PenaltiesOfPerjuryCertification8233, DbType.Boolean);
                parameters.Add("@ElectronicSignature8233", agents.ElectronicSignature8233, DbType.Boolean);
                parameters.Add("@NameAndAddress", agents.NameAndAddress, DbType.Boolean);
                parameters.Add("@Chapter3Status", agents.Chapter3Status, DbType.Boolean);
                parameters.Add("@USSourcedIncomeDeclarationOptional", agents.USSourcedIncomeDeclarationOptional, DbType.Boolean);
                parameters.Add("@IncomeCode", agents.IncomeCode, DbType.Boolean);
                parameters.Add("@UnitedStatesCitizenshipStatus", agents.UnitedStatesCitizenshipStatus, DbType.Boolean);
                parameters.Add("@UnitedStatesSubstantialPresenceTestOptional", agents.UnitedStatesSubstantialPresenceTestOptional, DbType.Boolean);
                parameters.Add("@USFATCAClassification", agents.USFATCAClassification, DbType.Boolean);
                parameters.Add("@Chapter4Status", agents.Chapter4Status, DbType.Boolean);
                parameters.Add("@DisregardedEntity", agents.DisregardedEntity, DbType.Boolean);
                parameters.Add("@ExemptionFromBackupWithholding", agents.ExemptionFromBackupWithholding, DbType.Boolean);
                parameters.Add("@ExemptionFromFATCAReporting", agents.ExemptionFromFATCAReporting, DbType.Boolean);
                parameters.Add("@TaxIdentificationNumber", agents.TaxIdentificationNumber, DbType.Boolean);
                parameters.Add("@SpecifiedUSPersonDetermination", agents.SpecifiedUSPersonDetermination, DbType.Boolean);
                parameters.Add("@ECIIncomeReport", agents.ECIIncomeReport, DbType.Boolean);
                parameters.Add("@TreatyClaim", agents.TreatyClaim, DbType.Boolean);
                parameters.Add("@SpecialRatesAndConditions", agents.SpecialRatesAndConditions, DbType.Boolean);
                parameters.Add("@SupportingDocumentationW9", agents.SupportingDocumentationW9, DbType.Boolean);
                parameters.Add("@SupportingDocumentationBEN", agents.SupportingDocumentationBEN, DbType.Boolean);
                parameters.Add("@SupportingDocumentationBENE", agents.SupportingDocumentationBENE, DbType.Boolean);
                parameters.Add("@SupportingDocumentationIMY", agents.SupportingDocumentationIMY, DbType.Boolean);
                parameters.Add("@SupportingDocumentationEXP", agents.SupportingDocumentationEXP, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationW9", agents.PenaltiesOfPerjuryCertificationW9, DbType.Boolean);
                parameters.Add("@SupportingDocumentationECI", agents.SupportingDocumentationECI, DbType.Boolean);
                parameters.Add("@ElectronicSignatureW9", agents.ElectronicSignatureW9, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertifcationBENE", agents.PenaltiesOfPerjuryCertifcationBENE, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationBEN", agents.PenaltiesOfPerjuryCertificationBEN, DbType.Boolean);
                parameters.Add("@ElectronicSignatureBEN", agents.ElectronicSignatureBEN, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationIMY", agents.PenaltiesOfPerjuryCertificationIMY, DbType.Boolean);
                parameters.Add("@ElectronicSignatureIMY", agents.ElectronicSignatureIMY, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationEXP", agents.PenaltiesOfPerjuryCertificationEXP, DbType.Boolean);
                parameters.Add("@ElectronicSignatureEXP", agents.ElectronicSignatureEXP, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationECI", agents.PenaltiesOfPerjuryCertificationECI, DbType.Boolean);
                parameters.Add("@ElectronicSignatureECI", agents.ElectronicSignatureECI, DbType.Boolean);
                parameters.Add("@USTaxCertificationComplete", agents.USTaxCertificationComplete, DbType.Boolean);
                parameters.Add("@SubstantialUSPresenceTest", agents.SubstantialUSPresenceTest, DbType.Boolean);
                parameters.Add("@IdentificationOfBeneficialOwner", agents.IdentificationOfBeneficialOwner, DbType.Boolean);
                parameters.Add("@SupportingDocumentation8233", agents.SupportingDocumentation8233, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationSelfCertEntity", agents.PenaltiesOfPerjuryCertificationSelfCertEntity, DbType.Boolean);
                parameters.Add("@ElectronicSignatureSelfCertEntity", agents.ElectronicSignatureSelfCertEntity, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationSelfCertIndividual", agents.PenaltiesOfPerjuryCertificationSelfCertIndividual, DbType.Boolean);
                parameters.Add("@ElectronicSignatureSelfCertIndividual", agents.ElectronicSignatureSelfCertIndividual, DbType.Boolean);
                parameters.Add("@W9ExemptFromBUWIndividualSolePs", agents.W9ExemptFromBUWIndividualSolePs, DbType.Boolean);
                parameters.Add("@ResidencyInformationForm", agents.ResidencyInformationForm, DbType.Boolean);
                parameters.Add("@AddressLine3Optional", agents.AddressLine3Optional, DbType.Boolean);
                parameters.Add("@CapacitydonotshowforEntities", agents.CapacitydonotshowforEntities, DbType.Boolean);
                parameters.Add("@CapacitydonotshowforIndividuals", agents.CapacitydonotshowforIndividuals, DbType.Boolean);
                parameters.Add("@FormSelectionGuideDirectRequestQuestion", agents.FormSelectionGuideDirectRequestQuestion, DbType.Boolean);
                parameters.Add("@FormSelectionGuideREITQuestion", agents.FormSelectionGuideREITQuestion, DbType.Boolean);
                parameters.Add("@GeneralClassification", agents.GeneralClassification, DbType.Boolean);
                parameters.Add("@LanguageDropdown", agents.LanguageDropdown, DbType.Boolean);
                parameters.Add("@MultipleTaxJurisdictions", agents.MultipleTaxJurisdictions, DbType.Boolean);
                parameters.Add("@RetroactiveStatement", agents.RetroactiveStatement, DbType.String);
                parameters.Add("@RetroactiveEffectiveDate", agents.RetroactiveEffectiveDate, DbType.String);
                parameters.Add("@DoNotAcceptURL", agents.DoNotAcceptURL, DbType.String);
                parameters.Add("@FinishURL", agents.FinishURL, DbType.String);
                parameters.Add("@ExitURL", agents.ExitURL, DbType.String);
                parameters.Add("@SaveAndExitURL", agents.SaveAndExitURL, DbType.String);
                parameters.Add("@TaxpayerInformation_NonUSIndividualOnly", agents.TaxpayerInformation_NonUSIndividualOnly, DbType.Boolean);
                parameters.Add("@CreatedOn", agents.CreatedOn, DbType.DateTime);

                //await connection.ExecuteAsync("sp_InsertAgent", parameters, commandType: CommandType.StoredProcedure);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertAgent", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> UpdateAgents(AgentUpdate agents)
        {
            agents.ModifiedOn = DateTime.UtcNow;

            using (var connection = CreateConnection())
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@Id", agents.Id, DbType.Int32);
                parameters.Add("@Name", agents.Name, DbType.String);
                parameters.Add("@W9RequestorName", agents.W9RequestorName, DbType.String);
                parameters.Add("@Line1", agents.Line1, DbType.String);
                parameters.Add("@Line2", agents.Line2, DbType.String);
                parameters.Add("@CityTown", agents.CityTown, DbType.String);
                parameters.Add("@StateProvince", agents.StateProvince, DbType.String);
                parameters.Add("@ZipPostalCode", agents.ZipPostalCode, DbType.String);
                parameters.Add("@CountryId", agents.CountryId, DbType.Int32);
                parameters.Add("@Other", agents.Other, DbType.String);
                parameters.Add("@DefaultSelection", agents.DefaultSelection, DbType.Int32);
                parameters.Add("@DefaultLanguageId", agents.DefaultLanguageId, DbType.Int32);
                parameters.Add("@IncludeDefaultEnglish", agents.IncludeDefaultEnglish, DbType.Boolean);
                parameters.Add("@Logo", agents.LogoId, DbType.String);
                parameters.Add("@LogoNavigateURL", agents.LogoNavigateURL, DbType.String);
                parameters.Add("@PDFWatermark", agents.PDFWatermark, DbType.String);
                parameters.Add("@DisplayVersion", agents.DisplayVersion, DbType.String);
                parameters.Add("@DisplayRenderTime", agents.DisplayRenderTime, DbType.Boolean);
                parameters.Add("@FormFeedUsername", agents.FormFeedUsername, DbType.String);
                parameters.Add("@FormFeedPassword", agents.FormFeedPassword, DbType.String);
                parameters.Add("@TINCheckUsername", agents.TINCheckUsername, DbType.String);
                parameters.Add("@TINCheckPassword", agents.TINCheckPassword, DbType.String);
                parameters.Add("@SupportsTINValidation", agents.SupportsTINValidation, DbType.Boolean);
                parameters.Add("@ContinueAfterTINValidationFailure", agents.ContinueAfterTINValidationFailure, DbType.Boolean);
                parameters.Add("@TermsAndConditions", agents.TermsAndConditions, DbType.String);
                parameters.Add("@TokenEmail", agents.TokenEmail, DbType.String);
                parameters.Add("@OnboardingAPIActive", agents.OnboardingAPIActive, DbType.Boolean);
                parameters.Add("@SkipAHDPage", agents.SkipAHDPage, DbType.Boolean);
                parameters.Add("@SkipTCPage", agents.SkipTCPage, DbType.Boolean);
                parameters.Add("@StoreCDFOnTheFly", agents.StoreCDFOnTheFly, DbType.Boolean);
                parameters.Add("@StoreCDFAndFormOnAfterFormSubmission", agents.StoreCDFAndFormOnAfterFormSubmission, DbType.Boolean);
                parameters.Add("@ShowUIDEntryFieldInTheEntityDetailsScreen", agents.ShowUIDEntryFieldInTheEntityDetailsScreen, DbType.Boolean);
                parameters.Add("@ShowUIDEntryFieldInTheEntityDetailsScreenRequiredFormat", agents.ShowUIDEntryFieldInTheEntityDetailsScreenRequiredFormat, DbType.String);
                parameters.Add("@ShowUIDEntryFieldInTheEntityDetailsScreenIncludeUIDOnReferenceLine", agents.ShowUIDEntryFieldInTheEntityDetailsScreenIncludeUIDOnReferenceLine, DbType.Boolean);
                parameters.Add("@UpdateCDFRecordOnTheFly", agents.UpdateCDFRecordOnTheFly, DbType.Boolean);
                parameters.Add("@AllowSecondSignatureSubmission", agents.AllowSecondSignatureSubmission, DbType.Boolean);
                parameters.Add("@AllowSecondSignatureSubmissionUseSameAgent", agents.AllowSecondSignatureSubmissionUseSameAgent, DbType.Boolean);
                parameters.Add("@AllowSecondSignatureSubmissionUseSameAgent", agents.AllowSecondSignatureSubmissionUseSameAgent, DbType.Boolean);
                parameters.Add("AllowSecondSignatureSubmissionSecondSubmissionMandatory", agents.AllowSecondSignatureSubmissionSecondSubmissionMandatory, DbType.Boolean);
                parameters.Add("@IncludeAdditionalInformationOnESubmitPDF", agents.IncludeAdditionalInformationOnESubmitPDF, DbType.Boolean);
                parameters.Add("@ShowUSourcedIncomeDeclaration", agents.ShowUSourcedIncomeDeclaration, DbType.Boolean);
                parameters.Add("@ShowUSourcedIncomeDeclarationAndWhenNoUSSourcedIncomeHideChapter4AndDREPage", agents.ShowUSourcedIncomeDeclarationAndWhenNoUSSourcedIncomeHideChapter4AndDREPage, DbType.Boolean);
                parameters.Add("@ShowRetroactiveStatementOnlyShowApplyForW8Forms", agents.ShowRetroactiveStatementOnlyShowApplyForW8Forms, DbType.Boolean);
                parameters.Add("@ShowRetroactiveStatementOnlyShowApplyForW8FormsRenderFullScreen", agents.ShowRetroactiveStatementOnlyShowApplyForW8FormsRenderFullScreen, DbType.Boolean);
                parameters.Add("@ShowRetroactiveStatementOnlyShowApplyForW8FormsMakeMandatory", agents.ShowRetroactiveStatementOnlyShowApplyForW8FormsMakeMandatory, DbType.Boolean);
                parameters.Add("@EnableSaveExitProcess", agents.EnableSaveExitProcess, DbType.Boolean);
                parameters.Add("@EnableAllocationStatementCreation", agents.EnableAllocationStatementCreation, DbType.Boolean);
                parameters.Add("@ConsentToSendAnElectronic1042SOr1099", agents.ConsentToSendAnElectronic1042SOr1099, DbType.Boolean);
                parameters.Add("@EnableExemptFromBackupWithholdingPagePopUp", agents.EnableExemptFromBackupWithholdingPagePopUp, DbType.Boolean);
                parameters.Add("@HideDownloadTemplateToCompleteWithholdingStatement", agents.HideDownloadTemplateToCompleteWithholdingStatement, DbType.Boolean);
                parameters.Add("@RequestBankAccountInformation", agents.RequestBankAccountInformation, DbType.Boolean);
                parameters.Add("@RequestBankAccountInformationAndWhenYesMakeMandatory", agents.RequestBankAccountInformationAndWhenYesMakeMandatory, DbType.Boolean);
                parameters.Add("@HideW8BENETreaty14C", agents.HideW8BENETreaty14C, DbType.Boolean);
                parameters.Add("@Forms", agents.Forms, DbType.Boolean);
                parameters.Add("@FederalTax", agents.FederalTax, DbType.Boolean);
                parameters.Add("@SingleUSOwnerDetails", agents.SingleUSOwnerDetails, DbType.Boolean);
                parameters.Add("@TaxpayerInformation_IndividualOnly", agents.TaxpayerInformation_IndividualOnly, DbType.Boolean);
                parameters.Add("@TaxpayerInformation_EntityOnly", agents.TaxpayerInformation_EntityOnly, DbType.Boolean);
                parameters.Add("@TaxpayerInformation_NonUSEntityOnly", agents.TaxpayerInformation_NonUSEntityOnly, DbType.Boolean);
                parameters.Add("@TaxpayerInformation_GIIN", agents.TaxpayerInformation_GIIN, DbType.Boolean);
                parameters.Add("@FormSelection", agents.FormSelection, DbType.Boolean);
                parameters.Add("@W8IMYRelatedFiles", agents.W8IMYRelatedFiles, DbType.Boolean);
                parameters.Add("@W8BENJuly2017PartIIWhenTreatyClaimNo", agents.W8BENJuly2017PartIIWhenTreatyClaimNo, DbType.Boolean);
                parameters.Add("@W8BENEPartIIIWhenTreatyClaimNo", agents.W8BENEPartIIIWhenTreatyClaimNo, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertification8233", agents.PenaltiesOfPerjuryCertification8233, DbType.Boolean);
                parameters.Add("@ElectronicSignature8233", agents.ElectronicSignature8233, DbType.Boolean);
                parameters.Add("@NameAndAddress", agents.NameAndAddress, DbType.Boolean);
                parameters.Add("@Chapter3Status", agents.Chapter3Status, DbType.Boolean);
                parameters.Add("@USSourcedIncomeDeclarationOptional", agents.USSourcedIncomeDeclarationOptional, DbType.Boolean);
                parameters.Add("@IncomeCode", agents.IncomeCode, DbType.Boolean);
                parameters.Add("@UnitedStatesCitizenshipStatus", agents.UnitedStatesCitizenshipStatus, DbType.Boolean);
                parameters.Add("@UnitedStatesSubstantialPresenceTestOptional", agents.UnitedStatesSubstantialPresenceTestOptional, DbType.Boolean);
                parameters.Add("@USFATCAClassification", agents.USFATCAClassification, DbType.Boolean);
                parameters.Add("@Chapter4Status", agents.Chapter4Status, DbType.Boolean);
                parameters.Add("@DisregardedEntity", agents.DisregardedEntity, DbType.Boolean);
                parameters.Add("@ExemptionFromBackupWithholding", agents.ExemptionFromBackupWithholding, DbType.Boolean);
                parameters.Add("@ExemptionFromFATCAReporting", agents.ExemptionFromFATCAReporting, DbType.Boolean);
                parameters.Add("@TaxIdentificationNumber", agents.TaxIdentificationNumber, DbType.Boolean);
                parameters.Add("@SpecifiedUSPersonDetermination", agents.SpecifiedUSPersonDetermination, DbType.Boolean);
                parameters.Add("@ECIIncomeReport", agents.ECIIncomeReport, DbType.Boolean);
                parameters.Add("@TreatyClaim", agents.TreatyClaim, DbType.Boolean);
                parameters.Add("@SpecialRatesAndConditions", agents.SpecialRatesAndConditions, DbType.Boolean);
                parameters.Add("@SupportingDocumentationW9", agents.SupportingDocumentationW9, DbType.Boolean);
                parameters.Add("@SupportingDocumentationBEN", agents.SupportingDocumentationBEN, DbType.Boolean);
                parameters.Add("@SupportingDocumentationBENE", agents.SupportingDocumentationBENE, DbType.Boolean);
                parameters.Add("@SupportingDocumentationIMY", agents.SupportingDocumentationIMY, DbType.Boolean);
                parameters.Add("@SupportingDocumentationEXP", agents.SupportingDocumentationEXP, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationW9", agents.PenaltiesOfPerjuryCertificationW9, DbType.Boolean);
                parameters.Add("@SupportingDocumentationECI", agents.SupportingDocumentationECI, DbType.Boolean);
                parameters.Add("@ElectronicSignatureW9", agents.ElectronicSignatureW9, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertifcationBENE", agents.PenaltiesOfPerjuryCertifcationBENE, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationBEN", agents.PenaltiesOfPerjuryCertificationBEN, DbType.Boolean);
                parameters.Add("@ElectronicSignatureBEN", agents.ElectronicSignatureBEN, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationIMY", agents.PenaltiesOfPerjuryCertificationIMY, DbType.Boolean);
                parameters.Add("@ElectronicSignatureIMY", agents.ElectronicSignatureIMY, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationEXP", agents.PenaltiesOfPerjuryCertificationEXP, DbType.Boolean);
                parameters.Add("@ElectronicSignatureEXP", agents.ElectronicSignatureEXP, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationECI", agents.PenaltiesOfPerjuryCertificationECI, DbType.Boolean);
                parameters.Add("@ElectronicSignatureECI", agents.ElectronicSignatureECI, DbType.Boolean);
                parameters.Add("@USTaxCertificationComplete", agents.USTaxCertificationComplete, DbType.Boolean);
                parameters.Add("@SubstantialUSPresenceTest", agents.SubstantialUSPresenceTest, DbType.Boolean);
                parameters.Add("@IdentificationOfBeneficialOwner", agents.IdentificationOfBeneficialOwner, DbType.Boolean);
                parameters.Add("@SupportingDocumentation8233", agents.SupportingDocumentation8233, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationSelfCertEntity", agents.PenaltiesOfPerjuryCertificationSelfCertEntity, DbType.Boolean);
                parameters.Add("@ElectronicSignatureSelfCertEntity", agents.ElectronicSignatureSelfCertEntity, DbType.Boolean);
                parameters.Add("@PenaltiesOfPerjuryCertificationSelfCertIndividual", agents.PenaltiesOfPerjuryCertificationSelfCertIndividual, DbType.Boolean);
                parameters.Add("@ElectronicSignatureSelfCertIndividual", agents.ElectronicSignatureSelfCertIndividual, DbType.Boolean);
                parameters.Add("@W9ExemptFromBUWIndividualSolePs", agents.W9ExemptFromBUWIndividualSolePs, DbType.Boolean);
                parameters.Add("@ResidencyInformationForm", agents.ResidencyInformationForm, DbType.Boolean);
                parameters.Add("@AddressLine3Optional", agents.AddressLine3Optional, DbType.Boolean);
                parameters.Add("@CapacitydonotshowforEntities", agents.CapacitydonotshowforEntities, DbType.Boolean);
                parameters.Add("@CapacitydonotshowforIndividuals", agents.CapacitydonotshowforIndividuals, DbType.Boolean);
                parameters.Add("@FormSelectionGuideDirectRequestQuestion", agents.FormSelectionGuideDirectRequestQuestion, DbType.Boolean);
                parameters.Add("@FormSelectionGuideREITQuestion", agents.FormSelectionGuideREITQuestion, DbType.Boolean);
                parameters.Add("@GeneralClassification", agents.GeneralClassification, DbType.Boolean);
                parameters.Add("@LanguageDropdown", agents.LanguageDropdown, DbType.Boolean);
                parameters.Add("@MultipleTaxJurisdictions", agents.MultipleTaxJurisdictions, DbType.Boolean);
                parameters.Add("@RetroactiveStatement", agents.RetroactiveStatement, DbType.String);
                parameters.Add("@RetroactiveEffectiveDate", agents.RetroactiveEffectiveDate, DbType.String);
                parameters.Add("@DoNotAcceptURL", agents.DoNotAcceptURL, DbType.String);
                parameters.Add("@FinishURL", agents.FinishURL, DbType.String);
                parameters.Add("@ExitURL", agents.ExitURL, DbType.String);
                parameters.Add("@SaveAndExitURL", agents.SaveAndExitURL, DbType.String);
                parameters.Add("@TaxpayerInformation_NonUSIndividualOnly", agents.TaxpayerInformation_NonUSIndividualOnly, DbType.Boolean);
                parameters.Add("@ModifiedOn", agents.ModifiedOn, DbType.DateTime);
                parameters.Add("@IsActive", agents.IsActive, DbType.Boolean);
                parameters.Add("@IsDeleted", agents.IsDeleted, DbType.Boolean);

                //await connection.ExecuteAsync("sp_UpdateAgent", parameters, commandType: CommandType.StoredProcedure);

                var result = await connection.ExecuteAsync("UpdateAgent", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        //public async Task<IReadOnlyList<AgentView>> GetAllAsync()
        //{
        //    var sql = "SELECT * FROM Agents";
        //    using (var connection = CreateConnection())
        //    {
        //        var result = await connection.QueryAsync<AgentView>(sql);
        //        return result.ToList();
        //    }
        //}

        public async Task<PaginationResponse<AgentView>> GetAllAsync(PaginationRequest request, string searchName)
        {
            using (var connection = CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IQueryable<AgentView> agents = connection.Query<AgentView>
                    ($@"SELECT * FROM Agents").AsQueryable();
                // and (name={searchName})

                // Apply search filter
                if (!string.IsNullOrEmpty(searchName))
                {
                    agents = agents.Where(f => f.Name.Contains(searchName));
                }
                // Sorting
                if (!string.IsNullOrEmpty(request.SortColumn))
                {
                    switch (request.SortDirection?.ToLower())
                    {
                        case "asc":
                            switch (request.SortColumn.ToLower())
                            {
                                case "name":
                                    agents = agents.OrderBy(f => f.Name);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "desc":
                            switch (request.SortColumn.ToLower())
                            {
                                case "name":
                                    agents = agents.OrderByDescending(f => f.Name);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }

                // Paging
                var totalRecords = agents.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalRecords / request.PageSize);
                var records = agents.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                // Return pagination response object
                return new PaginationResponse<AgentView>
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    Records = records
                };
            }
        }


        public async Task<AgentView> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Agents WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<AgentView>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Agents WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
    }
}
