using Microsoft.Extensions.Configuration;

namespace Quicktup;

public static class Configuration
{
    private static IConfigurationRoot? Config;

    public static string? ReadConfig(string key) => Config?[key];
            
    public static void ConfigureApplication()
    {
        try{
            Config = new ConfigurationBuilder()
            .AddIniFile($"{Directory.GetCurrentDirectory()}\\config.cfg", optional: false, reloadOnChange: false)
            .Build();
        }catch{}

        // Console.CancelKeyPress += (sender, e) =>
        // {
        //     string path = Directory.GetCurrentDirectory() + "\\wallpaper.png";

        //     if (File.Exists(path))
        //         File.Delete(path);

        //     Environment.Exit(0);
        // };
    }
    public static void ConfigureSettings()
    {   
        List<string> settingsConfig = [];
        foreach (var setting in Setup.settings.Values)
        {
            bool option = Ask(setting.Message());
            Console.WriteLine(setting.Message() + " -> " + option.ToString());
            settingsConfig.Add(setting.ConfigName + "=" + option.ToString());
            if(option)
            {
                string var = setting.SetVar();
                if(!var.Equals(""))
                    settingsConfig.Add(setting.ConfigName + "_var=" + var);
            }
        }
        File.WriteAllLines("config.cfg", settingsConfig);
    }


    static bool Ask(string message)
    {
        string[] options = ["Yes", "No"];
        int index = 0;
        ConsoleKey key;

        int crrLine = Console.CursorTop;
        do
        {
            Console.SetCursorPosition(0, crrLine);
            Console.WriteLine($"{message}        ");

            for (int i = 0; i < options.Length; i++)
            {
                if (i == index)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"> {options[i]} ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write($"  {options[i]} ");
                }
            }

            Console.Write("      ");

            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.LeftArrow && index > 0)
                index--;
            else if (key == ConsoleKey.RightArrow && index < options.Length-1)
                index++;

        } while (key != ConsoleKey.Enter);
        
        Console.SetCursorPosition(0, crrLine);
        return options[index].Equals("Yes");
    }
}