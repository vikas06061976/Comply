using ComplyExchangeCMS.Domain;
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

        #region Agent Capacity Hidden
        public async Task<IEnumerable<AgentCapacityHiddenViewModel>> GetAgentCapacityHiddenByAgentIdAsync(int agentId)
        {
            var sql = @"  SELECT C.Id as CapacityId, C.Name,AC.AgentId FROM Capacities C 
                 left JOIN AgentCapacity_Hidden AC ON C.Id = AC.CapacityId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentCapacityHiddenViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }
        public async Task UpsertAgentCapacityHiddenAsync(int agentId, List<int> existingAgentCapacityIds)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentCapacityHidden = await connection.QueryAsync<int>
                    ("SELECT CapacityId FROM AgentCapacity_Hidden WHERE AgentId = @AgentId",
                   new { AgentId = agentId });

                var CapacityToDelete = existingAgentCapacityHidden.Except(existingAgentCapacityIds);
                var CapacityToAdd = existingAgentCapacityIds.Except(existingAgentCapacityHidden);

                if (CapacityToDelete.Any())
                {
                    connection.Execute("delete from AgentCapacity_Hidden WHERE AgentId = @AgentId AND CapacityId IN @existingAgentCapacityIds",
                        new { AgentId = agentId, existingAgentCapacityIds = CapacityToDelete });
                }

                var AgentCapacityHidden = CapacityToAdd.Select
                    (CapacityId => new AgentCapacityHidden
                    {
                        AgentId = agentId,
                        CapacityId = CapacityId,
                        CreatedOn = DateTime.Now
                    });

                if (AgentCapacityHidden.Any())
                {
                    connection.Execute("INSERT INTO AgentCapacity_Hidden(AgentId, CapacityId, CreatedOn) " +
                        "VALUES (@AgentId, @CapacityId, @CreatedOn)",
                        AgentCapacityHidden);
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

        #region Agent Document Mandatory
        public async Task<IEnumerable<AgentDocumentationMandatoryViewModel>> GetAgentDocumentationMandatoryByAgentIdAsync(int agentId)
        {
            var sql = @"  SELECT C.Id as DocumentationId, C.Name,AC.AgentId,
AC.IsUSSubmission,AC.IsSelfCertification FROM Documentations C 
                 left JOIN AgentDocumentation_Mandatory AC ON C.Id = AC.DocumentationId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentDocumentationMandatoryViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }
        public async Task UpsertAgentDocumentationMandatoryAsync(int agentId, List<AgentDocumentationMandatoryInsertModel> existingAgentDocuments)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentDocumentationMandatory = await connection.QueryAsync<AgentDocumentationMandatory>("SELECT AgentId,DocumentationId, IsUSSubmission, IsSelfCertification FROM AgentDocumentation_Mandatory WHERE AgentId = @AgentId",
                    new { AgentId = agentId });
 
                var DocumentationIdsToDelete = existingAgentDocumentationMandatory
          .Where(d => !existingAgentDocuments.Any(e => e.DocumentationId == d.DocumentationId)
        //  || (!d.IsUSSubmission && !d.IsSelfCertification 
        || existingAgentDocuments.Any(e => e.DocumentationId == d.DocumentationId && !e.IsUSSubmission && !e.IsSelfCertification))
          .Select(d => d.DocumentationId);

                var DocumentationIdsToAdd = existingAgentDocuments
                    .Where(e => !existingAgentDocumentationMandatory.Any(d => d.DocumentationId == e.DocumentationId))
                    .Select(e => e.DocumentationId);

                if (DocumentationIdsToDelete.Any())
                {
                    connection.Execute("DELETE FROM AgentDocumentation_Mandatory WHERE AgentId = @AgentId AND DocumentationId IN @existingAgentDocumentsIds",
                        new { AgentId = agentId, existingAgentDocumentsIds = DocumentationIdsToDelete });
                }

                foreach (var documentation in existingAgentDocuments)
                {
                    var existingDocumentation = existingAgentDocumentationMandatory.FirstOrDefault
                        (d => d.DocumentationId == documentation.DocumentationId);

                    if (existingDocumentation != null)
                    {
                        if (!existingDocumentation.IsUSSubmission || !existingDocumentation.IsSelfCertification)
                        {
                            existingDocumentation.IsUSSubmission = documentation.IsUSSubmission;
                            existingDocumentation.IsSelfCertification = documentation.IsSelfCertification;

                            connection.Execute("UPDATE AgentDocumentation_Mandatory SET IsUSSubmission = @IsUSSubmission, IsSelfCertification = @IsSelfCertification,ModifiedOn=@ModifiedOn WHERE AgentId = @AgentId AND DocumentationId = @DocumentationId",
                                new
                                {
                                    AgentId = agentId,
                                    DocumentationId = existingDocumentation.DocumentationId,
                                    IsUSSubmission = existingDocumentation.IsUSSubmission,
                                    IsSelfCertification = existingDocumentation.IsSelfCertification,
                                    ModifiedOn=DateTime.Now
                                });
                        }
                    }
                    else
                    {
                        connection.Execute("INSERT INTO AgentDocumentation_Mandatory (AgentId, DocumentationId, IsUSSubmission, IsSelfCertification, CreatedOn) " +
                            "VALUES (@AgentId, @DocumentationId, @IsUSSubmission, @IsSelfCertification, @CreatedOn)",
                            new
                            {
                                AgentId = agentId,
                                DocumentationId = documentation.DocumentationId,
                                IsUSSubmission = documentation.IsUSSubmission,
                                IsSelfCertification = documentation.IsSelfCertification,
                                CreatedOn = DateTime.Now
                            });
                    }
                }
            }
        }
        #endregion

        #region Agent ExemptionCode Disabled
        public async Task<IEnumerable<AgentExemptionCodeViewModel>> GetAgentExemptionCodeDisabledByAgentIdAsync(int agentId)
        {
            var sql = @"  SELECT C.Id as ExemptionCodeId, C.Name,AC.AgentId FROM ExemptionCodes C 
                 left JOIN AgentExemptionCode_Disabled AC ON C.Id = AC.ExemptionCodeId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentExemptionCodeViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }

        public async Task UpsertAgentExemptionCodeDisabledAsync(int agentId, List<int> existingAgentExemptionCodeIds)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentExemptionCodeDisabled = await connection.QueryAsync<int>
                    ("SELECT ExemptionCodeId FROM AgentExemptionCode_Disabled WHERE AgentId = @AgentId",
                   new { AgentId = agentId });

                var ExemptionCodeToDelete = existingAgentExemptionCodeDisabled.Except(existingAgentExemptionCodeIds);
                var ExemptionCodeToAdd = existingAgentExemptionCodeIds.Except(existingAgentExemptionCodeDisabled);

                if (ExemptionCodeToDelete.Any())
                {
                    connection.Execute("delete from AgentExemptionCode_Disabled WHERE AgentId = @AgentId AND ExemptionCodeId IN @existingAgentExemptionCodeIds",
                        new { AgentId = agentId, existingAgentExemptionCodeIds = ExemptionCodeToDelete });
                }

                var AgentExemptionCodeDisabled = ExemptionCodeToAdd.Select
                    (ExemptionCodeId => new AgentExemptionCodeDisabled
                    {
                        AgentId = agentId,
                        ExemptionCodeId = ExemptionCodeId,
                        CreatedOn = DateTime.Now
                    });

                if (AgentExemptionCodeDisabled.Any())
                {
                    connection.Execute("INSERT INTO AgentExemptionCode_Disabled(AgentId, ExemptionCodeId, CreatedOn) " +
                        "VALUES (@AgentId, @ExemptionCodeId, @CreatedOn)",
                        AgentExemptionCodeDisabled);
                }
            }
        }
        #endregion

        #region  Agent IncomeCode Hidden
        public async Task<IEnumerable<AgentIncomeCodeViewModel>> GetAgentIncomeCodeHiddenByAgentIdAsync(int agentId)
        {
            var sql = @"SELECT C.Id as IncomeCodeId, C.Name,AC.AgentId FROM IncomeCodes C 
                 left JOIN AgentIncomeCode_Hidden AC ON C.Id = AC.IncomeCodeId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentIncomeCodeViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }

        public async Task UpsertAgentIncomeCodeHiddenAsync(int agentId, List<int> existingAgentIncomeCodeIds)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentIncomeCodeHidden = await connection.QueryAsync<int>
                    ("SELECT IncomeCodeId FROM AgentIncomeCode_Hidden WHERE AgentId = @AgentId",
                   new { AgentId = agentId });

                var IncomeCodeToDelete = existingAgentIncomeCodeHidden.Except(existingAgentIncomeCodeIds);
                var IncomeCodeToAdd = existingAgentIncomeCodeIds.Except(existingAgentIncomeCodeHidden);

                if (IncomeCodeToDelete.Any())
                {
                    connection.Execute("delete from AgentIncomeCode_Hidden WHERE AgentId = @AgentId AND IncomeCodeId IN @existingAgentIncomeCodeIds",
                        new { AgentId = agentId, existingAgentIncomeCodeIds = IncomeCodeToDelete });
                }

                var AgentIncomeCodeHidden = IncomeCodeToAdd.Select
                    (IncomeCodeId => new AgentIncomeCodeHidden
                    {
                        AgentId = agentId,
                        IncomeCodeId = IncomeCodeId,
                        CreatedOn = DateTime.Now
                    });

                if (AgentIncomeCodeHidden.Any())
                {
                    connection.Execute("INSERT INTO AgentIncomeCode_Hidden(AgentId, IncomeCodeId, CreatedOn) " +
                        "VALUES (@AgentId, @IncomeCodeId, @CreatedOn)",
                        AgentIncomeCodeHidden);
                }
            }
        }

        #endregion

        #region  Agent USVisaType Hidden
        public async Task<IEnumerable<AgentUSVisaTypeViewModel>> GetAgentUSVisaTypeHiddenByAgentIdAsync(int agentId)
        {
            var sql = @"  SELECT C.Id as USVisaTypeId, C.Name,AC.AgentId FROM USVisaTypes C 
                 left JOIN AgentUSVisaType_Hidden AC ON C.Id = AC.USVisaTypeId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentUSVisaTypeViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }

        public async Task UpsertAgentUSVisaTypeHiddenAsync(int agentId, List<int> existingAgentUSVisaTypeIds)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentUSVisaTypeHidden = await connection.QueryAsync<int>
                    ("SELECT USVisaTypeId FROM AgentUSVisaType_Hidden WHERE AgentId = @AgentId",
                   new { AgentId = agentId });

                var USVisaTypeToDelete = existingAgentUSVisaTypeHidden.Except(existingAgentUSVisaTypeIds);
                var USVisaTypeToAdd = existingAgentUSVisaTypeIds.Except(existingAgentUSVisaTypeHidden);

                if (USVisaTypeToDelete.Any())
                {
                    connection.Execute("delete from AgentUSVisaType_Hidden WHERE AgentId = @AgentId AND USVisaTypeId IN @existingAgentUSVisaTypeIds",
                        new { AgentId = agentId, existingAgentUSVisaTypeIds = USVisaTypeToDelete });
                }

                var AgentUSVisaTypeHidden = USVisaTypeToAdd.Select
                    (USVisaTypeId => new AgentUSVisaTypeHidden
                    {
                        AgentId = agentId,
                        USVisaTypeId = USVisaTypeId,
                        CreatedOn = DateTime.Now
                    });

                if (AgentUSVisaTypeHidden.Any())
                {
                    connection.Execute("INSERT INTO AgentUSVisaType_Hidden(AgentId, USVisaTypeId, CreatedOn) " +
                        "VALUES (@AgentId, @USVisaTypeId, @CreatedOn)",
                        AgentUSVisaTypeHidden);
                }
            }
        }
        #endregion

        #region Agent FATCAExemptionCode Hidden
        public async Task<IEnumerable<AgentFATCAExemptionCodeViewModel>> GetAgentFATCAExemptionCodeHiddenByAgentIdAsync(int agentId)
        {
            var sql = @"  SELECT C.Id as FATCAExemptionCodeId, C.Name,AC.AgentId FROM FATCAExemptionCodes C 
                 left JOIN AgentFATCAExemptionCode_Hidden AC ON C.Id = AC.FATCAExemptionCodeId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentFATCAExemptionCodeViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }

        public async Task UpsertAgentFATCAExemptionCodeHiddenAsync(int agentId, List<int> existingAgentFATCAExemptionCodeIds)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentFATCAExemptionCodeHidden = await connection.QueryAsync<int>
                    ("SELECT FATCAExemptionCodeId FROM AgentFATCAExemptionCode_Hidden WHERE AgentId = @AgentId",
                   new { AgentId = agentId });

                var FATCAExemptionCodeToDelete = existingAgentFATCAExemptionCodeHidden.Except(existingAgentFATCAExemptionCodeIds);
                var FATCAExemptionCodeToAdd = existingAgentFATCAExemptionCodeIds.Except(existingAgentFATCAExemptionCodeHidden);

                if (FATCAExemptionCodeToDelete.Any())
                {
                    connection.Execute("delete from AgentFATCAExemptionCode_Hidden WHERE AgentId = @AgentId AND FATCAExemptionCodeId IN @existingAgentFATCAExemptionCodeIds",
                        new { AgentId = agentId, existingAgentFATCAExemptionCodeIds = FATCAExemptionCodeToDelete });
                }

                var AgentFATCAExemptionCodeHidden = FATCAExemptionCodeToAdd.Select
                    (FATCAExemptionCodeId => new AgentFATCAExemptionCodeHidden
                    {
                        AgentId = agentId,
                        FATCAExemptionCodeId = FATCAExemptionCodeId,
                        CreatedOn = DateTime.Now
                    });

                if (AgentFATCAExemptionCodeHidden.Any())
                {
                    connection.Execute("INSERT INTO AgentFATCAExemptionCode_Hidden(AgentId, FATCAExemptionCodeId, CreatedOn) " +
                        "VALUES (@AgentId, @FATCAExemptionCodeId, @CreatedOn)",
                        AgentFATCAExemptionCodeHidden);
                }
            }
        }
        #endregion

        #region Agent PaymentType 
        public async Task<IEnumerable<AgentPaymentTypeViewModel>> GetAgentPaymentTypeByAgentIdAsync(int agentId)
        {
            var sql = @"  SELECT C.Id as PaymentTypeId, C.Name,AC.AgentId,
AC.Hide,AC.MakeDefault FROM PaymentTypes C 
                 left JOIN Agent_PaymentTypes AC ON C.Id = AC.PaymentTypeId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentPaymentTypeViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }
        public async Task UpsertAgentPaymentTypeAsync(int agentId, List<AgentPaymentTypeInsertModel> existingAgentPaymentTypes)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentPaymentType = await connection.QueryAsync<AgentPaymentType>("SELECT AgentId,PaymentTypeId, Hide, MakeDefault FROM Agent_PaymentTypes WHERE AgentId = @AgentId",
                    new { AgentId = agentId });

                var PaymentTypeIdsToDelete = existingAgentPaymentType
          .Where(d => !existingAgentPaymentTypes.Any(e => e.PaymentTypeId == d.PaymentTypeId)
        //  || (!d.Hide && !d.MakeDefault 
        || existingAgentPaymentTypes.Any(e => e.PaymentTypeId == d.PaymentTypeId && !e.Hide && !e.MakeDefault))
          .Select(d => d.PaymentTypeId);

                var PaymentTypeIdsToAdd = existingAgentPaymentTypes
                    .Where(e => !existingAgentPaymentType.Any(d => d.PaymentTypeId == e.PaymentTypeId))
                    .Select(e => e.PaymentTypeId);

                if (PaymentTypeIdsToDelete.Any())
                {
                    connection.Execute("DELETE FROM Agent_PaymentTypes WHERE AgentId = @AgentId AND PaymentTypeId IN @existingAgentPaymentTypesIds",
                        new { AgentId = agentId, existingAgentPaymentTypesIds = PaymentTypeIdsToDelete });
                }

                foreach (var PaymentType in existingAgentPaymentTypes)
                {
                    var existingPaymentType = existingAgentPaymentType.FirstOrDefault
                        (d => d.PaymentTypeId == PaymentType.PaymentTypeId);

                    if (existingPaymentType != null)
                    {
                        //if (!existingPaymentType.Hide || !existingPaymentType.MakeDefault)
                        //{
                            existingPaymentType.Hide = PaymentType.Hide;
                            existingPaymentType.MakeDefault = PaymentType.MakeDefault;

                            connection.Execute("UPDATE Agent_PaymentTypes SET Hide = @Hide, MakeDefault = @MakeDefault,ModifiedOn=@ModifiedOn WHERE AgentId = @AgentId AND PaymentTypeId = @PaymentTypeId",
                                new
                                {
                                    AgentId = agentId,
                                    PaymentTypeId = existingPaymentType.PaymentTypeId,
                                    Hide = existingPaymentType.Hide,
                                    MakeDefault = existingPaymentType.MakeDefault,
                                    ModifiedOn = DateTime.Now
                                });
                        //}
                    }
                    else
                    {
                        connection.Execute("INSERT INTO Agent_PaymentTypes (AgentId, PaymentTypeId, Hide, MakeDefault, CreatedOn) " +
                            "VALUES (@AgentId, @PaymentTypeId, @Hide, @MakeDefault, @CreatedOn)",
                            new
                            {
                                AgentId = agentId,
                                PaymentTypeId = PaymentType.PaymentTypeId,
                                Hide = PaymentType.Hide,
                                MakeDefault = PaymentType.MakeDefault,
                                CreatedOn = DateTime.Now
                            });
                    }
                }
            }
        }
        #endregion

        #region Agent FATCAEntityGIINChallenge Disabled
        public async Task<IEnumerable<AgentFATCAEntityGIINChallengeViewModel>> GetAgentFATCAEntityGIINChallengeDisabledByAgentIdAsync(int agentId)
        {
            var sql = @"  SELECT C.Id as FATCAEntityTypeId, C.Name,AC.AgentId FROM FATCAEntityTypes C 
                 left JOIN AgentFATCAEntityGIINChallenge_Disabled AC ON C.Id = AC.FATCAEntityTypeId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentFATCAEntityGIINChallengeViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }

        public async Task UpsertAgentFATCAEntityGIINChallengeDisabledAsync(int agentId, List<int> existingAgentFATCAEntityTypeIds)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentFATCAEntityGIINChallengeDisabled = await connection.QueryAsync<int>
                    ("SELECT FATCAEntityTypeId FROM AgentFATCAEntityGIINChallenge_Disabled WHERE AgentId = @AgentId",
                   new { AgentId = agentId });

                var ExemptionCodeToDelete = existingAgentFATCAEntityGIINChallengeDisabled.Except(existingAgentFATCAEntityTypeIds);
                var ExemptionCodeToAdd = existingAgentFATCAEntityTypeIds.Except(existingAgentFATCAEntityGIINChallengeDisabled);

                if (ExemptionCodeToDelete.Any())
                {
                    connection.Execute("delete from AgentFATCAEntityGIINChallenge_Disabled WHERE AgentId = @AgentId AND FATCAEntityTypeId IN @existingAgentFATCAEntityTypeIds",
                        new { AgentId = agentId, existingAgentFATCAEntityTypeIds = ExemptionCodeToDelete });
                }

                var AgentFATCAEntityGIINChallengeDisabled = ExemptionCodeToAdd.Select
                    (FATCAEntityTypeId => new AgentFATCAEntityGIINChallengeDisabled
                    {
                        AgentId = agentId,
                        FATCAEntityTypeId = FATCAEntityTypeId,
                        CreatedOn = DateTime.Now
                    });

                if (AgentFATCAEntityGIINChallengeDisabled.Any())
                {
                    connection.Execute("INSERT INTO AgentFATCAEntityGIINChallenge_Disabled(AgentId, FATCAEntityTypeId, CreatedOn) " +
                        "VALUES (@AgentId, @FATCAEntityTypeId, @CreatedOn)",
                        AgentFATCAEntityGIINChallengeDisabled);
                }
            }
        }
        #endregion

        #region Agent SPTQuestion Hidden
        public async Task<IEnumerable<AgentSPTQuestionViewModel>> GetAgentSPTQuestionHiddenByAgentIdAsync(int agentId)
        {
            var sql = @"  SELECT C.Id as SPTQuestionId, C.Name,AC.AgentId,
AC.Alias,AC.Hidden FROM SPTQuestions C 
                 left JOIN AgentSPTQuestion_Hidden AC ON C.Id = AC.SPTQuestionId 
                  WHERE AC.AgentId =@agentId or AC.AgentId is null ";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<AgentSPTQuestionViewModel>(sql, new { agentId = agentId });
                return result;
            }
        }
        public async Task UpsertAgentSPTQuestionHiddenAsync(int agentId, List<AgentSPTQuestionInsertModel> existingAgentSPTQuestions)
        {
            using (var connection = CreateConnection())
            {
                var existingAgentSPTQuestionHidden = await connection.QueryAsync<AgentSPTQuestionHidden>("SELECT AgentId,SPTQuestionId, Alias, Hidden FROM AgentSPTQuestion_Hidden WHERE AgentId = @AgentId",
                    new { AgentId = agentId });

                var SPTQuestionIdsToDelete = existingAgentSPTQuestionHidden
          .Where(d => !existingAgentSPTQuestions.Any(e => e.SPTQuestionId == d.SPTQuestionId)
        //  || (!d.IsUSSubmission && !d.IsSelfCertification 
        || existingAgentSPTQuestions.Any(e => e.SPTQuestionId == d.SPTQuestionId &&
        !e.Hidden && string.IsNullOrEmpty(e.Alias)))
          .Select(d => d.SPTQuestionId);

                var SPTQuestionIdsToAdd = existingAgentSPTQuestions
                    .Where(e => !existingAgentSPTQuestionHidden.Any(d => d.SPTQuestionId == e.SPTQuestionId))
                    .Select(e => e.SPTQuestionId);

                if (SPTQuestionIdsToDelete.Any())
                {
                    connection.Execute("DELETE FROM AgentSPTQuestion_Hidden WHERE AgentId = @AgentId AND SPTQuestionId IN @existingAgentSPTQuestionsIds",
                        new { AgentId = agentId, existingAgentSPTQuestionsIds = SPTQuestionIdsToDelete });
                }

                foreach (var SPTQuestion in existingAgentSPTQuestions)
                {
                    var existingSPTQuestion = existingAgentSPTQuestionHidden.FirstOrDefault
                        (d => d.SPTQuestionId == SPTQuestion.SPTQuestionId);

                    if (existingSPTQuestion != null)
                    {
                        //if (!existingSPTQuestion.IsUSSubmission || !existingSPTQuestion.IsSelfCertification)
                        //{
                            existingSPTQuestion.Alias = SPTQuestion.Alias;
                            existingSPTQuestion.Hidden = SPTQuestion.Hidden;

                            connection.Execute("UPDATE AgentSPTQuestion_Hidden SET Alias = @Alias, Hidden = @Hidden,ModifiedOn=@ModifiedOn WHERE AgentId = @AgentId AND SPTQuestionId = @SPTQuestionId",
                                new
                                {
                                    AgentId = agentId,
                                    SPTQuestionId = existingSPTQuestion.SPTQuestionId,
                                    Alias = existingSPTQuestion.Alias,
                                    Hidden = existingSPTQuestion.Hidden,
                                    ModifiedOn = DateTime.Now
                                });
                        //}
                    }
                    else
                    {
                        connection.Execute("INSERT INTO AgentSPTQuestion_Hidden (AgentId,SPTQuestionId,Alias, Hidden, CreatedOn) " +
                            "VALUES (@AgentId, @SPTQuestionId, @Alias, @Hidden, @CreatedOn)",
                            new
                            {
                                AgentId = agentId,
                                SPTQuestionId = SPTQuestion.SPTQuestionId,
                                Alias = SPTQuestion.Alias,
                                Hidden = SPTQuestion.Hidden,
                                CreatedOn = DateTime.Now
                            });
                    }
                }
            }
        }
        #endregion
    }
}
