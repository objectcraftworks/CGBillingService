using Microsoft.VisualStudio.TestTools.UnitTesting;
using BillingService;
using System;
using System.Collections.Generic;

namespace BillingServiceTests
{
    [TestClass]
    public class BillerTest
    {
        /*
         * Billing Service
Scenario
Cafe X menu consists of the following items:
Cola - Cold – 50 cents
Coffee - Hot - $1.00
Cheese Sandwich - Cold - $2.00
Steak Sandwich - Hot - $4.50
 
Customers don’t know how much to tip and staff need tips to survive!
Write some code to generate a bill including a service charge so customers don’t have to work out how much to tip.
Exercise part 1 – Service Charge (25 Minutes)
Pass in a list of purchased items that produces a total bill
When all purchased items are drinks no service charge is applied
When purchased items include any food apply a service charge of 10% to the total bill(rounded to 2 decimal places)
*/

        static MenuItem cola = new(new Drink("Cola", VictualServingTemperature.Cold), 0.5m);
        static MenuItem coffee = new(new Drink("Coffee", VictualServingTemperature.Hot), 1.0m);
        static MenuItem cheeseSandwich = new(new Food("Cheese Sandwich", VictualServingTemperature.Cold), 2.0m);
        static MenuItem steakSandwich = new(new Food("Steak Sandwich", VictualServingTemperature.Hot), 4.5m);

        Menu menu = new Menu( new List<MenuItem> { cola, coffee, cheeseSandwich, steakSandwich });

        Biller biller;
        BillCalculatorSelector billCalculatorSelector;
        Order order;

        [TestInitialize]
        public void Init()
        {
            order = new Order();

        }
        [TestMethod]
        public void ShouldNotChargeWhenNoItemIsOnTheOrder()
        {
            InitBillerFor(order);

            OrderCheck check = biller.Bill(order);
            Assert.AreEqual(check.ServiceCharge, 0);
            Assert.AreEqual(check.TotalBill, 0);
        }


        [TestMethod]
        public void ShouldChargeWhenAnItemIsOnTheOrder()
        {
            var cheeseSandwichQuantity = 1;

            order.AddItem(new(cheeseSandwich, 1));

            Assert.IsTrue(order.HasFoodItems());

            InitBillerFor(order);
            var totalOrdered = cheeseSandwich.Price* cheeseSandwichQuantity;
            decimal actualTotalBill = totalOrdered + (totalOrdered * billCalculatorSelector.ServiceChargePercentage / 100);

            OrderCheck check = biller.Bill(order);
            Assert.AreEqual(check.TotalOrdered, totalOrdered);
            Assert.AreEqual(check.TotalBill, actualTotalBill);

        }

        [TestMethod]
        public void ShouldAddServiceChargeWhenAFoodItemIsOnTheOrder()
        {
            var cheeseSandwichQuantity = 1;
            var colaQuantity = 2;
            Order(cheeseSandwich, cheeseSandwichQuantity);
            Order(cola, colaQuantity);
          

            InitBillerFor(order);
            var expectedTotalOrdered = (cheeseSandwich.Price * cheeseSandwichQuantity + cola.Price * colaQuantity);

            var expectedServiceCharge = expectedTotalOrdered * billCalculatorSelector.ServiceChargePercentage / 100;

            OrderCheck check = biller.Bill(order);
            Assert.AreEqual( check.ServiceCharge, expectedServiceCharge);
            Assert.AreEqual(check.TotalOrdered,  expectedTotalOrdered);

            Assert.AreEqual(check.TotalBill, expectedServiceCharge + expectedTotalOrdered);



        }

        [TestMethod]
        public void ShouldNotAddServiceChargeWhenDrinksOnlyOnTheOrder()
        {
            var quantity = 2;
            Order(cola, quantity);
            Order(coffee, quantity);

            var expectedTotal = cola.Price * quantity + coffee.Price * quantity;

            InitBillerFor(order);

            OrderCheck check = biller.Bill(order);
            Assert.AreEqual(  expectedTotal, check.TotalBill);
            Assert.AreEqual(0, check.ServiceCharge);
            Assert.AreEqual(check.TotalOrdered, check.TotalBill);

        }

        [TestMethod]
        public void ShouldRoundTo2DecimalsAllCharges()
        {
            var quantity = 2;
           var serviceChargePercentage = 10.7m;
            Order(cola, quantity);
            Order(cheeseSandwich, quantity);

            var expectedTotalOrdered = cola.Price * quantity + cheeseSandwich.Price * quantity;
            var expectedSurcharge =expectedTotalOrdered * serviceChargePercentage/100;
            var expectedTotal = decimal.Round(expectedTotalOrdered + expectedSurcharge,2);

            
            InitBillerFor(order,serviceChargePercentage);


            OrderCheck check = biller.Bill(order);
            Assert.AreEqual(expectedTotal, check.TotalBill);
            Assert.AreEqual(expectedSurcharge, check.ServiceCharge);
            Assert.AreEqual(expectedTotalOrdered, check.TotalOrdered);
        }


        private void InitBillerFor(Order order, decimal? serviceChargePercentage=null)
        {
            billCalculatorSelector = new(serviceChargePercentage);
            var calculator = billCalculatorSelector.GetCalculatorFor(order);
            biller = new(calculator);
        }

        private Order Order(MenuItem menuItem, int quantity)
        {
            order.AddItem(new(menuItem, quantity));
            return order;
        }
    }


}
