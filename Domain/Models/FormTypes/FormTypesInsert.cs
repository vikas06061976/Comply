using Microsoft.AspNetCore.Http;
using System;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Domain.Models.FormTypes
{
    public class FormTypesInsert
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Revision { get; set; }
        public bool IsDisabled { get; set; }
        public PDFTemplate ESubmitPDFTemplateId { get; set; }
        public PDFTemplate PrintPDFTemplateId { get; set; }
        public IFormFile Logo { get; set; }
        public string LogoPath { get; set; }
        public string IntroductionText { get; set; }
        public string TINPageText { get; set; }
        public string CertificationText { get; set; }
        public string ESignatureText { get; set; }
        public string ESignatureConfirmationText { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
