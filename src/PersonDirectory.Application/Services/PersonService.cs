using AutoMapper.QueryableExtensions;
using PersonDirectory.Application.Interfaces.Repositories;
using PersonDirectory.Infrastructure.Repositories.Interfaces;
using System.Linq.Expressions;

namespace PersonDirectory.Application.Services
{
    public class PersonService
        (IUnitOfWork unitofwork, IMapper mapper) : IPersonService
    {
        private readonly string _imageRoot = Path.Combine("wwwroot", "images");

        public async Task<PersonDTO> CreatePersonAsync(CreatePersonDTO dto)
        {
            var person = mapper.Map<Person>(dto);

            if (dto.PhoneNumbers is not null && dto.PhoneNumbers.Count > 0)
                person.PhoneNumbers = mapper.Map<List<PhoneNumber>>(dto.PhoneNumbers);

            if (dto.RelatedPersons is not null && dto.RelatedPersons.Count > 0)
            {
                person.RelatedPersons = dto.RelatedPersons.Select(r => new RelatedPerson
                {
                    RelationType = r.RelationType,
                    RelatedToPersonId = r.RelatedToPersonId
                }).ToList();
            }
            if (dto.ImageFile is not null && dto.ImageFile.Length > 0)
            {
                Directory.CreateDirectory(_imageRoot);

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.ImageFile.FileName)}";
                var fullPath = Path.Combine(_imageRoot, fileName);

                await using var fs = new FileStream(fullPath, FileMode.Create);
                await dto.ImageFile.CopyToAsync(fs);

                person.ImagePath = Path.Combine("images", fileName).Replace("\\", "/");
            }
            else
            {
                person.ImagePath = "images/default.png";
            }

            await unitofwork.Person.AddAsync(person);
            await unitofwork.SaveChangesAsync();

            var fullPerson = await unitofwork.Person.GetByIdDetailAsync(person.Id);

            return mapper.Map<PersonDTO>(fullPerson);
        }

        public async Task DeletePersonAsync(int id)
        {
            var person = await unitofwork.Person.GetByIdAsync(id);
            if (person == null)
            {
                throw new Exception($"Person with id {id} not found.");
            }
            unitofwork.Person.Remove(person);
            await unitofwork.SaveChangesAsync();

        }

        public async Task<PagedResult<PersonDTO>> GetAllPersonAsync(PaginatedRequestAll request)
        {
            var query = unitofwork.Person.Query();
            var totalcount = query.Count();

            var pagedPersons = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var personDTOs = mapper.Map<IEnumerable<PersonDTO>>(pagedPersons);

            return new PagedResult<PersonDTO>(personDTOs, totalcount);
        }

        public async Task<PersonDTO> GetById(int id)
        {
            var person = await unitofwork.Person.GetByIdAsync(id);
            if (person == null)
            {
                throw new Exception($"Person with id {id} not found.");
            }
            return mapper.Map<PersonDTO>(person);

        }

        public async Task<IEnumerable<RelatedPersonReportDTO>> GetRelationReportAsync()
        {
            var persons = await unitofwork.Person.GetAllWithRelationsAsync();

            var result = persons.Select(p => new RelatedPersonReportDTO
            {
                PersonId = p.Id,
                FullName = $"{p.FirstName} {p.LastName}",
                RelationCounts = p.RelatedPersons
                    .GroupBy(rp => rp.RelationType.ToString())
                    .ToDictionary(g => g.Key, g => g.Count())
            });

            return result;
        }

        public async Task<bool> PinExistsAsync(string personalNumber)
        {
            if (await unitofwork.Person.PinExistsAsync(personalNumber));
                throw new Exception($"Personal number {personalNumber} already exists.");
        }

        public async Task<PagedResult<PersonDTO>> SearchAsync(PersonSearchRequestDTO request)
        {
            var query = unitofwork.Person.Query();

            if (!string.IsNullOrEmpty(request.FirstName))
                query = query.Where(p => p.FirstName.Contains(request.FirstName));

            if (!string.IsNullOrEmpty(request.LastName))
                query = query.Where(p => p.LastName.Contains(request.LastName));

            if (!string.IsNullOrEmpty(request.PersonalNumber))
                query = query.Where(p => p.PersonalNumber.Contains(request.PersonalNumber));

            var totalCount = query.Count();

            var pagedData = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ProjectTo<PersonDTO>(mapper.ConfigurationProvider)
                .ToList();

            return new PagedResult<PersonDTO>(pagedData, totalCount);
        }

        public async Task<PersonDTO> UpdatePersonAsync(int id, UpdatePersonDTO dto)
        {
           var person = await unitofwork.Person.GetByIdAsync(id);
            if (person == null)
            {
                throw new Exception($"Person with id {id} not found.");
            }
            mapper.Map(dto, person);
            unitofwork.Person.Update(person);
            await unitofwork.SaveChangesAsync();
            return mapper.Map<PersonDTO>(person);
        }
    }
}
