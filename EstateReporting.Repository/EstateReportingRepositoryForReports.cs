namespace EstateReporting.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Shared.EntityFramework;

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
        private readonly Shared.EntityFramework.IDbContextFactory<EstateReportingContext> DbContextFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EstateReportingRepository" /> class.
        /// </summary>
        /// <param name="dbContextFactory">The database context factory.</param>
        public EstateReportingRepositoryForReports(Shared.EntityFramework.IDbContextFactory<EstateReportingContext> dbContextFactory)
        {
            this.DbContextFactory = dbContextFactory;
        }

        #endregion

        public async Task<SettlementByWeekModel> GetSettlementForEstateByWeek(Guid estateId,
                                                                              String startDate,
                                                                              String endDate,
                                                                              CancellationToken cancellationToken)
        {
            SettlementByWeekModel model = new SettlementByWeekModel
            {
                                              SettlementWeekModels = new List<SettlementWeekModel>()
                                          };

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.SettlementsView.Where(t => t.EstateId == estateId &&
                                                                   t.SettlementDate >= queryStartDate.Date && t.SettlementDate <= queryEndDate.Date)
                                      .GroupBy(txn => new
                                      {
                                          WeekNumber = txn.WeekNumber,
                                          Year = txn.YearNumber
                                      })
                                      .Select(txns => new
                                      {
                                          WeekNumber = txns.Key.WeekNumber,
                                          Year = txns.Key.Year,
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
        /// Gets the transactions for merchant by date.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
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

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.MerchantId == merchantId &&
                                                                   t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                                   t.TransactionType == "Sale").GroupBy(txn => txn.TransactionDate)
                                      .Select(txns => new
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

        public async Task<SettlementByMonthModel> GetSettlementForEstateByMonth(Guid estateId,
                                                                                String startDate,
                                                                                String endDate,
                                                                                CancellationToken cancellationToken)
        {
            SettlementByMonthModel model = new SettlementByMonthModel
            {
                                               SettlementMonthModels = new List<SettlementMonthModel>()
                                           };

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.SettlementsView.Where(t => t.EstateId == estateId &&
                                                                   t.SettlementDate >= queryStartDate.Date && t.SettlementDate <= queryEndDate.Date)
                                      .GroupBy(txn => new
                                      {
                                          MonthNumber = txn.MonthNumber,
                                          Year = txn.YearNumber
                                      })
                                      .Select(txns => new
                                      {
                                          MonthNumber = txns.Key.MonthNumber,
                                          Year = txns.Key.Year,
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

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                                   t.TransactionType == "Sale")
                                      .GroupBy(txn => new
                                                      {
                                                          WeekNumber = txn.WeekNumber,
                                                          Year = txn.YearNumber
                                                      })
                                      .Select(txns => new
                                                      {
                                                          WeekNumber = txns.Key.WeekNumber,
                                                          Year = txns.Key.Year,
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
        /// Gets the transactions for merchant by week.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
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

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.MerchantId == merchantId &&
                                                                   t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                                   t.TransactionType == "Sale")
                                      .GroupBy(txn => new
                                                      {
                                                          WeekNumber = txn.WeekNumber,
                                                          Year = txn.YearNumber
                                                      })
                                      .Select(txns => new
                                                      {
                                                          WeekNumber = txns.Key.WeekNumber,
                                                          Year = txns.Key.Year,
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
        /// Gets the transactions for estate by merchant.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortField"></param>
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

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                                   t.TransactionType == "Sale")
                                      .GroupBy(txn => new
                                                      {
                                                          MerchantId = txn.MerchantId,
                                                      })
                                      .Select(txns => new
                                                      {
                                                          MerchantId = txns.Key.MerchantId,
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

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.SettlementsView.Where(t => t.EstateId == estateId &&
                                                                   t.SettlementDate >= queryStartDate.Date && t.SettlementDate <= queryEndDate.Date)
                                      .GroupBy(txn => new
                                      {
                                          MerchantId = txn.MerchantId,
                                      })
                                      .Select(txns => new
                                      {
                                          MerchantId = txns.Key.MerchantId,
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

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                                   t.TransactionType == "Sale")
                                      .GroupBy(txn => new
                                      {
                                          OperatorName = txn.OperatorIdentifier,
                                      })
                                      .Select(txns => new
                                      {
                                          OperatorName = txns.Key.OperatorName,
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

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.SettlementsView.Where(t => t.EstateId == estateId &&
                                                                   t.SettlementDate >= queryStartDate.Date && t.SettlementDate <= queryEndDate.Date)
                                      .GroupBy(txn => new
                                      {
                                          OperatorName = txn.OperatorIdentifier,
                                      })
                                      .Select(txns => new
                                      {
                                          OperatorName = txns.Key.OperatorName,
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

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                                   t.TransactionType == "Sale")
                                      .GroupBy(txn => new
                                                      {
                                                          MonthNumber = txn.MonthNumber,
                                                          Year = txn.YearNumber
                                                      })
                                      .Select(txns => new
                                                      {
                                                          MonthNumber = txns.Key.MonthNumber,
                                                          Year = txns.Key.Year,
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
        /// Gets the transactions for merchant by month.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
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

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.MerchantId == merchantId &&
                                                                   t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                                   t.TransactionType == "Sale")
                                      .GroupBy(txn => new
                                                      {
                                                          MonthNumber = txn.MonthNumber,
                                                          Year = txn.YearNumber
                                                      })
                                      .Select(txns => new
                                                      {
                                                          MonthNumber = txns.Key.MonthNumber,
                                                          Year = txns.Key.Year,
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

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.TransactionsView.Where(t => t.EstateId == estateId &&
                                                                   t.TransactionDate >= queryStartDate.Date && t.TransactionDate <= queryEndDate.Date && t.IsAuthorised &&
                                                                   t.TransactionType == "Sale").GroupBy(txn => txn.TransactionDate)
                                      .Select(txns => new
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

        public async Task<SettlementByDayModel> GetSettlementForEstateByDate(Guid estateId,
                                                                                 String startDate,
                                                                                 String endDate,
                                                                                 CancellationToken cancellationToken)
        {
            SettlementByDayModel model = new SettlementByDayModel
            {
                                             SettlementDayModels = new List<SettlementDayModel>()
                                         };

            EstateReportingContext context = await this.DbContextFactory.GetContext(estateId, cancellationToken);

            DateTime queryStartDate = DateTime.ParseExact(startDate, "yyyyMMdd", null);
            DateTime queryEndDate = DateTime.ParseExact(endDate, "yyyyMMdd", null);

            var result = await context.SettlementsView.Where(t => t.EstateId == estateId &&
                                                                   t.SettlementDate >= queryStartDate.Date && 
                                                                   t.SettlementDate<= queryEndDate.Date)
                                      .GroupBy(txn => txn.SettlementDate)
                                      .Select(txns => new
                                      {
                                          Date = txns.Key,
                                          NumberofTransactionsSettled = txns.Count(),
                                          ValueOfSettlement = txns.Sum(t => t.CalculatedValue)
                                      }).ToListAsync(cancellationToken);

            result.ForEach(r => model.SettlementDayModels.Add(new SettlementDayModel()
            {
                CurrencyCode = "",
                Date = r.Date,
                NumberOfTransactionsSettled = r.NumberofTransactionsSettled,
                ValueOfSettlement = r.ValueOfSettlement
            }));

            return model;
        }
    }
}