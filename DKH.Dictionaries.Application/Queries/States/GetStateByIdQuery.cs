using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using DKH.Dictionaries.Application.Dto.States;
using DKH.Dictionaries.Application.Queries.States.Specifications;
using DKH.Dictionaries.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DKH.Dictionaries.Application.Queries.States;

public class GetStateByIdQuery : IRequest<GetState>
{
    public GetStateByIdQuery(string id)
    {
        Id = id;
    }

    public string Id { get; }
}

public class GetStateByIdQueryHandler : IRequestHandler<GetStateByIdQuery, GetState>
{
    private readonly DictionaryDbContext _context;
    private readonly IMapper _mapper;

    public GetStateByIdQueryHandler(DictionaryDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetState> Handle(GetStateByIdQuery request, CancellationToken cancellationToken)
    {
        return _mapper.Map<GetState>(
            await _context.States
                .WithSpecification(new GetStateByIdQuerySpec(request.Id))
                .SingleAsync(cancellationToken));
    }
}