namespace EstateReporting.Factories
{
    using DataTransferObjects;
    using Models;
    using SortDirection = BusinessLogic.SortDirection;
    using SortField = BusinessLogic.SortField;

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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionMonthResponse ConvertFrom(TransactionMonthModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionDayResponse ConvertFrom(TransactionDayModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionMerchantResponse ConvertFrom(TransactionMerchantModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionsByMerchantResponse ConvertFrom(TransactionsByMerchantModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        SortDirection ConvertFrom(DataTransferObjects.SortDirection model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        SortField ConvertFrom(DataTransferObjects.SortField model);

        #endregion
    }
}