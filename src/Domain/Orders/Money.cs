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
        ValueInCents = ToValueAsCents(value);
    }
    
    public static Money FromDecimal(decimal value) => new(value);
    
    public decimal GetValueFromCents() => ToValueAsDecimal(ValueInCents);
    
    private static decimal ToValueAsDecimal(long valueInCents) => decimal.Divide(valueInCents, CentsOperator);
    private static long ToValueAsCents(decimal value) => decimal.ToInt64(decimal.Multiply(value, CentsOperator));
}