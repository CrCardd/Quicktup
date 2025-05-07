using Microsoft.Win32;
using Quicktup.Util;
using Quicktup.Variables;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Quicktup.Settings;

public class Wallpaper(string ConfigName, IEnumerable<VariableInit> Variables) : Setting(ConfigName, Variables)
{
    public override string Execute()
    {
        string? var = Configuration.ReadConfig($"{this.ConfigName}{ConfigurationVariables.VarExtension}");
        if(var is null)
            return "Couldn't find wallpaperPath configuration variable";
        if(!File.Exists(var))
            return "Wallpaper File not found";

        #pragma warning disable
        Registry.SetValue("HKEY_CURRENT_USER\\Control Panel\\Desktop", "Wallpaper", var);
        if(!SystemParametersInfo(20, 0, var, 3))
        {
            return "Change wallpaper was not possible";
        }
        return "Success";
    }
    
    [DllImport("user32.dll", SetLastError = true)]
    public static extern bool SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

    public override string Message()
    {
        return "Desktop Wallpaper";
    }

    public override string? SetVar()
    {
        string path;
        Console.WriteLine("Wallpaper image absolute_path:  ");
        int crrLine = Console.CursorTop;
        do
        {
            path = Console.ReadLine();
            Console.SetCursorPosition(0, crrLine);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, crrLine-1);
        } while(!File.Exists(path) && !Regex.IsMatch(path, ".+(.PNG | .png | .JPG | .jpg)"));
        path = path.Replace("\\", "\\\\");
        
        string destinationPath = $"{ConfigurationVariables.WallpaperPath}\\wallpaper.png";
        if(File.Exists(destinationPath))
            File.Delete(destinationPath);

        File.Copy(path, destinationPath);
        return $"\"{destinationPath}\"";
    }
}
