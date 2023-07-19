using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Domain.Models.AgentEditList
{
    public class AgentTINTypeSelectionByIdViewModel
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int AgentId { get; set; }
        public int TaxpayerIdTypeID { get; set; }
        public string TaxpayerIdTypeName { get; set; }


    }
}
