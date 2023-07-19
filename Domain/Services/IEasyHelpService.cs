using ComplyExchangeCMS.Domain.Models.EasyHelp;
using ComplyExchangeCMS.Domain.Models.Master;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface IEasyHelpService
    {
        Task<PaginationResponse<EasyHelpView>> GetAllAsync
         (PaginationRequest request, string searchName);
        Task<int> InsertEasyHelp(EasyHelpInsert easyHelpModel);
        Task<int> UpdateEasyHelp(EasyHelpUpdate easyHelpModel);
        Task<EasyHelpView> GetByIdAsync(int id);
        Task<int> DeleteEasyHelp(int id); 
        Task<int> InsertEasyHelpTranslation(EasyHelpTranslation easyHelpModel);
        Task<EasyHelpTranslationView> GetEasyHelpTranslation(int easyHelpId, int languageId);
        Task<IReadOnlyList<ModuleLanguageView>> GetAllLanguage(int easyHelpId);
        void UploadFile(IFormFile files);
        byte[] GenerateExcelFile();
    }
}

