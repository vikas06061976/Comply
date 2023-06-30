using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Entities
{
    public class AgentSPTQuestionHidden
    {
        public int AgentId { get; set; }
        public int SPTQuestionId { get; set; }
        public bool Hidden { get; set; }
        public string Alias { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
