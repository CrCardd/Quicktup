using Quicktup;

public abstract class Setting(string ConfigName)
{
    public bool Active {get; set;} = bool.TryParse(Configuration.ReadConfig(ConfigName), out var val) && val;
    public string ConfigName = ConfigName;
    
    public abstract string Execute();
    public abstract string Message();
    public abstract string? SetVar();

    public void Run()
    {
        if(Active)
        {
            Console.WriteLine("Setting " + Message() + " - " + Execute());
        }
    }
}