using Xeptions;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions
{
    public class InvalidAnalyzedTextException : Xeption
    {
        public InvalidAnalyzedTextException()
            : base(message: "Invalid analyzed text error occurred")
        { }
    }
}