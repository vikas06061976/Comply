 using System.Threading.Tasks; 
using Microsoft.AspNetCore.Mvc; 
using ComplyExchangeCMS.Domain.Entities.Masters;
using Domain.Services;
using ComplyExchangeCMS.Domain.Entities;
using ComplyExchangeCMS.Domain.Models.Agent;
using ComplyExchangeCMS.Domain;
using System.Threading;
using System.IO;
using System;
using Microsoft.AspNetCore.Hosting;
using ComplyExchangeCMS.Domain.Models.EasyHelp;

namespace ComplyExchangeCMS.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHostingEnvironment _environment;
        public AgentController(IUnitOfWork unitOfWork, IHostingEnvironment environment)
        {
            this.unitOfWork = unitOfWork;
            _environment = environment;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreateAgent(AgentInsert agents)
        {/*
            #region Logo Image
            if (agents.Logo == null || agents.Logo.Length == 0)
                return BadRequest("No image selected");

            // Generate a unique filename for the uploaded image
            var fileName = Path.GetFileNameWithoutExtension(agents.Logo.FileName);
            var fileExtension = Path.GetExtension(agents.Logo.FileName);
            var uniqueFileName = $"{fileName}_{Path.GetRandomFileName()}{fileExtension}";

            // Save the image to the specified path
            var imagePath = Path.Combine(_environment.ContentRootPath, "AgentImages");
            Directory.CreateDirectory(imagePath);
            var filePath = Path.Combine(imagePath, agents.Logo.FileName);
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await agents.Logo.CopyToAsync(fileStream);
            }

            // Update the LogoPath property with the saved image path
            agents.Logo_ImagePath = fileName;
            #endregion
            */
            await unitOfWork.Agents.Insert(agents);
            return Ok("Agent created successfully.");
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAgent(AgentUpdate agents)
        {
            await unitOfWork.Agents.UpdateAgents(agents);
            return Ok("Agent updated successfully.");
        }

        [HttpGet("GetAllAgents")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var data = await unitOfWork.Agents.GetAllAsync();
        //    return Ok(data);
        //}
        // create method for adding 2 numbers

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
            var pages = await unitOfWork.Agents.GetAllAsync
                (request, searchTerm);

            return Ok(pages);
        }

        [HttpGet("GetAgentById")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await unitOfWork.Agents.GetByIdAsync(id);
            if (data == null) return Ok();
            return Ok(data);
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await unitOfWork.Agents.DeleteAsync(id);
            return Ok(data);
        }

        [HttpGet("GetAllLanguage")]
        public async Task<IActionResult> GetAllLanguage(int agentId)
        {
            var data = await unitOfWork.Agents.GetAllLanguage(agentId);
            return Ok(data);
        }
        [HttpPost("UpsertAgentTranslation")]
        public async Task<IActionResult> InsertAgentTranslation(AgentTranslationUpsert agentModel)
        {
            await unitOfWork.Agents.InsertAgentTranslation(agentModel);
            return Ok("Agent translation updated successfully.");
        }

        [HttpGet("GetAgentTranslation")]
        public async Task<IActionResult> GetAgentTranslation(int agentId, int languageId)
        {
            var data = await unitOfWork.Agents.GetAgentTranslation(agentId, languageId);
            if (data == null) return Ok();
            return Ok(data);
        }
    }
}
