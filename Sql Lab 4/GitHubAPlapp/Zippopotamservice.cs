using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class ZippopotamService
{
    private readonly HttpClient _httpClient;

    public ZippopotamService()
    {
        _httpClient = new HttpClient();
    }

    /// <summary>
    /// Hämtar platsinformation från Zippopotam API baserat på landskod och postnummer.
    /// </summary>
    /// <param name="countryCode">Landskoden (t.ex. "se" för Sverige).</param>
    /// <param name="zipCode">Postnumret (t.ex. "11122" för Stockholm).</param>
    /// <returns>Ett ZippopotamResponse-objekt eller null om ingen data hittades.</returns>
    public async Task<ZippopotamResponse?> GetLocationInfoAsync(string countryCode, string zipCode)
    {
        try
        {
            // Bygg URL för API-anropet
            var url = $"https://api.zippopotam.us/{countryCode}/{zipCode}";
            Console.WriteLine($"Anropar URL: {url}"); // Debug-utskrift för att verifiera URL

            // Skicka GET-förfrågan till API:et
            var response = await _httpClient.GetAsync(url);

            // Kontrollera om data finns
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine($"Ingen data hittades för {countryCode.ToUpper()} med postnummer {zipCode}.");
                return null;
            }

            // Säkerställ att förfrågan lyckades, annars kasta undantag
            response.EnsureSuccessStatusCode();

            // Läs och deserialisera JSON-svaret
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<ZippopotamResponse>(json);
        }
        catch (Exception ex)
        {
            // Fånga och logga fel
            Console.WriteLine($"Ett fel inträffade: {ex.Message}");
            return null;
        }
    }
}
