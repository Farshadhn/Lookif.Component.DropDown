using Lookif.Component.DropDown.Main;
using Microsoft.JSInterop;

namespace Lookif.Component.DropDown;

public class LFDropDownJSInterop : IAsyncDisposable
{
    private readonly IJSRuntime jsRuntime;

    public LFDropDownJSInterop(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
    }

    public async ValueTask SetOrUnsetInstance(DotNetObjectReference<DropDownBase> dotNetObjectReference, Guid identity, bool IsItSet)
    {
        await jsRuntime.InvokeVoidAsync("LFDropDown.SetOrUnsetInstance", dotNetObjectReference, identity, IsItSet);
    }

    public async ValueTask DisposeAsync()
    {
        // No cleanup needed for window-based JavaScript
        await Task.CompletedTask;
    }
}
