namespace EstateReporting.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SettlementByMerchantModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction merchant models.
        /// </summary>
        /// <value>
        /// The transaction merchant models.
        /// </value>
        public List<SettlementMerchantModel> SettlementMerchantModels { get; set; }

        #endregion
    }
}