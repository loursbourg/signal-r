# SignalR Notification Example

Basic SignalR application with a test endpoint to trigger notifications.

- SignalR hub to manage real-time notifications.
- Test endpoint (`/test`) to verify notifications are working.
- Configurable CORS policy to allow specific origins.


### Prerequisites
- .NET 6 or later
- Package: `Microsoft.AspNetCore.SignalR`
- A SignalR client for testing (e.g., a JavaScript client).

### Running the Application
1. Clone.
2. Build and run the application:
   ```bash
   dotnet run
   ```
3. By default, the SignalR hub is hosted at `/ws-channel`, and the test endpoint is at `/test`.

### CORS Configuration
To ensure the application works as expected, you need to configure CORS to allow requests from your client application.

In `Program.cs`, the CORS policy is defined as:
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowDevTunnels", policy =>
    {
        policy.WithOrigins("<YOUR_CLIENT_URL>")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
```
Replace `<YOUR_CLIENT_URL>` with the actual URL of your client application. For example:
```csharp
policy.WithOrigins("https://example-client.com")
```

### Testing Notifications
#### Test Endpoint
A test endpoint is available at `/test` to trigger notifications for all connected clients.

- **URL**: `/test`
- **Method**: GET
- **Response**: `200 OK` with the message `"Notification sent!"`

When the endpoint is called, it broadcasts the following message to all connected clients:
```json
{
  "Message": "Hello World!",
  "Id": "some-uuid"
}
```

#### Client Setup
Ensure your SignalR client is set up to listen for the `ReceiveNotification` event. Example (JavaScript client):
```javascript
const connection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:5000/ws-channel?id=user123")
    .build();

connection.on("ReceiveNotification", (message) => {
    console.log("Notification received:", message);
});

connection.start()
    .then(() => console.log("Connected to SignalR hub"))
    .catch(err => console.error("Error connecting to hub:", err));
```



