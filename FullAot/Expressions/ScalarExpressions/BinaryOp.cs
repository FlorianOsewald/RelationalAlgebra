using System.Numerics;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Expressions.ScalarExpressions;

//Binary Ops repraesentieren meinen Ansatz sowas wie \pi_{A<-B+C} moeglichst generisch zu modellieren
public class BinaryOp<T>(
    Op @operator,
    ScalarExpression<T> left,
    ScalarExpression<T> right)
    : ScalarExpression<T>
    where T : INumber<T>
{
    private Op Operator { get; } = @operator;
    private ScalarExpression<T> Left { get; } = left;
    private ScalarExpression<T> Right { get; } = right;

    public override T Evaluate(Tuple context)
    {
        switch (Operator)
        {
            case Op.Add:
            {
                return Left.Evaluate(context) + Right.Evaluate(context);
            }
            case Op.Sub:
            {
                return Left.Evaluate(context) - Right.Evaluate(context);
            }
            case Op.Mul:
            {
                return Left.Evaluate(context) * Right.Evaluate(context);
            }
            case Op.Div:
            {
                return Left.Evaluate(context) / Right.Evaluate(context);
            }
        }
        return default(T);
    }
}

public enum Op
{
    Add,
    Sub,
    Mul,
    Div
}