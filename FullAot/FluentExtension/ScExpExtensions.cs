using System.Numerics;
using FullAot.Expressions.ScalarExpressions;
using FullAot.Expressions.ScalarExpressions.Predicates;

namespace FullAot.FluentExtension;

//Beim schreiben der Beispiele habe ich gemerkt, was ein PITA das Schreiben der Queries werden wird. 
//Daher versuch' ich hier den Fluent-Api Style von Linq nachzumachen. 
public static class ScExpExtensions
{
    public static Predicate Gt<T>(this ScalarExpression<T> l, ScalarExpression<T> r)
        where T : struct, IComparisonOperators<T, T, bool> => new Gt<T>(l, r, false);
    public static Predicate Geq<T>(this ScalarExpression<T> l, ScalarExpression<T> r)
        where T : struct, IComparisonOperators<T, T, bool> => new Gt<T>(l, r, true);
    public static Predicate Lt<T>(this ScalarExpression<T> l, ScalarExpression<T> r)
        where T : struct, IComparisonOperators<T, T, bool> => new Lt<T>(l, r, false);
    public static Predicate Leq<T>(this ScalarExpression<T> l, ScalarExpression<T> r)
        where T : struct, IComparisonOperators<T, T, bool> => new Lt<T>(l, r, true);
    public static Predicate Eq<T>(this ScalarExpression<T> l, ScalarExpression<T> r)
        where T : IComparable<T>, IEquatable<T> => new Eq<T>(l, r);
    
    public static Literal<T> Lit<T>(T value) => new(value);

    public static Predicate Not(this Predicate input)
        => new Not(input);

    public static Predicate And(this Predicate l, Predicate r)
        => new And(l, r);
    
    public static Predicate Or(this Predicate l, Predicate r) 
        => new Or(l, r);
}