using System;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

namespace XrmSample.Plugins.Workflows
{
    public sealed class SetAccountCategoryWorkflow : CodeActivity
    {
        [Input("Account")]
        [ReferenceTarget("account")]
        public InArgument<EntityReference> AccountRef { get; set; }

        [Input("Category (Text)")]
        public InArgument<string> CategoryText { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            var wfContext = context.GetExtension<IWorkflowContext>();
            var serviceFactory = context.GetExtension<IOrganizationServiceFactory>();
            var service = serviceFactory.CreateOrganizationService(wfContext.UserId);

            var accountRef = AccountRef.Get(context);
            var category = CategoryText.Get(context);

            if (accountRef == null) throw new InvalidWorkflowException("Account reference is required.");

            // NOTE: new_categorytext is a sample custom text field you must add to account table
            var account = new Entity("account", accountRef.Id);
            account["new_categorytext"] = category;
            service.Update(account);
        }
    }
}
