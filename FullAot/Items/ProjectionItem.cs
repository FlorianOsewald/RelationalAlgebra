using FullAot.Expressions.ScalarExpressions;

namespace FullAot.Items;

public class ProjectionItem(ScalarExpression exp, string outputName)
{
    public ScalarExpression Expr { get; } = exp;
    public string OutputName { get; } = outputName;
    //auch hier benutze ich ein paar fluent-style static operators so dass das query schreiben etwas ansehnlicher wird
    //wenn nur ein string gefunden wird, dann ist es immer eine Uebernahme einer bestehenden Column
    public static implicit operator ProjectionItem(string column) =>
        new(new ColumnReference(column), column);

    //Wenn wir ein Tuple vorfinden, dann ist das ein Rename
    public static implicit operator ProjectionItem((string column, string alias) pair) =>
        new(new ColumnReference(pair.column), pair.alias);

    //Wenn wir eine BinaryOp finden, dann ist das eine computed Column + output name
    public static implicit operator ProjectionItem((BinaryOp expr, string alias) pair) =>
        new(pair.expr, pair.alias);
}