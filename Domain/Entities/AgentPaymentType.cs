using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Entities
{
    public class AgentPaymentType
    {
        public int AgentId { get; set; }
        public int PaymentTypeId { get; set; }
        public bool MakeDefault { get; set; }
        public bool Hide { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
