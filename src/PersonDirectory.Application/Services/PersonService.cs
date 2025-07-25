using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using PersonDirectory.Application.Exceptions;

namespace PersonDirectory.Application.Services
{
    public class PersonService
        (IUnitOfWork unitofwork, IMapper mapper) : IPersonService
    {
        private readonly string _imageRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        public async Task<PersonDTO> CreatePersonAsync(CreatePersonDTO dto)
        {
            var person = mapper.Map<Person>(dto);
            person.ImagePath = "images/" + "0f1b4ec9-a6a6-4b9e-b00e-aa3c81a04153.jpeg";
            if (await unitofwork.Person.PinExistsAsync(dto.PersonalNumber))
                throw new DuplicatePersonalNumberException(dto.PersonalNumber);

            var city = await unitofwork.City.GetByIdAsync(dto.CityId);
            if (city == null)
                throw new CityNotFoundException(dto.CityId);

            await unitofwork.Person.AddAsync(person);
            await unitofwork.SaveChangesAsync();

            var full = await unitofwork.Person.GetByIdDetailAsync(person.Id);
            if (full == null)
                throw new NotFoundException("Person not found.");

            var result = mapper.Map<PersonDTO>(full);

            return result;
        }


        public async Task DeletePersonAsync(int id)
        {
            var person = await unitofwork.Person.GetByIdAsync(id);
            if (person == null)
                throw new NotFoundException("Person not found.");

            unitofwork.Person.Remove(person);
            await unitofwork.SaveChangesAsync();

        }

        public async Task<PagedResult<PersonDTO>> GetAllPersonAsync(PaginatedRequestAll request)
        {
            var query = await unitofwork.Person.Query();
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
                throw new NotFoundException("Person not found.");

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
                throw new NotFoundException($"Personal number {personalNumber} already exists.");
        }

        public async Task<PagedResult<PersonDTO>> SearchAsync(PersonSearchRequestDTO request)
        {
            var query = await unitofwork.Person.Query();

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

        public async Task<PersonDTO> UploadPersonImageAsync(int personId, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                throw new ArgumentException("Image file is invalid");

            var person = await unitofwork.Person.GetByIdAsync(personId);
            if (person == null)
                throw new InvalidOperationException("Person not found");

            Directory.CreateDirectory(_imageRoot);

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(imageFile.FileName)}";
            var fullPath = Path.Combine(_imageRoot, fileName);

            await using var fs = new FileStream(fullPath, FileMode.Create);
            await imageFile.CopyToAsync(fs);

            person.ImagePath = Path.Combine("images", fileName).Replace("\\", "/");
            await unitofwork.SaveChangesAsync();

            return mapper.Map<PersonDTO>(person); ;
        }
    }
}
