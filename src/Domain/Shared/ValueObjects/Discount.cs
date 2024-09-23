namespace Domain.Shared.ValueObjects;

public class Discount
{
    public decimal ValueInPercentage { get; private set; }
    private const int PercentageDivisor = 100;

    private Discount() { }

    public Discount(decimal valueInPercentage)
    {
        if (valueInPercentage is < 0 or > 100)
            throw new ArgumentException("Discount value must be between 0 and 100");

        ValueInPercentage = valueInPercentage;
    }

    public decimal ApplyDiscount(decimal value) => value - (value * FromPercentage);
    private decimal FromPercentage => ValueInPercentage / PercentageDivisor;
}