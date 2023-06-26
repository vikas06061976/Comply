using ComplyExchangeCMS.Domain.Models.Pages;
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
using ComplyExchangeCMS.Domain.Models.Rules;
using ComplyExchangeCMS.Domain.Services;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class RuleService : IRuleService
    {
        private readonly IConfiguration _configuration;
        public RuleService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
            public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<int> InsertRules(RulesInsert rulesModel)
        {
            rulesModel.CreatedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Code", rulesModel.Code, DbType.String);
                parameters.Add("@RuleClass", rulesModel.RuleClass, DbType.String);
                parameters.Add("@Warning", rulesModel.Warning, DbType.String);
                parameters.Add("@isNotAllowedSubmissionToContinue", rulesModel.isNotAllowedSubmissionToContinue, DbType.Boolean);
                parameters.Add("@CreatedOn", rulesModel.CreatedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertRules", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> UpdateRules(RulesUpdate rulesModel)
        {
            rulesModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", rulesModel.Id, DbType.Int32);
                parameters.Add("@Code", rulesModel.Code, DbType.String);
                parameters.Add("@RuleClass", rulesModel.RuleClass, DbType.String);
                parameters.Add("@Warning", rulesModel.Warning, DbType.String);
                parameters.Add("@isNotAllowedSubmissionToContinue", rulesModel.isNotAllowedSubmissionToContinue, DbType.Boolean);
                parameters.Add("@IsActive", rulesModel.IsActive, DbType.Boolean);
                parameters.Add("@ModifiedOn", rulesModel.ModifiedOn, DbType.DateTime);

                var result = await connection.ExecuteAsync("UpdateRules", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<PaginationResponse<RulesView>> GetAllAsync
            (PaginationRequest request, string searchName)
        {
            using (var connection = CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IQueryable<RulesView> rules = connection.Query<RulesView>
                    ($@"SELECT * FROM Rules where IsActive=1").AsQueryable();
                // and (name={searchName})

                // Apply search filter
                if (!string.IsNullOrEmpty(searchName))
                {
                    rules = rules.Where(f => f.Code.Contains(searchName));
                }

                // Sorting
                if (!string.IsNullOrEmpty(request.SortColumn))
                {
                    switch (request.SortDirection?.ToLower())
                    {
                        case "asc":
                            switch (request.SortColumn.ToLower())
                            {
                                case "code":
                                    rules = rules.OrderBy(f => f.Code);
                                    break;
                                
                                default:
                                    break;
                            }
                            break;
                        case "desc":
                            switch (request.SortColumn.ToLower())
                            {
                                case "code":
                                    rules = rules.OrderBy(f => f.Code);
                                    break;                               
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }

                // Paging
                var totalRecords = rules.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalRecords / request.PageSize);
                var records = rules.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                // Return pagination response object
                return new PaginationResponse<RulesView>
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    Records = records
                };
            }
        }
        public async Task<RulesView> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Rules WHERE Id = @Id and IsActive=1";
            using (var connection = CreateConnection())                                                                                                                                                                                                                                                                                                                                                     
            {
                var result = await connection.QuerySingleOrDefaultAsync<RulesView>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> DeleteRules(int id)
        {
            var sql = "DELETE FROM Rules WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
    }
}
