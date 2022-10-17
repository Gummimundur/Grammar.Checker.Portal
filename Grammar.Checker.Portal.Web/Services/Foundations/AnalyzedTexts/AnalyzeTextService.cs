using Grammar.Checker.Portal.Web.Brokers.ExternalTextAnalyzers;
using Grammar.Checker.Portal.Web.Brokers.Loggings;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using System.Threading.Tasks;

namespace Grammar.Checker.Portal.Web.Services.Foundations.AnalyzedTexts
{
    public class AnalyzeTextService : IAnalyzeTextService
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

        public ValueTask<AnalyzedText> AnalyzeTextAsync(string text)
        {
            throw new System.NotImplementedException();
        }
    }
}