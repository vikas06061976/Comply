using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Documentation
{
    public class DocTranslationView
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public int DocId { get; set; }
    }
}
