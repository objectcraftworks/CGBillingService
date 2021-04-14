using System.Linq;

namespace BillingService
{
    public abstract class BillCalculatorBase : IBillCalculator
    {
        public decimal Surcharge => surcharge;
        public decimal TotalBill => totalBill;
        public decimal TotalOrdered => totalOrdered;


        protected decimal surcharge, totalOrdered, totalBill;

        public virtual void Calculate(Order order)
        {
            totalOrdered = CalculateTotalOrdered(order);
            surcharge = CalculateSurchage(totalOrdered);
            totalBill = decimal.Round(totalOrdered + surcharge, 2);
        }

        protected virtual decimal CalculateTotalOrdered(Order order)
        {
            return order.Items.Sum(i => i.MenuItem.Price * i.Quantity);
        }

        protected abstract decimal CalculateSurchage(decimal totalOrdered);
    }
}
