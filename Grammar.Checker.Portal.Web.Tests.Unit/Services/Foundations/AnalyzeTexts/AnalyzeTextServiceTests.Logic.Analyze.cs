using FluentAssertions;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.ExternalAnalyzedText;
using Moq;
using Xunit;

namespace Grammar.Checker.Portal.Web.Tests.Unit.Services.Foundations.AnalyzeTexts
{
    public partial class AnalyzeTextServiceTests
    {
        [Fact]
        public async Task ShouldAnalyzeTextAsync()
        {
            // Arrange
            string randomString = GetRandomString();
            string inputString = randomString;
            string serializedInputString = SerializeString(inputString);

            dynamic randomAnalyzedTextProperties =
                CreateRandomAnalyzedTextProperties();

            var randomExternalyzedText = new ExternalAnalyzedText
            {
                Result = randomAnalyzedTextProperties.Results,
                Statistics = randomAnalyzedTextProperties.Statistics,
                Text = randomAnalyzedTextProperties.Text,
                Reason = randomAnalyzedTextProperties.Reason,
                Valid = randomAnalyzedTextProperties.Valid
            };

            ExternalAnalyzedText retrievedExternalAnalyzedText =
                randomExternalyzedText;

            var expectedAnalyzedText = new AnalyzedText
            {
                Suggestions = randomAnalyzedTextProperties.Suggestions,
                Text = randomAnalyzedTextProperties.Text
            };

            this.externalTextAnalyzerBrokerMock.Setup(broker =>
                broker.AnalyzeTextAsync(serializedInputString))
                        .ReturnsAsync(retrievedExternalAnalyzedText);

            // Act
            AnalyzedText actualAnalyzedText =
                await this.analyzeTextService.AnalyzeTextAsync(inputString);

            // Assert
            actualAnalyzedText.Should().BeEquivalentTo(
                expectedAnalyzedText);

            this.externalTextAnalyzerBrokerMock.Verify(broker =>
                broker.AnalyzeTextAsync(serializedInputString),
                    Times.Once());

            this.externalTextAnalyzerBrokerMock.VerifyNoOtherCalls();
            this.loggingBrokerMock.VerifyNoOtherCalls();
        }
    }
}