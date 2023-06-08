using System.ComponentModel;
using DKH.Dictionaries.DataTranslator.Models;
using DKH.Dictionaries.Domain.Entities;
using DKH.Dictionaries.Domain.Enums;
using Newtonsoft.Json;

namespace DKH.Dictionaries.DataTranslator.Data
{
    public class DataGenerator
    {
        private readonly OverPassApi _overPassApi;

        private readonly List<CountryEntity> _countries = new();
        private readonly List<CountryTranslationEntity> _countryTranslations = new();

        private string GetDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi!.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public DataGenerator(OverPassApi overPassApi)
        {
            _overPassApi = overPassApi ?? throw new ArgumentNullException(nameof(overPassApi));
        }

        public IEnumerable<CountryEntity> Countries => _countries.AsReadOnly();
        public IEnumerable<CountryTranslationEntity> CountryTranslations => _countryTranslations.AsReadOnly();

        public async Task CountriesGenerate(IEnumerable<string> languages)
        {
            var countriesJson = await _overPassApi.GetResult(_overPassApi.GetQuery(OverPassApiQueryEnum.COUNTRIES));
            var countriesData = JsonConvert.DeserializeObject<OverPassResult>(countriesJson);

            countriesData?.Elements.ForEach(element =>
            {
                if (!element.Tags.ContainsKey("ISO3166-1:alpha2")) return;
                if (!element.Tags.ContainsKey("ISO3166-1:numeric")) return;
                
                var country = new CountryEntity(
                    id: Guid.NewGuid().ToString(),
                    name: element.Tags["name:en"],
                    nativeName: element.Tags["name"],
                    twoLetterCode: (CountryTwoLetterCodeEnum)Enum.Parse(typeof(CountryTwoLetterCodeEnum),
                        element.Tags["ISO3166-1:alpha2"], true),
                    threeLetterCode: (CountryThreeLetterCodeEnum)Enum.Parse(typeof(CountryThreeLetterCodeEnum),
                        element.Tags["ISO3166-1:alpha3"], true),
                    numericCode: element.Tags["ISO3166-1:numeric"]
                );

                _countries.Add(country);

                foreach (var language in languages)
                {
                    if (element.Tags.ContainsKey("name:" + language))
                        _countryTranslations.Add(new CountryTranslationEntity(
                            id: Guid.NewGuid().ToString(),
                            name: element.Tags["name:" + language],
                            countryId: country.Id,
                            languageCode: language
                        ));
                }
            });
        }
    }
}