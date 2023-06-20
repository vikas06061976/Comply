using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks; 
using ComplyExchangeCMS.Contracts.Models.ProductModels; 
using ComplyExchangeCMS.Domain;

namespace ComplyExchangeCMS.Services.Abstractions
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<PaginationResponse<ProductViewModel>> GetAllAsync
       (string searchTerm, PaginationRequest request,
       CancellationToken cancellationToken = default);
        Task<IEnumerable<ProductViewModel>> SearchAllAsync(string searchTerm, CancellationToken cancellationToken = default);

        Task<ProductViewModel> GetByIdAsync(Guid ProductId, CancellationToken cancellationToken = default);

        Task<ProductViewModel> CreateAsync(ProductInsertModel ProductForCreationModel, CancellationToken cancellationToken = default);

        Task UpdateAsync(ProductUpdateModel ProductForUpdateModel, CancellationToken cancellationToken = default);

        Task DeleteAsync(Guid ProductId, CancellationToken cancellationToken = default);
    }
}