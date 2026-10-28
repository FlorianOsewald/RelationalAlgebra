using System.Globalization;
using FullAot.DataItems;
using Tuple = FullAot.DataItems.Tuple;

namespace FullAot.Util;

public static class TpchLoader
{
    public static Relation Load(string filenname, Schema schema)
    {
        var relation = new Relation(schema);
        var path = Path.Combine(Environment.GetEnvironmentVariable("TPCH_DATA_DIR"), filenname);
        
        foreach (var line in File.ReadLines(path))
        {
            var parts = line.Split('|');
            var values = new object?[schema.Count];
            for (int i = 0; i < schema.Count; i++)
                values[i] = Parse(parts[i], schema[i].Type);
            relation.Insert(new Tuple(values));
        }
        return relation;
    }

    private static object? Parse(string s, DataType type)
    {
        switch (type)
        {
            case DataType.Double:
            {
                return double.Parse(s, CultureInfo.InvariantCulture);
            }
            case DataType.Long:
            {
                return long.Parse(s,  CultureInfo.InvariantCulture);
            }
            case DataType.Int:
            {
                return int.Parse(s, CultureInfo.InvariantCulture);
            }
            case DataType.DateTime:
            {
                return DateTime.ParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            case DataType.Bool:
            {
                return bool.Parse(s);
            }
            default:
            {
                return s;
            }
        }
    }
}