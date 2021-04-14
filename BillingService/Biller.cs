namespace BillingService
{
    public class Biller
    {
        IBillCalculator billCalculator;

        public Biller(IBillCalculator billCalculator)
        {

            this.billCalculator = billCalculator;

        }

        public OrderCheck Bill(Order order)
        {
            billCalculator.Calculate(order);
            return new OrderCheck(order, billCalculator.Surcharge, billCalculator.TotalOrdered, billCalculator.TotalBill);
        }
    }
}
