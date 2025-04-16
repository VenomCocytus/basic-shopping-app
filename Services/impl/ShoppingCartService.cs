using System.ComponentModel.DataAnnotations.Schema;
using basicShoppingCartMicroservice.Models;
using basicShoppingCartMicroservice.Services;

public class ShoppingCartService : IShoppingCartService
{

    /*
     * Use an in-memory dictionary instead of a database for now
     */
    private static readonly Dictionary<int, ShoppingCart> Database = 
        new Dictionary<int, ShoppingCart>();
    
    public ShoppingCart RetrieveShoppingCart(int shoppingCartId)
    {
        return Database.TryGetValue(shoppingCartId, out var existingCart)
            ? existingCart
            : new ShoppingCart(userId: shoppingCartId);
    }

    public void Save(ShoppingCart shoppingCart) =>
        Database[shoppingCart.UserId] = shoppingCart;
}