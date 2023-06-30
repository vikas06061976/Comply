using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ComplyExchangeCMS.Domain.Entities.Masters;
using Domain.Services;
using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.Documentation;
using ComplyExchangeCMS.Domain;
using System.Threading;
using ComplyExchangeCMS.Domain.Models.Rules;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public RuleController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllRules")]
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
            var pages = await unitOfWork.RuleService.GetAllAsync
                (request, searchTerm);

            return Ok(pages);
        }

        [HttpPost("InsertRules")]
        public async Task<IActionResult> CreateRules(RulesInsert ruleModel)
        {
            await unitOfWork.RuleService.InsertRules(ruleModel);
            return Ok("Rules created successfully.");
        }

        [HttpPut("UpdateRules")]
        public async Task<IActionResult> UpdateRules(RulesUpdate ruleModel)
        {
            await unitOfWork.RuleService.UpdateRules(ruleModel);
            return Ok("Rules updated successfully.");
        }

        [HttpGet("GetRulesById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.RuleService.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpDelete("RulesDelete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.RuleService.DeleteRules(id);
            return Ok(data);
        }
        [HttpPost("InsertRuleTranslation")]
        public async Task<IActionResult> InsertRuleTranslation(RuleTranslationInsert ruleModel)
        {
            await unitOfWork.RuleService.InsertRulesTranslation(ruleModel);
            return Ok("Rule translation updated successfully.");
        }

        [HttpGet("GetRuleTranslation")]
        public async Task<IActionResult> GetRuleTranslation(int ruleId, int languageId)
        {
            var data = await unitOfWork.RuleService.GetRuleTranslation(ruleId, languageId);
            if (data == null) return Ok();
            return Ok(data);
        }
    }
}
