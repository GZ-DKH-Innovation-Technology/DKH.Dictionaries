using AutoMapper;
using DKH.Dictionaries.Application.Dto.States;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Mapper;

public class StateProfile : Profile
{
    public StateProfile()
    {
        CreateMap<GetState, StateEntity>().ReverseMap();
        CreateMap<GetStateTranslation, StateTranslationEntity>().ReverseMap();
    }
}