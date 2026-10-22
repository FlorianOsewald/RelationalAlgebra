using FullAot.DataItems;

namespace FullAot.Expressions.RelationalExpressions;

//RelationalExpression sind quasi 'Brot und Butter'
//Das sind alles Expressions welche als Kontext eine Relation (oder Relationen) erhalten und eine Relation liefern
public abstract class RelationalExpression
{
    public abstract Schema OutputSchema { get; }
    public abstract Relation Evaluate();
    
    //kleiner helper den ich hier einfuege, wenn wir eine Relation bekommen, dann wrappen wir den einfach ineine RelationalReference
    public static implicit operator RelationalExpression(Relation relation)
        => new RelationReference(relation);
}