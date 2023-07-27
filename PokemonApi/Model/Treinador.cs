namespace PokemonApi.Model
{
    public class Treinador
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public IList<string> Pokemons { get; set; } = new List<string>();
    }
}
