using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Entities
{
    public class AgentCapacityHidden
    {
        public int AgentId { get; set; }
        public int CapacityId { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
