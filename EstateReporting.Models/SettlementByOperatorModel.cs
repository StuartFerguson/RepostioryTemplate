namespace EstateReporting.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SettlementByOperatorModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction merchant models.
        /// </summary>
        /// <value>
        /// The transaction merchant models.
        /// </value>
        public List<SettlementOperatorModel> SettlementOperatorModels { get; set; }

        #endregion
    }
}