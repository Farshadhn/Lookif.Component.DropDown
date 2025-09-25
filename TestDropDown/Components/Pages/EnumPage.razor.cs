using Lookif.Component.DropDown.Main;
using Lookif.Component.DropDown.Basic;
using Lookif.Component.DropDown.Complex;
using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Components;

namespace TestDropDown.Components.Pages;

public partial class EnumPage : ComponentBase
{
    // State variables for basic enum examples
    public List<string> prioritySelectedOptions = new();
    public List<string> statusSelectedOptions = new();
    public List<string> roleSelectedOptions = new();

    public string? prioritySelectedText;
    public string? statusSelectedText;
    public string? roleSelectedText;

    // State variables for RTL/LTR enum examples
    public List<string> rtlPrioritySelectedOptions = new();
    public List<string> ltrStatusSelectedOptions = new();

    public string? rtlPrioritySelectedText;
    public string? ltrStatusSelectedText;

    // State variables for multiple choice enum examples
    public List<string> multipleRoleSelectedOptions = new();
    public List<string> multiplePrioritySelectedOptions = new();

    public string? multipleRoleSelectedText;
    public string? multiplePrioritySelectedText;

    // Event handlers for basic enum examples
    public async Task OnPrioritySelectionChanged(List<string> selectedIds)
    {
        prioritySelectedOptions = selectedIds;
        prioritySelectedText = GetEnumSelectedText(selectedIds, typeof(Priority));
        await InvokeAsync(StateHasChanged);
    }

    public async Task OnStatusSelectionChanged(List<string> selectedIds)
    {
        statusSelectedOptions = selectedIds;
        statusSelectedText = GetEnumSelectedText(selectedIds, typeof(Status));
        await InvokeAsync(StateHasChanged);
    }

    public async Task OnRoleSelectionChanged(List<string> selectedIds)
    {
        roleSelectedOptions = selectedIds;
        roleSelectedText = GetEnumSelectedText(selectedIds, typeof(UserRole));
        await InvokeAsync(StateHasChanged);
    }

    // Event handlers for RTL/LTR enum examples
    public async Task OnRTLPrioritySelectionChanged(List<string> selectedIds)
    {
        rtlPrioritySelectedOptions = selectedIds;
        rtlPrioritySelectedText = GetEnumSelectedText(selectedIds, typeof(Priority));
        await InvokeAsync(StateHasChanged);
    }

    public async Task OnLTRStatusSelectionChanged(List<string> selectedIds)
    {
        ltrStatusSelectedOptions = selectedIds;
        ltrStatusSelectedText = GetEnumSelectedText(selectedIds, typeof(Status));
        await InvokeAsync(StateHasChanged);
    }

    // Event handlers for multiple choice enum examples
    public async Task OnMultipleRoleSelectionChanged(List<string> selectedIds)
    {
        multipleRoleSelectedOptions = selectedIds;
        multipleRoleSelectedText = GetEnumSelectedText(selectedIds, typeof(UserRole));
        await InvokeAsync(StateHasChanged);
    }

    public async Task OnMultiplePrioritySelectionChanged(List<string> selectedIds)
    {
        multiplePrioritySelectedOptions = selectedIds;
        multiplePrioritySelectedText = GetEnumSelectedText(selectedIds, typeof(Priority));
        await InvokeAsync(StateHasChanged);
    }

    private string GetEnumSelectedText(List<string> selectedIds, Type enumType)
    {
        if (!selectedIds.Any())
            return "";

        var selectedNames = new List<string>();
        foreach (var id in selectedIds)
        {
            if (int.TryParse(id, out int intValue))
            {
                var enumValue = Enum.ToObject(enumType, intValue);
                var field = enumType.GetField(enumValue.ToString());
                if (field != null)
                {
                    var descriptionAttribute = field.GetCustomAttribute<DescriptionAttribute>();
                    if (descriptionAttribute != null)
                    {
                        selectedNames.Add(descriptionAttribute.Description);
                    }
                    else
                    {
                        selectedNames.Add(enumValue.ToString());
                    }
                }
            }
        }
        return string.Join(", ", selectedNames);
    }

    // Enum definitions
    public enum Priority
    {
        [Description("Low Priority")]
        Low = 1,
        [Description("Medium Priority")]
        Medium = 2,
        [Description("High Priority")]
        High = 3,
        [Description("Critical Priority")]
        Critical = 4
    }

    public enum Status
    {
        [Description("Pending")]
        Pending = 0,
        [Description("In Progress")]
        InProgress = 1,
        [Description("Completed")]
        Completed = 2,
        [Description("Cancelled")]
        Cancelled = 3
    }

    public enum UserRole
    {
        [Description("Administrator")]
        Admin = 1,
        [Description("Manager")]
        Manager = 2,
        [Description("User")]
        User = 3,
        [Description("Guest")]
        Guest = 4
    }
}
