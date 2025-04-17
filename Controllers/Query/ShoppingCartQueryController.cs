using basicShoppingCartMicroservice.Models;
using basicShoppingCartMicroservice.Services;
using Lombok.NET;
using Microsoft.AspNetCore.Mvc;

namespace basicShoppingCartMicroservice.Controllers.Query;

[RequiredArgsConstructor]
[Route("/shopping-cart")]
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