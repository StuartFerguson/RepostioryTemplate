using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateReporting.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using BusinessLogic;
    using DataTransferObjects;
    using Factories;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route(ReportingController.ControllerRoute)]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class ReportingController : ControllerBase
    {
        /// <summary>
        /// The reporting manager
        /// </summary>
        private readonly IReportingManager ReportingManager;

        /// <summary>
        /// The model factory
        /// </summary>
        private readonly IModelFactory ModelFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportingController"/> class.
        /// </summary>
        /// <param name="reportingManager">The reporting manager.</param>
        /// <param name="modelFactory">The model factory.</param>
        public ReportingController(IReportingManager reportingManager, IModelFactory modelFactory)
        {
            this.ReportingManager = reportingManager;
            this.ModelFactory = modelFactory;
        }

        #region Others

        /// <summary>
        /// The controller name
        /// </summary>
        public const String ControllerName = "reporting";

        /// <summary>
        /// The controller route
        /// </summary>
        private const String ControllerRoute = "api/" + ReportingController.ControllerName;

        #endregion

        /// <summary>
        /// Gets the transaction for estate by date.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("estates/{estateId}/transactions/bydate")]
        public async Task<IActionResult> GetTransactionForEstateByDate([FromRoute] Guid estateId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            TransactionsByDayModel transactionsByDay = await this.ReportingManager.GetTransactionsForEstateByDate(estateId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByDayResponse response = this.ModelFactory.ConvertFrom(transactionsByDay);

            return this.Ok(response);
        }

        /// <summary>
        /// Gets the transaction for merchant by date.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("estates/{estateId}/merchants/{merchantId}/transactions/bydate")]
        public async Task<IActionResult> GetTransactionForMerchantByDate([FromRoute] Guid estateId, [FromRoute] Guid merchantId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            TransactionsByDayModel transactionsByDay = await this.ReportingManager.GetTransactionsForMerchantByDate(estateId, merchantId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByDayResponse response = this.ModelFactory.ConvertFrom(transactionsByDay);

            return this.Ok(response);
        }

        [HttpGet]
        [Route("estates/{estateId}/transactions/byweek")]
        public async Task<IActionResult> GetTransactionForEstateByWeek([FromRoute] Guid estateId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            TransactionsByWeekModel transactionsByWeek = await this.ReportingManager.GetTransactionsForEstateByWeek(estateId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByWeekResponse response = this.ModelFactory.ConvertFrom(transactionsByWeek);

            return this.Ok(response);
        }

        [HttpGet]
        [Route("estates/{estateId}/merchants/{merchantId}/transactions/byweek")]
        public async Task<IActionResult> GetTransactionForMerchantByWeek([FromRoute] Guid estateId, [FromRoute] Guid merchantId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            TransactionsByWeekModel transactionsByWeek = await this.ReportingManager.GetTransactionsForMerchantByWeek(estateId, merchantId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByWeekResponse response = this.ModelFactory.ConvertFrom(transactionsByWeek);

            return this.Ok(response);
        }

        [HttpGet]
        [Route("estates/{estateId}/transactions/bymonth")]
        public async Task<IActionResult> GetTransactionForEstateByMonth([FromRoute] Guid estateId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            TransactionsByMonthModel transactionsByMonth = await this.ReportingManager.GetTransactionsForEstateByMonth(estateId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByMonthResponse response = this.ModelFactory.ConvertFrom(transactionsByMonth);

            return this.Ok(response);
        }

        [HttpGet]
        [Route("estates/{estateId}/merchants/{merchantId}/transactions/bymonth")]
        public async Task<IActionResult> GetTransactionForMerchantByMonth([FromRoute] Guid estateId, [FromRoute] Guid merchantId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            TransactionsByMonthModel transactionsByMonth = await this.ReportingManager.GetTransactionsForMerchantByMonth(estateId, merchantId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByMonthResponse response = this.ModelFactory.ConvertFrom(transactionsByMonth);

            return this.Ok(response);
        }
    }
}
