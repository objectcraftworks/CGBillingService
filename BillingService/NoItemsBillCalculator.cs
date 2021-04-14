using System;

namespace BillingService
{


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
}
