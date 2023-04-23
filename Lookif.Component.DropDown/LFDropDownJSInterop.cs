using Lookif.Component.DropDown.Main;
using Microsoft.JSInterop;

namespace Lookif.Component.DropDown;


public class LFDropDownJSInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public LFDropDownJSInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/Lookif.Component.DropDown/LFDropDown.js").AsTask());
    }
    public async ValueTask SetOrUnsetInstance(DotNetObjectReference<DropDownBase> dotNetObjectReference, Guid identity, bool IsItSet)
    {
        var module = await moduleTask.Value;
        await module.InvokeVoidAsync("SetOrUnsetInstance", dotNetObjectReference, identity, IsItSet);
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
