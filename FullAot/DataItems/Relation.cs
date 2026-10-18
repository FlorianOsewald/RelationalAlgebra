namespace FullAot.DataItems;

public class Relation(Schema schema)
{
    public Schema Schema { get; } = schema;

    private readonly List<Tuple> _tuples = new();

    public void Insert(params object?[] values)
    {
        Validate(values);
        _tuples.Add(new Tuple(values));
    }

    public object? Get(Tuple t, string column)
    {
        return t[Schema.IndexOf(column)];
    }
    
    private void Validate(object?[] values)
    {
        if (values.Length != Schema.Count)
            throw new ArgumentException("Falsche Anzahl an Werten.");

        for (int i = 0; i < values.Length; i++)
        {
            var col = Schema[i];
            if (values[i] is null)
            {
                throw new ArgumentException($"{col.Name} darf nicht NULL sein.");
            }
            if (!TypeMatches(col.Type, values[i]!))
                throw new ArgumentException($"Falscher Typ für {col.Name}.");
        }
    }

    private static bool TypeMatches(DataType type, object value)
    {
        switch (type)
        {
            case DataType.Bool:
            {
                return value is bool;
            }
            case DataType.String:
            {
                return value is string;
            }
            case DataType.Long:
            {
                return value is long;
            }
            case DataType.DateTime:
            {
                return value is DateTime;
            }
            case DataType.Double:
            {
                return value is double;
            }
            case DataType.Int:
            {
                return value is int;
            }
        }
        return false;
    }
}