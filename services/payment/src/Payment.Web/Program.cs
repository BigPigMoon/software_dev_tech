using Microsoft.EntityFrameworkCore;
using Payment.Web.Application;
using Payment.Web.Components;
using Payment.Web.Infrastructure.Database;
using Payment.Web.Infrastructure.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddHttpClients();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<PaymentDbContext>();
    dbContext.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.Use(async (context, next) =>
{
    var ipAddress = context.Connection.RemoteIpAddress?.ToString();
    var timeStamp = DateTime.UtcNow.ToString("O");
    var logMessage = $"{timeStamp} - {ipAddress}";

    var logFilePath = Environment.GetEnvironmentVariable("LOG_FILE_PATH");
    if (!string.IsNullOrEmpty(logFilePath))
    {
        await using var stream = new FileStream(logFilePath, FileMode.Append, FileAccess.Write, FileShare.None);
        await using var writer = new StreamWriter(stream);
        await writer.WriteLineAsync(logMessage);
    }

    await next.Invoke();
});

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();