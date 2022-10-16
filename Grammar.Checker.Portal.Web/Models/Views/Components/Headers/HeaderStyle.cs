using SharpStyles.Models;
using SharpStyles.Models.Attributes;

namespace Grammar.Checker.Portal.Web.Models.Views.Components.Headers
{
    public class HeaderStyle : SharpStyle
    {
        [CssClass]
        public SharpStyle TopRow { get; set; }
    }
}