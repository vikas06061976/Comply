using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.Agent;
using ComplyExchangeCMS.Domain.Models.AgentEditList;
using ComplyExchangeCMS.Domain.Models.AgentUSSourceIncome;
using ComplyExchangeCMS.Domain.Models.Rules;
using ComplyExchangeCMS.Domain.Services;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class AgentUSSourceIncomeService : IAgentUSSourceIncomeService
    {
        private readonly IConfiguration _configuration;
        public AgentUSSourceIncomeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        #region  Agent IncomeType Hidden
        public async Task<IEnumerable<AgentIncomeTypeViewModel>> GetAgentIncomeTypeHiddenByAgentIdAsync(int agentId)
        {
            var sql = @"SELECT C.Id as IncomeTypeId, C.Name,AC.AgentId FROM IncomeTypes C 
                 left JOIN AgentIncomeType_Hidden AC ON C.Id = AC.IncomeTypeId and AC.AgentId =@agentId";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentIncomeTypeViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }

        public async Task UpsertAgentIncomeTypeHiddenAsync(int agentId, List<int> existingAgentIncomeTypeIds)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentIncomeTypeHidden = await connection.QueryAsync<int>
                    ("SELECT IncomeTypeId FROM AgentIncomeType_Hidden WHERE AgentId = @AgentId",
                   new { AgentId = agentId });

                var IncomeTypeToDelete = existingAgentIncomeTypeHidden.Except(existingAgentIncomeTypeIds);
                var IncomeTypeToAdd = existingAgentIncomeTypeIds.Except(existingAgentIncomeTypeHidden);

                if (IncomeTypeToDelete.Any())
                {
                    connection.Execute("delete from AgentIncomeType_Hidden WHERE AgentId = @AgentId AND IncomeTypeId IN @existingAgentIncomeTypeIds",
                        new { AgentId = agentId, existingAgentIncomeTypeIds = IncomeTypeToDelete });
                }

                var AgentIncomeTypeHidden = IncomeTypeToAdd.Select
                    (IncomeTypeId => new AgentIncomeTypeHidden
                    {
                        AgentId = agentId,
                        IncomeTypeId = IncomeTypeId,
                        CreatedOn = DateTime.Now
                    });

                if (AgentIncomeTypeHidden.Any())
                {
                    connection.Execute("INSERT INTO AgentIncomeType_Hidden(AgentId, IncomeTypeId, CreatedOn) " +
                        "VALUES (@AgentId, @IncomeTypeId, @CreatedOn)",
                        AgentIncomeTypeHidden);
                }
            }
        }
        #endregion
        #region USSourcedIncomeTypeSelection

        public async Task<IEnumerable<USSourcedIncomeTypeSelectionViewModel>> GetAgentUSSourcedIncomeTypeSelectionAsync(int agentId)
        {
            var sql = "select us.id as USSourcedIncomeTypeId,uts.id,us.USSourcedIncomeQuestion,us.QuestionText," +
                "uts.AgentId, uts.state,case  uts.state when 0 then 'Hidden' else 'Normal' end as Status,"+
               " uts.USSourcedIncomeQuestionAlias ,uts.QuestionTextAlias,uts.CreatedOn,uts.ModifiedOn " +
               "from USSourcedIncomeTypes us left outer join USSourcedIncomeTypeSelections uts" +
               " on us.id=uts.USSourcedIncomeTypeId and uts.AgentId= @agentId ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<USSourcedIncomeTypeSelectionViewModel>(sql, new { AgentId = agentId });
                return result;
            }
        }

        public async Task<USSourcedIncomeTypeSelectionViewModel> GetAgentUSSourcedIncomeTypeSelectionByIdAsync
            (int agentId, int uSSourcedIncomeTypeId)
        {
            var sql = "select  us.id as USSourcedIncomeTypeId,uts.id ,us.USSourcedIncomeQuestion,us.QuestionText," +
               "uts.AgentId, uts.state,case  uts.state when 0 then 'Hidden' else 'Normal' end as Status," +
              " uts.USSourcedIncomeQuestionAlias ,uts.QuestionTextAlias,uts.CreatedOn,uts.ModifiedOn " +
              "from USSourcedIncomeTypes us left outer join USSourcedIncomeTypeSelections uts" +
              " on us.id=uts.USSourcedIncomeTypeId  and  uts.AgentId=@agentid " +
              " where us.Id=@uSSourcedIncomeTypeId";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<USSourcedIncomeTypeSelectionViewModel>(sql, 
                    new { AgentId = agentId, USSourcedIncomeTypeId = uSSourcedIncomeTypeId });
                return result;
            }
        }
        public async Task<int> UpsertUSSourcedIncomeTypeSelectionAsync(USSourcedIncomeTypeSelectionUpsertModel upsertModel)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
            parameters.Add("@Id", upsertModel.Id, DbType.Int32);
            parameters.Add("@AgentId", upsertModel.AgentId, DbType.Int32);
            parameters.Add("@USSourcedIncomeTypeId", upsertModel.USSourcedIncomeTypeId, DbType.Int32);
            parameters.Add("@USSourcedIncomeQuestionAlias", upsertModel.USSourcedIncomeQuestionAlias, DbType.String);
            parameters.Add("@QuestionTextAlias", upsertModel.QuestionTextAlias, DbType.String);
            parameters.Add("@state", upsertModel.state, DbType.Boolean);
                parameters.Add("@output", dbType: DbType.Int16, direction: ParameterDirection.Output);
                await connection.ExecuteAsync("UpsertUSSourcedIncomeTypeSelection", parameters, commandType: CommandType.StoredProcedure);
               var result = parameters.Get<short>("@output");
            return result;
            }
        }

        #endregion
    }
}
