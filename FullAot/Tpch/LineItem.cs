using FullAot.DataItems;
using FullAot.Util;

namespace FullAot.Tpch;

public static class LineItem
{
    public static readonly string OrderKey = "l_orderkey";
    public static readonly string PartKey = "l_partkey";
    public static readonly string SuppKey = "l_suppkey";
    public static readonly string LineNumber = "l_linenumber";
    public static readonly string Quantity = "l_quantity";
    public static readonly string ExtendedPrice = "l_extendedprice";
    public static readonly string Discount = "l_discount";
    public static readonly string Tax = "l_tax";
    public static readonly string ReturnFlag = "l_returnflag";
    public static readonly string LineStatus = "l_linestatus";
    public static readonly string ShipDate = "l_shipdate";
    public static readonly string CommitDate = "l_commitdate";
    public static readonly string ReceiptDate = "l_receiptdate";
    public static readonly string Shipinstruct = "l_shipinstruct";
    public static readonly string ShipMode = "l_shipmode";
    public static readonly string Comment = "l_comment";
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