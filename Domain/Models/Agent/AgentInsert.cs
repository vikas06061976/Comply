using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Domain.Models.Agent
{
    public class AgentInsert
    {
        public string Name { get; set; }
        public string W9RequestorName { get; set; }
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string CityTown { get; set; }
        public string StateProvince { get; set; }
        public string ZipPostalCode { get; set; }
        public int CountryId { get; set; }
        public string Other { get; set; }
        public bool DefaultSelection { get; set; }
        public int DefaultLanguageId { get; set; }
        public bool IncludeDefaultEnglish { get; set; }
        public Logo LogoId { get; set; }
        public IFormFile Logo { get; set; }
        public string Logo_ImagePath { get; set; }
        public string LogoNavigateURL { get; set; }
        public string PDFWatermark { get; set; }
        public bool DisplayVersion { get; set; }
        public bool DisplayRenderTime { get; set; }
        public string FormFeedUsername { get; set; }
        public string FormFeedPassword { get; set; }
        public string TINCheckUsername { get; set; }
        public string TINCheckPassword { get; set; }
        public bool SupportsTINValidation { get; set; }
        public bool ContinueAfterTINValidationFailure { get; set; }
        public string TermsAndConditions { get; set; }
        public string TokenEmail { get; set; }
        public bool OnboardingAPIActive { get; set; }
        public bool SkipAHDPage { get; set; }
        public bool SkipTCPage { get; set; }
        public bool StoreCDFOnTheFly { get; set; }
        public bool StoreCDFAndFormOnAfterFormSubmission { get; set; }
        public bool ShowUIDEntryFieldInTheEntityDetailsScreen { get; set; }
        public string ShowUIDEntryFieldInTheEntityDetailsScreenRequiredFormat { get; set; }
        public bool ShowUIDEntryFieldInTheEntityDetailsScreenIncludeUIDOnReferenceLine { get; set; }
        public bool UpdateCDFRecordOnTheFly { get; set; }
        public bool AllowSecondSignatureSubmission { get; set; }
        public bool AllowSecondSignatureSubmissionUseSameAgent { get; set; }
        public bool AllowSecondSignatureSubmissionSecondSubmissionMandatory { get; set; }
        public bool IncludeAdditionalInformationOnESubmitPDF { get; set; }
        public bool ShowUSourcedIncomeDeclaration { get; set; }
        public bool ShowUSourcedIncomeDeclarationAndWhenNoUSSourcedIncomeHideChapter4AndDREPage { get; set; }
        public bool ShowRetroactiveStatementOnlyShowApplyForW8Forms { get; set; }
        public bool ShowRetroactiveStatementOnlyShowApplyForW8FormsRenderFullScreen { get; set; }
        public bool ShowRetroactiveStatementOnlyShowApplyForW8FormsMakeMandatory { get; set; }
        public bool EnableSaveExitProcess { get; set; }
        public bool EnableAllocationStatementCreation { get; set; }
        public bool ConsentToSendAnElectronic1042SOr1099 { get; set; }
        public bool EnableExemptFromBackupWithholdingPagePopUp { get; set; }
        public bool HideDownloadTemplateToCompleteWithholdingStatement { get; set; }
        public bool RequestBankAccountInformation { get; set; }
        public bool RequestBankAccountInformationAndWhenYesMakeMandatory { get; set; }
        public bool HideW8BENETreaty14C { get; set; }
        public bool Forms { get; set; }
        public bool FederalTax { get; set; }
        public bool SingleUSOwnerDetails { get; set; }
        public bool TaxpayerInformation_IndividualOnly { get; set; }
        public bool TaxpayerInformation_EntityOnly { get; set; }
        public bool TaxpayerInformation_NonUSEntityOnly { get; set; }
        public bool TaxpayerInformation_GIIN { get; set; }
        public bool FormSelection { get; set; }
        public bool W8IMYRelatedFiles { get; set; }
        public bool W8BENJuly2017PartIIWhenTreatyClaimNo { get; set; }
        public bool W8BENEPartIIIWhenTreatyClaimNo { get; set; }
        public bool PenaltiesOfPerjuryCertification8233 { get; set; }
        public bool ElectronicSignature8233 { get; set; }
        public bool NameAndAddress { get; set; }
        public bool Chapter3Status { get; set; }
        public bool USSourcedIncomeDeclarationOptional { get; set; }
        public bool IncomeCode { get; set; }
        public bool UnitedStatesCitizenshipStatus { get; set; }
        public bool UnitedStatesSubstantialPresenceTestOptional { get; set; }
        public bool USFATCAClassification { get; set; }
        public bool Chapter4Status { get; set; }
        public bool DisregardedEntity { get; set; }
        public bool ExemptionFromBackupWithholding { get; set; }
        public bool ExemptionFromFATCAReporting { get; set; }
        public bool TaxIdentificationNumber { get; set; }
        public bool SpecifiedUSPersonDetermination { get; set; }
        public bool ECIIncomeReport { get; set; }
        public bool TreatyClaim { get; set; }
        public bool SpecialRatesAndConditions { get; set; }
        public bool SupportingDocumentationW9 { get; set; }
        public bool SupportingDocumentationBEN { get; set; }
        public bool SupportingDocumentationBENE { get; set; }
        public bool SupportingDocumentationIMY { get; set; }
        public bool SupportingDocumentationEXP { get; set; }
        public bool PenaltiesOfPerjuryCertificationW9 { get; set; }
        public bool SupportingDocumentationECI { get; set; }
        public bool ElectronicSignatureW9 { get; set; }
        public bool PenaltiesOfPerjuryCertifcationBENE { get; set; }
        public bool PenaltiesOfPerjuryCertificationBEN { get; set; }
        public bool ElectronicSignatureBEN { get; set; }
        public bool PenaltiesOfPerjuryCertificationIMY { get; set; }
        public bool ElectronicSignatureIMY { get; set; }
        public bool PenaltiesOfPerjuryCertificationEXP { get; set; }
        public bool ElectronicSignatureEXP { get; set; }
        public bool PenaltiesOfPerjuryCertificationECI { get; set; }
        public bool ElectronicSignatureECI { get; set; }
        public bool USTaxCertificationComplete { get; set; }
        public bool SubstantialUSPresenceTest { get; set; }
        public bool IdentificationOfBeneficialOwner { get; set; }
        public bool SupportingDocumentation8233 { get; set; }
        public bool PenaltiesOfPerjuryCertificationSelfCertEntity { get; set; }
        public bool ElectronicSignatureSelfCertEntity { get; set; }
        public bool PenaltiesOfPerjuryCertificationSelfCertIndividual { get; set; }
        public bool ElectronicSignatureSelfCertIndividual { get; set; }
        public bool W9ExemptFromBUWIndividualSolePs { get; set; }
        public bool ResidencyInformationForm { get; set; }
        public bool AddressLine3Optional { get; set; }
        public bool CapacitydonotshowforEntities { get; set; }
        public bool CapacitydonotshowforIndividuals { get; set; }
        public bool FormSelectionGuideDirectRequestQuestion { get; set; }
        public bool FormSelectionGuideREITQuestion { get; set; }
        public bool GeneralClassification { get; set; }
        public bool LanguageDropdown { get; set; }
        public bool MultipleTaxJurisdictions { get; set; }
        public string RetroactiveStatement { get; set; }
        public string RetroactiveEffectiveDate { get; set; }
        public string DoNotAcceptURL { get; set; }
        public string FinishURL { get; set; }
        public string ExitURL { get; set; }
        public string SaveAndExitURL { get; set; }
        public bool TaxpayerInformation_NonUSIndividualOnly { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}

