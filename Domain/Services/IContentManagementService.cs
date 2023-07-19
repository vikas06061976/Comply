using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.ContentBlock;
using ComplyExchangeCMS.Domain.Models.ContentManagement;
using ComplyExchangeCMS.Domain.Models.Master;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface IContentManagementService
    {
        void UploadFile(IFormFile files);
        void InsertContent(string filePath);
        byte[] GenerateExcelFile();
        Task<int> UpdateContent(ContentManagementUpdate contentBlock);
        Task<IReadOnlyList<ContentManagementView>> GetAllContent();
        Task<ContentManagementView> GetContentById(int id);
        (string fileType, byte[] archiveData, string archiveName) DownloadFiles();
        Task<ContentManagementLanguageView> GetContentTranslation(int contentId, int languageId);
        Task<IReadOnlyList<ModuleLanguageView>> GetAllLanguage(int contentId);
        Task<int> InsertContentTranslation(ContentManagementLanguageInsert contentModel);

    }
}
