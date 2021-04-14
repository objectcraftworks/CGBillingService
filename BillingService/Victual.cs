namespace BillingService
{
    public class Victual
    {
        public Victual(string name, VictualServingTemperature servingTemperature)
        {
            Name = name;
            ServingTemperature = servingTemperature;
        }

        public string Name { get; private set; }
        public VictualServingTemperature ServingTemperature { get; private set; }
    }
}
