using Grammar.Checker.Portal.Web.Models.Configurations;
using Microsoft.Extensions.Configuration;
using RESTFulSense.Clients;
using System.Net.Http;
using System.Threading.Tasks;

namespace Grammar.Checker.Portal.Web.Brokers.ExternalTextAnalyzers
{
    public partial class ExternalTextAnalyzerBroker : IExternalTextAnalyzerBroker
    {
        private readonly HttpClient httpClient;
        private readonly IRESTFulApiFactoryClient apiClient;

        public ExternalTextAnalyzerBroker(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.apiClient = GetApiClient(configuration);
        }

        private async ValueTask<U> PostAsync<T, U>(string relativeUrl, T content, string mediaType) =>
            await this.apiClient.PostContentAsync<T, U>(relativeUrl, content, mediaType);

        private IRESTFulApiFactoryClient GetApiClient(IConfiguration configuration)
        {
            LocalConfigurations localConfigurations = configuration.Get<LocalConfigurations>();
            string apiBaseUrl = localConfigurations.ApiConfigurations.Url;
            this.httpClient.BaseAddress = new System.Uri(apiBaseUrl);

            return new RESTFulApiFactoryClient(this.httpClient);
        }
    }
}