using System;
using Domain.Repositories;
using Microsoft.AspNetCore.Hosting;
using ComplyExchangeCMS.Services.Abstractions;

namespace Services
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IProductService> _lazyProductService; 
      public ServiceManager(IRepositoryManager repositoryManager, IHostingEnvironment environment)
        {
            _lazyProductService = new Lazy<IProductService>(() => new ProductService(repositoryManager));
         }
        public IProductService ProductService
        {
            get
            {
                return _lazyProductService.Value;
            }
        }   
    }
}
