using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using DKH.Dictionaries.Application.Dto.States;
using DKH.Dictionaries.Application.Queries.States.Specifications;
using DKH.Dictionaries.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Queries.States;

public class GetStateTranslationsQuery : PagedAndSortedResultRequestDto,
    IRequest<PagedResultDto<GetStateTranslation>>
{
}

public class GetStateTranslationsQueryHandler : IRequestHandler<GetStateTranslationsQuery,
    PagedResultDto<GetStateTranslation>>
{
    private readonly DictionaryDbContext _context;
    private readonly IMapper _mapper;

    public GetStateTranslationsQueryHandler(DictionaryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<GetStateTranslation>> Handle(GetStateTranslationsQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.StateTranslations.WithSpecification(new GetStateTranslationsQuerySpec(request));
        var total = await query.LongCountAsync(cancellationToken);

        var stateTranslations = await query
            .Select(entity => _mapper.Map<GetStateTranslation>(entity))
            .ToListAsync(cancellationToken);

        return new PagedResultDto<GetStateTranslation>(total, stateTranslations);
    }
}