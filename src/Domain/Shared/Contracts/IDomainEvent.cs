namespace Domain.Shared.Contracts;

public interface IDomainEvent<TKey> where TKey : notnull
{
    public TKey Id { get; set; }
    public string GetEventName();
}