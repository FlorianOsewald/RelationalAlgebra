using FullAot.DataItems;

namespace FullAot.Expressions.RelationalExpressions;

public class Division : BinaryExpression
{
    public override Schema OutputSchema { get; }

    public Division(RelationalExpression left, RelationalExpression right) : base(left, right)
    {
        OutputSchema = left.OutputSchema.Except(right.OutputSchema);
    }

    public override Relation Evaluate()
    {
        //evaluate both children, then return a relation that only consists of tuples from left that contain all columns from right
        //derived operator: R ÷ S  =  π_X(R) − π_X( (π_X(R) × S) − R ) (X is the set of R's attributes that are not in S)
        throw new NotImplementedException();
    }
}