﻿namespace ProductsManager.Domain.Entities
{
    public class Product : Entity
    {
        public string Name { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
