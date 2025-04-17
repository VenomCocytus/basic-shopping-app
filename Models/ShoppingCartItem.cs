namespace basicShoppingCartMicroservice.Models;

public abstract record ShoppingCartItem(
    int CatalogueId,
    string Name,
    string Description,
    Money Price)
{
    public virtual bool Equals(ShoppingCartItem? obj) =>
        obj != null && this.CatalogueId.Equals(obj.CatalogueId);
    public override int GetHashCode() =>
        this.CatalogueId.GetHashCode();
}