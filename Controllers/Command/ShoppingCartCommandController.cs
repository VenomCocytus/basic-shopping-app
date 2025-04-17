using basicShoppingCartMicroservice.Models;
using basicShoppingCartMicroservice.Services;
using Lombok.NET;
using Microsoft.AspNetCore.Mvc;

namespace basicShoppingCartMicroservice.Controllers.Command;

[RequiredArgsConstructor]
[Route("/shopping-cart/{userId:int}/items")]
public partial class ShoppingCartCommandController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;

    [HttpPost]
    public Task<ShoppingCart> AddItemsToCart(int userId, [FromBody] int[] productIds) =>
        this._shoppingCartService.AddItemsToCart(userId, productIds);
    
    [HttpDelete]
    public ShoppingCart RemoveItemsFromCart(int userId, [FromBody] int[] productIds) =>
        this._shoppingCartService.RemoveItemsFromCart(userId, productIds);
}