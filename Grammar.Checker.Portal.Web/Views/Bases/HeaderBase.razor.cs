using Microsoft.AspNetCore.Components;

namespace Grammar.Checker.Portal.Web.Views.Bases
{
    public partial class HeaderBase : ComponentBase
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}