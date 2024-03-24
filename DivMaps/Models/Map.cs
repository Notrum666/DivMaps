namespace DivMaps.Models
{
    public class Map
    {
        public string Name { get; set; }
        public List<DivCard> DivCards { get; set; }
        public Map(string name, List<DivCard> divCards)
        {
            Name = name;
            DivCards = divCards;
        }
    }
}
