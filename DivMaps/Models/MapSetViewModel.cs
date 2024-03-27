namespace DivMaps.Models
{
    public class MapSetViewModel
    {
        public List<string> Maps { get; set; }
        public double ExpectedValue { get; set; }
        public List<DivCardViewModel> Pool { get; set; }
        public MapSetViewModel()
        {

        }
        public MapSetViewModel(List<string> maps, double expectedValue, List<DivCardViewModel> pool)
        {
            Maps = maps;
            ExpectedValue = expectedValue;
            Pool = pool;
        }
    }
}
