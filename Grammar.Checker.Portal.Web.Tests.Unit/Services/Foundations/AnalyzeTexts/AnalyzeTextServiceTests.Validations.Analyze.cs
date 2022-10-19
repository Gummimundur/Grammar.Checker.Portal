using FluentAssertions;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText.Exceptions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Grammar.Checker.Portal.Web.Tests.Unit.Services.Foundations.AnalyzeTexts
{
    public partial class AnalyzeTextServiceTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ShouldThrowValidationExceptionIfTextIsInvalidAndLogItAsync(
            string invalidString)
        {
            // Arrange
            string invalidText = invalidString;
            var invalidAnalyzedTextException = new InvalidAnalyzedTextException();

            invalidAnalyzedTextException.AddData(
                key: nameof(AnalyzedText.Text),
                values: "Text is required");

            var expectedAnalyzedTextValidationException =
                new AnalyzedTextValidationException(invalidAnalyzedTextException);

            // Act
            ValueTask<AnalyzedText> analyzeTextTask =
                this.analyzedTextService.AnalyzeTextAsync(invalidText);

            AnalyzedTextValidationException actualAnalyzedTextValidationException =
                await Assert.ThrowsAsync<AnalyzedTextValidationException>(
                    analyzeTextTask.AsTask);

            // Assert
            actualAnalyzedTextValidationException.Should().BeEquivalentTo(
                expectedAnalyzedTextValidationException);

            this.loggingBrokerMock.Verify(broker =>
                broker.LogError(It.Is(SameExceptionAs(
                    expectedAnalyzedTextValidationException))),
                        Times.Once);

            this.externalTextAnalyzerBrokerMock.Verify(broker =>
                broker.AnalyzeTextAsync(It.IsAny<string>()),
                    Times.Never);

            this.loggingBrokerMock.VerifyNoOtherCalls();
            this.externalTextAnalyzerBrokerMock.VerifyNoOtherCalls();
        }
    }
}