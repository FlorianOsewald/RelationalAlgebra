using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.ScalarExpressions.Predicates;

public class And(Predicate left, Predicate right) : Predicate
{
    private Predicate Left { get; } = left;
    private Predicate Right { get; } = right;

    public override bool Evaluate(Tuple context)
    {
        return Left.Evaluate(context) && Right.Evaluate(context);
    }
}