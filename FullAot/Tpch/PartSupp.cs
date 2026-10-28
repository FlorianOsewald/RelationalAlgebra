using FullAot.DataItems;
using FullAot.Util;

namespace FullAot.Tpch;

public static class PartSupp
{
    public static Relation Load()
    {
        //Columns erstellen
        var cols = new List<Column>()
        {
            new ("ps_partkey", DataType.Long),
            new ("ps_suppkey", DataType.Long),
            new ("ps_availqty", DataType.Long),
            new ("pw_supplycost",  DataType.Double),
            new ("ps_comment", DataType.String)
        };
        
        //Via static helper: File laden und Tuple erstellen und in relation inserten
        var relation = TpchLoader.Load("partsupp.tbl", new Schema(cols));
        //Relation zurückgeben.
        return relation;
    }
}