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
        /// <param name="groupingType">Type of the grouping.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<TransactionsByDayModel> GetTransactionsForEstate(Guid estateId,
                                                              String startDate,
                                                              String endDate,
                                                              GroupingType groupingType,
                                                              CancellationToken cancellationToken);

        #endregion

        //Task<TransactionsByDayModel> GetTransactionsForMerchant(Guid estateId,
        //                                                        Guid merchantId,
        //                                                        DateTime startDate,
        //                                                        DateTime endDate,
        //                                                        GroupingType groupingType,
        //                                                        CancellationToken cancellationToken);
    }

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
        /// Initializes a new instance of the <see cref="ReportingManager"/> class.
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
        /// <param name="groupingType">Type of the grouping.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByDayModel> GetTransactionsForEstate(Guid estateId,
                                                                           String startDate,
                                                                           String endDate,
                                                                           GroupingType groupingType,
                                                                           CancellationToken cancellationToken)
        {
            TransactionsByDayModel model = null;

            if (groupingType == GroupingType.ByDate)
            {
                model = await this.Repository.GetTransactionsForEstateByDate(estateId, startDate, endDate, cancellationToken);
            }

            return model;
        }

        #endregion
    }
}