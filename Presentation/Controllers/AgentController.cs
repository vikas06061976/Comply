 using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc; 
using ComplyExchangeCMS.Domain.Entities.Masters;
using Domain.Services;
using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.Agent;
using ComplyExchangeCMS.Domain;
using System.Threading;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AgentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAgent(AgentInsert agents)
        {
            await unitOfWork.Agents.Insert(agents);
            return Ok("Agent created successfully.");
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAgent(AgentUpdate agents)
        {
            await unitOfWork.Agents.UpdateAgents(agents);
            return Ok("Agent updated successfully.");
        }

        [HttpGet("GetAllAgents")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var data = await unitOfWork.Agents.GetAllAsync();
        //    return Ok(data);
        //}

        public async Task<IActionResult> GetAll(string searchTerm, int pageNumber, int pageSize, string sortColumn,
        string sortDirection, CancellationToken cancellationToken = default)
        {
            var request = new PaginationRequest
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SortColumn = sortColumn,
                SortDirection = sortDirection
            };
            var pages = await unitOfWork.Agents.GetAllAsync
                (request, searchTerm);

            return Ok(pages);
        }

        [HttpGet("GetAgentById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Agents.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Agents.DeleteAsync(id);
            return Ok(data);
        }
    }
}
