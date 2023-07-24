using RestSharp;

namespace PokemonApi
{
    public class Program
    {
        public static async Task Main()
        {
            const string baseUrl = "https://pokeapi.co/api/v2/pokemon/";

            var client = new RestClient(baseUrl);
            var request = new RestRequest();

            Console.Write("Deseja digitar o nome do Pokemon? (y/n): ");
            char option = Console.ReadLine()[0];

            if (option.ToString().ToLower() == "n")
            {
                request = new ("", Method.Get);
            }
            else
            {
                Console.Write("\nDigite o nome do Pokemon: ");
                string pokemonName = Console.ReadLine();

                request = new ($"{pokemonName.ToLower()}", Method.Get);
            }

            var response = await client.ExecuteAsync(request);

            Console.WriteLine($"\n{response.Content}");
        }
    }
}