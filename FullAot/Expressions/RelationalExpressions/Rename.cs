using FullAot.DataItems;

namespace FullAot.Expressions.RelationalExpressions;

public class Rename(RelationalExpression input, IReadOnlyList<(string From, string To)> map) : UnaryExpression(input)
{
    public IReadOnlyList<(string From, string To)> Map { get; set; } = map;

    public override Relation Evaluate()
    {
        //evaluate child, then return a relation that contains the renamed columns
        var rel = Input.Evaluate();
        //Wir müssen hier eigentlich 'nur' das schema aendern. 
        var oldSchema = rel.Schema;
        var newCols = new List<Column>();
        foreach (var col in oldSchema.Columns)
        {
            var renameTuple = map.FirstOrDefault(x => x.From == col.Name);
            if (renameTuple == default)
            {
                newCols.Add(col);
            }
            else
            {
                newCols.Add(new Column(renameTuple.To, col.Type));
            }
        }

        var newSchema = new Schema(newCols);
        //rel.Schema = newSchema;
        //Ich habe mich entschieden, dass eigentlich alles immutable sein sollte, also kein lazy weg mehr
        var retRel = new Relation(newSchema, rel.Tuples);
        return retRel;
    }
}