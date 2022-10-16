using Microsoft.AspNetCore.Components;

namespace Grammar.Checker.Portal.Web.Views.Bases
{
    public partial class ContainerBase : ComponentBase
    {
        [Parameter]
        public string Style { get; set; }

        [Parameter]
        public string CssClass { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}