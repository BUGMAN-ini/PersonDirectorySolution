using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.Interfaces.Services
{
    public interface IRelatedPersonService
    {
        Task<RelatedPersonDTO> AddRelation(CreateRelatedPersonDTO relation);
        Task<RelatedPersonDTO> UpdateRelation(int id, CreateRelatedPersonDTO relation);
        Task DeleteRelation(int id);
        Task<RelatedPersonDTO> GetRelationById(int id);
        Task<PagedResult<RelatedPersonDTO>> GetAllRelationsAsync(PaginatedRequestAll request);
    }
}
