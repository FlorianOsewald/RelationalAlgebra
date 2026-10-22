using FullAot.DataItems;
using FullAot.Items;

namespace FullAot.Expressions.RelationalExpressions;

public class Rename : UnaryExpression
{
    public Rename(RelationalExpression input, IReadOnlyList<RenameItem> map) : base(input)
    {
        Map = map;
        var oldSchema = input.OutputSchema;
        var newCols = new List<Column>();
        foreach (var col in oldSchema.Columns)
        {
            var renameObj = Map.FirstOrDefault(x => x.InName == col.Name);
            if (renameObj == default)
            {
                newCols.Add(col);
            }
            else
            {
                newCols.Add(new Column(renameObj.OutName, col.Type));
            }
        }
        OutputSchema = new Schema(newCols);
    }

    public IReadOnlyList<RenameItem> Map { get; }

    public override Schema OutputSchema { get; }

    public override Relation Evaluate()
    {
        //evaluate child, then return a relation that contains the renamed columns
        var rel = Input.Evaluate();
        //code wurde nochmal einfacher hier, da wir das fertige Schema bereits kennen dank der implementation von OutputSchema
        var retRel = new Relation(OutputSchema, rel.Tuples);
        return retRel;
    }
}