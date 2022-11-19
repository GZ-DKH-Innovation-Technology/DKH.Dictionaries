using AutoMapper;
using DKH.Dictionaries.Application.Dto.Currencies;
using DKH.Dictionaries.Domain.Entities;

namespace DKH.Dictionaries.Application.Mapper;

public class CurrencyProfile : Profile
{
    public CurrencyProfile()
    {
        CreateMap<GetCurrency, CurrencyEntity>().ReverseMap();
    }
}