namespace Domain.Shared.Exceptions;

public abstract class SalesOrderApiException : Exception 
{
    protected SalesOrderApiException(string message) : base(message)
    {
    }
}