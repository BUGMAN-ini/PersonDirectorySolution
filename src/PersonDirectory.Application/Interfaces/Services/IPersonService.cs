namespace PersonDirectory.Application.Interfaces.Services
{
    public interface IPersonService
    {
        Task<PersonDTO> CreatePersonAsync(CreatePersonDTO dto);
        Task<PersonDTO> UpdatePersonAsync(int id, UpdatePersonDTO dto);
        Task DeletePersonAsync(int id);
        Task<PersonDTO> GetById(int id);
        Task<IEnumerable<PersonDTO>> GetAllPersonAsync();
        Task<IEnumerable<PersonDTO>> SearchAsync(string? name, string? lastName, string? personalNumber);
    }
}
