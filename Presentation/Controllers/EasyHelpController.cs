using ComplyExchangeCMS.Domain;
using ComplyExchangeCMS.Domain.Models.EasyHelp;
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
    public class EasyHelpController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHostingEnvironment webHostEnvironment;

        public EasyHelpController(IUnitOfWork unitOfWork, IHostingEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
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
            //check id is exist or not
            var data = await unitOfWork.EasyHelpService.GetByIdAsync(easyHelpModel.Id);
            if (data == null) 
            return Ok("EasyHelp not found.");
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
        [HttpPost("UpsertEasyHelpTranslation")]
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

        [HttpGet("GetAllLanguage")]
        public async Task<IActionResult> GetAllLanguage(int easyHelpId)
        {
            var data = await unitOfWork.EasyHelpService.GetAllLanguage(easyHelpId);
            return Ok(data);
        }

        [HttpPost("Import")]
        public IActionResult CreateEasyHelp(IFormFile formFile)
        {
            try
            {
                unitOfWork.EasyHelpService.UploadFile(formFile);
                return Ok("File Uploaded successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("Export")]
        public IActionResult DownloadExcel()
        {
            byte[] excelData = unitOfWork.EasyHelpService.GenerateExcelFile();

            // Set the file name and content type
            string fileName = "EasyHelp.xlsx";
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
