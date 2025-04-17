using basicShoppingCartMicroservice.Models;

namespace basicShoppingCartMicroservice.Client.ProductCatalog;

public interface IProductCatalogClient
{
    Task<IEnumerable<ShoppingCartItem>> RetrieveAllShoppingCartItems(int[] productCatalogIds);
}