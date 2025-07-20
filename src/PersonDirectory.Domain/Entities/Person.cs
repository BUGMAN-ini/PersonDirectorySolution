using PersonDirectory.Domain.Enums;

namespace PersonDirectory.Domain.Entity
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int CityId { get; set; }
        public City City { get; set; }
        public string ImagePath { get; set; }

        public ICollection<PhoneNumber> PhoneNumbers { get; set; } = new List<PhoneNumber>();
        public ICollection<RelatedPerson> RelatedPersons { get; set; } = new List<RelatedPerson>();
        public ICollection<RelatedPerson> RelatedToPersons { get; set; } = new List<RelatedPerson>();

    }
}
