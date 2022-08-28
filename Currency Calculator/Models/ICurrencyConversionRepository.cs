namespace Currency_Calculator.Models
{
    public interface ICurrencyConversionRepository
    {
        Task<decimal?> ConvertCurrency(decimal value, string baseCurrencyCode, string targetCurrencyCode);
        Task<IEnumerable<CurrencyConversion>> GetAllCurrencyConversions();
        Task<CurrencyConversion?> GetCurrencyConversion(string baseCurrencyCode, string targetCurrencyCode);
        Task<CurrencyConversion> AddCurrencyConversion(CurrencyConversion currencyConversion);
        Task<CurrencyConversion?> UpdateCurrencyConversionRate(CurrencyConversion currencyConversion);
        Task DeleteCurrencyConversion(string baseCurrencyCode, string targetCurrencyCode);
    }
}
