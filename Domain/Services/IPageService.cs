using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Entities.Masters;
using ComplyExchangeCMS.Domain.Models.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface IPageService
    {
        Task<int> Insert(PageInsertModel pagesModel);
        Task<int> UpdatePages(PageUpdateModel pagesModel);
        Task<IReadOnlyList<PageLanguageView>> GetAllLanguage(int pageId);
        Task<PageViewModel> GetByIdAsync(int id);
        Task<int> DeleteAsync(int id);
        Task<PageViewModel> GetByNameAsync(string name);
        Task<IReadOnlyList<PageDropDownViewModel>> GetByParentIdAsync();
        Task<int> InsertSubPage(PageInsertModel pagesModel);
        Task<int> GetCountBySubPageIdAsync(int Id);
        Task<PaginationResponse<PageViewModel>> GetAllAsync(PaginationRequest request, string searchName); 
        Task<int> InsertPageTranslation(PageTranslationInsert pagesModel);
        Task<PageTranslationView> GetPageTranslation(int pageId, int languageId);
    }
}
