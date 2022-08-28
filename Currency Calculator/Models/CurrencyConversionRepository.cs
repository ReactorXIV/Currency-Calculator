using Microsoft.EntityFrameworkCore;

namespace Currency_Calculator.Models
{
    public class CurrencyConversionRepository : ICurrencyConversionRepository
    {
        private readonly AppDbContext appDbContext;

        public CurrencyConversionRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<CurrencyConversion> AddCurrencyConversion(CurrencyConversion currencyConversion)
        {
            var result = await appDbContext.CurrencyConversion.AddAsync(currencyConversion);
            await appDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<decimal?> ConvertCurrency(decimal value, string baseCurrencyCode, string targetCurrencyCode)
        {
            CurrencyConversion? currencyConversion = await GetCurrencyConversion(baseCurrencyCode, targetCurrencyCode);
            if (currencyConversion == null)
            {
                return null;
            }

            return value * currencyConversion.ConversionRate;
        }

        public async Task DeleteCurrencyConversion(string baseCurrencyCode, string targetCurrencyCode)
        {
            var result = await appDbContext.CurrencyConversion.
                   FirstOrDefaultAsync(c => c.BaseCurrencyCode == baseCurrencyCode && c.TargetCurrencyCode == targetCurrencyCode);
            if (result != null)
            {
                appDbContext.CurrencyConversion.Remove(result);
                appDbContext.SaveChanges();
            }
        }

        public async Task<IEnumerable<CurrencyConversion>> GetAllCurrencyConversions()
        {
            return await appDbContext.CurrencyConversion.ToListAsync();
        }

        public async Task<CurrencyConversion?> GetCurrencyConversion(string baseCurrencyCode, string targetCurrencyCode)
        {
            return await appDbContext.CurrencyConversion.
                FirstOrDefaultAsync(c => c.BaseCurrencyCode == baseCurrencyCode && c.TargetCurrencyCode == targetCurrencyCode);
        }

        public async Task<CurrencyConversion?> UpdateCurrencyConversionRate(CurrencyConversion currencyConversion)
        {
            var result = await appDbContext.CurrencyConversion.
                   FirstOrDefaultAsync(c => c.BaseCurrencyCode == currencyConversion.BaseCurrencyCode && c.TargetCurrencyCode == currencyConversion.TargetCurrencyCode);

            if(result != null)
            {
                result.ConversionRate = currencyConversion.ConversionRate;
                await appDbContext.SaveChangesAsync();

                return result;
            }

            return null;
        }
    }
}
