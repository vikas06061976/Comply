using ComplyExchangeCMS.Common;
using ComplyExchangeCMS.Domain.Models.ContentBlock;
using ComplyExchangeCMS.Domain.Models.FormTypes;
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
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class ContentManagementService : IContentManagementService
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ContentManagementService(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        #region Content Block
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
            InsertContent(filePath);
        }
        public void InsertContent(string filePath)
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
                List<ContentManagementInsert> data = new List<ContentManagementInsert>();

                for (int row = startRow; row <= endRow; row++)
                {
                    ContentManagementInsert rowData = new ContentManagementInsert
                    {
                        // Map the columns from the Excel file to your class properties
                        Name = worksheet.Cells[row, 1].Value?.ToString(),
                        Text = worksheet.Cells[row, 2].Value?.ToString(),
                        // ... map other properties
                    };

                    data.Add(rowData);
                }

                using (var connection = CreateConnection())
                {
                    foreach (ContentManagementInsert rowData in data)
                    {
                        string sql = "INSERT INTO [dbo].[ContentManagement] ([Name] ,[Text] ,[CreatedOn] ,[IsActive] ,[IsDeleted] ,[ModifiedOn],[TypeId]) VALUES (@Name, @Text ,GETUTCDATE() ,1 ,0 ,NULL,1)";
                        connection.Execute(sql, rowData);
                    }
                }
            }

            #region XML file upload
            // Read XML file and extract data

            //string xmlFilePath = "N:/Neelam Singh/Vinove/HyperWallet/CEAPI/ContentBlock.xml";
            //string xmlFilePath = "C:/ComplyDoc/ContentBlock.xml";

            //string xmlFilePath1 = formFile.FileName;

            //byte[] fileContent;
            //using (var memoryStream = new MemoryStream())
            //{
            //    formFile.CopyToAsync(memoryStream);
            //    memoryStream.Position=0;
            //    fileContent = memoryStream.ToArray();

            //    var serializer = new XmlSerializer(typeof(List<ContentBlockInsert>));
            //    var xmlData = (List<ContentBlockInsert>)serializer.Deserialize(memoryStream);
            //}
            //List<ContentBlockInsert> contents = ReadContentBlockFromXml(xmlFilePath);

            //using (var connection = CreateConnection())
            //{
            //    foreach (ContentBlockInsert contentBlock in contents)
            //    {
            //        string sql = "INSERT INTO [dbo].[ContentBlock] ([Name] ,[Content] ,[CreatedOn] ,[IsActive] ,[IsDeleted] ,[ModifiedOn]) VALUES (@Name, @Content ,GETUTCDATE() ,1 ,0 ,NULL)";
            //        connection.Execute(sql, contentBlock);
            //    }
            //}

            #endregion
        }
        public byte[] GenerateExcelFile()
        {
            // SQL query to retrieve data
            string query = "SELECT Name,Text FROM ContentManagement where Type=1";

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
        public (string fileType, byte[] archiveData, string archiveName) DownloadFiles()
        {
            var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";
            var files = Directory.GetFiles(Path.Combine(_hostingEnvironment.ContentRootPath, "UploadedFiles")).ToList();

            using (var memoryStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    files.ForEach(file =>
                    {
                        var theFile = archive.CreateEntry(file);
                        using (var streamWriter = new StreamWriter(theFile.Open()))
                        {
                            streamWriter.Write(File.ReadAllText(file));
                        }

                    });
                }
                return ("application/zip", memoryStream.ToArray(), zipName);
            }
        }
        public async Task<int> UpdateContent(ContentManagementUpdate contentBlock)
        {
            contentBlock.ModifiedOn = DateTime.UtcNow;
            var sql = "UPDATE ContentManagement SET Name = @Name ,Text = @Text ,IsActive = @IsActive ,IsDeleted = @IsDeleted ,ModifiedOn=@ModifiedOn WHERE Id = @Id and TypeId=1";
            using (var connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, contentBlock);
                return result;
            }
        }
        public async Task<IReadOnlyList<ContentManagementView>> GetAllContent()
        {
            var sql = "SELECT Id,Name,Text,CreatedOn,IsActive,IsDeleted,ModifiedOn FROM ContentManagement where TypeId=1";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<ContentManagementView>(sql);
                return result.ToList();
            }
        }
        public async Task<ContentManagementView> GetContentById(int id)
        {
            var sql = "SELECT Id,Name,Text,CreatedOn,IsActive,IsDeleted,ModifiedOn FROM ContentManagement where TypeId=1 and Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<ContentManagementView>(sql, new { Id = id });
                return result;
            }
        }
        #endregion

        #region Easy Help and Phrases
        public async Task<int> InsertContentManagement(ContentManagementInsert contentMgntModel)
        {
            contentMgntModel.CreatedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();
                ValidateContentManagementResult(contentMgntModel);
                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Name", contentMgntModel.Name, DbType.String);
                parameters.Add("@ToolTip", contentMgntModel.ToolTip, DbType.String);
                parameters.Add("@Text", contentMgntModel.Text, DbType.String);
                parameters.Add("@MoreText", contentMgntModel.MoreText, DbType.String);
                parameters.Add("@Translation", contentMgntModel.Translation, DbType.String);
                parameters.Add("@Language", contentMgntModel.Language, DbType.String);
                parameters.Add("@CreatedOn", contentMgntModel.CreatedOn, DbType.DateTime);
                parameters.Add("@TypeId", contentMgntModel.TypeId, DbType.Int32);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertContentManagement", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<int> UpdateContentManagement(ContentManagementUpdate contentMgntModel)
        {
            contentMgntModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();
                ValidateContentManagementResult(contentMgntModel);
                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Id", contentMgntModel.Id, DbType.Int32);
                parameters.Add("@Name", contentMgntModel.Name, DbType.String);
                parameters.Add("@ToolTip", contentMgntModel.ToolTip, DbType.String);
                parameters.Add("@Text", contentMgntModel.Text, DbType.String);
                parameters.Add("@MoreText", contentMgntModel.MoreText, DbType.String);
                parameters.Add("@Translation", contentMgntModel.Translation, DbType.String);
                parameters.Add("@Language", contentMgntModel.Language, DbType.String);
                parameters.Add("@IsActive", contentMgntModel.IsActive, DbType.Boolean);
                parameters.Add("@IsDeleted", contentMgntModel.IsDeleted, DbType.Boolean);
                parameters.Add("@ModifiedOn", contentMgntModel.ModifiedOn, DbType.DateTime);
                parameters.Add("@TypeId", contentMgntModel.TypeId, DbType.Int32);

                var result = await connection.QueryFirstOrDefaultAsync<int>("UpdateContentManagement", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<IReadOnlyList<ContentManagementView>> GetAllContentManagement(int TypeId)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TypeId", TypeId, DbType.Int32);
                var result = await connection.QueryAsync<ContentManagementView>("GetAllContentManagement", parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }
        public async Task<ContentManagementView> GetContentManagementById(int TypeId,int Id)
        {
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@TypeId", TypeId, DbType.Int32);
                parameters.Add("@Id", Id, DbType.Int32);
                var result = await connection.QuerySingleOrDefaultAsync<ContentManagementView>("GetAllContentManagementById", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        #endregion

        #region Phrases
        public async Task<IReadOnlyList<ContentManagementView>> GetAllPhrases()
        {
            var sql = "SELECT Id,Name,Language,Translation,CreatedOn,IsActive,IsDeleted,ModifiedOn,Typeid FROM ContentManagement where TypeId=3";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<ContentManagementView>(sql);
                return result.ToList();
            }
        }
        public async Task<ContentManagementView> GetPhrasesById(int id)
        {
            var sql = "SELECT Id,Name,Language,Translation,CreatedOn,IsActive,IsDeleted,ModifiedOn,Typeid FROM ContentManagement where TypeId=3 and Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<ContentManagementView>(sql, new { Id = id });
                return result;
            }
        }

        #endregion

        #region Validate_Methods
        private static void ValidateContentManagementResult(ContentManagementInsert item)
        {
            if (!EnumHelperMethods.EnumContainValue<ContentManagement>(item.TypeId))
            {
                throw new Exception("Type is invalid.");
            }
            if (item.TypeId == ContentManagement.Phrases)
            {
                if (string.IsNullOrEmpty(item.Translation))
                {
                    throw new Exception("Translation can't be blank");
                }
                if (string.IsNullOrEmpty(item.Language))
                {
                    throw new Exception("Language can't be blank");
                }
            }
        }

        private static void ValidateContentManagementResult(ContentManagementUpdate item)
        {
            if (!EnumHelperMethods.EnumContainValue<ContentManagement>(item.TypeId))
            {
                throw new Exception("Type is invalid.");
            }
            if (item.TypeId == ContentManagement.Phrases)
            {
                if (string.IsNullOrEmpty(item.Translation))
                {
                    throw new Exception("Translation can't be blank");
                }
                if (string.IsNullOrEmpty(item.Language))
                {
                    throw new Exception("Language can't be blank");
                }
            }
        }
        #endregion


    }
}
