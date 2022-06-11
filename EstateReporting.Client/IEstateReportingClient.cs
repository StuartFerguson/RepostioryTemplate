namespace EstateReporting.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DataTransferObjects;

    /// <summary>
    /// 
    /// </summary>
    public interface IEstateReportingClient
    {
        #region Methods

        Task<SettlementResponse> GetSettlement(String accessToken,
                                               Guid estateId,
                                               Guid? merchantId,
                                               Guid settlementId,
                                               CancellationToken cancellationToken);

        Task<List<SettlementResponse>> GetSettlements(String accessToken,
                                                      Guid estateId,
                                                      Guid? merchantId,
                                                      String startDate,
                                                      String endDate,
                                                      CancellationToken cancellationToken);
        
        #endregion
    }
}