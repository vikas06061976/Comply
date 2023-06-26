using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.Capacities;
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
    public class CapacitiesService : ICapacitiesService
    {
        private readonly IConfiguration _configuration;
        public CapacitiesService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<int> InsertCapacities(CapacitiesInsert capacityModel)
        {
            capacityModel.CreatedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Name", capacityModel.Name, DbType.String);
                parameters.Add("@isProxyMandatory", capacityModel.isProxyMandatory, DbType.Boolean);
                parameters.Add("@isCountryOfResidenceRequired", capacityModel.isCountryOfResidenceRequired, DbType.Boolean);
                parameters.Add("@isImportant", capacityModel.isImportant, DbType.Boolean);
                parameters.Add("@isUSIndividual", capacityModel.isUSIndividual, DbType.Boolean);
                parameters.Add("@isNonUSIndividual", capacityModel.isNonUSIndividual, DbType.Boolean);
                parameters.Add("@isUSBusiness", capacityModel.isUSBusiness, DbType.Boolean);
                parameters.Add("@isNonUSBusiness", capacityModel.isNonUSBusiness, DbType.Boolean);
                parameters.Add("@isIntermediary", capacityModel.isIntermediary, DbType.Boolean);
                parameters.Add("@isNonUSGovernment", capacityModel.isNonUSGovernment, DbType.Boolean);
                parameters.Add("@CreatedOn", capacityModel.CreatedOn, DbType.DateTime);
                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertCapacities", parameters, commandType: CommandType.StoredProcedure);
                ValidateMessage(capacityModel);
                return result;
            }
        }
        public async Task<int> UpdateCapacities(CapacitiesUpdate capacityModel)
        {
            capacityModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", capacityModel.Id, DbType.Int32);
                parameters.Add("@Name", capacityModel.Name, DbType.String);
                parameters.Add("@isProxyMandatory", capacityModel.isProxyMandatory, DbType.Boolean);
                parameters.Add("@isCountryOfResidenceRequired", capacityModel.isCountryOfResidenceRequired, DbType.Boolean);
                parameters.Add("@isImportant", capacityModel.isImportant, DbType.Boolean);
                parameters.Add("@isUSIndividual", capacityModel.isUSIndividual, DbType.Boolean);
                parameters.Add("@isNonUSIndividual", capacityModel.isNonUSIndividual, DbType.Boolean);
                parameters.Add("@isUSBusiness", capacityModel.isUSBusiness, DbType.Boolean);
                parameters.Add("@isNonUSBusiness", capacityModel.isNonUSBusiness, DbType.Boolean);
                parameters.Add("@isIntermediary", capacityModel.isIntermediary, DbType.Boolean);
                parameters.Add("@isNonUSGovernment", capacityModel.isNonUSGovernment, DbType.Boolean);
                parameters.Add("@IsActive", capacityModel.IsActive, DbType.Boolean);
                parameters.Add("@ModifiedOn", capacityModel.ModifiedOn, DbType.DateTime);

                var result = await connection.ExecuteAsync("UpdateCapacities", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<PaginationResponse<CapacitiesView>> GetAllAsync(PaginationRequest request, string searchName)
        {
            using (var connection = CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IQueryable<CapacitiesView> capacities = connection.Query<CapacitiesView>
                    ($@"SELECT * FROM Capacities where IsActive=1").AsQueryable();
                // and (name={searchName})

                // Apply search filter
                if (!string.IsNullOrEmpty(searchName))
                {
                    capacities = capacities.Where(f => f.Name.Contains(searchName));
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
                                    capacities = capacities.OrderBy(f => f.Name);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "desc":
                            switch (request.SortColumn.ToLower())
                            {
                                case "name":
                                    capacities = capacities.OrderByDescending(f => f.Name);
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
                var totalRecords = capacities.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalRecords / request.PageSize);
                var records = capacities.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                // Return pagination response object
                return new PaginationResponse<CapacitiesView>
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    Records = records
                };
            }
        }
        public async Task<CapacitiesView> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Capacities WHERE Id = @Id and IsActive=1";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<CapacitiesView>(sql, new { Id = id });
                return result;
            }
        }
        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Capacities WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }
        private static void ValidateMessage(CapacitiesInsert capacityModel)
        {
            if (string.IsNullOrEmpty(capacityModel.Name))
            {
                throw new Exception("Name can't be blank.");
            }
        }
    }
}
