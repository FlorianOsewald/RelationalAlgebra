namespace FullAot.DataItems;

public class Tuple
{
    public Tuple(object[] values)
    {
        Values = values;
    }
    public Tuple(object?[] values, Schema schema)
    {
        var strongValues = new object[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            switch (schema[i].Type)
            {
                case DataType.Bool:
                    strongValues[i] = (bool) values[i];
                    break;
                case DataType.DateTime:
                    strongValues[i] = (DateTime) values[i];
                    break;
                case DataType.Double:
                    strongValues[i] = Convert.ToDouble(values[i]);
                    break;
                case DataType.Int:
                    strongValues[i] = Convert.ToInt32(values[i]);
                    break;
                case DataType.Long:
                    strongValues[i] = Convert.ToInt64(values[i]);
                    break;
                case DataType.String:
                    strongValues[i] = (string) values[i];
                    break;
            }
            Values = strongValues;
        }
    }
    public object?[] Values { get; }
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