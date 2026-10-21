using System.Numerics;
using FullAot.DataItems;
using FullAot.Expressions.ScalarExpressions;
using FullAot.Expressions.ScalarExpressions.Predicates;

namespace FullAot.FluentExtension;

//Beim schreiben der Beispiele habe ich gemerkt, was ein PITA das Schreiben der Queries werden wird. 
//Daher versuch' ich hier den Fluent-Api Style von Linq nachzumachen. 
public static class ScExpExtensions
{
    public static Predicate Gt(this ScalarExpression l, ScalarExpression r)
        => new Gt(l, r, false);
    public static Predicate Geq(this ScalarExpression l, ScalarExpression r)
        => new Gt(l, r, true);
    public static Predicate Lt(this ScalarExpression l, ScalarExpression r)
        => new Lt(l, r, false);
    public static Predicate Leq(this ScalarExpression l, ScalarExpression r)
        => new Lt(l, r, true);
    public static Predicate Eq(this ScalarExpression l, ScalarExpression r)
        => new Eq(l, r);
    
    public static Literal Lit(int value) => new(value, DataType.Int);
    public static Literal Lit(double value) => new(value, DataType.Double);
    public static Literal Lit(long value) => new(value, DataType.Long);
    public static Literal Lit(bool value) => new(value, DataType.Bool);
    public static Literal Lit(string value) => new(value, DataType.String);
    public static Literal Lit(DateTime value) => new(value, DataType.DateTime);

    public static Predicate Not(this Predicate input)
        => new Not(input);

    public static Predicate And(this Predicate l, Predicate r)
        => new And(l, r);
    
    public static Predicate Or(this Predicate l, Predicate r) 
        => new Or(l, r);
}