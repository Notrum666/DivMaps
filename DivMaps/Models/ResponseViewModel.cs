namespace DivMaps.Models
{
    public class ResponseViewModel
    {
        public List<MapViewModel> Maps { get; set; }
        public int MaxCards { get; set; }
        public ResponseViewModel(List<MapViewModel> maps, int maxCards)
        {
            Maps = maps;
            MaxCards = maxCards;
        }
    }
}
