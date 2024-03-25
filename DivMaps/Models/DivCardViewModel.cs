namespace DivMaps.Models
{
    public class DivCardViewModel
    {
        public string Name { get; set; }
        public double Value { get; set; }
        public double ChaosValue { get; set; }
        public double StackSizeCoef { get; set; }
        public double DropRateCoef { get; set; }
        public DivCardViewModel(string name, double value, double chaosValue, double stackSizeCoef, double dropRateCoef) 
        {
            Name = name;
            Value = value;
            ChaosValue = chaosValue;
            StackSizeCoef = stackSizeCoef;
            DropRateCoef = dropRateCoef;
        }
    }
}
