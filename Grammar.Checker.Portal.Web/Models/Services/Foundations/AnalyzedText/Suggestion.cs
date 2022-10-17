using System.Collections.Generic;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText
{
    public class Suggestion
    {
        public List<Explanation> Explanations { get; set; }
        public string CorrectedText { get; set; }
        public string OriginalText { get; set; }
        public List<Token> Tokens { get; set; }
    }
}