using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using System.Threading.Tasks;

namespace Grammar.Checker.Portal.Web.Infrastructure.Provision.Brokers.Clouds
{
    public partial class CloudBroker
    {
        public async ValueTask<IResourceGroup> CreateResourceGroupAsync(string resourceGroupName)
        {
            return await this.azure.ResourceGroups
                .Define(name: resourceGroupName)
                .WithRegion(region: Region.EuropeNorth)
                .CreateAsync();
        }

        public async ValueTask DeleteResourceGroupAsync(string resourceGroupName) =>
            await azure.ResourceGroups.DeleteByNameAsync(resourceGroupName);

        public async ValueTask<bool> CheckResourceGroupExistsAsync(string resourceGroupName) =>
            await this.azure.ResourceGroups.ContainAsync(name: resourceGroupName);
    }
}