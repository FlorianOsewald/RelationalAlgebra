using FullAot.DataItems;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.ScalarExpressions.Predicates;

public class Not(Predicate @in) : Predicate
{
    private Predicate In { get; } = @in;


    public override object? Evaluate(Tuple context)
    {
        return !(bool)In.Evaluate(context);
    }

    public override void Bind(Schema schema)
    {
        In.Bind(schema);
    }
}