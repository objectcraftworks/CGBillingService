using System;
using System.Collections.Generic;
using System.Linq;

namespace BillingService
{
    
    public enum VictualServingTemperature
    {
        Hot,
        Cold
    }
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

    public class Drink : Victual
    {
        public Drink(string name, VictualServingTemperature servingTemperature) : base(name, servingTemperature)
        {

        } 
    }

    public class Food : Victual
    {
        public Food(string name, VictualServingTemperature servingTemperature) : base(name, servingTemperature)
        {

        }
    }
    public class MenuItem
    {
        public MenuItem(Victual victual, decimal price)
        {
            Victual = victual;
            Price = price;
        }

        public Victual Victual { get; private set; }
        public decimal Price { get; private set; }

        
    }
    public class OrderItem
    {
        public int Quantity { get; set; }
        public MenuItem MenuItem { get; set; }
        public OrderItem (MenuItem item,  int quantity)
        {
            MenuItem = item;
            Quantity = quantity;
        }
    }

    public class Order
    {
        private IList<OrderItem> items = new List<OrderItem>();
        public IList<OrderItem> Items { get => items; }

        public Order AddItem(OrderItem item)
        {
            Items.Add(item);
            return this;
        }
        public void RemoveItem(OrderItem item)
        {
            Items.Remove(item);
        }

       

        public bool HasFoodItems()
        {
           return Items.Any(item => item.MenuItem.Victual is Food);
        }

        public bool HasItems()
        {
            return items.Count !=0;
        }
    }

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

    public class Menu
    {
        IList<MenuItem> items;
        IList<MenuItem> Items => items;
        public Menu(IList<MenuItem> items) 
        {
            this.items = items;
        }
    }
}
