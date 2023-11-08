using System;
using System.Reflection.Metadata.Ecma335;

namespace EliTool.ExternSDK;

public enum FromType
{
    TextBox,
    RadioButton,
    ToggleButton,
    Slider,
    CheckButton,
    ToggleSwitch,
    ComboBox,
    DropdownBox
}

public class SettingItem : ICloneable, IEquatable<SettingItem>
{
    public string Name { get; set; }

    public object Value { get; set; }

    public string Description { get; set; }

    public FromType FromType { get; set; }

    public bool IsActivity { get; set; }

    public string Icon { get; set; }

    public object Clone() => 
        new SettingItem() 
        { 
            Name = Name, 
            Value = Value, 
            Description = Description, 
            FromType = FromType, 
            IsActivity = IsActivity ,
            Icon = Icon
        };

    public bool Equals(SettingItem? other) => 
        other.Name == Name && 
        other.Value == Value && 
        other.Description == Description &&
        other.FromType == FromType &&
        other.IsActivity == IsActivity &&
        other.Icon == Icon;
}
