using Microsoft.Xrm.Sdk;

namespace XrmSample.Common.Extensions
{
    public static class TracingExtensions
    {
        public static void Info(this ITracingService tracing, string message)
        {
            tracing?.Trace($"[INFO] {message}");
        }

        public static void Error(this ITracingService tracing, string message)
        {
            tracing?.Trace($"[ERROR] {message}");
        }
    }
}
