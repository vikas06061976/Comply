using ComplyExchangeCMS.Domain.Models.ContentBlock;
using ComplyExchangeCMS.Domain.Models.ContentManagement;
using Domain.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentManagementController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHostingEnvironment webHostEnvironment;
        public ContentManagementController(IUnitOfWork unitOfWork, IHostingEnvironment webHostEnvironment )
        {
            this.unitOfWork = unitOfWork;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpPost("Import")]
        public IActionResult CreateContent(IFormFile formFile)
        {
            try
            {
                unitOfWork.ContentManagement.UploadFile(formFile);
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
            byte[] excelData = unitOfWork.ContentManagement.GenerateExcelFile();

            // Set the file name and content type
            string fileName = "ContentBlock.xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            // Save the Excel file to the wwwroot folder
            string webRootPath = webHostEnvironment.WebRootPath;
            string filePath = Path.Combine(webRootPath, fileName);
            System.IO.File.WriteAllBytes(filePath, excelData);

            // Return the file as a response
            return File(excelData, contentType, fileName);
        }
        //public IActionResult Download()
        //{
        //    try
        //    {
        //        var (fileType, archiveData, archiveName) = unitOfWork.ContentManagement.DownloadFiles();
        //        return File(archiveData, fileType, archiveName);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

        [HttpGet("GetAllContent")]
        public async Task<IActionResult> GetAll()
        {
            var data = await unitOfWork.ContentManagement.GetAllContent();
            return Ok(data);
        }

        [HttpGet("GetContentById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.ContentManagement.GetContentById(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        //[HttpDelete]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var data = await unitOfWork.ContentManagement.DeleteAsync(id);
        //    return Ok(data);
        //}

        [HttpPut("Update")]
        public async Task<IActionResult> Update(ContentManagementUpdate contentBlock)
        {
            var data = await unitOfWork.ContentManagement.UpdateContent(contentBlock);
            return Ok(data);
        }

        [HttpPost("InsertContentTranslation")]
        public async Task<IActionResult> InsertContentTranslation(ContentManagementLanguageInsert contentModel)
        {
            await unitOfWork.ContentManagement.InsertContentTranslation(contentModel);
            return Ok("Content translation updated successfully.");
        }

        [HttpGet("GetContentTranslation")]
        public async Task<IActionResult> GetContentTranslation(int contentId, int languageId)
        {
            var data = await unitOfWork.ContentManagement.GetContentTranslation(contentId, languageId);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpGet("GetAllLanguage")]
        public async Task<IActionResult> GetAllLanguage(int contentId)
        {
            var data = await unitOfWork.ContentManagement.GetAllLanguage(contentId);
            return Ok(data);
        }
    }
}
