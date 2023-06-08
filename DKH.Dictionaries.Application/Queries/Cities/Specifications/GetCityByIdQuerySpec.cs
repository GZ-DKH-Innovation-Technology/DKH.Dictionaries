using Ardalis.Specification;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Queries.Cities.Specifications;

public sealed class GetCityByIdQuerySpec : Specification<CityEntity>, ISingleResultSpecification
{
    public GetCityByIdQuerySpec(string id)
    {
        Query
            .AsNoTracking()
            .Where(entity => entity.Id == id);
    }
}