namespace PersonDirectory.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CityDTO, City>().ReverseMap();

            CreateMap<PhoneNumber, PhoneNumberDTO>().ReverseMap();

            CreateMap<RelatedPerson, RelatedPersonDTO>()
                .ForMember(d => d.RelatedToPersonId,
                           opt => opt.MapFrom(s => s.RelatedToPersonId))
                .ReverseMap()
                .ForMember(r => r.Person, opt => opt.Ignore())
                .ForMember(r => r.RelatedToPerson, opt => opt.Ignore());

            CreateMap<UpdatePersonDTO, Person>()
                .ForMember(p => p.ImagePath, opt => opt.Ignore());

            CreateMap<Person, PersonDTO>()
                .ForMember(d => d.CityId, opt => opt.MapFrom(s => s.City.Id))
                .ForMember(d => d.PhoneNumbers, opt => opt.MapFrom(s => s.PhoneNumbers))
                .ForMember(d => d.RelatedPersons, opt => opt.MapFrom(s => s.RelatedPersons));
                    
        }
    }
}
