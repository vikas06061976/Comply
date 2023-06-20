using ComplyExchangeCMS.Domain.Models.Documentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface IDocumentationService
    {
        //Task<IReadOnlyList<DocumentationView>> GetAllAsync();
        Task<PaginationResponse<DocumentationView>> GetAllAsync(PaginationRequest request, string searchName);
        Task<int> InsertDocument(DocumentationInsert documentModel);
        Task<int> UpdateDocument(DocumentationUpdate documentModel);
        Task<int> DeleteDocument(int Id);
        Task<DocumentationView> GetByIdAsync(int Id);
    }
}
