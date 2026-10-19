using FullAot.DataItems;

namespace FullAot.Expressions.RelationalExpressions;

public abstract class BinaryExpression(RelationalExpression left, RelationalExpression right) : RelationalExpression
{
    protected RelationalExpression Left { get;  } = left;
    protected RelationalExpression Right { get;  } = right;
}