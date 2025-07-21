using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.Interfaces.Services
{
    public interface ICityService
    {
        Task<CityDTO> GetCityByIdAsync(int id);
        Task<CityDTO> CreateCityAsync(CreateCityDTO dto);
        Task<CityDTO> UpdateCityAsync(int id, CreateCityDTO dto);
        Task<string> DeleteCityAsync(int id);
        Task<PagedResult<CityDTO>> GetAllCitiesAsync(PaginatedRequestAll request);
    }
}
