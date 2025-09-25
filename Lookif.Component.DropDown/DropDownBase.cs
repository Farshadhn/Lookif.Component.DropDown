
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel;
using System.Reflection;

namespace Lookif.Component.DropDown;

public class DropDownBase : ComponentBase
{

    public delegate Task Update(List<string> selectedOptions);

    #region ...Function...

    public void ConvertToDropdownContextHolder(IReadOnlyCollection<object> newValue)
    {
        SanitizedRecords = new();
        foreach (var Record in newValue)
        {
            var property = Record.GetType().GetProperty(Value);

            var targetObject = property.GetValue(Record, null);
            if (targetObject is null)
                continue;
            var DropDownValue = targetObject.ToString();

            property = Record.GetType().GetProperty(Key);
            var DropDownKey =  property.GetValue(Record, null).ToString();
            if (!SanitizedRecords.Any(x => x.Key.Equals(DropDownKey)))
                SanitizedRecords.Add(new DropdownContextHolder<string>(DropDownValue, DropDownKey));
        }
        FilteredRecords = SanitizedRecords;
    }

    public void ConvertEnumToDropdownContextHolder()
    {
        if (EnumType == null || !EnumType.IsEnum)
            return;

        SanitizedRecords = new();
        var enumValues = Enum.GetValues(EnumType);
        
        foreach (var enumValue in enumValues)
        {
            var enumName = enumValue.ToString();
            var displayName = GetEnumDisplayName(enumValue);
            var key = ((int)enumValue).ToString();
            
            SanitizedRecords.Add(new DropdownContextHolder<string>(displayName, key));
        }
        FilteredRecords = SanitizedRecords;
    
    }

    private string GetEnumDisplayName(object enumValue)
    {
        if (!UseEnumDisplayNames)
            return enumValue.ToString();

        var field = enumValue.GetType().GetField(enumValue.ToString());
        if (field == null)
            return enumValue.ToString();

        var displayAttribute = field.GetCustomAttribute<DisplayNameAttribute>();
        if (displayAttribute != null)
            return displayAttribute.DisplayName;

        var descriptionAttribute = field.GetCustomAttribute<DescriptionAttribute>();
        if (descriptionAttribute != null)
            return descriptionAttribute.Description;

        return enumValue.ToString();
    }

    public void Bind()
    {
        firstRender = false;

        // Check if using enum
        if (EnumType != null && EnumType.IsEnum)
        {
            ConvertEnumToDropdownContextHolder();
            return;
        }

        if (Records is null or { Count: < 1 })
            return;

        ConvertToDropdownContextHolder(Records);
    }

    private void ShowAlreadySelectedOptions(List<string> selectedOption)
    {
        if (selectedOption is null || !selectedOption.Any())
            return;
        foreach (var item in SanitizedRecords)
        {
            if (selectedOption.Contains(item.Key))
                item.Status = true;
        }
        SelectedRecords = SanitizedRecords.Where(x => x.Status).ToList();
    }




    #endregion

    #region ...Event...
    public DropDownBase()
    {
        Identity = Guid.NewGuid();
    }
    public  void NameChanged(string value)
    {
        FilteredRecords = SanitizedRecords.Where(x => x.Content.Trim().ToLower().Contains(value.Trim().ToLower())).ToList();

    }
     
    protected override void OnAfterRender(bool firstRender)
    {
        if (firstRender)
        {
            Bind();
            objRef = DotNetObjectReference.Create(this);
            if (_jSRuntime is not null)
            {
                _lFDropDownJSInterop = new LFDropDownJSInterop(_jSRuntime);

            }
            ShowAlreadySelectedOptions(SelectedOption);
            StateHasChanged();
        }
    }
   
    public virtual async Task SelectOption(string key, bool status)
    {
        if (SanitizedRecords is { Count: < 1 })
            return;
        SanitizedRecords.FirstOrDefault(x => x.Key == key).Status = status;
        SelectedRecords = SanitizedRecords.Where(x => x.Status).ToList();
        FilteredRecords = SanitizedRecords;
        SelectedOption = SelectedRecords.Select(x => x.Key).ToList();
        await PerformUpdate.Invoke(SelectedOption);

        // For single choice dropdowns, close the dropdown after selection
        if (!Multiple)
        {
            await CloseDropdown();
        }
    }


    [JSInvokable("Toggle")]
    public async Task Toggle()
    {
        if (Disable)
            return;
        this.Show = !this.Show;
        await _lFDropDownJSInterop.SetOrUnsetInstance(objRef, Identity, Show);
        StateHasChanged();
    }

    public async Task CloseDropdown()
    {
        if (Disable)
            return;
        this.Show = false;
        if (_lFDropDownJSInterop != null)
        {
            await _lFDropDownJSInterop.SetOrUnsetInstance(objRef, Identity, false);
        }
        StateHasChanged();
    }

    // Synchronous wrapper for Razor event handlers
    public void SelectOptionSync(string key, bool status)
    {
        _ = Task.Run(async () => await SelectOption(key, status));
    }

    // RTL/LTR Helper methods
    public string GetTextDirection()
    {
        return IsRTL ? "rtl" : "ltr";
    }

    public string GetDirectionClass()
    {
        return IsRTL ? "rtl" : "ltr";
    }

    public string GetArrowPosition()
    {
        return IsRTL ? "left" : "right";
    }

    // Enum Helper methods
    public bool IsUsingEnum()
    {
        return EnumType != null && EnumType.IsEnum;
    }

    public string GetEnumDisplayName<T>(T enumValue) where T : Enum
    {
        if (!UseEnumDisplayNames)
            return enumValue.ToString();

        var field = enumValue.GetType().GetField(enumValue.ToString());
        if (field == null)
            return enumValue.ToString();

        var displayAttribute = field.GetCustomAttribute<DisplayNameAttribute>();
        if (displayAttribute != null)
            return displayAttribute.DisplayName;

        var descriptionAttribute = field.GetCustomAttribute<DescriptionAttribute>();
        if (descriptionAttribute != null)
            return descriptionAttribute.Description;

        return enumValue.ToString();
    }

    public T GetEnumValue<T>(string key) where T : Enum
    {
        if (int.TryParse(key, out int intValue))
        {
            return (T)Enum.ToObject(typeof(T), intValue);
        }
        return default(T);
    }
    #endregion

    #region ...Definition...

    public List<DropdownContextHolder<string>> SanitizedRecords { get; set; } = [];
    public List<DropdownContextHolder<string>> FilteredRecords { get; set; } = [];
    public List<DropdownContextHolder<string>> SelectedRecords { get; set; } = [];
    public bool Show { get; set; } = false;




    #endregion

    #region ...Properties...

    public List<string> returnValue;
    bool firstRender = true;
    public string _inputText;

    public string inputText
    {
        get => _inputText;
        set
        {
            _inputText = value;
            NameChanged(_inputText);
        }
    }

    public readonly Guid Identity;
    public DotNetObjectReference<DropDownBase> objRef { get; set; }
    public LFDropDownJSInterop _lFDropDownJSInterop { get; set; }
    #endregion

    #region ...Injection... 

    [Inject] IJSRuntime _jSRuntime { get; set; }
    #endregion

    #region ...Parameter...

    [Parameter(CaptureUnmatchedValues = true)] public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }
    [Parameter] public IReadOnlyCollection<object> Records { get; set; }

    [Parameter] public string FormName { get; set; }
    [Parameter] public string Key { get; set; }
    [Parameter] public string Value { get; set; }

    [Parameter] public Update PerformUpdate { get; set; }
    [Parameter] public List<string> SelectedOption { get; set; }
    [Parameter] public bool Multiple { get; set; }
    [Parameter] public bool Disable { get; set; } = false;
    [Parameter] public EventCallback<List<string>> ReturnValueChanged { get; set; }
    
    // RTL/LTR Support
    [Parameter] public bool IsRTL { get; set; } = true;
    
    // Enum Support
    [Parameter] public Type? EnumType { get; set; }
    [Parameter] public bool UseEnumDisplayNames { get; set; } = true;
     


    [Parameter]
    public List<string> ReturnValue
    {
        get => returnValue; set
        {
            if (value is not null && !value.Equals(returnValue) && value.Any())
                returnValue = value;
        }
    }



    #endregion


    #region ...IDisposable...
    public void Dispose()
    {
        objRef?.Dispose();
    }
    #endregion
}
