using System;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace XrmSample.Plugins.Plugins
{
    /// <summary>
    /// Pre-Operation on account Create: validates unique emailaddress1 and sets an accountnumber.
    /// </summary>
    public class AccountCreateValidationPlugin : XrmSample.Plugins.PluginBase
    {
        protected override void ExecutePlugin(IPluginExecutionContext context, IOrganizationService service, ITracingService tracing)
        {
            if (context.MessageName != "Create") return;
            if (!(context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity target)) return;
            if (target.LogicalName != "account") return;

            // Unique email check (simple example)
            if (target.Contains("emailaddress1") && target["emailaddress1"] is string email && !string.IsNullOrWhiteSpace(email))
            {
                var qe = new QueryExpression("account")
                {
                    ColumnSet = new ColumnSet(false),
                    Criteria = new FilterExpression
                    {
                        Conditions =
                        {
                            new ConditionExpression("emailaddress1", ConditionOperator.Equal, email)
                        }
                    },
                    TopCount = 1
                };
                var existing = service.RetrieveMultiple(qe);
                if (existing.Entities.Count > 0)
                {
                    throw new InvalidPluginExecutionException("An account with the same email already exists.");
                }
            }

            // Set an accountnumber if missing
            if (!target.Contains("accountnumber"))
            {
                target["accountnumber"] = $"ACCT-{DateTime.UtcNow:yyyyMMddHHmmss}";
            }
        }
    }
}
