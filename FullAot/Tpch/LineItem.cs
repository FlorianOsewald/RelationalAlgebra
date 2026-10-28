using FullAot.DataItems;
using FullAot.Util;

namespace FullAot.Tpch;

public static class LineItem
{
    public static Relation Load()
    {
        //Columns erstellen
        var cols = new List<Column>()
        {
            new ("l_orderkey", DataType.Long),
            new ("l_partkey", DataType.Long),
            new ("l_suppkey", DataType.Long),
            new ("l_linenumber",  DataType.Long),
            new ("l_quantity", DataType.Double),
            new ("l_extendedprice",  DataType.Double),
            new ("l_discount",  DataType.Double),
            new ("l_tax", DataType.Double),
            new ("l_returnflag", DataType.String),
            new ("l_linestatus",  DataType.String),
            new ("l_shipdate",   DataType.DateTime),
            new ("l_commitdate",   DataType.DateTime),
            new ("l_receiptdate",   DataType.DateTime),
            new ("l_shipinstruct",   DataType.String),
            new ("l_shipmode",    DataType.String),
            new ("l_comment", DataType.String)
        };
        
        //Via static helper: File laden und Tuple erstellen und in relation inserten
        var relation = TpchLoader.Load("lineitem.tbl", new Schema(cols));
        //Relation zurückgeben.
        return relation;
    }
}