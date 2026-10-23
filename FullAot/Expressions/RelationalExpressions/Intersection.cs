using FullAot.DataItems;
using FullAot.Util;

namespace FullAot.Expressions.RelationalExpressions;

public class Intersection : BinaryExpression
{
    public override Schema OutputSchema { get; }

    public Intersection(RelationalExpression left, RelationalExpression right) : base(left, right)
    {
        OutputSchema = left.OutputSchema;
    }
    public override Relation Evaluate()
    {
        //evaluate both children, then return a relation that only consists of tuples that are in left and in right
        var left = Left.Evaluate();
        var right = Right.Evaluate();
        
        if (!left.Schema.IsCompatibleWith(right.Schema))
        {
            throw new ArgumentException("Schemas do not match, Difference cannot be computed.");
        }

        var retRelation = new Relation(OutputSchema);
        
        //Garantiere dass beide Tuples immer gleich aufgebaut sind mit der neuen Extension-method
        var rightTuplesAligned = right.ReorderToMatch(left);
        foreach (var tuple in left)
        {  
            if (rightTuplesAligned.Contains(tuple))
            {
                retRelation.Insert(tuple);
            }
        }
        
        return retRelation;
    }
}