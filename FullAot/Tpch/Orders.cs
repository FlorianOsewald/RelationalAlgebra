using FullAot.DataItems;
using FullAot.Util;

namespace FullAot.Tpch;

public static class Orders
{
    public static Relation Load()
    {
        //Columns erstellen
        var cols = new List<Column>()
        {
            new ("o_orderkey", DataType.Long),
            new ("o_custkey", DataType.Long),
            new ("o_orderstatus", DataType.String),
            new ("o_totalprice", DataType.Double),
            new ("o_orderdate", DataType.DateTime),
            new ("o_orderpriority", DataType.String),
            new ("o_clerk", DataType.String),
            new ("o_shippriority", DataType.Int),
            new ("o_comment", DataType.String)
        };
        
        //Via static helper: File laden und Tuple erstellen und in relation inserten
        var relation = TpchLoader.Load("orders.tbl", new Schema(cols));
        //Relation zurückgeben.
        return relation;
    }
}