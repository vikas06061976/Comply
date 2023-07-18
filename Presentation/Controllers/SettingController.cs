using ComplyExchangeCMS.Domain.Models.Settings;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using static ComplyExchangeCMS.Common.Enums;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHostingEnvironment _environment;
        public SettingController(IUnitOfWork unitOfWork, IHostingEnvironment environment)
        {
            this.unitOfWork = unitOfWork;
            _environment = environment;
        }

        [HttpPost("UpsertSetting")]
        public async Task<IActionResult> CreateSetting([FromForm]SettingInsertModel settingModel)
        {
            #region DefaultCoverPage Image
            if (settingModel.DefaultCoverPagePdf == null || settingModel.DefaultCoverPagePdf.Length == 0)
                return BadRequest("No image selected");

            // Generate a unique filename for the uploaded image
            var fileName = Path.GetFileNameWithoutExtension(settingModel.DefaultCoverPagePdf.FileName);
            var fileExtension = Path.GetExtension(settingModel.DefaultCoverPagePdf.FileName);
            var uniqueFileName = $"{fileName}_{Path.GetRandomFileName()}{fileExtension}";

            // Save the image to the specified path
            var imagePath = Path.Combine(_environment.ContentRootPath, "SettingImages");
            Directory.CreateDirectory(imagePath);
            var filePath = Path.Combine(imagePath, settingModel.DefaultCoverPagePdf.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await settingModel.DefaultCoverPagePdf.CopyToAsync(fileStream);
            }

            // Update the LogoPath property with the saved image path
            settingModel.DefaultCoverPagePdf_FileName = fileName;
            #endregion

            #region DefaultLogo Image

            if (settingModel.DefaultLogoType == Logo.Upload)
            { 
                if (settingModel.DefaultLogo == null || settingModel.DefaultLogo.Length == 0)
                    return BadRequest("Please upload data on default logo.");

                // Generate a unique filename for the uploaded image
                var logofileName = Path.GetFileNameWithoutExtension(settingModel.DefaultLogo.FileName);
                var logofileExtension = Path.GetExtension(settingModel.DefaultLogo.FileName);
                var logouniqueFileName = $"{logofileName}_{Path.GetRandomFileName()}{logofileExtension}";

                // Save the image to the specified path
                var logoimagePath = Path.Combine(_environment.ContentRootPath, "SettingImages");
                var subfolderName = "LogoImages";
                var subfolderPath = Path.Combine(logoimagePath, subfolderName);
                Directory.CreateDirectory(subfolderPath);

                var logofilePath = Path.Combine(subfolderPath, settingModel.DefaultLogo.FileName);
                using (var fileStream = new FileStream(logofilePath, FileMode.Create))
                {
                    await settingModel.DefaultLogo.CopyToAsync(fileStream);
                }

                // Update the LogoPath property with the saved image path
                settingModel.DefaultLogo_FileName = logofileName;
            }
            #endregion

            await unitOfWork.SettingService.UpsertSetting(settingModel);
            return Ok("Setting updated successfully.");
        }

        [HttpGet("GetSetting")]
        public async Task<IActionResult> GetSetting()
        {
            var data = await unitOfWork.SettingService.GetSetting();
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpGet("GetQuestion")]
        public async Task<IActionResult> GetQuestion()
        {
            var data = await unitOfWork.SettingService.GetQuestions();
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpGet("GetQuestionsById")]
        public async Task<IActionResult> GetQuestionsById(int questionId)
        {
            var data = await unitOfWork.SettingService.GetByQuestionId(questionId);
            return Ok(data);
        }

        [HttpPost("UpdateQuestion")]
        public async Task<IActionResult> UpdateQuestion(QuestionView questionModel)
        {
            await unitOfWork.SettingService.UpdateQuestion(questionModel);
            return Ok("Question updated successfully.");
        }

        [HttpPost("UpsertQuestionTranslation")]
        public async Task<IActionResult> InsertQuestionTranslation(QuestionTranslationInsert questionModel)
        {
            await unitOfWork.SettingService.InsertQuestionTranslation(questionModel);
            return Ok("Question Translation updated successfully.");
        }

        [HttpGet("GetQuestionTranslation")]
        public async Task<IActionResult> GetQuestionTranslation(int? questionId, int languageId)
        {
            var data = await unitOfWork.SettingService.GetQuestionTranslation(questionId, languageId);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpGet("GetQuestionHintTranslation")]
        public async Task<IActionResult> GetQuestionHintTranslation(int? questionHintId, int languageId)
        {
            var data = await unitOfWork.SettingService.GetQuestionHintTranslation(questionHintId, languageId);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpGet("GetAllLanguage")]
        public async Task<IActionResult> GetAllLanguage(int questionId)
        {
            var data = await unitOfWork.SettingService.GetAllQuestionLanguage(questionId);
            return Ok(data);
        }

        [HttpGet("GetAllHintLanguage")]
        public async Task<IActionResult> GetAllHintLanguage(int questionHintId)
        {
            var data = await unitOfWork.SettingService.GetAllQuestionHintLanguage(questionHintId);
            return Ok(data);
        }
    }
}
