using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.Agent;
using ComplyExchangeCMS.Domain.Models.AgentEditList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface IAgentEditListService
    {
        // Task<int> Upsert(AgentInsert agents);


        #region Agent Countries Important
        Task<IEnumerable<AgentCountriesViewModel>> GetAgentCountriesImportantByAgentIdAsync(int agentId);
        Task UpsertAgentCountriesImportantAsync(int agentId, List<int> existingAgentChapter3Entitytypes);
        #endregion

        #region Agent Countries Hidden
        Task<IEnumerable<AgentCountriesViewModel>> GetAgentCountriesHiddenByAgentIdAsync(int agentId);
        Task UpsertAgentCountriesHiddenAsync(int agentId, List<int> existingAgentChapter3Entitytypes);
        #endregion

        #region Agent Chapter3EntityType Hidden
        Task<IEnumerable<AgentChapter3EntityTypeViewModel>> GetAgentChapter3EntityTypeHiddenByAgentIdAsync(int agentId);
        Task UpsertAgentChapter3EntityTypeHiddenAsync(int agentId, List<int> existingAgentChapter3Entitytypes);
        #endregion

        #region Agent Chapter4EntityType Hidden
        Task<IEnumerable<AgentChapter4EntityTypeViewModel>> GetAgentChapter4EntityTypeHiddenByAgentIdAsync(int agentId);
        Task UpsertAgentChapter4EntityTypeHiddenAsync(int agentId, List<int> existingAgentChapter4Entitytypes);
        #endregion

        #region Agent Chapter4EntityType Important
        Task<IEnumerable<AgentChapter4EntityTypeViewModel>> GetAgentChapter4EntityTypeImportantByAgentIdAsync(int agentId);
        Task UpsertAgentChapter4EntityTypeImportantAsync(int agentId, List<int> existingAgentChapter4Entitytypes);
        #endregion


    }
}
