using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using System.Threading.Tasks;

namespace Grammar.Checker.Portal.Web.Services.Foundations.AnalyzedTexts
{
    public interface IAnalyzedTextService
    {
        ValueTask<AnalyzedText> AnalyzeTextAsync(string text);
    }
}