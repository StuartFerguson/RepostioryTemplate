namespace EstateReporting.Factories
{
    using System.Collections.Generic;
    using System.Linq;
    using DataTransferObjects;
    using Models;
    using SortDirection = BusinessLogic.SortDirection;
    using SortField = BusinessLogic.SortField;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EstateReporting.Factories.IModelFactory" />
    public class ModelFactory : IModelFactory
    {
        #region Methods

        public SettlementFeeResponse ConvertFrom(SettlementFeeModel model)
        {
            if (model == null)
            {
                return null;
            }

            SettlementFeeResponse response = new SettlementFeeResponse
                                             {
                                                 SettlementDate = model.SettlementDate,
                                                 CalculatedValue = model.CalculatedValue,
                                                 SettlementId = model.SettlementId,
                                                 MerchantId = model.MerchantId,
                                                 MerchantName = model.MerchantName,
                                                 FeeDescription = model.FeeDescription,
                                                 TransactionId = model.TransactionId,
                                                 IsSettled = model.IsSettled,
                                                 OperatorIdentifier = model.OperatorIdentifier
                                             };

            return response;
        }

        public List<SettlementFeeResponse> ConvertFrom(List<SettlementFeeModel> model)
        {
            if (model == null || model.Any() == false)
            {
                return new List<SettlementFeeResponse>();
            }

            List<SettlementFeeResponse> response = new List<SettlementFeeResponse>();

            model.ForEach(m => response.Add(this.ConvertFrom(m)));

            return response;
        }

        public SettlementResponse ConvertFrom(SettlementModel model)
        {
            if (model == null)
            {
                return null;
            }

            SettlementResponse response = new SettlementResponse
                                          {
                                              SettlementDate = model.SettlementDate,
                                              IsCompleted = model.IsCompleted,
                                              NumberOfFeesSettled = model.NumberOfFeesSettled,
                                              SettlementId = model.SettlementId,
                                              ValueOfFeesSettled = model.ValueOfFeesSettled,
                                          };

            model.SettlementFees.ForEach(f => response.SettlementFees.Add(this.ConvertFrom(f)));

            return response;
        }

        public List<SettlementResponse> ConvertFrom(List<SettlementModel> model)
        {
            if (model == null || model.Any() == false)
            {
                return new List<SettlementResponse>();
            }

            List<SettlementResponse> response = new List<SettlementResponse>();

            model.ForEach(m => response.Add(this.ConvertFrom(m)));

            return response;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionsByDayResponse ConvertFrom(TransactionsByDayModel model)
        {
            if (model.TransactionDayModels == null || model.TransactionDayModels.Any() == false)
            {
                return null;
            }

            TransactionsByDayResponse response = new TransactionsByDayResponse
                                                 {
                                                     TransactionDayResponses = new List<TransactionDayResponse>()
                                                 };

            model.TransactionDayModels.ForEach(m => response.TransactionDayResponses.Add(this.ConvertFrom(m)));

            return response;
        }

        public SettlementByDayResponse ConvertFrom(SettlementByDayModel model)
        {
            if (model.SettlementDayModels == null || model.SettlementDayModels.Any() == false)
            {
                return null;
            }

            SettlementByDayResponse response = new SettlementByDayResponse
                                               {
                                                   SettlementDayResponses = new List<SettlementDayResponse>()
                                               };

            model.SettlementDayModels.ForEach(m => response.SettlementDayResponses.Add(this.ConvertFrom(m)));

            return response;
        }

        public SettlementByMerchantResponse ConvertFrom(SettlementByMerchantModel model)
        {
            if (model.SettlementMerchantModels == null || model.SettlementMerchantModels.Any() == false)
            {
                return null;
            }

            SettlementByMerchantResponse response = new SettlementByMerchantResponse
                                                    {
                                                        SettlementMerchantResponses = new List<SettlementMerchantResponse>()
                                                    };

            model.SettlementMerchantModels.ForEach(m => response.SettlementMerchantResponses.Add(this.ConvertFrom(m)));

            return response;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionsByWeekResponse ConvertFrom(TransactionsByWeekModel model)
        {
            if (model.TransactionWeekModels == null || model.TransactionWeekModels.Any() == false)
            {
                return null;
            }

            TransactionsByWeekResponse response = new TransactionsByWeekResponse
                                                  {
                                                      TransactionWeekResponses = new List<TransactionWeekResponse>()
                                                  };

            model.TransactionWeekModels.ForEach(m => response.TransactionWeekResponses.Add(this.ConvertFrom(m)));

            return response;
        }

        public SettlementByWeekResponse ConvertFrom(SettlementByWeekModel model)
        {
            if (model.SettlementWeekModels == null || model.SettlementWeekModels.Any() == false)
            {
                return null;
            }

            SettlementByWeekResponse response = new SettlementByWeekResponse
                                                {
                                                    SettlementWeekResponses = new List<SettlementWeekResponse>()
                                                };

            model.SettlementWeekModels.ForEach(m => response.SettlementWeekResponses.Add(this.ConvertFrom(m)));

            return response;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionsByMonthResponse ConvertFrom(TransactionsByMonthModel model)
        {
            if (model.TransactionMonthModels == null || model.TransactionMonthModels.Any() == false)
            {
                return null;
            }

            TransactionsByMonthResponse response = new TransactionsByMonthResponse
                                                   {
                                                       TransactionMonthResponses = new List<TransactionMonthResponse>()
                                                   };

            model.TransactionMonthModels.ForEach(m => response.TransactionMonthResponses.Add(this.ConvertFrom(m)));

            return response;
        }

        public SettlementByMonthResponse ConvertFrom(SettlementByMonthModel model)
        {
            if (model.SettlementMonthModels == null || model.SettlementMonthModels.Any() == false)
            {
                return null;
            }

            SettlementByMonthResponse response = new SettlementByMonthResponse
                                                 {
                                                     SettlementMonthResponses = new List<SettlementMonthResponse>()
                                                 };

            model.SettlementMonthModels.ForEach(m => response.SettlementMonthResponses.Add(this.ConvertFrom(m)));

            return response;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionWeekResponse ConvertFrom(TransactionWeekModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new TransactionWeekResponse
                   {
                       ValueOfTransactions = model.ValueOfTransactions,
                       WeekNumber = model.WeekNumber,
                       CurrencyCode = model.CurrencyCode,
                       NumberOfTransactions = model.NumberOfTransactions,
                       Year = model.Year
                   };
        }

        public SettlementWeekResponse ConvertFrom(SettlementWeekModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new SettlementWeekResponse
                   {
                       ValueOfSettlement = model.ValueOfSettlement,
                       WeekNumber = model.WeekNumber,
                       CurrencyCode = model.CurrencyCode,
                       NumberOfTransactionsSettled = model.NumberOfTransactionsSettled,
                       Year = model.Year
                   };
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionMonthResponse ConvertFrom(TransactionMonthModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new TransactionMonthResponse
                   {
                       CurrencyCode = model.CurrencyCode,
                       MonthNumber = model.MonthNumber,
                       NumberOfTransactions = model.NumberOfTransactions,
                       ValueOfTransactions = model.ValueOfTransactions,
                       Year = model.Year
                   };
        }

        public SettlementMonthResponse ConvertFrom(SettlementMonthModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new SettlementMonthResponse
                   {
                       CurrencyCode = model.CurrencyCode,
                       MonthNumber = model.MonthNumber,
                       NumberOfTransactionsSettled = model.NumberOfTransactionsSettled,
                       ValueOfSettlement = model.ValueOfSettlement,
                       Year = model.Year
                   };
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionDayResponse ConvertFrom(TransactionDayModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new TransactionDayResponse
                   {
                       CurrencyCode = model.CurrencyCode,
                       Date = model.Date,
                       NumberOfTransactions = model.NumberOfTransactions,
                       ValueOfTransactions = model.ValueOfTransactions
                   };
        }

        public SettlementDayResponse ConvertFrom(SettlementDayModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new SettlementDayResponse
                   {
                       CurrencyCode = model.CurrencyCode,
                       Date = model.Date,
                       NumberOfTransactionsSettled = model.NumberOfTransactionsSettled,
                       ValueOfSettlement = model.ValueOfSettlement
                   };
        }

        public SettlementMerchantResponse ConvertFrom(SettlementMerchantModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new SettlementMerchantResponse
                   {
                       CurrencyCode = model.CurrencyCode,
                       MerchantId = model.MerchantId,
                       MerchantName = model.MerchantName,
                       NumberOfTransactionsSettled = model.NumberOfTransactionsSettled,
                       ValueOfSettlement = model.ValueOfSettlement
                   };
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionMerchantResponse ConvertFrom(TransactionMerchantModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new TransactionMerchantResponse
                   {
                       MerchantId = model.MerchantId,
                       CurrencyCode = model.CurrencyCode,
                       MerchantName = model.MerchantName,
                       ValueOfTransactions = model.ValueOfTransactions,
                       NumberOfTransactions = model.NumberOfTransactions
                   };
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionsByMerchantResponse ConvertFrom(TransactionsByMerchantModel model)
        {
            if (model.TransactionMerchantModels == null || model.TransactionMerchantModels.Any() == false)
            {
                return null;
            }

            TransactionsByMerchantResponse response = new TransactionsByMerchantResponse
                                                      {
                                                          TransactionMerchantResponses = new List<TransactionMerchantResponse>()
                                                      };

            model.TransactionMerchantModels.ForEach(m => response.TransactionMerchantResponses.Add(this.ConvertFrom(m)));

            return response;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionOperatorResponse ConvertFrom(TransactionOperatorModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new TransactionOperatorResponse
                   {
                       CurrencyCode = model.CurrencyCode,
                       OperatorName = model.OperatorName,
                       ValueOfTransactions = model.ValueOfTransactions,
                       NumberOfTransactions = model.NumberOfTransactions
                   };
        }

        public SettlementOperatorResponse ConvertFrom(SettlementOperatorModel model)
        {
            if (model == null)
            {
                return null;
            }

            return new SettlementOperatorResponse
                   {
                       CurrencyCode = model.CurrencyCode,
                       OperatorName = model.OperatorName,
                       ValueOfSettlement = model.ValueOfSettlement,
                       NumberOfTransactionsSettled = model.NumberOfTransactionsSettled
                   };
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public TransactionsByOperatorResponse ConvertFrom(TransactionsByOperatorModel model)
        {
            if (model.TransactionOperatorModels == null || model.TransactionOperatorModels.Any() == false)
            {
                return null;
            }

            TransactionsByOperatorResponse response = new TransactionsByOperatorResponse
                                                      {
                                                          TransactionOperatorResponses = new List<TransactionOperatorResponse>()
                                                      };

            model.TransactionOperatorModels.ForEach(m => response.TransactionOperatorResponses.Add(this.ConvertFrom(m)));

            return response;
        }

        public SettlementByOperatorResponse ConvertFrom(SettlementByOperatorModel model)
        {
            if (model.SettlementOperatorModels == null || model.SettlementOperatorModels.Any() == false)
            {
                return null;
            }

            SettlementByOperatorResponse response = new SettlementByOperatorResponse
                                                    {
                                                        SettlementOperatorResponses = new List<SettlementOperatorResponse>()
                                                    };

            model.SettlementOperatorModels.ForEach(m => response.SettlementOperatorResponses.Add(this.ConvertFrom(m)));

            return response;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public SortDirection ConvertFrom(DataTransferObjects.SortDirection model)
        {
            SortDirection result = SortDirection.Ascending;
            if (model == DataTransferObjects.SortDirection.Ascending)
            {
                result = SortDirection.Ascending;
            }
            else if (model == DataTransferObjects.SortDirection.Descending)
            {
                result = SortDirection.Descending;
            }

            return result;
        }

        /// <summary>
        /// Converts from.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public SortField ConvertFrom(DataTransferObjects.SortField model)
        {
            SortField result = SortField.Value;
            if (model == DataTransferObjects.SortField.Count)
            {
                result = SortField.Count;
            }
            else if (model == DataTransferObjects.SortField.Value)
            {
                result = SortField.Value;
            }

            return result;
        }

        #endregion
    }
}