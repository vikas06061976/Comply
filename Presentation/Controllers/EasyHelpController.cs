using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.EasyHelp;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EasyHelpController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public EasyHelpController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllEasyHelp")]
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
            var pages = await unitOfWork.EasyHelpService.GetAllAsync
                (request, searchTerm);

            return Ok(pages);
        }

        [HttpPost("InsertEasyHelp")]
        public async Task<IActionResult> CreateEasyHelp(EasyHelpInsert easyHelpModel)
        {
            await unitOfWork.EasyHelpService.InsertEasyHelp(easyHelpModel);
            return Ok("EasyHelp created successfully.");
        }

        [HttpPut("UpdateEasyHelp")]
        public async Task<IActionResult> UpdateEasyHelp(EasyHelpUpdate easyHelpModel)
        {
            await unitOfWork.EasyHelpService.UpdateEasyHelp(easyHelpModel);
            return Ok("EasyHelp updated successfully.");
        }

        [HttpGet("GetEasyHelpById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.EasyHelpService.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpDelete("EasyHelpDelete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.EasyHelpService.DeleteEasyHelp(id);
            return Ok(data);
        }
        [HttpPost("InsertEasyHelpTranslation")]
        public async Task<IActionResult> InsertEasyHelpTranslation(EasyHelpTranslation easyHelpModel)
        {
            await unitOfWork.EasyHelpService.InsertEasyHelpTranslation(easyHelpModel);
            return Ok("EasyHelp translation updated successfully.");
        }

        [HttpGet("GetEasyHelpTranslation")]
        public async Task<IActionResult> GetEasyHelpTranslation(int easyHelpId, int languageId)
        {
            var data = await unitOfWork.EasyHelpService.GetEasyHelpTranslation(easyHelpId, languageId);
            if (data == null) return Ok();
            return Ok(data);
        }
    }
}
