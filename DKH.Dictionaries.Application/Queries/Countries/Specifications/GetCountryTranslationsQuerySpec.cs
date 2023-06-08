using System.Globalization;
using Ardalis.Specification;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Queries.Countries.Specifications;

public sealed class GetCountryTranslationsQuerySpec : Specification<CountryTranslationEntity>
{
    public GetCountryTranslationsQuerySpec(GetCountryTranslationsQuery request)
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
            .Where(entity => entity.LanguageCode == CultureInfo.CurrentCulture.Name)
            .Skip(request.SkipCount)
            .Take(request.MaxResultCount);
    }
}