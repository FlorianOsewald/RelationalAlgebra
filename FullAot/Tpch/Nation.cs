using FullAot.DataItems;
using FullAot.Util;

namespace FullAot.Tpch;

public static class Nation
{
    public static Relation Load()
    {
        //Columns erstellen
        var cols = new List<Column>()
        {
            new ("n_nationkey", DataType.Int),
            new ("n_name", DataType.String),
            new ("n_regionkey", DataType.Int),
            new ("n_comment", DataType.String)
        };
        
        //Via static helper: File laden und Tuple erstellen und in relation inserten
        var relation = TpchLoader.Load("nation.tbl", new Schema(cols));
        //Relation zurückgeben.
        return relation;
    }
}