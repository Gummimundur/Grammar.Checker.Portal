using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.RichTextEditor;
using System.Threading.Tasks;
using ChangeEventArgs = Syncfusion.Blazor.RichTextEditor.ChangeEventArgs;

namespace Grammar.Checker.Portal.Web.Views.Bases
{
    public partial class TextEditor : ComponentBase
    {
        public SfRichTextEditor RichTextEditor { get; set; }

        [Parameter]
        public string Value { get; set; }

        [Parameter]
        public string Placeholder { get; set; }

        [Parameter]
        public EventCallback<string> ValueChanged { get; set; }

        [Parameter]
        public bool IsDisabled { get; set; }

        [Parameter]
        public string CssClass { get; set; }

        public bool IsEnabled => IsDisabled is false;

        public Task SetValueAsync(string value) =>
        InvokeAsync(async () =>
        {
            this.Value = value;
            await this.ValueChanged.InvokeAsync(this.Value);
        });

        private async Task OnValueChanged(ChangeEventArgs changeEventArgs)
        {
            this.Value = await this.RichTextEditor.GetTextAsync();
            await ValueChanged.InvokeAsync(this.Value);
        }

        public void Disable()
        {
            this.IsDisabled = true;
            InvokeAsync(StateHasChanged);
        }

        public void Enable()
        {
            this.IsDisabled = false;
            InvokeAsync(StateHasChanged);
        }
    }
}