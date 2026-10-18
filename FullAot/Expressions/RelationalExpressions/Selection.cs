using FullAot.DataItems;
using FullAot.Expressions.ScalarExpressions.Predicates;

namespace FullAot.Expressions.RelationalExpressions;

public class Selection(RelationalExpression input, Predicate condition) : UnaryExpression(input)
{
    public Predicate Condition { get; set; } = condition;

    public override Relation Evaluate()
    {
        //evaluate child, then return a relation that contains all tuples where the Condition evaluates to true
        throw new NotImplementedException();
    }
}