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

    [Route(ReportingController.ControllerRoute)]
    [ApiController]
    [ExcludeFromCodeCoverage]
    public class ReportingController : ControllerBase
    {
        private readonly IReportingManager ReportingManager;

        private readonly IModelFactory ModelFactory;

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

        [HttpGet]
        [Route("estate/{estateId}/transactions/bydate")]
        public async Task<IActionResult> GetTransactionForEstateByDate([FromRoute] Guid estateId, [FromQuery(Name = "start_date")] String startDate, [FromQuery(Name = "end_date")] String endDate, CancellationToken cancellationToken)
        {
            // Get the data grouped as requested
            TransactionsByDayModel transactionsByDay = await this.ReportingManager.GetTransactionsForEstate(estateId, startDate, endDate, GroupingType.ByDate, cancellationToken);

            // Convert to a dto
            TransactionsByDayResponse response = this.ModelFactory.ConvertFrom(transactionsByDay);

            return this.Ok(response);
        }
    }
}
