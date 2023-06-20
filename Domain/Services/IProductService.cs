using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Entities.Masters;
using ComplyExchangeCMS.Domain.Services;

namespace Domain.Services
{
    public interface IProductService : IGenericService<Product>
    {
    }
}
