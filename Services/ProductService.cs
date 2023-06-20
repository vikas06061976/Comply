using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ComplyExchangeCMS.Contracts;
using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using ComplyExchangeCMS.Services.Abstractions;
using ComplyExchangeCMS.Domain.Entities.Masters; 
using ComplyExchangeCMS.Contracts.Models.ProductModels;
using ComplyExchangeCMS.Domain;

namespace Services
{
    internal sealed class ProductService : IProductService
    {
        private readonly IRepositoryManager _repositoryManager;

        public ProductService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var Products = await _repositoryManager.ProductRepository.GetAllAsync(cancellationToken);

            var ProductsData = Products.Adapt<IEnumerable<ProductViewModel>>();

            return ProductsData;
        }
        public async Task<PaginationResponse<ProductViewModel>> GetAllAsync
    (string searchTerm, PaginationRequest request,
    CancellationToken cancellationToken = default)
        {
            var response = await _repositoryManager.ProductRepository.GetAllAsync
                (searchTerm, request, cancellationToken);
            var ProductData = response.Records.Adapt
                <IEnumerable<ProductViewModel>>();

            return new PaginationResponse<ProductViewModel>
            {
                TotalRecords = response.TotalRecords,
                TotalPages = response.TotalPages,
                Records = ProductData
            };
        }
        public async Task<IEnumerable<ProductViewModel>> SearchAllAsync(string searchTerm, CancellationToken cancellationToken = default)
        {
            var Products = await _repositoryManager.ProductRepository.SearchAllAsync(searchTerm, cancellationToken);

            var ProductsData = Products.Adapt<IEnumerable<ProductViewModel>>();

            return ProductsData;
        }
        public async Task<ProductViewModel> GetByIdAsync(Guid ProductId, CancellationToken cancellationToken = default)
        {
            var Product = await _repositoryManager.ProductRepository.GetByIdAsync(ProductId, cancellationToken);

            if (Product is null)
            {
                throw new ProductNotFoundException(ProductId);
            }

            var ProductData = Product.Adapt<ProductViewModel>();

            return ProductData;
        }

        public async Task<ProductViewModel> CreateAsync(ProductInsertModel InsertModel, CancellationToken cancellationToken = default)
        {

            var Product = InsertModel.Adapt<Product>();

            _repositoryManager.ProductRepository.Insert(Product);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);

            return Product.Adapt<ProductViewModel>();
        }

        public async Task UpdateAsync( ProductUpdateModel UpdateModel, CancellationToken cancellationToken = default)
        {
            var Product = await _repositoryManager.ProductRepository.GetByIdAsync(UpdateModel.Id, cancellationToken);

            if (Product is null)
            {
                throw new ProductNotFoundException(UpdateModel.Id);
            }

            Product.Name = UpdateModel.Name;  

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid ProductId, CancellationToken cancellationToken = default)
        {
            var Product = await _repositoryManager.ProductRepository.GetByIdAsync(ProductId, cancellationToken);

            if (Product is null)
            {
                throw new ProductNotFoundException(ProductId);
            }

            _repositoryManager.ProductRepository.Remove(Product);

            await _repositoryManager.UnitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}