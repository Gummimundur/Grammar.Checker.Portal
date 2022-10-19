using Xeptions;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions
{
    public class AnalyzedTextServiceException : Xeption
    {
        public AnalyzedTextServiceException(Xeption innerException)
            : base(message: "Analyzed text service error occurred",
                  innerException)
        { }
    }
}