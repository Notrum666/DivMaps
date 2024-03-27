namespace DivMaps.Models
{
    public class ResponseViewModel
    {
        public List<MapSetViewModel> MapSets { get; set; }
        public bool Scarab { get; set; }
        public double MinValue { get; set; }
        public ResponseViewModel(List<MapSetViewModel> mapSets, bool scarab, double minValue)
        {
            MapSets = mapSets;
            Scarab = scarab;
            MinValue = minValue;
        }
    }
}
