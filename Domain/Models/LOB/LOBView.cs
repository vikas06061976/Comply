using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.LOB
{
    public class LOBView
    {
        public int Id { get; set; }
        public int Chapter3StatusId { get; set; }
        public bool IsCorporation { get; set; }
        public bool IsDisregardedEntity{ get; set; }
        public bool IsPartnership{ get; set; }
        public bool IsSimpleTrust{ get; set; }
        public bool IsGrantorTrust{ get; set; }
        public bool IsComplexTrust{ get; set; }
        public bool IsEstate{ get; set; }
        public bool IsGovernment{ get; set; }
        public bool IsCentralBankofIssue{ get; set; }
        public bool IsTaxExemptOrganization{ get; set; }
        public bool IsPrivateFoundation{ get; set; }
        public bool IsInternationalOrganization{ get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsActive { get; set; }
    }
}
