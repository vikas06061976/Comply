 using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc; 
using ComplyExchangeCMS.Domain.Entities.Masters;
using Domain.Services;
using ComplyExchangeCMS.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System;
using ComplyExchangeCMS.Domain.Models.Pages;
using ComplyExchangeCMS.Domain;
using System.Data;
using System.Linq;
using System.Threading;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public PageController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("InsertPage")]
        public async Task<IActionResult> CreatePages(PageInsertModel pagesModel)
        {
            await unitOfWork.Pages.Insert(pagesModel);
            return Ok("Page created successfully.");
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePages(PageUpdateModel pagesModel)
        {
            await unitOfWork.Pages.UpdatePages(pagesModel);
            return Ok("Page updated successfully.");
        }

        [HttpGet("GetAllPages")]
        public async Task<IActionResult> GetAll (string searchTerm, int pageNumber, int pageSize, string sortColumn,
          string sortDirection,CancellationToken cancellationToken = default)
          {
              var request = new PaginationRequest
              {
                  PageNumber = pageNumber,
                  PageSize = pageSize,
                  SortColumn = sortColumn,
                  SortDirection = sortDirection
              };
              var pages = await unitOfWork.Pages.GetAllAsync
                  (request, searchTerm);

              return Ok(pages);
          }
          //public async Task<IActionResult> GetAll()
          //{
          //    var data = await unitOfWork.Pages.GetAllAsync();
          //    return Ok(data);
          //}

        [HttpGet("GetPageById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Pages.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Pages.DeleteAsync(id);
            return Ok(data);
        }

        [HttpGet("GetPageByName")]
        public async Task<IActionResult> GetPageByName(string name)
        {
            var data = await unitOfWork.Pages.GetByNameAsync(name);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpGet("ParentDopDown")]
        public async Task<IActionResult> GetPageByParentId()
        {
            var data = await unitOfWork.Pages.GetByParentIdAsync();
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpPost("InsertSubPages")]
        public async Task<IActionResult> CreateSubPages(PageInsertModel pagesModel)
        {
            await unitOfWork.Pages.InsertSubPage(pagesModel);
            return Ok("Sub-Page created successfully.");
        }


        [HttpGet("GetCountBySubPageId")]
        public async Task<IActionResult> GetCountBySubPageId(int Id)
        {
            var data = await unitOfWork.Pages.GetCountBySubPageIdAsync(Id);
            if (data == 0) return Ok();
            return Ok(data);
        }
        [HttpPost("InsertTranslation")]
        public async Task<IActionResult> InsertTranslation(PageTranslationInsert pagesModel)
        {
            await unitOfWork.Pages.InsertPageTranslation(pagesModel);
            return Ok("Page Translation updated successfully.");
        }

        [HttpGet("GetPageTranslation")]
        public async Task<IActionResult> GetPageTranslation(int pageId, int languageId)
        {
            var data = await unitOfWork.Pages.GetPageTranslation(pageId, languageId);
            if (data == null) return Ok();
            return Ok(data);
        }
    }
}
