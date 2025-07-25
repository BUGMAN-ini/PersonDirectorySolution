using PersonDirectory.Application.DTOs;
using PersonDirectory.Application.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonDirectory.Application.Services
{
    public class RelatedPersonService(IUnitOfWork unitOfWork, IMapper mapper)
        : IRelatedPersonService
    {
        public async Task<RelatedPersonDTO> AddRelation(CreateRelatedPersonDTO relation)
        {
            var relatedPerson = mapper.Map<RelatedPerson>(relation);
            await unitOfWork.RelatedPersons.AddAsync(relatedPerson);
            await unitOfWork.SaveChangesAsync();
            var result = mapper.Map<RelatedPersonDTO>(relatedPerson);
            return result;
        }

        public async Task DeleteRelation(int id)
        {
            var relation = await unitOfWork.RelatedPersons.GetByIdAsync(id);
            if (relation == null)
                throw new NotFoundException($"Related person with id {id} not found.");

            unitOfWork.RelatedPersons.Remove(relation);
            await unitOfWork.SaveChangesAsync();

        }

        public async Task<PagedResult<RelatedPersonDTO>> GetAllRelationsAsync(PaginatedRequestAll request)
        {
            var query = await unitOfWork.RelatedPersons.Query();
            var totalCount = query.Count();

            var pagedRelations = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var relationDTOs = mapper.Map<IEnumerable<RelatedPersonDTO>>(pagedRelations);

            return new PagedResult<RelatedPersonDTO>(relationDTOs, totalCount);
        }

        public async Task<RelatedPersonDTO> GetRelationById(int id)
        {
            var relation = await unitOfWork.RelatedPersons.GetByIdAsync(id);
            if (relation == null)
                throw new NotFoundException($"Related person with id {id} not found.");
            return mapper.Map<RelatedPersonDTO>(relation);
        }

        public async Task<RelatedPersonDTO> UpdateRelation(int id, CreateRelatedPersonDTO relation)
        {
            var existingRelation = await unitOfWork.RelatedPersons.GetByIdAsync(id);
            if (existingRelation == null)
                throw new NotFoundException($"Related person with id {id} not found.");
            var updatedRelation = mapper.Map(relation, existingRelation);
            unitOfWork.RelatedPersons.Update(updatedRelation);
            await unitOfWork.SaveChangesAsync();

            return mapper.Map<RelatedPersonDTO>(updatedRelation);
        }
    }
}
