using ComplyExchangeCMS.Domain.Models.Capacities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface ICapacitiesService
    {
        Task<int> InsertCapacities(CapacitiesInsert capacityModel);
        Task<int> UpdateCapacities(CapacitiesUpdate capacityModel);
        Task<PaginationResponse<CapacitiesView>> GetAllAsync(PaginationRequest request, string searchName);
        Task<CapacitiesView> GetByIdAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}
