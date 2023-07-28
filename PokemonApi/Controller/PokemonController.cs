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

            if (nome.Length < 5)
            {
                Console.WriteLine("O nome de treinador(a) deve conter mais que 5 caracteres!");
                
                while (nome.Length < 5)
                {
                    Console.Write("\nDigite novamente: ");
                    nome = Console.ReadLine();
                }
            }

            Console.WriteLine($"\nSeja bem vindo(a), treinador(a) {nome}!");

            return nome;
        }

        public int ValidaOpcoes(string opcao)
        {
            int opcaoDesejada = 0;
            bool isValid = false;
            

            while (!isValid)
            {
                if (int.TryParse(opcao, out opcaoDesejada))
                {
                    isValid = true;

                    return opcaoDesejada;
                }

                Console.Write("\nA opção digitada não é um número, tente novamente! ");
                opcao = Console.ReadLine();

                return ValidaOpcoes(opcao);
            }

            return opcaoDesejada;
        }

        public string ValidaEscolha(char escolha)
        { 
            if (escolha.ToString().ToLower() == "y")
            {
                return escolha.ToString().ToLower();
            }
            else if (escolha.ToString().ToLower() == "n")
            {
                return escolha.ToString().ToLower();
            }
            else
            {
                Console.WriteLine($"\nA escolha pode ser apenas {"y"} ou {"n"}!");
                
                Console.Write("Digite novamente: ");
                char novaEscolha = Console.ReadLine()[0];

                return ValidaEscolha(novaEscolha);
            }
        }

        public string ToCapitalize(string word)
        {
            string formatted = word[0].ToString().ToUpper();
            formatted += word.Substring(1);

            return formatted;
        }
    }
}
