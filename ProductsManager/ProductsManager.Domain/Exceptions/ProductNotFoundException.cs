namespace ProductsManager.Domain.Exceptions
{
    public class ProductNotFoundException : Exception
    {
        public ProductNotFoundException(int productId) : base(productId.ToString())
        {
            
        }
    }
}
