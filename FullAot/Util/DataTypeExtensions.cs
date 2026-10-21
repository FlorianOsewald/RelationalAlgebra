using FullAot.DataItems;

namespace FullAot.Util;

public static class DataTypeExtensions
{
    public static bool IsNumeric(this DataType type) =>
        type is DataType.Int or DataType.Long or DataType.Double;

    public static DataType Promote(DataType a, DataType b) =>
        (a, b) switch
        {
            (DataType.Double, _) or (_, DataType.Double) => DataType.Double,
            (DataType.Long, _) or (_, DataType.Long) => DataType.Long,
            _ => DataType.Int
        };
}