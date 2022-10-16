using Newtonsoft.Json;
using static Grammar.Checker.Portal.Web.Views.Pages.Index;

namespace Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText
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
        public Token[] Tokens { get; set; }
    }
}