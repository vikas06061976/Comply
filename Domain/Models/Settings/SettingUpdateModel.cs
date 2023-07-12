using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Models.Settings
{
    public class SettingUpdateModel
    {
        public int Id { get; set; }
        public IFormFile DefaultCoverPagePdf_FileName { get; set; }
        public int LengthOfConfirmationCode { get; set; }
        public string DefaultLogoType { get; set; }
        public string DefaultLogo_FileName { get; set; }
        public string GoogleTranslateAPIKey { get; set; }
        public string PurgeRedundantSubmissionData { get; set; }
        public bool RunExchangeInIframe { get; set; }
        public string DefaultRetroactiveStatement { get; set; }
        public bool UnderMaintenance { get; set; }
        public bool ReSendTokenEmailFeature { get; set; }
        public bool ActivateNonEmailPINprocess { get; set; }
        public bool BlockForeignCharacterInput { get; set; }
        public string TwilioAuthToken { get; set; }
        public string TwilioAccountSid { get; set; }
        public string TwilioSMSFromMobileNumber { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
