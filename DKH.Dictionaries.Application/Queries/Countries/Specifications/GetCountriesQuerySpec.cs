using Ardalis.Specification;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Queries.Countries.Specifications;

public sealed class GetCountriesQuerySpec : Specification<CountryEntity>
{
    public GetCountriesQuerySpec(GetCountriesQuery request)
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