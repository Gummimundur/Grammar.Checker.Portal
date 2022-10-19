using Xeptions;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions
{
    public class AnalyzedTextDependencyException : Xeption
    {
        public AnalyzedTextDependencyException(Xeption innerException)
            : base(message: "Analyzed text dependency error occurred",
                  innerException)
        { }
    }
}