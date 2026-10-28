using FullAot.DataItems;
using FullAot.Util;

namespace FullAot.Tpch;

public static class Customer
{
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