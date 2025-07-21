using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.Interfaces.Services
{
    public interface IPhoneNumberService
    {
        Task<IEnumerable<PhoneNumberDTO>> GetNumberByPersonId(int personId);
        Task<IEnumerable<PhoneNumberDTO>> GetPersonIdByNumber(string phoneNumber);
        Task<PhoneNumberDTO> Update(int personId,PhoneNumberDTO dto);
        Task<PhoneNumberDTO> CreateNumberAsync(PhoneNumberDTO dto);
        Task DeletePersonAsync(int id);
        Task<PagedResult<PhoneNumberDTO>> GetAllAsync(PaginatedRequestAll request);
    }
}
