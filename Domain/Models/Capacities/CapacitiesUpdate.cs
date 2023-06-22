using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Capacities
{
    public class CapacitiesUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool isProxyMandatory { get; set; }
        public bool isCountryOfResidenceRequired { get; set; }
        public bool isImportant { get; set; }
        public bool isUSIndividual { get; set; }
        public bool isNonUSIndividual { get; set; }
        public bool isUSBusiness { get; set; }
        public bool isNonUSBusiness { get; set; }
        public bool isIntermediary { get; set; }
        public bool isNonUSGovernment { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }

    }
}
