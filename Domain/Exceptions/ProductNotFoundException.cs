using ComplyExchangeCMS.Domain.Entities.Masters;
using System;

namespace ComplyExchangeCMS.Domain.Exceptions
{
    public sealed class ProductNotFoundException : NotFoundException
    {
        public ProductNotFoundException(Guid ProductId)
            : base($"Product with the identifier {ProductId} was not found.")
        {
        }
    }
}
