using Grammar.Checker.Portal.Web.Infrastructure.Provision.Services.Processings;
using System.Threading.Tasks;

namespace Grammar.Checker.Portal.Web.Infrastructure.Provision
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var cloudManagementProcessingService = new CloudManagementProcessingService();
            await cloudManagementProcessingService.ProcessAsync();
        }
    }
}