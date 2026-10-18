using FullAot.DataItems;
namespace FullAot.Expressions.ScalarExpressions;

//ScalarExpressions sind das Gegenstueck zu RelationalExpressions. 
//Sie bekommen als Kontext einen Tupel und liefern einen einzelnen Wert (int, string, bool etc)
public abstract class ScalarExpression<T>
{
    public abstract T Evaluate(DataItems.Tuple context);
    
    public static implicit operator ScalarExpression<T>(T value) => new Literal<T>(value);
}