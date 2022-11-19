using System.Globalization;
using Ardalis.Specification;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Queries.States.Specifications;

public sealed class GetStateTranslationsQuerySpec : Specification<StateTranslationEntity>
{
    public GetStateTranslationsQuerySpec(GetStateTranslationsQuery request)
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
            .Where(entity => entity.Language.CultureName == CultureInfo.CurrentCulture.Name)
            .Skip(request.SkipCount)
            .Take(request.MaxResultCount);
    }
}