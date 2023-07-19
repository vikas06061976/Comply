using ComplyExchangeCMS.Domain.Models.ContentBlock;
using ComplyExchangeCMS.Domain.Models.ContentManagement;
using ComplyExchangeCMS.Domain.Models.Master;
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
                        string sql = "INSERT INTO [dbo].[ContentManagement] ([Name] ,[Text] ,[CreatedOn] ,[IsActive] ,[IsDeleted] ,[ModifiedOn]) VALUES (@Name, @Text ,GETUTCDATE() ,1 ,0 ,NULL)";
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
            var sql = "UPDATE ContentManagement SET Name = @Name ,Text = @Text ,ModifiedOn=@ModifiedOn WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, contentBlock);
                return result;
            }
        }
        public async Task<IReadOnlyList<ContentManagementView>> GetAllContent()
        {
            var sql = "SELECT Id,Name,Text,CreatedOn,ModifiedOn FROM ContentManagement";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<ContentManagementView>(sql);
                return result.ToList();
            }
        }
        public async Task<ContentManagementView> GetContentById(int id)
        {
            var sql = "SELECT Id,Name,Text,CreatedOn,ModifiedOn FROM ContentManagement where Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<ContentManagementView>(sql, new { Id = id });
                return result;
            }
        }
        public async Task<int> InsertContentTranslation(ContentManagementLanguageInsert contentModel)
        {
            contentModel.CreatedOn = DateTime.UtcNow;
            contentModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@ContentId", contentModel.ContentBlockId, DbType.Int32);
                parameters.Add("@LanguageId", contentModel.LanguageId, DbType.Int32);
                parameters.Add("@Content", contentModel.Content, DbType.String);
                parameters.Add("@BulkTranslation", contentModel.BulkTranslation, DbType.Boolean);
                parameters.Add("@CreatedOn", contentModel.CreatedOn, DbType.DateTime);
                parameters.Add("@ModifiedOn", contentModel.ModifiedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertContentTranslation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<ContentManagementLanguageView> GetContentTranslation(int contentId, int languageId)
        {
            var sql = "SELECT * FROM ContentManagementTranslations where ContentId= @ContentId and LanguageId=@languageId";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<ContentManagementLanguageView>(sql, new { ContentId = contentId, languageId = languageId });
                return result;
            }
        }

        public async Task<IReadOnlyList<ModuleLanguageView>> GetAllLanguage(int contentId)
        {
            var sql = "select l.Id,l.Name,cmt.ContentId as ModuleId from Languages as l left join ContentManagementTranslations as cmt on l.Id=cmt.LanguageId AND cmt.ContentId = @ContentId";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<ModuleLanguageView>(sql, new { ContentId = contentId });
                return result.ToList();
            }
        }

        #endregion

    }
}
