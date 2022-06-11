namespace EstateReporting.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// 
    /// </summary>
    public interface IEstateReportingRepositoryForReports
    {
        #region Methods

        Task<SettlementModel> GetSettlement(Guid estateId,
                                            Guid? merchantId,
                                            Guid settlementId,
                                            CancellationToken cancellationToken);

        Task<List<SettlementModel>> GetSettlements(Guid estateId,
                                                   Guid? merchantId,
                                                   String startDate,
                                                   String endDate,
                                                   CancellationToken cancellationToken);

        #endregion
    }
}