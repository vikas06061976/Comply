using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.ContentBlock;
using ComplyExchangeCMS.Domain.Models.EasyHelp;
using ComplyExchangeCMS.Domain.Models.Master;
using ComplyExchangeCMS.Domain.Models.Pages;
using ComplyExchangeCMS.Domain.Services;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class EasyHelpService : IEasyHelpService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        public EasyHelpService(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
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
        public async Task<int> InsertEasyHelpTranslation(EasyHelpTranslation easyHelpModel)
        {
            easyHelpModel.CreatedOn = DateTime.UtcNow;
            easyHelpModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@ToolTip", easyHelpModel.ToolTip, DbType.String);
                parameters.Add("@EasyHelpId", easyHelpModel.EasyHelpId, DbType.Int32);
                parameters.Add("@LanguageId", easyHelpModel.LanguageId, DbType.Int32);
                parameters.Add("@Text", easyHelpModel.Text, DbType.String);
                parameters.Add("@MoreText", easyHelpModel.MoreText, DbType.String);
                parameters.Add("@BulkTranslation", easyHelpModel.BulkTranslation, DbType.Boolean);
                parameters.Add("@CreatedOn", easyHelpModel.CreatedOn, DbType.DateTime);
                parameters.Add("@ModifiedOn", easyHelpModel.ModifiedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertEasyHelpTranslation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<EasyHelpTranslationView> GetEasyHelpTranslation(int easyHelpId, int languageId)
        {
            var sql = "select * from EasyHelpTranslations where EasyHelpId= @EasyHelpId and LanguageId = @LanguageId";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<EasyHelpTranslationView>(sql, new { EasyHelpId = easyHelpId, LanguageId = languageId });
                return result;
            }
        }
        public async Task<IReadOnlyList<ModuleLanguageView>> GetAllLanguage(int easyHelpId)
        {
            var sql = "select l.Id,l.Name,et.EasyHelpId as ModuleId from Languages as l left join EasyHelpTranslations as et on l.Id=et.LanguageId AND et.EasyHelpId = @easyHelpId";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<ModuleLanguageView>(sql, new { easyHelpId = easyHelpId });
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
            InsertEasyHelp(filePath);
        }
        public void InsertEasyHelp(string filePath)
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
                List<EasyHelpInsert> data = new List<EasyHelpInsert>();

                for (int row = startRow; row <= endRow; row++)
                {
                    EasyHelpInsert rowData = new EasyHelpInsert
                    {
                        // Map the columns from the Excel file to your class properties
                        Easykey = worksheet.Cells[row, 1].Value?.ToString(),
                        Tooltip= worksheet.Cells[row, 2].Value?.ToString(),
                        Text = worksheet.Cells[row, 3].Value?.ToString(),
                        MoreText = worksheet.Cells[row, 4].Value?.ToString(),
                        // ... map other properties
                    };

                    data.Add(rowData);
                }

                using (var connection = CreateConnection())
                {
                    foreach (EasyHelpInsert rowData in data)
                    {
                        string sql = "INSERT INTO [dbo].[EasyHelp] ([Easykey] ,[Tooltip] ,[Text] ,[MoreText] ,[CreatedOn] ,[IsActive] ,[ModifiedOn]) VALUES (@Easykey ,@Tooltip ,@Text ,@MoreText ,GETUTCDATE() ,1 ,NULL)";
                        connection.Execute(sql, rowData);
                    }
                }
            }
        }

        public byte[] GenerateExcelFile()
        {
            // SQL query to retrieve data
            string query = "SELECT Easykey,Tooltip,Text,MoreText FROM EasyHelp";

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
