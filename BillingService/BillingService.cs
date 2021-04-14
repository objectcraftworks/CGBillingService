using System;
using System.Linq;

namespace BillingService
{
    public class BillCalculatorSelector
    {
        public decimal DefaultDrinksOnlyServiceChargePercentage => 0;
        public decimal DefaultServiceChargePercentage => 10;
        public decimal ServiceChargePercentage { get; private set; }
        public decimal DrinksOnlyServiceChargePercentage { get; private set; }

        public BillCalculatorSelector(decimal? serviceChargePercentage=null, decimal? drinksOnlyServiceChargePercentage=null)
        {
            ServiceChargePercentage = serviceChargePercentage ?? DefaultServiceChargePercentage;
            DrinksOnlyServiceChargePercentage = drinksOnlyServiceChargePercentage ?? DefaultDrinksOnlyServiceChargePercentage;
        }
        public  IBillCalculator GetCalculatorFor(Order order)
        {
            if (!order.HasItems())
                return new NoItemsBillCalculator();

            if (!order.HasFoodItems())
                return new BillCalculator(serviceChargePercentage: DrinksOnlyServiceChargePercentage);

            return new BillCalculator(serviceChargePercentage: ServiceChargePercentage);
        }
    }
    public interface IBillCalculator
    {

        void Calculate(Order order);
        decimal Surcharge { get; }
        decimal TotalBill { get; }
        decimal TotalOrdered { get; }

    }

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

    
    public class NoItemsBillCalculator : BillCalculatorBase
    {
        protected override decimal CalculateTotalOrdered(Order order)
        {
            return 0;
        }
        protected override decimal CalculateSurchage(decimal total)
        {
            return 0;
        }
    }


    public class Biller
    {
        IBillCalculator billCalculator;
       
        public Biller (IBillCalculator billCalculator ){

            this.billCalculator = billCalculator;

        }

        public OrderCheck Bill(Order order)
        {
            billCalculator.Calculate(order);
            return new OrderCheck(order, billCalculator.Surcharge, billCalculator.TotalOrdered, billCalculator.TotalBill);
        }
    }
}
