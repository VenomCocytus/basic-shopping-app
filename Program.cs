    using basicShoppingCartMicroservice.Services;

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    // builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
    
    
    builder.Services.Scan(selector => selector
        .FromAssemblyOf<IShoppingCartService>()
        .AddClasses()
        .AsMatchingInterface()
        .WithScopedLifetime());

    var app = builder.Build();

    // Configure the HTTP Request
    // Redirect all HTTP requests to HTTPS
    app.UseHttpsRedirection();
    app.UseRouting();

    // Add all endpoints in all controllers to MVCs route table
    app.MapControllers();
    app.Run();