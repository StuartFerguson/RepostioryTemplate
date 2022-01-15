namespace EstateReporting.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IReportingManager
    {
        #region Methods



        /// <summary>
        /// Gets the settlement.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="settlementId">The settlement identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SettlementModel> GetSettlement(Guid estateId,
                                            Guid? merchantId,
                                            Guid settlementId,
                                            CancellationToken cancellationToken);

        /// <summary>
        /// Gets the settlement for estate by date.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SettlementByDayModel> GetSettlementForEstateByDate(Guid estateId,
                                                                String startDate,
                                                                String endDate,
                                                                CancellationToken cancellationToken);

        /// <summary>
        /// Gets the settlement for estate by merchant.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SettlementByMerchantModel> GetSettlementForEstateByMerchant(Guid estateId,
                                                                         String startDate,
                                                                         String endDate,
                                                                         Int32 recordCount,
                                                                         SortField sortField,
                                                                         SortDirection sortDirection,
                                                                         CancellationToken cancellationToken);

        /// <summary>
        /// Gets the settlement for estate by month.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SettlementByMonthModel> GetSettlementForEstateByMonth(Guid estateId,
                                                                   String startDate,
                                                                   String endDate,
                                                                   CancellationToken cancellationToken);

        /// <summary>
        /// Gets the settlement for estate by operator.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SettlementByOperatorModel> GetSettlementForEstateByOperator(Guid estateId,
                                                                         String startDate,
                                                                         String endDate,
                                                                         Int32 recordCount,
                                                                         SortField sortField,
                                                                         SortDirection sortDirection,
                                                                         CancellationToken cancellationToken);

        /// <summary>
        /// Gets the settlement for estate by week.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SettlementByWeekModel> GetSettlementForEstateByWeek(Guid estateId,
                                                                 String startDate,
                                                                 String endDate,
                                                                 CancellationToken cancellationToken);

        /// <summary>
        /// Gets the settlement for merchant by date.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SettlementByDayModel> GetSettlementForMerchantByDate(Guid estateId,
                                                                  Guid merchantId,
                                                                  String startDate,
                                                                  String endDate,
                                                                  CancellationToken cancellationToken);

        /// <summary>
        /// Gets the settlement for merchant by month.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SettlementByMonthModel> GetSettlementForMerchantByMonth(Guid estateId,
                                                                     Guid merchantId,
                                                                     String startDate,
                                                                     String endDate,
                                                                     CancellationToken cancellationToken);

        /// <summary>
        /// Gets the settlement for merchant by week.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<SettlementByWeekModel> GetSettlementForMerchantByWeek(Guid estateId,
                                                                   Guid merchantId,
                                                                   String startDate,
                                                                   String endDate,
                                                                   CancellationToken cancellationToken);

        /// <summary>
        /// Gets the settlements.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<List<SettlementModel>> GetSettlements(Guid estateId,
                                                   Guid? merchantId,
                                                   String startDate,
                                                   String endDate,
                                                   CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for estate.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByDayModel> GetTransactionsForEstateByDate(Guid estateId,
                                                                    String startDate,
                                                                    String endDate,
                                                                    CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for estate by merchant.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByMerchantModel> GetTransactionsForEstateByMerchant(Guid estateId,
                                                                             String startDate,
                                                                             String endDate,
                                                                             Int32 recordCount,
                                                                             SortField sortField,
                                                                             SortDirection sortDirection,
                                                                             CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for estate by month.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByMonthModel> GetTransactionsForEstateByMonth(Guid estateId,
                                                                       String startDate,
                                                                       String endDate,
                                                                       CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for estate by operator.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByOperatorModel> GetTransactionsForEstateByOperator(Guid estateId,
                                                                             String startDate,
                                                                             String endDate,
                                                                             Int32 recordCount,
                                                                             SortField sortField,
                                                                             SortDirection sortDirection,
                                                                             CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for estate by week.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByWeekModel> GetTransactionsForEstateByWeek(Guid estateId,
                                                                     String startDate,
                                                                     String endDate,
                                                                     CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for merchant.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByDayModel> GetTransactionsForMerchantByDate(Guid estateId,
                                                                      Guid merchantId,
                                                                      String startDate,
                                                                      String endDate,
                                                                      CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for merchant by month.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByMonthModel> GetTransactionsForMerchantByMonth(Guid estateId,
                                                                         Guid merchantId,
                                                                         String startDate,
                                                                         String endDate,
                                                                         CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for merchant by week.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByWeekModel> GetTransactionsForMerchantByWeek(Guid estateId,
                                                                       Guid merchantId,
                                                                       String startDate,
                                                                       String endDate,
                                                                       CancellationToken cancellationToken);

        #endregion
    }
}