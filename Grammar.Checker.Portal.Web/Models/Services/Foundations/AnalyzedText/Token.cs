namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText
{
    public class Token
    {
        public long WordIndex { get; set; }
        public TokenType TokenType { get; set; }
        public string OriginalText { get; set; }
        public string CorrectedText { get; set; }
    }
}