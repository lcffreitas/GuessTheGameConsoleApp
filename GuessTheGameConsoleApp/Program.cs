using IGDB;
using IGDB.Models;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var igdb = new IGDBClient(
          Environment.GetEnvironmentVariable("IGDB_CLIENT_ID"),
          Environment.GetEnvironmentVariable("IGDB_CLIENT_SECRET")
        );


        Random random = new Random();
        int index = random.Next(1, 4);
        

        var games = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields name,genres.name,platforms.name,rating,category,themes.name,screenshots.image_id," +
           "release_dates.y,player_perspectives.name,game_modes.name,version_parent.name; sort rating desc; limit 100;");
        //var game = games.First();


        int randomIndex = random.Next(games.Count());


        var game = games[randomIndex];


        string rightGuess = game.Name;
        string userGuess = "";


        int errorCount = 0;

        
        string img = $"//images.igdb.com/igdb/image/upload/t_1080p/{game.Screenshots.Values[index].ImageId}.jpg";


        if (game.VersionParent != null)
        {
            rightGuess = game.VersionParent.Value.Name;
        }

        Console.WriteLine("Welcome to GuessTheGame!");
        while (rightGuess != userGuess)
        { 
            switch (errorCount)
            {
                case 0:
                    Console.WriteLine("=================================");
                    Console.Write("Game Mode: ");
                    for (int i = 0; i < game.GameModes.Values.Length; i++)
                    {
                        Console.Write(game.GameModes.Values[i].Name);
                        if (i < game.GameModes.Values.Length - 1)
                        {
                            Console.Write(", ");
                        }
                        else
                        {
                            Console.WriteLine(". ");
                        }
                    }

                    Console.Write("Player Perspective: ");
                    for (int i = 0; i < game.PlayerPerspectives.Values.Length; i++)
                    {
                        Console.Write(game.PlayerPerspectives.Values[i].Name);
                        if (i < game.PlayerPerspectives.Values.Length - 1)
                        {
                            Console.Write(", ");
                        }
                        else
                        {
                            Console.WriteLine(". ");
                        }
                    }

                    Console.Write("Release Date: ");
                    Console.WriteLine(game.ReleaseDates.Values[0].Year + ". ");

                    Console.WriteLine("---------------------------------");

                    Console.Write("Enter your guess: ");
                    userGuess = Console.ReadLine();

                    Console.WriteLine("=================================");

                    errorCount++;
                    break;
                case 1:
                    Console.Write("Genres: ");
                    for (int i = 0; i < game.Genres.Values.Length; i++)
                    {
                        Console.Write(game.Genres.Values[i].Name);
                        if (i < game.Genres.Values.Length - 1)
                        {
                            Console.Write(", ");
                        }
                        else
                        {
                            Console.WriteLine(".");

                        }
                    }

                    Console.Write("Themes: ");
                    for (int i = 0; i < game.Themes.Values.Length; i++)
                    {
                        Console.Write(game.Themes.Values[i].Name);
                        if (i < game.Themes.Values.Length - 1)
                        {
                            Console.Write(", ");
                        }
                        else
                        {
                            Console.WriteLine(".");

                        }
                    }

                    Console.WriteLine("---------------------------------");

                    Console.Write("Enter your guess: ");
                    userGuess = Console.ReadLine();

                    Console.WriteLine("=================================");

                    errorCount++;
                    break;
                case 2:
                    Console.Write("Categorie: ");
                    Console.WriteLine(game.Category);

                    Console.Write("Rating: ");
                    Console.WriteLine($"{game.Rating:F2}");

                    Console.WriteLine("---------------------------------");

                    Console.Write("Enter your guess: ");
                    userGuess = Console.ReadLine();

                    Console.WriteLine("=================================");

                    errorCount++;
                    break;
                case 3:
                    Console.Write("Platforms: ");
                    for (int i = 0; i < game.Platforms.Values.Length; i++)
                    {
                        Console.Write(game.Platforms.Values[i].Name);
                        if (i < game.Platforms.Values.Length - 1)
                        {
                            Console.Write(", ");
                        }
                        else
                        {
                            Console.WriteLine(".");

                        }
                    }

                    Console.Write("Screenshot: ");
                    Console.WriteLine(img);

                    Console.WriteLine("---------------------------------");

                    Console.Write("Enter your guess: ");
                    userGuess = Console.ReadLine();

                    Console.WriteLine("=================================");

                    errorCount++;
                    break;
                case 4:
                    Console.WriteLine("GAME OVER. YOU FAILED!");
                    Console.WriteLine($"THE GAME WAS: {game.Name}");
                    userGuess = rightGuess;
                    break;
            }
        }
    }
}