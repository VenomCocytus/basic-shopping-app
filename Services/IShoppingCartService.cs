using basicShoppingCartMicroservice.Models;

namespace basicShoppingCartMicroservice.Services;

public interface IShoppingCartService
{
    void Save(ShoppingCart shoppingCart);
    
    ShoppingCart RetrieveUserShoppingCart(int userId);
    List<ShoppingCart> RetrieveAllShoppingCarts();
    
    Task<ShoppingCart> AddItemsToCart(int userId, int[] shoppingCartItemIds);
    ShoppingCart RemoveItemsFromCart(int userId, int[] shoppingCartItemIds);
    
}