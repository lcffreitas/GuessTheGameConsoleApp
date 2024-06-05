using IGDB;
using IGDB.Models;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var igdb = new IGDBClient(
          Environment.GetEnvironmentVariable("IGDB_CLIENT_ID"),
          Environment.GetEnvironmentVariable("IGDB_CLIENT_SECRET")
        );

        var games = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields id,name; where id = 4;");
        var game = games.First();
        Console.WriteLine(game.Id); 
        Console.WriteLine(game.Name); 
    }
}