namespace PokemonApi.Model
{
    public class Pokemon
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public double height { get; set; }
        public double weight { get; set; }
        public int order { get; set; }
        public IList<PokemonAbilities> abilities { get; set; } = new List<PokemonAbilities>();
    }
}
