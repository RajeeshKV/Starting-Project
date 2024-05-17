using Azure;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Starting_Project.Common;
using Starting_Project.Models;

namespace Starting_Project.Controllers
{
    [ApiController]
    [EnableCors("AllowRoutes")]
    public class ProgramDetailController : ControllerBase
    {
        private readonly Container _containerProgram;

        public ProgramDetailController(DbContext.DbContext dbContext)
        {
            _containerProgram = dbContext.GetContainerAsync<ProgramDetail>().Result;
        }

        [HttpPost]
        [Route(RouteMapConstants.CreateApplicationRoute)]
        public async Task<IActionResult> CreateProgram([FromBody] ProgramDetail program)
        {
            if (string.IsNullOrEmpty(program.ProgramName))
            {
                return BadRequest("Program name is required.");
            }

            // Validate mandatory personal details
            foreach (var detail in program.PersonalDetails)
            {
                if (detail.IsMandatory && string.IsNullOrEmpty(detail.FieldName))
            {
                    return BadRequest($"{detail.FieldName} is mandatory.");
                }
            }
            program.Id = Guid.NewGuid().ToString();

            // Save the program to Cosmos DB
            var response = await _containerProgram.CreateItemAsync(program);

            return Ok(response.Resource);
        }

        [HttpPut]
        [Route(RouteMapConstants.EditApplicationRoute)]
        public async Task<IActionResult> EditApplicationAsync(string id, ProgramDetail updatedProgram)
        {
            if (string.IsNullOrEmpty(updatedProgram.ProgramName))
            {
                return BadRequest("Program name is required.");
            }

            // Validate mandatory personal details
            foreach (var detail in updatedProgram.PersonalDetails)
            {
                if (detail.IsMandatory && string.IsNullOrEmpty(detail.FieldName))
                {
                    return BadRequest($"{detail.FieldName} is mandatory.");
                }
            }

            try
            {
                var existingProgram = await _containerProgram.ReadItemAsync<ProgramDetail>(id, new PartitionKey(id));
                if (existingProgram == null)
                {
                    return NotFound("Program not found.");
                }

                // Update the existing program with new details
                existingProgram.Resource.ProgramName = updatedProgram.ProgramName;
                if (updatedProgram.PersonalDetails != null)
                {
                    foreach (var detailUpdate in updatedProgram.PersonalDetails)
                    {
                        var existingDetail = existingProgram.Resource.PersonalDetails.FirstOrDefault(d => d.FieldName == detailUpdate.FieldName);
                        if (existingDetail != null)
                        {
                            existingDetail.IsMandatory = detailUpdate.IsMandatory; // Update value
                        }
                    }
                }
                //existingProgram.Resource.PersonalDetails = updatedProgram.PersonalDetails;
                existingProgram.Resource.Questions = updatedProgram.Questions;

                // Save the updated program to Cosmos DB
                await _containerProgram.UpsertItemAsync(existingProgram, new PartitionKey(id));

                return NoContent(); // 204 No Content indicates success without returning any content
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }
    }
}
