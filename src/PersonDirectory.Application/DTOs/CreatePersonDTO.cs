using Microsoft.AspNetCore.Http;
using PersonDirectory.Domain.Enums;

namespace PersonDirectory.Application.DTOs
{
    public class CreatePersonDTO
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public int CityId { get; set; }

        public List<PhoneNumberDTO>? PhoneNumbers { get; set; }
        public List<CreateRelatedPersonDTO>? RelatedPersons { get; set; }

        public IFormFile? ImageFile { get; set; }
    }
}
