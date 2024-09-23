namespace CrossCutting.FluentValidationNotifications;

public class FluentValidationNotification
{
    public string Message { get; }
    
    public FluentValidationNotification(string message)
    {
        Message = message;
    }
}