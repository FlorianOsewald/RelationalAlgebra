using FullAot.DataItems;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.ScalarExpressions;

//ColumnReferences sind einfache verweise auf eine spezielle Column, welche über den Namen aufgeloest wird
public class ColumnReference(string name) : ScalarExpression
{
    private string Name { get; } = name;
    private int Index { get; set; } = -1;

    public override DataType ResultType { get; set; }
    public override object? Evaluate(Tuple context)
    {
        if(Index >= 0)
            return context[Index];
        throw new IndexOutOfRangeException("Index out of range, bind the Reference before execution.");
    }

    public override void Bind(Schema schema)
    {
        Index = schema.IndexOf(Name);
        ResultType = schema[Index].Type;
    }
}