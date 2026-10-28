using FullAot.DataItems;
using FullAot.Util;

namespace FullAot.Tpch;

public static class Region
{
    public static readonly string RegionKey = "r_regionkey";
    public static readonly string Name = "r_name";
    public static readonly string Comment = "r_comment";
    public static Relation Load()
    {
        //Columns erstellen
        var cols = new List<Column>()
        {
            new ("r_regionkey", DataType.Int),
            new ("r_name", DataType.String),
            new ("r_comment", DataType.String)
        };
        
        //Via static helper: File laden und Tuple erstellen und in relation inserten
        var relation = TpchLoader.Load("region.tbl", new Schema(cols));
        //Relation zurückgeben.
        return relation;
    }
}