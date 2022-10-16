using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Grammar.Checker.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<IWebApp> CreateWebAppAsync(
            string webAppName,
            IAppServicePlan appServicePlan,
            IResourceGroup resourceGroup)
        {
            var webAppSettings = new Dictionary<string, string>
            {
                { "ASPNETCORE_ENVIRONMENT", ProjectEnvironment },
                { "ApiConfigurations:Url", this.externalTextAnalyzerUrl },
                { "Syncfusion:LicenseKey", this.licenseKey }
            };

            return await azure.AppServices.WebApps
                .Define(name: webAppName)
                .WithExistingWindowsPlan(appServicePlan: appServicePlan)
                .WithExistingResourceGroup(group: resourceGroup)
                .WithNetFrameworkVersion(version: NetFrameworkVersion.Parse("v6.0"))
                .WithAppSettings(settings: webAppSettings)
                .CreateAsync();
        }
    }
}