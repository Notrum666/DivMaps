namespace DivMaps.Models
{
    public class DivCardViewModel
    {
        public string Name { get; set; }
        public int ChaosValue { get; set; }
        public DivCardViewModel(string name, int chaosValue) 
        {
            Name = name;
            ChaosValue = chaosValue;
        }
    }
}
