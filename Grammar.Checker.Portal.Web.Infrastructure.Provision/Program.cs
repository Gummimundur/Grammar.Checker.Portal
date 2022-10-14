using System.Threading.Tasks;
using Grammar.Checker.Portal.Web.Infrastructure.Provision.Services.Processings;

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