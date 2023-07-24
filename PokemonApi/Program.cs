using RestSharp;
using System.Text.Json;

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
                request = new("", Method.Get);

                var response = await client.ExecuteAsync(request);

                Console.WriteLine($"\n{response.Content}");

                return;
            }
            else
            {
                Console.Write("\nDigite o nome do Pokemon: ");
                string pokemonName = Console.ReadLine();

                request = new($"{pokemonName.ToLower()}", Method.Get);

                var response = await client.ExecuteAsync(request);

                var pokemonInfo = JsonSerializer.Deserialize<Pokemon>(response.Content);

                Console.WriteLine($"\nPokemon: {ToCapitalize(pokemonInfo.name)}");
                Console.WriteLine($"N° na Pokedex: {pokemonInfo.order}");
                Console.WriteLine($"Altura: {pokemonInfo.height / 10.0} m");
                Console.WriteLine($"Peso: {pokemonInfo.weight / 10.0} kg");
                Console.WriteLine("Habilidades: ");

                for (int i = 0; i < pokemonInfo.abilities.Count; i++)
                {
                    Console.WriteLine($"-> {ToCapitalize(pokemonInfo.abilities[i].ability.name)}");
                }

                return;
            }
        }

        public static string ToCapitalize(string word)
        {
            string formatted = word[0].ToString().ToUpper();
            formatted += word.Substring(1);

            return formatted;
        }
    }

    public class Pokemon
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public double height { get; set; }
        public double weight { get; set; }
        public int order { get; set; }
        public IList<PokemonAbilities> abilities { get; set; }
    }

    public class PokemonAbilities
    {
        public Ability ability { get; set; }
    }

    public class Ability
    {
        public string name { get; set; } = string.Empty;
    }
}