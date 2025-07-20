using Microsoft.AspNetCore.Mvc;
using PersonDirectory.Application.DTOs;
using PersonDirectory.Application.Interfaces.Services;

namespace PersonDirectory.API.Controllers
{
    [Route("persons")]
    [ApiController]
    public class PersonsController(IPersonService person)
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
        public async Task<IActionResult> GetAllAsync()
        {
            var persons = await person.GetAllPersonAsync();
            return Ok(persons);
        }
    }
}
