using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.EasyHelp
{
    public class EasyHelpUpdate
    {
        public int Id { get; set; }
        public string Easykey { get; set; }
        public string Tooltip { get; set; }
        public string Text { get; set; }
        public string MoreText { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
