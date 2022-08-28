using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Currency_Calculator
{
    public record CurrencyConversion
    {
        private string baseCurrencyCode = null!;
        private string targetCurrencyCode = null!;

        /// <summary>
        /// The currency code of the base currency in ISO 4217 three letter format
        /// </summary>
        /// <example>EUR</example>
        [ForeignKey(nameof(BaseCurrency))]
        [StringLength(3)]
        [MaxLength(3)]
        [MinLength(3)]
        [Required]
        public string BaseCurrencyCode
        {
            get { return baseCurrencyCode.ToUpper(); }
            set { baseCurrencyCode = value; }
        }

        [JsonIgnore]
        public Currency? BaseCurrency { get; set; }

        /// <summary>
        /// The currency code of the target currency in ISO 4217 three letter format
        /// </summary>
        /// <example>USD</example>
        [ForeignKey(nameof(TargetCurrency))]
        [StringLength(3)]
        [MaxLength(3)]
        [MinLength(3)]
        [Required]
        public string TargetCurrencyCode
        {
            get { return targetCurrencyCode.ToUpper(); }
            set { targetCurrencyCode = value; }
        }

        [JsonIgnore]
        public Currency? TargetCurrency { get; set; }

        /// <summary>
        /// The currency conversion rate from the base currency to the target currency
        /// </summary>
        /// <example>1.2492</example>
        [Required]
        [Column(TypeName = "decimal(18,10)")]
        public decimal ConversionRate { get; set; }

    }
}
