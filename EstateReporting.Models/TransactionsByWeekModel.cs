namespace EstateReporting.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class TransactionsByWeekModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction week models.
        /// </summary>
        /// <value>
        /// The transaction day models.
        /// </value>
        public List<TransactionWeekModel> TransactionWeekModels { get; set; }

        #endregion
    }
}