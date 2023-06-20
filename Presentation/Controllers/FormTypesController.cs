using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.FormTypes;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormTypesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public FormTypesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("InsertSC")]
        public async Task<IActionResult> CreateFormType([FromForm] FormTypesInsert formTypesModel)
        {
            await unitOfWork.FormTypes.Insert(formTypesModel);
            return Ok("FormType is created successfully.");
        }

        [HttpPut("UpdateSC")]
        public async Task<IActionResult> UpdateFormType([FromForm] FormTypesUpdate formTypesModel)
        {
            await unitOfWork.FormTypes.Update(formTypesModel);
            return Ok("FormType updated successfully.");
        }
       
        [HttpGet("GetAllFormTypes")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var data = await unitOfWork.FormTypes.GetAllAsync();
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
            var pages = await unitOfWork.FormTypes.GetAllAsync
                (request, searchTerm);

            return Ok(pages);
        }

        [HttpGet("GetFormTypeById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.FormTypes.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        #region Form Type US Certification


        [HttpPut("UpdateUSC")]
        public async Task<IActionResult> UpdateUSFormType([FromForm] FormTypesUpdate formTypesModel)
        {
            await unitOfWork.FormTypes.Update(formTypesModel);
            return Ok("FormType updated successfully.");
        }

        [HttpGet("GetAllUSFormTypes")]
        public async Task<IActionResult> GetAllUSFormType()
        {
            var data = await unitOfWork.FormTypes.GetAllUSCertificate();
            return Ok(data);
        }

        [HttpGet("GetUSFormTypeId")]
        public async Task<IActionResult> GetByIdUSFormType(int id)
        {
            var data = await unitOfWork.FormTypes.GetByIdUSCertificate(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        #endregion
    }
}
