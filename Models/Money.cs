namespace basicShoppingCartMicroservice.Models;

public record Money(
    string Currency,
    decimal Amount){}