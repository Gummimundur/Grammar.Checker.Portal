using Grammar.Checker.Portal.Web.Infrastructure.Provision.Models.Configurations;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Grammar.Checker.Portal.Web.Infrastructure.Provision.Brokers.Configurations
{
    public class ConfigurationBroker : IConfigurationBroker
    {
        public CloudManagementConfiguration GetConfiguration()
        {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                .SetBasePath(basePath: Directory.GetCurrentDirectory())
                .AddJsonFile(path: "Grammar.Checker.Portal.Web.Infrastructure.Provision\\appSettings.json", optional: false)
                .Build();

            return configurationRoot.Get<CloudManagementConfiguration>();
        }
    }
}