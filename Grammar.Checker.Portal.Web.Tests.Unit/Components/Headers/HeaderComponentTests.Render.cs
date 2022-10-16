using FluentAssertions;
using Grammar.Checker.Portal.Web.Models.Views.Components.Headers;
using Grammar.Checker.Portal.Web.Views.Components.Headers;
using SharpStyles.Models;
using Xunit;

namespace Grammar.Checker.Portal.Web.Tests.Unit.Components.Headers
{
    public partial class HeaderComponentTests
    {
        [Fact]
        public void ShouldRenderDefaultValues()
        {
            // given . when
            var renderedHeaderComponent = new HeaderComponent();

            // then
            renderedHeaderComponent.Header.Should().BeNull();
            renderedHeaderComponent.Style.Should().BeNull();
            renderedHeaderComponent.StyleElement.Should().BeNull();
        }

        [Fact]
        public void ShouldRenderHeaderWithStyle()
        {
            // given
            string expectedClassName = "top-row";

            var expectedHeaderStyle = new HeaderStyle
            {
                TopRow = new SharpStyle
                {
                    BackgroundColor = "#0078d4",
                    BorderBottom = "1px solid #d6d5d5",
                    JustifyContent = "flex-start",
                    Height = "3.5rem",
                    Display = "flex",
                    AlignItems = "center",
                }
            };

            // when
            this.renderedHeaderComponent = RenderComponent<HeaderComponent>();

            // then
            this.renderedHeaderComponent.Instance.Header
                .Should().NotBeNull();

            this.renderedHeaderComponent.Instance.Header.ClassName
                .Should().Be(expectedClassName);

            this.renderedHeaderComponent.Instance.Style
                .Should().BeEquivalentTo(expectedHeaderStyle);

            this.renderedHeaderComponent.Instance.StyleElement.Style
                .Should().BeEquivalentTo(expectedHeaderStyle);
        }
    }
}