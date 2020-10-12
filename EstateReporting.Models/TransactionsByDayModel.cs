namespace EstateReporting.Models
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class TransactionsByDayModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets the transaction day models.
        /// </summary>
        /// <value>
        /// The transaction day models.
        /// </value>
        public List<TransactionDayModel> TransactionDayModels { get; set; }

        #endregion
    }
}