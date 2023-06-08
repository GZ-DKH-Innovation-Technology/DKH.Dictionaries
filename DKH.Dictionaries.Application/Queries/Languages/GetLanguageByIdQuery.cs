using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using DKH.Dictionaries.Application.Dto.Languages;
using DKH.Dictionaries.Application.Queries.Languages.Specifications;
using DKH.Dictionaries.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DKH.Dictionaries.Application.Queries.Languages;

public class GetLanguageByIdQuery : IRequest<GetLanguage>
{
    public GetLanguageByIdQuery(string id)
    {
        Id = id;
    }

    public string Id { get; set; }
}

public class GetLanguageByIdQueryHandler : IRequestHandler<GetLanguageByIdQuery, GetLanguage>
{
    private readonly DictionaryDbContext _context;
    private readonly IMapper _mapper;

    public GetLanguageByIdQueryHandler(DictionaryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetLanguage> Handle(GetLanguageByIdQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<GetLanguage>(
            await _context.Languages
                .WithSpecification(new GetLanguageByIdQuerySpec(request.Id))
                .SingleAsync(cancellationToken));
    }
}