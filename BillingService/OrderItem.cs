namespace BillingService
{
    public class OrderItem
    {
        public int Quantity { get; set; }
        public MenuItem MenuItem { get; set; }
        public OrderItem(MenuItem item, int quantity)
        {
            MenuItem = item;
            Quantity = quantity;
        }
    }
}
