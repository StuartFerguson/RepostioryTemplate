namespace EstateReporting.Factories
{
    using System.Collections.Generic;
    using System.Linq;
    using BusinessLogic;
    using DataTransferObjects;
    using Models;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="EstateReporting.Factories.IModelFactory" />
    public class ModelFactory : IModelFactory
    {
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
    }
}