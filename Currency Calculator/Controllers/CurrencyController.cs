using Currency_Calculator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Currency_Calculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository currencyRepository;

        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            this.currencyRepository = currencyRepository;
        }

        /// <summary>
        /// Returns all currencies
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<Currency>>> GetAllCurrencies()
        {
            try
            {
                return Ok(await currencyRepository.GetAllCurrencies());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Returns currency with specified currencyCode
        /// </summary>
        [HttpGet("{currencyCode}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Currency>> GetCurrency(string currencyCode)
        {
            try
            {
                var result = await currencyRepository.GetCurrency(currencyCode);
                if(result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Adds currency to database
        /// </summary>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Currency>> AddCurrency(Currency currency)
        {
            try
            {
                var createdCurrency = await currencyRepository.AddCurrency(currency);

                return CreatedAtAction(nameof(GetCurrency), new { currencyCode = currency.CurrencyCode }, createdCurrency);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes currency from database
        /// </summary>
        [Authorize]
        [HttpDelete("{currencyCode}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Currency>> DeleteCurrency(string currencyCode)
        {
            try
            {
                var currency = await GetCurrency(currencyCode);
                if (currency == null)
                {
                    return NotFound();
                }

                await currencyRepository.DeleteCurrency(currencyCode);
                return Ok(currency);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
