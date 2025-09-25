using Lookif.Component.DropDown.Main;
using Lookif.Component.DropDown.Basic;
using Lookif.Component.DropDown.Complex;
using Microsoft.AspNetCore.Components;

namespace TestDropDown.Components.Pages;

public partial class MultipleChoicePage : ComponentBase
{
    // Data sources
    public List<City> persianCities = new();
    public List<City> englishCities = new();
    public List<City> cities = new();
    public List<Fruit> fruits = new();

    // State variables
    public List<string> rtlMultipleSelectedOptions = new();
    public List<string> ltrMultipleSelectedOptions = new();
    public List<string> complexSelectedOptions = new();
    public List<string> directMultipleSelected = new();

    public string? rtlMultipleSelectedText;
    public string? ltrMultipleSelectedText;
    public string? complexSelectedText;
    public string? directMultipleText;

    public bool complexEnabled = true;
    public bool directMultipleEnabled = true;

    protected override void OnInitialized()
    {
        // Persian cities (RTL)
        persianCities = new List<City>
        {
            new() { Id = "1", Name = "تهران" },
            new() { Id = "2", Name = "مشهد" },
            new() { Id = "3", Name = "اصفهان" },
            new() { Id = "4", Name = "شیراز" },
            new() { Id = "5", Name = "تبریز" }
        };

        // English cities (LTR)
        englishCities = new List<City>
        {
            new() { Id = "1", Name = "New York" },
            new() { Id = "2", Name = "London" },
            new() { Id = "3", Name = "Paris" },
            new() { Id = "4", Name = "Tokyo" },
            new() { Id = "5", Name = "Sydney" }
        };

        // Mixed cities for complex dropdown
        cities = new List<City>
        {
            new() { Id = "1", Name = "تهران" },
            new() { Id = "2", Name = "مشهد" },
            new() { Id = "3", Name = "اصفهان" },
            new() { Id = "4", Name = "شیراز" },
            new() { Id = "5", Name = "تبریز" }
        };

        // Fruits for direct component
        fruits = new List<Fruit>
        {
            new() { Id = "1", Name = "سیب" },
            new() { Id = "2", Name = "موز" },
            new() { Id = "3", Name = "پرتقال" },
            new() { Id = "4", Name = "انگور" },
            new() { Id = "5", Name = "توت فرنگی" }
        };
    }

    // Event handlers
    public async Task OnRTLMultipleSelectionChanged(List<string> selectedIds)
    {
        rtlMultipleSelectedOptions = selectedIds;
        rtlMultipleSelectedText = GetSelectedText(selectedIds, persianCities);
        await InvokeAsync(StateHasChanged);
    }

    public async Task OnLTRMultipleSelectionChanged(List<string> selectedIds)
    {
        ltrMultipleSelectedOptions = selectedIds;
        ltrMultipleSelectedText = GetSelectedText(selectedIds, englishCities);
        await InvokeAsync(StateHasChanged);
    }

    public async Task OnComplexSelectionChanged(List<string> selectedIds)
    {
        complexSelectedOptions = selectedIds;
        complexSelectedText = GetSelectedText(selectedIds, cities);
        await InvokeAsync(StateHasChanged);
    }

    public async Task OnDirectMultipleChanged(List<string> selectedIds)
    {
        directMultipleSelected = selectedIds;
        directMultipleText = GetSelectedText(selectedIds, fruits);
        await InvokeAsync(StateHasChanged);
    }

    private string GetSelectedText(List<string> selectedIds, List<City> dataSource)
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

    private string GetSelectedText(List<string> selectedIds, List<Fruit> dataSource)
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

    public class City
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
    }

    public class Fruit
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
    }
}
