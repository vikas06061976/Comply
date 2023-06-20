using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Domain.Models.FormTypes
{
    public class FormTypesUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Revision { get; set; }
        public bool IsDisabled { get; set; }
        public PDFTemplate ESubmitPDFTemplateId { get; set; }
        public PDFTemplate PrintPDFTemplateId { get; set; }
        public IFormFile Logo { get; set; }
        public string IntroductionText { get; set; }
        public string TINPageText { get; set; }
        public string CertificationText { get; set; }
        public string ESignatureText { get; set; }
        public string ESignatureConfirmationText { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
