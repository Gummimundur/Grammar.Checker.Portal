using Newtonsoft.Json;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText
{
    public class Token
    {
        [JsonProperty("i")]
        public long WordIndex { get; set; }

        [JsonProperty("k")]
        public WordType WordType { get; set; }

        [JsonProperty("o")]
        public string OriginalText { get; set; }

        [JsonProperty("x")]
        public string CorrectedText { get; set; }
    }
}