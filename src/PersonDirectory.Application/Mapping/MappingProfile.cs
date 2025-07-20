namespace PersonDirectory.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatePersonDTO, Person>();
            CreateMap<PhoneNumber, PhoneNumberDTO>().ReverseMap();
            CreateMap<RelatedPersonDTO, RelatedPerson>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<CityDTO, City>().ReverseMap();
        }
    }
}
