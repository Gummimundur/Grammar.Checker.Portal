using Newtonsoft.Json;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.ExternalAnalyzedText
{
    public class ExternalAnalyzedText
    {
        [JsonProperty("result")]
        public Result[][] Result { get; set; }

        [JsonProperty("stats")]
        public Statistics Statistics { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("reason")]
        public string Reason { get; set; }

        [JsonProperty("valid")]
        public bool Valid { get; set; }
    }
}