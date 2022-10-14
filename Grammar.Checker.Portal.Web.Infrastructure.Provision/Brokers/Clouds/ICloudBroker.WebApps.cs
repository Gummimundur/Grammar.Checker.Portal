using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using System.Threading.Tasks;

namespace Grammar.Checker.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial interface ICloudBroker
    {
        ValueTask<IWebApp> CreateWebAppAsync(
            string webAppName,
            IAppServicePlan servicePlan,
            IResourceGroup resourceGroup);
    }
}