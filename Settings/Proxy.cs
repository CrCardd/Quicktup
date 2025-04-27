using Quicktup.Util;

namespace Quicktup.Settings;

public class Proxy(string ConfigName) : Setting(ConfigName)
{
    public override string Execute()
    {
        string? data = Configuration.ReadConfig($"{ConfigurationVariables.Proxy}{ConfigurationVariables.VarExtension}");
        if(data is null) 
            return "Couldn't found proxy configuration variable";

        string[] data_splited = data.Split(':', '@');
        if(data_splited.Length < 3)
            return "Not all parameters was setted on the proxy";

        string user = data_splited[0];  
        string passowrd = Cripto.Decrypt(data_splited[1]);  
        string proxy = data_splited[2];  
        string setting = $"{user}:{passowrd}@{proxy}";

        if(proxy is null)
            return "Could't fount proxy configuration variable";
        Environment.SetEnvironmentVariable("http_proxy", $"http://{setting}", EnvironmentVariableTarget.User);
        Environment.SetEnvironmentVariable("https_proxy", $"https://{setting}", EnvironmentVariableTarget.User);

        return "Succes";
    }

    public override string Message()
    {
        return "Proxy";
    }

    public override string? SetVar()
    {
        string? user;
        string? passowrd;
        string? proxy;

        do
        {
            Console.WriteLine("User: ");
            user = Console.ReadLine();
        } while(user is null || user.Length == 0);
        user = user.Replace("@", ConfigurationVariables.AtSign);
        do
        {
            Console.WriteLine("User's password: ");
            passowrd = Console.ReadLine();
        } while(passowrd is null || passowrd.Length == 0);
        passowrd = passowrd.Replace("@", ConfigurationVariables.AtSign);
        do
        {
            Console.WriteLine("Proxy: ");
            proxy = Console.ReadLine();
        } while(proxy is null || proxy.Length == 0);

        return $"{user}:{Cripto.Encrypt(passowrd)}@{proxy}";
    }
}