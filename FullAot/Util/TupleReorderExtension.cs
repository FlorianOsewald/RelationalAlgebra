namespace FullAot.Util;
using FullAot.DataItems;


public static class TupleReorderExtension
{
    public static HashSet<Tuple> Reorder(this IEnumerable<Tuple> right, int[] map)
    {
        HashSet<Tuple> correctRight = new();
        foreach (var tuple in right)
        {
            var correctedValues = new Object[tuple.Length];
            for (int i = 0; i < map.Length; i++)
            {
                correctedValues[i] = tuple[map[i]];
            }
            correctRight.Add(new Tuple(correctedValues));
        }
        return correctRight;
    }

    public static HashSet<Tuple> ReorderToMatch(this Relation right, Relation left)
    {
        if (!left.Schema.IsCompatibleWith(right.Schema))
        {
            throw new ArgumentException("Schemas do not match! Cannot Reorder to match Left Relation");
        }
        
        if (left.Schema.Equals(right.Schema))
        {
            //die Schema liegen komplett identisch
            return right.Tuples.ToHashSet();
        }
        //die Schema sind kompatible aber nicht positionsgetreu
        //erstelle ein mapping und re-order die tuple
        left.Schema.TryGetUnionMapping(right.Schema, out var map);
        return right.Tuples.Reorder(map);
    }
}