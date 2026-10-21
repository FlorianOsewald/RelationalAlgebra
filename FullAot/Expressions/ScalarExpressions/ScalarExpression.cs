using FullAot.DataItems;
namespace FullAot.Expressions.ScalarExpressions;

//ScalarExpressions sind das Gegenstueck zu RelationalExpressions. 
//Sie bekommen als Kontext einen Tupel und liefern einen einzelnen Wert (int, string, bool etc)
public abstract class ScalarExpression
{
    public abstract DataType ResultType { get; set; }
    public abstract object? Evaluate(DataItems.Tuple context);
    public abstract void Bind(Schema schema);
    public static implicit operator ScalarExpression(int value) => new Literal(value, DataType.Int);
    public static implicit operator ScalarExpression(long value) => new Literal(value, DataType.Long);
    public static implicit operator ScalarExpression(double value) => new Literal(value, DataType.Double);
    public static implicit operator ScalarExpression(string value) => new Literal(value, DataType.String);
    public static implicit operator ScalarExpression(bool value) => new Literal(value, DataType.Bool);
    public static implicit operator ScalarExpression(DateTime value) => new Literal(value, DataType.DateTime);
}