using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Domain.Models.AgentEditList
{
    public class AgentTINTypeSelectionViewModel
    {
          public int Id { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int AgentId { get; set; }
        public int TaxpayerIdTypeID { get; set; }
        public string TaxpayerIdTypeName { get; set; } 
        public bool NonUSIndividual { get; set; }
        public bool USIndividual { get; set; }
        public bool USEntity { get; set; }
        public bool NonUSEntity { get; set; }
        public bool Intermediary { get; set; }
        public bool NonUSGovernment { get; set; } 
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }          
 
    }
}
