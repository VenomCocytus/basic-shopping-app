using basicShoppingCartMicroservice.Models;

namespace basicShoppingCartMicroservice.Services;

public interface IShoppingCartService
{
    ShoppingCart RetrieveShoppingCart(int shoppingCartId);
    void Save(ShoppingCart shoppingCart);
    
}