using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PersonDirectory.Application.DTOs;
using PersonDirectory.Application.Interfaces.Services;
using PersonDirectory.Application.Services;

namespace PersonDirectory.API.Controllers
{
    [Route("persons")]
    [ApiController]
    public class PersonsController(IPersonService person,HealthCheckService healthCheck)
        : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> CreatePersonAsync([FromBody] CreatePersonDTO dto)
        {
            var personDto = await person.CreatePersonAsync(dto);
            return Ok(personDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var personDto = await person.GetById(id);
            return Ok(personDto);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedRequestAll request)
        {
            var persons = await person.GetAllPersonAsync(request);
            return Ok(persons);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchAsync([FromQuery] PersonSearchRequestDTO request)
        {
            var result = await person.SearchAsync(request);
            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePersonAsync(int id)
        {
            await person.DeletePersonAsync(id);
            return Ok(new { message = $"Person with id {id} deleted successfully." });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePersonAsync(int id, [FromForm] UpdatePersonDTO dto)
        {
            var updatedPerson = await person.UpdatePersonAsync(id, dto);
            return Ok(updatedPerson);
        }

        [HttpGet("relation-report")]
        public async Task<IActionResult> GetRelationReportAsync()
        {
            var report = await person.GetRelationReportAsync();
            return Ok(report);
        }

        [HttpGet("health")]
        public async Task<IActionResult> Health()
        {
            var report = await healthCheck.CheckHealthAsync();

            var result = new
            {
                status = report.Status.ToString(),
                results = report.Entries.Select(entry => new
                {
                    key = entry.Key,
                    status = entry.Value.Status.ToString(),
                    description = entry.Value.Description
                })
            };

            return Ok(result);
        }
    }
}
