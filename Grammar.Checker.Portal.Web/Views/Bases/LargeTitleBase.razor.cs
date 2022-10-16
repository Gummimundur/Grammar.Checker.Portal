using Microsoft.AspNetCore.Components;

namespace Grammar.Checker.Portal.Web.Views.Bases
{
    public partial class LargeTitleBase : ComponentBase
    {
        [Parameter]
        public string Text { get; set; }

        [Parameter]
        public string ClassName { get; set; }
    }
}