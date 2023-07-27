using PokemonApi.Model;
using PokemonApi.Service;
using System.Text.RegularExpressions;

namespace PokemonApi.Controller
{
    public class PokemonController
    {
        public string ValidaNomeTreinador(string nome)
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

        public string ToCapitalize(string word)
        {
            string formatted = word[0].ToString().ToUpper();
            formatted += word.Substring(1);

            return formatted;
        }
    }
}
