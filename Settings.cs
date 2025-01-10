using Zorro.Settings;

namespace DontSaveToDesktop;

[ContentWarningSetting]
public class FileName : StringSetting, IExposedSetting
{
    public override void ApplyValue() { }

    public SettingCategory GetSettingCategory() => SettingCategory.Mods;

    public string GetDisplayName() => "[DontSaveToDesktop] Video Filename";
    
    protected override string GetDefaultValue() => "content_warning_%HANDLE";
}

[ContentWarningSetting]
public class FilePath : StringSetting, IExposedSetting
{
    public override void ApplyValue() { }

    public SettingCategory GetSettingCategory() => SettingCategory.Mods;

    public string GetDisplayName() => "[DontSaveToDesktop] Video Path";
    
    protected override string GetDefaultValue() => "%USER\\Desktop";
}
