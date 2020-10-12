namespace EstateReporting.DataTransferObjects
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// 
    /// </summary>
    public class TransactionDayResponse
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the number of transactions.
        /// </summary>
        /// <value>
        /// The number of transactions.
        /// </value>
        [JsonProperty("number_of_transactions")]
        public Int32 NumberOfTransactions { get; set; }

        /// <summary>
        /// Gets or sets the value of transactions.
        /// </summary>
        /// <value>
        /// The value of transactions.
        /// </value>
        [JsonProperty("value_of_transactions")]
        public Decimal ValueOfTransactions { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        [JsonProperty("currency_code")]
        public String CurrencyCode { get; set; }
    }
}