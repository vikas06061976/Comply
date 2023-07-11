using ComplyExchangeCMS.Domain.Models.Documentation;
using ComplyExchangeCMS.Domain;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplyExchangeCMS.Domain.Services;
using ComplyExchangeCMS.Domain.Models.Settings;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class SettingService : ISettingService
    {
        private readonly IConfiguration _configuration;
        public SettingService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<int> UpsertSetting(SettingInsertModel settingModel)
        {
            settingModel.CreatedOn = DateTime.UtcNow;
            settingModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();

                parameters.Add("@Id", settingModel.Id, DbType.Int32);
                parameters.Add("@DefaultCoverPagePdf_FileName", settingModel.DefaultCoverPagePdf_FileName, DbType.String);
                parameters.Add("@LengthOfConfirmationCode", settingModel.LengthOfConfirmationCode, DbType.Int32);
                parameters.Add("@DefaultLogoType", settingModel.DefaultLogoType, DbType.String);
                parameters.Add("@DefaultLogo_FileName", settingModel.DefaultLogo_FileName, DbType.String);
                parameters.Add("@GoogleTranslateAPIKey", settingModel.GoogleTranslateAPIKey, DbType.String);
                parameters.Add("@PurgeRedundantSubmissionData", settingModel.PurgeRedundantSubmissionData, DbType.String);
                parameters.Add("@RunExchangeInIframe", settingModel.RunExchangeInIframe, DbType.Boolean);
                parameters.Add("@DefaultRetroactiveStatement", settingModel.DefaultRetroactiveStatement, DbType.String);
                parameters.Add("@UnderMaintenance", settingModel.UnderMaintenance, DbType.Boolean);
                parameters.Add("@ReSendTokenEmailFeature", settingModel.ReSendTokenEmailFeature, DbType.Boolean);
                parameters.Add("@ActivateNonEmailPINprocess", settingModel.ActivateNonEmailPINprocess, DbType.Boolean);
                parameters.Add("@BlockForeignCharacterInput", settingModel.BlockForeignCharacterInput, DbType.Boolean);
                parameters.Add("@CreatedOn", settingModel.CreatedOn, DbType.DateTime);
                parameters.Add("@ModifedOn", settingModel.ModifiedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertSetting", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

       /* public async Task<int> UpdateSetting(SettingUpdateModel settingModel)
        {
            settingModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Id", settingModel.Id, DbType.Int32);
                parameters.Add("@DefaultCoverPagePdf_FileName", settingModel.DefaultCoverPagePdf_FileName, DbType.String);
                parameters.Add("@LengthOfConfirmationCode", settingModel.LengthOfConfirmationCode, DbType.Int32);
                parameters.Add("@DefaultLogoType", settingModel.DefaultLogoType, DbType.String);
                parameters.Add("@GoogleTranslateAPIKey", settingModel.GoogleTranslateAPIKey, DbType.String);
                parameters.Add("@PurgeRedundantSubmissionData", settingModel.PurgeRedundantSubmissionData, DbType.String);
                parameters.Add("@RunExchangeInIframe", settingModel.RunExchangeInIframe, DbType.Boolean);
                parameters.Add("@DefaultRetroactiveStatement", settingModel.DefaultRetroactiveStatement, DbType.String);
                parameters.Add("@UnderMaintenance", settingModel.UnderMaintenance, DbType.Boolean);
                parameters.Add("@ReSendTokenEmailFeature", settingModel.ReSendTokenEmailFeature, DbType.Boolean);
                parameters.Add("@ActivateNonEmailPINprocess", settingModel.ActivateNonEmailPINprocess, DbType.Boolean);
                parameters.Add("@BlockForeignCharacterInput", settingModel.BlockForeignCharacterInput, DbType.Boolean);
                parameters.Add("@ModifiedOn", settingModel.ModifiedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("UpdateDocumentation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<int> DeleteDocument(int Id)
        {
            var sql = "DELETE FROM Documentations WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = Id });
                return result;
            }
        }*/
        public async Task<SettingViewModel> GetSetting()
        {
            var sql = "SELECT * FROM Settings";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<SettingViewModel>(sql);
                return result;
            }
        }

        public async Task<IReadOnlyCollection<QuestionView>> GetQuestions()
        {
            var sql = "SELECT [Id], [Question], [QuestionHint] FROM Questions";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<QuestionView>(sql);
                return result.ToList();
            }
        }
        public async Task<int> InsertQuestionTranslation(QuestionTranslationInsert settingModel)
        {
            settingModel.CreatedOn = DateTime.UtcNow;
            settingModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();

                parameters.Add("@Content", settingModel.Content, DbType.String);
                parameters.Add("@QuestionId", settingModel.QuestionId, DbType.Int32);
                parameters.Add("@QuestionHintId", settingModel.QuestionHintId, DbType.Int32);
                parameters.Add("@LanguageId", settingModel.LanguageId, DbType.Int32);
                parameters.Add("@BulkTranslation", settingModel.BulkTranslation, DbType.Boolean);
                parameters.Add("@CreatedOn", settingModel.CreatedOn, DbType.DateTime);
                parameters.Add("@ModifiedOn", settingModel.ModifiedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertTranslationQuestion", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<QuestionTranslationView> GetQuestionTranslation(int? questionId, int? questionHintId, int languageId)
        {
            var sql = "SELECT * FROM QuestionsTranslations where (QuestionId= @QuestionId or QuestionHintId = @QuestionHintId) and LanguageId=@languageId";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<QuestionTranslationView>(sql, new { QuestionId = questionId, QuestionHintId= questionHintId, languageId = languageId });
                return result;
            }
        }
    }
}
