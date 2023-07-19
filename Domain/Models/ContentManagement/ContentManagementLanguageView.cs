using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.ContentManagement
{
    public class ContentManagementLanguageView
    {
        public int LanguageId { get; set; }
        public int ContentBlockId { get; set; }
        public string Content { get; set; }
        public bool BulkTranslation { get; set; }
    }
}
