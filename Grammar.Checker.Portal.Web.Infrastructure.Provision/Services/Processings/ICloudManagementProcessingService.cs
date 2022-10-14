using System.Threading.Tasks;

namespace Grammar.Checker.Portal.Web.Infrastructure.Provision.Services.Processings
{
    public interface ICloudManagementProcessingService
    {
        ValueTask ProcessAsync();
    }
}