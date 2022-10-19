using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.ExternalAnalyzedText;

namespace Grammar.Checker.Portal.Web.Services.Foundations.AnalyzedTexts
{
    public partial class AnalyzeTextService
    {
        private static void ValidateText(string text) =>
            Validate((Rule: IsInvalid(text), Parameter: nameof(AnalyzedText.Text)));

        private static void ValidateExternalAnalyzedText(ExternalAnalyzedText externalAnalyzedText)
        {
            if (externalAnalyzedText.Valid is not true)
            {
                throw new InvalidAnalyzedTextRequestException(reason: externalAnalyzedText.Reason);
            }
        }

        private static dynamic IsInvalid(string text) => new
        {
            Condition = string.IsNullOrWhiteSpace(text),
            Message = "Text is required"
        };

        private static void Validate(params (dynamic Rule, string Parameter)[] validations)
        {
            var invalidAnalyzedTextException = new InvalidAnalyzedTextException();

            foreach ((dynamic rule, string parameter) in validations)
            {
                if (rule.Condition)
                {
                    invalidAnalyzedTextException.UpsertDataList(
                        key: parameter,
                        value: rule.Message);
                }
            }

            invalidAnalyzedTextException.ThrowIfContainsErrors();
        }
    }
}