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
        switch (_target)
        {
            case DataType.Int:
            {
                return Convert.ToInt32(v);
            }
            case DataType.Double:
            {
                return Convert.ToDouble(v);
            }
            case DataType.Long:
            {
                return Convert.ToInt64(v);
            }
        }

        throw new InvalidOperationException($"Cannot cast to {_target}");
    }

    public override void Bind(Schema schema)
    {
        //nichts zu tun
        input.Bind(schema);
    }
}