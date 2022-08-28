using Microsoft.EntityFrameworkCore;

namespace Currency_Calculator.Models
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly AppDbContext appDbContext;

        public CurrencyRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<Currency> AddCurrency(Currency currency)
        {
            var result = await appDbContext.Currency.AddAsync(currency);
            await appDbContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task DeleteCurrency(string currencyCode)
        {
            var result = await appDbContext.Currency.
                FirstOrDefaultAsync(c => c.CurrencyCode == currencyCode);
            if(result != null)
            {
                appDbContext.Currency.Remove(result);
                appDbContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<Currency>> GetAllCurrencies()
        {
            return await appDbContext.Currency.ToListAsync();
        }

        public async Task<Currency?> GetCurrency(string currencyCode)
        {
            return await appDbContext.Currency.
                FirstOrDefaultAsync(c => c.CurrencyCode == currencyCode);
        }
    }
}
