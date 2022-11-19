using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using DKH.Dictionaries.Application.Dto.Cities;
using DKH.Dictionaries.Application.Queries.Cities.Specifications;
using DKH.Dictionaries.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Queries.Cities;

public class GetCityTranslationsQuery : PagedAndSortedResultRequestDto,
    IRequest<PagedResultDto<GetCityTranslation>>
{
}

public class GetCityTranslationsQueryHandler : IRequestHandler<GetCityTranslationsQuery,
    PagedResultDto<GetCityTranslation>>
{
    private readonly DictionaryDbContext _context;
    private readonly IMapper _mapper;

    public GetCityTranslationsQueryHandler(DictionaryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<GetCityTranslation>> Handle(GetCityTranslationsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.CityTranslations.WithSpecification(new GetCityTranslationsQuerySpec(request));
        var total = await query.LongCountAsync(cancellationToken);

        var cityTranslations = await query
            .Select(entity => _mapper.Map<GetCityTranslation>(entity))
            .ToListAsync(cancellationToken);

        return new PagedResultDto<GetCityTranslation>(total, cityTranslations);
    }
}