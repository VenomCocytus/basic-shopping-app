using basicShoppingCartMicroservice.Models;
using basicShoppingCartMicroservice.Services;
using Microsoft.AspNetCore.Mvc;

namespace basicShoppingCartMicroservice.Controllers.Query;

[Route("/shoppingcart")]
public class ShoppingCartQueryController(ShoppingCartService shoppingCartService) : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService = shoppingCartService;

    [HttpGet("userId:int")]
    public ShoppingCart RetrieveShoppingCart(int userId) =>
        this._shoppingCartService.RetrieveShoppingCart(userId);
    
    
}