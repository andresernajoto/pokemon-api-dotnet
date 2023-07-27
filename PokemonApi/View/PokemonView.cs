using PokemonApi.Model;
using PokemonApi.Controller;
using PokemonApi.Service;

namespace PokemonApi.View
{
    public class PokemonView
    {
        private static readonly PokemonService _service = new ();
        private static readonly PokemonController _controller = new ();

        public void Inicio()
        {
            Treinador treinador = new();
            ImprimeBoasVindas();

            Console.Write("\nDigite seu nome de treinador(a): ");
            treinador.Name = Console.ReadLine();
            treinador.Name = _controller.ValidaNomeTreinador(treinador.Name);

            ImprimeOpcoesInicial();
            int opcaoInicial = int.Parse(Console.ReadLine());
            OpcoesIncial(opcaoInicial, treinador);
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

        public static void ImprimeOpcoesInicial()
        {
            Console.WriteLine("\n1 - Adotar um Pokemon");
            Console.WriteLine("2 - Ver Pokemons adotados");
            Console.WriteLine("3 - Sair");
            Console.Write("\nEscolha uma das opções acima: ");
        }

        public static void ImprimeDetalhesPokemon(Pokemon pokemonInfo)
        {
            Console.WriteLine($"\nPokemon: {_controller.ToCapitalize(pokemonInfo.name)}");
            Console.WriteLine($"N° na Pokedex: {pokemonInfo.order}");
            Console.WriteLine($"Altura: {pokemonInfo.height / 10.0} m");
            Console.WriteLine($"Peso: {pokemonInfo.weight / 10.0} kg");
            Console.WriteLine("Habilidades: ");

            for (int i = 0; i < pokemonInfo.abilities.Count; i++)
            {
                Console.WriteLine($"-> {_controller.ToCapitalize(pokemonInfo.abilities[i].ability.name)}");
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

        public void OpcoesIncial(int opcoes, Treinador treinador)
        {
            switch (opcoes)
            {
                case 1:
                    Console.WriteLine("\n1 - Escolher o Pokemon");
                    Console.WriteLine("2 - Voltar");

                    Console.Write("\nEscolha uma das opções acima: ");
                    opcoes = int.Parse(Console.ReadLine());

                    OpcoesAdocao(opcoes, treinador);

                    break;
                case 2:
                    if (treinador.Pokemons.Count > 0)
                    {
                        Console.WriteLine("\nLista de Pokemons adotados:");

                        foreach (string pokemon in treinador.Pokemons)
                        {
                            Console.WriteLine($"-> {_controller.ToCapitalize(pokemon)}");
                        }

                        Console.Write("\nDeseja fazer algo a mais? (y/n): ");
                        char opcaoDetalhes = Console.ReadLine()[0];

                        if (opcaoDetalhes.ToString().ToLower() == "y")
                        {
                            ImprimeOpcoesInicial();

                            opcoes = int.Parse(Console.ReadLine());
                            OpcoesIncial(opcoes, treinador);
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
                        ImprimeOpcoesInicial();

                        opcoes = int.Parse(Console.ReadLine());
                        OpcoesIncial(opcoes, treinador);

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
                    OpcoesIncial(opcoes, treinador);

                    break;
            }
        }

        public void OpcoesAdocao(int opcoes, Treinador treinador)
        {
            switch (opcoes)
            {
                case 1:
                    Console.Write("\nDigite o nome do Pokemon que deseja adotar: ");
                    string pokemonName = Console.ReadLine().ToLower();

                    Pokemon pokemonInfo = _service.GetPokemonResponse(pokemonName);
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
                        ImprimeOpcoesInicial();

                        opcoes = int.Parse(Console.ReadLine());
                        OpcoesIncial(opcoes, treinador);
                    }
                    else
                    {
                        ImprimeOvoPokemon();
                    }

                    break;
                case 2:
                    ImprimeOpcoesInicial();

                    opcoes = int.Parse(Console.ReadLine());
                    OpcoesIncial(opcoes, treinador);

                    break;
                default:
                    Console.Write("\nOpção inválida! Digite novamente: ");

                    opcoes = int.Parse(Console.ReadLine());
                    OpcoesIncial(opcoes, treinador);

                    break;
            }
        }
    }
}