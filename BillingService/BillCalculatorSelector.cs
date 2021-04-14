namespace BillingService
{
    public class BillCalculatorSelector
    {
        public decimal DefaultDrinksOnlyServiceChargePercentage => 0;
        public decimal DefaultServiceChargePercentage => 10;
        public decimal ServiceChargePercentage { get; private set; }
        public decimal DrinksOnlyServiceChargePercentage { get; private set; }

        public BillCalculatorSelector(decimal? serviceChargePercentage = null, decimal? drinksOnlyServiceChargePercentage = null)
        {
            ServiceChargePercentage = serviceChargePercentage ?? DefaultServiceChargePercentage;
            DrinksOnlyServiceChargePercentage = drinksOnlyServiceChargePercentage ?? DefaultDrinksOnlyServiceChargePercentage;
        }
        public IBillCalculator GetCalculatorFor(Order order)
        {
            if (!order.HasItems())
                return new NoItemsBillCalculator();

            if (!order.HasFoodItems())
                return new BillCalculator(serviceChargePercentage: DrinksOnlyServiceChargePercentage);

            return new BillCalculator(serviceChargePercentage: ServiceChargePercentage);
        }
    }
}
