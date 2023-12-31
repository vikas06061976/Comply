﻿using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.Agent;
using ComplyExchangeCMS.Domain.Models.AgentEditList;
using ComplyExchangeCMS.Domain.Services;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class AgentEditListService : IAgentEditListService
    { 
        private readonly IConfiguration _configuration;
        public AgentEditListService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Upsert(AgentInsert agents)
        {
            throw new NotImplementedException();
        }
        #region Agent Countries Hidden
        public async Task<IEnumerable<AgentCountriesViewModel>> GetAgentCountriesHiddenByAgentIdAsync(int agentId)
        {
            var sql = @"  SELECT C.Id as CountryId, C.Name,AC.AgentId FROM Countries C 
                 left JOIN AgentCountries_Hidden AC ON C.Id = AC.CountryId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentCountriesViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }
        public async Task UpsertAgentCountriesHiddenAsync(int agentId, List<int> existingAgentCountryIds)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentCountriesHidden = await connection.QueryAsync<int>("SELECT CountryId FROM AgentCountries_Hidden WHERE AgentId = @AgentId",
                   new { AgentId = agentId });

                var CountriesToDelete = existingAgentCountriesHidden.Except(existingAgentCountryIds);
                var CountriesToAdd = existingAgentCountryIds.Except(existingAgentCountriesHidden);

                if (CountriesToDelete.Any())
                {
                    connection.Execute("delete from AgentCountries_Hidden WHERE AgentId = @AgentId AND CountryId IN @existingAgentCountryIds",
                        new { AgentId = agentId, existingAgentCountryIds = CountriesToDelete });
                }

                var AgentCountriesHidden = CountriesToAdd.Select
                    (countryId => new AgentCountriesHidden
                    {
                        AgentId = agentId,
                        CountryId = countryId,
                        CreatedOn = DateTime.Now
                    });

                if (AgentCountriesHidden.Any())
                {
                    connection.Execute("INSERT INTO AgentCountries_Hidden(AgentId, CountryId, CreatedOn) " +
                        "VALUES (@AgentId, @CountryId, @CreatedOn)",
                        AgentCountriesHidden);
                }
            }
        }

        #endregion

        #region Agent Countries Important
        public async Task<IEnumerable<AgentCountriesViewModel>> GetAgentCountriesImportantByAgentIdAsync(int agentId)
        {
            var sql = @"  SELECT C.Id as CountryId, C.Name,AC.AgentId FROM Countries C 
                 left JOIN AgentCountries_Important AC ON C.Id = AC.CountryId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentCountriesViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }
        public async Task UpsertAgentCountriesImportantAsync(int agentId, List<int> existingAgentCountryIds)
        {
            using (var connection = CreateConnection())
            {
          var existingAgentCountriesImportant =await connection.QueryAsync<int>("SELECT CountryId FROM AgentCountries_Important WHERE AgentId = @AgentId",
             new { AgentId = agentId });

                var CountriesToDelete = existingAgentCountriesImportant.Except(existingAgentCountryIds);
                var CountriesToAdd = existingAgentCountryIds.Except(existingAgentCountriesImportant);

                if (CountriesToDelete.Any())
                {
                    connection.Execute("delete from AgentCountries_Important WHERE AgentId = @AgentId AND CountryId IN @existingAgentCountryIds",
                        new { AgentId = agentId, existingAgentCountryIds = CountriesToDelete });
                }
                var AgentCountriesImportant = CountriesToAdd.Select

                    (countryId => new AgentCountriesImportant
                    {
                    AgentId = agentId,
                    CountryId = countryId,
                   CreatedOn=DateTime.Now
                });

                if (AgentCountriesImportant.Any())
                {
                    connection.Execute("INSERT INTO AgentCountries_Important (AgentId, CountryId, CreatedOn) " +
                        "VALUES (@AgentId, @CountryId, @CreatedOn)",
                        AgentCountriesImportant);
                }
            }
        }
        #endregion

        #region Agent Chapter3EntityType Hidden
        public async Task<IEnumerable<AgentChapter3EntityTypeViewModel>> GetAgentChapter3EntityTypeHiddenByAgentIdAsync(int agentId)
        {
            var sql = @"  SELECT C.Id as Chapter3EntityTypeId, C.Name,AC.AgentId FROM Chapter3EntityTypes C 
                 left JOIN AgentChapter3EntityType_Hidden AC ON C.Id = AC.Chapter3EntityTypeId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentChapter3EntityTypeViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }
        public async Task UpsertAgentChapter3EntityTypeHiddenAsync(int agentId, List<int> existingAgentChapter3EntityTypeIds)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentChapter3EntityTypeHidden = await connection.QueryAsync<int>
                    ("SELECT Chapter3EntityTypeId FROM AgentChapter3EntityType_Hidden WHERE AgentId = @AgentId",
                   new { AgentId = agentId });

                var Chapter3EntityTypeToDelete = existingAgentChapter3EntityTypeHidden.Except(existingAgentChapter3EntityTypeIds);
                var Chapter3EntityTypeToAdd = existingAgentChapter3EntityTypeIds.Except(existingAgentChapter3EntityTypeHidden);

                if (Chapter3EntityTypeToDelete.Any())
                {
                    connection.Execute("delete from AgentChapter3EntityType_Hidden WHERE AgentId = @AgentId AND CountryId IN @existingAgentChapter3EntityTypeIds",
                        new { AgentId = agentId, existingAgentCountryIds = Chapter3EntityTypeToDelete });
                }

                var AgentChapter3EntityTypeHidden = Chapter3EntityTypeToAdd.Select
                    (chapter3EntityTypeId => new AgentChapter3EntityTypeHidden
                    {
                        AgentId = agentId,
                        Chapter3EntityTypeId = chapter3EntityTypeId,
                        CreatedOn = DateTime.Now
                    });

                if (AgentChapter3EntityTypeHidden.Any())
                {
                    connection.Execute("INSERT INTO AgentChapter3EntityType_Hidden(AgentId, Chapter3EntityTypeId, CreatedOn) " +
                        "VALUES (@AgentId, @Chapter3EntityTypeId, @CreatedOn)",
                        AgentChapter3EntityTypeHidden);
                }
            }
        }

        #endregion

        #region Agent Chapter4EntityType Hidden
        public async Task<IEnumerable<AgentChapter4EntityTypeViewModel>> GetAgentChapter4EntityTypeHiddenByAgentIdAsync(int agentId)
        {
            var sql = @"  SELECT C.Id as Chapter4EntityTypeId, C.Name,AC.AgentId FROM Chapter4EntityTypes C 
                 left JOIN AgentChapter4EntityType_Hidden AC ON C.Id = AC.Chapter4EntityTypeId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentChapter4EntityTypeViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }
        public async Task UpsertAgentChapter4EntityTypeHiddenAsync(int agentId, List<int> existingAgentChapter4EntityTypeIds)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentChapter4EntityTypeHidden = await connection.QueryAsync<int>
                    ("SELECT Chapter4EntityTypeId FROM AgentChapter4EntityType_Hidden WHERE AgentId = @AgentId",
                   new { AgentId = agentId });

                var Chapter4EntityTypeToDelete = existingAgentChapter4EntityTypeHidden.Except(existingAgentChapter4EntityTypeIds);
                var Chapter4EntityTypeToAdd = existingAgentChapter4EntityTypeIds.Except(existingAgentChapter4EntityTypeHidden);

                if (Chapter4EntityTypeToDelete.Any())
                {
                    connection.Execute("delete from AgentChapter4EntityType_Hidden WHERE AgentId = @AgentId AND Chapter4EntityTypeId IN @existingAgentChapter4EntityTypeIds",
                        new { AgentId = agentId, existingAgentCountryIds = Chapter4EntityTypeToDelete });
                }

                var AgentChapter4EntityTypeHidden = Chapter4EntityTypeToAdd.Select
                    (Chapter4EntityTypeId => new AgentChapter4EntityTypeHidden
                    {
                        AgentId = agentId,
                        Chapter4EntityTypeId = Chapter4EntityTypeId,
                        CreatedOn = DateTime.Now
                    });

                if (AgentChapter4EntityTypeHidden.Any())
                {
                    connection.Execute("INSERT INTO AgentChapter4EntityType_Hidden(AgentId, Chapter4EntityTypeId, CreatedOn) " +
                        "VALUES (@AgentId, @Chapter4EntityTypeId, @CreatedOn)",
                        AgentChapter4EntityTypeHidden);
                }
            }
        }

        #endregion

        #region Agent Chapter4EntityType Important
        public async Task<IEnumerable<AgentChapter4EntityTypeViewModel>> GetAgentChapter4EntityTypeImportantByAgentIdAsync(int agentId)
        {
            var sql = @"  SELECT C.Id as Chapter4EntityTypeId, C.Name,AC.AgentId FROM Chapter4EntityTypes C 
                 left JOIN AgentChapter4EntityType_Important AC ON C.Id = AC.Chapter4EntityTypeId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentChapter4EntityTypeViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }
        public async Task UpsertAgentChapter4EntityTypeImportantAsync(int agentId, List<int> existingAgentChapter4EntityTypeIds)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentChapter4EntityTypeImportant = await connection.QueryAsync<int>
                    ("SELECT Chapter4EntityTypeId FROM AgentChapter4EntityType_Important WHERE AgentId = @AgentId",
                   new { AgentId = agentId });

                var Chapter4EntityTypeToDelete = existingAgentChapter4EntityTypeImportant.Except(existingAgentChapter4EntityTypeIds);
                var Chapter4EntityTypeToAdd = existingAgentChapter4EntityTypeIds.Except(existingAgentChapter4EntityTypeImportant);

                if (Chapter4EntityTypeToDelete.Any())
                {
                    connection.Execute("delete from AgentChapter4EntityType_Important WHERE AgentId = @AgentId AND Chapter4EntityTypeId IN @existingAgentChapter4EntityTypeIds",
                        new { AgentId = agentId, existingAgentCountryIds = Chapter4EntityTypeToDelete });
                }

                var AgentChapter4EntityTypeImportant = Chapter4EntityTypeToAdd.Select
                    (Chapter4EntityTypeId => new AgentChapter4EntityTypeImportant
                    {
                        AgentId = agentId,
                        Chapter4EntityTypeId = Chapter4EntityTypeId,
                        CreatedOn = DateTime.Now
                    });

                if (AgentChapter4EntityTypeImportant.Any())
                {
                    connection.Execute("INSERT INTO AgentChapter4EntityType_Important(AgentId, Chapter4EntityTypeId, CreatedOn) " +
                        "VALUES (@AgentId, @Chapter4EntityTypeId, @CreatedOn)",
                        AgentChapter4EntityTypeImportant);
                }
            }
        }

        #endregion

    }
}
