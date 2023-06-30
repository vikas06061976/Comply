using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.AgentEditList
{
    public class AgentPaymentTypeInsertModel
    { 
        public int PaymentTypeId { get; set; }
        public bool MakeDefault { get; set; }
        public bool Hide { get; set; } 
        public DateTime? ModifiedOn { get; set; }
    }
}
