namespace EstateReporting.Controllers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Claims;
    using System.Threading;
    using System.Threading.Tasks;
    using BusinessLogic;
    using Common;
    using Common.Examples;
    using DataTransferObjects;
    using Factories;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Swashbuckle.AspNetCore.Annotations;
    using Swashbuckle.AspNetCore.Filters;

    [Route(MerchantReportingController.ControllerRoute)]
    [Authorize]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class MerchantReportingController : ControllerBase
    {
        #region Fields

        /// <summary>
        /// The model factory
        /// </summary>
        private readonly IModelFactory ModelFactory;

        /// <summary>
        /// The reporting manager
        /// </summary>
        private readonly IReportingManager ReportingManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EstateReportingController" /> class.
        /// </summary>
        /// <param name="reportingManager">The reporting manager.</param>
        /// <param name="modelFactory">The model factory.</param>
        public MerchantReportingController(IReportingManager reportingManager,
                                           IModelFactory modelFactory)
        {
            this.ReportingManager = reportingManager;
            this.ModelFactory = modelFactory;
        }

        #endregion

        #region Methods

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
        [SwaggerResponse(200, "OK", typeof(TransactionsByDayResponse))]
        [SwaggerResponseExample(200, typeof(TransactionsByDayResponseExample))]
        public async Task<IActionResult> GetTransactionForMerchantByDate([FromRoute] Guid estateId,
                                                                         [FromRoute] Guid merchantId,
                                                                         [FromQuery(Name = "start_date")] String startDate,
                                                                         [FromQuery(Name = "end_date")] String endDate,
                                                                         CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            TransactionsByDayModel transactionsByDay =
                await this.ReportingManager.GetTransactionsForMerchantByDate(estateId, merchantId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByDayResponse response = this.ModelFactory.ConvertFrom(transactionsByDay);

            return this.Ok(response);
        }

        [HttpGet]
        [Route("estates/{estateId}/merchants/{merchantId}/settlements/bydate")]
        [SwaggerResponse(200, "OK", typeof(SettlementByDayResponse))]
        [SwaggerResponseExample(200, typeof(SettlementByDayResponseExample))]
        public async Task<IActionResult> GetSettlementForMerchantByDate([FromRoute] Guid estateId,
                                                                         [FromRoute] Guid merchantId,
                                                                         [FromQuery(Name = "start_date")] String startDate,
                                                                         [FromQuery(Name = "end_date")] String endDate,
                                                                         CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            SettlementByDayModel settlementByDayModel =
                await this.ReportingManager.GetSettlementForMerchantByDate(estateId, merchantId, startDate, endDate, cancellationToken);

            // Convert to a dto
            SettlementByDayResponse response = this.ModelFactory.ConvertFrom(settlementByDayModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Gets the transaction for merchant by month.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("estates/{estateId}/merchants/{merchantId}/transactions/bymonth")]
        [SwaggerResponse(200, "OK", typeof(TransactionsByMonthResponse))]
        [SwaggerResponseExample(200, typeof(TransactionsByMonthResponseExample))]
        public async Task<IActionResult> GetTransactionForMerchantByMonth([FromRoute] Guid estateId,
                                                                          [FromRoute] Guid merchantId,
                                                                          [FromQuery(Name = "start_date")] String startDate,
                                                                          [FromQuery(Name = "end_date")] String endDate,
                                                                          CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            // Get the data grouped as requested
            TransactionsByMonthModel transactionsByMonth =
                await this.ReportingManager.GetTransactionsForMerchantByMonth(estateId, merchantId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByMonthResponse response = this.ModelFactory.ConvertFrom(transactionsByMonth);

            return this.Ok(response);
        }

        [HttpGet]
        [Route("estates/{estateId}/merchants/{merchantId}/settlements/bymonth")]
        [SwaggerResponse(200, "OK", typeof(SettlementByMonthResponse))]
        [SwaggerResponseExample(200, typeof(SettlementByMonthResponseExample))]
        public async Task<IActionResult> GetSettlementForMerchantByMonth([FromRoute] Guid estateId,
                                                                          [FromRoute] Guid merchantId,
                                                                          [FromQuery(Name = "start_date")] String startDate,
                                                                          [FromQuery(Name = "end_date")] String endDate,
                                                                          CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            // Get the data grouped as requested
            SettlementByMonthModel settlementByMonth =
                await this.ReportingManager.GetSettlementForMerchantByMonth(estateId, merchantId, startDate, endDate, cancellationToken);

            // Convert to a dto
            SettlementByMonthResponse response = this.ModelFactory.ConvertFrom(settlementByMonth);

            return this.Ok(response);
        }

        /// <summary>
        /// Gets the transaction for merchant by week.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("estates/{estateId}/merchants/{merchantId}/transactions/byweek")]
        [SwaggerResponse(200, "OK", typeof(TransactionsByWeekResponse))]
        [SwaggerResponseExample(200, typeof(TransactionsByWeekResponseExample))]
        public async Task<IActionResult> GetTransactionForMerchantByWeek([FromRoute] Guid estateId,
                                                                         [FromRoute] Guid merchantId,
                                                                         [FromQuery(Name = "start_date")] String startDate,
                                                                         [FromQuery(Name = "end_date")] String endDate,
                                                                         CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            // Get the data grouped as requested
            TransactionsByWeekModel transactionsByWeek =
                await this.ReportingManager.GetTransactionsForMerchantByWeek(estateId, merchantId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByWeekResponse response = this.ModelFactory.ConvertFrom(transactionsByWeek);

            return this.Ok(response);
        }

        [HttpGet]
        [Route("estates/{estateId}/merchants/{merchantId}/settlements/byweek")]
        [SwaggerResponse(200, "OK", typeof(TransactionsByWeekResponse))]
        [SwaggerResponseExample(200, typeof(TransactionsByWeekResponseExample))]
        public async Task<IActionResult> GetSettlementForMerchantByWeek([FromRoute] Guid estateId,
                                                                         [FromRoute] Guid merchantId,
                                                                         [FromQuery(Name = "start_date")] String startDate,
                                                                         [FromQuery(Name = "end_date")] String endDate,
                                                                         CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            // Get the data grouped as requested
            SettlementByWeekModel settlementByWeek =
                await this.ReportingManager.GetSettlementForMerchantByWeek(estateId, merchantId, startDate, endDate, cancellationToken);

            // Convert to a dto
            SettlementByWeekResponse response = this.ModelFactory.ConvertFrom(settlementByWeek);

            return this.Ok(response);
        }

        /// <summary>
        /// Determines whether [is token valid] [the specified estate identifier].
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <returns>
        ///   <c>true</c> if [is token valid] [the specified estate identifier]; otherwise, <c>false</c>.
        /// </returns>
        private Boolean IsTokenValid(Guid estateId)
        {
            // Get the Estate Id claim from the user
            Claim estateIdClaim = ClaimsHelper.GetUserClaim(this.User, "EstateId", estateId.ToString());

            String estateRoleName = Environment.GetEnvironmentVariable("EstateRoleName");
            if (ClaimsHelper.IsUserRolesValid(this.User, new[] {string.IsNullOrEmpty(estateRoleName) ? "Estate" : estateRoleName}) == false)
            {
                return false;
            }

            if (ClaimsHelper.ValidateRouteParameter(estateId, estateIdClaim) == false)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region Others

        /// <summary>
        /// The controller name
        /// </summary>
        public const String ControllerName = "reporting";

        /// <summary>
        /// The controller route
        /// </summary>
        private const String ControllerRoute = "api/" + MerchantReportingController.ControllerName;

        #endregion
    }
}