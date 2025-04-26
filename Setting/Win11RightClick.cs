using System.Diagnostics;
using Microsoft.Win32;

namespace Quicktup;

public class Win11RightClick(string ConfigName) : Setting(ConfigName)
{
    public override string Execute()
    {
        #pragma warning disable
        Registry.SetValue("HKEY_CURRENT_USER\\Software\\Classes\\CLSID\\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\\InprocServer32", "", "");
        var processes = Process.GetProcessesByName("explorer");
        if(!processes.Any())
        {
            return "Windows Explorer was not found. You probably screwed up somewhere else. Big big problem :(";
        }

        foreach(var p in processes)
        {
            p.Kill();
        }

        if(Process.Start(processes[0].MainModule.FileName) == null)
        {
            return "Could not restart Windows Explorer. This console is all that's left...";
        }

        return "Success";
    }

    public override string Message()
    {
        return "Windows 11 right click menu";
    }

    public override string? SetVar() => null;
}