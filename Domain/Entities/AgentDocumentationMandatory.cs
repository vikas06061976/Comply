using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Entities
{
    public class AgentDocumentationMandatory
    {
        public int AgentId { get; set; }
        public int DocumentationId { get; set; }
        public bool IsUSSubmission { get; set; }
        public bool IsSelfCertification { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
