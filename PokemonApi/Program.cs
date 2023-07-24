using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PokemonApi
{
    public class Program
    {
        public static void Main()
        {
            Treinador treinador = new ();
            ImprimeBoasVindas();

            Console.Write("\nDigite seu nome de treinador(a): ");
            treinador.Name = Console.ReadLine();
            treinador.Name = ValidaNomeTreinador(treinador.Name);

            ImprimeOpcoesSwitchUm();
            int opcaoInicial = int.Parse(Console.ReadLine());
            OpcoesSwitch(opcaoInicial, treinador);
        }

        public static Pokemon GetPokemonResponse(string pokemonName)
        {
            const string baseUrl = "https://pokeapi.co/api/v2/pokemon/";

            RestClient client = new (baseUrl);
            RestRequest request = new($"{pokemonName.ToLower()}", Method.Get);
            RestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("\nPokemon não encontrado!");
                Console.Write("Gostaria de procurar novamente? (y/n) ");
                char option = Console.ReadLine()[0];

                if (option.ToString().ToLower() == "n")
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

        public static string ValidaNomeTreinador(string nome)
        {
            Regex pattern = new Regex(@"^[a-zA-Z]+$");

            if (!pattern.IsMatch(nome))
            {
                Console.WriteLine("O nome de treinador(a) não pode conter apenas números!");

                while (!pattern.IsMatch(nome))
                {
                    Console.Write("\nDigite novamente: ");
                    nome = Console.ReadLine();
                }
            }

            Console.WriteLine($"\nSeja bem vindo(a), treinador(a) {nome}!");

            return nome;
        }

        public static void OpcoesSwitch(int opcoes, Treinador treinador)
        {
            switch (opcoes)
            {
                case 1:
                    Console.WriteLine("\n1 - Escolher o Pokemon");
                    Console.WriteLine("2 - Voltar");

                    Console.Write("\nEscolha uma das opções acima: ");
                    opcoes = int.Parse(Console.ReadLine());

                    switch (opcoes)
                    {
                        case 1:
                            Console.Write("\nDigite o nome do Pokemon que deseja adotar: ");
                            string pokemonName = Console.ReadLine().ToLower();
                            
                            Pokemon pokemonInfo = GetPokemonResponse(pokemonName);
                            treinador.Pokemons.Add(pokemonName);

                            Console.Write($"\nMuito bem, treinador(a) {treinador.Name}! Deseja conhecer mais sobre este Pokemon? (y/n): ");
                            char opcaoDetalhes = Console.ReadLine()[0];

                            if (opcaoDetalhes.ToString().ToLower() == "y")
                            {
                               ImprimeDetalhesPokemon(pokemonInfo);
                            }

                            Console.Write("\nDeseja fazer algo a mais? (y/n): ");
                            opcaoDetalhes = Console.ReadLine()[0];

                            if (opcaoDetalhes.ToString().ToLower() == "y")
                            {
                                ImprimeOpcoesSwitchUm();

                                opcoes = int.Parse(Console.ReadLine());
                                OpcoesSwitch(opcoes, treinador);
                            }
                            else
                            {
                                ImprimeOvoPokemon();
                            }

                            break;
                        case 2:
                            ImprimeOpcoesSwitchUm();

                            opcoes = int.Parse(Console.ReadLine());
                            OpcoesSwitch(opcoes, treinador);

                            break;
                        default:
                            Console.Write("\nOpção inválida! Digite novamente: ");

                            opcoes = int.Parse(Console.ReadLine());
                            OpcoesSwitch(opcoes, treinador);

                            break;
                    }

                    break;
                case 2:
                    if (treinador.Pokemons.Count > 0 )
                    {
                        Console.WriteLine("\nLista de Pokemons adotados:");

                        foreach (string pokemon in treinador.Pokemons)
                        {
                            Console.WriteLine($"-> {ToCapitalize(pokemon)}");
                        }

                        Console.Write("\nDeseja fazer algo a mais? (y/n): ");
                        char opcaoDetalhes = Console.ReadLine()[0];

                        if (opcaoDetalhes.ToString().ToLower() == "y")
                        {
                            ImprimeOpcoesSwitchUm();

                            opcoes = int.Parse(Console.ReadLine());
                            OpcoesSwitch(opcoes, treinador);
                        }
                        else
                        {
                            ImprimeOvoPokemon();
                        }
                        break;
                    }

                    Console.WriteLine("\nVocê ainda não tem nenhum Pokemon adotado. Gostaria de adotar? (y/n): ");
                    char opcaoAdocao = Console.ReadLine()[0];

                    if (opcaoAdocao.ToString().ToLower() == "y")
                    {
                        ImprimeOpcoesSwitchUm();

                        opcoes = int.Parse(Console.ReadLine());
                        OpcoesSwitch(opcoes, treinador);

                        break;
                    }

                    Console.WriteLine($"\nObrigado por hoje, treinador(a) {treinador.Name}, até breve!");

                    break;
                case 3:
                    Console.WriteLine($"\nObrigado por hoje, treinador(a) {treinador.Name}, até breve!");
                    Environment.Exit(0);

                    break;
                default:
                    Console.Write("\nOpção inválida! Digite novamente: ");

                    opcoes = int.Parse(Console.ReadLine());
                    OpcoesSwitch(opcoes, treinador);

                    break;
            }
        }

        public static void ImprimeBoasVindas()
        {
            Console.WriteLine("##########################################################");
            Console.WriteLine("\n######   ######   #   #   ######   #    #   ######   #   #");
            Console.WriteLine("#    #   #    #   #  #    #        ##  ##   #    #   ##  #");
            Console.WriteLine("######   #    #   ###     ######   # ## #   #    #   # # #");
            Console.WriteLine("#        #    #   #  #    #        #    #   #    #   #  ##");
            Console.WriteLine("#        ######   #   #   ######   #    #   ######   #   #");
            Console.WriteLine("\n##########################################################");
        }

        public static void ImprimeOpcoesSwitchUm()
        {
            Console.WriteLine("\n1 - Adotar um Pokemon");
            Console.WriteLine("2 - Ver Pokemons adotados");
            Console.WriteLine("3 - Sair");
            Console.Write("\nEscolha uma das opções acima: ");
        }

        public static void ImprimeDetalhesPokemon(Pokemon pokemonInfo)
        {
            Console.WriteLine($"\nPokemon: {ToCapitalize(pokemonInfo.name)}");
            Console.WriteLine($"N° na Pokedex: {pokemonInfo.order}");
            Console.WriteLine($"Altura: {pokemonInfo.height / 10.0} m");
            Console.WriteLine($"Peso: {pokemonInfo.weight / 10.0} kg");
            Console.WriteLine("Habilidades: ");

            for (int i = 0; i < pokemonInfo.abilities.Count; i++)
            {
                Console.WriteLine($"-> {ToCapitalize(pokemonInfo.abilities[i].ability.name)}");
            }
        }

        public static void ImprimeOvoPokemon()
        {
            Console.WriteLine("\nAqui está seu Poke Ovo, cuide bem!");

            Console.WriteLine("\n   ■■■■■   ");
            Console.WriteLine("  ■■■■■■■  ");
            Console.WriteLine(" ■■■■■■■■■ ");
            Console.WriteLine(" ■■■■■■■■■ ");
            Console.WriteLine("  ■■■■■■■  ");
        }

        public static string ToCapitalize(string word)
        {
            string formatted = word[0].ToString().ToUpper();
            formatted += word.Substring(1);

            return formatted;
        }
    }

    public class Treinador
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IList<string> Pokemons { get; set; } = new List<string>();
    }

    public class Pokemon
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public double height { get; set; }
        public double weight { get; set; }
        public int order { get; set; }
        public IList<PokemonAbilities> abilities { get; set; } = new List<PokemonAbilities>();
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