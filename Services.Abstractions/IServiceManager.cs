using Domain.Repositories;

namespace ComplyExchangeCMS.Services.Abstractions
{
    public interface IServiceManager
    {
        #region Masters
        IProductService ProductService { get; }
        #endregion
    }
}
