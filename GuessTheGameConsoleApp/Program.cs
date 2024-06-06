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

        string tryAgain = "";

        Console.WriteLine("Welcome to GuessTheGame!");
        while (tryAgain != "no")
        {
            Random random = new Random();
            

            var games = await igdb.QueryAsync<Game>(IGDBClient.Endpoints.Games, query: "fields name,genres.name,platforms.name,rating,category,themes.name,screenshots.image_id," +
           "release_dates.y,player_perspectives.name,game_modes.name,version_parent.name; sort rating desc; limit 100; where category = 0;");
            int randomIndex = random.Next(games.Count());
            var game = games[randomIndex];

            int index = random.Next(1, 4);

            string rightGuess = game.Name;
            string userGuess = "";

            int errorCount = 0;

            string img = $"//images.igdb.com/igdb/image/upload/t_1080p/{game.Screenshots.Values[index].ImageId}.jpg";

            if (game.VersionParent != null)
            {
                rightGuess = game.VersionParent.Value.Name;
            }
            while (rightGuess != userGuess)
            {
                switch (errorCount)
                {
                    case 0:
                        Console.WriteLine("=================================");
                        if (game.GameModes != null)
                        {
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
                        }

                        if (game.PlayerPerspectives != null)
                        {
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
                        }

                        if (game.ReleaseDates != null)
                        {
                            Console.Write("Release Date: ");
                            Console.WriteLine(game.ReleaseDates.Values[0].Year + ". ");
                        }

                        Console.WriteLine("---------------------------------");

                        Console.Write("Enter your guess: ");
                        userGuess = Console.ReadLine();

                        Console.WriteLine("=================================");

                        errorCount++;
                        break;
                    case 1:
                        if (game.Genres != null)
                        {
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
                        }

                        if (game.Themes != null)
                        {
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
                        }


                        Console.WriteLine("---------------------------------");

                        Console.Write("Enter your guess: ");
                        userGuess = Console.ReadLine();

                        Console.WriteLine("=================================");

                        errorCount++;
                        break;
                    case 2:
                        if (game.Screenshots != null)
                        {
                            Console.Write("Screenshot: ");
                            Console.WriteLine(img);
                        }


                        Console.Write("Rating: ");
                        Console.WriteLine($"{game.Rating:F2}");

                        Console.WriteLine("---------------------------------");

                        Console.Write("Enter your guess: ");
                        userGuess = Console.ReadLine();

                        Console.WriteLine("=================================");

                        errorCount++;
                        break;
                    case 3:
                        int newIndex = random.Next(1, 4);

                        if (newIndex != index)
                        {
                            img = $"//images.igdb.com/igdb/image/upload/t_1080p/{game.Screenshots.Values[newIndex].ImageId}.jpg";
                        }
                        else
                        {
                            newIndex = random.Next(1, 4);
                            img = $"//images.igdb.com/igdb/image/upload/t_1080p/{game.Screenshots.Values[newIndex].ImageId}.jpg";
                        }

                        if (game.Platforms != null)
                        {
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
                        }

                        if (game.Screenshots != null)
                        {
                            Console.Write("Screenshot: ");
                            Console.WriteLine(img);
                        }

                        Console.WriteLine("---------------------------------");

                        Console.Write("Enter your guess: ");
                        userGuess = Console.ReadLine();

                        Console.WriteLine("=================================");

                        errorCount++;
                        break;
                    case 4:
                        Console.WriteLine("GAME OVER. YOU FAILED!");
                        Console.WriteLine($"THE GAME WAS: {game.Name}");
                        errorCount++;
                        break;
                }
                if (userGuess == rightGuess)
                {
                    Console.WriteLine("Well Done! You guessed the game!");
                    Console.WriteLine("Do you want to try again?(To quit just type 'no')");
                    tryAgain = Console.ReadLine();
                    if (tryAgain == "no") { break; }
                    else { errorCount = 0; }
                }
                else if (errorCount == 5)
                {
                    Console.WriteLine("Do you want to try again?(To quit just type 'no')");
                    tryAgain = Console.ReadLine();
                    if (tryAgain == "no") { break; }
                    else { errorCount = 0; }
                    userGuess = rightGuess;

                }
            }
        }
    }
}