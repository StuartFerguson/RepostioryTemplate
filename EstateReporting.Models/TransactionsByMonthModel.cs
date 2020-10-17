namespace EstateReporting.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class TransactionsByMonthModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction month models.
        /// </summary>
        /// <value>
        /// The transaction day models.
        /// </value>
        public List<TransactionMonthModel> TransactionMonthModels { get; set; }

        #endregion
    }
}