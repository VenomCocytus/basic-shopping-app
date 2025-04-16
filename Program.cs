var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var app = builder.Build();


// Configure the HTTP Request
// Redirect all HTTP requests to HTTPS
app.UseHttpsRedirection();
app.UseRouting();

// Add all endpoints in all controllers to MVCs route table
app.MapControllers();
app.Run();