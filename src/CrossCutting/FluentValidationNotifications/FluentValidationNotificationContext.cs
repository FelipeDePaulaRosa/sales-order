namespace CrossCutting.FluentValidationNotifications;

public class FluentValidationNotificationContext : IFluentValidationNotificationContext
{
    public IEnumerable<FluentValidationNotification> Notifications => _notifications;
    public bool HasNotifications => _notifications.Count != 0;
    
    private readonly List<FluentValidationNotification> _notifications = new();

    public void AddNotifications(IEnumerable<string> messages)
    {
        var notifications = messages.Select(x => new FluentValidationNotification(x));
        _notifications.AddRange(notifications);
    }
}