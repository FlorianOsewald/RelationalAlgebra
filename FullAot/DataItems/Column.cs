namespace FullAot.DataItems;

public class Column(string name, DataType type)
{
    public string Name { get; } = name;
    public DataType Type { get; } = type;
}