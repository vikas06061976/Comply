using ComplyExchangeCMS.Domain.Models.Documentation;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComplyExchangeCMS.Domain.Models.LOB;
using Dapper;
using ComplyExchangeCMS.Domain.Services;
using ComplyExchangeCMS.Domain.Models.Pages;
using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.Master;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class LOBService : ILOBService
    {
        private readonly IConfiguration _configuration;
        public LOBService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<IReadOnlyList<Chapter3Status>> GetLOB()
        {
            var sql = "select * from Chapter3Status where IsActive=1";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<Chapter3Status>(sql);
                return result.ToList();
            }
        }

        public async Task<IReadOnlyList<LOBView>> GetAllAsync()
        {
            var sql = "SELECT * FROM LOBApplicability where IsActive=1";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<LOBView>(sql);
                return result.ToList();
            }
        }

        public async Task<int> InsertLOB(LOBInsert LOBModel)
        {
            LOBModel.CreatedOn = DateTime.UtcNow;
            LOBModel.ModifiedOn = DateTime.UtcNow;

            using (var connection = CreateConnection())
            {
                connection.Open();
                var parameters = new DynamicParameters();
                parameters.Add("@Chapter3StatusId", LOBModel.Chapter3StatusId, DbType.Int32);
                parameters.Add("@IsCorporation", LOBModel.IsCorporation, DbType.Boolean);
                parameters.Add("@IsDisregardedEntity", LOBModel.IsDisregardedEntity, DbType.Boolean);
                parameters.Add("@IsPartnership", LOBModel.IsPartnership, DbType.Boolean);
                parameters.Add("@IsSimpleTrust", LOBModel.IsSimpleTrust, DbType.Boolean);
                parameters.Add("@IsGrantorTrust", LOBModel.IsGrantorTrust, DbType.Boolean);
                parameters.Add("@IsComplexTrust", LOBModel.IsComplexTrust, DbType.Boolean);
                parameters.Add("@IsEstate", LOBModel.IsEstate, DbType.Boolean);
                parameters.Add("@IsGovernment", LOBModel.IsGovernment, DbType.Boolean);
                parameters.Add("@IsCentralBankofIssue", LOBModel.IsCentralBankofIssue, DbType.Boolean);
                parameters.Add("@IsTaxExemptOrganization", LOBModel.IsTaxExemptOrganization, DbType.Boolean);
                parameters.Add("@IsPrivateFoundation", LOBModel.IsPrivateFoundation, DbType.Boolean);
                parameters.Add("@IsInternationalOrganization", LOBModel.IsInternationalOrganization, DbType.Boolean);
                parameters.Add("@CreatedOn", LOBModel.CreatedOn, DbType.DateTime);
                parameters.Add("@ModifiedOn", LOBModel.ModifiedOn, DbType.DateTime);
                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertLOBApplicability", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
