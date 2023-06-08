using Newtonsoft.Json;

namespace DKH.Dictionaries.Application.Queries.OverPass.Dto;

public class GetOverPassResult
{
    private GetOverPassResult()
    {
        Elements = new List<OverPassElement>();
    }

    public GetOverPassResult(string version, string generator, OverPassOsm3S osm3S, List<OverPassElement> elements) : base()
    {
        Version = version;
        Generator = generator;
        Osm3S = osm3S;
        Elements = elements;
    }

    [JsonProperty("version")] public string Version { get; private set; } = string.Empty;

    [JsonProperty("generator")] public string Generator { get; private set; } = string.Empty;

    [JsonProperty("osm3s")] public OverPassOsm3S Osm3S { get; private set; } = default!;

    [JsonProperty("elements")] public List<OverPassElement> Elements { get; private set; }
}

public class OverPassOsm3S
{
    private OverPassOsm3S()
    {
    }

    public OverPassOsm3S(DateTimeOffset timestampOsmBase, DateTimeOffset timestampAreasBase, string copyRight) : base()
    {
        TimestampOsmBase = timestampOsmBase;
        TimestampAreasBase = timestampAreasBase;
        CopyRight = copyRight;
    }

    [JsonProperty("timestamp_osm_base")] public DateTimeOffset TimestampOsmBase { get; private set; } = default!;

    [JsonProperty("timestamp_areas_base")] public DateTimeOffset? TimestampAreasBase { get; private set; } = default!;

    [JsonProperty("copyright")] public string CopyRight { get; private set; } = string.Empty;
}

public class OverPassElement
{
    private OverPassElement()
    {
    }

    public OverPassElement(string type, long id, double lat, double lon,
        Dictionary<string, string> tags) : base()
    {
        Type = type;
        Id = id;
        Lat = lat;
        Lon = lon;
        Tags = tags;
    }

    [JsonProperty("type")] public string Type { get; private set; } = default!;
    [JsonProperty("id")] public long Id { get; private set; }
    [JsonProperty("lat")] public double Lat { get; private set; }
    [JsonProperty("lon")] public double Lon { get; private set; }
    [JsonProperty("tags")] public Dictionary<string, string> Tags { get; private set; } = default!;
}