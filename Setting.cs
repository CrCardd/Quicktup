public abstract class Setting
{
    public bool Active {get; set;} = true;
    public abstract string Execute();
    public abstract string StartMessage();

    public void Run()
    {
        if(Active)
        {
            Console.Write(StartMessage() + " - " + Execute());
        }
    }
}