using ComplyExchangeCMS.Domain.Models.FormInstructions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Domain.Services
{
    public interface IFormInstructionsService
    {
        Task<int> InsertFormInstruction(FormInstructionsInsert formInstructionsModel);
        Task<int> UpdateFormInstruction(FormInstructionsUpdate formInstructionsModel);
        Task<PaginationResponse<FormInstructionsView>> GetAllAsync(PaginationRequest request, string searchName);
        Task<FormInstructionsView> GetByIdAsync(int id);
        Task<int> DeleteAsync(int id);
    }
}
