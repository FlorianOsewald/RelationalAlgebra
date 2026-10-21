using FullAot.DataItems;
using FullAot.Expressions.ScalarExpressions;

namespace FullAot.Util;

public static class TypeCoalescence
{
    public static DataType Coalesce(ref ScalarExpression left, ref ScalarExpression right)
    {
        var leftType = left.ResultType;
        var rightType = right.ResultType;
        if(leftType == rightType)
            return leftType;
        if (leftType.IsNumeric() && rightType.IsNumeric())
        {
            var common = DataTypeExtensions.Promote(leftType, rightType);
            if (leftType != common)
            {
                left = new Cast(left, common);
            }

            if (rightType != common)
            {
                right = new Cast(right, common);
            }
            return common;
        }
        throw new InvalidOperationException($"Operation not supported for Types {leftType} and {rightType}");
    }
}