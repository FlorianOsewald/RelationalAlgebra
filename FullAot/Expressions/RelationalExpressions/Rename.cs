using FullAot.DataItems;

namespace FullAot.Expressions.RelationalExpressions;

public class Rename(RelationalExpression input, IReadOnlyList<(string From, string To)> map) : UnaryExpression(input)
{
    public IReadOnlyList<(string From, string To)> Map { get; set; } = map;

    public override Relation Evaluate()
    {
        //evaluate child, then return a relation that contains the renamed columns
        throw new NotImplementedException();
    }
}