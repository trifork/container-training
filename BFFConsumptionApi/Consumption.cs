namespace BFFConsumptionApi
{
    public class Consumption
    {
        public double Value { get; set; }
        public ConsumptionType Unit { get; set; }
    }

    public enum ConsumptionType
    {
        Kwh,
        Wh
    }
}
