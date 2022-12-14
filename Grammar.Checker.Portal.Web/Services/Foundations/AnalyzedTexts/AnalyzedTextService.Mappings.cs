using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.ExternalAnalyzedText;
using System.Collections.Generic;
using System.Linq;

namespace Grammar.Checker.Portal.Web.Services.Foundations.AnalyzedTexts
{
    public partial class AnalyzedTextService
    {
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
                };
            }).ToList();
        }

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
    }
}