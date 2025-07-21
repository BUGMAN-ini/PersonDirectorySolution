using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonDirectory.Application.DTOs;
using PersonDirectory.Application.Interfaces.Services;

namespace PersonDirectory.API.Controllers
{
    [Route("phone")]
    [ApiController]
    public class PhoneNumberController(IPhoneNumberService service)
        : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] PhoneNumberDTO dto)
        {
            var phoneNumber = await service.CreateNumberAsync(dto);
            return Ok(phoneNumber);
        }

        [HttpGet("get-by/{personid}")]
        public async Task<IActionResult> GetbyPerson(int personid)
        {
            var phoneNumber = await service.GetNumberByPersonId(personid);
            if (phoneNumber == null)
            {
                return NotFound();
            }
            return Ok(phoneNumber);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery] PaginatedRequestAll request)
        {
            var phoneNumbers = await service.GetAllAsync(request);
            return Ok(phoneNumbers);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await service.DeletePersonAsync(id);
            return Ok(new { message = $"Phone number with id {id} deleted successfully." });
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdatePhoneNumberByPersonId(int id, [FromBody] PhoneNumberDTO dto)
        {
            var updatedPhoneNumber = await service.Update(id, dto);
            return Ok(updatedPhoneNumber);
        }
    }
}
