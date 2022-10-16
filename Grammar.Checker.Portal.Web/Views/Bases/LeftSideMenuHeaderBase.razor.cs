using Microsoft.AspNetCore.Components;

namespace Grammar.Checker.Portal.Web.Views.Bases
{
    public partial class LeftSideMenuHeaderBase : ComponentBase
    {
        [Parameter]
        public string Title { get; set; }
    }
}