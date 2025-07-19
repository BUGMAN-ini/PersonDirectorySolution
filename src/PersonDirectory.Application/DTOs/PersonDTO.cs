using PersonDirectory.Domain.Entity;
using PersonDirectory.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.DTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string PersonalNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public CityDTO City { get; set; }
        public string ImagePath { get; set; }
        public List<PhoneNumberDTO> PhoneNumbers { get; set; }
        public List<RelatedPersonDTO> RelatedPersons { get; set; }
    }
}
