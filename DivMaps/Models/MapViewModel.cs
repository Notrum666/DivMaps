namespace DivMaps.Models
{
    public class MapViewModel
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public List<DivCardViewModel> ValueSources { get; set; }
        public MapViewModel()
        {

        }
        public MapViewModel(string name, int value, List<DivCardViewModel> valueSources)
        {
            Name = name;
            Value = value;
            ValueSources = valueSources;
        }
    }
}
