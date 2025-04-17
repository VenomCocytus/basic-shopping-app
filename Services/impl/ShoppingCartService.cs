using basicShoppingCartMicroservice.Models;

namespace basicShoppingCartMicroservice.Services.impl;

public abstract class ShoppingCartService : IShoppingCartService
{

    /*
     * Use an in-memory dictionary instead of a database for now
     */
    private static readonly Dictionary<int, ShoppingCart> Database = 
        new Dictionary<int, ShoppingCart>();
    
    public ShoppingCart RetrieveShoppingCart(int shoppingCartId) =>
        Database.TryGetValue(shoppingCartId, out var existingCart)
            ? existingCart
            : new ShoppingCart(userId: shoppingCartId);

    public List<ShoppingCart> RetrieveAllShoppingCarts() =>
        Database.Values.ToList();

    public void Save(ShoppingCart shoppingCart) =>
        Database[shoppingCart.UserId] = shoppingCart;
}