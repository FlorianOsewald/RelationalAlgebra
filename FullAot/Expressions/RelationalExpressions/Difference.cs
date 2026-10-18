using FullAot.DataItems;

namespace FullAot.Expressions.RelationalExpressions;

public class Difference(RelationalExpression left, RelationalExpression right) : BinaryExpression(left, right)
{
    public override Relation Evaluate()
    {
        //evaluate both children, then return a relation that only consists of tuples that are in left but not right
        throw new NotImplementedException();
    }
}