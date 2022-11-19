using Ardalis.Specification;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Queries.Languages.Specifications;

public sealed class GetLanguagesQuerySpec : Specification<LanguageEntity>
{
    public GetLanguagesQuerySpec(GetLanguagesQuery request)
    {
        switch (request.Sorting?.ToLower())
        {
            case "id":
                Query.OrderBy(x => x.Id);
                break;
            default:
                Query.OrderBy(x => x.EnglishName);
                break;
        }

        Query
            .AsNoTracking()
            .Skip(request.SkipCount)
            .Take(request.MaxResultCount);
    }
}