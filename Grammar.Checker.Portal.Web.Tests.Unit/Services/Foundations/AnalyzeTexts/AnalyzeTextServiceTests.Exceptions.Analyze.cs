using FluentAssertions;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.ExternalAnalyzedText;
using Moq;
using RESTFulSense.Exceptions;
using Xeptions;
using Xunit;

namespace Grammar.Checker.Portal.Web.Tests.Unit.Services.Foundations.AnalyzeTexts
{
    public partial class AnalyzeTextServiceTests
    {
        [Theory]
        [MemberData(nameof(CriticalDependencyExceptions))]
        public async Task ShouldThrowCriticalDependencyExceptionOnAnalyzeTextIfCriticalErrorOccursAndLogItAsync(
            Xeption criticalDependencyException)
        {
            // Arrange
            string randomString = GetRandomString();
            string someInputText = randomString;

            var failedAnalyzedTextDependencyException =
                new FailedAnalyzedTextDependencyException(criticalDependencyException);

            var expectedAnalyzedTextDependencyException =
                new AnalyzedTextDependencyException(failedAnalyzedTextDependencyException);

            this.externalTextAnalyzerBrokerMock.Setup(broker =>
                broker.AnalyzeTextAsync(It.IsAny<string>()))
                    .ThrowsAsync(criticalDependencyException);

            // Act
            ValueTask<AnalyzedText> analyzeTextTask =
                this.analyzeTextService.AnalyzeTextAsync(someInputText);

            AnalyzedTextDependencyException actualAnalyzedTextDependencyException =
                await Assert.ThrowsAsync<AnalyzedTextDependencyException>(
                    analyzeTextTask.AsTask);

            // Assert
            actualAnalyzedTextDependencyException.Should().BeEquivalentTo(
                expectedAnalyzedTextDependencyException);

            this.externalTextAnalyzerBrokerMock.Verify(broker =>
                broker.AnalyzeTextAsync(It.IsAny<string>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogCritical(It.Is(SameExceptionAs(
                    expectedAnalyzedTextDependencyException))),
                        Times.Once);

            this.externalTextAnalyzerBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyExceptionOnAnalyzeTextIfErrorOccursAndLogItAsync()
        {
            // Arrange
            string randomString = GetRandomString();
            string someInputText = randomString;
            var httpResponseException = new HttpResponseException();

            var failedAnalyzedTextDependencyException =
                new FailedAnalyzedTextDependencyException(httpResponseException);

            var expectedAnalyzedTextDependencyException =
                new AnalyzedTextDependencyException(failedAnalyzedTextDependencyException);

            this.externalTextAnalyzerBrokerMock.Setup(broker =>
                broker.AnalyzeTextAsync(It.IsAny<string>()))
                    .ThrowsAsync(httpResponseException);

            // Act
            ValueTask<AnalyzedText> analyzeTextTask =
                this.analyzeTextService.AnalyzeTextAsync(someInputText);

            AnalyzedTextDependencyException actualAnalyzedTextDependencyException =
                await Assert.ThrowsAsync<AnalyzedTextDependencyException>(
                    analyzeTextTask.AsTask);

            // Assert
            actualAnalyzedTextDependencyException.Should().BeEquivalentTo(
                expectedAnalyzedTextDependencyException);

            this.externalTextAnalyzerBrokerMock.Verify(broker =>
                broker.AnalyzeTextAsync(It.IsAny<string>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedAnalyzedTextDependencyException))),
                        Times.Once);

            this.externalTextAnalyzerBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowDependencyValidationExceptionOnAnalyzeTextIfExternalAnalyzedTextIsInvalidAndLogItAsync()
        {
            // Arrange
            string randomString = GetRandomString();
            string someInputText = randomString;

            ExternalAnalyzedText invalidExternalAnalyzedText =
                CreateRandomExternalAnalyzedText(valid: false);

            var invalidAnalyzedTextRequestException =
                new InvalidAnalyzedTextRequestException(
                    reason: invalidExternalAnalyzedText.Reason);

            var expectedAnalyzedTextDependencyValidationException =
                new AnalyzedTextDependencyValidationException(
                    invalidAnalyzedTextRequestException);

            this.externalTextAnalyzerBrokerMock.Setup(broker =>
                broker.AnalyzeTextAsync(It.IsAny<string>()))
                    .ReturnsAsync(invalidExternalAnalyzedText);

            // Act
            ValueTask<AnalyzedText> analyzeTextTask =
                this.analyzeTextService.AnalyzeTextAsync(someInputText);

            AnalyzedTextDependencyValidationException actualAnalyzedTextDependencyValidationException =
                await Assert.ThrowsAsync<AnalyzedTextDependencyValidationException>(
                    analyzeTextTask.AsTask);

            // Assert
            actualAnalyzedTextDependencyValidationException.Should().BeEquivalentTo(
                expectedAnalyzedTextDependencyValidationException);

            this.externalTextAnalyzerBrokerMock.Verify(broker =>
                broker.AnalyzeTextAsync(It.IsAny<string>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedAnalyzedTextDependencyValidationException))),
                        Times.Once);

            this.externalTextAnalyzerBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task ShouldThrowServiceExceptionOnAnalyzeTextIfErrorOccursAndLogItAsync()
        {
            // Arrange
            string randomString = GetRandomString();
            string someInputText = randomString;
            var serviceException = new Exception();

            var failedAnalyzedTextServiceException =
                new FailedAnalyzedTextServiceException(serviceException);

            var expectedAnalyzedtextServiceException =
                new AnalyzedTextServiceException(failedAnalyzedTextServiceException);

            this.externalTextAnalyzerBrokerMock.Setup(broker =>
                broker.AnalyzeTextAsync(It.IsAny<string>()))
                    .ThrowsAsync(serviceException);

            // Act
            ValueTask<AnalyzedText> analyzeTextTask =
                this.analyzeTextService.AnalyzeTextAsync(someInputText);

            AnalyzedTextServiceException actualAnalyzedTextServiceException =
                await Assert.ThrowsAsync<AnalyzedTextServiceException>(
                    analyzeTextTask.AsTask);

            // Assert
            actualAnalyzedTextServiceException.Should().BeEquivalentTo(
                expectedAnalyzedtextServiceException);

            this.externalTextAnalyzerBrokerMock.Verify(broker =>
                broker.AnalyzeTextAsync(It.IsAny<string>()),
                    Times.Once);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedAnalyzedtextServiceException))),
                        Times.Once);

            this.externalTextAnalyzerBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}