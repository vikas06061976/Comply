using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Agent
{
    public class AgentTranslationView
    {
        public int Id { get; set; }
        public int AgentId { get; set; }
        public int LanguageId { get; set; }
        public string Description { get; set; }
        public string TermsCondition { get; set; }
        public string TokenEmail { get; set; }
        public string SendForSignatoryEmail { get; set; }
        public string SendForSignatoryEmailContinuationLink { get; set; }
        public string SaveAndExitEmail { get; set; }
        public string NextAgentIntroductionText { get; set; }
        public string WelcomePopup { get; set; }
        public bool BulkTranslation { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
