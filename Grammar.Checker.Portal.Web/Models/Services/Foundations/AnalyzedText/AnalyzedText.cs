using System.Collections.Generic;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText
{
    public class AnalyzedText
    {
        public List<Suggestion> Suggestions { get; set; }
        public string Text { get; set; }
    }
}