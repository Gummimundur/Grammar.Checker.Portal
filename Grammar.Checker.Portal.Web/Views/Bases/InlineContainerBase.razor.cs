using Microsoft.AspNetCore.Components;

namespace Grammar.Checker.Portal.Web.Views.Bases
{
    public partial class InlineContainer : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string CssClass { get; set; }
    }
}