using Lookif.Component.DropDown;
using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Reflection;

namespace TestDropDown.Components.Pages;

public partial class SamplePage : ComponentBase
{
    // Sample data models
    public class Country
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class City
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class Color
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    public class Fruit
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

    // Sample Enums
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

    // Sample data - Persian (RTL)
    private List<Country> countries = new()
    {
        new() { Id = "1", Name = "ایران" },
        new() { Id = "2", Name = "آمریکا" },
        new() { Id = "3", Name = "انگلستان" },
        new() { Id = "4", Name = "فرانسه" },
        new() { Id = "5", Name = "آلمان" },
        new() { Id = "6", Name = "ژاپن" },
        new() { Id = "7", Name = "چین" },
        new() { Id = "8", Name = "هند" }
    };

    // Sample data - English (LTR)
    private List<Country> englishCountries = new()
    {
        new() { Id = "1", Name = "United States" },
        new() { Id = "2", Name = "United Kingdom" },
        new() { Id = "3", Name = "France" },
        new() { Id = "4", Name = "Germany" },
        new() { Id = "5", Name = "Japan" },
        new() { Id = "6", Name = "China" },
        new() { Id = "7", Name = "India" },
        new() { Id = "8", Name = "Canada" }
    };

    private List<City> cities = new()
    {
        new() { Id = "1", Name = "تهران" },
        new() { Id = "2", Name = "اصفهان" },
        new() { Id = "3", Name = "شیراز" },
        new() { Id = "4", Name = "مشهد" },
        new() { Id = "5", Name = "تبریز" },
        new() { Id = "6", Name = "کرج" },
        new() { Id = "7", Name = "اهواز" },
        new() { Id = "8", Name = "قم" }
    };

    private List<Color> colors = new()
    {
        new() { Id = "1", Name = "قرمز" },
        new() { Id = "2", Name = "آبی" },
        new() { Id = "3", Name = "سبز" },
        new() { Id = "4", Name = "زرد" },
        new() { Id = "5", Name = "نارنجی" },
        new() { Id = "6", Name = "بنفش" },
        new() { Id = "7", Name = "صورتی" },
        new() { Id = "8", Name = "مشکی" }
    };

    private List<Fruit> fruits = new()
    {
        new() { Id = "1", Name = "سیب" },
        new() { Id = "2", Name = "موز" },
        new() { Id = "3", Name = "پرتقال" },
        new() { Id = "4", Name = "انگور" },
        new() { Id = "5", Name = "توت فرنگی" },
        new() { Id = "6", Name = "هلو" },
        new() { Id = "7", Name = "گلابی" },
        new() { Id = "8", Name = "آناناس" }
    };

    // Control states
    private bool basicEnabled = true;
    private bool complexEnabled = true;
    private bool directSingleEnabled = true;
    private bool directMultipleEnabled = true;

    // Selected options
    private List<string> basicSelectedOptions = new();
    private List<string> complexSelectedOptions = new();
    private List<string> directSingleSelected = new();
    private List<string> directMultipleSelected = new();
    
    // RTL/LTR demonstration
    private List<string> rtlSelectedOptions = new();
    private List<string> ltrSelectedOptions = new();

    // Enum demonstration
    private List<string> prioritySelectedOptions = new();
    private List<string> statusSelectedOptions = new();
    private List<string> roleSelectedOptions = new();

    // Display text
    private string basicSelectedText = string.Empty;
    private string complexSelectedText = string.Empty;
    private string directSingleText = string.Empty;
    private string directMultipleText = string.Empty;
    
    // RTL/LTR display text
    private string rtlSelectedText = string.Empty;
    private string ltrSelectedText = string.Empty;
    
    // Enum display text
    private string prioritySelectedText = string.Empty;
    private string statusSelectedText = string.Empty;
    private string roleSelectedText = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        StateHasChanged();
    }

    // Event handlers
    private async Task OnBasicSelectionChanged(List<string> selectedOptions)
    {
        basicSelectedOptions = selectedOptions;
        basicSelectedText = GetSelectedText(selectedOptions, countries);
        StateHasChanged();
        await Task.CompletedTask;
    }

    private async Task OnComplexSelectionChanged(List<string> selectedOptions)
    {
        complexSelectedOptions = selectedOptions;
        complexSelectedText = GetSelectedText(selectedOptions, cities);
        StateHasChanged();
        await Task.CompletedTask;
    }

    private async Task OnDirectSingleChanged(List<string> selectedOptions)
    {
        directSingleSelected = selectedOptions;
        directSingleText = GetSelectedText(selectedOptions, colors);
        StateHasChanged();
        await Task.CompletedTask;
    }

    private async Task OnDirectMultipleChanged(List<string> selectedOptions)
    {
        directMultipleSelected = selectedOptions;
        directMultipleText = GetSelectedText(selectedOptions, fruits);
        StateHasChanged();
        await Task.CompletedTask;
    }

    // RTL/LTR demonstration event handlers
    private async Task OnRTLSelectionChanged(List<string> selectedOptions)
    {
        rtlSelectedOptions = selectedOptions;
        rtlSelectedText = GetSelectedText(selectedOptions, countries);
        StateHasChanged();
        await Task.CompletedTask;
    }

    private async Task OnLTRSelectionChanged(List<string> selectedOptions)
    {
        ltrSelectedOptions = selectedOptions;
        ltrSelectedText = GetSelectedText(selectedOptions, englishCountries);
        StateHasChanged();
        await Task.CompletedTask;
    }

    // Enum event handlers
    private async Task OnPrioritySelectionChanged(List<string> selectedOptions)
    {
        prioritySelectedOptions = selectedOptions;
        prioritySelectedText = GetEnumSelectedText(selectedOptions, typeof(Priority));
        StateHasChanged();
        await Task.CompletedTask;
    }

    private async Task OnStatusSelectionChanged(List<string> selectedOptions)
    {
        statusSelectedOptions = selectedOptions;
        statusSelectedText = GetEnumSelectedText(selectedOptions, typeof(Status));
        StateHasChanged();
        await Task.CompletedTask;
    }

    private async Task OnRoleSelectionChanged(List<string> selectedOptions)
    {
        roleSelectedOptions = selectedOptions;
        roleSelectedText = GetEnumSelectedText(selectedOptions, typeof(UserRole));
        StateHasChanged();
        await Task.CompletedTask;
    }

    // Helper method to get display text for selected options
    private string GetSelectedText<T>(List<string> selectedIds, List<T> data) where T : class
    {
        if (!selectedIds.Any())
            return "";

        var selectedItems = data.Where(item =>
        {
            var idProperty = item.GetType().GetProperty("Id");
            return idProperty != null && selectedIds.Contains(idProperty.GetValue(item)?.ToString() ?? "");
        }).ToList();

        var nameProperty = typeof(T).GetProperty("Name");
        if (nameProperty != null)
        {
            var names = selectedItems.Select(item => nameProperty.GetValue(item)?.ToString()).Where(n => !string.IsNullOrEmpty(n));
            return string.Join(", ", names);
        }

        return string.Join(", ", selectedIds);
    }

    // Helper method to get display text for selected enum options
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
}
