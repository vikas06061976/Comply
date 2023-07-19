 using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc; 
using ComplyExchangeCMS.Domain.Entities.Masters;
using Domain.Services;
using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.Agent;
using ComplyExchangeCMS.Domain;
using System.Threading;
using System.Collections.Generic;
using ComplyExchangeCMS.Domain.Models.Rules;
using ComplyExchangeCMS.Domain.Models.AgentUSSourceIncome;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentUSSourceIncomeController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AgentUSSourceIncomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        #region Agent IncomeType Hidden   
        [HttpGet("GetAgentIncomeTypeHidden")]
        public async Task<IActionResult> GetAgentIncomeTypeHiddenByAgentId(int id)
        {
            var data = await unitOfWork.AgentUSSourceIncomeService.GetAgentIncomeTypeHiddenByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentIncomeTypeHidden")]
        public async Task<IActionResult> UpsertAgentIncomeTypeHidden(int agentId, List<int> existingAgentIncomeTypeIds)
        {
            await unitOfWork.AgentUSSourceIncomeService.UpsertAgentIncomeTypeHiddenAsync(agentId, existingAgentIncomeTypeIds);
            return Ok("Agent IncomeType Hidden List updated successfully.");
        }
        #endregion

        #region USSourcedIncomeTypeSelection


        [HttpGet("GetUSSourcedIncomeTypeSelection")]
        public async Task<IActionResult> GetUSSourcedIncomeTypeSelection(int AgentId)
        {
            var data = await unitOfWork.AgentUSSourceIncomeService.GetAgentUSSourcedIncomeTypeSelectionAsync(AgentId);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpGet("GetUSSourcedIncomeTypeSelectionById")]
        public async Task<IActionResult> GetById(int agentid, int uSSourcedIncomeTypeId)
        {
            var data = await unitOfWork.AgentUSSourceIncomeService.GetAgentUSSourcedIncomeTypeSelectionByIdAsync(agentid, uSSourcedIncomeTypeId);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertUSSourcedIncomeTypeSelection")]
        public async Task<IActionResult> UpsertUSSourcedIncomeTypeSelection(USSourcedIncomeTypeSelectionUpsertModel upsertModel)
        {
            await unitOfWork.AgentUSSourceIncomeService.UpsertUSSourcedIncomeTypeSelectionAsync(upsertModel);
            return Ok("US Sourced Income Type updated successfully.");
        }
        #endregion
    }
}
