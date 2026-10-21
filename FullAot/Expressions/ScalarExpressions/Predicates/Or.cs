using FullAot.DataItems;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.ScalarExpressions.Predicates;

public class Or(Predicate left, Predicate right) : Predicate
{
    private Predicate Left { get;  } = left;
    private Predicate Right { get;  } = right;

    public override object? Evaluate(Tuple context)
    {
        return (bool)Left.Evaluate(context) || (bool)Right.Evaluate(context);
    }

    public override void Bind(Schema schema)
    {
        Left.Bind(schema);
        Right.Bind(schema);
    }
}