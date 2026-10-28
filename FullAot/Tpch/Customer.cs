using FullAot.DataItems;
using FullAot.Util;

namespace FullAot.Tpch;

public static class Customer
{
    public static readonly string CustKey = "c_custkey";
    public static readonly string Name = "c_name";
    public static readonly string Address = "c_address";
    public static readonly string NationKey = "c_nationkey";
    public static readonly string Phone = "c_phone";
    public static readonly string AcctBal = "c_acctbal";
    public static readonly string MktSegment = "c_mktsegment";
    public static readonly string Comment = "c_comment";
    public static Relation Load()
    {
        //Columns erstellen
        var cols = new List<Column>()
        {
            new ("c_custkey", DataType.Long), 
            new ("c_name", DataType.String),
            new ("c_address",  DataType.String),
            new ("c_nationkey", DataType.Int),
            new ("c_phone", DataType.String),
            new ("c_acctbal", DataType.Double),
            new ("c_mktsegment", DataType.String),
            new ("c_comment", DataType.String)
        };
        
        //Via static helper: File laden und Tuple erstellen und in relation inserten
        var relation = TpchLoader.Load("customer.tbl", new Schema(cols));
        //Relation zurückgeben.
        return relation;
    }
}