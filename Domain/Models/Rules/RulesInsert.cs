using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ComplyExchangeCMS.Domain.Models.Rules
{
    public class RulesInsert
    {
        public string Code { get; set; }
        public string RuleClass { get; set; }
        public string Warning { get; set; }
        public bool isNotAllowedSubmissionToContinue { get; set; }
        public bool DisableRule { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
