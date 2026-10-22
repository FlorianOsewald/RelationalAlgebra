using FullAot.DataItems;

namespace FullAot.Expressions.RelationalExpressions;

//This is the leaf-node that contains a real relation to kick off the chain
public class RelationReference(Relation rel) : RelationalExpression
{
    private Relation Relation { get; } = rel;
    public override Schema OutputSchema => Relation.Schema;

    public override Relation Evaluate()
    {
        return Relation;
    }
}