using PersonDirectory.Application.Interfaces.Repositories;
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
        public async Task<CityDTO> CreateCityAsync(CreateCityDTO dto)
        {
            var city = mapper.Map<City>(dto);
            await unitOfWork.City.AddAsync(city);
            await unitOfWork.SaveChangesAsync();
            var result = mapper.Map<CityDTO>(city); 
            return result;
        }

        public async Task<string> DeleteCityAsync(int id)
        {
            var city = await unitOfWork.City.GetByIdAsync(id);
            unitOfWork.City.Remove(city);
            await unitOfWork.SaveChangesAsync();
            return $"City with id {id} deleted successfully.";
        }

        public async Task<PagedResult<CityDTO>> GetAllCitiesAsync(PaginatedRequestAll request)
        {
            var query = await unitOfWork.City.Query();
            var totalcount = query.Count();

            var pagedPersons = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var cityDTOs = mapper.Map<IEnumerable<CityDTO>>(pagedPersons);

            return new PagedResult<CityDTO>(cityDTOs, totalcount);
        }

        public async Task<CityDTO> GetCityByIdAsync(int id)
        {
            var city = await unitOfWork.City.GetByIdAsync(id);
            return mapper.Map<CityDTO>(city);
        }

        public async Task<CityDTO> UpdateCityAsync(int id, CreateCityDTO dto)
        {
            var city = await unitOfWork.City.GetByIdAsync(id);
            if (city == null)
            {
                throw new KeyNotFoundException($"City with id {id} not found.");
            }
            mapper.Map(dto, city);
            unitOfWork.City.Update(city);
            await unitOfWork.SaveChangesAsync();
            return mapper.Map<CityDTO>(city);
        }
    }
}
