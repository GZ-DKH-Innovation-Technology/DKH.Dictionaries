using Ardalis.Specification;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Queries.Cities.Specifications;

public sealed class GetCitiesQuerySpec : Specification<CityEntity>
{
    public GetCitiesQuerySpec(GetCitiesQuery request)
    {
        switch (request.Sorting?.ToLower())
        {
            case "id":
                Query.OrderBy(x => x.Id);
                break;
            default:
                Query.OrderBy(x => x.Name);
                break;
        }

        Query
            .AsNoTracking()
            .Skip(request.SkipCount)
            .Take(request.MaxResultCount);
    }
}