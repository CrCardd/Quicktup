namespace Quicktup;

public struct SettingKey
{
    public SettingKey(string key, Setting setting)
    {
        this.Key = key;
        this.Setting = setting;
    }
    public string Key;
    public Setting Setting;

    public readonly void Run(){Setting.Run();}
}

public static class Setup
{
    public static Dictionary<string, Setting> settings = new(){
        ["Win11"] = new Win11RightClick()
    };
}

public class Program
{
    public static void Main(string[] args)
    {
        Setting? setting;
        if(Setup.settings.TryGetValue("Win11", out setting)){
            setting.Run();
        }
    }
}