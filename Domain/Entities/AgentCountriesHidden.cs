using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Entities
{
    public class AgentCountriesHidden
    {
        public int AgentId { get; set; }
        public int CountryId { get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
