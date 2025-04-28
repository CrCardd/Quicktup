using Quicktup.Util;
using System.Diagnostics;

namespace Quicktup.Settings;

public class MapUnit(string ConfigName) : Setting(ConfigName)
{
    public override string Execute()
    {
        string? networkPath = Configuration.ReadConfig($"{ConfigurationVariables.MapUnit}{ConfigurationVariables.VarExtension}");
        if(networkPath is null)
            return "Couldn't find networkPath configuration variable";
            
        string letter = "S";

        Process process = new();
        process.StartInfo.FileName = "powershell.exe";
        process.StartInfo.Arguments = $"New-PSDrive -Name \"{letter}\" -Root {networkPath} -Persist -PSProvider \"FileSystem\" -Scope \"Global\"";
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden; 
        process.StartInfo.CreateNoWindow = true;

        process.Start();
        process.WaitForExit();   

        if(process.ExitCode != 0)
            return "Network path is not valid";
        return "Success";
    }

    public override string Message()
    {
        return "New network unit";
    }

    public override string? SetVar()
    {
        string? networkPath;
        do
        {
            Console.WriteLine("Newtork unit path: ");
            networkPath = Console.ReadLine();
        } while(networkPath is null || networkPath.Length == 0);
        return networkPath;
    }
}