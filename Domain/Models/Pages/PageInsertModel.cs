using System;

namespace ComplyExchangeCMS.Domain.Models.Pages
{
    public class PageInsertModel
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool DisplayOnTopMenu { get; set; }
        public string RedirectPageLabelToURL { get; set; }
        public string MenuBackgroundColor { get; set; }
        public string UnselectedTextColor { get; set; }
        public string SelectedTextColor { get; set; }
        public bool DisplayOnFooter { get; set; }
        public bool DisplayOnLeftMenu { get; set; }
        public string PageContent { get; set; }
        public string Summary { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
