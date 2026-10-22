using FullAot.DataItems;
using FullAot.Expressions.ScalarExpressions.Predicates;

namespace FullAot.Expressions.RelationalExpressions;

public class Selection : UnaryExpression
{
    public Selection(RelationalExpression input, Predicate condition) : base(input)
    {
        Condition = condition;
        OutputSchema = input.OutputSchema;
        Condition.Bind(input.OutputSchema);
    }

    private Predicate Condition { get; }

    public override Schema OutputSchema { get; }

    public override Relation Evaluate()
    {
        //evaluate child, then return a relation that contains all tuples where the Condition evaluates to true
        var relation = Input.Evaluate();
        var retRelation = new Relation(OutputSchema); //On Selection Schema stays the same compared to the input
       // Condition.Bind(relation.Schema); //new: Bind the condition to the current schema before evaluate
        foreach (var tuple in relation)
        {
            if ((bool)Condition.Evaluate(tuple))
            {
                retRelation.Insert(tuple);
            }
        }
        return retRelation;
    }
}