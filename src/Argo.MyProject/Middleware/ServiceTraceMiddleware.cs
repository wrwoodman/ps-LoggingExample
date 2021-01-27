using System;
using System.Threading.Tasks;
using Argo.MyProject.Logging;
using Argo.MyProject.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PostSharp.Patterns.Diagnostics;
#pragma warning disable 1591  // Disable XML comment warning

namespace Argo.MyProject.Middleware
{
    [Log(AttributeExclude = true)]
    public class ServiceTraceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ServiceTraceMiddleware> _logger;
        private static readonly LogSource _logSource = LogSource.Get();

        public ServiceTraceMiddleware(RequestDelegate next, ILogger<ServiceTraceMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        // Concept for this was found at https://exceptionnotfound.net/using-middleware-to-log-requests-and-responses-in-asp-net-core/
        public async Task InvokeAsync(HttpContext context, IHttpContextAccessor httpContextAccessor)
        {
            var startTime = DateTime.UtcNow;
            string uniqueId = string.Empty;
            if (context.Request.Headers.ContainsKey(Constants.UniqueIdKey))
            {
                uniqueId = context.Request.Headers[Constants.UniqueIdKey];
            }

            var loggingPropertyData = new LoggingPropertyData { UniqueId = uniqueId };
            OpenActivityOptions options = new OpenActivityOptions(loggingPropertyData);
            using (var activity = _logSource.Default.OpenActivity(FormattedMessageBuilder.Formatted("Start request"), options))
            {
                // This will write the unique id to log messages that we don't control
                var request = $"{context.Request.Method} {context.Request.Scheme} {context.Request.Host}{context.Request.Path} {context.Request.QueryString}";
                _logger.LogInformation(request);

                //Continue down the Middleware pipeline, eventually returning to this class
                await _next(context);

                decimal elapsedTime = (decimal)(DateTimeOffset.Now - startTime).TotalSeconds;
                var responseStr = $"{context.Request.Method} {context.Request.Scheme} {context.Request.Host}{context.Request.Path} {context.Response.StatusCode} Elapsed Time: {elapsedTime}";
                _logger.LogInformation(responseStr);

                activity.SetOutcome(PostSharp.Patterns.Diagnostics.LogLevel.Info, FormattedMessageBuilder.Formatted("Request Completed."));
            }
        }

    }
}
