using AutoMapper;
using DKH.Dictionaries.Application.Dto.Countries;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Mapper;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<GetCountry, CountryEntity>().ReverseMap();
        CreateMap<GetCountryTranslation, CountryTranslationEntity>().ReverseMap();
    }
}