using FullAot.DataItems;

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
        if (!left.Schema.Equals(right.Schema))
        {
            throw new ArgumentException("Schemas do not match, Difference cannot be computed.");
        }
        //Wir sind noch hier, also vom schema her zumindest alles gut
        var retRelation = new Relation(left.Schema);
        //iterate ueber alle tuple aus left und gucke ob diese in right enthalten sind. Wenn ja, dann continue, wenn nein, dann fuege diesen tuple in retRelation ein
        //update, die tuple sind nun keine liste mehr sondern ein hashset. Damit gibts viele Sachen wie uniqueness und witzigerweise auch Difference quasi geschenkt. 
        foreach (var tuple in left)
        {
            //Soweit ich das korrekt gelesen habe, bekommt man Contains gratis, weil wir IEnumerable implementiert haben auf der Relation
            //Unter der Haube sollte das native die Funktion vom HashSet ansprechen
            if (!right.Contains(tuple))
            {
                retRelation.Insert(tuple);
            }
        }
        
        return retRelation;
    }
}