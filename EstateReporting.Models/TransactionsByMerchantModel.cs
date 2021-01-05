namespace EstateReporting.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class TransactionsByMerchantModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction merchant models.
        /// </summary>
        /// <value>
        /// The transaction merchant models.
        /// </value>
        public List<TransactionMerchantModel> TransactionMerchantModels { get; set; }

        #endregion
    }
}