using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System.Threading.Tasks;

namespace Grammar.Checker.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<IAppServicePlan> CreatePlanAsync(string planName, IResourceGroup resourceGroup)
        {
            return await azure.AppServices.AppServicePlans
                .Define(name: planName)
                .WithRegion(region: Region.EuropeNorth)
                .WithExistingResourceGroup(group: resourceGroup)
                .WithPricingTier(pricingTier: PricingTier.StandardS1)
                .WithOperatingSystem(operatingSystem: OperatingSystem.Windows)
                .CreateAsync();
        }
    }
}