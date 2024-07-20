using System.Globalization;

namespace FinSecure.Platform.Common.ValueObjects;

public static class CurrencyFormatter
{
    public static string FormatCurrency(Currency currency, Amount amount, int decPlaces)
    {
        NumberFormatInfo localFormat = (NumberFormatInfo)NumberFormatInfo.CurrentInfo.Clone();
        localFormat.CurrencySymbol = (currency ?? Currency.Default).Symbol;
        localFormat.CurrencyDecimalDigits = decPlaces;
        return ((decimal)amount).ToString("c", localFormat);
    }
}
