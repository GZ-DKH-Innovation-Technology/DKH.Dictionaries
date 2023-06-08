using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using DKH.Dictionaries.Application.Dto.Countries;
using DKH.Dictionaries.Application.Queries.Countries.Specifications;
using DKH.Dictionaries.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Queries.Countries;

public class GetCountryTranslationsQuery : PagedAndSortedResultRequestDto,
    IRequest<PagedResultDto<GetCountryTranslation>>
{
}

public class GetCountryTranslationsQueryHandler : IRequestHandler<GetCountryTranslationsQuery,
    PagedResultDto<GetCountryTranslation>>
{
    private readonly DictionaryDbContext _context;
    private readonly IMapper _mapper;

    public GetCountryTranslationsQueryHandler(DictionaryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<GetCountryTranslation>> Handle(GetCountryTranslationsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.CountryTranslations.WithSpecification(new GetCountryTranslationsQuerySpec(request));
        var total = await query.LongCountAsync(cancellationToken);

        var countryTranslations = await query
            .Select(entity => _mapper.Map<GetCountryTranslation>(entity))
            .ToListAsync(cancellationToken);

        return new PagedResultDto<GetCountryTranslation>(total, countryTranslations);
    }
}