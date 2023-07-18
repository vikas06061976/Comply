 using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc; 
using ComplyExchangeCMS.Domain.Entities.Masters;
using Domain.Services;
using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.Pages;
using ComplyExchangeCMS.Domain.Models.Master;
using ComplyExchangeCMS.Domain;
using System.Threading;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public LanguageController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllLanguage")]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.Languages.GetAllLanguage();
            return Ok(data);
        }

        [HttpGet("LanguageSearch")]
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
            var pages = await unitOfWork.Languages.GetAllAsync
                (request, searchTerm);

            return Ok(pages);
        }

        [HttpPost("InsertLanguage")]
        public async Task<IActionResult> CreateLangugaes(LanguageInsert languageModel)
        {
            await unitOfWork.Languages.InsertLanguage(languageModel);
            return Ok("Language created successfully.");
        }

        [HttpPut("UpdateLanguage")]
        public async Task<IActionResult> UpdateLangugaes(LanguageUpdate languageModel)
        {
            await unitOfWork.Languages.UpdateLanguage(languageModel);
            return Ok("Language updated successfully.");
        }

        [HttpGet("GetLanguageById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Languages.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpDelete("LangugaeDelete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Languages.DeleteLanguage(id);
            return Ok(data);
        }
                                                                                                                                            
    }
}
