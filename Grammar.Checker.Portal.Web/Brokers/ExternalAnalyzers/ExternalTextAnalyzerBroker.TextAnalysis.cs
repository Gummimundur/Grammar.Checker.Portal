using Grammar.Checker.Portal.Web.Models.Services.Foundations.ExternalAnalyzedText;
using System.Threading.Tasks;

namespace Grammar.Checker.Portal.Web.Brokers.ExternalTextAnalyzers
{
    public partial class ExternalTextAnalyzerBroker
    {
        const string YfirlesturRelativeUrl = "correct.api";

        public async ValueTask<ExternalAnalyzedText> AnalyzeTextAsync(string serializedText)
        {
            return await PostAsync<string, ExternalAnalyzedText>(
                relativeUrl: YfirlesturRelativeUrl,
                content: serializedText,
                mediaType: "application/json");
        }
    }
}