using basicShoppingCartMicroservice.Models;
using basicShoppingCartMicroservice.Services;
using basicShoppingCartMicroservice.Services.impl;
using Lombok.NET;
using Microsoft.AspNetCore.Mvc;

namespace basicShoppingCartMicroservice.Controllers.Query;

[RequiredArgsConstructor]
[Route("/shoppingcart")]
public partial class ShoppingCartQueryController : ControllerBase
{
    
    private readonly IShoppingCartService _shoppingCartService;
        
    [HttpGet("userId:int")]
    public ShoppingCart RetrieveShoppingCart(int userId) => 
        _shoppingCartService.RetrieveShoppingCart(userId);
    
    [HttpGet]
    public List<ShoppingCart> RetrieveAllShoppingCarts() =>
        _shoppingCartService.RetrieveAllShoppingCarts();
    
    
}