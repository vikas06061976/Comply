using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.FormTypes
{
    public class FormTypesUSCTranslationView
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int FormUSCId { get; set; }
        public int LanguageId { get; set; }
        public string SubstituteStatement { get; set; }
        public bool BulkTranslation { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
