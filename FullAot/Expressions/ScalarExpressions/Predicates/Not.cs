using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.ScalarExpressions.Predicates;

public class Not(Predicate @in) : Predicate
{
    private Predicate In { get; } = @in;
    
    public override bool Evaluate(Tuple context)
    {
        return !In.Evaluate(context);
    }
}