using Microsoft.Extensions.Configuration;
using Quicktup.Util;

namespace Quicktup;

public static class Configuration
{
    private static IConfigurationRoot? Config;
    public static string? ReadConfig(string key) => Config?[key];
    public static void ConfigureApplication()
    {
        if(!File.Exists("config.cfg"))
            GenerateConfig();
        Config = new ConfigurationBuilder()
            .AddIniFile($"{Directory.GetCurrentDirectory()}\\config.cfg", optional: false, reloadOnChange: false)
            .Build();

        foreach(Setting setting in Setup.settings.Values)
            ConfigureSetting(setting);
        
        Config = new ConfigurationBuilder()
            .AddIniFile($"{Directory.GetCurrentDirectory()}\\config.cfg", optional: false, reloadOnChange: false)
            .Build();
    }
    private static void GenerateConfig()
    {   
        List<string> config_cfg = [
            $"VarExtension=\"{ConfigurationVariables.VarExtension}\"",
            "AskAll=true"
        ];  
        File.WriteAllLines("config.cfg", config_cfg);
    }
    private static void ConfigureSetting(Setting setting)
    {
        if(ReadConfig(setting.ConfigName) is not null || !(bool.TryParse(ReadConfig("AskAll"), out var val) && val))
            return;
        List<string> lines = [];
        bool option = Ask(setting.Message());
        Console.WriteLine(setting.Message() + " -> " + (option ? "Yes" : "No"));
        lines.Add($"{setting.ConfigName}={option}");
        setting.Active = option;
        if(option)
        {
            string? var = setting.SetVar();
            if(var is not null)
                lines.Add($"{setting.ConfigName}{ConfigurationVariables.VarExtension}={var}");
        }
        File.AppendAllLines("config.cfg", lines);
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