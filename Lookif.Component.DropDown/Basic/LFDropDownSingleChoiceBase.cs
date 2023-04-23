namespace Lookif.Component.DropDown.Basic;

public class LFDropDownSingleChoiceBase  : DropDownBase
{
    public LFDropDownSingleChoiceBase()
    {
        Multiple = false;
    }
    public override async void SelectOption(string key, bool status)
    {
        if (SanitizedRecords is { Count: < 1 })
            return;
        SanitizedRecords.ForEach(x => x.Status = false);
          base.SelectOption(key, status);
    }
}
