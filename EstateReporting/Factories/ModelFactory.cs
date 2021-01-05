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