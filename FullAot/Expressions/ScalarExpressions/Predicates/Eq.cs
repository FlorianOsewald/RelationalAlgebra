using FullAot.DataItems;
using FullAot.Util;
using Tuple = FullAot.DataItems.Tuple;
using static FullAot.Util.DataTypeExtensions;

namespace FullAot.Expressions.ScalarExpressions.Predicates;

public class Eq(ScalarExpression left, ScalarExpression right) : Predicate
{
    private ScalarExpression _left = left;
    private ScalarExpression _right = right;
    private ScalarExpression Left { get; set; } = left;
    private ScalarExpression Right {get; set; } = right;

    public override object? Evaluate(Tuple context)
    {
        var l = Left.Evaluate(context);
        var r = Right.Evaluate(context);
        return Equals(l, r);
    }

    public override void Bind(Schema schema)
    {
        Left.Bind(schema);
        Right.Bind(schema);
        ResultType = DataType.Bool;
        TypeCoalescence.Coalesce(ref _left, ref _right);
    }
}