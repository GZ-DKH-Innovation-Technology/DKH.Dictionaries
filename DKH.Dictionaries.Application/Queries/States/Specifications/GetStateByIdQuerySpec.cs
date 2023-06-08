using Ardalis.Specification;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Queries.States.Specifications;

public sealed class GetStateByIdQuerySpec : Specification<StateEntity>, ISingleResultSpecification
{
    public GetStateByIdQuerySpec(string id)
    {
        Query
            .AsNoTracking()
            .Where(entity => entity.Id == id);
    }
}