namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText
{
    public class Explanation
    {
        public string Code { get; set; }
        public string Detail { get; set; }
        public long EndCharacterIndex { get; set; }
        public long StartCharacterIndex { get; set; }
        public string Suggestion { get; set; }
        public string Text { get; set; }
    }
}