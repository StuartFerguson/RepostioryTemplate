namespace EstateReporting.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class SettlementByDayModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction day models.
        /// </summary>
        /// <value>
        /// The transaction day models.
        /// </value>
        public List<SettlementDayModel> SettlementDayModels { get; set; }

        #endregion
    }
}