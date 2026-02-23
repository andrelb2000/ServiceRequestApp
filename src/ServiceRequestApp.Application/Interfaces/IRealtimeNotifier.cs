namespace ServiceRequestApp.Application.Interfaces;

/// <summary>
/// Abstraction for pushing real-time notifications to connected clients.
/// The Service layer depends on this interface — not on SignalR directly.
/// This keeps the Service project free of infrastructure dependencies.
/// </summary>
public interface IRealtimeNotifier{
    /// <summary>
    /// Notifies all connected clients that a new ServiceRequest was created.
    /// </summary>
    Task NotifyRequestCreatedAsync();
}