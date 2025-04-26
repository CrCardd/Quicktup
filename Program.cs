namespace Quicktup;

public static class Setup
{
    public static Dictionary<string, Setting> settings = 
    new()
    {
        ["Win11"] = new Win11RightClick("Win11"),
        ["Wllpp"] = new Wallpaper("Wllpp")
    };
}

public class Program
{
    public static void Main(string[] args)
    {
        Configuration.ConfigureApplication();

        Setting? setting;
        if(Setup.settings.TryGetValue("Wllpp", out setting)){
            setting.Run();
        }
        if(Setup.settings.TryGetValue("Win11", out setting)){
            setting.Run();
        }
        Console.ReadLine();
    }
}