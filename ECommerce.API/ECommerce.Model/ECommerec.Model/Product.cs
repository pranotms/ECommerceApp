namespace ECommerceApp.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }=string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }=string.Empty;
        public int Status { get; set; }
    }
}
