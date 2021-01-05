namespace EstateReporting.Client
{
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using ClientProxyBase;
    using DataTransferObjects;
    using Newtonsoft.Json;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ClientProxyBase.ClientProxyBase" />
    /// <seealso cref="EstateReporting.Client.IEstateReportingClient" />
    public class EstateReportingClient : ClientProxyBase, IEstateReportingClient
    {
        #region Fields

        /// <summary>
        /// The base address
        /// </summary>
        private readonly String BaseAddress;

        /// <summary>
        /// The base address resolver
        /// </summary>
        private readonly Func<String, String> BaseAddressResolver;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EstateReportingClient" /> class.
        /// </summary>
        /// <param name="baseAddressResolver">The base address resolver.</param>
        /// <param name="httpClient">The HTTP client.</param>
        public EstateReportingClient(Func<String, String> baseAddressResolver,
                                     HttpClient httpClient) : base(httpClient)
        {
            this.BaseAddressResolver = baseAddressResolver;

            // Add the API version header
            this.HttpClient.DefaultRequestHeaders.Add("api-version", "1.0");
        }

        #endregion

        /// <summary>
        /// Builds the request URL.
        /// </summary>
        /// <param name="route">The route.</param>
        /// <returns></returns>
        private String BuildRequestUrl(String route)
        {
            String baseAddress = this.BaseAddressResolver("EstateReportingApi");

            String requestUri = $"{baseAddress}{route}";

            return requestUri;
        }

        /// <summary>
        /// Gets the transactions by date.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByDayResponse> GetTransactionsForEstateByDate(String accessToken,
                                                                           Guid estateId,
                                                                           String startDate,
                                                                           String endDate,
                                                                           CancellationToken cancellationToken)
        {
            TransactionsByDayResponse response = null;

            String requestUri = this.BuildRequestUrl($"/api/reporting/estates/{estateId}/transactions/bydate?start_date={startDate}&end_date={endDate}");

            try
            {
                StringContent httpContent = new StringContent(String.Empty, Encoding.UTF8, "application/json");

                // Add the access token to the client headers
                this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Make the Http Call here
                HttpResponseMessage httpResponse = await this.HttpClient.GetAsync(requestUri, cancellationToken);

                // Process the response
                String content = await this.HandleResponse(httpResponse, cancellationToken);

                // call was successful so now deserialise the body to the response object
                response = JsonConvert.DeserializeObject<TransactionsByDayResponse>(content);
            }
            catch (Exception ex)
            {
                // An exception has occurred, add some additional information to the message
                Exception exception = new Exception($"Error getting transactions by date for estate [{estateId}]");

                throw exception;
            }

            return response;
        }

        /// <summary>
        /// Gets the transactions for estate by merchant.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="recordCount">The record count.</param>
        /// <param name="sortDirection">The sort direction.</param>
        /// <param name="sortField">The sort field.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByMerchantResponse> GetTransactionsForEstateByMerchant(String accessToken,
                                                                                             Guid estateId,
                                                                                             String startDate,
                                                                                             String endDate,
                                                                                             Int32 recordCount,
                                                                                             SortDirection sortDirection,
                                                                                             SortField sortField,
                                                                                             CancellationToken cancellationToken)
        {
            if (recordCount == 0)
            {
                recordCount = 5;
            }

            TransactionsByMerchantResponse response = null;

            String requestUri = this.BuildRequestUrl($"/api/reporting/estates/{estateId}/transactions/bymerchant?start_date={startDate}&end_date={endDate}&record_count={recordCount}&sort_direction={sortDirection}&sort_field={sortField}");

            try
            {
                StringContent httpContent = new StringContent(String.Empty, Encoding.UTF8, "application/json");

                // Add the access token to the client headers
                this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Make the Http Call here
                HttpResponseMessage httpResponse = await this.HttpClient.GetAsync(requestUri, cancellationToken);

                // Process the response
                String content = await this.HandleResponse(httpResponse, cancellationToken);

                // call was successful so now deserialise the body to the response object
                response = JsonConvert.DeserializeObject<TransactionsByMerchantResponse>(content);
            }
            catch (Exception ex)
            {
                // An exception has occurred, add some additional information to the message
                Exception exception = new Exception($"Error getting transactions by merchant for estate [{estateId}]");

                throw exception;
            }

            return response;
        }

        /// <summary>
        /// Gets the transactions for estate by week.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByWeekResponse> GetTransactionsForEstateByWeek(String accessToken,
                                                                                     Guid estateId,
                                                                                     String startDate,
                                                                                     String endDate,
                                                                                     CancellationToken cancellationToken)
        {
            TransactionsByWeekResponse response = null;

            String requestUri = this.BuildRequestUrl($"/api/reporting/estates/{estateId}/transactions/byweek?start_date={startDate}&end_date={endDate}");

            try
            {
                StringContent httpContent = new StringContent(String.Empty, Encoding.UTF8, "application/json");

                // Add the access token to the client headers
                this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Make the Http Call here
                HttpResponseMessage httpResponse = await this.HttpClient.GetAsync(requestUri, cancellationToken);

                // Process the response
                String content = await this.HandleResponse(httpResponse, cancellationToken);

                // call was successful so now deserialise the body to the response object
                response = JsonConvert.DeserializeObject<TransactionsByWeekResponse>(content);
            }
            catch (Exception ex)
            {
                // An exception has occurred, add some additional information to the message
                Exception exception = new Exception($"Error getting transactions by week for estate [{estateId}]");

                throw exception;
            }

            return response;
        }

        /// <summary>
        /// Gets the transactions for estate by month.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByMonthResponse> GetTransactionsForEstateByMonth(String accessToken,
                                                                                       Guid estateId,
                                                                                       String startDate,
                                                                                       String endDate,
                                                                                       CancellationToken cancellationToken)
        {
            TransactionsByMonthResponse response = null;

            String requestUri = this.BuildRequestUrl($"/api/reporting/estates/{estateId}/transactions/bymonth?start_date={startDate}&end_date={endDate}");

            try
            {
                StringContent httpContent = new StringContent(String.Empty, Encoding.UTF8, "application/json");

                // Add the access token to the client headers
                this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Make the Http Call here
                HttpResponseMessage httpResponse = await this.HttpClient.GetAsync(requestUri, cancellationToken);

                // Process the response
                String content = await this.HandleResponse(httpResponse, cancellationToken);

                // call was successful so now deserialise the body to the response object
                response = JsonConvert.DeserializeObject<TransactionsByMonthResponse>(content);
            }
            catch (Exception ex)
            {
                // An exception has occurred, add some additional information to the message
                Exception exception = new Exception($"Error getting transactions by month for estate [{estateId}]");

                throw exception;
            }

            return response;
        }

        /// <summary>
        /// Gets the transactions by date.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByDayResponse> GetTransactionsForMerchantByDate(String accessToken,
                                                                           Guid estateId,
                                                                           Guid merchantId,
                                                                           String startDate,
                                                                           String endDate,
                                                                           CancellationToken cancellationToken)
        {
            TransactionsByDayResponse response = null;

            String requestUri = this.BuildRequestUrl($"/api/reporting/estates/{estateId}/merchants/{merchantId}/transactions/bydate?start_date={startDate}&end_date={endDate}");

            try
            {
                StringContent httpContent = new StringContent(String.Empty, Encoding.UTF8, "application/json");

                // Add the access token to the client headers
                this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Make the Http Call here
                HttpResponseMessage httpResponse = await this.HttpClient.GetAsync(requestUri, cancellationToken);

                // Process the response
                String content = await this.HandleResponse(httpResponse, cancellationToken);

                // call was successful so now deserialise the body to the response object
                response = JsonConvert.DeserializeObject<TransactionsByDayResponse>(content);
            }
            catch (Exception ex)
            {
                // An exception has occurred, add some additional information to the message
                Exception exception = new Exception($"Error getting transactions by date for merchant [{merchantId}] estate [{estateId}]");

                throw exception;
            }

            return response;
        }

        /// <summary>
        /// Gets the transactions for merchant by week.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByWeekResponse> GetTransactionsForMerchantByWeek(String accessToken,
                                                                                       Guid estateId,
                                                                                       Guid merchantId,
                                                                                       String startDate,
                                                                                       String endDate,
                                                                                       CancellationToken cancellationToken)
        {
            TransactionsByWeekResponse response = null;

            String requestUri = this.BuildRequestUrl($"/api/reporting/estates/{estateId}/merchants/{merchantId}/transactions/byweek?start_date={startDate}&end_date={endDate}");

            try
            {
                StringContent httpContent = new StringContent(String.Empty, Encoding.UTF8, "application/json");

                // Add the access token to the client headers
                this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Make the Http Call here
                HttpResponseMessage httpResponse = await this.HttpClient.GetAsync(requestUri, cancellationToken);

                // Process the response
                String content = await this.HandleResponse(httpResponse, cancellationToken);

                // call was successful so now deserialise the body to the response object
                response = JsonConvert.DeserializeObject<TransactionsByWeekResponse>(content);
            }
            catch (Exception ex)
            {
                // An exception has occurred, add some additional information to the message
                Exception exception = new Exception($"Error getting transactions by week for merchant [{merchantId}] estate [{estateId}]");

                throw exception;
            }

            return response;
        }

        /// <summary>
        /// Gets the transactions for merchant by month.
        /// </summary>
        /// <param name="accessToken">The access token.</param>
        /// <param name="estateId">The estate identifier.</param>
        /// <param name="merchantId">The merchant identifier.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public async Task<TransactionsByMonthResponse> GetTransactionsForMerchantByMonth(String accessToken,
                                                                                         Guid estateId,
                                                                                         Guid merchantId,
                                                                                         String startDate,
                                                                                         String endDate,
                                                                                         CancellationToken cancellationToken)
        {
            TransactionsByMonthResponse response = null;

            String requestUri = this.BuildRequestUrl($"/api/reporting/estates/{estateId}/merchants/{merchantId}/transactions/bymonth?start_date={startDate}&end_date={endDate}");

            try
            {
                StringContent httpContent = new StringContent(String.Empty, Encoding.UTF8, "application/json");

                // Add the access token to the client headers
                this.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                // Make the Http Call here
                HttpResponseMessage httpResponse = await this.HttpClient.GetAsync(requestUri, cancellationToken);

                // Process the response
                String content = await this.HandleResponse(httpResponse, cancellationToken);

                // call was successful so now deserialise the body to the response object
                response = JsonConvert.DeserializeObject<TransactionsByMonthResponse>(content);
            }
            catch (Exception ex)
            {
                // An exception has occurred, add some additional information to the message
                Exception exception = new Exception($"Error getting transactions by month for merchant [{merchantId}] estate [{estateId}]");

                throw exception;
            }

            return response;
        }
    }
}