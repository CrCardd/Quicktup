using Quicktup;
using Quicktup.Variables;

public abstract class Setting
{
    public Setting(string ConfigName, IEnumerable<VariableInit> Variables)
    {
        this.ConfigName = ConfigName;
        this.Active = bool.TryParse(Configuration.ReadConfig(ConfigName), out bool val) && val;

        foreach(var v in Variables)
        {
            this.Variables.Add(v.Name, new Variable(){Type = v.Type});
        }
    }

    public bool Active {get; set;}
    public string ConfigName;

    public Dictionary<string, Variable> Variables = new();
    
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