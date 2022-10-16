using Newtonsoft.Json;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText
{
    public class Annotation
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("detail")]
        public object Detail { get; set; }

        [JsonProperty("end")]
        public long End { get; set; }

        [JsonProperty("end_char")]
        public long EndCharacter { get; set; }

        [JsonProperty("references")]
        public object[] References { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("start_char")]
        public long StartCharacter { get; set; }

        [JsonProperty("suggest")]
        public string Suggest { get; set; }

        [JsonProperty("suggestlist")]
        public object SuggestionList { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }
}