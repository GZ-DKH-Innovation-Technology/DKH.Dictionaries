using Ardalis.Specification;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Queries.Currencies.Specifications;

public sealed class GetCurrenciesQuerySpec : Specification<CurrencyEntity>
{
    public GetCurrenciesQuerySpec(GetCurrenciesQuery request)
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