using Currency_Calculator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Currency_Calculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyConversionController : ControllerBase
    {
        private readonly ICurrencyConversionRepository currencyConversionRepository;

        public CurrencyConversionController(ICurrencyConversionRepository currencyConversionRepository)
        {
            this.currencyConversionRepository = currencyConversionRepository;
        }

        /// <summary>
        /// Returns the converted value from the base currency to the target currency
        /// </summary>
        [HttpGet("ConvertCurrency")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<decimal>> ConvertCurrency(string baseCurrencyCode, string targetCurrencyCode, decimal value)
        {
            try
            {
                var result = await currencyConversionRepository.ConvertCurrency(value, baseCurrencyCode, targetCurrencyCode);
                if (result == null)
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
        /// Returns all currency conversions
        /// </summary>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<CurrencyConversion>>> GetAllCurrencyConversions()
        {
            try
            {
                return Ok(await currencyConversionRepository.GetAllCurrencyConversions());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Returns the conversion currency entry with the specified base currency and target currency
        /// </summary>
        [HttpGet("{baseCurrencyCode}/{targetCurrencyCode}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CurrencyConversion>> GetCurrencyConversion(string baseCurrencyCode, string targetCurrencyCode)
        {
            try
            {
                var result = await currencyConversionRepository.GetCurrencyConversion(baseCurrencyCode, targetCurrencyCode);
                if (result == null)
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
        /// Adds a conversion currency entry to the database
        /// </summary>
        [Authorize]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CurrencyConversion>> AddCurrencyConversion(CurrencyConversion currencyConversion)
        {
            try
            {
                var createdCurrencyConversion = await currencyConversionRepository.AddCurrencyConversion(currencyConversion);

                return CreatedAtAction(nameof(GetCurrencyConversion), 
                    new { baseCurrencyCode = currencyConversion.BaseCurrencyCode, targetCurrencyCode = currencyConversion.TargetCurrencyCode}, 
                    createdCurrencyConversion);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes the conversion currency entry with the specified base currency and target currency from the database
        /// </summary>
        [Authorize]
        [HttpDelete("{baseCurrencyCode}/{targetCurrencyCode}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CurrencyConversion>> DeleteCurrencyConversion(string baseCurrencyCode, string targetCurrencyCode)
        {
            try
            {
                var currencyConversion = await GetCurrencyConversion(baseCurrencyCode, targetCurrencyCode);
                if (currencyConversion == null)
                {
                    return NotFound();
                }

                await currencyConversionRepository.DeleteCurrencyConversion(baseCurrencyCode, targetCurrencyCode);
                return Ok(currencyConversion);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates the conversion currency rate of the conversion currency entry with the specified base currency and target currency in the database
        /// </summary>
        [Authorize]
        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CurrencyConversion>> UpdateCurrencyConversionRate(CurrencyConversion currencyConversion)
        {
            try
            {
                if (currencyConversion == null)
                {
                    return BadRequest();
                }
                var result = await GetCurrencyConversion(currencyConversion.BaseCurrencyCode, currencyConversion.TargetCurrencyCode);
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(await currencyConversionRepository.UpdateCurrencyConversionRate(currencyConversion));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

    }
}