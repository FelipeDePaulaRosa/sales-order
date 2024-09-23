namespace Domain.Shared.Contracts;

public interface IDomainEvent
{
    public string Id { get; set; }
    public string GetEventName();
}