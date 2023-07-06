using ComplyExchangeCMS.Domain.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface ISettingService
    {
        Task<int> InsertSetting(SettingInsertModel settingModel);
        Task<SettingViewModel> GetSetting();
        Task<int> InsertSettingTranslation(SettingInsertTranslation settingModel);
        Task<SettingViewTranslation> GetSettingTranslation(int settingId, int languageId);
    }
}
