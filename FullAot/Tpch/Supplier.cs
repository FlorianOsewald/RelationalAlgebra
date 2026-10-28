using FullAot.DataItems;
using FullAot.Util;

namespace FullAot.Tpch;

public static class Supplier
{
    public static readonly string SuppKey = "s_suppkey";
    public static readonly string Name = "s_name";
    public static readonly string Address = "s_address";
    public static readonly string NationKey = "s_nationkey";
    public static readonly string Phone = "s_phone";
    public static readonly string AcctBal = "s_acctbal";
    public static readonly string Comment = "s_comment";
    public static Relation Load()
    {
        //Columns erstellen
        var cols = new List<Column>()
        {
            new("s_suppkey", DataType.Long),
            new("s_name", DataType.String),
            new("s_address", DataType.String),
            new("s_nationkey", DataType.Int),
            new("s_phone", DataType.String),
            new("s_acctbal",  DataType.Double),
            new("s_comment", DataType.String)
        };
        
        //Via static helper: File laden und Tuple erstellen und in relation inserten
        var relation = TpchLoader.Load("supplier.tbl", new Schema(cols));
        //Relation zurückgeben.
        return relation;
    }
}