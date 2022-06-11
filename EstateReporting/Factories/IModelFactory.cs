namespace EstateReporting.Factories
{
    using System.Collections.Generic;
    using DataTransferObjects;
    using Models;
    /// <summary>
    /// 
    /// </summary>
    public interface IModelFactory
    {
        #region Methods

        SettlementFeeResponse ConvertFrom(SettlementFeeModel model);

        List<SettlementFeeResponse> ConvertFrom(List<SettlementFeeModel> model);

        SettlementResponse ConvertFrom(SettlementModel model);

        List<SettlementResponse> ConvertFrom(List<SettlementModel> model);

        #endregion
    }
}