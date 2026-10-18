using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.ScalarExpressions;

//Literal ist eine Literal-Expression, sowas wie 'WHERE id = 4', dann ist '4' ein Literal
public class Literal<T>(T value) : ScalarExpression<T>
{
    private T Value { get; } = value;
    public override T Evaluate(Tuple context)
    {
        return Value;
    }
}