using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.EasyHelp;
using ComplyExchangeCMS.Domain.Models.FormTypes;
using ComplyExchangeCMS.Domain.Models.Pages;
using Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormTypesController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHostingEnvironment _environment;

        public FormTypesController(IUnitOfWork unitOfWork, IHostingEnvironment environment)
        {
            this.unitOfWork = unitOfWork;
            _environment = environment;
        }

        [HttpPost("InsertSC")]
        public async Task<IActionResult> CreateFormType([FromForm] FormTypesInsert formTypesModel)
        {
            if (formTypesModel.Logo == null || formTypesModel.Logo.Length == 0)
                return BadRequest("No image selected");

            // Generate a unique filename for the uploaded image
            var fileName = Path.GetFileNameWithoutExtension(formTypesModel.Logo.FileName);
            var fileExtension = Path.GetExtension(formTypesModel.Logo.FileName);
            var uniqueFileName = $"{fileName}_{Path.GetRandomFileName()}{fileExtension}";

            // Save the image to the specified path
            var imagePath = Path.Combine(_environment.ContentRootPath, "FormTypeImages");
            var subfolderName = "SelfCertificationImages";
            var subfolderPath = Path.Combine(imagePath, subfolderName);
            Directory.CreateDirectory(subfolderPath);
            var filePath = Path.Combine(imagePath, formTypesModel.Logo.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formTypesModel.Logo.CopyToAsync(fileStream);
            }

            // Update the LogoPath property with the saved image path
            formTypesModel.LogoPath = fileName;
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

        [HttpGet("GetAllSelfLanguage")]
        public async Task<IActionResult> GetAllLanguage(int scFormId)
        {
            var data = await unitOfWork.FormTypes.GetAllLanguage(scFormId);
            return Ok(data);
        }

        #region Form Type US Certification


        [HttpPut("UpdateUSC")]
        public async Task<IActionResult> UpdateUSFormType([FromForm] FormTypesUSCertiUpdate formTypesModel)
        {
            #region PrintTemplatePDF Images
            if(formTypesModel.PrintTemplatePDFId== Common.Enums.Logo.Upload)
            {
                if (formTypesModel.PrintTemplatePDF == null || formTypesModel.PrintTemplatePDF.Length == 0)
                    return BadRequest("Please upload print template pdf");

                // Generate a unique filename for the uploaded image
                var fileName = Path.GetFileNameWithoutExtension(formTypesModel.PrintTemplatePDF.FileName);
                var fileExtension = Path.GetExtension(formTypesModel.PrintTemplatePDF.FileName);
                var uniqueFileName = $"{fileName}_{Path.GetRandomFileName()}{fileExtension}";

                // Save the image to the specified path
                var imagePath = Path.Combine(_environment.ContentRootPath, "FormTypeImages");
                var subfolderName = "PrintTempUSCertificationImages";
                var subfolderPath = Path.Combine(imagePath, subfolderName);
                Directory.CreateDirectory(subfolderPath);

                var filePath = Path.Combine(subfolderPath, formTypesModel.PrintTemplatePDF.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await formTypesModel.PrintTemplatePDF.CopyToAsync(fileStream);
                }

                // Update the LogoPath property with the saved image path
                formTypesModel.PrintTemplatePDF_ImagePath = fileName;

            }
            #endregion

            #region ESubmitTemplatePDF Images
            if (formTypesModel.ESubmitTemplatePDFId == Common.Enums.Logo.Upload)
            {
                if (formTypesModel.ESubmitTemplatePDF == null || formTypesModel.ESubmitTemplatePDF.Length == 0)
                    return BadRequest("No image selected");

                // Generate a unique filename for the uploaded image
                var ESubfileName = Path.GetFileNameWithoutExtension(formTypesModel.ESubmitTemplatePDF.FileName);
                var ESubfileExtension = Path.GetExtension(formTypesModel.ESubmitTemplatePDF.FileName);
                var ESubuniqueFileName = $"{ESubfileName}_{Path.GetRandomFileName()}{ESubfileExtension}";

                // Save the image to the specified path
                var ESubimagePath = Path.Combine(_environment.ContentRootPath, "FormTypeImages");
                var EsubfolderName = "EsubmitUSCertificationImages";
                var EsubfolderPath = Path.Combine(ESubimagePath, EsubfolderName);
                Directory.CreateDirectory(EsubfolderPath);

                var ESubfilePath = Path.Combine(EsubfolderPath, formTypesModel.ESubmitTemplatePDF.FileName);
                using (var fileStream = new FileStream(ESubfilePath, FileMode.Create))
                {
                    await formTypesModel.PrintTemplatePDF.CopyToAsync(fileStream);
                }

                // Update the LogoPath property with the saved image path
                formTypesModel.ESubmitTemplatePDF_ImagePath = ESubfileName;
            }
            #endregion
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

        [HttpGet("GetAllUSLanguage")]
        public async Task<IActionResult> GetAllUSLanguage(int usFormId)
        {
            var data = await unitOfWork.FormTypes.GetAllUSLanguage(usFormId);
            return Ok(data);
        }

        #endregion
    }
}
