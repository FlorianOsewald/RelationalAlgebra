using FullAot.DataItems;

namespace FullAot.Expressions.RelationalExpressions;

public class Division(RelationalExpression left, RelationalExpression right) : BinaryExpression(left, right)
{
    public override Relation Evaluate()
    {
        //evaluate both children, then return a relation that only consists of tuples from left that contain all columns from right
        throw new NotImplementedException();
    }
}