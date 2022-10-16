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
    }
}