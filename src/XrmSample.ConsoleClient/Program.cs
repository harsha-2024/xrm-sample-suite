using System;
using System.IO;
using System.Text.Json;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

class Program
{
    static void Main()
    {
        var connection = Environment.GetEnvironmentVariable("DATAVERSE_CONNECTION");
        if (string.IsNullOrWhiteSpace(connection))
        {
            var path = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            if (File.Exists(path))
            {
                using var fs = File.OpenRead(path);
                var doc = JsonDocument.Parse(fs);
                if (doc.RootElement.TryGetProperty("ConnectionString", out var el))
                    connection = el.GetString();
            }
        }

        if (string.IsNullOrWhiteSpace(connection))
        {
            Console.WriteLine("Provide a connection string via DATAVERSE_CONNECTION or appsettings.json");
            return;
        }

        using var svc = new ServiceClient(connection);
        Console.WriteLine($"IsReady: {svc.IsReady}, Url: {svc.ConnectedOrgUriActual}");

        // Create
        var account = new Entity("account");
        account["name"] = $"Sample Co {DateTime.UtcNow:HHmmss}";
        account["emailaddress1"] = $"sample{DateTime.UtcNow:HHmmss}@contoso.com";
        var id = svc.Create(account);
        Console.WriteLine($"Created account: {id}");

        // Retrieve (FetchXML)
        var fetchXml = $@"<fetch top='5'>
  <entity name='account'>
    <attribute name='name' />
    <attribute name='accountnumber' />
    <filter>
      <condition attribute='accountid' operator='eq' value='{id}' />
    </filter>
  </entity>
</fetch>";
        var result = svc.RetrieveMultiple(new FetchExpression(fetchXml));
        Console.WriteLine($"Fetch returned: {result.Entities.Count}");

        // Update
        var update = new Entity("account", id) { ["telephone1"] = "+353-1-555-0100" };
        svc.Update(update);
        Console.WriteLine("Updated telephone1.");

        // ExecuteMultiple (use outside plugins)
        var em = new ExecuteMultipleRequest
        {
            Settings = new ExecuteMultipleSettings { ContinueOnError = true, ReturnResponses = true },
            Requests = new OrganizationRequestCollection()
        };
        for (int i = 0; i < 3; i++)
        {
            var a = new Entity("account");
            a["name"] = $"BatchCo-{i}-{DateTime.UtcNow:HHmmss}";
            em.Requests.Add(new CreateRequest { Target = a });
        }
        var emResp = (ExecuteMultipleResponse)svc.Execute(em);
        Console.WriteLine($"Batch created: {emResp.Responses.Count}");

        // Cleanup sample record
        svc.Delete("account", id);
        Console.WriteLine("Deleted the first sample account.");
    }
}
