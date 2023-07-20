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
using ComplyExchangeCMS.Domain.Models.EasyHelp;
using ComplyExchangeCMS.Domain.Models.Master;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class RuleService : IRuleService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        public RuleService(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
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
                parameters.Add("@Class", rulesModel.RuleClass, DbType.String);
                parameters.Add("@Warning", rulesModel.Warning, DbType.String);
                parameters.Add("@isNotAllowedSubmissionToContinue", rulesModel.isNotAllowedSubmissionToContinue, DbType.Boolean);
                parameters.Add("@DisableRule", rulesModel.DisableRule, DbType.Boolean);
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
                parameters.Add("@Class", rulesModel.RuleClass, DbType.String);
                parameters.Add("@Warning", rulesModel.Warning, DbType.String);
                parameters.Add("@isNotAllowedSubmissionToContinue", rulesModel.isNotAllowedSubmissionToContinue, DbType.Boolean);
                parameters.Add("@DisableRule", rulesModel.DisableRule, DbType.Boolean);
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
        public async Task<int> InsertRulesTranslation(RuleTranslationInsert ruleModel)
        {
            ruleModel.CreatedOn = DateTime.UtcNow;
            ruleModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Warning", ruleModel.Warning, DbType.String);
                parameters.Add("@RulesId", ruleModel.RulesId, DbType.Int32);
                parameters.Add("@LanguageId", ruleModel.LanguageId, DbType.Int32);
                parameters.Add("@BulkTranslation", ruleModel.BulkTranslation, DbType.Boolean);
                parameters.Add("@CreatedOn", ruleModel.CreatedOn, DbType.DateTime);
                parameters.Add("@ModifiedOn", ruleModel.ModifiedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertRulesTranslation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<RuleTranslationView> GetRuleTranslation(int ruleId, int languageId)
        {
            var sql = "select * from RulesTranslations where RulesId= @ruleId and LanguageId = @LanguageId";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<RuleTranslationView>(sql, new { ruleId = ruleId, languageId = languageId });
                return result;
            }
        }

        public async Task<IReadOnlyList<ModuleLanguageView>> GetAllLanguage(int ruleId)
        {
            var sql = "select l.Id,l.Name,rt.RulesId as ModuleId from Languages as l left join RulesTranslations as rt on l.Id=rt.LanguageId AND rt.RulesId = @RulesId";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<ModuleLanguageView>(sql, new { RulesId = ruleId });
                return result.ToList();
            }
        }
        public void UploadFile(IFormFile files)
        {
            var target = Path.Combine(_hostingEnvironment.ContentRootPath, "UploadedFiles");

            Directory.CreateDirectory(target);

            if (files.Length <= 0) return;
            var filePath = Path.Combine(target, files.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                files.CopyToAsync(stream);
            }
            InsertRules(filePath);
        }
        public void InsertRules(string filePath)
        {
            // Assuming you have the path to the Excel file

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // Assuming data is in the first worksheet

                // Assuming the data starts from the second row (excluding header)
                int startRow = 2;
                int endRow = worksheet.Dimension.End.Row;

                // Assuming you have a class representing the data structure
                List<RulesInsert> data = new List<RulesInsert>();

                for (int row = startRow; row <= endRow; row++)
                {
                    RulesInsert rowData = new RulesInsert
                    {
                        // Map the columns from the Excel file to your class properties
                        Code = worksheet.Cells[row, 1].Value?.ToString(),
                        RuleClass = worksheet.Cells[row, 2].Value?.ToString(),
                        Warning = worksheet.Cells[row, 3].Value?.ToString(),
                        CreatedOn = DateTime.UtcNow
                    };

                    bool.TryParse(worksheet.Cells[row, 4].Value?.ToString(), out bool isNotAllowedSubmission);
                    rowData.isNotAllowedSubmissionToContinue = isNotAllowedSubmission;

                    bool.TryParse(worksheet.Cells[row, 5].Value?.ToString(), out bool disableRule);
                    rowData.DisableRule = disableRule;

                    data.Add(rowData);
                }


                using (var connection = CreateConnection())
                {
                    foreach (RulesInsert rowData in data)
                    {
                        string sql = "INSERT INTO [dbo].[Rules] ([Code] ,[class] ,[Warning] ,[isNotAllowedSubmissionToContinue] ,[DisableRule] ,[CreatedOn] ,[ModifiedOn] ,[IsActive]) VALUES (@Code ,@RuleClass ,@Warning ,@isNotAllowedSubmissionToContinue ,@DisableRule ,@CreatedOn ,NULL ,1)";
                        connection.Execute(sql, rowData);
                    }
                }
            }
        }
        public byte[] GenerateExcelFile()
        {
            // SQL query to retrieve data
            string query = "SELECT [Code] ,[class] ,[Warning] ,[isNotAllowedSubmissionToContinue] ,[DisableRule] FROM [Rules]";

            using (var connection = CreateConnection())
            {
                // Execute the query and retrieve data using Dapper
                var data = connection.Query(query);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                // Create a new Excel package
                ExcelPackage package = new ExcelPackage();

                // Add a new worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Write data to the worksheet
                worksheet.Cells.LoadFromCollection(data, true);

                // Save the Excel package to a memory stream
                using (MemoryStream stream = new MemoryStream())
                {
                    package.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
