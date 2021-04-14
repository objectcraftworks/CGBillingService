namespace BillingService
{
    public class BillCalculator : BillCalculatorBase
    {
        public decimal ServiceChargePercentage { get; private set; }
        public BillCalculator(decimal serviceChargePercentage)
        {
            ServiceChargePercentage = serviceChargePercentage;
        }



        protected override decimal CalculateSurchage(decimal total)
        {
            return total * ServiceChargePercentage / 100;
        }
    }
}
