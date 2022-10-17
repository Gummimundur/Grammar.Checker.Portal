using Grammar.Checker.Portal.Web.Brokers.ExternalTextAnalyzers;
using Grammar.Checker.Portal.Web.Brokers.Loggings;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.ExternalAnalyzedText;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public async ValueTask<AnalyzedText> AnalyzeTextAsync(string text)
        {
            ExternalAnalyzedText externalAnalyzedText =
                await RunExternalTextAnalyzerAsync(text);

            return AsAnalyzedText(externalAnalyzedText);
        }

        private async Task<ExternalAnalyzedText> RunExternalTextAnalyzerAsync(string text)
        {
            string serializedText = SerializeText(text);

            ExternalAnalyzedText externalAnalyxedText =
                await this.externalTextAnalyzerBroker.AnalyzeTextAsync(serializedText);

            return externalAnalyxedText;
        }

        private static AnalyzedText AsAnalyzedText(ExternalAnalyzedText externalAnalyzedText)
        {
            return new AnalyzedText
            {
                Suggestions = AsSuggestions(externalAnalyzedText),
                Text = externalAnalyzedText.Text
            };
        }

        private static List<Suggestion> AsSuggestions(ExternalAnalyzedText externalAnalyzedText)
        {
            IEnumerable<Result> result = 
                externalAnalyzedText.Result.SelectMany(result => result);

            return externalAnalyzedText.Result.SelectMany(result => result).Select(result =>
            {
                return new Suggestion
                {
                    Explanations = AsExplanation(result),
                    CorrectedText = result.Corrected,
                    OriginalText = result.Original,
                    Tokens = AsTokens(result)
                };
            }).ToList();
        }

        private static List<Token> AsTokens(Result result)
        {
            return result.Symbols.Select(symbol =>
            {
                return new Token
                {
                    WordIndex = symbol.WordIndex,
                    TokenType = AsTokenType(symbol),
                    OriginalText = symbol.OriginalText,
                    CorrectedText = symbol.CorrectedText
                };
            }).ToList();
        }

        private static TokenType AsTokenType(Symbol symbol) =>
            (TokenType) symbol.SymbolType;

        private static List<Explanation> AsExplanation(Result result)
        {
            return result.Annotations.Select(annotation =>
            {
                return new Explanation
                {
                    Code = annotation.Code,
                    Detail = annotation.Detail,
                    EndCharacterIndex = annotation.EndCharacter,
                    StartCharacterIndex = annotation.StartCharacter,
                    Suggestion = annotation.Suggest,
                    Text = annotation.Text
                };
            }).ToList();
        }

        private static string SerializeText(string text)
        {
            object objectToSerialize = new { text };

            return JsonConvert.SerializeObject(objectToSerialize);
        }
    }
}