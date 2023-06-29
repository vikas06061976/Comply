using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Entities
{
    public class AgentExemptionCodeDisabled
    {
        public int AgentId { get; set; }
        public int ExemptionCodeId { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
