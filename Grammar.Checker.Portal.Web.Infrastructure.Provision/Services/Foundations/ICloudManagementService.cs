using Microsoft.Azure.Management.AppService.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using System.Threading.Tasks;

namespace Grammar.Checker.Portal.Web.Infrastructure.Provision.Services.Foundations
{
    public interface ICloudManagementService
    {
        ValueTask<IResourceGroup> ProvisionResourceGroupAsync(string projectName, string environment);

        ValueTask<IAppServicePlan> ProvisionAppServicePlanAsync(
            string projectName,
            string environment,
            IResourceGroup resourceGroup);

        ValueTask<IWebApp> ProvisionWebAppAsync(
            string projectName,
            string environment,
            IAppServicePlan servicePlan,
            IResourceGroup resourceGroup);

        ValueTask DeprovisionResourceGroupAsync(string projectName, string environment);
    }
}