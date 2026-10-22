using System.Collections;

namespace FullAot.DataItems;

public class Relation(Schema schema) : IEnumerable<Tuple>
{
    public Schema Schema { get; set; } = schema;

    private readonly HashSet<Tuple> _tuples = new();

    public Relation(Schema schema, IEnumerable<Tuple> tuples) : this(schema)
    {
        _tuples = [.. tuples]; //Meine IDE (Rider) hat mir das vorgeschlagen, offen gestanden ich kenne die Syntax nicht, sieht aber schick aus.
    }

    public void Insert(object[] values)
    {
        _tuples.Add(new Tuple(values, Schema));
    }
    public void Insert(Tuple values)
    {
        _tuples.Add(values);
    }
    
    public IReadOnlyCollection<Tuple> Tuples => _tuples;
    
    public int Count => _tuples.Count;
    public object? Get(Tuple t, string column)
    {
        return t[Schema.IndexOf(column)];
    }

    public IEnumerator<Tuple> GetEnumerator()
    {
        return  _tuples.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}