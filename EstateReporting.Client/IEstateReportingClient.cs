namespace EstateReporting.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DataTransferObjects;

    /// <summary>
    /// 
    /// </summary>
    public interface IEstateReportingClient
    {
        #region Methods

        Task<SettlementResponse> GetSettlement(String accessToken,
                                               Guid estateId,
                                               Guid? merchantId,
                                               Guid settlementId,
                                               CancellationToken cancellationToken);

        Task<SettlementByDayResponse> GetSettlementForEstateByDate(String accessToken,
                                                                   Guid estateId,
                                                                   String startDate,
                                                                   String endDate,
                                                                   CancellationToken cancellationToken);

        Task<SettlementByMerchantResponse> GetSettlementForEstateByMerchant(String accessToken,
                                                                            Guid estateId,
                                                                            String startDate,
                                                                            String endDate,
                                                                            Int32 recordCount,
                                                                            SortDirection sortDirection,
                                                                            SortField sortField,
                                                                            CancellationToken cancellationToken);

        Task<SettlementByMonthResponse> GetSettlementForEstateByMonth(String accessToken,
                                                                      Guid estateId,
                                                                      String startDate,
                                                                      String endDate,
                                                                      CancellationToken cancellationToken);

        Task<SettlementByOperatorResponse> GetSettlementForEstateByOperator(String accessToken,
                                                                            Guid estateId,
                                                                            String startDate,
                                                                            String endDate,
                                                                            Int32 recordCount,
                                                                            SortDirection sortDirection,
                                                                            SortField sortField,
                                                                            CancellationToken cancellationToken);

        Task<SettlementByWeekResponse> GetSettlementForEstateByWeek(String accessToken,
                                                                    Guid estateId,
                                                                    String startDate,
                                                                    String endDate,
                                                                    CancellationToken cancellationToken);

        Task<SettlementByDayResponse> GetSettlementForMerchantByDate(String accessToken,
                                                                     Guid estateId,
                                                                     Guid merchantId,
                                                                     String startDate,
                                                                     String endDate,
                                                                     CancellationToken cancellationToken);

        Task<SettlementByMonthResponse> GetSettlementForMerchantByMonth(String accessToken,
                                                                        Guid estateId,
                                                                        Guid merchantId,
                                                                        String startDate,
                                                                        String endDate,
                                                                        CancellationToken cancellationToken);

        Task<SettlementByWeekResponse> GetSettlementForMerchantByWeek(String accessToken,
                                                                      Guid estateId,
                                                                      Guid merchantId,
                                                                      String startDate,
                                                                      String endDate,
                                                                      CancellationToken cancellationToken);

        Task<List<SettlementResponse>> GetSettlements(String accessToken,
                                                      Guid estateId,
                                                      Guid? merchantId,
                                                      String startDate,
                                                      String endDate,
                                                      CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions by date.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByDayResponse> GetTransactionsForEstateByDate(String accessToken,
                                                                       Guid estateId,
                                                                       String startDate,
                                                                       String endDate,
                                                                       CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for estate by merchant.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByMerchantResponse> GetTransactionsForEstateByMerchant(String accessToken,
                                                                                Guid estateId,
                                                                                String startDate,
                                                                                String endDate,
                                                                                Int32 recordCount,
                                                                                SortDirection sortDirection,
                                                                                SortField sortField,
                                                                                CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for estate by month.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByMonthResponse> GetTransactionsForEstateByMonth(String accessToken,
                                                                          Guid estateId,
                                                                          String startDate,
                                                                          String endDate,
                                                                          CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for estate by operator.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByOperatorResponse> GetTransactionsForEstateByOperator(String accessToken,
                                                                                Guid estateId,
                                                                                String startDate,
                                                                                String endDate,
                                                                                Int32 recordCount,
                                                                                SortDirection sortDirection,
                                                                                SortField sortField,
                                                                                CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for estate by week.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByWeekResponse> GetTransactionsForEstateByWeek(String accessToken,
                                                                        Guid estateId,
                                                                        String startDate,
                                                                        String endDate,
                                                                        CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for merchant by date.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByDayResponse> GetTransactionsForMerchantByDate(String accessToken,
                                                                         Guid estateId,
                                                                         Guid merchantId,
                                                                         String startDate,
                                                                         String endDate,
                                                                         CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for merchant by month.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByMonthResponse> GetTransactionsForMerchantByMonth(String accessToken,
                                                                            Guid estateId,
                                                                            Guid merchantId,
                                                                            String startDate,
                                                                            String endDate,
                                                                            CancellationToken cancellationToken);

        /// <summary>
        /// Gets the transactions for merchant by week.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByWeekResponse> GetTransactionsForMerchantByWeek(String accessToken,
                                                                          Guid estateId,
                                                                          Guid merchantId,
                                                                          String startDate,
                                                                          String endDate,
                                                                          CancellationToken cancellationToken);

        #endregion
    }
}