namespace DivMaps.Models
{
    public class ResponseViewModel
    {
        public List<MapViewModel> Maps { get; set; }
        public int MaxCards { get; set; }
        public bool StackSizeCoef { get; set; }
        public bool DropRateCoef { get; set; }
        public ResponseViewModel(List<MapViewModel> maps, int maxCards, bool stackSizeCoef, bool dropRateCoef)
        {
            Maps = maps;
            MaxCards = maxCards;
            StackSizeCoef = stackSizeCoef;
            DropRateCoef = dropRateCoef;
        }
    }
}
