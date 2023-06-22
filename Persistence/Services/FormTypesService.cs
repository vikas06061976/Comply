using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.FormTypes;
using ComplyExchangeCMS.Domain.Models.Pages;
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
    public class FormTypesService :IFormTypesService
    {
        private readonly IConfiguration _configuration;
        public FormTypesService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<int> Insert(FormTypesInsert formTypesModel)
        {
            formTypesModel.CreatedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Name", formTypesModel.Name, DbType.String);
                parameters.Add("@DisplayName", formTypesModel.DisplayName, DbType.String);
                parameters.Add("@Revision", formTypesModel.Revision, DbType.String);
                parameters.Add("@IsDisabled", formTypesModel.IsDisabled, DbType.Boolean);
                parameters.Add("@ESubmitPDFTemplateId", formTypesModel.ESubmitPDFTemplateId, DbType.String);
                parameters.Add("@PrintPDFTemplateId", formTypesModel.PrintPDFTemplateId, DbType.String);
                parameters.Add("@Logo", formTypesModel.Logo, DbType.String);
                parameters.Add("@IntroductionText", formTypesModel.IntroductionText, DbType.String);
                parameters.Add("@TINPageText", formTypesModel.TINPageText, DbType.String);
                parameters.Add("@CertificationText", formTypesModel.CertificationText, DbType.String);
                parameters.Add("@ESignatureText", formTypesModel.ESignatureText, DbType.String);
                parameters.Add("@ESignatureConfirmationText", formTypesModel.ESignatureConfirmationText, DbType.String);
                parameters.Add("@CreatedOn", formTypesModel.CreatedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertFormTypes", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> Update(FormTypesUpdate formTypesModel)
        {
            formTypesModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", formTypesModel.Id, DbType.Int32);
                parameters.Add("@Name", formTypesModel.Name, DbType.String);
                parameters.Add("@DisplayName", formTypesModel.DisplayName, DbType.String);
                parameters.Add("@Revision", formTypesModel.Revision, DbType.String);
                parameters.Add("@IsDisabled", formTypesModel.IsDisabled, DbType.Boolean);
                parameters.Add("@ESubmitPDFTemplateId", formTypesModel.ESubmitPDFTemplateId, DbType.String);
                parameters.Add("@PrintPDFTemplateId", formTypesModel.PrintPDFTemplateId, DbType.String);
                parameters.Add("@Logo", formTypesModel.Logo, DbType.String);
                parameters.Add("@IntroductionText", formTypesModel.IntroductionText, DbType.String);
                parameters.Add("@TINPageText", formTypesModel.TINPageText, DbType.String);
                parameters.Add("@CertificationText", formTypesModel.CertificationText, DbType.String);
                parameters.Add("@ESignatureText", formTypesModel.ESignatureText, DbType.String);
                parameters.Add("@ESignatureConfirmationText", formTypesModel.ESignatureConfirmationText, DbType.String);
                parameters.Add("@IsActive", formTypesModel.IsActive, DbType.Boolean);
                parameters.Add("@IsDeleted", formTypesModel.IsDeleted, DbType.Boolean);
                parameters.Add("@ModifiedOn", formTypesModel.ModifiedOn, DbType.DateTime);

                var result = await connection.ExecuteAsync("UpdateFormTypes", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        //public async Task<IReadOnlyList<FormTypesView>> GetAllAsync()
        //{
        //    var sql = "SELECT * FROM FormTypesSelfCertifications where IsActive=1 and IsDeleted=0";
        //    using (var connection = CreateConnection())
        //    {
        //        var result = await connection.QueryAsync<FormTypesView>(sql);
        //        return result.ToList();
        //    }
        //}

        public async Task<PaginationResponse<FormTypesView>> GetAllAsync(PaginationRequest request, string searchName)
        {
            using (var connection = CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IQueryable<FormTypesView> forms = connection.Query<FormTypesView>
                    ($@"SELECT * FROM FormTypesSelfCertifications where IsActive=1 and IsDeleted=0").AsQueryable();
                // and (name={searchName})
                // Apply search filter
                if (!string.IsNullOrEmpty(searchName))
                {
                    forms = forms.Where(f => f.Name.Contains(searchName));
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
                                    forms = forms.OrderBy(f => f.Name);
                                    break;
                                case "displayName":
                                    forms = forms.OrderBy(f => f.DisplayName);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "desc":
                            switch (request.SortColumn.ToLower())
                            {
                                case "name":
                                    forms = forms.OrderByDescending(f => f.Name);
                                    break;
                                case "displayName":
                                    forms = forms.OrderByDescending(f => f.DisplayName);
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
                var totalRecords = forms.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalRecords / request.PageSize);
                var records = forms.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                // Return pagination response object
                return new PaginationResponse<FormTypesView>
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    Records = records
                };
            }
        }


        public async Task<FormTypesView> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM FormTypesSelfCertifications WHERE Id = @Id and IsActive=1 and IsDeleted=0";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<FormTypesView>(sql, new { Id = id });
                return result;
            }
        }

        #region Form types (United States Certificates)

        public async Task<int> UpdateUSCertificate(FormTypesUSCertiUpdate formTypesUSCerti)
        {
            formTypesUSCerti.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", formTypesUSCerti.Id, DbType.Int32);
                parameters.Add("@Name", formTypesUSCerti.Name, DbType.String);
                parameters.Add("@IsDisabled", formTypesUSCerti.IsDisabled, DbType.Boolean);
                parameters.Add("@FullHeader", formTypesUSCerti.FullHeader, DbType.String);
                parameters.Add("@SummaryHeader", formTypesUSCerti.SummaryHeader, DbType.Boolean);
                parameters.Add("@Description", formTypesUSCerti.Description, DbType.String);
                parameters.Add("@SubstituteFormStatement", formTypesUSCerti.SubstituteFormStatement, DbType.String);
                parameters.Add("@ESubmitStatement", formTypesUSCerti.ESubmitStatement, DbType.String);
                parameters.Add("@PrintTemplatePDF", formTypesUSCerti.PrintTemplatePDFId, DbType.String);
                parameters.Add("@ESubmitTemplatePDF", formTypesUSCerti.ESubmitTemplatePDFId, DbType.String);
                parameters.Add("@UseOnboardingURL", formTypesUSCerti.UseOnboardingURL, DbType.String);
                parameters.Add("@SpecifyURL", formTypesUSCerti.SpecifyURL, DbType.String);
                parameters.Add("@IsActive", formTypesUSCerti.IsActive, DbType.Boolean);
                parameters.Add("@IsDeleted", formTypesUSCerti.IsDeleted, DbType.Boolean);
                parameters.Add("@ModifiedOn", formTypesUSCerti.ModifiedOn, DbType.DateTime);

                var result = await connection.ExecuteAsync("UpdateUSCertiFicationFormTypes", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<IReadOnlyList<FormTypesUSCertiView>> GetAllUSCertificate()
        {
            var sql = "SELECT * FROM [FormTypesUSCertificates] where IsActive=1 and IsDeleted=0";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<FormTypesUSCertiView>(sql);
                return result.ToList();
            }
        }

        public async Task<FormTypesUSCertiView> GetByIdUSCertificate(int id)
        {
            var sql = "SELECT * FROM [FormTypesUSCertificates] WHERE Id = @Id and IsActive=1 and IsDeleted=0";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<FormTypesUSCertiView>(sql, new { Id = id });
                return result;
            }
        }

        #endregion
    }
}
