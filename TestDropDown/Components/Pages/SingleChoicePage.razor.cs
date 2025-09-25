using Lookif.Component.DropDown.Main;
using Lookif.Component.DropDown.Basic;
using Lookif.Component.DropDown.Complex;
using System.ComponentModel;
using Microsoft.AspNetCore.Components;

namespace TestDropDown.Components.Pages;

public partial class SingleChoicePage : ComponentBase
{
    // Data sources
    public List<Country> countries = new();
    public List<Country> englishCountries = new();
    public List<Color> colors = new();

    // State variables
    public List<string> rtlSelectedOptions = new();
    public List<string> ltrSelectedOptions = new();
    public List<string> basicSelectedOptions = new();
    public List<string> directSingleSelected = new();

    public string? rtlSelectedText;
    public string? ltrSelectedText;
    public string? basicSelectedText;
    public string? directSingleText;

    public bool basicEnabled = true;
    public bool directSingleEnabled = true;

    protected override void OnInitialized()
    {
        // Persian countries (RTL)
        countries = new List<Country>
        {
            new() { Id = "1", Name = "ایران" },
            new() { Id = "2", Name = "عراق" },
            new() { Id = "3", Name = "ترکیه" },
            new() { Id = "4", Name = "عربستان سعودی" },
            new() { Id = "5", Name = "مصر" }
        };

        // English countries (LTR)
        englishCountries = new List<Country>
        {
            new() { Id = "1", Name = "United States" },
            new() { Id = "2", Name = "United Kingdom" },
            new() { Id = "3", Name = "Canada" },
            new() { Id = "4", Name = "Australia" },
            new() { Id = "5", Name = "Germany" }
        };

        // Colors for direct component
        colors = new List<Color>
        {
            new() { Id = "1", Name = "قرمز" },
            new() { Id = "2", Name = "آبی" },
            new() { Id = "3", Name = "سبز" },
            new() { Id = "4", Name = "زرد" },
            new() { Id = "5", Name = "بنفش" }
        };
    }

    // Event handlers
    public async Task OnRTLSelectionChanged(List<string> selectedIds)
    {
        rtlSelectedOptions = selectedIds;
        rtlSelectedText = GetSelectedText(selectedIds, countries);
        await InvokeAsync(StateHasChanged);
    }

    public async Task OnLTRSelectionChanged(List<string> selectedIds)
    {
        ltrSelectedOptions = selectedIds;
        ltrSelectedText = GetSelectedText(selectedIds, englishCountries);
        await InvokeAsync(StateHasChanged);
    }

    public async Task OnBasicSelectionChanged(List<string> selectedIds)
    {
        basicSelectedOptions = selectedIds;
        basicSelectedText = GetSelectedText(selectedIds, countries);
        await InvokeAsync(StateHasChanged);
    }

    public async Task OnDirectSingleChanged(List<string> selectedIds)
    {
        directSingleSelected = selectedIds;
        directSingleText = GetSelectedText(selectedIds, colors);
        await InvokeAsync(StateHasChanged);
    }

    private string GetSelectedText(List<string> selectedIds, List<Country> dataSource)
    {
        if (!selectedIds.Any())
            return "";

        var selectedNames = new List<string>();
        foreach (var id in selectedIds)
        {
            var item = dataSource.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                selectedNames.Add(item.Name);
            }
        }
        return string.Join(", ", selectedNames);
    }

    private string GetSelectedText(List<string> selectedIds, List<Color> dataSource)
    {
        if (!selectedIds.Any())
            return "";

        var selectedNames = new List<string>();
        foreach (var id in selectedIds)
        {
            var item = dataSource.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                selectedNames.Add(item.Name);
            }
        }
        return string.Join(", ", selectedNames);
    }

    public class Country
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
    }

    public class Color
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
