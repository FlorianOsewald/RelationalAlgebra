using FullAot.DataItems;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.ScalarExpressions;

//ColumnReferences sind einfache verweise auf eine spezielle Column, welche über den Namen aufgeloest wird
public class ColumnReference<T>(string name, Schema schema) : ScalarExpression<T>
{
    private string Name { get; } = name;
    private Schema Schema { get; } = schema;
    private readonly int _index = schema.IndexOf(name);
    public override T Evaluate(Tuple context)
    {
        return (T) context[_index];
    }
}