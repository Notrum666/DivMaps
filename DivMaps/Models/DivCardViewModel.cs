namespace DivMaps.Models
{
    public class DivCardViewModel
    {
        public string Name { get; set; }
        public double ChaosValue { get; set; }
        public int StackSize { get; set; }
        public double DropWeight { get; set; }
        public double Value { get; set; }
        public DivCardViewModel(string name, double chaosValue, int stackSize, double dropWeight, double value) 
        {
            Name = name;
            ChaosValue = chaosValue;
            StackSize = stackSize;
            DropWeight = dropWeight;
            Value = value;
        }
    }
}
