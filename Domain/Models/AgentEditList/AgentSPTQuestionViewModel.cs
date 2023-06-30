using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.AgentEditList
{
    public class AgentSPTQuestionViewModel
    {
        public int AgentId { get; set; }
        public int SPTQuestionId { get; set; }
        public bool Hidden { get; set; }
        public string Alias { get; set; }
        public string Name { get; set; }
  
    }
}
