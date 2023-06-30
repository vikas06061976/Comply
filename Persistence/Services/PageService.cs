using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.Pages;
using ComplyExchangeCMS.Domain.Services;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace ComplyExchangeCMS.Persistence.Services
{
    public class PageService : IPageService
    {
        private readonly IConfiguration _configuration;
        public PageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<int> Insert(PageInsertModel pagesModel)
        {
            pagesModel.CreatedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Name", pagesModel.Name, DbType.String);
                parameters.Add("@ParentId", pagesModel.ParentId, DbType.Int32);
                parameters.Add("@DisplayOnTopMenu", pagesModel.DisplayOnTopMenu, DbType.Boolean);
                parameters.Add("@RedirectPageLabelToURL", pagesModel.RedirectPageLabelToURL, DbType.String);
                parameters.Add("@MenuBackgroundColor", pagesModel.MenuBackgroundColor, DbType.String);
                parameters.Add("@UnselectedTextColor", pagesModel.UnselectedTextColor, DbType.String);
                parameters.Add("@SelectedTextColor", pagesModel.SelectedTextColor, DbType.String);                                                                                                                          
                parameters.Add("@DisplayOnFooter", pagesModel.DisplayOnFooter, DbType.Boolean);
                parameters.Add("@DisplayOnLeftMenu", pagesModel.DisplayOnLeftMenu, DbType.Boolean);
                parameters.Add("@PageContent", pagesModel.PageContent, DbType.String);
                parameters.Add("@Summary", pagesModel.Summary, DbType.String);
                parameters.Add("@CreatedOn", pagesModel.CreatedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertPages", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> UpdatePages(PageUpdateModel pagesModel)
        {
            pagesModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Id", pagesModel.Id, DbType.Int32);
                parameters.Add("@Name", pagesModel.Name, DbType.String);
                parameters.Add("@DisplayOnTopMenu", pagesModel.DisplayOnTopMenu, DbType.Boolean);
                parameters.Add("@DisplayOnFooter", pagesModel.DisplayOnFooter, DbType.Boolean);
                parameters.Add("@RedirectPageLabelToURL", pagesModel.RedirectPageLabelToURL, DbType.String);
                parameters.Add("@MenuBackgroundColor", pagesModel.MenuBackgroundColor, DbType.String);
                parameters.Add("@UnselectedTextColor", pagesModel.UnselectedTextColor, DbType.String);
                parameters.Add("@SelectedTextColor", pagesModel.SelectedTextColor, DbType.String);
                parameters.Add("@DisplayOnLeftMenu", pagesModel.DisplayOnLeftMenu, DbType.Boolean);
                parameters.Add("@PageContent", pagesModel.PageContent, DbType.String);
                parameters.Add("@Summary", pagesModel.Summary, DbType.String);
                parameters.Add("@IsActive", pagesModel.IsActive, DbType.Boolean);
                parameters.Add("@IsDeleted", pagesModel.IsDeleted, DbType.Boolean);
                parameters.Add("@ModifiedOn", pagesModel.ModifiedOn, DbType.DateTime);

                var result = await connection.ExecuteAsync("UpdatePages", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        //public async Task<IReadOnlyList<PageViewModel>> GetAllAsync()
        //{
        //    var sql = "SELECT * FROM Pages where IsActive=1 and IsDeleted=0";
        //    using (var connection = CreateConnection())
        //    {
        //        var result = await connection.QueryAsync<PageViewModel>(sql);
        //        return result.ToList();
        //    }
        //}

        public async Task<PaginationResponse<PageViewModel>> GetAllAsync
            (PaginationRequest request, string searchName)
        {
            using (var connection = CreateConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                IQueryable<PageViewModel> pages = connection.Query<PageViewModel>
                    ($@"SELECT * FROM Pages where ParentId is NULL and IsActive=1 and IsDeleted=0").AsQueryable();
                // and (name={searchName})

                // Apply search filter
                if (!string.IsNullOrEmpty(searchName))
                {
                    pages = pages.Where(f => f.Name.Contains(searchName));
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
                                    pages = pages.OrderBy(f => f.Name);
                                    break;
                                case "subpage":
                                    pages = pages.OrderBy(f => f.SubPage);
                                    break;
                                case "parent":
                                    pages = pages.OrderBy(f => f.Parent);
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case "desc":
                            switch (request.SortColumn.ToLower())
                            {
                                case "name":
                                    pages = pages.OrderByDescending(f => f.Name);
                                    break;
                                case "subpage":
                                    pages = pages.OrderByDescending(f => f.SubPage);
                                    break;
                                case "parent":
                                    pages = pages.OrderByDescending(f => f.Parent);
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
                var totalRecords = pages.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalRecords / request.PageSize);
                var records = pages.Skip((request.PageNumber - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToList();

                // Return pagination response object
                return new PaginationResponse<PageViewModel>
                {
                    TotalRecords = totalRecords,
                    TotalPages = totalPages,
                    Records = records
                };
            }
        }
        public async Task<PageViewModel> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Pages WHERE Id = @Id and IsActive=1 and IsDeleted=0";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<PageViewModel>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Pages WHERE Id = @Id";
            using (var connection = CreateConnection())
            {
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<PageViewModel> GetByNameAsync(string name)
        {
            var sql = "SELECT * FROM Pages WHERE Name like '%'+@Name+'%' and IsActive=1 and IsDeleted=0";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<PageViewModel>(sql, new { Name = name });
                return result;
            }
        }

        public async Task<IReadOnlyList<PageDropDownViewModel>> GetByParentIdAsync()
        {
            var sql = "select * from Pages where ParentId is null and IsActive=1 and IsDeleted=0";
            using (var connection = CreateConnection())
            {
                var result = await connection.QueryAsync<PageDropDownViewModel>(sql);
                return result.ToList();
            }
        }

        public async Task<int> InsertSubPage(PageInsertModel pagesModel)
        {
            pagesModel.CreatedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Name", pagesModel.Name, DbType.String);
                parameters.Add("@ParentId", pagesModel.ParentId, DbType.Int32);
                parameters.Add("@ParentId", pagesModel.ParentId, DbType.Int32);
                parameters.Add("@DisplayOnTopMenu", pagesModel.DisplayOnTopMenu, DbType.Boolean);
                parameters.Add("@DisplayOnFooter", pagesModel.DisplayOnFooter, DbType.Boolean);
                parameters.Add("@RedirectPageLabelToURL", pagesModel.RedirectPageLabelToURL, DbType.String);
                parameters.Add("@MenuBackgroundColor", pagesModel.MenuBackgroundColor, DbType.String);
                parameters.Add("@UnselectedTextColor", pagesModel.UnselectedTextColor, DbType.String);
                parameters.Add("@SelectedTextColor", pagesModel.SelectedTextColor, DbType.String);
                parameters.Add("@DisplayOnLeftMenu", pagesModel.DisplayOnLeftMenu, DbType.Boolean);
                parameters.Add("@PageContent", pagesModel.PageContent, DbType.String);
                parameters.Add("@Summary", pagesModel.Summary, DbType.String);
                parameters.Add("@CreatedOn", pagesModel.CreatedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertSubPages", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }

        public async Task<int> GetCountBySubPageIdAsync(int Id)
        {
            var sql = "select count(1) from Pages where ParentId=@Id and IsActive=1 and IsDeleted=0";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<int>(sql, new { Id = Id });
                return result;
            }
        }
        public async Task<int> InsertPageTranslation(PageTranslationInsert pagesModel)
        {
            pagesModel.CreatedOn = DateTime.UtcNow;
            pagesModel.ModifiedOn = DateTime.UtcNow;
            using (var connection = CreateConnection())
            {
                connection.Open();

                // Create the parameters for the stored procedure
                var parameters = new DynamicParameters();
                parameters.Add("@Name", pagesModel.Name, DbType.String);
                parameters.Add("@PageId", pagesModel.PageId, DbType.Int32);
                parameters.Add("@LanguageId", pagesModel.LanguageId, DbType.Int32);
                parameters.Add("@PageContent", pagesModel.PageContent, DbType.String);
                parameters.Add("@Summary", pagesModel.Summary, DbType.String);
                parameters.Add("@CreatedOn", pagesModel.CreatedOn, DbType.DateTime);
                parameters.Add("@ModifiedOn", pagesModel.ModifiedOn, DbType.DateTime);

                var result = await connection.QueryFirstOrDefaultAsync<int>("InsertTranslationPages", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<PageTranslationView> GetPageTranslation(int pageId, int languageId)
        {
            var sql = "select * from PageTranslations where PageId=@pageId and LanguageId=@languageId";
            using (var connection = CreateConnection())
            {
                var result = await connection.QuerySingleOrDefaultAsync<PageTranslationView>(sql, new { pageId = pageId, languageId = languageId });
                return result;
            }
        }


    }
}
