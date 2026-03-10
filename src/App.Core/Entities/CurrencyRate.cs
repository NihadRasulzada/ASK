using App.Core.Entities.Common;

namespace App.Core.Entities;

public class CurrencyRate : BaseEntity
{
    public string CurrencyCode { get; private set; }
    public decimal Rate { get; private set; }
    public DateOnly RateDate { get; private set; }

    private CurrencyRate() : base(Guid.NewGuid())
    {
        CurrencyCode = string.Empty;
    }

    public CurrencyRate(string currencyCode, decimal rate, DateOnly rateDate) : base(Guid.NewGuid())
    {
        CurrencyCode = currencyCode;
        Rate = rate;
        RateDate = rateDate;
    }

    public void UpdateRate(decimal rate) => Rate = rate;
}
