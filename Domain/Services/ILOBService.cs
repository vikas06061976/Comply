using ComplyExchangeCMS.Domain.Models.LOB;
using ComplyExchangeCMS.Domain.Models.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface ILOBService
    {
        Task<IReadOnlyList<Chapter3Status>> GetLOB();
        Task<IReadOnlyList<LOBView>> GetAllAsync();
        Task<int> InsertLOB(LOBInsert LOBModel);
    }
}
