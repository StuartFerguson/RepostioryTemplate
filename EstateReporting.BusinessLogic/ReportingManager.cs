namespace EstateReporting.BusinessLogic
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;
    using Repository;
    using Shared.EventStore.Subscriptions;

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

        /// <summary>
        /// The repository for reports
        /// </summary>
        private readonly IEstateReportingRepositoryForReports RepositoryForReports;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingManager" /> class.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// <param name="repositoryForReports">The repository for reports.</param>
        public ReportingManager(IEstateReportingRepository repository,
                                IEstateReportingRepositoryForReports repositoryForReports)
        {
            this.Repository = repository;
            this.RepositoryForReports = repositoryForReports;
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

            model = await this.RepositoryForReports.GetTransactionsForEstateByDate(estateId, startDate, endDate, cancellationToken);

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

            model = await this.RepositoryForReports.GetTransactionsForMerchantByDate(estateId, merchantId, startDate, endDate, cancellationToken);

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

            model = await this.RepositoryForReports.GetTransactionsForEstateByWeek(estateId, startDate, endDate, cancellationToken);

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

            model = await this.RepositoryForReports.GetTransactionsForMerchantByWeek(estateId, merchantId, startDate, endDate, cancellationToken);

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

            model = await this.RepositoryForReports.GetTransactionsForEstateByMonth(estateId, startDate, endDate, cancellationToken);

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

            model = await this.RepositoryForReports.GetTransactionsForMerchantByMonth(estateId, merchantId, startDate, endDate, cancellationToken);

            return model;
        }

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
        public async Task<TransactionsByMerchantModel> GetTransactionsForEstateByMerchant(Guid estateId,
                                                                                          String startDate,
                                                                                          String endDate,
                                                                                          Int32 recordCount,
                                                                                          SortField sortField,
                                                                                          SortDirection sortDirection,
                                                                                          CancellationToken cancellationToken)
        {
            TransactionsByMerchantModel model = null;
            EstateReporting.Repository.SortDirection repoSortDirection = EstateReporting.Repository.SortDirection.NotSet;
            if (sortDirection == SortDirection.Ascending)
            {
                repoSortDirection = EstateReporting.Repository.SortDirection.Ascending;
            }
            else if (sortDirection == SortDirection.Descending)
            {
                repoSortDirection = EstateReporting.Repository.SortDirection.Descending;
            }

            EstateReporting.Repository.SortField repoSortField = EstateReporting.Repository.SortField.NotSet;
            if (sortField == SortField.Count)
            {
                repoSortField = EstateReporting.Repository.SortField.Count;
            }
            else if (sortField == SortField.Value)
            {
                repoSortField = EstateReporting.Repository.SortField.Value;
            }

            model = await this.RepositoryForReports.GetTransactionsForEstateByMerchant(estateId,  startDate, endDate, recordCount, repoSortField, repoSortDirection, cancellationToken);

            return model;
        }

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
        public async Task<TransactionsByOperatorModel> GetTransactionsForEstateByOperator(Guid estateId,
                                                                                          String startDate,
                                                                                          String endDate,
                                                                                          Int32 recordCount,
                                                                                          SortField sortField,
                                                                                          SortDirection sortDirection,
                                                                                          CancellationToken cancellationToken)
        {
            TransactionsByOperatorModel model = null;
            EstateReporting.Repository.SortDirection repoSortDirection = EstateReporting.Repository.SortDirection.NotSet;
            if (sortDirection == SortDirection.Ascending)
            {
                repoSortDirection = EstateReporting.Repository.SortDirection.Ascending;
            }
            else if (sortDirection == SortDirection.Descending)
            {
                repoSortDirection = EstateReporting.Repository.SortDirection.Descending;
            }

            EstateReporting.Repository.SortField repoSortField = EstateReporting.Repository.SortField.NotSet;
            if (sortField == SortField.Count)
            {
                repoSortField = EstateReporting.Repository.SortField.Count;
            }
            else if (sortField == SortField.Value)
            {
                repoSortField = EstateReporting.Repository.SortField.Value;
            }

            model = await this.RepositoryForReports.GetTransactionsForEstateByOperator(estateId, startDate, endDate, recordCount, repoSortField, repoSortDirection, cancellationToken);

            return model;
        }

        #endregion
    }
}