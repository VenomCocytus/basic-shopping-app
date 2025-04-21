using basicShoppingCartMicroservice.EventFeed.Interface;

namespace basicShoppingCartMicroservice.Models;

public class ShoppingCart(int userId)
{
    private readonly HashSet<ShoppingCartItem> _shoppingCartItems = new();
    
    public int UserId { get; } = userId;
    public IEnumerator<ShoppingCartItem> Items => this._shoppingCartItems.GetEnumerator();

    public void AddItems(IEnumerable<ShoppingCartItem> shoppingCartItems, IEventService eventService)
    {
        foreach (var cartItem in shoppingCartItems)
            if(this._shoppingCartItems.Add(cartItem))
                eventService.Raise("ShoppingCartItemAdded", new {UserId, cartItem});
    }

    public void RemoveItems(int[] catalogueIds, IEventService eventService)
    {
        // Find cart item to remove
        var itemsToRemove = _shoppingCartItems
            .Where(cartItem => catalogueIds.Contains(cartItem.CatalogueId));
        
        foreach (var cartItem in itemsToRemove)
            if(this._shoppingCartItems.Remove(cartItem))
                eventService.Raise("ShoppingCartItemRemoved", new {UserId, cartItem});
    }
}