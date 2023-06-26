 using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc; 
using ComplyExchangeCMS.Domain.Entities.Masters;
using Domain.Services;
using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.Documentation;
using ComplyExchangeCMS.Domain.Models.LOB;
using ComplyExchangeCMS.Domain;
using System.Threading;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LOBController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public LOBController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GetLOB")]
        public async Task<IActionResult> GetLOB()
        {
            var data = await unitOfWork.LOBService.GetLOB();
            return Ok(data);
        }

        [HttpGet("GetAllLOB")]
        public async Task<IActionResult> GetAllLOB()
        {
            var data = await unitOfWork.LOBService.GetAllAsync();
            return Ok(data);
        }

        [HttpPost("InsertLOB")]
        public async Task<IActionResult> CreateLOB(LOBInsert insertModel)
        {
            await unitOfWork.LOBService.InsertLOB(insertModel);
            return Ok("LOB updated successfully.");
        }
    }
}
