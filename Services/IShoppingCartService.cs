using basicShoppingCartMicroservice.Models;

namespace basicShoppingCartMicroservice.Services;

public interface IShoppingCartService
{
    ShoppingCart RetrieveShoppingCart(int shoppingCartId);
    List<ShoppingCart> RetrieveAllShoppingCarts();
    void Save(ShoppingCart shoppingCart);
    
}