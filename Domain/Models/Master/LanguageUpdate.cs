using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Master
{
    public class LanguageUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}
