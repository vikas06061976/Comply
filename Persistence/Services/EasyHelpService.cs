using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.EasyHelp;
using ComplyExchangeCMS.Domain.Services;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class EasyHelpService : IEasyHelpService
    {
        private readonly IConfiguration _configuration;
        public EasyHelpService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
            public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<int> InsertEasyHelp(EasyHelpInsert easyHelpModel)
        {
            easyHelpModel.CreatedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Easykey", easyHelpModel.Easykey, DbType.String);
                parameters.Add("@Tooltip", easyHelpModel.Tooltip, DbType.String);
                parameters.Add("@Text", easyHelpModel.Text, DbType.String);
                parameters.Add("@MoreText", easyHelpModel.MoreText, DbType.String);
                parameters.Add("@CreatedOn", easyHelpModel.CreatedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertEasyHelp", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> UpdateEasyHelp(EasyHelpUpdate easyHelpModel)
        {
            easyHelpModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", easyHelpModel.Id, DbType.Int32);
                parameters.Add("@Easykey", easyHelpModel.Easykey, DbType.String);
                parameters.Add("@Tooltip", easyHelpModel.Tooltip, DbType.String);
                parameters.Add("@Text", easyHelpModel.Text, DbType.String);
                parameters.Add("@MoreText", easyHelpModel.MoreText, DbType.String);
                parameters.Add("@IsActive", easyHelpModel.IsActive, DbType.Boolean);
                parameters.Add("@ModifiedOn", easyHelpModel.ModifiedOn, DbType.DateTime);

                var result = await connection.ExecuteAsync("UpdateEasyHelp", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<PaginationResponse<EasyHelpView>> GetAllAsync
            (PaginationRequest request, string searchName)
        {
            using (var connection = CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IQueryable<EasyHelpView> easyHelps = connection.Query<EasyHelpView>
                    ($@"SELECT * FROM EasyHelp where IsActive=1").AsQueryable();
                // and (name={searchName})

                // Apply search filter
                if (!string.IsNullOrEmpty(searchName))
                {
                    easyHelps = easyHelps.Where(f => f.Easykey.Contains(searchName));
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
                                    easyHelps = easyHelps.OrderBy(f => f.Easykey);
                                    break;

                                default:
                                    break;
                            }
                            break;
                        case "desc":
                            switch (request.SortColumn.ToLower())
                            {
                                case "code":
                                    easyHelps = easyHelps.OrderBy(f => f.Easykey);
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
                var totalRecords = easyHelps.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalRecords / request.PageSize);
                var records = easyHelps.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                // Return pagination response object
                return new PaginationResponse<EasyHelpView>
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    Records = records
                };
            }
        }
        public async Task<EasyHelpView> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM EasyHelp WHERE Id = @Id and IsActive=1";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<EasyHelpView>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> DeleteEasyHelp(int id)
        {
            var sql = "DELETE FROM EasyHelp WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
    }
}
