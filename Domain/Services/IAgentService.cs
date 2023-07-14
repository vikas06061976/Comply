using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.Agent;
using ComplyExchangeCMS.Domain.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface IAgentService
    {
        Task<int> Insert(AgentInsert agents);
        Task<int> UpdateAgents(AgentUpdate agents);
        //Task<IReadOnlyList<AgentView>> GetAllAsync();
        Task<PaginationResponse<AgentView>> GetAllAsync(PaginationRequest request, string searchName);
        Task<AgentView> GetByIdAsync(int id);
        Task<int> DeleteAsync(int id);
        Task<IReadOnlyList<ModuleLanguageView>> GetAllLanguage(int agentId);
        Task<int> InsertAgentTranslation(AgentTranslationUpsert agentModel);
        Task<AgentTranslationView> GetAgentTranslation(int agentId, int languageId);
    }
}
