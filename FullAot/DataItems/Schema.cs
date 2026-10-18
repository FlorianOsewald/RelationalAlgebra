namespace FullAot.DataItems;

public class Schema
{
    private readonly Column[] _columns;
    private readonly Dictionary<string, int> _index;

    public Schema(IEnumerable<Column> columns)
    {
        _columns = columns.ToArray();
        _index = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        
        for (int i = 0; i < _columns.Length; i++)
        {
            if (!_index.TryAdd(_columns[i].Name, i))
                throw new ArgumentException($"Doppelter Spaltenname (manueller Fehler?!): {_columns[i].Name}");
        }
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

}