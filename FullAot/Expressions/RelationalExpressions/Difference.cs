using FullAot.DataItems;
using FullAot.Util;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.RelationalExpressions;

public class Difference : BinaryExpression
{
    public Difference(RelationalExpression left, RelationalExpression right) : base(left, right)
    {
        this.OutputSchema = left.OutputSchema;
    }

    public override Schema OutputSchema { get; }

    public override Relation Evaluate()
    {
        //evaluate both children, then return a relation that only consists of tuples that are in left but not right
        var left = Left.Evaluate();
        var right = Right.Evaluate();
        
        if (!left.Schema.IsCompatibleWith(right.Schema))
        {
            throw new ArgumentException("Schemas do not match, Difference cannot be computed.");
        }
        //Wir sind noch hier, also vom schema her zumindest alles gut
        var retRelation = new Relation(left.Schema);
        
        //Garantiere dass beide Tuples immer gleich aufgebaut sind mit der neuen Extension-method
        var rightTuplesAligned = right.ReorderToMatch(left);
        foreach (var tuple in left)
        {
            //update: Ich habe gelernt, dass HashSet.Contains() hier tatsächlich positions-aware ist
            //Das heißt ein Schema <name, id> und <id, name> sind zwar Compatible _aber_ deren Tuple sind nicht identisch. 
            if (!rightTuplesAligned.Contains(tuple))
            {
                retRelation.Insert(tuple);
            }
        }
        
        return retRelation;
    }
}