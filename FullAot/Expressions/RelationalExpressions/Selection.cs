using FullAot.DataItems;
using FullAot.Expressions.ScalarExpressions.Predicates;

namespace FullAot.Expressions.RelationalExpressions;

public class Selection(RelationalExpression input, Predicate condition) : UnaryExpression(input)
{
    private Predicate Condition { get; } = condition;

    public override Relation Evaluate()
    {
        //evaluate child, then return a relation that contains all tuples where the Condition evaluates to true
        var relation = Input.Evaluate();
        var retRelation = new Relation(relation.Schema); //On Selection Schema stays the same compared to the input
        foreach (var tuple in relation)
        {
            if (Condition.Evaluate(tuple))
            {
                retRelation.Insert(tuple);
            }
        }
        return retRelation;
    }
}