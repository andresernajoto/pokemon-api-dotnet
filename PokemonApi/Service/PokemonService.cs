using PokemonApi.Controller;
using PokemonApi.Model;
using RestSharp;
using System.Text.Json;

namespace PokemonApi.Service
{
    public class PokemonService
    {
        private readonly PokemonController _controller = new();

        public Pokemon GetPokemonResponse(string pokemonName)
        {
            const string baseUrl = "https://pokeapi.co/api/v2/pokemon/";

            RestClient client = new(baseUrl);
            RestRequest request = new($"{pokemonName.ToLower()}", Method.Get);
            RestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("\nPokemon não encontrado!");
                Console.Write("Gostaria de procurar novamente? (y/n): ");
                char option = Console.ReadLine()[0];

                string opcaoValidada = _controller.ValidaEscolha(option);

                if (opcaoValidada == "n")
                {
                    Console.WriteLine("\nEncerrando programa!");
                    Environment.Exit(0);
                }

                Console.Write("\nDigite novamente o nome do Pokemon: ");
                pokemonName = Console.ReadLine();

                return GetPokemonResponse(pokemonName);
            }

            Pokemon pokemonInfo = JsonSerializer.Deserialize<Pokemon>(response.Content);

            return pokemonInfo;
        }
    }
}
