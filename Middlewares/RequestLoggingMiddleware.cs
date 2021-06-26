using System;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace TodoApiDTO.Middlewares
{
    /// <summary>
    /// Logs the start/finish of request processing.
    /// </summary>
    public class RequestLoggingMiddleware
    {
        public const string RequestStopwatchKey = "RequestStopwatch";

        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            LogRequestStart(context);
            try
            {
                await _next(context);
            }
            finally
            {
                LogRequestEnd(context);
            }
        }

        private void LogRequestStart(HttpContext context)
        {
            context.Items[RequestStopwatchKey] = Stopwatch.StartNew();

            string method = context.Request.Method;
            if (method == HttpMethod.Options.Method)
            {
                return;
            }

            string uri = context.Request.GetDisplayUrl();
            _logger.LogInformation( $"{method} {uri} Request started.");
        }

        private void LogRequestEnd(HttpContext context)
        {
            string method = context.Request.Method;
            if (method == HttpMethod.Options.Method)
            {
                return;
            }

            var watch = (Stopwatch)context.Items[RequestStopwatchKey];
            string milliseconds =
                watch.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture);

            string uri = context.Request.GetDisplayUrl();
            string reason = ReasonPhrases.GetReasonPhrase(context.Response.StatusCode);
            _logger.LogInformation(
            $"{method} {uri} Request finished. {context.Response.StatusCode} {reason} | "
                + $"Duration: {milliseconds} ms.");
        }
    }
}