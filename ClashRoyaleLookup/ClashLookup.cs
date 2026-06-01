using System.Net.Http.Json;

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
            public string? name { get; set; }
            public int expLevel { get; set; }
            public int trophies { get; set; }
            public int bestTrophies { get; set; }
            public int wins { get; set; }
            public int losses { get; set; }
            public int battleCount { get; set; }
            public int threeCrownWins { get; set; }
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