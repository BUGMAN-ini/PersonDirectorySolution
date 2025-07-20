namespace PersonDirectory.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatePersonDTO, Person>()
                .ForMember(p => p.ImagePath, opt => opt.Ignore());

            CreateMap<UpdatePersonDTO, Person>()
                .ForMember(p => p.ImagePath, opt => opt.Ignore());

            CreateMap<Person, PersonDTO>()
                .ForMember(d => d.City, opt => opt.MapFrom(s => s.City))
                .ForMember(d => d.PhoneNumbers, opt => opt.MapFrom(s => s.PhoneNumbers))
                .ForMember(d => d.RelatedPersons, opt => opt.MapFrom(s => s.RelatedPersons));

            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<PhoneNumber, PhoneNumberDTO>().ReverseMap();
            CreateMap<RelatedPerson, RelatedPersonDTO>()
                .ReverseMap()
                .ForMember(r => r.Person, opt => opt.Ignore())
                .ForMember(r => r.RelatedToPerson, opt => opt.Ignore());
        }
    }
}
