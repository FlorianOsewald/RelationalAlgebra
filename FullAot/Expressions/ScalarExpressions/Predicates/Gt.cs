using System.Numerics;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.ScalarExpressions.Predicates;

public class Gt<T>(
    ScalarExpression<T> left, 
    ScalarExpression<T> right, 
    bool allowEquality) 
    : Predicate 
    where T : struct, IComparisonOperators<T, T, bool>
{
    private ScalarExpression<T> Left { get;  } = left;
    private ScalarExpression<T> Right { get; } = right;
    private bool AllowEquality { get;  } = allowEquality;

    public override bool Evaluate(Tuple context)
    {
        if (AllowEquality)
        {
            return Left.Evaluate(context) >= Right.Evaluate(context);
        }
        return Left.Evaluate(context) > Right.Evaluate(context);
    }
}