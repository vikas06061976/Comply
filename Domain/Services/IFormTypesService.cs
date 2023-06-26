using ComplyExchangeCMS.Domain.Models.FormTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface IFormTypesService
    {
        Task<int> Insert(FormTypesInsert formTypesModel);
        Task<int> Update(FormTypesUpdate formTypesModel);
        //Task<IReadOnlyList<FormTypesView>> GetAllAsync();
        Task<PaginationResponse<FormTypesView>> GetAllAsync(PaginationRequest request, string searchName);
        Task<FormTypesView> GetByIdAsync(int id);
        Task<int> UpdateUSCertificate(FormTypesUSCertiUpdate formTypesUSCerti);
        Task<IReadOnlyList<FormTypesUSCertiView>> GetAllUSCertificate();
        Task<FormTypesUSCertiView> GetByIdUSCertificate(int id);
    }
}
