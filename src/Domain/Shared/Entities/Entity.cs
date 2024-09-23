using System.ComponentModel.DataAnnotations;
using Domain.Shared.Contracts;

namespace Domain.Shared.Entities;

public abstract class Entity<TKey> : IEntity<TKey>
    where TKey : notnull
{
    [Key]
    public TKey Id { get; protected set; } = default!;
}