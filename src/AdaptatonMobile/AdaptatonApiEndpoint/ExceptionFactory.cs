using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AdaptatonApiEndpoint.Models;

namespace AdaptatonApiEndpoint
{
    public class ExceptionFactory
    {
        public static async Task<ApiException> ThrowApiException(string methodName, HttpResponseMessage response)
        {
            var apiException = new ApiException(response.StatusCode, $"Error in {methodName}: {response.ReasonPhrase}");
            var errorString = await response.Content.ReadAsStringAsync();
            apiException.ErrorContent = Newtonsoft.Json.JsonConvert.DeserializeObject<ErrorContent>(errorString);
            return apiException;
        }
    }

    public class ApiException : Exception
    {
        /// <summary>
        /// Gets or sets the error code (HTTP status code)
        /// </summary>
        /// <value>The error code (HTTP status code).</value>
        public HttpStatusCode ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the error content (body json object)
        /// </summary>
        /// <value>The error content (Http response body).</value>
        public ErrorContent ErrorContent { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        public ApiException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="errorCode">HTTP status code.</param>
        /// <param name="message">Error message.</param>
        public ApiException(HttpStatusCode errorCode, string message) : base(message)
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiException"/> class.
        /// </summary>
        /// <param name="errorCode">HTTP status code.</param>
        /// <param name="message">Error message.</param>
        /// <param name="errorContent">Error content.</param>
        public ApiException(HttpStatusCode errorCode, string message, dynamic errorContent = null) : base(message)
        {
            this.ErrorCode = errorCode;
            this.ErrorContent = errorContent;
        }
    }
}
