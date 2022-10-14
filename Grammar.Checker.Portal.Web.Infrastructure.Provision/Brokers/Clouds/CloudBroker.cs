using System;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.ResourceManager.Fluent;

namespace Grammar.Checker.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker : ICloudBroker
    {
        private const string ProjectEnvironment = "Production";
        private readonly string yfirlesturApiUrl;
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string tenantId;
        private readonly string adminName;
        private readonly string adminAccess;
        private readonly IAzure azure;

        public CloudBroker()
        {
            this.clientId = Environment.GetEnvironmentVariable("AzureClientId");
            this.clientSecret = Environment.GetEnvironmentVariable("AzureClientSecret");
            this.tenantId = Environment.GetEnvironmentVariable("AzureTenantId");
            this.adminName = Environment.GetEnvironmentVariable("AzureAdminName");
            this.adminAccess = Environment.GetEnvironmentVariable("AzureAdminAccess");
            this.yfirlesturApiUrl = Environment.GetEnvironmentVariable("YfirlesturApiUrl");
            this.azure = AuthenticateAzure();
        }

        private IAzure AuthenticateAzure()
        {
            AzureCredentials credentials = 
                SdkContext.AzureCredentialsFactory.FromServicePrincipal(
                    clientId: this.clientId,
                    clientSecret: this.clientSecret,
                    tenantId: this.tenantId,
                    environment: AzureEnvironment.AzureGlobalCloud);

            return Azure.Configure()
                .WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic)
                .Authenticate(azureCredentials: credentials)
                .WithDefaultSubscription();
        }
    }
}