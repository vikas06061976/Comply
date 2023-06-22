 using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc; 
using ComplyExchangeCMS.Domain.Entities.Masters;
using Domain.Services;
using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.Documentation;
using ComplyExchangeCMS.Domain.Models.LOB;
using ComplyExchangeCMS.Domain;
using System.Threading;
using ComplyExchangeCMS.Domain.Models.Capacities;
using ComplyExchangeCMS.Domain.Models.FormInstructions;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormInstructionsController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public FormInstructionsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GetFormInstructionsById")]
        public async Task<IActionResult> GetFormInstructionsById(int Id)
        {
            var data = await unitOfWork.FormInstructionsService.GetByIdAsync(Id);
            return Ok(data);
        }

        [HttpGet("GetAllFormInstructions")]
        public async Task<IActionResult> GetAllFormInstructions(string searchTerm, int pageNumber, int pageSize, string sortColumn,
          string sortDirection, CancellationToken cancellationToken = default)
        {
            var request = new PaginationRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortDirection = sortDirection
            };
            var data = await unitOfWork.FormInstructionsService.GetAllAsync(request, searchTerm);
            return Ok(data);
        }

        [HttpPost("InsertFormInstructions")]
        public async Task<IActionResult> CreateFormInstructions(FormInstructionsInsert formInsModel)
        {
            await unitOfWork.FormInstructionsService.InsertFormInstruction(formInsModel);
            return Ok("Capacity saved successfully.");
        }

        [HttpPut("UpdateFormInstructions")]
        public async Task<IActionResult> UpdateFormInstructions(FormInstructionsUpdate formInsModel)
        {
            await unitOfWork.FormInstructionsService.UpdateFormInstruction(formInsModel);
            return Ok("Capacity updated successfully.");
        }

        [HttpDelete("DeleteFormInstructions")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.FormInstructionsService.DeleteAsync(id);
            return Ok(data);
        }
    }
}
