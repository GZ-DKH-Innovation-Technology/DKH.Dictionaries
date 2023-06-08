using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using DKH.Dictionaries.Application.Dto.States;
using DKH.Dictionaries.Application.Queries.Cities.Specifications;
using DKH.Dictionaries.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DKH.Dictionaries.Application.Queries.Cities;

public class GetCityByIdQuery : IRequest<GetState>
{
    public GetCityByIdQuery(string id)
    {
        Id = id;
    }

    public string Id { get; }
}

public class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, GetState>
{
    private readonly DictionaryDbContext _context;
    private readonly IMapper _mapper;

    public GetCityByIdQueryHandler(DictionaryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetState> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<GetState>(
            await _context.Cities
                .WithSpecification(new GetCityByIdQuerySpec(request.Id))
                .SingleAsync(cancellationToken));
    }
}