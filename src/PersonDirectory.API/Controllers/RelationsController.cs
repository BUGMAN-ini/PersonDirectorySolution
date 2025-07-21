using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonDirectory.Application.DTOs;
using PersonDirectory.Application.Interfaces.Services;

namespace PersonDirectory.API.Controllers
{
    [Route("relations")]
    [ApiController]
    public class RelationsController(IRelatedPersonService service)
        : ControllerBase
    {

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRelatedPersonDTO dto)
        {
            var relatedPerson = await service.AddRelation(dto);
            return Ok(relatedPerson);
        }

        [HttpGet("get-by/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var relatedPerson = await service.GetRelationById(id);
            if (relatedPerson == null)
            {
                return NotFound();
            }
            return Ok(relatedPerson);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await service.DeleteRelation(id);
            return Ok(new { message = $"Related person with id {id} deleted successfully." });
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedRequestAll request)
        {
            var relatedPersons = await service.GetAllRelationsAsync(request);
            return Ok(relatedPersons);
        }
    }
}
