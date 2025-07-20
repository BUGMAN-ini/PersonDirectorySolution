namespace PersonDirectory.Application.Interfaces.Services
{
    public interface IPersonService
    {
        Task<PersonDTO> CreatePersonAsync(CreatePersonDTO dto);
        Task<PersonDTO> UpdatePersonAsync(int id, UpdatePersonDTO dto);
        Task DeletePersonAsync(int id);
        Task<PersonDTO> GetById(int id);
        Task<PagedResult<PersonDTO>> GetAllPersonAsync(PaginatedRequestAll request);
        Task<PagedResult<PersonDTO>> SearchAsync(PersonSearchRequestDTO request);
    }
}
