using Xeptions;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions
{
    public class AnalyzedTextValidationException : Xeption
    {
        public AnalyzedTextValidationException(Xeption innerException)
            : base(message: "Analyzed text validation error occurred",
                  innerException)
        { }
    }
}