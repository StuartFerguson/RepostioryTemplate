namespace EstateReporting.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SettlementByMonthModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction month models.
        /// </summary>
        /// <value>
        /// The transaction day models.
        /// </value>
        public List<SettlementMonthModel> SettlementMonthModels { get; set; }

        #endregion
    }
}