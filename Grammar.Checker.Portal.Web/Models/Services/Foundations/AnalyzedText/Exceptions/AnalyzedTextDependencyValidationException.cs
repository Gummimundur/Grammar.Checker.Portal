using Xeptions;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions
{
    public class AnalyzedTextDependencyValidationException : Xeption
    {
        public AnalyzedTextDependencyValidationException(Xeption innerException)
            : base(message: "Analyzed text dependency validation error occurred",
                  innerException)
        { }
    }
}