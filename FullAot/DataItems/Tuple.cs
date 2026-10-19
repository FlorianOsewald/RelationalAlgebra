namespace FullAot.DataItems;

public class Tuple(object?[] values)
{
    public object?[] Values { get; } = values;
    public object? this[int index] => Values[index];
    public int Length => Values.Length;
    
    public override bool Equals(object? obj) =>
        obj is Tuple other && Values.AsSpan().SequenceEqual(other.Values);

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var v in Values)
        {
            hash.Add(v);
        }
        return hash.ToHashCode();
    }
}