namespace EstateReporting.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Database.ViewEntities;
    using Microsoft.EntityFrameworkCore;
    using Models;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EstateReporting.Repository.IEstateReportingRepositoryForReports" />
    [ExcludeFromCodeCoverage]
    public class EstateReportingRepositoryForReports : IEstateReportingRepositoryForReports
    {
        #region Fields

        /// <summary>
        /// The database context factory
        /// </summary>
        private readonly Shared.EntityFramework.IDbContextFactory<EstateReportingGenericContext> DbContextFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EstateReportingRepository" /> class.
        /// </summary>
        /// <param name="dbContextFactory">The database context factory.</param>
        public EstateReportingRepositoryForReports(Shared.EntityFramework.IDbContextFactory<EstateReportingGenericContext> dbContextFactory)
        {
            this.DbContextFactory = dbContextFactory;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the settlement.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="settlementId">The settlement identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<SettlementModel> GetSettlement(Guid estateId,
                                                         Guid? merchantId,
                                                         Guid settlementId,
                                                         CancellationToken cancellationToken)
        {
            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            IQueryable<SettlementView> query = context.SettlementsView.Where(t => t.EstateId == estateId && t.SettlementId == settlementId).AsQueryable();

            if (merchantId.HasValue)
            {
                query = query.Where(t => t.MerchantId == merchantId);
            }

            var result = query.AsEnumerable().GroupBy(t => new
                                                           {
                                                               t.SettlementId,
                                                               t.SettlementDate,
                                                               t.IsCompleted
                                                           }).SingleOrDefault();

            if (result == null)
                return null;

            SettlementModel model = new SettlementModel
                                    {
                                        SettlementDate = result.Key.SettlementDate,
                                        SettlementId = result.Key.SettlementId,
                                        NumberOfFeesSettled = result.Count(),
                                        ValueOfFeesSettled = result.Sum(x => x.CalculatedValue),
                                        IsCompleted = result.Key.IsCompleted
                                    };

            result.ToList().ForEach(f => model.SettlementFees.Add(new SettlementFeeModel
                                                                  {
                                                                      SettlementDate = f.SettlementDate,
                                                                      SettlementId = f.SettlementId,
                                                                      CalculatedValue = f.CalculatedValue,
                                                                      MerchantId = f.MerchantId,
                                                                      MerchantName = f.MerchantName,
                                                                      FeeDescription = f.FeeDescription,
                                                                      IsSettled = f.IsSettled,
                                                                      TransactionId = f.TransactionId,
                                                                      OperatorIdentifier = f.OperatorIdentifier
                                                                  }));

            return model;
        }

        /// <summary>
        /// Gets the settlement for estate by date.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<SettlementByDayModel> GetSettlementForEstateByDate(Guid estateId,
                                                                             String startDate,
                                                                             String endDate,
                                                                             CancellationToken cancellationToken)
        {
            SettlementByDayModel model = new SettlementByDayModel
                                         {
                                             SettlementDayModels = new List<SettlementDayModel>()
                                         };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.SettlementsView
                                      .Where(t => t.EstateId == estateId && t.SettlementDate >= queryStartDate.Date && t.SettlementDate <= queryEndDate.Date)
                                      .GroupBy(txn => txn.SettlementDate).Select(txns => new
                                                                                         {
                                                                                             Date = txns.Key,
                                                                                             NumberofTransactionsSettled = txns.Count(),
                                                                                             ValueOfSettlement = txns.Sum(t => t.CalculatedValue)
                                                                                         }).ToListAsync(cancellationToken);

            result.ForEach(r => model.SettlementDayModels.Add(new SettlementDayModel
                                                              {
                                                                  CurrencyCode = "",
                                                                  Date = r.Date,
                                                                  NumberOfTransactionsSettled = r.NumberofTransactionsSettled,
                                                                  ValueOfSettlement = r.ValueOfSettlement
                                                              }));

            return model;
        }

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
        public async Task<SettlementByMerchantModel> GetSettlementForEstateByMerchant(Guid estateId,
                                                                                      String startDate,
                                                                                      String endDate,
                                                                                      Int32 recordCount,
                                                                                      SortField sortField,
                                                                                      SortDirection sortDirection,
                                                                                      CancellationToken cancellationToken)
        {
            SettlementByMerchantModel model = new SettlementByMerchantModel
                                              {
                                                  SettlementMerchantModels = new List<SettlementMerchantModel>()
                                              };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.SettlementsView.Where(t => t.EstateId == estateId &&
                                                                  t.SettlementDate >= queryStartDate.Date && t.SettlementDate <= queryEndDate.Date).GroupBy(txn => new
                {
                    txn.MerchantId,
                }).Select(txns => new
                                  {
                                      txns.Key.MerchantId,
                                      NumberofTransactionsSettled = txns.Count(),
                                      ValueOfSettlement = txns.Sum(t => t.CalculatedValue)
                                  }).ToListAsync(cancellationToken);

            if (sortDirection == SortDirection.Ascending && sortField == SortField.Value)
            {
                result = result.OrderBy(o => o.ValueOfSettlement).ToList();
            }
            else if (sortDirection == SortDirection.Ascending && sortField == SortField.Count)
            {
                result = result.OrderBy(o => o.NumberofTransactionsSettled).ToList();
            }

            if (sortDirection == SortDirection.Descending && sortField == SortField.Value)
            {
                result = result.OrderByDescending(o => o.ValueOfSettlement).ToList();
            }
            else if (sortDirection == SortDirection.Descending && sortField == SortField.Count)
            {
                result = result.OrderByDescending(o => o.NumberofTransactionsSettled).ToList();
            }

            result = result.Take(recordCount).ToList();

            var result2 = result.Join(context.Merchants,
                                      x => x.MerchantId,
                                      y => y.MerchantId,
                                      (x,
                                       y) => new
                                             {
                                                 x.MerchantId,
                                                 x.NumberofTransactionsSettled,
                                                 x.ValueOfSettlement,
                                                 y.Name
                                             }).ToList();

            result2.ForEach(r => model.SettlementMerchantModels.Add(new SettlementMerchantModel
                                                                    {
                                                                        CurrencyCode = "",
                                                                        MerchantId = r.MerchantId,
                                                                        MerchantName = r.Name,
                                                                        NumberOfTransactionsSettled = r.NumberofTransactionsSettled,
                                                                        ValueOfSettlement = r.ValueOfSettlement
                                                                    }));

            return model;
        }

        /// <summary>
        /// Gets the settlement for estate by month.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<SettlementByMonthModel> GetSettlementForEstateByMonth(Guid estateId,
                                                                                String startDate,
                                                                                String endDate,
                                                                                CancellationToken cancellationToken)
        {
            SettlementByMonthModel model = new SettlementByMonthModel
                                           {
                                               SettlementMonthModels = new List<SettlementMonthModel>()
                                           };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.SettlementsView.Where(t => t.EstateId == estateId &&
                                                                  t.SettlementDate >= queryStartDate.Date && t.SettlementDate <= queryEndDate.Date).GroupBy(txn => new
                {
                    txn.MonthNumber,
                    Year = txn.YearNumber
                }).Select(txns => new
                                  {
                                      txns.Key.MonthNumber,
                                      txns.Key.Year,
                                      NumberofTransactionsSettled = txns.Count(),
                                      ValueOfSettlement = txns.Sum(t => t.CalculatedValue)
                                  }).ToListAsync(cancellationToken);

            result.ForEach(r => model.SettlementMonthModels.Add(new SettlementMonthModel
                                                                {
                                                                    CurrencyCode = "",
                                                                    MonthNumber = r.MonthNumber,
                                                                    Year = r.Year,
                                                                    ValueOfSettlement = r.ValueOfSettlement,
                                                                    NumberOfTransactionsSettled = r.NumberofTransactionsSettled
                                                                }));

            return model;
        }

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
        public async Task<SettlementByOperatorModel> GetSettlementForEstateByOperator(Guid estateId,
                                                                                      String startDate,
                                                                                      String endDate,
                                                                                      Int32 recordCount,
                                                                                      SortField sortField,
                                                                                      SortDirection sortDirection,
                                                                                      CancellationToken cancellationToken)
        {
            SettlementByOperatorModel model = new SettlementByOperatorModel
                                              {
                                                  SettlementOperatorModels = new List<SettlementOperatorModel>()
                                              };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.SettlementsView.Where(t => t.EstateId == estateId &&
                                                                  t.SettlementDate >= queryStartDate.Date && t.SettlementDate <= queryEndDate.Date).GroupBy(txn => new
                {
                    OperatorName = txn.OperatorIdentifier,
                }).Select(txns => new
                                  {
                                      txns.Key.OperatorName,
                                      NumberofTransactionsSettled = txns.Count(),
                                      ValueOfSettlement = txns.Sum(t => t.CalculatedValue)
                                  }).ToListAsync(cancellationToken);

            if (sortDirection == SortDirection.Ascending && sortField == SortField.Value)
            {
                result = result.OrderBy(o => o.ValueOfSettlement).ToList();
            }
            else if (sortDirection == SortDirection.Ascending && sortField == SortField.Count)
            {
                result = result.OrderBy(o => o.NumberofTransactionsSettled).ToList();
            }

            if (sortDirection == SortDirection.Descending && sortField == SortField.Value)
            {
                result = result.OrderByDescending(o => o.ValueOfSettlement).ToList();
            }
            else if (sortDirection == SortDirection.Descending && sortField == SortField.Count)
            {
                result = result.OrderByDescending(o => o.NumberofTransactionsSettled).ToList();
            }

            result = result.Take(recordCount).ToList();

            result.ForEach(r => model.SettlementOperatorModels.Add(new SettlementOperatorModel
                                                                   {
                                                                       CurrencyCode = "",
                                                                       OperatorName = r.OperatorName,
                                                                       ValueOfSettlement = r.ValueOfSettlement,
                                                                       NumberOfTransactionsSettled = r.NumberofTransactionsSettled
                                                                   }));

            return model;
        }

        /// <summary>
        /// Gets the settlement for estate by week.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<SettlementByWeekModel> GetSettlementForEstateByWeek(Guid estateId,
                                                                              String startDate,
                                                                              String endDate,
                                                                              CancellationToken cancellationToken)
        {
            SettlementByWeekModel model = new SettlementByWeekModel
                                          {
                                              SettlementWeekModels = new List<SettlementWeekModel>()
                                          };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.SettlementsView.Where(t => t.EstateId == estateId &&
                                                                  t.SettlementDate >= queryStartDate.Date && t.SettlementDate <= queryEndDate.Date).GroupBy(txn => new
                {
                    txn.WeekNumber,
                    Year = txn.YearNumber
                }).Select(txns => new
                                  {
                                      txns.Key.WeekNumber,
                                      txns.Key.Year,
                                      NumberofTransactionsSettled = txns.Count(),
                                      ValueOfSettlement = txns.Sum(t => t.CalculatedValue)
                                  }).ToListAsync(cancellationToken);

            result.ForEach(r => model.SettlementWeekModels.Add(new SettlementWeekModel
                                                               {
                                                                   CurrencyCode = "",
                                                                   WeekNumber = r.WeekNumber,
                                                                   Year = r.Year,
                                                                   ValueOfSettlement = r.ValueOfSettlement,
                                                                   NumberOfTransactionsSettled = r.NumberofTransactionsSettled
                                                               }));

            return model;
        }

        /// <summary>
        /// Gets the settlement for merchant by date.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<SettlementByDayModel> GetSettlementForMerchantByDate(Guid estateId,
                                                                               Guid merchantId,
                                                                               String startDate,
                                                                               String endDate,
                                                                               CancellationToken cancellationToken)
        {
            SettlementByDayModel model = new SettlementByDayModel
                                         {
                                             SettlementDayModels = new List<SettlementDayModel>()
                                         };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.SettlementsView.Where(t => t.EstateId == estateId &&
                                                                  t.MerchantId == merchantId && t.SettlementDate >= queryStartDate.Date &&
                                                                  t.SettlementDate <= queryEndDate.Date).GroupBy(txn => txn.SettlementDate).Select(txns => new
                {
                    Date = txns.Key,
                    NumberOfTransactionsSettled = txns.Count(),
                    ValueOfSettlement = txns.Sum(t => t.CalculatedValue)
                }).ToListAsync(cancellationToken);

            result.ForEach(r => model.SettlementDayModels.Add(new SettlementDayModel
                                                              {
                                                                  CurrencyCode = "",
                                                                  Date = r.Date,
                                                                  NumberOfTransactionsSettled = r.NumberOfTransactionsSettled,
                                                                  ValueOfSettlement = r.ValueOfSettlement
                                                              }));

            return model;
        }

        /// <summary>
        /// Gets the settlement for merchant by month.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<SettlementByMonthModel> GetSettlementForMerchantByMonth(Guid estateId,
                                                                                  Guid merchantId,
                                                                                  String startDate,
                                                                                  String endDate,
                                                                                  CancellationToken cancellationToken)
        {
            SettlementByMonthModel model = new SettlementByMonthModel
                                           {
                                               SettlementMonthModels = new List<SettlementMonthModel>()
                                           };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.SettlementsView.Where(t => t.EstateId == estateId &&
                                                                  t.MerchantId == merchantId && t.SettlementDate >= queryStartDate.Date &&
                                                                  t.SettlementDate <= queryEndDate.Date).GroupBy(txn => new
                                                                                                                     {
                                                                                                                         txn.MonthNumber,
                                                                                                                         Year = txn.YearNumber
                                                                                                                     }).Select(txns => new
                {
                    txns.Key.MonthNumber,
                    txns.Key.Year,
                    NumberOfTransactionsSettled = txns.Count(),
                    ValueOfSettlement = txns.Sum(t => t.CalculatedValue)
                }).ToListAsync(cancellationToken);

            result.ForEach(r => model.SettlementMonthModels.Add(new SettlementMonthModel
                                                                {
                                                                    CurrencyCode = "",
                                                                    MonthNumber = r.MonthNumber,
                                                                    Year = r.Year,
                                                                    NumberOfTransactionsSettled = r.NumberOfTransactionsSettled,
                                                                    ValueOfSettlement = r.ValueOfSettlement
                                                                }));

            return model;
        }

        /// <summary>
        /// Gets the settlement for merchant by week.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<SettlementByWeekModel> GetSettlementForMerchantByWeek(Guid estateId,
                                                                                Guid merchantId,
                                                                                String startDate,
                                                                                String endDate,
                                                                                CancellationToken cancellationToken)
        {
            SettlementByWeekModel model = new SettlementByWeekModel
                                          {
                                              SettlementWeekModels = new List<SettlementWeekModel>()
                                          };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.SettlementsView.Where(t => t.EstateId == estateId &&
                                                                  t.MerchantId == merchantId && t.SettlementDate >= queryStartDate.Date &&
                                                                  t.SettlementDate <= queryEndDate.Date).GroupBy(txn => new
                                                                                                                     {
                                                                                                                         txn.WeekNumber,
                                                                                                                         Year = txn.YearNumber
                                                                                                                     }).Select(txns => new
                {
                    txns.Key.WeekNumber,
                    txns.Key.Year,
                    NumberOfTransactionsSettled = txns.Count(),
                    ValueOfSettlement = txns.Sum(t => t.CalculatedValue)
                }).ToListAsync(cancellationToken);

            result.ForEach(r => model.SettlementWeekModels.Add(new SettlementWeekModel
                                                               {
                                                                   CurrencyCode = "",
                                                                   WeekNumber = r.WeekNumber,
                                                                   Year = r.Year,
                                                                   NumberOfTransactionsSettled = r.NumberOfTransactionsSettled,
                                                                   ValueOfSettlement = r.ValueOfSettlement
                                                               }));

            return model;
        }

        /// <summary>
        /// Gets the settlements.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<List<SettlementModel>> GetSettlements(Guid estateId,
                                                                Guid? merchantId,
                                                                String startDate,
                                                                String endDate,
                                                                CancellationToken cancellationToken)
        {
            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            IQueryable<SettlementView> query = context.SettlementsView.Where(t => t.EstateId == estateId &&
                                                                                  t.SettlementDate >= queryStartDate.Date && t.SettlementDate <= queryEndDate.Date)
                                                      .AsQueryable();

            if (merchantId.HasValue)
            {
                query = query.Where(t => t.MerchantId == merchantId);
            }

            List<SettlementModel> result = await query.GroupBy(t => new
                                                                    {
                                                                        t.SettlementId,
                                                                        t.SettlementDate,
                                                                        t.IsCompleted
                                                                    }).Select(t => new SettlementModel
                                                                                   {
                                                                                       SettlementId = t.Key.SettlementId,
                                                                                       SettlementDate = t.Key.SettlementDate,
                                                                                       NumberOfFeesSettled = t.Count(),
                                                                                       ValueOfFeesSettled = t.Sum(x => x.CalculatedValue),
                                                                                       IsCompleted = t.Key.IsCompleted
                                                                                   }).OrderByDescending(t => t.SettlementDate).ToListAsync(cancellationToken);

            return result;
        }

        /// <summary>
        /// Gets the transactions for estate by date.
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
            TransactionsByDayModel model = new TransactionsByDayModel
                                           {
                                               TransactionDayModels = new List<TransactionDayModel>()
                                           };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                                   t.TransactionType == "Sale").GroupBy(txn => txn.TransactionDate).Select(txns => new
                {
                    Date = txns.Key,
                    NumberofTransactions = txns.Count(),
                    ValueOfTransactions = txns.Sum(t => t.Amount)
                }).ToListAsync(cancellationToken);

            result.ForEach(r => model.TransactionDayModels.Add(new TransactionDayModel
                                                               {
                                                                   CurrencyCode = "",
                                                                   Date = r.Date,
                                                                   NumberOfTransactions = r.NumberofTransactions,
                                                                   ValueOfTransactions = r.ValueOfTransactions
                                                               }));

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
            TransactionsByMerchantModel model = new TransactionsByMerchantModel
                                                {
                                                    TransactionMerchantModels = new List<TransactionMerchantModel>()
                                                };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                                   t.TransactionType == "Sale").GroupBy(txn => new
                                                                                                               {
                                                                                                                   txn.MerchantId,
                                                                                                               }).Select(txns => new
                {
                    txns.Key.MerchantId,
                    NumberofTransactions = txns.Count(),
                    ValueOfTransactions = txns.Sum(t => t.Amount)
                }).ToListAsync(cancellationToken);

            if (sortDirection == SortDirection.Ascending && sortField == SortField.Value)
            {
                result = result.OrderBy(o => o.ValueOfTransactions).ToList();
            }
            else if (sortDirection == SortDirection.Ascending && sortField == SortField.Count)
            {
                result = result.OrderBy(o => o.NumberofTransactions).ToList();
            }

            if (sortDirection == SortDirection.Descending && sortField == SortField.Value)
            {
                result = result.OrderByDescending(o => o.ValueOfTransactions).ToList();
            }
            else if (sortDirection == SortDirection.Descending && sortField == SortField.Count)
            {
                result = result.OrderByDescending(o => o.NumberofTransactions).ToList();
            }

            result = result.Take(recordCount).ToList();

            var result2 = result.Join(context.Merchants,
                                      x => x.MerchantId,
                                      y => y.MerchantId,
                                      (x,
                                       y) => new
                                             {
                                                 x.MerchantId,
                                                 x.NumberofTransactions,
                                                 x.ValueOfTransactions,
                                                 y.Name
                                             }).ToList();

            result2.ForEach(r => model.TransactionMerchantModels.Add(new TransactionMerchantModel
                                                                     {
                                                                         CurrencyCode = "",
                                                                         MerchantId = r.MerchantId,
                                                                         MerchantName = r.Name,
                                                                         NumberOfTransactions = r.NumberofTransactions,
                                                                         ValueOfTransactions = r.ValueOfTransactions
                                                                     }));

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
        [ExcludeFromCodeCoverage]
        public async Task<TransactionsByMonthModel> GetTransactionsForEstateByMonth(Guid estateId,
                                                                                    String startDate,
                                                                                    String endDate,
                                                                                    CancellationToken cancellationToken)
        {
            TransactionsByMonthModel model = new TransactionsByMonthModel
                                             {
                                                 TransactionMonthModels = new List<TransactionMonthModel>()
                                             };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                                   t.TransactionType == "Sale").GroupBy(txn => new
                                                                                                               {
                                                                                                                   txn.MonthNumber,
                                                                                                                   Year = txn.YearNumber
                                                                                                               }).Select(txns => new
                {
                    txns.Key.MonthNumber,
                    txns.Key.Year,
                    NumberofTransactions = txns.Count(),
                    ValueOfTransactions = txns.Sum(t => t.Amount)
                }).ToListAsync(cancellationToken);

            result.ForEach(r => model.TransactionMonthModels.Add(new TransactionMonthModel
                                                                 {
                                                                     CurrencyCode = "",
                                                                     MonthNumber = r.MonthNumber,
                                                                     Year = r.Year,
                                                                     NumberOfTransactions = r.NumberofTransactions,
                                                                     ValueOfTransactions = r.ValueOfTransactions
                                                                 }));

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
            TransactionsByOperatorModel model = new TransactionsByOperatorModel
                                                {
                                                    TransactionOperatorModels = new List<TransactionOperatorModel>()
                                                };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                                   t.TransactionType == "Sale").GroupBy(txn => new
                                                                                                               {
                                                                                                                   OperatorName = txn.OperatorIdentifier,
                                                                                                               }).Select(txns => new
                {
                    txns.Key.OperatorName,
                    NumberofTransactions = txns.Count(),
                    ValueOfTransactions = txns.Sum(t => t.Amount)
                }).ToListAsync(cancellationToken);

            if (sortDirection == SortDirection.Ascending && sortField == SortField.Value)
            {
                result = result.OrderBy(o => o.ValueOfTransactions).ToList();
            }
            else if (sortDirection == SortDirection.Ascending && sortField == SortField.Count)
            {
                result = result.OrderBy(o => o.NumberofTransactions).ToList();
            }

            if (sortDirection == SortDirection.Descending && sortField == SortField.Value)
            {
                result = result.OrderByDescending(o => o.ValueOfTransactions).ToList();
            }
            else if (sortDirection == SortDirection.Descending && sortField == SortField.Count)
            {
                result = result.OrderByDescending(o => o.NumberofTransactions).ToList();
            }

            result = result.Take(recordCount).ToList();

            result.ForEach(r => model.TransactionOperatorModels.Add(new TransactionOperatorModel
                                                                    {
                                                                        CurrencyCode = "",
                                                                        OperatorName = r.OperatorName,
                                                                        NumberOfTransactions = r.NumberofTransactions,
                                                                        ValueOfTransactions = r.ValueOfTransactions
                                                                    }));

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
        [ExcludeFromCodeCoverage]
        public async Task<TransactionsByWeekModel> GetTransactionsForEstateByWeek(Guid estateId,
                                                                                  String startDate,
                                                                                  String endDate,
                                                                                  CancellationToken cancellationToken)
        {
            TransactionsByWeekModel model = new TransactionsByWeekModel
                                            {
                                                TransactionWeekModels = new List<TransactionWeekModel>()
                                            };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                                   t.TransactionType == "Sale").GroupBy(txn => new
                                                                                                               {
                                                                                                                   txn.WeekNumber,
                                                                                                                   Year = txn.YearNumber
                                                                                                               }).Select(txns => new
                {
                    txns.Key.WeekNumber,
                    txns.Key.Year,
                    NumberofTransactions = txns.Count(),
                    ValueOfTransactions = txns.Sum(t => t.Amount)
                }).ToListAsync(cancellationToken);

            result.ForEach(r => model.TransactionWeekModels.Add(new TransactionWeekModel
                                                                {
                                                                    CurrencyCode = "",
                                                                    WeekNumber = r.WeekNumber,
                                                                    Year = r.Year,
                                                                    NumberOfTransactions = r.NumberofTransactions,
                                                                    ValueOfTransactions = r.ValueOfTransactions
                                                                }));

            return model;
        }

        /// <summary>
        /// Gets the transactions for merchant by date.
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
            TransactionsByDayModel model = new TransactionsByDayModel
                                           {
                                               TransactionDayModels = new List<TransactionDayModel>()
                                           };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.MerchantId == merchantId && t.TransactionDate >= queryStartDate.Date &&
                                                                   t.TransactionDate <= queryEndDate.Date && t.IsAuthorised && t.TransactionType == "Sale")
                                      .GroupBy(txn => txn.TransactionDate).Select(txns => new
                                                                                          {
                                                                                              Date = txns.Key,
                                                                                              NumberofTransactions = txns.Count(),
                                                                                              ValueOfTransactions = txns.Sum(t => t.Amount)
                                                                                          }).ToListAsync(cancellationToken);

            result.ForEach(r => model.TransactionDayModels.Add(new TransactionDayModel
                                                               {
                                                                   CurrencyCode = "",
                                                                   Date = r.Date,
                                                                   NumberOfTransactions = r.NumberofTransactions,
                                                                   ValueOfTransactions = r.ValueOfTransactions
                                                               }));

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
            TransactionsByMonthModel model = new TransactionsByMonthModel
                                             {
                                                 TransactionMonthModels = new List<TransactionMonthModel>()
                                             };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.MerchantId == merchantId && t.TransactionDate >= queryStartDate.Date &&
                                                                   t.TransactionDate <= queryEndDate.Date && t.IsAuthorised && t.TransactionType == "Sale")
                                      .GroupBy(txn => new
                                                      {
                                                          txn.MonthNumber,
                                                          Year = txn.YearNumber
                                                      }).Select(txns => new
                                                                        {
                                                                            txns.Key.MonthNumber,
                                                                            txns.Key.Year,
                                                                            NumberofTransactions = txns.Count(),
                                                                            ValueOfTransactions = txns.Sum(t => t.Amount)
                                                                        }).ToListAsync(cancellationToken);

            result.ForEach(r => model.TransactionMonthModels.Add(new TransactionMonthModel
                                                                 {
                                                                     CurrencyCode = "",
                                                                     MonthNumber = r.MonthNumber,
                                                                     Year = r.Year,
                                                                     NumberOfTransactions = r.NumberofTransactions,
                                                                     ValueOfTransactions = r.ValueOfTransactions
                                                                 }));

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
            TransactionsByWeekModel model = new TransactionsByWeekModel
                                            {
                                                TransactionWeekModels = new List<TransactionWeekModel>()
                                            };

            EstateReportingGenericContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.MerchantId == merchantId && t.TransactionDate >= queryStartDate.Date &&
                                                                   t.TransactionDate <= queryEndDate.Date && t.IsAuthorised && t.TransactionType == "Sale")
                                      .GroupBy(txn => new
                                                      {
                                                          txn.WeekNumber,
                                                          Year = txn.YearNumber
                                                      }).Select(txns => new
                                                                        {
                                                                            txns.Key.WeekNumber,
                                                                            txns.Key.Year,
                                                                            NumberofTransactions = txns.Count(),
                                                                            ValueOfTransactions = txns.Sum(t => t.Amount)
                                                                        }).ToListAsync(cancellationToken);

            result.ForEach(r => model.TransactionWeekModels.Add(new TransactionWeekModel
                                                                {
                                                                    CurrencyCode = "",
                                                                    WeekNumber = r.WeekNumber,
                                                                    Year = r.Year,
                                                                    NumberOfTransactions = r.NumberofTransactions,
                                                                    ValueOfTransactions = r.ValueOfTransactions
                                                                }));

            return model;
        }

        #endregion
    }
}