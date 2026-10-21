using System.Numerics;
using FullAot.DataItems;
using FullAot.Util;
using Tuple = FullAot.DataItems.Tuple;
using static FullAot.Util.DataTypeExtensions;

namespace FullAot.Expressions.ScalarExpressions;

//Binary Ops repraesentieren meinen Ansatz sowas wie \pi_{A<-B+C} moeglichst generisch zu modellieren
public class BinaryOp(Op @operator, ScalarExpression left, ScalarExpression right) : ScalarExpression
{
    private ScalarExpression _right = right;
    private ScalarExpression _left = left;
    private Op Operator { get; } = @operator;
    private ScalarExpression Left { get; set; } = left;
    private ScalarExpression Right { get; set; } = right;

    public override DataType ResultType { get; set; }

    public override object? Evaluate(Tuple context)
    {
        var left = Left.Evaluate(context);
        var right = Right.Evaluate(context);
        switch (ResultType)
        {
            case DataType.Int:
            {
                return Apply<int>((int)left, (int)right);
            }
            case DataType.Double:
            {
                return Apply<double>((double)left, (double)right);
            }

            case DataType.Long:
            {
                return Apply<long>((long)left, (long)right);
            }
        }
        throw  new InvalidOperationException($"Operator {Operator} not defined for {Left.ResultType} and {Right.ResultType}");
    }

    public override void Bind(Schema schema)
    {
        Left.Bind(schema);
        Right.Bind(schema);

        ResultType = TypeCoalescence.Coalesce(ref _left, ref _right);
    }

    private object Apply<T>(T a, T b) where T : INumber<T>
    {
        switch(Operator){
            case Op.Add:
                return a + b;
            case Op.Sub:
                return a - b;
            case Op.Mul:
                return a * b;
            case Op.Div:
                return a / b;
        }

        throw new InvalidOperationException($"Operation {Operator} not supported");
    }
}

public enum Op
{
    Add,
    Sub,
    Mul,
    Div
}