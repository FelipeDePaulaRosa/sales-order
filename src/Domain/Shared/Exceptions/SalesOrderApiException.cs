namespace Domain.Shared.Exceptions;

public class SalesOrderApiException : Exception 
{
    public SalesOrderApiException(string message) : base(message)
    {
    }
}