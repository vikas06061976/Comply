using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.AgentEditList
{
    public class AgentSPTQuestionInsertModel
    {
        public int SPTQuestionId { get; set; }
        public bool Hidden { get; set; }
        public string Alias { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
