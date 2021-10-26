namespace EstateReporting.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SettlementByWeekModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction week models.
        /// </summary>
        /// <value>
        /// The transaction day models.
        /// </value>
        public List<SettlementWeekModel> SettlementWeekModels { get; set; }

        #endregion
    }
}