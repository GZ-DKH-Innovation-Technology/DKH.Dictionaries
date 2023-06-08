using DKH.Dictionaries.DataTranslator;
using DKH.Dictionaries.DataTranslator.Data;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

string CreateDirectory(string path)
{
    if (!File.Exists(path))
        Directory.CreateDirectory(path);
    return path;
}

var serviceProvider = new ServiceCollection()
    .AddSingleton<OverPassApi>()
    .AddSingleton<DataGenerator>()
    .BuildServiceProvider();

var dataGenerator = serviceProvider.GetService<DataGenerator>();

if (dataGenerator != null)
{
    Console.WriteLine(
        "Please enter the language or languages using a space in which you want to generate translation data");
    Console.WriteLine("example: en fr de zh-Hans zh-Hant");
    args = args.Length > 0
        ? args
        : Console.ReadLine() is string input && !string.IsNullOrWhiteSpace(input)
            ? input.Split(' ')
            : new[] { "en", "ru", "zh-Hans", "zh-Hant" };

    await dataGenerator.CountriesGenerate(args);

    var jsonSettings = new JsonSerializerSettings
    {
        ContractResolver = new CamelCasePropertyNamesContractResolver(),
        Formatting = Formatting.Indented,
        Converters = new List<JsonConverter>()
        {
            new StringEnumConverter()
        }
    };
    var countriesOutput = JsonConvert.SerializeObject(dataGenerator.Countries, jsonSettings);
    await File.WriteAllTextAsync(CreateDirectory("Data" + Path.DirectorySeparatorChar) + "Countries.json",
        countriesOutput);

    foreach (var arg in args)
    {
        var translationOutput = JsonConvert.SerializeObject(
            dataGenerator.CountryTranslations.Where(ct => ct.LanguageCode == arg),
            jsonSettings);

        await File.WriteAllTextAsync(CreateDirectory(
                                         "Data" + Path.DirectorySeparatorChar + "CountryTranslations" +
                                         Path.DirectorySeparatorChar) + arg +
                                     ".json",
            translationOutput);
    }
}

Console.WriteLine("Press any key to exit");
Console.ReadLine();