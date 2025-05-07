namespace Quicktup.Variables;

public enum VariableType
{
    INT, BOOL, STRING
}

public class Variable
{
    public string? Value {get; set;}
    public VariableType Type {get; set;}

    public dynamic? GetValue()
    {
        switch(Type)
        {
            case VariableType.BOOL:
            {
                if(bool.TryParse(Value, out bool ret))
                {
                    return ret;
                }
                return null;
            }
            case VariableType.INT:
            {
                if(int.TryParse(Value, out int ret))
                {
                    return ret;
                }
                return null;
            }
            case VariableType.STRING:
            return Value;
            default:
            return null;
        }
    }
}

public struct VariableInit(string Name, VariableType Type)
{
    public string Name = Name;
    public VariableType Type = Type;
}