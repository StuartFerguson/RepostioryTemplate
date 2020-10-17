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
    /// <seealso cref="EstateReporting.BusinessLogic.IReportingManager" />
    public class ReportingManager : IReportingManager
    {
        #region Fields

        /// <summary>
        /// The repository
        /// </summary>
        private readonly IEstateReportingRepository Repository;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingManager" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        public ReportingManager(IEstateReportingRepository repository)
        {
            this.Repository = repository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the transactions for estate.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByDayModel> GetTransactionsForEstateByDate(Guid estateId,
                                                                           String startDate,
                                                                           String endDate,
                                                                           CancellationToken cancellationToken)
        {
            TransactionsByDayModel model = null;

            model = await this.Repository.GetTransactionsForEstateByDate(estateId, startDate, endDate, cancellationToken);

            return model;
        }

        /// <summary>
        /// Gets the transactions for merchant.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByDayModel> GetTransactionsForMerchantByDate(Guid estateId,
                                                                                   Guid merchantId,
                                                                                   String startDate,
                                                                                   String endDate,
                                                                                   CancellationToken cancellationToken)
        {
            TransactionsByDayModel model = null;

            model = await this.Repository.GetTransactionsForMerchantByDate(estateId, merchantId, startDate, endDate, cancellationToken);

            return model;
        }

        /// <summary>
        /// Gets the transactions for estate by week.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByWeekModel> GetTransactionsForEstateByWeek(Guid estateId,
                                                                                  String startDate,
                                                                                  String endDate,
                                                                                  CancellationToken cancellationToken)
        {
            TransactionsByWeekModel model = null;

            model = await this.Repository.GetTransactionsForEstateByWeek(estateId, startDate, endDate, cancellationToken);

            return model;
        }

        /// <summary>
        /// Gets the transactions for merchant by week.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByWeekModel> GetTransactionsForMerchantByWeek(Guid estateId,
                                                                                    Guid merchantId,
                                                                                    String startDate,
                                                                                    String endDate,
                                                                                    CancellationToken cancellationToken)
        {
            TransactionsByWeekModel model = null;

            model = await this.Repository.GetTransactionsForMerchantByWeek(estateId, merchantId, startDate, endDate, cancellationToken);

            return model;
        }

        /// <summary>
        /// Gets the transactions for estate by month.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByMonthModel> GetTransactionsForEstateByMonth(Guid estateId,
                                                                                    String startDate,
                                                                                    String endDate,
                                                                                    CancellationToken cancellationToken)
        {
            TransactionsByMonthModel model = null;

            model = await this.Repository.GetTransactionsForEstateByMonth(estateId, startDate, endDate, cancellationToken);

            return model;
        }

        /// <summary>
        /// Gets the transactions for merchant by month.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByMonthModel> GetTransactionsForMerchantByMonth(Guid estateId,
                                                                                      Guid merchantId,
                                                                                      String startDate,
                                                                                      String endDate,
                                                                                      CancellationToken cancellationToken)
        {
            TransactionsByMonthModel model = null;

            model = await this.Repository.GetTransactionsForMerchantByMonth(estateId, merchantId, startDate, endDate, cancellationToken);

            return model;
        }

        #endregion
    }
}