namespace EstateReporting.BusinessLogic
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;
    using Repository;

    /// <summary>
    /// 
    /// </summary>
    public interface IReportingManager
    {
        #region Methods

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

        Task<TransactionsByMerchantModel> GetTransactionsForEstateByMerchant(Guid estateId,
                                                                             String startDate,
                                                                             String endDate,
                                                                             Int32 recordCount,
                                                                             SortField sortField,
                                                                             SortDirection sortDirection,
                                                                             CancellationToken cancellationToken);

        #endregion
    }
}