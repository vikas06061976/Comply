 using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc; 
using ComplyExchangeCMS.Domain.Entities.Masters;
using Domain.Services;
using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.AgentEditList;
using ComplyExchangeCMS.Domain;
using System.Threading;
using ComplyExchangeCMS.Domain.Models.Agent;
using System.Collections.Generic;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentEditListController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public AgentEditListController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        #region Agent Countries Important   
        [HttpGet("GetAgentCountriesImportant")]
        public async Task<IActionResult> GetAgentCountriesImportantByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentCountriesImportantByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentCountriesImportant")]
        public async Task<IActionResult> UpsertAgentCountriesImportant(int agentId, List<int> existingAgentCountryIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentCountriesImportantAsync(agentId, existingAgentCountryIds);
            return Ok("Agent Countries Important List updated successfully.");
        }


        #endregion

        #region Agent Countries Hidden   
        [HttpGet("GetAgentCountriesHidden")]
        public async Task<IActionResult> GetAgentCountriesHiddenByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentCountriesHiddenByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentCountriesHidden")]
        public async Task<IActionResult> UpsertAgentCountriesHidden(int agentId, List<int> existingAgentCountryIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentCountriesHiddenAsync(agentId, existingAgentCountryIds);
            return Ok("Agent Countries Hidden List updated successfully.");
        }
        #endregion

        #region Agent Chapter3EntityType Hidden   
        [HttpGet("GetAgentChapter3EntityTypeHidden")]
        public async Task<IActionResult> GetAgentChapter3EntityTypeHiddenByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentChapter3EntityTypeHiddenByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentChapter3EntityTypeHidden")]
        public async Task<IActionResult> UpsertAgentChapter3EntityTypeHidden(int agentId, List<int> existingAgentChapter4EntityTypeIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentChapter3EntityTypeHiddenAsync(agentId, existingAgentChapter4EntityTypeIds);
            return Ok("Agent Chapter3EntityType Hidden List updated successfully.");
        }
        #endregion


        #region Agent Chapter4EntityType Hidden   
        [HttpGet("GetAgentChapter4EntityTypeHidden")]
        public async Task<IActionResult> GetAgentChapter4EntityTypeHiddenByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentChapter4EntityTypeHiddenByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentChapter4EntityTypeHidden")]
        public async Task<IActionResult> UpsertAgentChapter4EntityTypeHidden(int agentId, List<int> existingAgentChapter4EntityTypeIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentChapter4EntityTypeHiddenAsync(agentId, existingAgentChapter4EntityTypeIds);
            return Ok("Agent Chapter4EntityType Hidden List updated successfully.");
        }
        #endregion

        #region Agent Chapter4EntityType Important   
        [HttpGet("GetAgentChapter4EntityTypeImportant")]
        public async Task<IActionResult> GetAgentChapter4EntityTypeImportantByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentChapter4EntityTypeImportantByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentChapter4EntityTypeImportant")]
        public async Task<IActionResult> UpsertAgentChapter4EntityTypeImportant(int agentId, List<int> existingAgentChapter4EntityTypeIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentChapter4EntityTypeImportantAsync(agentId, existingAgentChapter4EntityTypeIds);
            return Ok("Agent Chapter4EntityType Important List updated successfully.");
        }
        #endregion

        //[HttpDelete("Delete")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var data = await unitOfWork.AgentEditListService.DeleteAsync(id);
        //    return Ok(data);
        //}
    }
}
