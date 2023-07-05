using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Settings
{
    public class SettingInsertTranslation
    {
        public string Content { get; set; }
        public string SettingId { get; set; }
        public string LanguageId { get; set; }
        public string BulkTranslation { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
