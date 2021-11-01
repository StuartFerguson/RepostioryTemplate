namespace EstateReporting.Factories
{
    using System.Collections.Generic;
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

        SettlementFeeResponse ConvertFrom(SettlementFeeModel model);

        List<SettlementFeeResponse> ConvertFrom(List<SettlementFeeModel> model);

        SettlementResponse ConvertFrom(SettlementModel model);

        List<SettlementResponse> ConvertFrom(List<SettlementModel> model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionsByDayResponse ConvertFrom(TransactionsByDayModel model);

        SettlementByDayResponse ConvertFrom(SettlementByDayModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionsByWeekResponse ConvertFrom(TransactionsByWeekModel model);

        SettlementByWeekResponse ConvertFrom(SettlementByWeekModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionsByMonthResponse ConvertFrom(TransactionsByMonthModel model);

        SettlementByMonthResponse ConvertFrom(SettlementByMonthModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionWeekResponse ConvertFrom(TransactionWeekModel model);

        SettlementWeekResponse ConvertFrom(SettlementWeekModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionMonthResponse ConvertFrom(TransactionMonthModel model);

        SettlementMonthResponse ConvertFrom(SettlementMonthModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionDayResponse ConvertFrom(TransactionDayModel model);

        SettlementDayResponse ConvertFrom(SettlementDayModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionMerchantResponse ConvertFrom(TransactionMerchantModel model);

        SettlementMerchantResponse ConvertFrom(SettlementMerchantModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionsByMerchantResponse ConvertFrom(TransactionsByMerchantModel model);

        SettlementByMerchantResponse ConvertFrom(SettlementByMerchantModel model);

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

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionOperatorResponse ConvertFrom(TransactionOperatorModel model);

        SettlementOperatorResponse ConvertFrom(SettlementOperatorModel model);

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        TransactionsByOperatorResponse ConvertFrom(TransactionsByOperatorModel model);

        SettlementByOperatorResponse ConvertFrom(SettlementByOperatorModel model);

        #endregion
    }
}