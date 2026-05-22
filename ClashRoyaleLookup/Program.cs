using System.Net.Http.Json;

namespace ClashRoyaleLookup
{
    public abstract class ClashLookup
    {
        private static readonly HttpClient Client = new HttpClient();

        public static async Task SearchForUser(string? playerTag)
        {
            // remove the # to make stuff easier
            playerTag = playerTag?.Replace("#", "");
            var clashApiKey = Environment.GetEnvironmentVariable("CLASH_ROYALE_API_KEY");

            if (clashApiKey == null)
            {
                throw new Exception("You need a API Key to use this program. Go to https://developer.clashroyale.com to create an Account \n and get a token. This program will now exit.");
            }
            
            Client.DefaultRequestHeaders.Add(name: "Authorization",
                "Bearer " + clashApiKey);
            var response =
                await Client.GetFromJsonAsync<PlayerProfile>(
                    $"https://api.clashroyale.com/v1/players/%23{playerTag}");

            switch (response)
            {
                case null:
                    Console.WriteLine("No players found.");
                    return;
            }
            
            // TODO: Add more stats and recent cards used. Also include a GUI rundown.
            Console.WriteLine($"Name: {response.name}");
            Console.WriteLine($"Level: {response.expLevel}");
            Console.WriteLine($"Trophies: {response.trophies}");
            Console.WriteLine($"BestTrophies: {response.bestTrophies}");
            Console.WriteLine($"Wins: {response.wins}");
            Console.WriteLine($"Losses: {response.losses}");
            Console.WriteLine($"Battle Count: {response.battleCount}");
            Console.WriteLine($"ThreeCrown Wins: {response.threeCrownWins}");
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
