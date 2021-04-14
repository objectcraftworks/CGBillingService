using System;
using System.Linq;

namespace BillingService
{

    public class OrderCheck
    {
        public OrderCheck(Order order, decimal serviceCharge, decimal totalOrdered, decimal totalBill)
        {
            Order = order;
            TotalOrdered = totalOrdered;
            ServiceCharge = serviceCharge;
            TotalBill = totalBill;
        }

        public Order Order { get; private set; }
        public decimal ServiceCharge { get; private  set; }
        public decimal TotalBill { get; private set; }
        public decimal TotalOrdered { get; private set; }

    }
}
