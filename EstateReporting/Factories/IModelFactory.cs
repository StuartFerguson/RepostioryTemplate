namespace EstateReporting.Factories
{
    using BusinessLogic;
    using DataTransferObjects;
    using Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IModelFactory
    {
        #region Methods

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionsByDayResponse ConvertFrom(TransactionsByDayModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionsByWeekResponse ConvertFrom(TransactionsByWeekModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionsByMonthResponse ConvertFrom(TransactionsByMonthModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionWeekResponse ConvertFrom(TransactionWeekModel model);

        TransactionMonthResponse ConvertFrom(TransactionMonthModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionDayResponse ConvertFrom(TransactionDayModel model);

        #endregion
    }
}