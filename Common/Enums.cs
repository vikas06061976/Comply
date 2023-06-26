using System.ComponentModel;

namespace ComplyExchangeCMS.Common
{
    public class Enums
    {
        public enum BalanceAgentCountriesImportant
        {
            Individual,
            Entity
        }
        public enum Trinary
        {
            True,
            False,
            Unknown
        }
        public enum EntityActivationStatus
        {
            Active,
            Deceased,
            Suspended
        }
        public enum HybridStatus
        {
            Hybrid,
            [Description("Reverse Hybrid")]
            ReverseHybrid,
            [Description(" Not Applicable")]
            NotApplicable
        }
        public enum Boolean
        {
            True,
            False
        }

        public enum BrandType
        {
            [Description("Own Brand")]
            OwnBrand,
            [Description("Competitor Brand")]
            CompetitorBrand
        }

        public enum Logo
        {
            [Description("Keep Existing")]
            KeepExisting =1,
            [Description("Upload")]
            Upload,
            [Description("Remove")]
            Remove
        }

        public enum PDFTemplate
        {
            [Description("International Individual")]
            InternationalIndividual =1,
            [Description("International Entity")]
            InternationalEntity,
            [Description("Custom")]
            Custom,
            [Description("Generic Entity1")]
            GenericEntity1,
            [Description("Generic Entity2")]
            GenericEntity2,
            [Description("Generic Individual")]
            GenericIndividual
        }

        public enum ContentManagement
        {
            [Description("Content Block")]
            ContentBlock = 1 ,
            [Description("Easy Help")]
            EasyHelp,
            [Description("Phrases")]
            Phrases
        }
    }
}