using System.Numerics;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.ScalarExpressions.Predicates;

public class Eq<T>(
    ScalarExpression<T> left, 
    ScalarExpression<T> right) 
    : Predicate
    where T : IComparable<T>, IEquatable<T>
{
    private ScalarExpression<T> Left { get; } = left;
    private ScalarExpression<T> Right {get; } = right;

    public override bool Evaluate(Tuple context)
    {
        return Left.Evaluate(context).Equals(Right.Evaluate(context));
    }
}