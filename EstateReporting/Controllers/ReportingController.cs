using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EstateReporting.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading;
    using BusinessLogic;
    using Common.Examples;
    using DataTransferObjects;
    using Factories;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using Repository;
    using Swashbuckle.AspNetCore.Annotations;
    using Swashbuckle.AspNetCore.Filters;
    using SortDirection = DataTransferObjects.SortDirection;
    using SortField = DataTransferObjects.SortField;

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
        /// Initializes a new instance of the <see cref="ReportingController" /> class.
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
        [SwaggerResponse(200, "OK", typeof(TransactionsByDayResponse))]
        [SwaggerResponseExample(200, typeof(TransactionsByDayResponseExample))]
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
        [SwaggerResponse(200, "OK", typeof(TransactionsByDayResponse))]
        [SwaggerResponseExample(200, typeof(TransactionsByDayResponseExample))]
        public async Task<IActionResult> GetTransactionForMerchantByDate([FromRoute] Guid estateId, [FromRoute] Guid merchantId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            TransactionsByDayModel transactionsByDay = await this.ReportingManager.GetTransactionsForMerchantByDate(estateId, merchantId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByDayResponse response = this.ModelFactory.ConvertFrom(transactionsByDay);

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
        public async Task<IActionResult> GetTransactionForEstateByWeek([FromRoute] Guid estateId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            TransactionsByWeekModel transactionsByWeek = await this.ReportingManager.GetTransactionsForEstateByWeek(estateId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByWeekResponse response = this.ModelFactory.ConvertFrom(transactionsByWeek);

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
        public async Task<IActionResult> GetTransactionForMerchantByWeek([FromRoute] Guid estateId, [FromRoute] Guid merchantId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            TransactionsByWeekModel transactionsByWeek = await this.ReportingManager.GetTransactionsForMerchantByWeek(estateId, merchantId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByWeekResponse response = this.ModelFactory.ConvertFrom(transactionsByWeek);

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
        public async Task<IActionResult> GetTransactionForEstateByMonth([FromRoute] Guid estateId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            TransactionsByMonthModel transactionsByMonth = await this.ReportingManager.GetTransactionsForEstateByMonth(estateId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByMonthResponse response = this.ModelFactory.ConvertFrom(transactionsByMonth);

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
        public async Task<IActionResult> GetTransactionForMerchantByMonth([FromRoute] Guid estateId, [FromRoute] Guid merchantId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            TransactionsByMonthModel transactionsByMonth = await this.ReportingManager.GetTransactionsForMerchantByMonth(estateId, merchantId, startDate, endDate, cancellationToken);

            // Convert to a dto
            TransactionsByMonthResponse response = this.ModelFactory.ConvertFrom(transactionsByMonth);

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
        public async Task<IActionResult> GetTransactionForEstateByMerchant([FromRoute] Guid estateId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, [FromQuery(Name = "record_count")] Int32 recordCount, [FromQuery(Name = "sort_direction")] SortDirection sortDirection, [FromQuery(Name = "sort_field")] SortField sortField, CancellationToken cancellationToken)
        {
            BusinessLogic.SortDirection sortDir = this.ModelFactory.ConvertFrom(sortDirection);
            BusinessLogic.SortField sortBy = this.ModelFactory.ConvertFrom(sortField);

            // Get the data grouped as requested
            TransactionsByMerchantModel transactionsByMerchant = await this.ReportingManager.GetTransactionsForEstateByMerchant(estateId, startDate, endDate, recordCount, sortBy, sortDir, cancellationToken);

            // Convert to a dto
            TransactionsByMerchantResponse response = this.ModelFactory.ConvertFrom(transactionsByMerchant);

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
        public async Task<IActionResult> GetTransactionForEstateByOperator([FromRoute] Guid estateId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, [FromQuery(Name = "record_count")] Int32 recordCount, [FromQuery(Name = "sort_direction")] SortDirection sortDirection, [FromQuery(Name = "sort_field")] SortField sortField, CancellationToken cancellationToken)
        {
            BusinessLogic.SortDirection sortDir = this.ModelFactory.ConvertFrom(sortDirection);
            BusinessLogic.SortField sortBy = this.ModelFactory.ConvertFrom(sortField);

            // Get the data grouped as requested
            TransactionsByOperatorModel transactionsByOperator = await this.ReportingManager.GetTransactionsForEstateByOperator(estateId, startDate, endDate, recordCount, sortBy, sortDir, cancellationToken);

            // Convert to a dto
            TransactionsByOperatorResponse response = this.ModelFactory.ConvertFrom(transactionsByOperator);

            //return this.Ok(response);
            return this.Ok(response);
        }
    }
}
