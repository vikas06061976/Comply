using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.AgentEditList
{
    public class AgentDocumentationMandatoryViewModel
    {
        public int AgentId { get; set; }
        public int DocumentationId { get; set; }
        public string Name { get; set; }
        public bool IsUSSubmission { get; set; }
        public bool IsSelfCertification { get; set; }
    }
}
