namespace CrossCutting.FluentValidationNotifications;

public interface IFluentValidationNotificationContext
{
    IEnumerable<FluentValidationNotification> Notifications { get; }
    bool HasNotifications { get; }
    void AddNotifications(IEnumerable<string> messages);
}