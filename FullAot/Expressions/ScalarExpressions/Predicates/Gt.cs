using System.Numerics;
using FullAot.DataItems;
using FullAot.Util;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.ScalarExpressions.Predicates;

public class Gt(ScalarExpression left, ScalarExpression right, bool allowEquality) : Predicate 
{
    private ScalarExpression _right = right;
    private ScalarExpression _left = left;
   // private ScalarExpression Left { get;  } = left;
   
   //private ScalarExpression Right { get; } = right;
    private bool AllowEquality { get;  } = allowEquality;
    
    public override object? Evaluate(Tuple context)
    {
        var l = _left.Evaluate(context);
        var r = _right.Evaluate(context);
        var comp = ((IComparable)l).CompareTo(r);
        if (AllowEquality)
            return comp >= 0;
        return comp > 0;
    }

    public override void Bind(Schema schema)
    {
        _left.Bind(schema);
        _right.Bind(schema);
        ResultType = DataType.Bool;
        TypeCoalescence.Coalesce(ref _left, ref _right);
    }
}