using Newtonsoft.Json;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.ExternalAnalyzedText
{
    public class Symbol
    {
        [JsonProperty("i")]
        public long WordIndex { get; set; }

        [JsonProperty("k")]
        public SymbolType SymbolType { get; set; }

        [JsonProperty("o")]
        public string OriginalText { get; set; }

        [JsonProperty("x")]
        public string CorrectedText { get; set; }
    }
}