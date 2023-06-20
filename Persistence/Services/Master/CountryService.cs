using ComplyExchangeCMS.Domain.Entities;
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
using ComplyExchangeCMS.Domain.Entities.Masters;

namespace ComplyExchangeCMS.Persistence.Services.Master
{
    public class CountryService : ICountryService
    {
        private readonly IConfiguration _configuration;
        public CountryService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<IReadOnlyList<Country>> GetAllAsync()
        {
            var sql = "select * from Countries where IsActive=1 and IsDeleted=0";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<Country>(sql);
                return result.ToList();
            }
        }
    }
}
