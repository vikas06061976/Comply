 using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc; 
using ComplyExchangeCMS.Domain.Entities.Masters;
using Domain.Services;
using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.Documentation;
using ComplyExchangeCMS.Domain;
using System.Threading;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentationController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public DocumentationController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("GetAllDocumentation")]
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
            var pages = await unitOfWork.Documentation.GetAllAsync
                (request, searchTerm);

            return Ok(pages);
        }

        [HttpPost("InsertDocumentation")]
        public async Task<IActionResult> CreateLangugaes(DocumentationInsert documentationModel)
        {
            await unitOfWork.Documentation.InsertDocument(documentationModel);
            return Ok("Documentation created successfully.");
        }

        [HttpPut("UpdateDocumentation")]
        public async Task<IActionResult> UpdateLangugaes(DocumentationUpdate documentationModel)
        {
            await unitOfWork.Documentation.UpdateDocument(documentationModel);
            return Ok("Documentation updated successfully.");
        }

        [HttpGet("GetDocumentationById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Documentation.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpDelete("DocumentationDelete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Documentation.DeleteDocument(id);
            return Ok(data);
        }

        [HttpGet("GetDocumentationTypes")]
        public async Task<IActionResult> GetDocumentationTypes()
        {
            var data = await unitOfWork.Documentation.GetDocumentTypes();
            if (data == null) return Ok();
            return Ok(data);
        }
    }
}
