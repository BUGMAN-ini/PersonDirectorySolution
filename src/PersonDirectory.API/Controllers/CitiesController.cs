using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonDirectory.Application.DTOs;
using PersonDirectory.Application.Interfaces.Repositories;
using PersonDirectory.Application.Interfaces.Services;

namespace PersonDirectory.API.Controllers
{
    [Route("cities")]
    [ApiController]
    public class CitiesController(ICityService service)
        : ControllerBase
    {
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedRequestAll request)
        {
            var cities = await service.GetAllCitiesAsync(request);
            return Ok(cities);
        }

        [HttpGet("get-by/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var city = await service.GetCityByIdAsync(id);
            return Ok(city);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateAsync([FromForm] CreateCityDTO dto)
        {
            var city = await service.CreateCityAsync(dto);
            return Ok(city);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] CreateCityDTO dto)
        {
            var updatedCity = await service.UpdateCityAsync(id, dto);
            return Ok(updatedCity);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await service.DeleteCityAsync(id);
            return Ok(new { message = $"City with id {id} deleted successfully." });
        }
    }
}
