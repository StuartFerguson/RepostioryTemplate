namespace EstateReporting.Factories
{
    using System.Collections.Generic;
    using System.Linq;
    using DataTransferObjects;
    using Models;

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

        #endregion
    }
}