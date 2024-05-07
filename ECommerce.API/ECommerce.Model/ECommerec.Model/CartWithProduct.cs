namespace ECommerceApp.Model
{
    public class CartWithProduct
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
    }
}
