﻿namespace Domain.Shared.Contracts;

public interface IEntity<out TKey>
{
    TKey Id { get; }
}