using ComplyExchangeCMS.Domain.Entities.Masters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services.Master
{
    public interface ICountryService
    {
        Task<IReadOnlyList<Country>> GetAllAsync();
    }
}
