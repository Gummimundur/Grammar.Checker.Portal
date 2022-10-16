﻿using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using System.Threading.Tasks;

namespace Grammar.Checker.Portal.Web.Brokers.ExternalAnalyzers
{
    public partial interface IExternalTextAnalyzerBroker
    {
        ValueTask<ExternalAnalyzedText> AnalyzeTextAsync(string serializedText);
    }
}