using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.Rules;
using Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHostingEnvironment webHostEnvironment;
        public RuleController(IUnitOfWork unitOfWork, IHostingEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
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

        [HttpGet("GetAllLanguage")]
        public async Task<IActionResult> GetAllLanguage(int ruleId)
        {
            var data = await unitOfWork.RuleService.GetAllLanguage(ruleId);
            return Ok(data);
        }

        [HttpPost("Import")]
        public IActionResult CreateEasyHelp(IFormFile formFile)
        {
            unitOfWork.RuleService.UploadFile(formFile);
            return Ok("File Uploaded successfully");
            
        }

        [HttpGet("Export")]
        public IActionResult DownloadExcel()
        {
            byte[] excelData = unitOfWork.RuleService.GenerateExcelFile();

            // Set the file name and content type
            string fileName = "Rules.xlsx";
            string easyHelpType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            // Save the Excel file to the wwwroot folder
            string webRootPath = webHostEnvironment.WebRootPath;
            string filePath = Path.Combine(webRootPath, fileName);
            System.IO.File.WriteAllBytes(filePath, excelData);

            // Return the file as a response
            return File(excelData, easyHelpType, fileName);
        }
    }
}
