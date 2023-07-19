using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Domain.Models.AgentEditList
{
    public class AgentWrittenStatementSelectionViewModel
    {
        public int Id { get; set; }
        public int contentmanagementid { get; set; }
        public int AgentId { get; set; }    
        public string Name { get; set; }
        public int WrittenStatementReasonId { get; set; }
        public bool TaxJurisdictionMismatch { get; set; }
        public bool TaxResidencyMismatch { get; set; }
        public bool TelephoneCountryMismatch { get; set; }
        public bool AddressCountryMismatch { get; set; }
        public bool USCitizenshipAdditionalInfo { get; set; }
        public bool AccountCountryMismatch { get; set; }
        public bool NoFTINProvided { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }          
 
    }
}
