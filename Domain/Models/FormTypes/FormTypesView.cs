using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Domain.Models.FormTypes
{
    public class FormTypesView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Revision { get; set; }
        public bool IsDisabled { get; set; }
        public PDFTemplate ESubmitPDFTemplate { get; set; }
        public PDFTemplate PrintPDFTemplate { get; set; }
        public string Logo { get; set; }
        public string IntroductionText { get; set; }
        public string TINPageText { get; set; }
        public string CertificationText { get; set; }
        public string ESignatureText { get; set; }
        public string ESignatureConfirmationText { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
