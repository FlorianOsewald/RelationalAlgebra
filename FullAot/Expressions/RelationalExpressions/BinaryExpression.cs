using FullAot.DataItems;

namespace FullAot.Expressions.RelationalExpressions;

public abstract class BinaryExpression(RelationalExpression left, RelationalExpression right) : RelationalExpression
{
    private RelationalExpression Left { get;  } = left;
    private RelationalExpression Right { get;  } = right;
}