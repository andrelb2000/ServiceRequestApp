using Microsoft.AspNetCore.SignalR;
namespace ServiceRequestApp.Api.Hubs;

/// <summary>
/// SignalR hub for real-time ServiceRequest notifications.
/// This hub is broadcast-only — clients connect and listen.
/// Broadcasting is triggered externally via IHubContext.
/// </summary>
public class ServiceRequestHub : Hub {
    // No client-callable methods required for broadcast-only scenario.
    // IHubContext<ServiceRequestHub> is used in SignalRRealtimeNotifier
    // to push events to all connected clients.
}

