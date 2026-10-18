using FullAot.DataItems;

namespace FullAot.Expressions.RelationalExpressions;

public class Union(RelationalExpression left, RelationalExpression right) : BinaryExpression(left, right)
{
    public override Relation Evaluate()
    {
        //Evaluate both children, then return a relation that contains all tuples of both children
        throw new NotImplementedException();
    }
}