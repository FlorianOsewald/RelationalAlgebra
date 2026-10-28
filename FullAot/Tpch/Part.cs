using FullAot.DataItems;
using FullAot.Util;

namespace FullAot.Tpch;

public static class Part
{
    public static readonly string PartKey = "p_partkey";
    public static readonly string Name = "p_name";
    public static readonly string Mfgr = "p_mfgr";
    public static readonly string Brand = "p_brand";
    public static readonly string Type = "p_type";
    public static readonly string Size = "p_size";
    public static readonly string Container = "p_container";
    public static readonly string RetailPrice = "p_retailprice";
    public static readonly string Comment = "p_comment";
    public static Relation Load()
    {
        //Columns erstellen
        var cols = new List<Column>()
        {
            new ("p_partkey", DataType.Long),
            new ("p_name", DataType.String),
            new ("p_mfgr", DataType.String),
            new ("p_brand", DataType.String),
            new ("p_type", DataType.String),
            new ("p_size", DataType.Int),
            new ("p_container",  DataType.String),
            new ("p_retailprice",  DataType.Double),
            new ("p_comment", DataType.String)
        };
        
        //Via static helper: File laden und Tuple erstellen und in relation inserten
        var relation = TpchLoader.Load("part.tbl", new Schema(cols));
        //Relation zurückgeben.
        return relation;
    }
}