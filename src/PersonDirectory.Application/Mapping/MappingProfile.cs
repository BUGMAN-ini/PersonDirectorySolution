namespace PersonDirectory.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatePersonDTO, Person>().ReverseMap();
            CreateMap<PhoneNumber, PhoneNumberDTO>().ReverseMap();
            CreateMap<RelatedPersonDTO, RelatedPerson>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
        }
    }
}
