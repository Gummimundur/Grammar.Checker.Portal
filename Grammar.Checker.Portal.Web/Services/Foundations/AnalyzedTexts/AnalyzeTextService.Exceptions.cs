using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions;
using RESTFulSense.Exceptions;
using System.Threading.Tasks;
using Xeptions;

namespace Grammar.Checker.Portal.Web.Services.Foundations.AnalyzedTexts
{
    public partial class AnalyzeTextService
    {
        private delegate ValueTask<AnalyzedText> ReturningAnalyzedTextFunction();

        private async ValueTask<AnalyzedText> TryCatch(ReturningAnalyzedTextFunction returningAnalyzedTextFunction)
        {
            try
            {
                return await returningAnalyzedTextFunction();
            }
            catch (HttpResponseUrlNotFoundException httpResponseUrlNotFoundException)
            {
                var failedAnalyzedTextDependencyException =
                    new FailedAnalyzedTextDependencyException(httpResponseUrlNotFoundException);

                throw CreateAndLogCriticalDependencyException(failedAnalyzedTextDependencyException);
            }
            catch (HttpResponseUnauthorizedException httpResponseUnauthorizedException)
            {
                var failedAnalyzedTextDependencyException =
                    new FailedAnalyzedTextDependencyException(httpResponseUnauthorizedException);

                throw CreateAndLogCriticalDependencyException(failedAnalyzedTextDependencyException);
            }
            catch (HttpResponseForbiddenException httpResponseForbiddenException)
            {
                var failedAnalyzedTextDependencyException =
                    new FailedAnalyzedTextDependencyException(httpResponseForbiddenException);

                throw CreateAndLogCriticalDependencyException(failedAnalyzedTextDependencyException);
            }
            catch (HttpResponseException httpResponseException)
            {
                var failedAnalyzedTextDependencyException = 
                    new FailedAnalyzedTextDependencyException(httpResponseException);

                throw CreateAndLogDependencyException(failedAnalyzedTextDependencyException);
            }
        }
        private AnalyzedTextDependencyException CreateAndLogCriticalDependencyException(Xeption exception)
        {
            var analyzedTextDependencyException = new AnalyzedTextDependencyException(exception);
            this.loggingBroker.LogCritical(analyzedTextDependencyException);

            return analyzedTextDependencyException;
        }

        private AnalyzedTextDependencyException CreateAndLogDependencyException(Xeption exception)
        {
            var analyzedTextDependencyException = new AnalyzedTextDependencyException(exception);
            this.loggingBroker.LogError(analyzedTextDependencyException);

            return analyzedTextDependencyException;
        }
    }
}