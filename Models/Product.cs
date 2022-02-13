﻿namespace WebApp.Models
{

    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }
        public bool Price { get; set; }
        public bool IsSale { get; set; }
    }

}
