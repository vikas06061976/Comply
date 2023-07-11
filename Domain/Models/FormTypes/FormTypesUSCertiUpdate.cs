using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Domain.Models.FormTypes
{
    public class FormTypesUSCertiUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDisabled { get; set; }
        public string FullHeader { get; set; }
        public string SummaryHeader { get; set; }
        public string Description { get; set; }
        public string SubstituteFormStatement { get; set; }
        public string ESubmitStatement { get; set; }
        public Logo PrintTemplatePDFId { get; set; }
        public Logo ESubmitTemplatePDFId { get; set; }
        public IFormFile PrintTemplatePDF { get; set; }
        public IFormFile ESubmitTemplatePDF { get; set; }
        public string PrintTemplatePDF_ImagePath { get; set; }
        public string ESubmitTemplatePDF_ImagePath { get; set; }
        public bool UseOnboardingURL { get; set; }
        public string SpecifyURL { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
