using AutoMapper;
using DKH.Dictionaries.Application.Dto.Cities;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Mapper;

public class CityProfile : Profile
{
    public CityProfile()
    {
        CreateMap<GetCity, CityEntity>().ReverseMap();
        CreateMap<GetCityTranslation, CityTranslationEntity>().ReverseMap();
    }
}