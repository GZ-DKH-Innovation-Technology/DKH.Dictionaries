using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using DKH.Dictionaries.Application.Dto.Countries;
using DKH.Dictionaries.Application.Queries.Countries.Specifications;
using DKH.Dictionaries.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Queries.Countries;

public class GetCountriesQuery : PagedAndSortedResultRequestDto, IRequest<PagedResultDto<GetCountry>>
{
}

public class GetCountriesQueryHandler : IRequestHandler<GetCountriesQuery, PagedResultDto<GetCountry>>
{
    private readonly DictionaryDbContext _context;
    private readonly IMapper _mapper;

    public GetCountriesQueryHandler(DictionaryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<GetCountry>> Handle(GetCountriesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Countries;
        var total = await query.LongCountAsync(cancellationToken);

        var countries = await query
            .WithSpecification(new GetCountriesQuerySpec(request))
            .Select(entity => _mapper.Map<GetCountry>(entity))
            .ToListAsync(cancellationToken);

        return new PagedResultDto<GetCountry>(total, countries);
    }
}