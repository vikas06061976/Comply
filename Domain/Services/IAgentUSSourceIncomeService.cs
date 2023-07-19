using ComplyExchangeCMS.Domain.Models.Agent;
using ComplyExchangeCMS.Domain.Models.AgentEditList;
using ComplyExchangeCMS.Domain.Models.AgentUSSourceIncome;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface IAgentUSSourceIncomeService
    {

        #region Agent IncomeType Hidden
        Task<IEnumerable<AgentIncomeTypeViewModel>> GetAgentIncomeTypeHiddenByAgentIdAsync(int agentId);
        Task UpsertAgentIncomeTypeHiddenAsync(int agentId, List<int> existingAgentIncomeTypes);

        #endregion

        #region SourcedIncomeTypeSelection    
        Task<int> UpsertUSSourcedIncomeTypeSelectionAsync(USSourcedIncomeTypeSelectionUpsertModel agents); 
        Task<IEnumerable<USSourcedIncomeTypeSelectionViewModel>> GetAgentUSSourcedIncomeTypeSelectionAsync(int AgentId);
        Task<USSourcedIncomeTypeSelectionViewModel> GetAgentUSSourcedIncomeTypeSelectionByIdAsync
            (int agentid, int uSSourcedIncomeTypeId);
        #endregion

        #region Agent IncomeCode Hidden OnBoarding
        Task<IEnumerable<AgentIncomeCodeViewModel>> GetAgentHiddenIncomeCodeOnboardingByAgentIdAsync(int agentId);
        Task UpsertAgentHiddenIncomeCodeOnboardingAsync(int agentId, List<int> existingAgentIncomeCodes);
        #endregion
    }
}
