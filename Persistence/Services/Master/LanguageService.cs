using ComplyExchangeCMS.Domain.Entities.Masters;
using ComplyExchangeCMS.Domain.Services.Master;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ComplyExchangeCMS.Domain.Models.FormTypes;
using ComplyExchangeCMS.Domain.Models.Master;
using ComplyExchangeCMS.Domain.Models.Pages;
using ComplyExchangeCMS.Domain.Models.Agent;
using ComplyExchangeCMS.Domain;

namespace ComplyExchangeCMS.Persistence.Services.Master
{
    public class LanguageService : ILanguageService
    {
        private readonly IConfiguration _configuration;
        public LanguageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<PaginationResponse<LanguageView>> GetAllAsync(PaginationRequest request, string searchName)
        {
            using (var connection = CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IQueryable<LanguageView> lang = connection.Query<LanguageView>
                    ($@"select * from Languages where IsActive=1 and IsDeleted=0").AsQueryable();
                // and (name={searchName})

                // Apply search filter
                if (!string.IsNullOrEmpty(searchName))
                {
                    lang = lang.Where(f => f.Name.Contains(searchName));
                }
                // Sorting
                if (!string.IsNullOrEmpty(request.SortColumn))
                {
                    switch (request.SortDirection?.ToLower())
                    {
                        case "asc":
                            switch (request.SortColumn.ToLower())
                            {
                                case "name":
                                    lang = lang.OrderBy(f => f.Name);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "desc":
                            switch (request.SortColumn.ToLower())
                            {
                                case "name":
                                    lang = lang.OrderByDescending(f => f.Name);
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
                var totalRecords = lang.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalRecords / request.PageSize);
                var records = lang.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                // Return pagination response object
                return new PaginationResponse<LanguageView>
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    Records = records
                };
            }
        }

        public async Task<IReadOnlyList<LanguageView>> GetAllLanguage()
        {
            var sql = "select * from Languages where IsActive=1 and IsDeleted=0";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<LanguageView>(sql);
                return result.ToList();
            }
        }
        public async Task<int> InsertLanguage(LanguageInsert languageModel)
        {
            languageModel.CreatedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Name", languageModel.Name, DbType.String);
                parameters.Add("@IsoCode", languageModel.IsoCode, DbType.String);
                parameters.Add("@CreatedOn", languageModel.CreatedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertLanguage", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<int> UpdateLanguage(LanguageUpdate languageModel)
        {
            languageModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Id", languageModel.Id, DbType.Int32);
                parameters.Add("@Name", languageModel.Name, DbType.String);
                parameters.Add("@IsoCode", languageModel.IsoCode, DbType.String);
                parameters.Add("@IsActive", languageModel.IsActive, DbType.Boolean);
                parameters.Add("@IsDeleted", languageModel.IsDeleted, DbType.Boolean);
                parameters.Add("@ModifiedOn", languageModel.ModifiedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("UpdateLanguage", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<int> DeleteLanguage(int Id)
        {
            var sql = "DELETE FROM Languages WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = Id });
                return result;
            }
        }
        public async Task<LanguageView> GetByIdAsync(int Id)
        {
            var sql = "SELECT * FROM Languages WHERE Id = @Id and IsActive=1 and IsDeleted=0";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<LanguageView>(sql, new { Id = Id });
                return result;
            }
        }
    }
}
