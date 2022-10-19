using Grammar.Checker.Portal.Web.Brokers.ExternalTextAnalyzers;
using Grammar.Checker.Portal.Web.Brokers.Loggings;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.ExternalAnalyzedText;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Grammar.Checker.Portal.Web.Services.Foundations.AnalyzedTexts
{
    public partial class AnalyzeTextService : IAnalyzeTextService
    {
        private readonly IExternalTextAnalyzerBroker externalTextAnalyzerBroker;
        private readonly ILoggingBroker loggingBroker;

        public AnalyzeTextService(
            IExternalTextAnalyzerBroker externalTextAnalyzerBroker,
            ILoggingBroker loggingBroker)
        {
            this.externalTextAnalyzerBroker = externalTextAnalyzerBroker;
            this.loggingBroker = loggingBroker;
        }

        public ValueTask<AnalyzedText> AnalyzeTextAsync(string text) =>
        TryCatch(async () =>
        {
            ValidateText(text);

            ExternalAnalyzedText externalAnalyzedText =
                await RunExternalTextAnalyzerAsync(text);

            return AsAnalyzedText(externalAnalyzedText);
        });

        private async Task<ExternalAnalyzedText> RunExternalTextAnalyzerAsync(string text)
        {
            string serializedText = SerializeText(text);

            ExternalAnalyzedText externalAnalyzedText =
                await this.externalTextAnalyzerBroker.AnalyzeTextAsync(serializedText);

            ValidateExternalAnalyzedText(externalAnalyzedText);

            return externalAnalyzedText;
        }

        private static string SerializeText(string textToSerialize)
        {
            object objectToSerialize = new { text = textToSerialize };

            return JsonConvert.SerializeObject(objectToSerialize);
        }
    }
}