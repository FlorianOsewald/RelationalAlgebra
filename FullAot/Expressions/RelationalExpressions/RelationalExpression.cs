using FullAot.DataItems;

namespace FullAot.Expressions.RelationalExpressions;

//RelationalExpression sind quasi 'Brot und Butter'
//Das sind alles Expressions welche als Kontext eine Relation (oder Relationen) erhalten und eine Relation liefern
public abstract class RelationalExpression
{
    public abstract Relation Evaluate();
}