using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.Interfaces.Services
{
    public interface ICityService
    {
        Task<IEnumerable<CityDTO>> GetAllCitiesAsync();
        Task<CityDTO> GetCityByIdAsync(int id);
        Task<CityDTO> CreateCityAsync(CityDTO dto);
        Task<CityDTO> UpdateCityAsync(int id, CityDTO dto);
        Task DeleteCityAsync(int id);
        Task<IEnumerable<CityDTO>> SearchCitiesAsync(string? name, string? postalCode);
    }
}
