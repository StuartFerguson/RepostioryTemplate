namespace EstateReporting.Client
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DataTransferObjects;

    /// <summary>
    /// 
    /// </summary>
    public interface IEstateReportingClient
    {
        #region Methods

        /// <summary>
        /// Gets the transactions by date.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByDayResponse> GetTransactionsByDate(String accessToken,
                                                              Guid estateId,
                                                              String startDate,
                                                              String endDate,
                                                              CancellationToken cancellationToken);

        #endregion
    }
}