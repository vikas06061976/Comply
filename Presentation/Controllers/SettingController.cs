using ComplyExchangeCMS.Domain.Models.Settings;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public SettingController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpPost("UpsertSetting")]
        public async Task<IActionResult> CreateSetting([FromForm]SettingInsertModel settingModel)
        {
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

        [HttpPost("InsertQuestionTranslation")]
        public async Task<IActionResult> InsertQuestionTranslation(QuestionTranslationInsert questionModel)
        {
            await unitOfWork.SettingService.InsertQuestionTranslation(questionModel);
            return Ok("Question Translation updated successfully.");
        }

        [HttpGet("GetQuestionTranslation")]
        public async Task<IActionResult> GetQuestionTranslation(int? questionId, int? questionHintId, int languageId)
        {
            var data = await unitOfWork.SettingService.GetQuestionTranslation(questionId, questionHintId, languageId);
            if (data == null) return Ok();
            return Ok(data);
        }
    }
}
