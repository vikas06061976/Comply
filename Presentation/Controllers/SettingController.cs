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

        [HttpPost("InsertSetting")]
        public async Task<IActionResult> CreateSetting([FromForm]SettingInsertModel settingModel)
        {
            await unitOfWork.SettingService.InsertSetting(settingModel);
            return Ok("Setting created successfully.");
        }

        [HttpGet("GetSetting")]
        public async Task<IActionResult> GetSetting()
        {
            var data = await unitOfWork.SettingService.GetSetting();
            if (data == null) return Ok();
            return Ok(data);
        }

        /*[HttpPost("InsertSettingTranslation")]
        public async Task<IActionResult> InsertSettingTranslation(SettingInsertTranslation settingModel)
        {
            await unitOfWork.SettingService.InsertSettingTranslation(settingModel);
            return Ok("Setting Translation updated successfully.");
        }

        [HttpGet("GetSettingTranslation")]
        public async Task<IActionResult> GetSettingTranslation(int settingId, int languageId)
        {
            var data = await unitOfWork.SettingService.GetSettingTranslation(settingId, languageId);
            if (data == null) return Ok();
            return Ok(data);
        }*/
    }
}
