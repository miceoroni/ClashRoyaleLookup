using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClashRoyaleLookup
{
    public static class ClashLookup
    {
        private static readonly HttpClient Client = new HttpClient();

        public static async Task<PlayerProfile?> SearchForUser(string? playerTag, string apiKey)
        {
            // remove the # to make stuff easier
            playerTag = playerTag?.Replace("#", "");


            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);
            var response =
                await Client.GetFromJsonAsync<PlayerProfile>(
                    $"https://api.clashroyale.com/v1/players/%23{playerTag}");

            switch (response)
            {
                case null:
                    Console.WriteLine("No players found.");
                    break;
                default:
                    return response;
            }

            // TODO: Add more stats and recent cards used. Also include a GUI rundown.
            return null;
        }

        public class PlayerProfile
        {
            public Clan clan { get; set; }
            public string? name { get; set; }
            public int expLevel { get; set; }
            public int trophies { get; set; }
            public int bestTrophies { get; set; }
            public int wins { get; set; }
            public int losses { get; set; }
            public int battleCount { get; set; }
            public int threeCrownWins { get; set; }
            public CurrentFavouriteCard currentFavouriteCard { get; set; }
    }

        public class Clan
        {
            [JsonPropertyName("tag")]
            public string Tag { get; set; }
            
            [JsonPropertyName("name")]
            public string Name { get; set; }
            
            [JsonPropertyName("badgeId")]
            public int BadgeId { get; set; }
        }

        public class ClanBadge
        {
            [JsonPropertyName("name")]
            public string Name { get; set; }
            
            [JsonPropertyName("icon_export_name")]
            public string IconExportName { get; set; }
            
            [JsonPropertyName("id")]
            public int Id { get; set; }
            
            public string? IconUrl { get; set; }
        }

        public class CardIconUrls
        {
            public string? medium { get; set; }
            public string? evolutionMedium { get; set; }
        }
        public class CurrentFavouriteCard
        {
            public CardIconUrls iconUrls { get; set; }
            
            public string name { get; set; }
            
            public int id { get; set; }
            
            public string rarity { get; set; }
            
            public int maxLevel { get; set; }
            
            public int elixirCost { get; set; }
            
            public int maxEvolutionLevel { get; set; }
        }
        // I hate supercell for not including an api for this. - Mice 6/4/26
        public static async Task<ClanBadge> GrabClanIcon(int BadgeId)
        {
            string path = Path.Combine(AppContext.BaseDirectory, "alliance_badges.json");
            using FileStream stream = File.OpenRead(path);
            var list = await JsonSerializer.DeserializeAsync<List<ClanBadge>>(stream);
            
            return list?.FirstOrDefault(b => b.Id == BadgeId);
        }
    }
}
/*
public static class ClashLookupProgram
{
    public static async Task Main(string[] args)
    {
        Console.WriteLine("ClashRoyaleLookup - Written by Mice");
        Console.WriteLine("https://github.com/miceoroni/ClashRoyaleLookup");
        while (true)
        {
            Console.WriteLine("Enter the clash user ID you wish to find: ");
            var playerTag = Console.ReadLine();

            if (string.Equals(playerTag.Trim(), "exit", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting...");
                return;
            }
            if (!string.IsNullOrWhiteSpace(playerTag))
            {
                await ClashLookup.SearchForUser(playerTag);
            }
            else
            {
                Console.WriteLine("You need to enter a tag.");
            }
        }
    }
}
}
*/