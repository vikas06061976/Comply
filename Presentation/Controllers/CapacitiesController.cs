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

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CapacitiesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public CapacitiesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GetCapacitiesById")]
        public async Task<IActionResult> GetCapacitiesById(int Id)
        {
            var data = await unitOfWork.CapacitiesService.GetByIdAsync(Id);
            return Ok(data);
        }

        [HttpGet("GetAllCapacities")]
        public async Task<IActionResult> GetAllCapacities(string searchTerm, int pageNumber, int pageSize, string sortColumn,
          string sortDirection, CancellationToken cancellationToken = default)
        {
            var request = new PaginationRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortDirection = sortDirection
            };
            var data = await unitOfWork.CapacitiesService.GetAllAsync(request, searchTerm);
            return Ok(data);
        }

        [HttpPost("InsertCapacities")]
        public async Task<IActionResult> CreateCapacity(CapacitiesInsert capacitiesModel)
        {
            await unitOfWork.CapacitiesService.InsertCapacities(capacitiesModel);
            return Ok("Capacity saved successfully.");
        }

        [HttpPut("UpdateCapacities")]
        public async Task<IActionResult> UpdateCapacity(CapacitiesUpdate capacitiesModel)
        {
            await unitOfWork.CapacitiesService.UpdateCapacities(capacitiesModel);
            return Ok("Capacity updated successfully.");
        }

        [HttpDelete("DeleteCapacities")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.CapacitiesService.DeleteAsync(id);
            return Ok(data);
        }
    }
}
