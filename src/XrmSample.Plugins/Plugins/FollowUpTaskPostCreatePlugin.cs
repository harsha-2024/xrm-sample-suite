using System;
using Microsoft.Xrm.Sdk;

namespace XrmSample.Plugins.Plugins
{
    /// <summary>
    /// Post-Operation on account Create: creates a follow-up task.
    /// </summary>
    public class FollowUpTaskPostCreatePlugin : XrmSample.Plugins.PluginBase
    {
        protected override void ExecutePlugin(IPluginExecutionContext context, IOrganizationService service, ITracingService tracing)
        {
            if (context.MessageName != "Create") return;
            if (!(context.OutputParameters.Contains("id") && context.OutputParameters["id"] is Guid accountId)) return;

            var task = new Entity("task");
            task["subject"] = "Follow up: New account created";
            task["description"] = "Please reach out to the new account within 7 days.";
            task["scheduledend"] = DateTime.UtcNow.AddDays(7);
            task["regardingobjectid"] = new EntityReference("account", accountId);

            service.Create(task);
        }
    }
}
