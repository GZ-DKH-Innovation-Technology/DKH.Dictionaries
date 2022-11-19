using Ardalis.Specification;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Queries.Countries.Specifications;

public sealed class GetCountryByIdQuerySpec : Specification<CountryEntity>, ISingleResultSpecification
{
    public GetCountryByIdQuerySpec(int id)
    {
        Query
            .AsNoTracking()
            .Where(entity => entity.Id == id);
    }
}