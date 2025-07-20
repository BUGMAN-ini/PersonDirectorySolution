using PersonDirectory.Domain.Enums;

namespace PersonDirectory.Application.DTOs
{
    public class UpdatePersonDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CityId { get; set; }
        public List<PhoneNumberDTO> PhoneNumbers { get; set; }
    }
}
