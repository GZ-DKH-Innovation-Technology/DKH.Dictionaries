using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using DKH.Dictionaries.Application.Dto.Languages;
using DKH.Dictionaries.Application.Queries.Languages.Specifications;
using DKH.Dictionaries.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;

namespace DKH.Dictionaries.Application.Queries.Languages;

public class GetLanguagesQuery : PagedAndSortedResultRequestDto, IRequest<PagedResultDto<GetLanguage>>
{
}

public class GetLanguagesQueryHandler : IRequestHandler<GetLanguagesQuery, PagedResultDto<GetLanguage>>
{
    private readonly DictionaryDbContext _context;
    private readonly IMapper _mapper;

    public GetLanguagesQueryHandler(DictionaryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResultDto<GetLanguage>> Handle(GetLanguagesQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Languages;
        var total = await query.LongCountAsync(cancellationToken);

        var currencies = await query
            .WithSpecification(new GetLanguagesQuerySpec(request))
            .Select(entity => _mapper.Map<GetLanguage>(entity))
            .ToListAsync(cancellationToken);

        return new PagedResultDto<GetLanguage>(total, currencies);
    }
}