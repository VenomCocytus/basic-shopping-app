namespace basicShoppingCartMicroservice.Models;

public class ShoppingCart(int userId)
{
    private readonly HashSet<ShoppingCartItem> _shoppingCartItems = new();
    
    public int UserId { get; } = userId;
    public IEnumerator<ShoppingCartItem> Items => this._shoppingCartItems.GetEnumerator();

    public void AddItemsToCart(IEnumerable<ShoppingCartItem> shoppingCartItems)
    {
        foreach (var cartItem in shoppingCartItems)
            this._shoppingCartItems.Add(cartItem);
    }

    public void RemoveItemsFromCart(int[] catalogueIds)
    {
        this._shoppingCartItems
            .RemoveWhere(cartItem => 
                catalogueIds.Contains(cartItem.CatalogueId));
    }
}