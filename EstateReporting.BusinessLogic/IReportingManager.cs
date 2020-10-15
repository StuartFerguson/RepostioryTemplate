namespace EstateReporting.BusinessLogic
{
    using System;
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

        #endregion
    }
}