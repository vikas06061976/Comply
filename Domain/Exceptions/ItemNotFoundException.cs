using ComplyExchangeCMS.Domain.Entities.Masters;
using System;

namespace ComplyExchangeCMS.Domain.Exceptions
{
    public sealed class ItemNotFoundException : NotFoundException
    {
        public ItemNotFoundException(string description, string value)
            : base($"{description} with {value} was not found.")
        {
         //   Product with the identifier { productId} was not found.
        }
    }
}
