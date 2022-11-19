using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using DKH.Dictionaries.Application.Dto.Currencies;
using DKH.Dictionaries.Application.Queries.Currencies.Specifications;
using DKH.Dictionaries.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Queries.Currencies;

public class GetCurrenciesQuery : PagedAndSortedResultRequestDto, IRequest<PagedResultDto<GetCurrency>>
{
}

public class GetCurrenciesQueryHandler : IRequestHandler<GetCurrenciesQuery, PagedResultDto<GetCurrency>>
{
    private readonly DictionaryDbContext _context;
    private readonly IMapper _mapper;

    public GetCurrenciesQueryHandler(DictionaryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<GetCurrency>> Handle(GetCurrenciesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Currencies;
        var total = await query.LongCountAsync(cancellationToken);

        var currencies = await query
            .WithSpecification(new GetCurrenciesQuerySpec(request))
            .Select(entity => _mapper.Map<GetCurrency>(entity))
            .ToListAsync(cancellationToken);

        return new PagedResultDto<GetCurrency>(total, currencies);
    }
}