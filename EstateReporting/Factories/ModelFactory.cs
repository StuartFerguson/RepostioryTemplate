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
        
        #endregion
    }
}