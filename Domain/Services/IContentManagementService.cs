using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.ContentBlock;
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
        Task<int> UpdateContentText(ContentManagementUpdateText contentBlock);
        Task<IReadOnlyList<ContentManagementView>> GetAllContent();
        Task<ContentManagementView> GetContentById(int id);
        (string fileType, byte[] archiveData, string archiveName) DownloadFiles();

        #region Easy Help
        Task<int> InsertContentManagement(ContentManagementInsert contentMgntModel);
        Task<int> UpdateContentManagement(ContentManagementUpdate contentMgntModel);
        Task<IReadOnlyList<ContentManagementView>> GetAllContentManagement(int TypeId);
        Task<ContentManagementView> GetContentManagementById(int TypeId, int Id);

        #endregion
    }
}
