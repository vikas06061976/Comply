using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Pages
{
    public class PageTranslationView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PageId { get; set; }
        public int LanguageId { get; set; }
        public string PageContent { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
