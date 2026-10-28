using FullAot.DataItems;
using FullAot.Util;

namespace FullAot.Tpch;

public static class Orders
{
    public static readonly string OrderKey = "o_orderkey";
    public static readonly string CustKey = "o_custkey";
    public static readonly string OrderStatus = "o_orderstatus";
    public static readonly string TotalPrice = "o_totalprice";
    public static readonly string OrderDate = "o_orderdate";
    public static readonly string OrderPriority = "o_orderpriority";
    public static readonly string Clerk = "o_clerk";
    public static readonly string ShipPriority = "o_shippriority";
    public static readonly string Comment = "o_comment";
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