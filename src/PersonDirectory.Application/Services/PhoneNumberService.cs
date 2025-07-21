using PersonDirectory.Application.DTOs;
using PersonDirectory.Application.Exceptions;
namespace PersonDirectory.Application.Services
{
    public class PhoneNumberService(IUnitOfWork unitOfWork, IMapper mapper)
        : IPhoneNumberService
    {
        public async Task<PhoneNumberDTO> CreateNumberAsync(PhoneNumberDTO dto)
        {
            var num = mapper.Map<PhoneNumber>(dto);
            await unitOfWork.PhoneNumber.AddAsync(num);
            await unitOfWork.SaveChangesAsync();
            var result = mapper.Map<PhoneNumberDTO>(num);
            return result;
        }

        public async Task DeletePersonAsync(int id)
        {
            var phoneNumber = await unitOfWork.PhoneNumber.GetByIdAsync(id);
            if (phoneNumber == null)
                throw new NotFoundException("Phone number not found.");
            unitOfWork.PhoneNumber.Remove(phoneNumber);
            await unitOfWork.SaveChangesAsync();
        }

        public async Task<PagedResult<PhoneNumberDTO>> GetAllAsync(PaginatedRequestAll request)
        {
            var query = unitOfWork.PhoneNumber.Query();
            var totalCount = query.Count();

            var pagedNumbers = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var phoneNumberDTOs = mapper.Map<IEnumerable<PhoneNumberDTO>>(pagedNumbers);
            return new PagedResult<PhoneNumberDTO>(phoneNumberDTOs, totalCount);
        }

        public async Task<IEnumerable<PhoneNumberDTO>> GetNumberByPersonId(int personId)
        {
            var phoneNumbers = await unitOfWork.PhoneNumber.FindAsync(p => p.PersonId == personId);
            if (phoneNumbers == null || !phoneNumbers.Any())
                throw new NotFoundException("No phone numbers found for this person.");
            return mapper.Map<IEnumerable<PhoneNumberDTO>>(phoneNumbers);
        }

        public async Task<IEnumerable<PhoneNumberDTO>> GetPersonIdByNumber(string phoneNumber)
        {
            var phoneNumbers = await unitOfWork.PhoneNumber.FindAsync(p => p.Number == phoneNumber);
            if (phoneNumbers == null || !phoneNumbers.Any())
                throw new NotFoundException("No Person found for this number.");
            return mapper.Map<IEnumerable<PhoneNumberDTO>>(phoneNumbers);
        }

        public async Task<PhoneNumberDTO> Update(int personId, PhoneNumberDTO dto)
        {
           var phoneNumber = await unitOfWork.PhoneNumber.GetByIdAsync(personId);
            if (phoneNumber == null)
                throw new NotFoundException("Phone number not found.");

            mapper.Map(dto, phoneNumber);
            unitOfWork.PhoneNumber.Update(phoneNumber);
            await unitOfWork.SaveChangesAsync();
            var result = mapper.Map<PhoneNumberDTO>(phoneNumber);
            return result;
        }

    }
}
