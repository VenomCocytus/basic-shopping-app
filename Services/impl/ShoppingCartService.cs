using basicShoppingCartMicroservice.Models;
using Lombok.NET;

namespace basicShoppingCartMicroservice.Services.impl;

[RequiredArgsConstructor]
public partial class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartService _shoppingCartService;
    private readonly IProductCatalogClient _productCatalogClient;
    private readonly IEventService _eventService;
    
    /*
     * Use an in-memory dictionary instead of a database for now
     */
    private static readonly Dictionary<int, ShoppingCart> Database = 
        new Dictionary<int, ShoppingCart>();
    
    public ShoppingCart RetrieveUserShoppingCart(int userId) =>
        Database.TryGetValue(userId, out var existingCart)
            ? existingCart
            : new ShoppingCart(userId: userId);

    public List<ShoppingCart> RetrieveAllShoppingCarts() =>
        Database.Values.ToList();

    public void Save(ShoppingCart shoppingCart) =>
        Database[shoppingCart.UserId] = shoppingCart;

    public async Task<ShoppingCart> AddItemsToCart(int userId, int[] shoppingCartItemIds)
    {
        var shoppingCart = RetrieveUserShoppingCart(userId);
        
        // Fetch the catalogue information from the Product Catalog microservice
        var shoppingCartItems =
            await this._productCatalogClient.RetrieveShoppingCartItems(shoppingCartItemIds);
        
        // Adding items to the cart
        shoppingCart.AddItemsToCart(shoppingCartItems, _eventService);
        
        // Save the updated cart to the data store
        this._shoppingCartService.Save(shoppingCart);
        
        // Return the updated cart
        return shoppingCart;
    }

    public ShoppingCart RemoveItemsFromCart(int userId, int[] shoppingCartItemIds)
    {
        var shoppingCart = RetrieveUserShoppingCart(userId);
        
        // Removing items to the shopping cart
        shoppingCart.RemoveItemsFromCart(shoppingCartItemIds, _eventService);
        
        // Save the updated cart to the data store
        this._shoppingCartService.Save(shoppingCart);

        return shoppingCart;
    }
}