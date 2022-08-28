using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Currency_Calculator
{
    public record Currency
    {
        private string currencyCode = null!;

        /// <summary>
        /// The currency code in ISO 4217 three letter format
        /// </summary>
        /// <example>EUR</example>
        [StringLength(3)]
        [MaxLength(3)]
        [MinLength(3)]
        [Required]
        public string CurrencyCode
        {
            get { return currencyCode.ToUpper(); }
            set { currencyCode = value; }
        }

        [InverseProperty(nameof(CurrencyConversion.BaseCurrency))]
        [JsonIgnore]
        public ICollection<CurrencyConversion>? CurrencyConversionBase { get; set; }

        [InverseProperty(nameof(CurrencyConversion.TargetCurrency))]
        [JsonIgnore]
        public ICollection<CurrencyConversion>? CurrencyConversionTarget { get; set; }

    }
}
