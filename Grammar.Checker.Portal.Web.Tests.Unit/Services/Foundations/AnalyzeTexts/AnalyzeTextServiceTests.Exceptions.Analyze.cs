using FluentAssertions;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions;
using Moq;
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
    }
}