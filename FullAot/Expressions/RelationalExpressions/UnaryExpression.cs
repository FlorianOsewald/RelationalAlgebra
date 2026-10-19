using System.Data;
using FullAot.DataItems;

namespace FullAot.Expressions.RelationalExpressions;

public abstract class UnaryExpression(RelationalExpression input) : RelationalExpression
{
    protected RelationalExpression Input { get; } = input;
}