using System.Net.Http.Headers;
using System.Text.Json;
using basicShoppingCartMicroservice.Models;

namespace basicShoppingCartMicroservice.Client.ProductCatalog;

public abstract class ProductCatalogClient : IProductCatalogClient
{
    
    private readonly HttpClient _httpClient;
    private const string ProductCatalogBaseUrl = "https://catalog.catalog.core.windows.net";
    private const string GetProductPathTemplate = "?productIds=[{0}]";

    // Injecting HttpClient
    protected ProductCatalogClient(HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri(ProductCatalogBaseUrl);
        
        /*
         * Configure the Http Client to accept JSON responses
         * from the product catalog
         */
        httpClient
            .DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        this._httpClient = httpClient;
    }

    public async Task<IEnumerable<ShoppingCartItem>> RetrieveAllShoppingCartItems(int[] productCatalogIds)
    {
        using var responseMessage = await RequestProductsFromProductCatalogService(productCatalogIds);

        return await ConvertToShoppingCartItems(responseMessage);
    }

    private async Task<HttpResponseMessage> RequestProductsFromProductCatalogService(int[] productCatalogIds)
    {
        
        var productsResource = string.Format(GetProductPathTemplate, 
            string.Join(",", productCatalogIds));
        
        /*
         * Tells HttpClient to perform the HTTP GET asynchronously
         */
        return await this._httpClient.GetAsync(productsResource);
    }

    private static async Task<IEnumerable<ShoppingCartItem>> ConvertToShoppingCartItems(HttpResponseMessage responseMessage)
    {
        responseMessage.EnsureSuccessStatusCode();
        
        // Deserializing the JSON from the product catalog microservice
        var retrievedProducts = await JsonSerializer.DeserializeAsync<List<CatalogProduct>>(
            await responseMessage.Content.ReadAsStreamAsync(),
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new List<CatalogProduct>();
        
        // Creating a shopping cart item for each product retrieve from the catalog microservice
        return retrievedProducts.Select(product => new ShoppingCartItem(
                product.ProductId,
                product.Name,
                product.Description,
                product.Price
            ));
    }

    private abstract record CatalogProduct(
        int ProductId,
        string Name,
        string Description,
        Money Price);

}