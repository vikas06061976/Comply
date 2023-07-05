using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.Documentation;
using ComplyExchangeCMS.Domain.Services;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class DocumentationService : IDocumentationService
    {
        private readonly IConfiguration _configuration;
        public DocumentationService(IConfiguration configuration) 
        {
            _configuration = configuration; 
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<IReadOnlyList<DocumentationType>> GetDocumentTypes()
        {
            var sql = "SELECT Id,Name FROM DocumentTypes where IsActive=1";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<DocumentationType>(sql);
                return result.ToList();
            }
        }
        public async Task<PaginationResponse<DocumentationView>> GetAllAsync(PaginationRequest request, string searchName)
        {
            using (var connection = CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IQueryable<DocumentationView> documents = connection.Query<DocumentationView>
                    ($@"SELECT * FROM Documentations where IsActive=1").AsQueryable();
                // and (name={searchName})

                // Apply search filter
                if (!string.IsNullOrEmpty(searchName))
                {
                    documents = documents.Where(f => f.Name.Contains(searchName));
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
                                    documents = documents.OrderBy(f => f.Name);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "desc":
                            switch (request.SortColumn.ToLower())
                            {
                                case "name":
                                    documents = documents.OrderByDescending(f => f.Name);
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
                var totalRecords = documents.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalRecords / request.PageSize);
                var records = documents.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                // Return pagination response object
                return new PaginationResponse<DocumentationView>
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    Records = records
                };
            }
        }
        public async Task<int> InsertDocument(DocumentationInsert documentModel)
        {
            documentModel.CreatedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Name", documentModel.Name, DbType.String);
                parameters.Add("@DocumentationId", documentModel.DocumentationId, DbType.Int32);
                parameters.Add("@CreatedOn", documentModel.CreatedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertDocumentation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<int> UpdateDocument(DocumentationUpdate documentModel)
        {
            documentModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Id", documentModel.Id, DbType.Int32);
                parameters.Add("@Name", documentModel.Name, DbType.String);
                parameters.Add("@IsActive", documentModel.IsActive, DbType.Boolean);
                parameters.Add("@ModifiedOn", documentModel.ModifiedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("UpdateDocumentation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<int> DeleteDocument(int Id)
        {
            var sql = "DELETE FROM Documentations WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = Id });
                return result;
            }
        }
        public async Task<DocumentationView> GetByIdAsync(int Id)
        {
            var sql = "SELECT * FROM Documentations WHERE Id = @Id and IsActive=1";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<DocumentationView>(sql, new { Id = Id });
                return result;
            }
        }
        public async Task<int> InsertDocumentTranslation(DocumentTranslationInsert documentationsModel)
        {
            documentationsModel.CreatedOn = DateTime.UtcNow;
            documentationsModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Content", documentationsModel.Content, DbType.String);
                parameters.Add("@DocId", documentationsModel.DocId, DbType.Int32);
                parameters.Add("@LanguageId", documentationsModel.LanguageId, DbType.Int32);
                parameters.Add("@BulkTranslation", documentationsModel.BulkTranslation, DbType.Boolean);
                parameters.Add("@CreatedOn", documentationsModel.CreatedOn, DbType.DateTime);
                parameters.Add("@ModifiedOn", documentationsModel.ModifiedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertTranslationDocumentation", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<DocumentationTranslationView> GetDocumentTranslation(int docId, int languageId)
        {
            var sql = "select * from DocumentationTranslations where DocId=@docId and LanguageId=@languageId";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<DocumentationTranslationView>(sql, new { docId = docId, languageId = languageId });
                return result;
            }
        }
        public async Task<IReadOnlyList<DocTranslationView>> GetAllLanguage(int docId)
        {
            var sql = "select dt.LanguageId,l.Name,dt.DocId from Languages as l left join DocumentationTranslations as dt on l.Id=dt.LanguageId where dt.DocId=@docId or dt.DocId is null";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<DocTranslationView>(sql, new { docId = docId });
                return result.ToList();
            }
        }
    }
}
