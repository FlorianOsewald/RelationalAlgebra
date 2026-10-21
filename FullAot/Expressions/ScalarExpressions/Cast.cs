using FullAot.DataItems;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.ScalarExpressions;

public class Cast(ScalarExpression input, DataType target) : ScalarExpression
{
    private DataType _target = target;

    public override DataType ResultType
    {
        get => _target;
        set => _target = value;
    }

    public override object? Evaluate(Tuple context)
    {
        var v = input.Evaluate(context);
        if (v is null) return null;
        return _target switch
        {
            DataType.Int    => Convert.ToInt32(v),
            DataType.Long   => Convert.ToInt64(v),
            DataType.Double => Convert.ToDouble(v),
            _ => throw new InvalidOperationException($"Cannot cast to {_target}")
        };
    }

    public override void Bind(Schema schema)
    {
        //nichts zu tun
    }
}