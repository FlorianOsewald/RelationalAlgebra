using FullAot.DataItems;
using FullAot.Expressions.ScalarExpressions.Predicates;

namespace FullAot.Expressions.RelationalExpressions;

public class Join(RelationalExpression left, RelationalExpression right, Predicate? on) : BinaryExpression(left, right)
{
    private Predicate? On { get; } = on;

    public override Relation Evaluate()
    {
        //Evaluate both children, then return a relation that contains all attributes referenced in On (or a natural join)
        throw new NotImplementedException();
    }
}