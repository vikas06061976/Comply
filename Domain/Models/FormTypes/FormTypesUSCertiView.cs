using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Domain.Models.FormTypes
{
    public class FormTypesUSCertiView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public string FullHeader { get; set; }
        public string SummaryHeader { get; set; }
        public string Description { get; set; }
        public string SubstituteFormStatement { get; set; }
        public string ESubmitStatement { get; set; }
        public Logo PrintTemplatePDF { get; set; }
        public Logo ESubmitTemplatePDF { get; set; }
        public bool UseOnboardingURL { get; set; }
        public string SpecifyURL { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
