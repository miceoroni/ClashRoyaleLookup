var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddSingleton(builder.Configuration["ClashRoyaleApiKey"]!);
var app = builder.Build();
app.UseStaticFiles();
app.MapControllers();
app.Run();