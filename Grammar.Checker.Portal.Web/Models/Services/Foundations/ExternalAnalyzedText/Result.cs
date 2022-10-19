using Newtonsoft.Json;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.ExternalAnalyzedText
{
    public class Result
    {
        [JsonProperty("annotations")]
        public Annotation[] Annotations { get; set; }

        [JsonProperty("corrected")]
        public string Corrected { get; set; }

        [JsonProperty("original")]
        public string Original { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("tokens")]
        public Symbol[] Symbols { get; set; }
    }
}