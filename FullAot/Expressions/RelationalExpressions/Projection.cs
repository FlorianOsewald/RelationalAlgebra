using FullAot.DataItems;
using FullAot.Expressions.ScalarExpressions;
using FullAot.Items;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.RelationalExpressions;

public class Projection : UnaryExpression
{
    public Projection(RelationalExpression input, IEnumerable<ProjectionItem> items) : base(input)
    {
        _items = items.ToArray();
        //Output-schema berechnen basierenda auf input und items oder so
        var colsForOutput = new List<Column>();
        foreach (var item in _items)
        {
            item.Expr.Bind(Input.OutputSchema);
            colsForOutput.Add(new Column(item.OutputName, item.Expr.ResultType));
        }
        OutputSchema = new Schema(colsForOutput);
    }

    private readonly ProjectionItem[] _items;

    public override Schema OutputSchema { get; }

    public override Relation Evaluate()
    {
        //evaluate child, then return a relation that only contains the columns referenced in Items
        var result = new Relation(OutputSchema);
        foreach (var t in Input.Evaluate())
        {
            var values = new object?[_items.Length];
            for (int i = 0; i < _items.Length; i++)
                values[i] = _items[i].Expr.Evaluate(t);
            result.Insert(new Tuple(values));
        }
        return result;
    }
}