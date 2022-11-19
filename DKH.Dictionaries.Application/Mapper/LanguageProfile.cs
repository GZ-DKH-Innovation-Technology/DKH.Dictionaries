using AutoMapper;
using DKH.Dictionaries.Application.Dto.Languages;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Mapper;

public class LanguageProfile : Profile
{
    public LanguageProfile()
    {
        CreateMap<GetLanguage, LanguageEntity>().ReverseMap();
    }
}