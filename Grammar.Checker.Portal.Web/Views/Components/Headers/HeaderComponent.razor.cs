using Grammar.Checker.Portal.Web.Models.Views.Components.Headers;
using Grammar.Checker.Portal.Web.Views.Bases;
using Microsoft.AspNetCore.Components;
using SharpStyles.Models;

namespace Grammar.Checker.Portal.Web.Views.Components.Headers
{
    public partial class HeaderComponent : ComponentBase
    {
        public HeaderBase Header { get; set; }
        public HeaderStyle Style { get; set; }
        public StyleBase StyleElement { get; set; }

        protected override void OnInitialized() =>
            SetupStyles();

        private void SetupStyles()
        {
            this.Style = new HeaderStyle
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
        }
    }
}
