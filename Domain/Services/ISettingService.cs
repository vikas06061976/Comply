﻿using ComplyExchangeCMS.Domain.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface ISettingService
    {
        Task<int> UpsertSetting(SettingInsertModel settingModel);
        Task<SettingViewModel> GetSetting();
        Task<IReadOnlyCollection<QuestionView>> GetQuestions();
        Task<int> InsertQuestionTranslation(QuestionTranslationInsert settingModel);
        Task<QuestionTranslationView> GetQuestionTranslation(int? questionId, int? questionHintId, int languageId);
    }
}
