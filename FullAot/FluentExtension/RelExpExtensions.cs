using FullAot.DataItems;
using FullAot.Expressions.RelationalExpressions;
using FullAot.Expressions.ScalarExpressions;
using FullAot.Expressions.ScalarExpressions.Predicates;
using FullAot.Items;

namespace FullAot.FluentExtension;

public static class RelExpExtensions
{
    public static RelationalExpression DivideWith(this RelationalExpression l, RelationalExpression r)
        => new Division(l, r);
    
    public static RelationalExpression Without(this RelationalExpression l, RelationalExpression r)
        => new Difference(l, r);
    
    public static RelationalExpression Join(this RelationalExpression l, RelationalExpression r, IEnumerable<(string leftCol, string rightCol)> on)
        => new Join(l, r, on);
    public static RelationalExpression Join(this RelationalExpression l, RelationalExpression r, IEnumerable<string> on)
        => new Join(l, r, on);

    public static RelationalExpression NaturalJoin(this RelationalExpression l, RelationalExpression r)
        => new Join(l, r);
    
    public static RelationalExpression X(this RelationalExpression l, RelationalExpression r)
        => new CartesianProduct(l, r);

    public static RelationalExpression Project(this RelationalExpression input, IReadOnlyList<ProjectionItem> items)
        => new Projection(input, items);
    
    public static RelationalExpression Rename(this RelationalExpression input, IReadOnlyList<RenameItem> map)
        => new Rename(input, map);
    
    public static RelationalExpression Where(this RelationalExpression input, Predicate condition) 
        => new Selection(input, condition);
    
    public static RelationalExpression Union(this RelationalExpression l, RelationalExpression r)
        => new Union(l, r);
    
    public static RelationReference Rel(Relation value) => new(value);
}