using Quicktup.Settings;
using Quicktup.Util;

namespace Quicktup;

public static class Setup
{
    public static Dictionary<string, Setting> settings = 
    new()
    {
        [ConfigurationVariables.Wallpaper] = new Wallpaper(ConfigurationVariables.Wallpaper),
        [ConfigurationVariables.Proxy] = new Proxy(ConfigurationVariables.Proxy),
        [ConfigurationVariables.MapUnit] = new MapUnit(ConfigurationVariables.MapUnit),
        [ConfigurationVariables.Win11RightClick] = new Win11RightClick(ConfigurationVariables.Win11RightClick),
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