using System;
using System.Text.Json.Serialization;

public class GitHubRepository
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("html_url")]
    public string HtmlUrl { get; set; }

    [JsonPropertyName("homepage")]
    public string Homepage { get; set; }

    [JsonPropertyName("watchers")]
    public int Watchers { get; set; }

    [JsonPropertyName("pushed_at")]
    public DateTime PushedAt { get; set; }
}