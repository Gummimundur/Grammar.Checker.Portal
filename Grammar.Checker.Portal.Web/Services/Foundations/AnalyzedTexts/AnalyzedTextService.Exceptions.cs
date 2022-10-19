using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions;
using RESTFulSense.Exceptions;
using System;
using System.Threading.Tasks;
using Xeptions;

namespace Grammar.Checker.Portal.Web.Services.Foundations.AnalyzedTexts
{
    public partial class AnalyzedTextService
    {
        private delegate ValueTask<AnalyzedText> ReturningAnalyzedTextFunction();

        private async ValueTask<AnalyzedText> TryCatch(ReturningAnalyzedTextFunction returningAnalyzedTextFunction)
        {
            try
            {
                return await returningAnalyzedTextFunction();
            }
            catch (InvalidAnalyzedTextException invalidAnalyzedTextException)
            {
                throw CreateAndLogValidationException(invalidAnalyzedTextException);
            }
            catch (InvalidAnalyzedTextRequestException invalidAnalyzedTextRequestException)
            {
                throw CreateAndLogDependencyValidationException(invalidAnalyzedTextRequestException);
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
            catch (Exception exception)
            {
                var failedAnalyzedTextServiceException = new FailedAnalyzedTextServiceException(exception);

                throw CreateAndLogServiceException(failedAnalyzedTextServiceException);
            }
        }

        private AnalyzedTextValidationException CreateAndLogValidationException(Xeption exception)
        {
            var analyzedTextValidationException = new AnalyzedTextValidationException(exception);
            this.loggingBroker.LogError(analyzedTextValidationException);

            return analyzedTextValidationException;
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

        private AnalyzedTextDependencyValidationException CreateAndLogDependencyValidationException(Xeption exception)
        {
            var analyzedTextDependencyValidationException = new AnalyzedTextDependencyValidationException(exception);
            this.loggingBroker.LogError(analyzedTextDependencyValidationException);

            return analyzedTextDependencyValidationException;
        }

        private AnalyzedTextServiceException CreateAndLogServiceException(Xeption exception)
        {
            var analyzedTextServiceException = new AnalyzedTextServiceException(exception);
            this.loggingBroker.LogError(analyzedTextServiceException);

            return analyzedTextServiceException;
        }
    }
}