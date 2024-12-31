using Microsoft.AspNetCore.SignalR;
var builder = WebApplication.CreateBuilder(args);

// Add CORS services to the container
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowDevTunnels", policy =>
    {
        // Insert a dev tunnel here
        // to allow cors
        // 
        // policy.WithOrigins("https://origin.devtunnels.ms")
        //       .AllowAnyHeader()
        //       .AllowAnyMethod()
        //       .AllowCredentials(); // Necessary for SignalR
    });
});

// Add SignalR service
builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseRouting();

// Use the CORS policy
app.UseCors("AllowDevTunnels");

// Map the SignalR hub
app.MapHub<NotificationHub>("/notificationHub");

// Add a test endpoint
app.MapGet("/test", async (IHubContext<NotificationHub> hubContext) =>
{
    // Trigger an event on the notification channel
    await hubContext.Clients.All.SendAsync("ReceiveNotification", new { Message = "Hello World!", Id = "some-uuid" });
    return Results.Ok("Notification sent!");
});

app.Run();
