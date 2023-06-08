using DKH.Dictionaries.DataTranslator.Models;

namespace DKH.Dictionaries.DataTranslator
{
    // overpass api class
    public class OverPassApi
    {
        // overpass api url
        private const string OverpassApiUrl = "https://overpass-api.de/api/interpreter";

        // countries query
        private const string QueryForCountries = @"
            [out:json];
            rel[""boundary""=""administrative""][""admin_level""=""2""];
            out tags;
        ";
        
        // country capitals query
        private const string QueryForCapitals = @"
            [out:json];
            rel[""boundary""=""administrative""][""admin_level""=""2""];
            node(r:""admin_centre"");
            out tags;
        ";

        // country states query
        private const string QueryForStates = @"
            [out:json];
            area[""ISO3166-1""=""{0}""]->.country;
            rel(area.country)[""boundary""=""administrative""][""admin_level""=""4""];
            out tags;
        ";

        // country states districts query
        private const string QueryForStatesDistricts = @"
            [out:json];
            area[""ISO3166-1""=""{0}""]->.country;
            rel(area.country)[""boundary""=""administrative""][""admin_level""=""6""];
            out tags;
        ";
        
        // country cities query
        private const string QueryForCities = @"
            [out:json];
            area[""ISO3166-1""=""{0}""]->.country;
            node[place=""city""](area.country);
            out tags;
        ";

        // country cities, including small (towns and village) query
        private const string QueryForCitiesWithTownsAndVillage = @"
            [out:json];
            area[""ISO3166-1""=""{0}""]->.country;
            ( 
                node[place=""city""](area.country);
                node[place=""town""](area.country);
                node[place=""village""](area.country);
            );
            out tags;
        ";

        /// <summary>
        ///     Get query for overpass api
        /// </summary>
        /// <param name="query"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public string GetQuery(OverPassApiQueryEnum query, string? country = "")
        {
            return query switch
            {
                OverPassApiQueryEnum.COUNTRIES => QueryForCountries,
                OverPassApiQueryEnum.CAPITALS => QueryForCapitals,
                OverPassApiQueryEnum.STATES => string.IsNullOrEmpty(country)
                    ? throw new ArgumentNullException(nameof(country))
                    : string.Format(QueryForStates, country),
                OverPassApiQueryEnum.STATES_DISTRICTS => string.IsNullOrEmpty(country)
                    ? throw new ArgumentNullException(nameof(country))
                    : string.Format(QueryForStatesDistricts, country),
                OverPassApiQueryEnum.CITIES => string.IsNullOrEmpty(country)
                    ? throw new ArgumentNullException(nameof(country))
                    : string.Format(QueryForCities, country),
                OverPassApiQueryEnum.CITIES_WITH_TOWNS_AND_VILLAGE => string.IsNullOrEmpty(country)
                    ? throw new ArgumentNullException(nameof(country))
                    : string.Format(QueryForCitiesWithTownsAndVillage,
                        country),
                _ => throw new ArgumentOutOfRangeException(nameof(query), query, null)
            };
        }

        /// <summary>
        ///     Get result from overpass api
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<string> GetResult(string query)
        {
            using var httpClient = new HttpClient();
            return await httpClient.PostAsync(OverpassApiUrl, new StringContent(query)).Result.Content
                .ReadAsStringAsync();
        }
    }
}