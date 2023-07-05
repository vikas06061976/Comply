using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Documentation
{
    public class DocumentTranslationInsert
    {
        public string Content { get; set; }
        public int DocId { get; set; }
        public int LanguageId { get; set; }
        public bool BulkTranslation { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
