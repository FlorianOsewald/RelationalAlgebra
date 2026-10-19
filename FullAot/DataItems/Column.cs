namespace FullAot.DataItems;

public class Column(string name, DataType type) : IEquatable<Column>
{
    public string Name { get; } = name;
    public DataType Type { get; } = type;

    public bool Equals(Column? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Type == other.Type;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, (int)Type);
    }
}