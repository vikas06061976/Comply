using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.FormTypes
{
    public class FormTypeSelfCertiTranslationInsert
    {
        public string IntroductionText { get; set; }
        public int FormSCId { get; set; }
        public int LanguageId { get; set; }
        public string TINPageText { get; set; }
        public string CertificationText { get; set; }
        public string ESignatureText { get; set; }
        public string ESignatureConfirmationText { get; set; }
        public bool BulkTranslation { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
