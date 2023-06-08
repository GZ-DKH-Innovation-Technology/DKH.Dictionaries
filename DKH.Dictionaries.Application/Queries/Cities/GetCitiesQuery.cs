using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using DKH.Dictionaries.Application.Dto.Cities;
using DKH.Dictionaries.Application.Queries.Cities.Specifications;
using DKH.Dictionaries.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Queries.Cities;

public class GetCitiesQuery : PagedAndSortedResultRequestDto, IRequest<PagedResultDto<GetCity>>
{
}

public class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, PagedResultDto<GetCity>>
{
    private readonly DictionaryDbContext _context;
    private readonly IMapper _mapper;

    public GetCitiesQueryHandler(DictionaryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<GetCity>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Cities;
        var total = await query.LongCountAsync(cancellationToken);

        var cities = await query
            .WithSpecification(new GetCitiesQuerySpec(request))
            .Select(entity => _mapper.Map<GetCity>(entity))
            .ToListAsync(cancellationToken);

        return new PagedResultDto<GetCity>(total, cities);
    }
}