using Ardalis.Specification;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Queries.Languages.Specifications;

public sealed class GetLanguageByIdQuerySpec : Specification<LanguageEntity>, ISingleResultSpecification
{
    public GetLanguageByIdQuerySpec(int id)
    {
        Query
            .AsNoTracking()
            .Where(entity => entity.Id == id);
    }
}