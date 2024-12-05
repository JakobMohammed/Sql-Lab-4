using System.Collections.Generic;
using System.Text.Json.Serialization;

public class ZippopotamResponse
{
    [JsonPropertyName("post code")]
    public string PostCode { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }

    [JsonPropertyName("places")]
    public List<Place> Places { get; set; }
}

public class Place
{
    [JsonPropertyName("place name")]
    public string PlaceName { get; set; }

    [JsonPropertyName("latitude")]
    public string Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public string Longitude { get; set; }
}