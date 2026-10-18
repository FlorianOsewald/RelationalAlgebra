namespace FullAot.DataItems;

public class Tuple(object?[] values)
{
    public object? this[int index] => values[index];
    public int Length => values.Length;
}