using FullAot.DataItems;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.ScalarExpressions;

//Literal ist eine Literal-Expression, sowas wie 'WHERE id = 4', dann ist '4' ein Literal
public class Literal(object? value, DataType type) : ScalarExpression
{
    private object? Value { get; } = value;
    public override DataType ResultType { get; set; } = type;

    public override object? Evaluate(Tuple context)
    {
        return Value;
    }

    public override void Bind(Schema schema)
    {
        //nichts zu tun, dieses object bindet sich selbst bei instanziierung basierend auf dem ctor
    }
}