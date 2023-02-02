using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //This method is important because its behavior interacts with the server just like a GET request does, and 
            //returns the "response body" as a string. 
            var client = new HttpClient();

            var keepGoing = true;
            while (keepGoing)
            {
                Console.Clear();
                Console.WriteLine("Hello and welcome to this DEMO of a client side API.");
                Console.WriteLine("This API generates JOKES!");
                Console.WriteLine();
                Console.Write("Choose from the following options: ");
                Console.WriteLine();
                Console.Write("Get (O)ne random joke, get (T)en random jokes, or (Q)uit: ");
                var choice = Console.ReadLine().ToUpper();

                switch (choice)
                {
                    case "Q":
                        keepGoing = false;
                        break;

                    case "O":
                        var singleJoke = await client.GetStreamAsync("https://official-joke-api.appspot.com/random_joke");

                        var item = await JsonSerializer.DeserializeAsync<Item>(singleJoke);

                        Console.WriteLine($"{item.type}, {item.setup}, {item.punchline}, {item.id}");
                        Console.ReadLine();
                        break;

                    case "T":
                        var tenJokes = await client.GetStreamAsync("https://official-joke-api.appspot.com/random_ten");

                        var jokes = await JsonSerializer.DeserializeAsync<List<Item>>(tenJokes);

                        foreach (var joke in jokes)
                        {
                            Console.WriteLine($"{joke.type}, {joke.setup}, {joke.punchline}, {joke.id}");
                        }
                        Console.ReadLine();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}