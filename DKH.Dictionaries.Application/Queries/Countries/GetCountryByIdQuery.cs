using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using DKH.Dictionaries.Application.Dto.Countries;
using DKH.Dictionaries.Application.Queries.Countries.Specifications;
using DKH.Dictionaries.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DKH.Dictionaries.Application.Queries.Countries;

public class GetCountryByIdQuery : IRequest<GetCountry>
{
    public GetCountryByIdQuery(string id)
    {
        Id = id;
    }

    public string Id { get; }
}

public class GetCountryByIdQueryHandler : IRequestHandler<GetCountryByIdQuery, GetCountry>
{
    private readonly DictionaryDbContext _context;
    private readonly IMapper _mapper;

    public GetCountryByIdQueryHandler(DictionaryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetCountry> Handle(GetCountryByIdQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<GetCountry>(
            await _context.Countries
                .WithSpecification(new GetCountryByIdQuerySpec(request.Id))
                .SingleAsync(cancellationToken));
    }
}