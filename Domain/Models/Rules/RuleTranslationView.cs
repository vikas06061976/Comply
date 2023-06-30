using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Rules
{
    public class RuleTranslationView
    {
        public int Id { get; set; }
        public string Warning { get; set; }
        public int RulesId { get; set; }
        public int LanguageId { get; set; }
        public bool BulkTranslation { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
