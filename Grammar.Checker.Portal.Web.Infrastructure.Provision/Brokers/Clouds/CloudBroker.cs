using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System;

namespace Grammar.Checker.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker : ICloudBroker
    {
        private const string ProjectEnvironment = "Production";
        private readonly string externalTextAnalyzerUrl;
        private readonly string clientId;
        private readonly string clientSecret;
        private readonly string tenantId;
        private readonly string adminName;
        private readonly string adminAccess;
        private readonly string licenseKey;
        private readonly IAzure azure;

        public CloudBroker()
        {
            this.clientId = Environment.GetEnvironmentVariable("AzureClientId");
            this.clientSecret = Environment.GetEnvironmentVariable("AzureClientSecret");
            this.tenantId = Environment.GetEnvironmentVariable("AzureTenantId");
            this.adminName = Environment.GetEnvironmentVariable("AzureAdminName");
            this.adminAccess = Environment.GetEnvironmentVariable("AzureAdminAccess");
            this.externalTextAnalyzerUrl = Environment.GetEnvironmentVariable("ExternalTextAnalyzerUrl");
            this.licenseKey = Environment.GetEnvironmentVariable("SyncFusionLicenseKey");
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