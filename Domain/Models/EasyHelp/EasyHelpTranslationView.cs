using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.EasyHelp
{
    public class EasyHelpTranslationView
    {
        public int Id { get; set; }
        public string ToolTip { get; set; }
        public int EasyHelpId { get; set; }
        public int LanguageId { get; set; }
        public string Text { get; set; }
        public string MoreText { get; set; }
        public bool BulkTranslation { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
