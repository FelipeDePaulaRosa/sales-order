namespace Domain.Shared.Exceptions;

public class SalesOrderNotFoundException : Exception
{
    public SalesOrderNotFoundException(string message) : base(message)
    {
    }
}