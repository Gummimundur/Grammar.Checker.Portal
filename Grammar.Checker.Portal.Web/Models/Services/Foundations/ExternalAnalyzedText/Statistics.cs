using Newtonsoft.Json;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.ExternalAnalyzedText
{
    public class Statistics
    {
        [JsonProperty("ambiguity")]
        public long Ambiguity { get; set; }

        [JsonProperty("num_chars")]
        public long TotalCharacters { get; set; }

        [JsonProperty("num_parsed")]
        public long TotalParsed { get; set; }

        [JsonProperty("num_sentences")]
        public long TotalSentences { get; set; }

        [JsonProperty("num_tokens")]
        public long TotalTokens { get; set; }
    }
}