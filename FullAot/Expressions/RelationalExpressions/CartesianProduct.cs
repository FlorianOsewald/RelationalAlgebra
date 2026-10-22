using FullAot.DataItems;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.RelationalExpressions;

public class CartesianProduct : BinaryExpression
{
    private readonly int[] _rightKeep;
    public override Schema OutputSchema { get; }

    public CartesianProduct(RelationalExpression left, RelationalExpression right) : base(left, right)
    {
        OutputSchema = left.OutputSchema.Join(right.OutputSchema); 
        
        //Erstelle ein schnelles Lookup array welche index von rechts behalten werden sollen nach dem join
        var exceptSchema = Right.OutputSchema.Except(Left.OutputSchema);
        _rightKeep = new int[exceptSchema.Count];
        for (int i = 0; i < exceptSchema.Count; i++)
        {
            _rightKeep[i] = exceptSchema.IndexOf(exceptSchema[i].Name);
        }
    }

    public override Relation Evaluate()
    {
        //Evaluate both children, then return a relation that contains all attributes in combination
        var left = Left.Evaluate();
        var right = Right.Evaluate();
       
        var retRelation = new Relation(OutputSchema);
        foreach (var tupleLeft in left)
        {
            foreach (var tupleRight in right)
            {
                var outTupleValues = new Object[OutputSchema.Count];
                //nehme alles von links
                Array.Copy(tupleLeft.Values, 0, outTupleValues, 0, left.Schema.Count);
                //nehme nur von rechts was neu ist
                for (int i = 0; i < _rightKeep.Length; i++)
                {
                    outTupleValues[i + left.Schema.Count] = tupleRight[_rightKeep[i]];
                }
                //Debug
                string repr = "";
                for (int i = 0; i < outTupleValues.Length; i++)
                {
                    repr += outTupleValues[i].ToString();
                    repr += " ";
                }
                Console.WriteLine(repr);
                //insert den Tuple in retRelation
                retRelation.Insert(new Tuple(outTupleValues));
            }
        }
        return retRelation;
    }
}