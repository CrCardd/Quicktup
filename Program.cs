using Quicktup.Settings;
using Quicktup.Util;
using Quicktup.Variables;
using static Quicktup.Variables.VariableType;

namespace Quicktup;

public static class Setup
{
    public static Dictionary<string, Setting> settings = 
    new()
    {
        [ConfigurationVariables.Wallpaper] = new Wallpaper(ConfigurationVariables.Wallpaper, [new("Path", STRING)]),
        [ConfigurationVariables.Proxy] = new Proxy(ConfigurationVariables.Proxy, [new("Host", STRING), new("User", STRING), new("Password", STRING)]),
        [ConfigurationVariables.MapUnit] = new MapUnit(ConfigurationVariables.MapUnit, [new("Letter", STRING), new("Path", STRING)]),
        [ConfigurationVariables.Win11RightClick] = new Win11RightClick(ConfigurationVariables.Win11RightClick, []),
    };
}

public class Program
{
    public static void Main(string[] args)
    {
        Configuration.ConfigureApplication();

        Setting? setting;
        if(Setup.settings.TryGetValue(ConfigurationVariables.Wallpaper, out setting)){
            setting.Run();
        }
        if(Setup.settings.TryGetValue(ConfigurationVariables.Proxy, out setting)){
            setting.Run();
        }
        if(Setup.settings.TryGetValue(ConfigurationVariables.MapUnit, out setting)){
            setting.Run();
        }
        if(Setup.settings.TryGetValue(ConfigurationVariables.Win11RightClick, out setting)){
            setting.Run();
        }
    }
}