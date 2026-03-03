namespace App.Core.Entities;

public class CurrencyRate : BaseEntity
{
    public string CurrencyCode { get; private set; }
    public decimal Rate { get; private set; }
    public DateOnly RateDate { get; private set; }

    private CurrencyRate()
    {
        CurrencyCode = string.Empty;
    }

    public CurrencyRate(string currencyCode, decimal rate, DateOnly rateDate)
    {
        CurrencyCode = currencyCode;
        Rate = rate;
        RateDate = rateDate;
    }

    public void UpdateRate(decimal rate) => Rate = rate;
}
