using System;
using Xeptions;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions
{
    public class FailedAnalyzedTextServiceException : Xeption
    {
        public FailedAnalyzedTextServiceException(Exception innerException)
            : base(message: "Failed analyzed text service error occurred",
                  innerException)
        { }
    }
}