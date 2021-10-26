namespace EstateReporting.Repository
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IEstateReportingRepositoryForReports
    {
        /// <summary>
        /// Gets the transactions for estate by date.
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

        Task<SettlementByDayModel> GetSettlementForEstateByDate(Guid estateId,
                                                                    String startDate,
                                                                    String endDate,
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

        Task<SettlementByMonthModel> GetSettlementForEstateByMonth(Guid estateId,
                                                                       String startDate,
                                                                       String endDate,
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

        Task<SettlementByWeekModel> GetSettlementForEstateByWeek(Guid estateId,
                                                                     String startDate,
                                                                     String endDate,
                                                                     CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for merchant by date.
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

        Task<SettlementByDayModel> GetSettlementForMerchantByDate(Guid estateId,
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

        Task<SettlementByMonthModel> GetSettlementForMerchantByMonth(Guid estateId,
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

        Task<SettlementByWeekModel> GetSettlementForMerchantByWeek(Guid estateId,
                                                                       Guid merchantId,
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

        Task<SettlementByMerchantModel> GetSettlementForEstateByMerchant(Guid estateId,
                                                                             String startDate,
                                                                             String endDate,
                                                                             Int32 recordCount,
                                                                             SortField sortField,
                                                                             SortDirection sortDirection,
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

        Task<SettlementByOperatorModel> GetSettlementForEstateByOperator(Guid estateId,
                                                                             String startDate,
                                                                             String endDate,
                                                                             Int32 recordCount,
                                                                             SortField sortField,
                                                                             SortDirection sortDirection,
                                                                             CancellationToken cancellationToken);
    }
}