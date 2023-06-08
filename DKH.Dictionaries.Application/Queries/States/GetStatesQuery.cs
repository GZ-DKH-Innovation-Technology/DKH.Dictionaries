using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using DKH.Dictionaries.Application.Dto.States;
using DKH.Dictionaries.Application.Queries.States.Specifications;
using DKH.Dictionaries.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Queries.States;

public class GetStatesQuery : PagedAndSortedResultRequestDto, IRequest<PagedResultDto<GetState>>
{
}

public class GetStatesQueryHandler : IRequestHandler<GetStatesQuery, PagedResultDto<GetState>>
{
    private readonly DictionaryDbContext _context;
    private readonly IMapper _mapper;

    public GetStatesQueryHandler(DictionaryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<GetState>> Handle(GetStatesQuery request, CancellationToken cancellationToken)
    {
        var query = _context.States;
        var total = await query.LongCountAsync(cancellationToken);

        var states = await query
            .WithSpecification(new GetStatesQuerySpec(request))
            .Select(entity => _mapper.Map<GetState>(entity))
            .ToListAsync(cancellationToken);

        return new PagedResultDto<GetState>(total, states);
    }
}