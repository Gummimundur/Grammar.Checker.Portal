using Microsoft.AspNetCore.Components;
using SharpStyles.Models;

namespace Grammar.Checker.Portal.Web.Views.Bases
{
    public partial class StyleBase : ComponentBase
    {
        [Parameter]
        public SharpStyle Style { get; set; }
    }
}