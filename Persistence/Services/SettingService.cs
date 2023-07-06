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
        public async Task<int> InsertSetting(SettingInsertModel settingModel)
        {
            settingModel.CreatedOn = DateTime.UtcNow;
            settingModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();

                parameters.Add("@Id", settingModel.Id, DbType.Int32);
                parameters.Add("@DefaultCoverPagePdf_FileName", "", DbType.String);
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
                parameters.Add("@CreatedOn", settingModel.CreatedOn, DbType.DateTime);
                parameters.Add("@ModifiedOn", settingModel.ModifiedOn, DbType.DateTime);

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
        public async Task<int> InsertSettingTranslation(SettingInsertTranslation settingModel)
        {
            settingModel.CreatedOn = DateTime.UtcNow;
            settingModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Content", settingModel.Content, DbType.String);
                parameters.Add("@SettingId", settingModel.SettingId, DbType.Int32);
                parameters.Add("@LanguageId", settingModel.LanguageId, DbType.Int32);
                parameters.Add("@BulkTranslation", settingModel.BulkTranslation, DbType.Boolean);
                parameters.Add("@CreatedOn", settingModel.CreatedOn, DbType.DateTime);
                parameters.Add("@ModifiedOn", settingModel.ModifiedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertTranslationDocumentation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<SettingViewTranslation> GetSettingTranslation(int settingId, int languageId)
        {
            var sql = "select * from DocumentationTranslations where DocId=@docId and LanguageId=@languageId";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<SettingViewTranslation>(sql, new { docId = settingId, languageId = languageId });
                return result;
            }
        }
    }
}
