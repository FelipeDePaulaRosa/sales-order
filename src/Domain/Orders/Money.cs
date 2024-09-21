using Domain.Shared.Exceptions;

namespace Domain.Orders;

public class Money
{
    public decimal Value { get; private set; }
    public long ValueInCents { get; private set; }
    private const int CentsOperator = 100;

    private Money() { }
    private Money(decimal value)
    {
        if (value < 0)
            throw new SalesOrderApiException("Money value cannot be negative");
     
        Value = value;
        ValueInCents = GetValueAsCents(value);
    }
    
    private Money(long valueInCents)
    {
        if (valueInCents < 0)
            throw new SalesOrderApiException("Money value cannot be negative");
     
        ValueInCents = valueInCents;
        Value = GetValueAsDecimal(valueInCents);
    }
    
    public static Money FromDecimal(decimal value) => new(value);
    
    private decimal GetValueAsDecimal(long valueInCents) => decimal.Divide(valueInCents, CentsOperator);
    private long GetValueAsCents(decimal value) => decimal.ToInt64(decimal.Multiply(value, CentsOperator));
    
}