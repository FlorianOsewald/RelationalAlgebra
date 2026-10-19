using FullAot.DataItems;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.RelationalExpressions;

public class Union(RelationalExpression left, RelationalExpression right) : BinaryExpression(left, right)
{
    public override Relation Evaluate()
    {
        //Evaluate both children, then return a relation that contains all tuples of both children
        //Hier muss drauf geachtet werden, dass die Reihenfolge innerhalb des Tuples mit dem Schema der Relation uebereinstimmt
        //das heißt, wir koennen zwar ohne wenn und aber alles von left nehmen, bei right muessen wir aber gucken. 
        //bzw. ich weiß gar nicht wie das unter der haube ablaeuft, wenn wir auf Equality vom Schema pruefen, garantiert das eine homogene reihenfolge?
        //alles klar, also die reihenfolge ist nicht gut, dafuer muss ich mir noch was ueberlegen
        //SequenceEqual reicht aber zumindest fuer das erste aus. 
        var left = Left.Evaluate();
        var right = Right.Evaluate();
        //check hier ob die Schemas kompatible sind
        if (!left.Schema.IsCompatibleWith(right.Schema))
        {
            throw new InvalidOperationException("schema Left and schema Right are not compatible for Union");
        }
        var retRel = new Relation(left.Schema, left.Tuples);

        if (retRel.Schema.TryGetUnionMapping(right.Schema, out var mapping))
        {
            //add all tuples from right, but map each value to keep schema consistent
            foreach (var t in right)
            {
                //for each attribute in tuple, we have to get the left index, use that to look up the right index, use that to look up the value and 
                //construct a new tuple with that
                //luckily we have a handy cache of that in form of 'mapping'
                object?[] tupleVars = new object?[mapping.Length];
                for (int i = 0; i < mapping.Length; i++)
                {
                    tupleVars[i] = t[mapping[i]];
                }
                retRel.Insert(new Tuple(tupleVars));
            }
            return retRel;
        }
        else
        {
            throw new InvalidOperationException("Unable to map between Schemas for Union");
        }
    }
}