using Grammar.Checker.Portal.Web.Infrastructure.Provision.Models.Configurations;

namespace Grammar.Checker.Portal.Web.Infrastructure.Provision.Brokers.Configurations
{
    public interface IConfigurationBroker
    {
        public CloudManagementConfiguration GetConfiguration();
    }
}