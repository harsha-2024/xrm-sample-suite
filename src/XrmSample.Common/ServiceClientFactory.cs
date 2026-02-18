using System;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace XrmSample.Common
{
    public static class ServiceClientFactory
    {
        /// <summary>
        /// Creates a Dataverse ServiceClient from an environment variable (DATAVERSE_CONNECTION)
        /// or a provided connection string.
        /// </summary>
        public static IOrganizationService Create(string? connectionString = null)
        {
            var conn = connectionString ?? Environment.GetEnvironmentVariable("DATAVERSE_CONNECTION");
            if (string.IsNullOrWhiteSpace(conn))
                throw new InvalidOperationException("Dataverse connection string not provided. Set DATAVERSE_CONNECTION or pass explicitly.");

            var client = new ServiceClient(conn);
            if (!client.IsReady)
                throw new InvalidOperationException("ServiceClient failed to connect. Check credentials and URL.");
            return client;
        }
    }
}
