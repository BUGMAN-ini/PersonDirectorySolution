using Microsoft.AspNetCore.Http;
using PersonDirectory.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PersonDirectory.Application.DTOs
{
    public class CreatePersonDTO
    {
        [Required, StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required, StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required]
        public Gender Gender { get; set; } = Gender.Male;

        [Required]
        [RegularExpression(@"^\d{11}$"
                , ErrorMessage = "Must be exactly 11 digits")]
        public string PersonalNumber { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; } = new DateTime(1999, 01, 01);

        [Required]
        public int CityId { get; set; } = 1;

        [Required]
        public List<PhoneNumberDTO> PhoneNumbers { get; set; }

        public List<CreateRelatedPersonDTO> RelatedPersons { get; set; }
    }
}
