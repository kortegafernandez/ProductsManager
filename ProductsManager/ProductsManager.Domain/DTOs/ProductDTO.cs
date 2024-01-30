using ProductsManager.Domain.Entities;

namespace ProductsManager.Domain.DTOs
{
    public  class ProductDTO : Entity
    {
        public string Name { get; set; } = string.Empty;
        public int StatusId { get; set; }
        public string StatusDescription { get; set; } = string.Empty;
        public int Stock { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
