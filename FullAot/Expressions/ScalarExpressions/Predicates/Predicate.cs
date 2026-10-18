namespace FullAot.Expressions.ScalarExpressions.Predicates;

//Predicates sind hard-typed ScalarExpressions auf Bool
//Sie bekommen ihren eigenen Namespace damit es uebersichtlicher ist
public abstract class Predicate : ScalarExpression<bool>
{
}