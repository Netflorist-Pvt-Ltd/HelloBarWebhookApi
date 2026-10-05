using HelloBarWebhookApi.Controllers;
using HelloBarWebhookApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IHelloBarService, HelloBarService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();