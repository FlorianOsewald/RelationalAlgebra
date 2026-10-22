namespace FullAot.DataItems;

public class Schema
{
    private readonly Column[] _columns;
    private readonly Dictionary<string, int> _index;
    private readonly Column[] _normalizedColumns;

    public Schema(IEnumerable<Column> columns)
    {
        _columns = columns.ToArray();
        _index = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        
        for (int i = 0; i < _columns.Length; i++)
        {
            if (!_index.TryAdd(_columns[i].Name, i))
                throw new ArgumentException($"Doppelter Spaltenname (manueller Fehler?!): {_columns[i].Name}");
        }

        _normalizedColumns = _columns.OrderBy(x => x.Name).ToArray();
    }
    public int Count => _columns.Length;
    public Column this[int index] => _columns[index];
    public IReadOnlyList<Column> Columns => _columns;

    public int IndexOf(string name)
    {
        if(_index.TryGetValue(name, out int index))
            return index;
        
        throw new KeyNotFoundException("Unbekannte Spalte: {name}");
    }

    public bool IsCompatibleWith(Schema other)
    {
        return _normalizedColumns.SequenceEqual(other._normalizedColumns);
    }

    public Schema Join(Schema right)
    {
        var newColumns = new List<Column>(Columns);
        newColumns.AddRange(right.Columns);
        var unique = newColumns.Distinct();
        return new Schema(unique);
    }

    public Schema Combine(Schema right)
    {
        var newColumns = new List<Column>();
        foreach (var column in Columns)
        {
            newColumns.Add(new Column($"l.{column.Name}", column.Type));
        }
        foreach (var column in right.Columns)
        {
            newColumns.Add(new Column($"r.{column.Name}", column.Type));
        }
        return new Schema(newColumns);
    }

    public Schema Intersect(Schema right)
    {
        //this returns a Schema that _only_ has columns which are in both schemas
        //could be useful for NaturalJoin?
        var commonalities = Columns.Intersect(right.Columns);
        return new Schema(commonalities);
    }

    public Schema Except(Schema right)
    {
        //returns a Schema that only has comlumns which are in left but _not_ in right
        var uniqueLeftCols = Columns.Except(right.Columns);
        return new Schema(uniqueLeftCols);
    }

    public bool TryGetUnionMapping(Schema right, out int[] rightToLeft)
    {
        rightToLeft = new int[_columns.Length];
        if (Count != right.Count)
            return false;
        for (int i = 0; i < Count; i++)
        {
            var column = this[i];
            try
            {
                rightToLeft[i] = right.IndexOf(column.Name);
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        return true;
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is Schema))
            return false;
        //obj is schema 
        var other = (Schema)obj;
        return _columns.SequenceEqual(other._columns);
    }

    public override int GetHashCode()
    {
        var hash = new HashCode();
        foreach (var column in _columns)
        {
            hash.Add(column.Name);
            hash.Add(column.Type);
        }
        return hash.ToHashCode();
    }
}