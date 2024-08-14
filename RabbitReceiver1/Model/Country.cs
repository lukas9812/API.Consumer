using System.Text.Json.Serialization;

namespace RabbitReceiver1.Model;

public class Country
{
    [JsonPropertyName("name")] public Name Name { get; set; }
    
    [JsonPropertyName("tld")] public List<string> Tld { get; set; }

    [JsonPropertyName("cca2")] public string Cca2 { get; set; }

    [JsonPropertyName("ccn3")] public string Ccn3 { get; set; }

    [JsonPropertyName("cca3")] public string Cca3 { get; set; }

    [JsonPropertyName("cioc")] public string Cioc { get; set; }

    [JsonPropertyName("independent")] public bool Independent { get; set; }

    [JsonPropertyName("status")] public string Status { get; set; }

    [JsonPropertyName("unMember")] public bool UnMember { get; set; }

    [JsonPropertyName("currencies")] public Dictionary<string, Currency> Currencies { get; set; }
}

public class Name
{
    [JsonPropertyName("common")]
    public string Common { get; set; }

    [JsonPropertyName("official")]
    public string Official { get; set; }

    [JsonPropertyName("nativeName")]
    public NativeName NativeName { get; set; }
}

public class NativeName
{
    [JsonPropertyName("deu")]
    public LanguageName Deu { get; set; }
}

public class LanguageName
{
    [JsonPropertyName("official")]
    public string Official { get; set; }

    [JsonPropertyName("common")]
    public string Common { get; set; }
}

// Class for the "currencies" part
public class Currency
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("symbol")]
    public string Symbol { get; set; }
}