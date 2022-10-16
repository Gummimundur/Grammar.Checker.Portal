using SharpStyles.Models.Attributes;
using SharpStyles.Models;

namespace Grammar.Checker.Portal.Web.Models.Views.Components.Headers
{
    public class HeaderStyle : SharpStyle
    {
        [CssClass]
        public SharpStyle TopRow { get; set; }
    }
}