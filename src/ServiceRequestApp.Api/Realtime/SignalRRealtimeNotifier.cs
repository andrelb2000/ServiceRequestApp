using Microsoft.AspNetCore.SignalR;
using ServiceRequestApp.Api.Hubs;
using ServiceRequestApp.Application.Interfaces;

namespace ServiceRequestApp.Api.Realtime;

/// Implements IRealtimeNotifier using SignalR.
public class SignalRRealtimeNotifier : IRealtimeNotifier {
	private readonly IHubContext<ServiceRequestHub> _hub;

	public SignalRRealtimeNotifier(IHubContext<ServiceRequestHub> hub) {
		_hub = hub;
	}

 public async Task NotifyRequestCreatedAsync() {
	 // Broadcast "RequestCreated" to ALL connected SignalR clients.
	 // Clients listen for this event name in Index.razor.
	 await _hub.Clients.All.SendAsync("RequestCreated");
 }
}