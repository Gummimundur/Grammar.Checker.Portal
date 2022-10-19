using Bunit;
using Grammar.Checker.Portal.Web.Brokers.ExternalTextAnalyzers;
using Grammar.Checker.Portal.Web.Brokers.Loggings;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.AnalyzedText;
using Grammar.Checker.Portal.Web.Models.Services.Foundations.ExternalAnalyzedText;
using Grammar.Checker.Portal.Web.Services.Foundations.AnalyzedTexts;
using KellermanSoftware.CompareNetObjects;
using Moq;
using Newtonsoft.Json;
using RESTFulSense.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Tynamix.ObjectFiller;
using Xeptions;
using Xunit;

namespace Grammar.Checker.Portal.Web.Tests.Unit.Services.Foundations.AnalyzeTexts
{
    public partial class AnalyzeTextServiceTests
    {
        private readonly Mock<IExternalTextAnalyzerBroker> externalTextAnalyzerBrokerMock;
        private readonly Mock<ILoggingBroker> loggingBrokerMock;
        private readonly ICompareLogic compareLogic;

        private readonly IAnalyzedTextService analyzedTextService;

        public AnalyzeTextServiceTests()
        {
            this.externalTextAnalyzerBrokerMock = new Mock<IExternalTextAnalyzerBroker>();
            this.loggingBrokerMock = new Mock<ILoggingBroker>();
            this.compareLogic = new CompareLogic();

            this.analyzedTextService = new AnalyzedTextService(
                externalTextAnalyzerBroker: this.externalTextAnalyzerBrokerMock.Object,
                loggingBroker: this.loggingBrokerMock.Object);
        }

        public static TheoryData CriticalDependencyExceptions()
        {
            return new TheoryData<Xeption>()
            {
                new HttpResponseUrlNotFoundException(),
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException()
            };
        }

        private static ExternalAnalyzedText CreateRandomExternalAnalyzedText(bool valid) =>
            CreateExternalAnalyzedTextFiller(valid).Create();

        private static Filler<ExternalAnalyzedText> CreateExternalAnalyzedTextFiller(bool valid)
        {
            var filler = new Filler<ExternalAnalyzedText>();

            filler.Setup()
                .OnProperty(externalAnalyzedFiller => externalAnalyzedFiller.Valid)
                    .Use(valid);

            return filler;
        }

        private static Expression<Func<Xeption, bool>> SameExceptionAs(Xeption expectedException) =>
            actualException => actualException.SameExceptionAs(expectedException);

        public static dynamic CreateRandomAnalyzedTextProperties()
        {
            Statistics randomStatistics =
                CreateRandomStatistics();

            (Result[][] randomResults, List<Suggestion> correspondingSuggestions) =
                GetRandomResultProperties();

            return new
            {
                Results = randomResults,
                Suggestions = correspondingSuggestions,
                Corrected = GetRandomString(),
                Original = GetRandomString(),
                Token = GetRandomString(),
                Statistics = randomStatistics,
                Text = GetRandomString(),
                Reason = GetRandomString(),
                Valid = true
            };
        }

        private static (Result[][] randomResults, List<Suggestion> correspondingSuggestions) GetRandomResultProperties()
        {
            int randomCount = GetRandomNumber();

            List<(Result randomResult, Suggestion randomSuggestion)> randomObjects =
                Enumerable.Range(start: 0, count: randomCount).Select(iteration =>
            {
                (Annotation[] randomAnnotations, List<Explanation> correspondingExplanations) =
                    GetRandomAnnotationsProperties();

                string randomCorrected = GetRandomString();
                string randomOriginal = GetRandomString();
                string randomToken = GetRandomString();

                var randomResult = new Result
                {
                    Annotations = randomAnnotations,
                    Corrected = randomCorrected,
                    Original = randomOriginal,
                    Token = randomToken,
                    Symbols = GetRandomSymbols(),
                };

                var randomSuggestion = new Suggestion
                {
                    Explanations = correspondingExplanations,
                    CorrectedText = randomCorrected,
                    OriginalText = randomOriginal
                };

                return (randomResult, randomSuggestion);
            }).ToList();

            var randomResults = new Result[][]
            {
                randomObjects.Select(randomObject =>
                {
                    return new Result
                    {
                        Annotations = randomObject.randomResult.Annotations,
                        Corrected = randomObject.randomResult.Corrected,
                        Original = randomObject.randomResult.Original,
                        Token = randomObject.randomResult.Token,
                        Symbols = randomObject.randomResult.Symbols
                    };
                }).ToArray()
            };

            List<Suggestion> randomSuggestions = randomObjects.Select(randomObject =>
            {
                return new Suggestion
                {
                    Explanations = randomObject.randomSuggestion.Explanations,
                    CorrectedText = randomObject.randomSuggestion.CorrectedText,
                    OriginalText = randomObject.randomSuggestion.OriginalText,
                };
            }).ToList();

            return (randomResults, randomSuggestions);
        }

        private static Symbol[] GetRandomSymbols()
        {
            int randomCount = GetRandomNumber();

            var filler = new Filler<Symbol>();

            return filler.Create(count: randomCount).ToArray();
        }

        private static (Annotation[] randomAnnotations, List<Explanation> correspondingExplanations)
            GetRandomAnnotationsProperties()
        {
            Annotation[] randomAnnotations = CreateRandomAnnotations();

            List<Explanation> correspondingExplanations = randomAnnotations.Select(annotation =>
            {
                return new Explanation
                {
                    Code = annotation.Code,
                    Detail = annotation.Detail,
                    EndCharacterIndex = annotation.EndCharacter,
                    StartCharacterIndex = annotation.StartCharacter,
                    Suggestion = annotation.Suggest,
                    Text = annotation.Text
                };
            }).ToList();

            return (randomAnnotations, correspondingExplanations);
        }

        private static string SerializeString(string inputString) =>
            JsonConvert.SerializeObject(new { text = inputString });

        private static Annotation[] CreateRandomAnnotations()
        {
            int randomCount = GetRandomNumber();

            var filler = new Filler<Annotation>();

            return filler.Create(count: randomCount).ToArray();
        }

        private static Statistics CreateRandomStatistics() =>
            new Filler<Statistics>().Create();

        private static int GetRandomNumber() =>
            new IntRange(min: 1, max: 15).GetValue();

        private static string GetRandomString() =>
            new MnemonicString().GetValue();
    }
}