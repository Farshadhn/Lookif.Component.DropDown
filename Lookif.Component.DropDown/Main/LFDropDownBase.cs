using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Security.Principal;

namespace Lookif.Component.DropDown.Main;

public class LFDropDownBase : DropDownBase 
{

	 
    public async Task PerformUpdate(List<string> selectedOptions)
    { 
        await ReturnValueChanged.InvokeAsync(selectedOptions);
    }
   

}

