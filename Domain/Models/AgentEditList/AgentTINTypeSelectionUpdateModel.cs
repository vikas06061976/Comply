using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Domain.Models.AgentEditList
{
    public class AgentTINTypeSelectionUpdateModel
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
