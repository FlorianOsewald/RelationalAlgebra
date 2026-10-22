using FullAot.DataItems;
using Spectre.Console;

namespace FullAot.Util;

public static class RelationPrinter
{
    public static void Print(this Relation relation, int maxWidth = 30)
    {
        var table = new Table().Border(TableBorder.Rounded);

        foreach (var col in relation.Schema.Columns)
            table.AddColumn(Markup.Escape($"{col.Name} ({col.Type})"));

        foreach (var tuple in relation)
        {
            table.AddRow(tuple.Values.Select(v => Markup.Escape(Format(v, maxWidth))).ToArray());
        }

        AnsiConsole.Write(table);
    }
    
    private static string Format(object? v, int maxWidth)
    {
        var s = v switch
        {
            null => "NULL",
            DateTime d => d.ToString("dd.MM.yyyy HH:mm:ss"),
            _ => v.ToString() ?? ""
        };
        return s.Length > maxWidth ? s[..(maxWidth - 1)] + "…" : s;
    }
}