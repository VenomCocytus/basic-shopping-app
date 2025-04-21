    using basicShoppingCartMicroservice.Client.ProductCatalog;
    using Polly;
    using Scrutor;

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.Services.AddControllers();
    
    // Scanning services and classes
    // builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
    
    // Use Scrutor to scan services and classes
    var assemblies = AppDomain.CurrentDomain.GetAssemblies()
        .Where(assembly => !assembly.IsDynamic && 
                           assembly.GetName().Name!.StartsWith("basicShoppingCartMicroservice"))
        .ToArray();
    
    builder.Services.Scan(selector => selector
        .FromAssemblies(assemblies)
        // To register a specific assembly    
        // .FromAssemblyOf<IShoppingCartService>()
        .AddClasses(classes => 
                // Optional: Filter by name
            classes.Where(type => type.Name.EndsWith("Service") 
                                  || type.Name.EndsWith("Repository")), 
            // Optional: Choose only public classes or not (abstract etc...)
            publicOnly: false)
        .UsingRegistrationStrategy(RegistrationStrategy.Skip)
        .AsMatchingInterface()
        .WithScopedLifetime());
    
    // Registering Http client
    builder.Services.AddHttpClient<IProductCatalogClient, ProductCatalogClient>()
        //  Adding a retry policy with Polly to retry any failed calls
        .AddTransientHttpErrorPolicy(policy => 
            policy.WaitAndRetryAsync(3, retryAttempt => 
                TimeSpan.FromMilliseconds(100*Math.Pow(2, retryAttempt))));

    var app = builder.Build();

    // Configure the HTTP Request
    // Redirect all HTTP requests to HTTPS
    app.UseHttpsRedirection();
    app.UseRouting();

    // Add all endpoints in all controllers to MVCs route table
    app.MapControllers();
    app.Run();