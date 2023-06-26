using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Master
{
    public class LanguageInsert
    {
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    }
}
