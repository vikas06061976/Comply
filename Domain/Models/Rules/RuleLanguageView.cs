using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Rules
{
    public class RuleLanguageView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RulesId { get; set; }
    }
}
