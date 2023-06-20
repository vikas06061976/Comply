using System;

namespace ComplyExchangeCMS.Domain.Models.Pages
{
    public class PageUpdateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
        public bool DisplayOnTopMenu { get; set; }
        public bool DisplayOnFooter { get; set; }
        public string RedirectPageLabelToURL { get; set; }
        public string MenuBackgroundColor { get; set; }
        public string UnselectedTextColor { get; set; }
        public string SelectedTextColor { get; set; }
        public bool DisplayOnLeftMenu { get; set; }
        public string PageContent { get; set; }
        public string Summary { get; set; }
        public bool  IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
