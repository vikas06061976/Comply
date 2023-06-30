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

        #region Agent Capacity Hidden   
        [HttpGet("GetAgentCapacityHidden")]
        public async Task<IActionResult> GetAgentCapacityHiddenByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentCapacityHiddenByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentCapacityHidden")]
        public async Task<IActionResult> UpsertAgentCapacityHidden(int agentId, List<int> existingAgentChapter4EntityTypeIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentCapacityHiddenAsync(agentId, existingAgentChapter4EntityTypeIds);
            return Ok("Agent Capacity Hidden List updated successfully.");
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

        #region Agent Documentation Mandatory   
        [HttpGet("GetAgentDocumentationMandatory")]
        public async Task<IActionResult> GetAgentDocumentationMandatoryByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentDocumentationMandatoryByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentDocumentationMandatory")]
        public async Task<IActionResult> UpsertAgentDocumentationMandatory(int agentId, List<AgentDocumentationMandatoryInsertModel> existingAgentDocumentationIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentDocumentationMandatoryAsync(agentId, existingAgentDocumentationIds);
            return Ok("Agent Documentation Mandatory List updated successfully.");
        }
        #endregion

        #region Agent ExemptionCode Disabled
        [HttpGet("GetAgentExemptionCodeDisabled")]
        public async Task<IActionResult> GetAgentExemptionCodeDisabledByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentExemptionCodeDisabledByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentExemptionCodeDisabled")]
        public async Task<IActionResult> UpsertAgentExemptionCodeDisabled(int agentId, List<int> existingAgentExemptionCodeIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentExemptionCodeDisabledAsync(agentId, existingAgentExemptionCodeIds);
            return Ok("Agent ExemptionCode Disabled List updated successfully.");
        }
        #endregion

        #region Agent IncomeCode Hidden   
        [HttpGet("GetAgentIncomeCodeHidden")]
        public async Task<IActionResult> GetAgentIncomeCodeHiddenByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentIncomeCodeHiddenByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentIncomeCodeHidden")]
        public async Task<IActionResult> UpsertAgentIncomeCodeHidden(int agentId, List<int> existingAgentIncomeCodeIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentIncomeCodeHiddenAsync(agentId, existingAgentIncomeCodeIds);
            return Ok("Agent IncomeCode Hidden List updated successfully.");
        }
        #endregion

        #region Agent USVisaType Hidden   
        [HttpGet("GetAgentUSVisaTypeHidden")]
        public async Task<IActionResult> GetAgentUSVisaTypeHiddenByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentUSVisaTypeHiddenByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentUSVisaTypeHidden")]
        public async Task<IActionResult> UpsertAgentUSVisaTypeHidden(int agentId, List<int> existingAgentUSVisaTypeIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentUSVisaTypeHiddenAsync(agentId, existingAgentUSVisaTypeIds);
            return Ok("Agent USVisaType Hidden List updated successfully.");
        }
        #endregion

        #region Agent FATCAExemptionCode Hidden
        [HttpGet("GetAgentFATCAExemptionCodeHidden")]
        public async Task<IActionResult> GetAgentFATCAExemptionCodeHiddenByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentFATCAExemptionCodeHiddenByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentFATCAExemptionCodeHidden")]
        public async Task<IActionResult> UpsertAgentFATCAExemptionCodeHidden(int agentId, List<int> existingAgentFATCAExemptionCodeIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentFATCAExemptionCodeHiddenAsync(agentId, existingAgentFATCAExemptionCodeIds);
            return Ok("Agent FATCAExemptionCode Hidden List updated successfully.");
        }
        #endregion

        #region Agent PaymentType  
        [HttpGet("GetAgentPaymentType")]
        public async Task<IActionResult> GetAgentPaymentTypeByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentPaymentTypeByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentPaymentType")]
        public async Task<IActionResult> UpsertAgentPaymentType(int agentId, List<AgentPaymentTypeInsertModel> existingAgentPaymentTypeIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentPaymentTypeAsync(agentId, existingAgentPaymentTypeIds);
            return Ok("Agent PaymentType List updated successfully.");
        }
        #endregion

        #region Agent FATCAEntityGIINChallenge Disabled
        [HttpGet("GetAgentFATCAEntityGIINChallengeDisabled")]
        public async Task<IActionResult> GetAgentFATCAEntityGIINChallengeDisabledByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentFATCAEntityGIINChallengeDisabledByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentFATCAEntityGIINChallengeDisabled")]
        public async Task<IActionResult> UpsertAgentFATCAEntityGIINChallengeDisabled(int agentId, List<int> existingAgentFATCAEntityGIINChallengeIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentFATCAEntityGIINChallengeDisabledAsync(agentId, existingAgentFATCAEntityGIINChallengeIds);
            return Ok("Agent FATCAEntityGIINChallenge Disabled List updated successfully.");
        }
        #endregion


        #region Agent AgentSPTQuestion Hidden  
        [HttpGet("GetAgentSPTQuestionHidden")]
        public async Task<IActionResult> GetAgentSPTQuestionHiddenByAgentId(int id)
        {
            var data = await unitOfWork.AgentEditListService.GetAgentSPTQuestionHiddenByAgentIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }
        [HttpPost("UpsertAgentSPTQuestionHidden")]
        public async Task<IActionResult> UpsertAgentSPTQuestionHidden(int agentId, List<AgentSPTQuestionInsertModel> existingAgentSPTQuestionIds)
        {
            await unitOfWork.AgentEditListService.UpsertAgentSPTQuestionHiddenAsync(agentId, existingAgentSPTQuestionIds);
            return Ok("Agent SPTQuestion Hidden List updated successfully.");
        }
        #endregion
    }
}
