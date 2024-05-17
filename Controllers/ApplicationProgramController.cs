using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Starting_Project.Common;
using Starting_Project.Models;
using System.Net;

namespace Starting_Project.Controllers
{
    [ApiController]
    [EnableCors("AllowRoutes")]
    public class ApplicationProgramController : ControllerBase
    {
        private readonly Container _containerProgram;
        private readonly Container _containerApplication;

        public ApplicationProgramController(DbContext.DbContext dbContext)
        {
            _containerProgram = dbContext.GetContainerAsync<ProgramDetail>().Result;
            _containerApplication = dbContext.GetContainerAsync<ApplicationProgram>().Result;
        }

        [HttpPost]
        [Route(RouteMapConstants.SubmitApplicationRoute)]
        public async Task<IActionResult> SubmitApplicationAsync(ApplicationProgram application)
        {
            if (application == null)
            {
                return BadRequest("Invalid request payload.");
            }
            try
            {
                var response = await _containerApplication.CreateItemAsync(application);
                return Ok(response.Resource);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.Conflict)
            {
                return Conflict($"An item with the same id already exists: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error has occurred: {ex.Message}");
            }
        }

        [HttpGet]
        [Route(RouteMapConstants.GetApplicationRoute)]
        public async Task<IActionResult> GetApplicationAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Application ID is required.");
            }

            try
            {
                var response = await _containerProgram.ReadItemAsync<ProgramDetail>(id, new PartitionKey(id));
                
                var programData = new ProgramDetail
                {
                    ProgramName = response.Resource.ProgramName,
                    ProgramId  = response.Resource.Id,
                    PersonalDetails = response.Resource.PersonalDetails.Where(f => f.IsMandatory).ToList(),
                    Questions = response.Resource.Questions.Select(q => new Question
                    {
                        Type = q.Type,
                        QuestionText = q.QuestionText,
                        DropdownChoices = q.DropdownChoices // Include options only for relevant question types
                    }).ToList()
                };


                return Ok(programData);
            }
            catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
            {
                return NotFound($"Application with ID {id} not found.");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error has occurred: {ex.Message}");
            }
        }
    }
}