using ComplyExchangeCMS.Domain.Models.Pages;
using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Services;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using ComplyExchangeCMS.Domain.Models.FormInstructions;
using ComplyExchangeCMS.Domain.Models.Documentation;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class FormInstructionsService : IFormInstructionsService
    {
        private readonly IConfiguration _configuration;
        public FormInstructionsService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> InsertFormInstruction(FormInstructionsInsert formInstructionsModel)
        {
            formInstructionsModel.CreatedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Description", formInstructionsModel.Description, DbType.String);
                parameters.Add("@URL", formInstructionsModel.URL, DbType.String);
                parameters.Add("@CreatedOn", formInstructionsModel.CreatedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertFormsInstruction", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> UpdateFormInstruction(FormInstructionsUpdate formInstructionsModel)
        {
            formInstructionsModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", formInstructionsModel.Id, DbType.Int32);
                parameters.Add("@Description", formInstructionsModel.Description, DbType.String);
                parameters.Add("@URL", formInstructionsModel.URL, DbType.String);
                parameters.Add("@IsActive", formInstructionsModel.IsActive, DbType.Boolean);
                parameters.Add("@ModifiedOn", formInstructionsModel.ModifiedOn, DbType.DateTime);

                var result = await connection.ExecuteAsync("UpdateFormsInstruction", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

       public async Task<PaginationResponse<FormInstructionsView>> GetAllAsync(PaginationRequest request, string searchName)
        {
            using (var connection = CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IQueryable<FormInstructionsView> formIns = connection.Query<FormInstructionsView>
                    ($@"SELECT * FROM FormInstructions where IsActive=1").AsQueryable();
                // and (name={searchName})

                // Apply search filter
                if (!string.IsNullOrEmpty(searchName))
                {
                    formIns = formIns.Where(f => f.Description.Contains(searchName));
                }

                // Sorting
                if (!string.IsNullOrEmpty(request.SortColumn))
                {
                    switch (request.SortDirection?.ToLower())
                    {
                        case "asc":
                            switch (request.SortColumn.ToLower())
                            {
                                case "description":
                                    formIns = formIns.OrderBy(f => f.Description);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "desc":
                            switch (request.SortColumn.ToLower())
                            {
                                case "description":
                                    formIns = formIns.OrderByDescending(f => f.Description);
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
                var totalRecords = formIns.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalRecords / request.PageSize);
                var records = formIns.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                // Return pagination response object
                return new PaginationResponse<FormInstructionsView>
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    Records = records
                };
            }
        }
        public async Task<FormInstructionsView> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM FormInstructions WHERE Id = @Id and IsActive=1";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<FormInstructionsView>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM FormInstructions WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
    }
}
