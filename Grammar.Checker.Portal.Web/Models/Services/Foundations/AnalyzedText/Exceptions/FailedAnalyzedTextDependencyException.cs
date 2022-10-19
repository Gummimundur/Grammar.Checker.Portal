using System;
using Xeptions;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions
{
    public class FailedAnalyzedTextDependencyException : Xeption
    {
        public FailedAnalyzedTextDependencyException(Exception innerException)
            : base(message: "Failed analyzed text dependency error occurred",
                  innerException)
        { }
    }
}