using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.FormTypes;
using ComplyExchangeCMS.Domain.Models.Pages;
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

        [HttpPost("InsertFormTypeSelfCertiTranslation")]
        public async Task<IActionResult> InsertFormTypeSelfCertiTranslation(FormTypeSelfCertiTranslationInsert formTypeSCModel)
        {
            await unitOfWork.FormTypes.InsertFormTypeSelfCertiTranslation(formTypeSCModel);
            return Ok("FormType Self Certificate Translation updated successfully.");
        }

        [HttpGet("GetFormTypeSelfCertiTranslation")]
        public async Task<IActionResult> GetFormTypeSelfCertiTranslation(int formTypeId, int languageId)
        {
            var data = await unitOfWork.FormTypes.GetFormTypeSCTranslation(formTypeId, languageId);
            if (data == null) return Ok();
            return Ok(data);
        }

        #region Form Type US Certification


        [HttpPut("UpdateUSC")]
        public async Task<IActionResult> UpdateUSFormType([FromForm] FormTypesUSCertiUpdate formTypesModel)
        {
            await unitOfWork.FormTypes.UpdateUSCertificate(formTypesModel);
            return Ok("Form types (United States Certificates) updated successfully.");
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

        [HttpPost("InsertFormTypeUSCTranslation")]
        public async Task<IActionResult> InsertFormTypeUSCTranslation(FormTypesUSCTranslationInsert formTypeUSCModel)
        {
            await unitOfWork.FormTypes.InsertFormTypeUSCTranslation(formTypeUSCModel);
            return Ok("FormType US Certificate Translation updated successfully.");
        }

        [HttpGet("GetFormTypeUSCTranslation")]
        public async Task<IActionResult> GetFormTypeUSCTranslation(int formTypeId, int languageId)
        {
            var data = await unitOfWork.FormTypes.GetFormTypeUSCTranslation(formTypeId, languageId);
            if (data == null) return Ok();
            return Ok(data);
        }

        #endregion
    }
}
