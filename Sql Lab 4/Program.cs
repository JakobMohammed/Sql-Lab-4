using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var gitHubService = new GitHubService();
        var zippopotamService = new ZippopotamService();

        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Välkommen till API App!");
            Console.WriteLine("Välj ett alternativ:");
            Console.WriteLine("1. Visa GitHub-repositories");
            Console.WriteLine("2. Visa platsinformation för Stockholm och Oslo");
            Console.WriteLine("0. Avsluta");

            Console.Write("\nDitt val: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await ShowGitHubRepositories(gitHubService);
                    break;
                case "2":
                    await ShowStockholmAndOsloInfo(zippopotamService);
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    // Metoden för att visa GitHub-repositories
    static async Task ShowGitHubRepositories(GitHubService gitHubService)
    {
        try
        {
            List<GitHubRepository> repositories = await gitHubService.GetRepositoriesAsync();

            Console.WriteLine("GitHub-repositories:");
            foreach (var repo in repositories)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine($"Name: {repo.Name}");
                Console.WriteLine($"Description: {repo.Description ?? "Ingen beskrivning"}");
                Console.WriteLine($"URL: {repo.HtmlUrl}");
                Console.WriteLine($"Homepage: {repo.Homepage ?? "Ingen hemsida"}");
                Console.WriteLine($"Watchers: {repo.Watchers}");
                Console.WriteLine($"Pushed At: {repo.PushedAt:yyyy-MM-dd HH:mm:ss}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ett fel inträffade: {ex.Message}");
        }

        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }

    // Metoden för att visa Stockholm och Oslo platsinformation
    static async Task ShowStockholmAndOsloInfo(ZippopotamService zippopotamService)
    {
        var locations = new List<(string CountryCode, string ZipCode, string ExpectedCity)>
        {
            ("se", "11122", "Stockholm"), // Stockholm
            ("no", "0010", "Oslo")       // Oslo
        };

        foreach (var (countryCode, zipCode, expectedCity) in locations)
        {
            try
            {
                var locationInfo = await zippopotamService.GetLocationInfoAsync(countryCode, zipCode);

                if (locationInfo == null)
                {
                    Console.WriteLine($"Kunde inte hämta data för {countryCode.ToUpper()} med postnummer {zipCode}.");
                    continue;
                }

                var capitalPlace = locationInfo.Places.Find(place => place.PlaceName.Equals(expectedCity, StringComparison.OrdinalIgnoreCase));
                if (capitalPlace != null)
                {
                    Console.WriteLine($"Platsinformation för {locationInfo.Country} ({countryCode.ToUpper()}):");
                    Console.WriteLine($"Platsnamn: {capitalPlace.PlaceName}");
                    Console.WriteLine($"Latitud: {capitalPlace.Latitude}");
                    Console.WriteLine($"Longitud: {capitalPlace.Longitude}");
                }
                else
                {
                    Console.WriteLine($"Huvudstaden {expectedCity} hittades inte för postnummer {zipCode}.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ett fel inträffade vid hämtning av data för {countryCode.ToUpper()}: {ex.Message}");
            }

            Console.WriteLine();
        }

        Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
        Console.ReadKey();
    }
}
