using System;
using Microsoft.Xrm.Sdk;

namespace XrmSample.Plugins
{
    public abstract class PluginBase : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            var tracing = (ITracingService)serviceProvider.GetService(typeof(ITracingService));
            var context  = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            var factory  = (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
            var service  = factory.CreateOrganizationService(context.UserId);

            try
            {
                ExecutePlugin(context, service, tracing);
            }
            catch (InvalidPluginExecutionException)
            {
                throw;
            }
            catch (Exception ex)
            {
                tracing?.Trace($"Unhandled: {ex}");
                throw new InvalidPluginExecutionException("An unexpected error occurred in XrmSample.Plugins.", ex);
            }
        }

        protected abstract void ExecutePlugin(IPluginExecutionContext context, IOrganizationService service, ITracingService tracing);
    }
}
