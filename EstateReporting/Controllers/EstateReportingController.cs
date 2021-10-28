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
    using SortDirection = DataTransferObjects.SortDirection;
    using SortField = DataTransferObjects.SortField;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route(EstateReportingController.ControllerRoute)]
    [Authorize]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class EstateReportingController : ControllerBase
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
        public EstateReportingController(IReportingManager reportingManager,
                                         IModelFactory modelFactory)
        {
            this.ReportingManager = reportingManager;
            this.ModelFactory = modelFactory;
        }

        #endregion

        #region Methods

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
        [SwaggerResponse(200, "OK", typeof(TransactionsByDayResponse))]
        [SwaggerResponseExample(200, typeof(TransactionsByDayResponseExample))]
        public async Task<IActionResult> GetTransactionForEstateByDate([FromRoute] Guid estateId,
                                                                       [FromQuery(Name = "start_date")] String startDate,
                                                                       [FromQuery(Name = "end_date")] String endDate,
                                                                       CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            // Get the data grouped as requested

            TransactionsByDayModel transactionsByDay = await this.ReportingManager.GetTransactionsForEstateByDate(estateId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByDayResponse response = this.ModelFactory.ConvertFrom(transactionsByDay);

            return this.Ok(response);
        }

        /// <summary>
        /// Gets the transaction for estate by merchant.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("estates/{estateId}/transactions/bymerchant")]
        [SwaggerResponse(200, "OK", typeof(TransactionsByMerchantResponse))]
        [SwaggerResponseExample(200, typeof(TransactionsByMerchantResponseExample))]
        public async Task<IActionResult> GetTransactionForEstateByMerchant([FromRoute] Guid estateId,
                                                                           [FromQuery(Name = "start_date")] String startDate,
                                                                           [FromQuery(Name = "end_date")] String endDate,
                                                                           [FromQuery(Name = "record_count")] Int32 recordCount,
                                                                           [FromQuery(Name = "sort_direction")] SortDirection sortDirection,
                                                                           [FromQuery(Name = "sort_field")] SortField sortField,
                                                                           CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            BusinessLogic.SortDirection sortDir = this.ModelFactory.ConvertFrom(sortDirection);
            BusinessLogic.SortField sortBy = this.ModelFactory.ConvertFrom(sortField);

            // Get the data grouped as requested
            TransactionsByMerchantModel transactionsByMerchant =
                await this.ReportingManager.GetTransactionsForEstateByMerchant(estateId, startDate, endDate, recordCount, sortBy, sortDir, cancellationToken);

            // Convert to a dto
            TransactionsByMerchantResponse response = this.ModelFactory.ConvertFrom(transactionsByMerchant);

            return this.Ok(response);
        }

        /// <summary>
        /// Gets the transaction for estate by month.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("estates/{estateId}/transactions/bymonth")]
        [SwaggerResponse(200, "OK", typeof(TransactionsByMonthResponse))]
        [SwaggerResponseExample(200, typeof(TransactionsByMonthResponseExample))]
        public async Task<IActionResult> GetTransactionForEstateByMonth([FromRoute] Guid estateId,
                                                                        [FromQuery(Name = "start_date")] String startDate,
                                                                        [FromQuery(Name = "end_date")] String endDate,
                                                                        CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            // Get the data grouped as requested
            TransactionsByMonthModel transactionsByMonth = await this.ReportingManager.GetTransactionsForEstateByMonth(estateId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByMonthResponse response = this.ModelFactory.ConvertFrom(transactionsByMonth);

            return this.Ok(response);
        }

        /// <summary>
        /// Gets the transaction for estate by operator.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("estates/{estateId}/transactions/byoperator")]
        [SwaggerResponse(200, "OK", typeof(TransactionsByOperatorResponse))]
        [SwaggerResponseExample(200, typeof(TransactionsByOperatorResponseExample))]
        public async Task<IActionResult> GetTransactionForEstateByOperator([FromRoute] Guid estateId,
                                                                           [FromQuery(Name = "start_date")] String startDate,
                                                                           [FromQuery(Name = "end_date")] String endDate,
                                                                           [FromQuery(Name = "record_count")] Int32 recordCount,
                                                                           [FromQuery(Name = "sort_direction")] SortDirection sortDirection,
                                                                           [FromQuery(Name = "sort_field")] SortField sortField,
                                                                           CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            BusinessLogic.SortDirection sortDir = this.ModelFactory.ConvertFrom(sortDirection);
            BusinessLogic.SortField sortBy = this.ModelFactory.ConvertFrom(sortField);

            // Get the data grouped as requested
            TransactionsByOperatorModel transactionsByOperator =
                await this.ReportingManager.GetTransactionsForEstateByOperator(estateId, startDate, endDate, recordCount, sortBy, sortDir, cancellationToken);

            // Convert to a dto
            TransactionsByOperatorResponse response = this.ModelFactory.ConvertFrom(transactionsByOperator);

            //return this.Ok(response);
            return this.Ok(response);
        }

        /// <summary>
        /// Gets the transaction for estate by week.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("estates/{estateId}/transactions/byweek")]
        [SwaggerResponse(200, "OK", typeof(TransactionsByWeekResponse))]
        [SwaggerResponseExample(200, typeof(TransactionsByWeekResponseExample))]
        public async Task<IActionResult> GetTransactionForEstateByWeek([FromRoute] Guid estateId,
                                                                       [FromQuery(Name = "start_date")] String startDate,
                                                                       [FromQuery(Name = "end_date")] String endDate,
                                                                       CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            // Get the data grouped as requested
            TransactionsByWeekModel transactionsByWeek = await this.ReportingManager.GetTransactionsForEstateByWeek(estateId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByWeekResponse response = this.ModelFactory.ConvertFrom(transactionsByWeek);

            return this.Ok(response);
        }

        [HttpGet]
        [Route("estates/{estateId}/settlements/bydate")]
        [SwaggerResponse(200, "OK", typeof(SettlementByDayResponse))]
        [SwaggerResponseExample(200, typeof(SettlementByDayResponseExample))]
        public async Task<IActionResult> GetSettlementForEstateByDate([FromRoute] Guid estateId,
                                                                       [FromQuery(Name = "start_date")] String startDate,
                                                                       [FromQuery(Name = "end_date")] String endDate,
                                                                       CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            // Get the data grouped as requested

            SettlementByDayModel settlementByDay = await this.ReportingManager.GetSettlementForEstateByDate(estateId, startDate, endDate, cancellationToken);

            // Convert to a dto
            SettlementByDayResponse response = this.ModelFactory.ConvertFrom(settlementByDay);

            return this.Ok(response);
        }

        [HttpGet]
        [Route("estates/{estateId}/settlements/bymerchant")]
        [SwaggerResponse(200, "OK", typeof(SettlementByMerchantResponse))]
        [SwaggerResponseExample(200, typeof(SettlementByMerchantResponseExample))]
        public async Task<IActionResult> GetSettlementForEstateByMerchant([FromRoute] Guid estateId,
                                                                           [FromQuery(Name = "start_date")] String startDate,
                                                                           [FromQuery(Name = "end_date")] String endDate,
                                                                           [FromQuery(Name = "record_count")] Int32 recordCount,
                                                                           [FromQuery(Name = "sort_direction")] SortDirection sortDirection,
                                                                           [FromQuery(Name = "sort_field")] SortField sortField,
                                                                           CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            BusinessLogic.SortDirection sortDir = this.ModelFactory.ConvertFrom(sortDirection);
            BusinessLogic.SortField sortBy = this.ModelFactory.ConvertFrom(sortField);

            // Get the data grouped as requested
            SettlementByMerchantModel settlementByMerchant =
                await this.ReportingManager.GetSettlementForEstateByMerchant(estateId, startDate, endDate, recordCount, sortBy, sortDir, cancellationToken);

            // Convert to a dto
            SettlementByMerchantResponse response = this.ModelFactory.ConvertFrom(settlementByMerchant);

            return this.Ok(response);
        }

        /// <summary>
        /// Gets the transaction for estate by month.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("estates/{estateId}/settlements/bymonth")]
        [SwaggerResponse(200, "OK", typeof(SettlementByMonthResponse))]
        [SwaggerResponseExample(200, typeof(SettlementByMonthResponseExample))]
        public async Task<IActionResult> GetSettlementForEstateByMonth([FromRoute] Guid estateId,
                                                                        [FromQuery(Name = "start_date")] String startDate,
                                                                        [FromQuery(Name = "end_date")] String endDate,
                                                                        CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            // Get the data grouped as requested
            SettlementByMonthModel settlementByMonth = await this.ReportingManager.GetSettlementForEstateByMonth(estateId, startDate, endDate, cancellationToken);

            // Convert to a dto
            SettlementByMonthResponse response = this.ModelFactory.ConvertFrom(settlementByMonth);

            return this.Ok(response);
        }

        /// <summary>
        /// Gets the transaction for estate by operator.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("estates/{estateId}/settlements/byoperator")]
        [SwaggerResponse(200, "OK", typeof(SettlementByOperatorResponse))]
        [SwaggerResponseExample(200, typeof(SettlementByOperatorResponseExample))]
        public async Task<IActionResult> GetSettlementForEstateByOperator([FromRoute] Guid estateId,
                                                                           [FromQuery(Name = "start_date")] String startDate,
                                                                           [FromQuery(Name = "end_date")] String endDate,
                                                                           [FromQuery(Name = "record_count")] Int32 recordCount,
                                                                           [FromQuery(Name = "sort_direction")] SortDirection sortDirection,
                                                                           [FromQuery(Name = "sort_field")] SortField sortField,
                                                                           CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            BusinessLogic.SortDirection sortDir = this.ModelFactory.ConvertFrom(sortDirection);
            BusinessLogic.SortField sortBy = this.ModelFactory.ConvertFrom(sortField);

            // Get the data grouped as requested
            SettlementByOperatorModel settlementByOperator =
                await this.ReportingManager.GetSettlementForEstateByOperator(estateId, startDate, endDate, recordCount, sortBy, sortDir, cancellationToken);

            // Convert to a dto
            SettlementByOperatorResponse response = this.ModelFactory.ConvertFrom(settlementByOperator);

            //return this.Ok(response);
            return this.Ok(response);
        }

        /// <summary>
        /// Gets the transaction for estate by week.
        /// </summary>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("estates/{estateId}/settlements/byweek")]
        [SwaggerResponse(200, "OK", typeof(SettlementByWeekResponse))]
        [SwaggerResponseExample(200, typeof(SettlementByWeekResponseExample))]
        public async Task<IActionResult> GetSettlementForEstateByWeek([FromRoute] Guid estateId,
                                                                       [FromQuery(Name = "start_date")] String startDate,
                                                                       [FromQuery(Name = "end_date")] String endDate,
                                                                       CancellationToken cancellationToken)
        {
            if (this.IsTokenValid(estateId) == false)
                return this.Forbid();

            // Get the data grouped as requested
            SettlementByWeekModel settlementByWeek = await this.ReportingManager.GetSettlementForEstateByWeek(estateId, startDate, endDate, cancellationToken);

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
        private const String ControllerRoute = "api/" + EstateReportingController.ControllerName;

        #endregion
    }
}