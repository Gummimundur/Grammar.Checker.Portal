using Xeptions;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions
{
    public class InvalidAnalyzedTextRequestException : Xeption
    {
        public InvalidAnalyzedTextRequestException(string reason) :
            base (message: $"Text could not be analyzed: {reason}")
        { }
    }
}