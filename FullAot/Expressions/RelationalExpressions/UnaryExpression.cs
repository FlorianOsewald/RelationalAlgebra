using System.Data;
using FullAot.DataItems;

namespace FullAot.Expressions.RelationalExpressions;

public abstract class UnaryExpression(RelationalExpression input) : RelationalExpression
{
    private RelationalExpression Input { get; } = input;
}