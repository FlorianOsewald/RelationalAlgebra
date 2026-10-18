using FullAot.DataItems;
using FullAot.Items;

namespace FullAot.Expressions.RelationalExpressions;

public class Projection(RelationalExpression input, IReadOnlyList<ProjectionItem> items) : UnaryExpression(input)
{
    IReadOnlyList<ProjectionItem> Items { get; } = items;

    public override Relation Evaluate()
    {
        //evaluate child, then return a relation that only contains the columns referenced in Items
        throw new NotImplementedException();
    }
}