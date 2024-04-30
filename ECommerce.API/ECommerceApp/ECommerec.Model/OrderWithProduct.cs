﻿namespace ECommerceApp.Model
{
    public class OrderWithProduct
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public Product Product { get; set; }
        

    }
}
