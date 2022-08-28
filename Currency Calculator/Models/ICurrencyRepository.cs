namespace Currency_Calculator.Models
{
    public interface ICurrencyRepository
    {
        Task<IEnumerable<Currency>> GetAllCurrencies();
        Task<Currency?> GetCurrency(string currencyCode);
        Task<Currency> AddCurrency(Currency currency);
        Task DeleteCurrency(string currencyCode);
    }
}
