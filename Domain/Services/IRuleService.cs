﻿using ComplyExchangeCMS.Domain.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface IRuleService
    {
        Task<PaginationResponse<RulesView>> GetAllAsync
         (PaginationRequest request, string searchName);
        Task<int> InsertRules(RulesInsert rulesModel);
        Task<int> UpdateRules(RulesUpdate rulesModel);
        Task<RulesView> GetByIdAsync(int id);
        Task<int> DeleteRules(int id); 
        Task<int> InsertRulesTranslation(RuleTranslationInsert ruleModel);
        Task<RuleTranslationView> GetRuleTranslation(int ruleId, int languageId);
        Task<IReadOnlyList<RuleLanguageView>> GetAllLanguage(int ruleId);
    }
}

