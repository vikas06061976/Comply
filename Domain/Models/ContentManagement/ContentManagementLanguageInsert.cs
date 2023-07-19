using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.ContentManagement
{
    public class ContentManagementLanguageInsert
    {
        public int LanguageId { get; set; }
        public int ContentBlockId { get; set; }
        public string Content { get; set; }
        public bool BulkTranslation { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
