using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.Services
{
    public class CityService(IUnitOfWork unitOfWork, IMapper mapper)
        : ICityService
    {
        public Task<CityDTO> CreateCityAsync(CityDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCityAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CityDTO>> GetAllCitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CityDTO> GetCityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CityDTO>> SearchCitiesAsync(string? name, string? postalCode)
        {
            throw new NotImplementedException();
        }

        public Task<CityDTO> UpdateCityAsync(int id, CityDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
